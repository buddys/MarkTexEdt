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

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = config = util.Config.ConfigInstance;
        }

        /// <summary>
        /// 窗口载入事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            config.RestoreWindow(this);
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
           // Uri uri = new Uri(@"http://www.baidu.com", UriKind.Absolute);

            //预览HTML页面
          //  wbViewer.Navigate(uri);

        }


        /// <summary>
        /// 新建文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            //TODO: clear current content
            tbEditor.IsEnabled = true;
        }


        /// <summary>
        /// 打开文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Open_Click(object sender, RoutedEventArgs e)
        {
           OpenFileDialog ofd = new OpenFileDialog();
            ofd.CheckFileExists = true;
            ofd.Filter = config.MarkdownFileFilter;
            ofd.Multiselect = false;
            if ((bool)ofd.ShowDialog())
            {
                //TODO: Open file ofd.FileName                
            }
        }

        /// <summary>
        /// 保存文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            //TODO: save current file
        }

        /// <summary>
        /// 另存为
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveAs_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.CheckPathExists = true;
            sfd.Filter = config.MarkdownFileFilter;
            if ((bool)sfd.ShowDialog())
            {
                //TODO: Save file sfd.FileName
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
        /// 撤销
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Undo_Click(object sender, RoutedEventArgs e)
        {
            if (tbEditor.CanUndo == true)
            {
                tbEditor.Undo();
            }
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
        /// 对选择的文本进行加粗，若是没选择，则对接下去输入的字符加粗
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Bold_Click(object sender, RoutedEventArgs e)
        {
            TextSelection selection = tbEditor.Selection;
            // 如果没有选择文本，则按选取了一个普通字体的文本来处理
            FontWeight currentState = FontWeights.Normal;
            // 尝试获取所选文本的粗体状态
            if (selection.GetPropertyValue(Run.FontWeightProperty) !=
              DependencyProperty.UnsetValue)
            {
                currentState = (FontWeight)selection.GetPropertyValue(Run.FontWeightProperty);
            }
            if (currentState == FontWeights.Normal)
            {
                selection.ApplyPropertyValue(Run.FontWeightProperty, FontWeights.Bold);
            }
            else
            {
                selection.ApplyPropertyValue(Run.FontWeightProperty, FontWeights.Normal);
            }

            TextRange a = new TextRange(tbEditor.Document.ContentStart, tbEditor.Document.ContentEnd);           
            String strText = a.Text.ToString();           
            TextPointer tpEnd = tbEditor.Document.ContentStart.GetPositionAtOffset(strText.Length);
            TextRange b = new TextRange(tbEditor.Document.ContentStart, tpEnd);
            String strDes = "**" + b.Text + "**";
            Console.WriteLine(strDes);   
                   
            // 这个细节很关键，实现将焦点返回给文本框，这样用户将可以直接再更改设置的字体
            tbEditor.Focus();
        }
        /// <summary>
        /// 对选择的文本进行斜体，若是没选择，则接下去输入的字符会是斜体
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Italic_Click(object sender, RoutedEventArgs e)
        {
            TextSelection selection = tbEditor.Selection;
            // 如果没有选择文本，则按选取了一个普通字体的文本来处理
            //FontWeight currentState = FontWeights.Normal;
            FontStyle currentState = FontStyles.Normal;
            // 尝试获取所选文本的粗体状态
           if (selection.GetPropertyValue(Run.FontStyleProperty) !=
              DependencyProperty.UnsetValue)
            {
                  currentState = (FontStyle)selection.GetPropertyValue(Run.FontStyleProperty);  
                //currentState = (FontWeight)selection.GetPropertyValue(Run.FontWeightProperty);
            }
            if (currentState == FontStyles.Normal)
            {
                selection.ApplyPropertyValue(Run.FontStyleProperty, FontStyles.Italic);
            }
            else
            {
                selection.ApplyPropertyValue(Run.FontStyleProperty, FontStyles.Normal);
               // selection.ApplyPropertyValue(Run.FontWeightProperty, FontWeights.Normal);
            }

            TextRange a = new TextRange(tbEditor.Document.ContentStart, tbEditor.Document.ContentEnd);
            String strText = a.Text.ToString();
            TextPointer tpEnd = tbEditor.Document.ContentStart.GetPositionAtOffset(strText.Length);
            TextRange b = new TextRange(tbEditor.Document.ContentStart, tpEnd);
            String strDes = "*" + b.Text + "*";
            Console.WriteLine(strDes);   

            // 这个细节很关键，实现将焦点返回给文本框，这样用户将可以直接再更改设置的字体
            tbEditor.Focus();
        }

        private void Left_Click(object sender, RoutedEventArgs e)
        {/*
            tbEditor.LineLeft();
            Console.Write("left");
            tbEditor.Focus();*/
        }

        private void Center_Click(object sender, RoutedEventArgs e)
        {
        }

        private void Right_Click(object sender, RoutedEventArgs e)
        {
          //  TextSelection selection = tbEditor.Selection;
           // tbEditor.HorizontalContentAlignment = HorizontalAlignment.Right;
        /*    if (tbEditor.HorizontalContentAlignment == HorizontalAlignment.Right)
            {
                tbEditor.HorizontalContentAlignment = HorizontalAlignment.Left;
                //  tbEditor.Content = "Control horizontal alignment changes from left to right.";

            }
            else
            {
                tbEditor.HorizontalContentAlignment = HorizontalAlignment.Right;
                // tbEditor.Content = "HorizontalContentAlignment";
            }*/

          //  tbEditor.Focus();
        }
        /// <summary>
        /// 重做
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Redo_Click(object sender, RoutedEventArgs e)
        {
            if (tbEditor.CanRedo == true)
            {
                tbEditor.Redo();
            }
        }
        /// <summary>
        /// 实现全选功能
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectAll_Click(object sender, RoutedEventArgs e)
        {
            //TextSelection selection = tbEditor.Selection;
            tbEditor.SelectAll();
        }
        /// <summary>
        ///  复制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Copy_Click(object sender, RoutedEventArgs e)
        {
            tbEditor.Copy();
        }
        /// <summary>
        /// 剪切
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cut_Click(object sender, RoutedEventArgs e)
        {
            tbEditor.Cut();
        }
        /// <summary>
        /// 粘贴
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Paste_Click(object sender, RoutedEventArgs e)
        {
            tbEditor.Paste();
        }

    }
}
