using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop;
using Word = Microsoft.Office.Interop.Word;
using Microsoft.Office.Interop.Word;
using System.Data;

namespace HHSoft.FieldProtect.FrameWork.Utility
{
    public class WordHelper : IDisposable
    {
        private Word.ApplicationClass oWordApplic;
        private Word.Document oDoc;


        public WordHelper()
        {
            oWordApplic = new Word.ApplicationClass();
        }

        ~WordHelper()
        {
            if (oWordApplic != null)
            {
                oWordApplic = null;
            }
            if (oDoc != null)
            {
                oDoc = null;
            }
        }
        public void Open(string strFileName)
        {
            foreach (System.Diagnostics.Process p in System.Diagnostics.Process.GetProcesses())
            {
                if (p.ProcessName.ToLower().Equals("winword"))
                    p.Kill();
            }

            object fileName = strFileName;
            object readOnly = false;
            object isVisible = true;
            object missing = System.Reflection.Missing.Value;

            oWordApplic = new Word.ApplicationClass();
            oDoc = oWordApplic.Documents.Open(ref fileName, ref missing, ref readOnly,
                ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref isVisible, ref missing, ref missing, ref missing, ref missing);

            oDoc.Activate();
        }

        public void Open()
        {
            object missing = System.Reflection.Missing.Value;
            oDoc = oWordApplic.Documents.Add(ref missing, ref missing, ref missing, ref missing);

            oDoc.Activate();
        }

        public void Save()
        {
            oDoc.Save();
        }

        public void SaveAs(string strFileName)
        {
            object missing = System.Reflection.Missing.Value;
            object fileName = strFileName;

            oDoc.SaveAs(ref fileName, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
                ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);
        }


        public void Print(object fileName)
        {
            try
            {
                object missing = System.Reflection.Missing.Value;
                object background = false;
                oDoc.PrintOut(ref background, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);
                object saveOption = WdSaveOptions.wdSaveChanges;
                this.oDoc.Close(ref saveOption, ref missing, ref missing);
                saveOption = Microsoft.Office.Interop.Word.WdSaveOptions.wdDoNotSaveChanges;
            }
            catch (Exception ex)
            {

            }
        }

        public void PrintViewWord(object fileName)
        {
            object Missing = System.Reflection.Missing.Value;
            object readOnly = true;
            this.oDoc = this.oWordApplic.Documents.Open(ref fileName, ref Missing, ref readOnly, ref Missing, ref Missing, ref Missing, ref Missing, ref Missing, ref Missing, ref Missing, ref Missing, ref Missing, ref Missing, ref Missing, ref Missing, ref Missing);
            this.oWordApplic.Visible = true;
            //this.oDoc.Activate();            
            this.oDoc.PrintPreview();
        }

        // Save the document in HTML format
        public void SaveAsHtml(string strFileName)
        {
            object missing = System.Reflection.Missing.Value;
            object fileName = strFileName;
            object Format = (int)Word.WdSaveFormat.wdFormatHTML;
            oDoc.SaveAs(ref fileName, ref Format, ref missing, ref missing, ref missing, ref missing, ref missing,
            ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing);
        }


