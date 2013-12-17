using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Controls;

namespace MarkTexEdt.util
{
    class HighLight
    {
        static RichTextBox Out_tbEditor;
        public HighLight(RichTextBox tb)
        {
            Out_tbEditor = tb;
        }

        public void HighLight5()
        {
            Regex reg = new Regex("^ {4,}.*$|^>.*$|^```.*```$");       //高亮代码、引用
            //   Regex reg1 = new Regex("^>.*$");          //高亮引用
            // Regex reg2 = new Regex("^```.*```$");     //高亮代码
            Regex reg3 = new Regex("^#(#)?.*$");          //高亮一级标题、二级标题
            //Regex reg = new Regex("(?:^|\n)    (.*)(?:\n|$)", RegexOptions.Compiled | RegexOptions.IgnoreCase);
            FlowDocument document = Out_tbEditor.Document;
            StringBuilder buffer = new StringBuilder();
            for (TextPointer navigator = document.ContentStart;
               navigator.CompareTo(document.ContentEnd) < 0;
               navigator = navigator.GetNextContextPosition(LogicalDirection.Forward))
            {
                switch (navigator.GetPointerContext(LogicalDirection.Forward))
                {
                    case TextPointerContext.ElementStart:
                        // Output opening tag of a TextElement
                        buffer.AppendFormat("<{0}>", navigator.GetAdjacentElement(LogicalDirection.Forward).GetType().Name);
                        // Console.WriteLine("ElementStart" + buffer.ToString());
                        break;
                    case TextPointerContext.ElementEnd:
                        // Output closing tag of a TextElement
                        buffer.AppendFormat("</{0}>", navigator.GetAdjacentElement(LogicalDirection.Forward).GetType().Name);
                        // Console.WriteLine("ElementEnd" + buffer.ToString());
                        break;
                    case TextPointerContext.EmbeddedElement:
                        // Output simple tag for embedded element
                        buffer.AppendFormat("<{0}/>", navigator.GetAdjacentElement(LogicalDirection.Forward).GetType().Name);
                        break;
                    case TextPointerContext.Text:
                        string tmp = navigator.GetTextInRun(LogicalDirection.Forward);
                        buffer.Append(tmp);
                        Match match = reg.Match(tmp);
                        Match match3 = reg3.Match(tmp);
                        int matchIndex = 0, matchLength = 0;
                        if (match.Success)
                        {
                            matchIndex = match.Index;
                            matchLength = match.Length;
                            TextRange textrange = new TextRange(navigator.GetPositionAtOffset(match.Index), navigator.GetPositionAtOffset(match.Index + match.Length));
                            //Console.WriteLine("matched:" + match.ToString() + "(" + match.Index + "," + match.Length + ") in " + textrange.Text);
                            textrange.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Green));
                        }
                        else if (match3.Success)
                        {
                            matchIndex = match3.Index;
                            matchLength = match3.Length;
                            TextRange textrange = new TextRange(navigator.GetPositionAtOffset(match3.Index), navigator.GetPositionAtOffset(match3.Index + match3.Length));
                            //Console.WriteLine("matched:" + match.ToString() + "(" + match.Index + "," + match.Length + ") in " + textrange.Text);
                            textrange.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Red));
                        }
                        TextRange t1 = new TextRange(navigator, navigator.GetPositionAtOffset(matchIndex));
                        t1.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Black));

                        TextRange t2 = new TextRange(navigator.GetPositionAtOffset(matchIndex + matchLength), navigator.GetPositionAtOffset(tmp.Length));
                        t2.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Black));

                        navigator = navigator.GetPositionAtOffset(matchIndex + matchLength);

                        break;
                }
            }
        }

    }

}
