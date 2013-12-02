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
            Uri uri = new Uri(@"http://www.baidu.com", UriKind.Absolute);

            //预览HTML页面
            wbViewer.Navigate(uri);

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

    }
}