        public void TableAddRow(int iTableNo)
        {
            Object missing = System.Reflection.Missing.Value;
            Word.Table characterTable = oDoc.Tables[iTableNo];
            characterTable.Rows.Add(ref missing);            
        }

       
        public bool InsertTable(DataTable dt, bool haveBorder, double[] colWidths, string[] MergeAry)
        {
            try
            {
                object Nothing = System.Reflection.Missing.Value;                
                int lenght = oDoc.Characters.Count - 1;
                object start = lenght;
                object end = lenght;
                //表格起始坐标                
                Microsoft.Office.Interop.Word.Range tableLocation = oDoc.Range(ref start, ref end);
                //添加Word表格                     
                Microsoft.Office.Interop.Word.Table table =
                    oDoc.Tables.Add(tableLocation, dt.Rows.Count, dt.Columns.Count, ref Nothing, ref Nothing);
                if (colWidths != null)
                {
                    for (int i = 0; i < colWidths.Length; i++)
                    {
                        table.Columns[i + 1].Width = (float)(28.5F * colWidths[i]);
                    }
                }               
                
                ///设置TABLE的样式                
                table.Rows.HeightRule = Microsoft.Office.Interop.Word.WdRowHeightRule.wdRowHeightAtLeast;
                table.Rows.Height = oWordApplic.CentimetersToPoints(float.Parse("0.8"));
                table.Range.Font.Size = 10.5F;
                table.Range.Font.Name = "宋体";
                table.Range.Font.Bold = 0;
                table.Range.ParagraphFormat.Alignment =
                    Microsoft.Office.Interop.Word.WdParagraphAlignment.wdAlignParagraphCenter;
                table.Range.Cells.VerticalAlignment = Microsoft.Office.Interop.Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                if (haveBorder == true)
                {
                    //设置外框样式                    
                    table.Borders.OutsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                    table.Borders.InsideLineStyle = Microsoft.Office.Interop.Word.WdLineStyle.wdLineStyleSingle;
                    //样式设置结束                
                }
                for (int row = 0; row < dt.Rows.Count; row++)
                {
                    for (int col = 0; col < dt.Columns.Count; col++)
                    {
                        table.Cell(row + 1, col + 1).Range.Text = dt.Rows[row][col].ToString();
                    }
                }
                if (MergeAry != null)
                {
                    foreach (string mStr in MergeAry)
                    {
                        string[] cellStr = mStr.Split(',');
                        table.Cell(int.Parse(cellStr[0]), int.Parse(cellStr[1]))
                            .Merge(table.Cell(int.Parse(cellStr[2]), int.Parse(cellStr[3])));
                    }                    
                }
                ////table.Cell(dt.Rows.Count,2).Merge(table.Cell(dt.Rows.Count,dt.Columns.Count));

                ////table.Cell(dt.Rows.Count, 1).Merge(table.Cell(dt.Rows.Count, 3));
 
                return true;
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.ToString(), "错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);  
                return false;
            }
            finally
            {
            }
        }
        public bool InsertTable(DataTable dt, bool haveBorder)
        {
            return InsertTable(dt, haveBorder, null,null);
        }
        public bool InsertTable(DataTable dt)
        {
            return InsertTable(dt, true, null, null);
        }

        #region - 页面设置 -   
     

        public void SetPage(WOrientation orientation, double width, double height, double topMargin, 
            double leftMargin, double rightMargin, double bottomMargin)        
        {            
            oDoc.PageSetup.PageWidth = oWordApplic.CentimetersToPoints((float)width);
            oDoc.PageSetup.PageHeight = oWordApplic.CentimetersToPoints((float)height);
            if (orientation == WOrientation.横板)
            {                
                oDoc.PageSetup.Orientation = Microsoft.Office.Interop.Word.WdOrientation.wdOrientLandscape;  
            }           
            oDoc.PageSetup.TopMargin = (float)(topMargin * 25);//上边距      
            oDoc.PageSetup.LeftMargin = (float)(leftMargin * 25);//左边距         
            oDoc.PageSetup.RightMargin = (float)(rightMargin * 25);//右边距  
            oDoc.PageSetup.BottomMargin = (float)(bottomMargin * 25);//下边距       
        }       
        
        public void SetPage(WOrientation orientation, double topMargin, double leftMargin,
            double rightMargin, double bottomMargin)       
        {            
            SetPage(orientation, 21, 29.7, topMargin, leftMargin, rightMargin, bottomMargin);  
        }        
        public void SetPage(double topMargin, double leftMargin, double rightMargin, double bottomMargin)     
        {
            SetPage(WOrientation.竖板, 21, 29.7, topMargin, leftMargin, rightMargin, bottomMargin);      
        }        
        #endregion

        #region - 插入分页符 -        
        public void InsertBreak()        
        {            
            Word.Paragraph para;
            Object missing = System.Reflection.Missing.Value;
            para = oDoc.Content.Paragraphs.Add(ref missing);            
            object pBreak = (int)WdBreakType.wdSectionBreakNextPage;            
            para.Range.InsertBreak(ref pBreak);        
        }        
        #endregion
        public void TableOperate(int iTableNo, int iRow, int iCol, string strValue)
        {
            Object missing = System.Reflection.Missing.Value;
            Word.Table characterTable = oDoc.Tables[iTableNo];
            //talbe的第一个单元格为cell(1,1)
            characterTable.Cell(iRow, iCol).Range.Text = strValue;              
        }

