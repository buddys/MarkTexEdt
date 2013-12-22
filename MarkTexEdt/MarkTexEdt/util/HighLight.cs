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
        public void highLightAll()
        {
            TextRange text = new TextRange(Out_tbEditor.Document.ContentStart,Out_tbEditor.Document.ContentEnd);
            text.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Red));
        }
        public void HighLightMultiLines()
        {
            Regex reg1 = new Regex(@"^```.*$|^\$\$\$.*$");
            Regex reg2 = new Regex(@"^```|^\$\$\$");

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
                        //  while(navigator.GetPointerContext(LogicalDirection.Forward) != TextPointerContext.Text)
                        string tmp = navigator.GetTextInRun(LogicalDirection.Forward);
                        String temp2 = "";
                        buffer.Append(tmp);
                        Match match1 = reg1.Match(tmp);
                        int flag = 0, flag2 = 0;                           //flag标记是否最终匹配成功， flag2标识是否 reg1匹配成功。
                        TextPointer matchStart, matchCurrent, matchEnd, high1, high2;
                        matchStart = navigator;
                        matchCurrent = navigator.GetPositionAtOffset(tmp.Length, LogicalDirection.Forward);
                        if (match1.Success)                     // Regex reg1 = new Regex(@"^```.*$|^\$\$\$.*$");
                        {
                            flag2 = 1;                              //成功匹配 reg1
                            int count, count2;                          
                           
                           // TextPointer matchStart, matchCurrent, matchEnd, high1, high2;
                            //matchStart = navigator;
                          //  matchCurrent = navigator.GetPositionAtOffset(tmp.Length, LogicalDirection.Forward);
                            String test2 = matchCurrent.GetTextInRun(LogicalDirection.Forward).ToString();
                            matchEnd = navigator;
                            //   while (matchEnd.CompareTo(document.ContentEnd)<0)
                            while (matchCurrent.CompareTo(document.ContentEnd) < 0)
                            {
                                if (matchCurrent.GetPointerContext(LogicalDirection.Forward) != TextPointerContext.Text)
                                {
                                    matchCurrent = matchCurrent.GetNextContextPosition(LogicalDirection.Forward);
                                }
                                else
                                {
                                    temp2 = matchCurrent.GetTextInRun(LogicalDirection.Forward);
                                    Match match2 = reg2.Match(temp2);  
                                    if (match2.Success)
                                    {
                                        matchEnd = matchCurrent.GetPositionAtOffset(match2.Length, LogicalDirection.Forward);
                                        TextRange textRange = new TextRange(matchStart, matchEnd);
                                        String test11 = matchStart.GetTextInRun(LogicalDirection.Forward).ToString();
                                        String test12 = matchEnd.GetTextInRun(LogicalDirection.Forward).ToString();
                                        String test15 = textRange.Text.ToString();

                                        high1 = matchStart.GetLineStartPosition(1, out count);
                                        while (high1.GetPointerContext(LogicalDirection.Forward) != TextPointerContext.Text)
                                        {
                                            high1 = high1.GetNextContextPosition(LogicalDirection.Forward);
                                        }
                                        String test9 = high1.GetTextInRun(LogicalDirection.Forward).ToString();
                                        high2 = high1.GetLineStartPosition(1, out count2);
                                        while (high2.GetPointerContext(LogicalDirection.Forward) != TextPointerContext.Text)
                                        {
                                            high2 = high2.GetNextContextPosition(LogicalDirection.Forward);
                                        }
                                        String test10 = high2.GetTextInRun(LogicalDirection.Forward).ToString();
                                        while (true)
                                        {
                                            if (count2 == 0)
                                                break;
                                          //  high1.GetTextInRun(LogicalDirection.Forward).ToString().Length;
                                            TextRange t5 = new TextRange(high1, high1.GetPositionAtOffset(high1.GetTextInRun(LogicalDirection.Forward).ToString().Length));
                                            String test8 = t5.Text.ToString();
                                            t5.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Green));
                                            high1 = high2;
                                            high2 = high2.GetLineStartPosition(1,out count2);
 
                                        }
                                       
                                       /* textRange.ClearAllProperties();
                                        textRange.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Green));
                                     // textrange.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Red));
                                        String test3 = textRange.Text.ToString();*/
                                       

                                        TextRange t1 = new TextRange(matchEnd, matchEnd.GetPositionAtOffset(temp2.Length - match2.Length));
                                        String test4 = t1.Text.ToString();
                                        t1.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Black));
                                        flag = 1;
                                    }
                                   
                                    matchCurrent = matchCurrent.GetPositionAtOffset(temp2.Length);
                                    String test7 = matchCurrent.GetTextInRun(LogicalDirection.Forward).ToString();
                                     if (flag == 1)
                                    {
                                        navigator = matchCurrent;         //开始下一次判断，看是否有新的同时匹配reg1、reg2的区间
                                        String test6 = navigator.GetTextInRun(LogicalDirection.Forward).ToString();
                                        break;
                                    }
                                    matchCurrent = matchCurrent.GetNextContextPosition(LogicalDirection.Forward);
                                    String test5 = matchCurrent.GetTextInRun(LogicalDirection.Forward).ToString();
                                }
                            }
                            
                        }
                       /* if (flag2 == 0)
                        {
                            navigator = navigator.GetPositionAtOffset(tmp.Length, LogicalDirection.Forward);
                        }
                        else if (flag2 == 1)
                        {
                            if (flag == 0)
                            {
 
                            } 
                        }
                        */
                        if (flag != 1)
                        {
                            navigator = navigator.GetPositionAtOffset(tmp.Length, LogicalDirection.Forward);
                            String test1 = navigator.GetTextInRun(LogicalDirection.Forward).ToString();
                        }
                        break;
                }
            }
        }
                
      
        public void HighLight5()
        {
            Regex reg = new Regex("^ *#.*$");       //高亮一级标题、二级标题   => 红色           
            Regex reg3 = new Regex(@"^ {4,}.*$|^>.*$|\$.*\$|`.*`", RegexOptions.Multiline);          //高亮代码、引用  => 绿色         
         
            Regex reg4 = new Regex("[*][*].*[*][*]");       // 加粗高亮  => 加粗
            Regex reg5 = new Regex("[*].*[*]");             //高亮斜体 => 斜体
            //Regex reg6 = new Regex("`.*`");                 //高亮代码    
            Regex reg7 = new Regex(@"\[.*\]\(.*\)");  //高亮超链接 => 蓝色
            //GFM syntex highlight
            Regex reg8 = new Regex(@"^- \[[x ]\] |^[0-9][\.] |^[-+*] ");    // 出现复选框， => 蓝色

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
                       // Console.WriteLine("begin");
                       // Console.WriteLine(tmp);
                        //Console.WriteLine("end");
                        Match match = reg.Match(tmp);
                        Match match3 = reg3.Match(tmp);
                        Match match4 = reg4.Match(tmp);
                        Match match5 = reg5.Match(tmp);
                        Match match7 = reg7.Match(tmp);
                        Match match8 = reg8.Match(tmp);
                        int matchIndex = 0, matchLength = 0;

                        if (match.Success)               //(@"^ {4,}.*$|^>.*$|\$.*\$|`.*`", RegexOptions.Multiline)
                        {
                            matchIndex = match.Index;
                            matchLength = match.Length;
                            TextRange textrange = new TextRange(navigator.GetPositionAtOffset(matchIndex), navigator.GetPositionAtOffset(matchIndex + matchLength));
                            //Console.WriteLine("matched:" + match.ToString() + "(" + match.Index + "," + match.Length + ") in " + textrange.Text);
                            textrange.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Red));
                        }
                        else if (match3.Success)    
                        {
                            matchIndex = match3.Index;
                            matchLength = match3.Length;
                            TextRange textrange = new TextRange(navigator.GetPositionAtOffset(matchIndex), navigator.GetPositionAtOffset(matchIndex + matchLength));
                            textrange.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Green));
                        }
                        else if (match4.Success)         //粗体   
                        {
                            matchIndex = match4.Index;
                            matchLength = match4.Length;
                            TextRange textrange = new TextRange(navigator.GetPositionAtOffset(matchIndex), navigator.GetPositionAtOffset(matchIndex + matchLength));
                            textrange.ApplyPropertyValue(Run.FontWeightProperty, FontWeights.Bold);
                            textrange.ApplyPropertyValue(Run.FontStyleProperty, FontStyles.Normal);
                            textrange.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Black));
                        }
                        else if (match5.Success)         //斜体
                        {
                            matchIndex = match5.Index;
                            matchLength = match5.Length;
                            TextRange textrange = new TextRange(navigator.GetPositionAtOffset(matchIndex), navigator.GetPositionAtOffset(matchIndex + matchLength));
                            textrange.ApplyPropertyValue(Run.FontStyleProperty, FontStyles.Italic);
                            textrange.ApplyPropertyValue(Run.FontWeightProperty, FontWeights.Normal);
                            textrange.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Black));
                        }
                        else if (match7.Success)         //粗体
                        {
                            matchIndex = match7.Index;
                            matchLength = match7.Length;
                            TextRange textrange = new TextRange(navigator.GetPositionAtOffset(matchIndex), navigator.GetPositionAtOffset(matchIndex + matchLength));
                            textrange.ApplyPropertyValue(Run.FontStyleProperty, FontStyles.Normal);
                            textrange.ApplyPropertyValue(Run.FontWeightProperty, FontWeights.Normal);
                            textrange.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Blue));
                        }
                        else if (match8.Success)        //数字开头、复选框、-+* 
                        {
                            matchIndex = match8.Index;
                            matchLength = match8.Length;
                            TextRange textrange = new TextRange(navigator.GetPositionAtOffset(matchIndex), navigator.GetPositionAtOffset(matchIndex + matchLength));
                            textrange.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Gray));
                        }

                        TextRange t1 = new TextRange(navigator, navigator.GetPositionAtOffset(matchIndex));
                        t1.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Black));
                        t1.ClearAllProperties();
                        //t1.ApplyPropertyValue(Run.FontWeightProperty, FontWeights.Normal);
                        //t1.ApplyPropertyValue(Run.FontStyleProperty, FontStyles.Normal);

                        TextRange t2 = new TextRange(navigator.GetPositionAtOffset(matchIndex + matchLength), navigator.GetPositionAtOffset(tmp.Length));

                        t2.ApplyPropertyValue(TextElement.ForegroundProperty, new SolidColorBrush(Colors.Black));
                        t2.ClearAllProperties();
                       // t2.ApplyPropertyValue(Run.FontWeightProperty, FontWeights.Normal);
                       // t2.ApplyPropertyValue(Run.FontStyleProperty, FontStyles.Normal);

                        navigator = navigator.GetPositionAtOffset(matchIndex + matchLength);
                        break;
                }
            }
        }

    }

}
