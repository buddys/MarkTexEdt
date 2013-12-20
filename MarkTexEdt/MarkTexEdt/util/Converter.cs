using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using mshtml;
using System.Reflection;
using Awesomium.Core;
using Awesomium.Web;
using Awesomium.Windows.Controls;

namespace MarkTexEdt.util
{
    public class Converter
    {
        WebControl w;

        string source;

        public string Source
        {
            get { return source; }
            set {
                if (value == source) return;
                else
                {
                    source = value;
                    Update(Source);
                }
            }
        }

        public Converter(WebControl _w)
        {
            this.w = _w;
            w.Source = new Uri("file:///resources/html/template.html");
        }
        public void Update(string src="")
        {
            JSObject jsobject = w.CreateGlobalJavascriptObject("jsobject");
            //src = src.Replace("\n", @"\n").Replace("\r", "").Replace("'", @"\'");
            //string source = "func('" + Uri.EscapeUriString(src) + "')";
            //Console.WriteLine(source);
            if (src == null || src == "")
            src = " " + src;                        //src不能为空 否则报错
            string temp = "func('" + Uri.EscapeUriString(src) + "')";
            w.ExecuteJavascript(temp);
            
        }
    }
}
