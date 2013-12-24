using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MarkTexEdt
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if (e.Args.Length > 0)
            {
                MainWindow w = new MainWindow(e.Args[0]);
                w.Show();
            }
            else
            {
                MainWindow w = new MainWindow();
                w.Show();
            }
        }   
    }
}
