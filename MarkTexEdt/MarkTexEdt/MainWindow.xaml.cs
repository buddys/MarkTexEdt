﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging; 
using System.Windows.Navigation;
using System.Windows.Shapes;
using Awesomium.Core;
using Awesomium.Web;
using Microsoft.Win32;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.IO;
using MarkTexEdt.util;

namespace MarkTexEdt
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// 配置接口
        /// </summary>
        util.Config config;
        util.Converter converter;
        util.EditBasicFuction edit;
        util.HighLight highLight;
        util.CommandAndInsert commandAndInsert;
        string filePath;

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this.config = util.Config.ConfigInstance;

            this.edit = new util.EditBasicFuction(tbEditor);
            this.highLight = new util.HighLight(tbEditor);
            this.commandAndInsert = new util.CommandAndInsert(tbEditor);
        }

        /// <summary>
        /// 文件关联
        /// </summary>
        /// <param name="filePath"></param>
        public MainWindow(string filePath)
            : this()
        {
            this.filePath = filePath;
        }

        /// <summary>
        /// 窗口载入事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            config.RestoreWindow(this);
            tbEditor.Focus();   //窗口载入后，左边的编辑框获得焦点
            config.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(config_PropertyChanged);
            converter = new Converter(this.browser);
            if (this.filePath != null && this.filePath != "")
                edit.OpenFile(filePath);
        }

        /// <summary>
        /// 设置项改变时触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void config_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SynchroScroll")
            {
                UpdateView();
            }
        }

        /// <summary>
        /// 编辑窗口发生改动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbEditor_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateView();
        }

        /// <summary>
        /// 得到编辑框内当前显示的内容
        /// </summary>
        /// <returns>stringToDisplay</returns>
        private string getDisplaying()
        {
            string stringToDisplay = null;
            double offset = scrollView.ContentVerticalOffset;
            double height = scrollView.ExtentHeight;
            double vertical = scrollView.ScrollableHeight;
            string source = GetSource();
            if (source != null && source != "" && source != "\r\n")
            {
                string temp = source.Replace("\n", "");
                int line = source.Count() - temp.Count();
                int firstIndex = (Int32)(line * offset / (height + 0.00001));
                if (firstIndex == -1) firstIndex = 0;
                int lastIndex = (Int32)(line * (offset + height - vertical) / (height + 0.00001)) + 1;
                string[] split = source.Split(new Char[] { '\n' });
                for (int i = firstIndex; i <= lastIndex; i++)
                {
                    stringToDisplay = stringToDisplay + split[i] + "\n";
                }
            }
            return stringToDisplay;
        }

        /// <summary>
        /// 编辑窗口滚动状态发生变化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            UpdateView();
        }

        /// <summary>
        /// 更新显示
        /// </summary>
        private void UpdateView()
        {
            if (!this.IsLoaded) return;
            converter.Source = config.SynchroScroll ? getDisplaying() : GetSource();
        }

        /// <summary>
        /// 浏览器页面地址发生改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void browser_AddressChanged(object sender, UrlEventArgs e)
        {
            //string currentUrl = browser.Source.ToString();
            ////Console.WriteLine(currentUrl);

            //converter.Source = getDisplaying();
            //if (currentUrl != "file:///resources/html/template.html")
            //{
            //    RegistryKey key = Registry.ClassesRoot.OpenSubKey(@"http\shell\open\command\");
            //    string s = key.GetValue("").ToString();
            //    Regex reg = new Regex("\"([^\"]+)\"");
            //    MatchCollection matchs = reg.Matches(s);
            //    string filename = "";
            //    if (matchs.Count > 0)
            //    {
            //        filename = matchs[0].Groups[1].Value;
            //        System.Diagnostics.Process.Start(filename, currentUrl); 
            //    }
            //}
            //browser.Source = new Uri("file:///resources/html/template.html");
            ////browser.Reload(false);

        }



        /// <summary>
        /// 打开文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Open_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = Config.MarkdownFileFilter;
            open.Multiselect = false;

            if ((bool)open.ShowDialog())
            {
                edit.OpenFile(open.FileName);
            }
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (config.CurrentFilePath != null && config.CurrentFilePath != "")
            {
                edit.SaveFile(config.CurrentFilePath);
            }
            else
            {
                SaveFileDialog save = new SaveFileDialog();
                save.Filter = Config.MarkdownFileFilter;
                save.FileName = Config.DefaultFileName;
                if ((bool)save.ShowDialog())
                {
                    edit.SaveFile(save.FileName);
                    config.CurrentFilePath = save.FileName;
                }
            }
        }

        /// <summary>
        /// 另存为
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = Config.MarkdownFileFilter;
            save.FileName = config.SafeFileName;
            if ((bool)save.ShowDialog())
            {
                edit.SaveFile(save.FileName);
            }
        }

        /// <summary>
        /// 点击设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Config_Click(object sender, RoutedEventArgs e)
        {
            view.Config c = new view.Config();
            c.Owner = this;
            c.ShowDialog();
        }

        /// <summary>
        /// 窗口关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            config.SaveWindow(this);
            config.Save();
        }

        /// <summary>
        /// 得到编辑框中的文本
        /// </summary>
        /// <returns>文本字符串</returns>
        private string GetSource()
        {
            return new TextRange(tbEditor.Document.ContentStart, tbEditor.Document.ContentEnd).Text.ToString();
        }

        private void tbEditor_KeyUp(object sender, KeyEventArgs e)
        {
            highLight.HighLight5();
            highLight.HighLightMultiLines();
        }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog pd = new PrintDialog();
            if ((pd.ShowDialog() == true))
            {
                //use either one of the below      
                pd.PrintVisual(tbEditor as Visual, "printing as visual");
                pd.PrintDocument((((IDocumentPaginatorSource)tbEditor.Document).DocumentPaginator), "printing as paginator");
            }
        }

        #region 编辑器工具栏


        /// <summary>
        /// 撤销
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Undo_Click(object sender, RoutedEventArgs e)
        {
            edit.Undo();
        }
        /// <summary>
        /// 重做
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Redo_Click(object sender, RoutedEventArgs e)
        {
            edit.Redo();
        }

        /// <summary>
        /// 加粗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Bold_Click(object sender, RoutedEventArgs e)
        {
            edit.Bold();
        }
        /// <summary>
        /// 对选择的文本进行斜体，若是没选择，则接下去输入的字符会是斜体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Italic_Click(object sender, RoutedEventArgs e)
        {
            edit.Italic();
        }

        /// <summary>
        /// 全选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectAll_Click(object sender, RoutedEventArgs e)
        {
            edit.SelectAll();
        }
        /// <summary>
        ///  复制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            edit.Copy();
        }
        /// <summary>
        /// 剪切
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cut_Click(object sender, RoutedEventArgs e)
        {
            edit.Cut();
        }

        /// <summary>
        /// 粘贴
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Paste_Click(object sender, RoutedEventArgs e)
        {
            edit.Paste();
        }

        private void Code_Click(object sender, RoutedEventArgs e)
        {
            commandAndInsert.Add_Code();

        }

        private void HeadLine1_Click(object sender, RoutedEventArgs e)
        {
            commandAndInsert.Add_HeadLine1();
        }

        private void HeadLine2_Click(object sender, RoutedEventArgs e)
        {
            commandAndInsert.Add_HeadLine2();
        }

        private void HeadLine3_Click(object sender, RoutedEventArgs e)
        {
            commandAndInsert.Add_HeadLine3();
        }

        private void HeadLine4_Click(object sender, RoutedEventArgs e)
        {
            commandAndInsert.Add_HeadLine4();
        }

        private void Horizontal_Scale_Click(object sender, RoutedEventArgs e)
        {
            commandAndInsert.Add_Horizontal_Scale();
        }

        private void Graphic_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Time_Click(object sender, RoutedEventArgs e)
        {
            edit.Get_Current_Time();
        }

        private void Add_Link_Click(object sender, RoutedEventArgs e)
        {
            edit.Add_Link();
        }

        private void Decrease_Font_Size_Click(object sender, RoutedEventArgs e)
        {
            if (tbEditor.FontSize > 5)
            {
                tbEditor.FontSize -= 5;
            }
            else
            {
                tbEditor.FontSize = 5;
            }
        }

        private void Increase_Font_Size_Click(object sender, RoutedEventArgs e)
        {
            //commandAndInsert.Increase_Font_Size();
            if (tbEditor.FontSize < 50)
            {
                tbEditor.FontSize += 5;
                if (tbEditor.FontSize > 50)
                {
                    tbEditor.FontSize = 50;
                }
            }
            else
            {
                tbEditor.FontSize = 50;
            }
        }

        private void Strike_Through_Text_Click(object sender, RoutedEventArgs e)
        {
            edit.Strike_Through_Text();
        }

        private void Header_Click(object sender, RoutedEventArgs e)
        {
            commandAndInsert.Add_Head();
        }

        private void Center_Click(object sender, RoutedEventArgs e)
        {
            edit.Set_Center();
        }

        private void Add_Picture_Click(object sender, RoutedEventArgs e)
        {
            commandAndInsert.Add_Picture();
        }

        private void Add_Code_Click(object sender, RoutedEventArgs e)
        {
            commandAndInsert.Add_Code();
        }

        private void Add_Math_Formula(object sender, RoutedEventArgs e)
        {
            commandAndInsert.Add_Math_Formula();
        }

        private void Right_Click(object sender, RoutedEventArgs e)
        {
            edit.Set_Right();
        }

        private void Add_Quotation_Click(object sender, RoutedEventArgs e)
        {
            edit.Add_Quotation();
        }

        private void Add_Time_Stamp_Click(object sender, RoutedEventArgs e)
        {
            edit.Get_Current_Time();
        }

        #endregion

        #region 编辑命令

        private void CommandBinding_Increase_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (tbEditor.FontSize > 50)
            {
                e.CanExecute = false;
            }
            else
            {
                e.CanExecute = true;
            }
        }

        private void CommandBinding_Increase_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            tbEditor.FontSize += 5;
        }

        private void CommandBinding_Decrease_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (tbEditor.FontSize <= 5)
            {
                e.CanExecute = false;
            }
            else
            {
                e.CanExecute = true;
            }
        }

        private void CommandBinding_Decrease_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            tbEditor.FontSize -= 5;
        }

        private void CommandBinding_Bold_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandBinding_Bold_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            edit.Bold();
        }

        private void CommandBinding_Italic_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandBinding_Italic_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            edit.Italic();
        }

        private void CommandBinding_Code_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandBinding_Code_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            commandAndInsert.Add_Code();
        }

        private void CommandBinding_HeadLine1_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandBinding_HeadLine1_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            commandAndInsert.Add_HeadLine1();
        }

        private void CommandBinding_HeadLine2_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandBinding_HeadLine2_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            commandAndInsert.Add_HeadLine2();
        }

        private void CommandBinding_HeadLine3_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandBinding_HeadLine3_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            commandAndInsert.Add_HeadLine3();
        }

        private void CommandBinding_HeadLine4_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandBinding_HeadLine4_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            commandAndInsert.Add_HeadLine4();
        }

        private void CommandBinding_HyperLink_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandBinding_HyperLink_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            edit.Add_Link();
        }

        private void CommandBinding_Time_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandBinding_Time_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            edit.Get_Current_Time();
        }

        private void CommandBinding_Horizontal_Scale_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandBinding_Horizontal_Scale_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            commandAndInsert.Add_Horizontal_Scale();
        }

        private void CommandBinding_Undo_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandBinding_Undo_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            edit.Redo();
        }

        private void CommandBinding_Open_File_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandBinding_Open_File_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            Open_Click(sender, e);
        }

        private void CommandBinding_Save_File_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandBinding_Save_File_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            Save_Click(sender, e);

        }

        private void CommandBinding_Head_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandBinding_Head_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            commandAndInsert.Add_Head();
        }

        private void CommandBinding_Set_Center_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandBinding_Set_Center_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            edit.Set_Center();
        }

        private void CommandBinding_Set_Right_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;


        }

        private void CommandBinding_Set_Right_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            edit.Set_Right();
        }

        private void CommandBinding_Picture_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandBinding_Picture_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            commandAndInsert.Add_Picture();
        }

        #endregion

        /// <summary>
        /// 导出为 PDF
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportToPdf(object sender, RoutedEventArgs e)
        {
            browser.PrintToFile(Config.CachePath, PrintConfig.Default);
        }

        /// <summary>
        /// 导出为 HTML
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExportToHtml(object sender, RoutedEventArgs e)
        {
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = Config.HtmlFileFilter;
            save.FileName = config.SafeFileName;

            if ((bool)save.ShowDialog())
            {
                converter.SaveAsHtml(save.FileName);
            }
        }

        /// <summary>
        /// 导出PDF完成，移动至指定位置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void browser_PrintComplete(object sender, PrintCompleteEventArgs e)
        {
            FileInfo temp = new FileInfo(e.Files[0]);
            SaveFileDialog save = new SaveFileDialog();
            save.FileName = config.SafeFileName;
            save.Filter = Config.PdfFileFilter;
            if ((bool)save.ShowDialog())
            {
                temp.CopyTo(save.FileName, true);
                temp.Delete();
            }
        }

        /// <summary>
        /// 去除toolbar右侧的溢出按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolBar_Loaded(object sender, RoutedEventArgs e)
        {
            ToolBar toolBar = sender as ToolBar;
            var overflowGrid = toolBar.Template.FindName("OverflowGrid", toolBar) as FrameworkElement;
            if (overflowGrid != null)
            {
                overflowGrid.Visibility = Visibility.Collapsed;
            }
        }

        /// <summary>
        /// 关闭窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 使用默认浏览器预览
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PreviewInBrowser_Click(object sender, RoutedEventArgs e)
        {
            converter.SaveAsHtml(Config.CacheFilePath);
            System.Diagnostics.Process.Start(Config.CacheFilePath);
        }

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(Config.HelpUrl);
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(Config.HomeUrl);
        }

        private void browser_DocumentReady(object sender, UrlEventArgs e)
        {
            //由命令行参数打开程序时，未能更新显示，此时作一次更新。
            if (filePath != null && filePath != "")
                converter.Update();
        }
    }
}
