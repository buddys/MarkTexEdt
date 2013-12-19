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

        public Converter(WebControl _w)
        {
            this.w = _w;
            w.Source = new Uri("file:///resources/html/template.html");
        }
        public void Update(string src="")
        {
            JSObject jsobject = w.CreateGlobalJavascriptObject("jsobject");
            //src = src.Replace("\n", @"\n").Replace("\r", "").Replace("'", @"\'");
            string source = "func('" + Uri.EscapeUriString(src) + "')";
         //   Console.WriteLine(source);
            w.ExecuteJavascript(source);
            
        }
    }
}
