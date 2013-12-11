using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using mshtml;
using System.Reflection;

namespace MarkTexEdt.util
{
    public class Converter
    {
        WebBrowser w;

        public Converter(WebBrowser _w)
        {
            this.w = _w;

        }



        public void Update(string src="")
        {
            /*string html = "<html><head><meta charset='utf-8'><meta name='description' content='Rendered by MarkTex'><base href='file:///" +
                AppDomain.CurrentDomain.BaseDirectory.Replace('\\','/') +
                "resources/' /><link href='css/gfm.css' /></head><body><div id='dst'></div>" +
                "<textarea id='src' style='display:none;'>" +
                System.Security.SecurityElement.Escape(src.Replace("\r", "")) +
                "</textarea>" +
                "<script src='js/marked.js'></script>"+
                "<script src='js/config.js'></script>" +
                "</body></html>";
            w.NavigateToString(html);*/
            string url = "file:///" + AppDomain.CurrentDomain.BaseDirectory.Replace('\\', '/') + "resources/html/standard.html";
            w.Navigate(url);
        }
    }
}
