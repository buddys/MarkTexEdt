using System;
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
using Microsoft.Win32;
//using System.Windows.Forms
using Awesomium.Core;
using Awesomium.Web;

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

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this.config = util.Config.ConfigInstance;
            this.converter = new util.Converter(this.browser);

            this.edit = new util.EditBasicFuction(tbEditor);
            this.highLight = new util.HighLight(tbEditor);
            this.commandAndInsert = new util.CommandAndInsert(tbEditor);
            //browser.Source = new Uri("file:///test.html");
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
        }
       
        /// <summary>
        /// 点击关于
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void About_Click(object sender, RoutedEventArgs e)
        {
            view.About abt = new view.About();
            abt.ShowDialog();
        }

        /// <summary>
        /// 编辑窗口发生改动
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbEditor_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Uri uri = new Uri(@"http://www.baidu.com", UriKind.Absolute);

            //预览HTML页面
            //wbViewer.Navigate(uri);
            //Console.WriteLine(GetSource());
            //converter.Update(GetSource());
            converter.Update(GetSource());
         
        }
       /* private void Update(string src)
        {
            JSObject jsobject = browser.CreateGlobalJavascriptObject("jsobject");
            src = src.Replace("\n", @"\n").Replace("\r", "").Replace("'", @"\'");
            string source = "func('" + src + "')";
            Console.WriteLine(source);
            browser.ExecuteJavascript(source);
        }*/
        
        /// <summary>
        /// 打开文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Open_Click(object sender, RoutedEventArgs e)
        {
            edit.Open_File();
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            //TODO: save current file
            edit.Save_File();
        }

        /// <summary>
        /// 另存为
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            edit.Save_File();
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
        /// 窗口关闭事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            config.SaveWindow(this);
            config.SaveToFile();
        }

        /// <summary>
        /// 得到编辑框中的文本
        /// </summary>
        /// <returns>文本字符串</returns>
        private string GetSource()
        {
            if ((new TextRange(tbEditor.Document.ContentStart, tbEditor.Document.ContentEnd)).Text.ToString() != "")
                return (new TextRange(tbEditor.Document.ContentStart, tbEditor.Document.ContentEnd)).Text.ToString();
            else
                return "";
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
        /// 实现全选功能
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


        private void tbEditor_KeyUp(object sender, KeyEventArgs e)
        {
            highLight.HighLight5();
            highLight.HighLightMultiLines();
         }

        private void Print_Click(object sender, RoutedEventArgs e)
        {
            edit.Print();
        }

        private void Time_Click(object sender, RoutedEventArgs e)
        {           
            edit.Get_Current_Time();
        }

        private void Add_Link_Click(object sender, RoutedEventArgs e)
        {
            edit.Add_Link();
        }

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

       
        private void Graphic_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Horizontal_Scale_Click(object sender, RoutedEventArgs e)
        {
            commandAndInsert.Add_Horizontal_Scale();
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

        private void CommandBinding_Open_File_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandBinding_Open_File_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            edit.Open_File();
        }

        private void CommandBinding_Save_File_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void CommandBinding_Save_File_Execute(object sender, ExecutedRoutedEventArgs e)
        {
            edit.Save_File();           
        }

       
    }
}
