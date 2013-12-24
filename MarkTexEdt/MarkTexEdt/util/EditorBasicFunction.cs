using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using Microsoft.Win32;
using System.IO;

namespace MarkTexEdt.util
{
    public class EditBasicFuction
    {
        static RichTextBox Out_tbEditor;
        public EditBasicFuction(RichTextBox tb)
        {
            Out_tbEditor = tb;
            //TextPointer tp = 
        }

        /// <summary>
        /// 设置使字体变粗
        /// </summary>
        public void Bold()
        {
            Regex reg = new Regex(@"\*\*.*\*\*");
            TextSelection selection = Out_tbEditor.Selection;
            String before_Click_Text = "    ", click_Again_Text = "  ";
            // 如果没有选择文本，则按选取了一个普通字体的文本来处理
            FontWeight currentState = FontWeights.Normal;
            // 尝试获取所选文本的粗体状态
            if (selection.GetPropertyValue(Run.FontWeightProperty) !=
              DependencyProperty.UnsetValue)
            {
                currentState = (FontWeight)selection.GetPropertyValue(Run.FontWeightProperty);
            }
            if (currentState == FontWeights.Normal)
            {
                before_Click_Text = selection.Text;
                selection.Text = "**" + before_Click_Text + "**";
                selection.ApplyPropertyValue(Run.FontWeightProperty, FontWeights.Bold);
                //selection.Text = before_Click_Text;

            }
            else if(currentState == FontWeights.Bold)
            {
                if (reg.Match(selection.Text).Success)
                {
                    click_Again_Text = selection.Text.Substring(2, selection.Text.Length - 4);
                    selection.Text = click_Again_Text;
                    selection.ApplyPropertyValue(Run.FontWeightProperty, FontWeights.Normal);
                }
                else
                {
                    selection.Text = "**" + selection.Text + "**";
                    selection.ApplyPropertyValue(Run.FontWeightProperty, FontWeights.Bold);
                }
               
            }

            Out_tbEditor.Focus();
        }

        /// <summary>
        /// 设置使字体变为斜体
        /// </summary>
        public void Italic()
        {
            Regex reg = new Regex(@"\*.*\*");
            TextSelection selection = Out_tbEditor.Selection;
            String before_Click_Text = "  ", click_Again_Text = "  ";
            // 如果没有选择文本，则按选取了一个普通字体的文本来处理
            FontStyle currentState = FontStyles.Normal;
            // 尝试获取所选文本的粗体状态
            if (selection.GetPropertyValue(Run.FontStyleProperty) !=
               DependencyProperty.UnsetValue)
            {                
                currentState = (FontStyle)selection.GetPropertyValue(Run.FontStyleProperty);
            }
            if (currentState == FontStyles.Normal)
            {              
                    before_Click_Text = "*" + selection.Text + "*";
                    selection.Text = before_Click_Text;
                    selection.ApplyPropertyValue(Run.FontStyleProperty, FontStyles.Italic);              
            }
            else if(currentState == FontStyles.Italic)
            {
                if (reg.Match(selection.Text).Success)
                {
                    click_Again_Text = selection.Text.Substring(1, selection.Text.Length - 2);
                    selection.Text = click_Again_Text;
                    selection.ApplyPropertyValue(Run.FontStyleProperty, FontStyles.Normal);
                }
                else
                {
                    selection.Text = "*" + selection.Text + "*";
                    selection.ApplyPropertyValue(Run.FontStyleProperty,FontStyles.Italic);
                }               
            }
            Out_tbEditor.Focus();
        }

        /// <summary>
        /// 撤销操作
        /// </summary>
        public void Undo()
        {
            if (Out_tbEditor.CanUndo == true)
            {
                Out_tbEditor.Undo();
            }
        }
        /// <summary>
        /// 重做操作
        /// </summary>
        public void Redo()
        {

            if (Out_tbEditor.CanRedo == true)
            {
                Out_tbEditor.Redo();
            }
        }
        /// <summary>
        /// 全选
        /// </summary>
        public void SelectAll()
        {
            Out_tbEditor.SelectAll();
        }
        /// <summary>
        /// 复制
        /// </summary>
        public void Copy()
        {
            Out_tbEditor.Copy();
        }
        /// <summary>
        /// 粘贴
        /// </summary>
        public void Paste()
        {
            Out_tbEditor.Paste();
        }
        /// <summary>
        /// 剪切
        /// </summary>
        public void Cut()
        {
            Out_tbEditor.Cut();
        }

        /// <summary>
        /// 执行打开文件
        /// </summary>
        /// <param name="filePath"></param>
        public void OpenFile(string filePath)
        {
            try
            {
                FileStream fs = new FileStream(filePath, FileMode.Open);

                TextRange text = new TextRange(Out_tbEditor.Document.ContentStart, Out_tbEditor.Document.ContentEnd);
                text.Load(fs, DataFormats.Text);
                fs.Close();

                Config.ConfigInstance.CurrentFilePath = filePath;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 执行文件写入
        /// </summary>
        /// <param name="path"></param>
        public void SaveFile(String path)
        {
            try
            {
                FileStream fs = new FileStream(path, FileMode.Create);

                TextRange range = new TextRange(Out_tbEditor.Document.ContentStart, Out_tbEditor.Document.ContentEnd);
                range.Save(fs, DataFormats.Text);

                fs.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void Get_Current_Time()
        {
            String dayAndTime = "";
            //获得星期几
            //dayAndTime =  DateTime.Now.ToString("dddd", new System.Globalization.CultureInfo("zh-cn"));
            //获得年/月/日
            dayAndTime += DateTime.Now.ToString("yyyy/MM/dd");   //yyyy年MM月dd日
            dayAndTime += " ";
            //获得时:分:秒
            dayAndTime += DateTime.Now.ToString("HH:mm:ss ");
            Out_tbEditor.CaretPosition.InsertTextInRun(dayAndTime);
            //return dayAndTime;
        }

        public void Add_Link()
        {
            String link = "[urlName](http:// \"title\")";
            Out_tbEditor.CaretPosition.InsertTextInRun(link);
        }

        public void Strike_Through_Text()
        {
            Out_tbEditor.CaretPosition.InsertTextInRun("~~~~");
            Out_tbEditor.CaretPosition = Out_tbEditor.CaretPosition.GetPositionAtOffset(-2, LogicalDirection.Forward);
        }


        public void Set_Center()
        {
            Out_tbEditor.CaretPosition.InsertTextInRun(">");
        }

        public void Set_Right()
        {
            Out_tbEditor.CaretPosition.InsertTextInRun(">>");
        }

        public void Add_Quotation()
        {
            Out_tbEditor.CaretPosition = Out_tbEditor.CaretPosition.GetLineStartPosition(0);
            Out_tbEditor.CaretPosition.InsertTextInRun(">");
        }
    }
}
