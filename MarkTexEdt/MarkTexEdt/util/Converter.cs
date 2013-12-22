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
        WebControl webControl;
        util.Config config;

        string source;
        public string Source
        {
            get { return source; }
            set
            {
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
            this.webControl = _w;
            this.config = util.Config.ConfigInstance;
            webControl.Source = new Uri("file:///resources/html/template.html");
        }

        public void Update(string src = "")
        {
            string options = this.GetOptions();
            JSObject jsobject = webControl.CreateGlobalJavascriptObject("jsobject");
            //src不能为空 否则报错
            if (src == null || src == "")
                src = " ";
            string inputString = "func('" + Uri.EscapeUriString(src) + "'," + options + ")";
            webControl.ExecuteJavascript(inputString);
        }

        private string GetOptions()
        {
            string options = "{";
            options = "gfm:" + config.Gfm.ToString() + ",";
            options += ("tables:" + config.Tables.ToString() + ",");
            options += ("breaks:" + config.Breaks.ToString() + ",");
            options += ("todo:" + config.Todo.ToString() + ",");
            options += ("marktex:" + config.MarkTex.ToString() + ",");
            options += ("smartlist:" + config.SmartList.ToString() + ",");
            options += ("smartquote:" + config.SmartQuote.ToString() + ",");
            options += ("align:" + config.Align.ToString() + ",");
            options += ("pedantic:" + config.Pedantic.ToString() + ",");
            options += ("sanitize:" + config.Sanitize.ToString() + ",");
            options += ("smartypants:" + config.SmartyPants.ToString() + ",");
            options += Properties.Resources.footer + "}";
            return options;
        }
    }
}