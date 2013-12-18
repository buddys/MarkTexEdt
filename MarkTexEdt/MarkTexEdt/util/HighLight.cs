using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows;

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
            Regex reg = new Regex("^ *#.*$");       //高亮一级标题、二级标题   => 红色           
            Regex reg3 = new Regex(@"^ {4,}.*$|^>.*$|\$.*\$",RegexOptions.Multiline);          //高亮代码、引用  => 绿色
           
            Regex reg6 = new Regex("`.*`");
            //Regex reg8 = new Regex("```.*```");
            Regex reg4 = new Regex("[*][*].*[*][*]");       // 加粗高亮  => 加粗
            Regex reg5 = new Regex("[*].*[*]");             //高亮斜体 => 斜体
            Regex reg7 = new Regex("^.*\\[.*\\]\\(.*\\).*$");
            //GFM syntex highlight
            Regex reg8 = new Regex("^- \\[[x ]\\] |^[0-9][\\.] |^[-+*] ");    // 出现复选框， => 蓝色
         
            //自定义 syntex highlight
          //  Regex reg9 = new Regex("\\$.*\\$");
            //Regex reg9 = new Regex(@"(\n|^)\s*``` *\S*\r?\n[\s\S]*```[ \t]*(\r?\n|$)");
            Regex reg9 = new Regex(@"^```.*$");   
            Regex reg10 = new Regex(@"^```\s*$");
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
                        Console.WriteLine("begin");
                        Console.WriteLine(tmp);
                        Console.WriteLine("end");
                        Match match = reg.Match(tmp);
                        Match match3 = reg3.Match(tmp);
                        Match match4 = reg4.Match(tmp);
                        Match match5 = reg5.Match(tmp);
                        Match match6 = reg6.Match(tmp);
                        Match match7 = reg7.Match(tmp);
                        Match match8 = reg8.Match(tmp);
                        Match match9 = reg9.Match(tmp);
                        Match match10 = reg10.Match(tmp);
                        int matchIndex = 0, matchLength = 0;

                        if (match.Success)
                        {
                            matchIndex = match.Index;
                            matchLength = match.Length;
                            TextRange textrange = new TextRange(navigator.GetPositionAtOffset(matchIndex), navigator.GetPositionAtOffset(matchIndex + matchLength));
                            //Console.WriteLine("matched:" + match.ToString() + "(" + match.Index + "," + match.Length + ") in " + textrange.Text);
                            textrange.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Red));
                        }
                        /*else if(match9.Success) 
                        {
                            int count;
                            String temp2;
                            TextPointer matchStart, matchCurrent, matchEnd;
                            matchStart = navigator;
                            matchCurrent = navigator;
                            matchEnd = navigator;
                         //   while (matchEnd.CompareTo(document.ContentEnd)<0)
                            while (true)                           
                            {
                                matchCurrent = matchCurrent.GetLineStartPosition(1, out count);
                                Console.WriteLine(matchCurrent.GetTextInRun(LogicalDirection.Forward));
                                temp2 = matchCurrent.GetTextInRun(LogicalDirection.Forward);
                                Console.WriteLine(temp2);
                                if (reg10.Match(temp2).Success)
                                {
                                    matchLength = reg10.Match(temp2).Length;
                                    matchEnd = matchCurrent.GetPositionAtOffset(3);
                                    TextRange textrange = new TextRange(matchStart,matchEnd);
                                    textrange.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Green));

                                    TextRange t3 = new TextRange(matchEnd, matchEnd.GetPositionAtOffset(temp2.Length-3));
                                    t3.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Black));

                                    break;
                                }
                                if (count != 1)
                                 {
                                     matchEnd = document.ContentEnd ;
                                   break;
                                 } 
                            }
                            navigator = matchEnd.GetNextContextPosition(LogicalDirection.Backward);
                            break;                           

                        }*/
                        else if(match9.Success)
                        {

                            matchIndex = match9.Index;
                            matchLength = match9.Length;
                            TextRange textrange = new TextRange(navigator.GetPositionAtOffset(matchIndex), navigator.GetPositionAtOffset(matchIndex + matchLength));
                            //Console.WriteLine("matched:" + match.ToString() + "(" + match.Index + "," + match.Length + ") in " + textrange.Text);
                            textrange.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Green));
                        }
                        else if (match3.Success)
                        {
                            matchIndex = match3.Index;
                            matchLength = match3.Length;
                            TextRange textrange = new TextRange(navigator.GetPositionAtOffset(matchIndex), navigator.GetPositionAtOffset(matchIndex + matchLength));
                            //Console.WriteLine("matched:" + match.ToString() + "(" + match.Index + "," + match.Length + ") in " + textrange.Text);
                            textrange.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Green));
                        }
                        else if (match8.Success)
                        {
                            matchIndex = match8.Index;
                            matchLength = match8.Length;
                            TextRange textrange = new TextRange(navigator.GetPositionAtOffset(matchIndex), navigator.GetPositionAtOffset(matchIndex + matchLength));
                            //Console.WriteLine("matched:" + match.ToString() + "(" + match.Index + "," + match.Length + ") in " + textrange.Text);
                            textrange.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Gray)); 
                        }
                        else if (match6.Success)
                        {
                            matchIndex = match6.Index;
                            matchLength = match6.Length;
                            TextRange textrange = new TextRange(navigator.GetPositionAtOffset(matchIndex), navigator.GetPositionAtOffset(matchIndex + matchLength));
                            //Console.WriteLine("matched:" + match.ToString() + "(" + match.Index + "," + match.Length + ") in " + textrange.Text);
                            textrange.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Green));  
                       } 
                        else if (match4.Success)
                        {
                            matchIndex = match4.Index;
                            matchLength = match4.Length;
                            TextRange textrange = new TextRange(navigator.GetPositionAtOffset(matchIndex), navigator.GetPositionAtOffset(matchIndex + matchLength));
                            //Console.WriteLine("matched:" + match.ToString() + "(" + match.Index + "," + match.Length + ") in " + textrange.Text);
                            //  textrange.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Red));
                            textrange.ApplyPropertyValue(Run.FontWeightProperty, FontWeights.Bold);
                            textrange.ApplyPropertyValue(Run.FontStyleProperty, FontStyles.Normal);
                        }
                        else if (match5.Success)
                        {
                            matchIndex = match5.Index;
                            matchLength = match5.Length;
                            TextRange textrange = new TextRange(navigator.GetPositionAtOffset(matchIndex), navigator.GetPositionAtOffset(matchIndex + matchLength));
                            //Console.WriteLine("matched:" + match.ToString() + "(" + match.Index + "," + match.Length + ") in " + textrange.Text);
                            //  textrange.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Red));
                            textrange.ApplyPropertyValue(Run.FontStyleProperty, FontStyles.Italic);
                            textrange.ApplyPropertyValue(Run.FontWeightProperty, FontWeights.Normal);
                        }
                        else if (match7.Success)
                        {
                            matchIndex = match7.Index;
                            matchLength = match7.Length;
                            TextRange textrange = new TextRange(navigator.GetPositionAtOffset(matchIndex), navigator.GetPositionAtOffset(matchIndex + matchLength));
                            //Console.WriteLine("matched:" + match.ToString() + "(" + match.Index + "," + match.Length + ") in " + textrange.Text);
                            //  textrange.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Red));
                            textrange.ApplyPropertyValue(Run.FontStyleProperty, FontStyles.Normal);
                            textrange.ApplyPropertyValue(Run.FontWeightProperty, FontWeights.Normal);
                            textrange.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Blue));

                        }

                        TextRange t1 = new TextRange(navigator, navigator.GetPositionAtOffset(matchIndex));
                        t1.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Black));
                        t1.ApplyPropertyValue(Run.FontWeightProperty, FontWeights.Normal);
                        t1.ApplyPropertyValue(Run.FontStyleProperty, FontStyles.Normal);

                        TextRange t2 = new TextRange(navigator.GetPositionAtOffset(matchIndex + matchLength), navigator.GetPositionAtOffset(tmp.Length));
                        t2.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Black));
                        t2.ApplyPropertyValue(Run.FontWeightProperty, FontWeights.Normal);
                        t2.ApplyPropertyValue(Run.FontStyleProperty, FontStyles.Normal);


                        navigator = navigator.GetPositionAtOffset(matchIndex + matchLength);

                        break;
                }
            }
        }

    }

}