        public void MergeTableCell(int iRowBegin, int iRowEnd, string sCol)
        {
            Word.Table characterTable = oDoc.Tables[1];
            foreach (string s in sCol.Split(','))
            {
                characterTable.Cell(iRowBegin, int.Parse(s)).Merge(characterTable.Cell(iRowEnd, int.Parse(s)));
            }            
        }
        


        public void InsertText(string strText)
        {
            oWordApplic.Selection.TypeText(strText);
        }
        public bool ReplaceText(string findStr, string replaceStr)
        {
            object replaceAll = Word.WdReplace.wdReplaceAll;

            object missing = System.Reflection.Missing.Value;

            oWordApplic.Selection.Find.ClearFormatting();

            object findText = findStr;

            oWordApplic.Selection.Find.Replacement.ClearFormatting();
            oWordApplic.Selection.Find.Replacement.Text = replaceStr;

            if (oWordApplic.Selection.Find.Execute(ref findText, ref missing, ref missing, ref missing, ref missing, ref missing, ref missing,
         ref missing, ref missing, ref missing, ref replaceAll, ref missing, ref missing, ref missing, ref missing))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void InsertLineBreak()
        {
            oWordApplic.Selection.TypeParagraph();
        }
        public void InsertLineBreak(int nline)
        {
            for (int i = 0; i < nline; i++)
                oWordApplic.Selection.TypeParagraph();
        }

        // Change the paragraph alignement
        public void SetAlignment(string strType)
        {
            switch (strType)
            {
                case "Center":
                    oWordApplic.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
                    break;
                case "Left":
                    oWordApplic.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphLeft;
                    break;
                case "Right":
                    oWordApplic.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphRight;
                    break;
                case "Justify":
                    oWordApplic.Selection.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
                    break;
            }

        }


        // if you use thif function to change the font you should call it again with 
        // no parameter in order to set the font without a particular format
        public void SetFont(string strType)
        {
            switch (strType)
            {
                case "Bold":
                    oWordApplic.Selection.Font.Bold = 1;
                    break;
                case "Italic":
                    oWordApplic.Selection.Font.Italic = 1;
                    break;
                case "Underlined":
                    oWordApplic.Selection.Font.Subscript = 0;
                    break;
            }
        }

        // disable all the style 
        public void SetFont()
        {
            oWordApplic.Selection.Font.Bold = 0;
            oWordApplic.Selection.Font.Italic = 0;
            oWordApplic.Selection.Font.Subscript = 0;
        }

        public void SetFontName(string strType)
        {
            oWordApplic.Selection.Font.Name = strType;
        }

        public void SetFontSize(int nSize)
        {
            oWordApplic.Selection.Font.Size = nSize;

        }

        public void SetFontColor()
        {
            oWordApplic.Selection.Font.Color = Word.WdColor.wdColorRed;
        }

        public void InsertPagebreak()
        {
            // VB : Selection.InsertBreak Type:=wdPageBreak
            object pBreak = (int)Word.WdBreakType.wdPageBreak;
            oWordApplic.Selection.InsertBreak(ref pBreak);
        }

        public void ReplaceBookMark(object strBookMarkName, string strValue)
        {

            // VB :  Selection.GoTo What:=wdGoToBookmark, Name:="nome"
            object missing = System.Reflection.Missing.Value;
            Word.Range wordrng = oDoc.Bookmarks.get_Item(ref strBookMarkName).Range;
            wordrng.Text = strValue;
        }
        public void ReplaceBookMarkPic(object strBookMarkName, string strPath)
        {
            // VB :  Selection.GoTo What:=wdGoToBookmark, Name:="nome"
            object missing = System.Reflection.Missing.Value;
            Word.Range wordrng = oDoc.Bookmarks.get_Item(ref strBookMarkName).Range;
            wordrng.Text = "";
            wordrng.InlineShapes.AddPicture(strPath, ref missing, ref missing, ref missing);
        }
        // Go to a predefined bookmark, if the bookmark doesn't exists the application will raise an error

        public void GotoBookMark(string strBookMarkName)
        {
            // VB :  Selection.GoTo What:=wdGoToBookmark, Name:="nome"
            object missing = System.Reflection.Missing.Value;

            object Bookmark = (int)Word.WdGoToItem.wdGoToBookmark;
            object NameBookMark = strBookMarkName;
            oWordApplic.Selection.GoTo(ref Bookmark, ref missing, ref missing, ref NameBookMark);
        }

        public void GoToTheEnd()
        {
            // VB :  Selection.EndKey Unit:=wdStory
            object missing = System.Reflection.Missing.Value;
            object unit;
            unit = Word.WdUnits.wdStory;
            oWordApplic.Selection.EndKey(ref unit, ref missing);

        }
        public void GoToTheBeginning()
        {
            // VB : Selection.HomeKey Unit:=wdStory
            object missing = System.Reflection.Missing.Value;
            object unit;
            unit = Word.WdUnits.wdStory;
            oWordApplic.Selection.HomeKey(ref unit, ref missing);

        }

        public void GoToTheTable(int ntable)
        {

            object missing = System.Reflection.Missing.Value;
            object what;
            what = Word.WdUnits.wdTable;
            object which;
            which = Word.WdGoToDirection.wdGoToFirst;
            object count;
            count = 1;
            oWordApplic.Selection.GoTo(ref what, ref which, ref count, ref missing);
            oWordApplic.Selection.Find.ClearFormatting();

            oWordApplic.Selection.Text = "";


        }

        public void GoToRightCell()
        {
            // Selection.MoveRight Unit:=wdCell

            object missing = System.Reflection.Missing.Value;
            object direction;
            direction = Word.WdUnits.wdCell;
            oWordApplic.Selection.MoveRight(ref direction, ref missing, ref missing);
        }

        public void GoToLeftCell()
        {
            // Selection.MoveRight Unit:=wdCell

            object missing = System.Reflection.Missing.Value;
            object direction;
            direction = Word.WdUnits.wdCell;
            oWordApplic.Selection.MoveLeft(ref direction, ref missing, ref missing);
        }

        public void GoToDownCell()
        {
            // Selection.MoveRight Unit:=wdCell

            object missing = System.Reflection.Missing.Value;
            object direction;
            direction = Word.WdUnits.wdLine;
            oWordApplic.Selection.MoveDown(ref direction, ref missing, ref missing);
        }

        public void GoToUpCell()
        {
            // Selection.MoveRight Unit:=wdCell

            object missing = System.Reflection.Missing.Value;
            object direction;
            direction = Word.WdUnits.wdLine;
            oWordApplic.Selection.MoveUp(ref direction, ref missing, ref missing);
        }


        // this function doesn't work
        public void InsertPageNumber(string strType, bool bHeader)
        {
            object missing = System.Reflection.Missing.Value;
            object alignment;
            object bFirstPage = false;
            object bF = true;
            //if (bHeader == true)
            //WordApplic.Selection.HeaderFooter.PageNumbers.ShowFirstPageNumber = bF;
            switch (strType)
            {
                case "Center":
                    alignment = Word.WdPageNumberAlignment.wdAlignPageNumberCenter;
                    oWordApplic.Selection.HeaderFooter.PageNumbers.Add(ref alignment, ref bFirstPage);
                    //Word.Selection objSelection = WordApplic.pSelection;

                    //oWordApplic.Selection.HeaderFooter.PageNumbers.Item(1).Alignment = Word.WdPageNumberAlignment.wdAlignPageNumberCenter;
                    break;
                case "Right":
                    alignment = Word.WdPageNumberAlignment.wdAlignPageNumberRight;
                    //oWordApplic.Selection.HeaderFooter.PageNumbers.Item(1).Alignment = Word.WdPageNumberAlignment.wdAlignPageNumberRight;
                    oWordApplic.Selection.HeaderFooter.PageNumbers.Add(ref alignment, ref bFirstPage);
                    break;
                case "Left":
                    alignment = Word.WdPageNumberAlignment.wdAlignPageNumberLeft;
                    oWordApplic.Selection.HeaderFooter.PageNumbers.Add(ref alignment, ref bFirstPage);
                    break;
            }

        }       
        

        #region IDisposable 成员

        public void Dispose()
        {
            object missing = System.Reflection.Missing.Value;
            if (oWordApplic != null)
            {
                oWordApplic.Application.Quit(ref missing, ref missing, ref missing);
            }
        }

        #endregion
    }

    public enum WOrientation
    {
        横板,
        竖板
    }


}
