﻿using System;
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
            Settings.Default.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(Default_PropertyChanged);
        }

        /// <summary>
        /// Singleton 接口
        /// </summary>
        static Config configInstance;
        static public Config ConfigInstance
        {
            get {
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
        public void SaveToFile()
        {
            Settings.Default.Save();
        }

        #region 视图

        /// <summary>
        /// 通知视图元素
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Default_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NotifyPropertyChanged(e.PropertyName);
        }

        /// <summary>
        /// 菜单栏可见性
        /// </summary>
        Visibility menuBarVisibility = Visibility.Visible;
        public Visibility MenuBarVisibility
        {
            get { return menuBarVisibility; }
            set { menuBarVisibility = value; }
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

        #endregion


        #region 转换器

        //文件后缀
        string[] markdownFilter = new string[]{".md",".markdown"};

        //文件后缀过滤器，用于文件对话空
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

        /// <summary>
        /// 默认转换器
        /// </summary>
        public int ConverterType
        {
            get
            {
                return Settings.Default.ConverterType;
            }
            set
            {
                Settings.Default.ConverterType = value;
            }
        }

        #endregion

    }
}