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
        util.Config config;
        

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
            this.DataContext = this.config = util.Config.ConfigInstance;
            w.Source = new Uri("file:///resources/html/template.html");
        }
        public void Update(string src="")
        {
            string options = this.SetOptions();
            JSObject jsobject = w.CreateGlobalJavascriptObject("jsobject");
            if (src == null || src == "")
            src = " " + src;                        //src不能为空 否则报错
            string inputString = "func('" + Uri.EscapeUriString(src) + "'," + '"' + Uri.EscapeUriString(options) + '"' + ")";
            //Console.WriteLine(inputString);
            w.ExecuteJavascript(inputString);
            
        }
        private string ReturnTrueOrFalse(bool option) {
            if (option)
            {
                return "true";
            }
            return "false";
        }
        private string SetOptions() { 
            string options;
            options = "gfm:" + ReturnTrueOrFalse(config.Gfm) + ",";
            options += ("tables:" + ReturnTrueOrFalse(config.Tables) + ",");
            options += ("breaks:" + ReturnTrueOrFalse(config.Breaks) + ",");
            options += ("todo:" + ReturnTrueOrFalse(config.Todo) + ",");
            options += ("marktex:" + ReturnTrueOrFalse(config.MarkTex) + ",");
            options += ("smartlist:" + ReturnTrueOrFalse(config.SmartList) + ",");
            options += ("smartquote:" + ReturnTrueOrFalse(config.SmartQuote) + ",");
            options += ("align:" + ReturnTrueOrFalse(config.Align) + ",");
            options += ("pedantic:" + ReturnTrueOrFalse(config.Pedantic) + ",");
            options += ("sanitize:" + ReturnTrueOrFalse(config.Sanitize) + ",");
            options += ("smartypants:" + ReturnTrueOrFalse(config.SmartyPants) + ",");
            options += Properties.Resources.footer;
            return options;
        }

        public Config DataContext { get; set; }
    }
}
