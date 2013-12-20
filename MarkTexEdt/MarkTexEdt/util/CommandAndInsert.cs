using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Documents;

namespace MarkTexEdt.util
{
    class CommandAndInsert
    {
        static RichTextBox Out_tbEditor;
        public CommandAndInsert(RichTextBox rtb)
        {
            Out_tbEditor = rtb;
        }

        public void Add_Code()
        {
            String code = "``";
            Out_tbEditor.CaretPosition.InsertTextInRun(code);
            Out_tbEditor.CaretPosition = Out_tbEditor.CaretPosition.GetPositionAtOffset(1,LogicalDirection.Backward);
        }

        public void Add_HeadLine1()
        {
            Out_tbEditor.CaretPosition.InsertTextInRun("#  #");
            Out_tbEditor.CaretPosition = Out_tbEditor.CaretPosition.GetPositionAtOffset(-2,LogicalDirection.Forward);
        }

        public void Add_HeadLine2()
        {
            Out_tbEditor.CaretPosition.InsertTextInRun("##  ##");
            Out_tbEditor.CaretPosition = Out_tbEditor.CaretPosition.GetPositionAtOffset(-3, LogicalDirection.Forward);
        }

        public void Add_HeadLine3()
        {
            Out_tbEditor.CaretPosition.InsertTextInRun("###  ###");
            Out_tbEditor.CaretPosition = Out_tbEditor.CaretPosition.GetPositionAtOffset(-4, LogicalDirection.Forward);
        }

        public void Add_HeadLine4()
        {
            Out_tbEditor.CaretPosition.InsertTextInRun("####  ####");
            Out_tbEditor.CaretPosition = Out_tbEditor.CaretPosition.GetPositionAtOffset(-5, LogicalDirection.Forward);
        }

        public void Add_Horizontal_Scale()
        {
            Out_tbEditor.CaretPosition.InsertTextInRun("----------");
        }

        public void Increase_Font_Size()
        {
            double fontSize = Out_tbEditor.FontSize;
            if(fontSize<50)
            {
                fontSize+=5;
                if(fontSize>50)
                {
                    fontSize = 50;
                }
            }
            TextSelection selection = Out_tbEditor.Selection;
            if (selection.Text != "")
            {
                selection.ApplyPropertyValue(TextElement.FontSizeProperty, fontSize);
            }
            else
            {
                Out_tbEditor.FontSize = fontSize;
            }
        }
    }
}
