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
        /// 打开文件
        /// </summary>
        bool isOpen = false;
        public void Open_File()
        {
            OpenFileDialog open = new OpenFileDialog();
            open.FileName = "";
            //open.DefaultExt = ".rtf";
            open.DefaultExt = ".md";
            open.Filter = "markdown文件(.md)|*.md|txt文件(.txt)|*.txt|rtf文件(.rtf)|*.rtf|doc文件(.doc)|*.doc";
            Stream checkStream = null;

            if ((bool)open.ShowDialog())
            {
                try
                {
                    if ((checkStream = open.OpenFile()) != null)
                    {
                        isOpen = true;
                        FileStream fs;
                        if (File.Exists(open.FileName))
                        {
                            fs = new FileStream(open.FileName, FileMode.Open, FileAccess.Read);
                            StreamReader streamReader = new StreamReader(fs, System.Text.Encoding.UTF8);
                            using (fs)
                            {
                                TextRange text = new TextRange(Out_tbEditor.Document.ContentStart, Out_tbEditor.Document.ContentEnd);
                                // if (!open.FileName.Substring(open.FileName.Length - 4, 4).Contains(".txt"))
                                if (!open.FileName.Substring(open.FileName.Length - 4, 4).Contains(".md"))
                                {
                                    //text.Load(fs, DataFormats.Rtf);
                                    text.Load(fs, DataFormats.Rtf);
                                }
                                else
                                {
                                    text.Load(fs, DataFormats.Text);
                                    //text.Load(fs, DataFormats.Xaml);                                                                            
                                }
                                isOpen = false;
                            }

                            streamReader.Dispose();
                            streamReader.Close();
                            fs.Dispose();
                            fs.Close();
                        }
                        checkStream.Dispose();
                        checkStream.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("错误: 无法从磁盘读取文件。原始错误: " + ex.Message);
                }
            }
            else
            {

            }
        }
        /// <summary>
        /// 保存文件
        /// </summary>
        public void Save_File()
        {
            //TODO: save current file
            SaveFileDialog save = new SaveFileDialog();
            //  save.Filter = "txt文件(.txt)|*.txt|rtf文件(.rtf)|*.rtf|doc文件(.doc)|*.doc";
            save.Filter = "markdown文件(.md)|*.md|markdown文件(.md)|*.markdown|markdown文件(.md)|*.mkd|txt文件(.txt)|*.txt|rtf文件(.rtf)|*.rtf|doc文件(.doc)|*.doc";
            try
            {
                if ((bool)save.ShowDialog())
                {
                    SaveFile(save.FileName);
                    MessageBox.Show("保存成功");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void SaveFile(String path)
        {
            FileStream fs = new FileStream(path, FileMode.Create);

            TextRange range = new TextRange(Out_tbEditor.Document.ContentStart, Out_tbEditor.Document.ContentEnd);
            if (!path.Substring(path.Length - 4, 4).Contains(".md"))
                range.Save(fs, DataFormats.Rtf);//DataFormats.Xaml 或者 DataFormats.XamlPackage  
            else
                range.Save(fs, DataFormats.Text);

            fs.Close();
        }
        /// <summary>
        /// 打印操作
        /// </summary>
        public void Print()
        {
            PrintDialog pd = new PrintDialog();
            if ((pd.ShowDialog() == true))
            {
                //use either one of the below      
                pd.PrintVisual(Out_tbEditor as Visual, "printing as visual");
                pd.PrintDocument((((IDocumentPaginatorSource)Out_tbEditor.Document).DocumentPaginator), "printing as paginator");
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
        

    }
}
