using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using MarkTexEdt.Properties;
using System.ComponentModel;

namespace MarkTexEdt.util
{
    /// <summary>
    /// 配置文件接口
    /// </summary>
    public class Config: util.ObservableClass
    {
        /// <summary>
        /// Singleton
        /// </summary>
        private Config()
        {
            //设置发生改变时通知视图元素
            Settings.Default.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(Config_PropertyChanged);
        }

        /// <summary>
        /// Singleton 接口
        /// </summary>
        static Config configInstance;
        static public Config ConfigInstance
        {
            get
            {
                if (configInstance == null)
                {
                    configInstance = new Config();
                }
                return configInstance;
            }
        }

        /// <summary>
        /// 保存配置到文件
        /// </summary>
        public void Save()
        {
            Settings.Default.Save();
        }

        /// <summary>
        /// 从文件恢复配置
        /// </summary>
        public void Revert()
        {
            Settings.Default.Reload();
        }

        /// <summary>
        /// 恢复初始设置
        /// </summary>
        public void RestoreDefault()
        {
            Settings.Default.Reset();
        }


        #region 工具

        /// <summary>
        /// 缓存目录
        /// </summary>
        public string CachePath
        {
            get
            {
                return System.Environment.GetFolderPath(System.Environment.SpecialFolder.InternetCache);
            }
        }

        //markdown后缀
        string[] markdownFilter = new string[] { ".md", ".markdown" };

        //markdown过滤器
        public string MarkdownFileFilter
        {
            get
            {
                string str = "Markdown文件|";
                foreach (string s in markdownFilter)
                    str += "*" + s + ";";
                str += @"|所有文件|*.*";
                return str;
            }
        }

        //html后缀
        string[] htmlFilter = new string[] { ".html", ".htm" };

        //html过滤器
        public string HtmlFileFilter
        {
            get
            {
                string str = "HTML 文件|";
                foreach (string s in markdownFilter)
                    str += "*" + s + ";";
                str += @"|所有文件|*.*";
                return str;
            }
        }

        #endregion


        #region 视图

        /// <summary>
        /// 通知视图元素
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Config_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NotifyPropertyChanged(e.PropertyName);
        }

        /// <summary>
        /// 菜单栏可见性
        /// </summary>
        public Visibility MenuBarVisibility
        {
            get { return Settings.Default.menuBarVisibility; }
            set { Settings.Default.menuBarVisibility = value; 
            }
        }        

        /// <summary>
        /// 恢复窗口状态
        /// </summary>
        /// <param name="w"></param>
        public void RestoreWindow(Window w)
        {
            if (Settings.Default.MainWindowSize !=null && !Settings.Default.MainWindowSize.IsEmpty)
            {
                w.Height = Settings.Default.MainWindowSize.Height;
                w.Width = Settings.Default.MainWindowSize.Width;
            }
            if (!Settings.Default.MainWindowLocation.IsEmpty)
            {
                w.Left = Settings.Default.MainWindowLocation.X;
                w.Top = Settings.Default.MainWindowLocation.Y;
            }
            w.WindowState = Settings.Default.MainWindowState;
        }

        /// <summary>
        /// 保存窗口状态
        /// </summary>
        /// <param name="w"></param>
        public void SaveWindow(Window w)
        {
            Settings.Default.MainWindowSize = new System.Drawing.Size((int)w.Width, (int)w.Height);
            Settings.Default.MainWindowLocation = new System.Drawing.Point((int)w.Left, (int)w.Top);
            Settings.Default.MainWindowState = w.WindowState;
        }        

        #endregion


        #region 编辑器

        /// <summary>
        /// 是否同步滚动
        /// </summary>
        public bool SynchroScroll
        {
            get
            {
                return Settings.Default.SynchroScroll;
            }
            set
            {
                Settings.Default.SynchroScroll = value;
            }
        }

        #endregion


        #region 编译器


        public bool Gfm
        {
            get
            {
                return Settings.Default.Gfm;
            }
            set
            {
                Settings.Default.Gfm = value;
            }
        }

        public bool Tables
        {
            get
            {
                return Settings.Default.Tables;
            }
            set
            {
                Settings.Default.Tables = value;
            }
        }
        public bool Todo
        {
            get
            {
                return Settings.Default.Todo;
            }
            set
            {
                Settings.Default.Todo = value;
            }
        }

        public bool Breaks
        {
            get
            {
                return Settings.Default.Breaks;
            }
            set
            {
                Settings.Default.Breaks = value;
            }
        }

        public bool MarkTex
        {
            get
            {
                return Settings.Default.MarkTex;
            }
            set
            {
                Settings.Default.MarkTex = value;
            }
        }

        public bool SmartList
        {
            get
            {
                return Settings.Default.SmartList;
            }
            set
            {
                Settings.Default.SmartList = value;
            }
        }

        public bool SmartQuote
        {
            get
            {
                return Settings.Default.SmartQuote;
            }
            set
            {
                Settings.Default.SmartQuote = value;
            }
        }

        public bool Align
        {
            get
            {
                return Settings.Default.Align;
            }
            set
            {
                Settings.Default.Align = value;
            }
        }

        public bool Pedantic
        {
            get
            {
                return Settings.Default.Pedantic;
            }
            set
            {
                Settings.Default.Pedantic = value;
            }
        }

        public bool Sanitize
        {
            get
            {
                return Settings.Default.Sanitize;
            }
            set
            {
                Settings.Default.Sanitize = value;
            }
        }

        public bool SmartyPants
        {
            get
            {
                return Settings.Default.SmartyPants;
            }
            set
            {
                Settings.Default.SmartyPants = value;
            }
        }


        #endregion

    }
}
