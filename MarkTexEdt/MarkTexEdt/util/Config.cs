using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MarkTexEdt.util
{
    /// <summary>
    /// 配置文件接口
    /// </summary>
    class Config
    {
        //Markdown文件后缀
        static string[] markdownFilter = new string[]{".md",".markdown"};

        //Markdown文件过滤器
        //用于文件对话空
        public static string MarkdownFileFilter
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
    }
}
