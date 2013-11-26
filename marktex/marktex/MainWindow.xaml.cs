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

namespace marktex
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// 前台窗口的配置接口
        /// </summary>
        WindowConfig windowConfig;

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗口载入事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.DataContext = windowConfig = new WindowConfig(this);
        }

        /// <summary>
        /// 点击关于
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void About_Click(object sender, RoutedEventArgs e)
        {
            window.About abt = new window.About();
            abt.ShowDialog();
        }


        /// <summary>
        /// 管理MainWindow的配置
        /// </summary>
        public class WindowConfig : util.ObservableClass
        {
            public MainWindow mainWindow;

            public WindowConfig(MainWindow w)
            {
                mainWindow = w;
            }

            /// <summary>
            /// 菜单栏可见性
            /// </summary>
            Visibility menuBarVisibility = Visibility.Visible;
            public Visibility MenuBarVisibility
            {
                get { return menuBarVisibility; }
                set
                {
                    menuBarVisibility = value;
                    NotifyPropertyChanged("MenuBarVisibility");
                }
            }
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

    }
}
