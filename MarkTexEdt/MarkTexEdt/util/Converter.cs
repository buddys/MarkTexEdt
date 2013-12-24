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

        JSObject jsobject;

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
            webControl.Source = Config.TemplateUri;
            jsobject = webControl.CreateGlobalJavascriptObject("jsobject");
        }

        public void Update(string src)
        {
            string options = this.GetOptions();
            if (src == null) src = "";
            string inputString = "update(" + EncodeJsString(src) + "," + options + ")";
            Console.WriteLine(inputString);
            webControl.ExecuteJavascript(inputString);
        }

        public void SaveAsHtml(string filename)
        {
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

        public static string EncodeJsString(string s)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\"");
            foreach (char c in s)
            {
                switch (c)
                {
                    case '\"':
                        sb.Append("\\\"");
                        break;
                    case '\\':
                        sb.Append("\\\\");
                        break;
                    case '\b':
                        sb.Append("\\b");
                        break;
                    case '\f':
                        sb.Append("\\f");
                        break;
                    case '\n':
                        sb.Append("\\n");
                        break;
                    case '\r':
                        sb.Append("\\r");
                        break;
                    case '\t':
                        sb.Append("\\t");
                        break;
                    default:
                        int i = (int)c;
                        if (i < 32 || i > 127)
                        {
                            sb.AppendFormat("\\u{0:X04}", i);
                        }
                        else
                        {
                            sb.Append(c);
                        }
                        break;
                }
            }
            sb.Append("\"");

            return sb.ToString();
        }
    }
}