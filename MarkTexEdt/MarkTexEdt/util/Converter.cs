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
using System.IO;

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
            webControl.Source = new Uri(Config.TemplateUrl);
        }

        public void Update(string src)
        {
            string options = this.GetOptions();
            JSObject jsobject = webControl.CreateGlobalJavascriptObject("jsobject");
            if ( src ==null ) src = "";
            string inputString = "update('" + Uri.EscapeUriString(src) + "'," + options + ")";
            webControl.ExecuteJavascript(inputString);
        }

        public void SaveAsHtml(string filename)
        {
            JSObject jsobject = webControl.CreateGlobalJavascriptObject("jsobject");
            string result = webControl.ExecuteJavascriptWithResult("exportHTML()");
            //Console.WriteLine(result);
            StreamWriter sw = new StreamWriter(filename);
            sw.Write(result);
            sw.Close();
        }

        private string GetOptions()
        {
            string options = "{";
            options += "gfm:" + config.Gfm.ToString().ToLower() + ",";
            options += ("tables:" + config.Tables.ToString().ToLower() + ",");
            options += ("breaks:" + config.Breaks.ToString().ToLower() + ",");
            options += ("todo:" + config.Todo.ToString().ToLower() + ",");
            options += ("marktex:" + config.MarkTex.ToString().ToLower() + ",");
            options += ("smartlist:" + config.SmartList.ToString().ToLower() + ",");
            options += ("smartquote:" + config.SmartQuote.ToString().ToLower() + ",");
            options += ("align:" + config.Align.ToString().ToLower() + ",");
            options += ("pedantic:" + config.Pedantic.ToString().ToLower() + ",");
            options += ("sanitize:" + config.Sanitize.ToString().ToLower() + ",");
            options += ("smartypants:" + config.SmartyPants.ToString().ToLower() + ",");
            options += Properties.Resources.options + "}";
            return options;
        }
    }
}