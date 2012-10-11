using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Word = Microsoft.Office.Interop.Word;
using Microsoft.Office.Interop.Word;
using System.Data;  


namespace HHSoft.FieldProtect.FrameWork.Utility
{
    public class WordHelper1
    {
        private Word.Document wDoc = null;
        private Word.Application wApp = null;
        public Word.Document Document
        {
            get { return wDoc; }
            set { wDoc = value; }
        }

        public Word.Application Application
        {
            get { return wApp; }
            set { wApp = value; }
        }
        #region 从模板创建新的Word文档
        /// <summary>  
        /// 从模板创建新的Word文档  
        /// </summary>  
        /// <param name="templateName">模板文件名</param>  
        /// <returns></returns>  
        public bool CreateNewWordDocument(string templateName)
        {
            try
            {
                return CreateNewWordDocument(templateName, ref wDoc, ref wApp);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 从模板创建新的Word文档,并且返回对象Document,Application
        /// <summary>  
        /// 从模板创建新的Word文档，  
        /// </summary>  
        /// <param name="templateName">模板文件名</param>  
        /// <param name="wDoc">返回的Word.Document对象</param>  
        /// <param name="WApp">返回的Word.Application对象</param>  
        /// <returns></returns>  
        public static bool CreateNewWordDocument(string templateName, ref Word.Document wDoc, ref  Word.Application WApp)
        {
            Word.Document thisDocument = null;
            Word.Application thisApplication = new Word.ApplicationClass();
            thisApplication.Visible = false;
            thisApplication.Caption = "";
            thisApplication.Options.CheckSpellingAsYouType = false;
            thisApplication.Options.CheckGrammarAsYouType = false;

            Object Template = templateName;// Optional Object. The name of the template to be used for the new document. If this argument is omitted, the Normal template is used.  
            Object NewTemplate = false;// Optional Object. True to open the document as a template. The default value is False.  
            Object DocumentType = Word.WdNewDocumentType.wdNewBlankDocument; // Optional Object. Can be one of the following WdNewDocumentType constants: wdNewBlankDocument, wdNewEmailMessage, wdNewFrameset, or wdNewWebPage. The default constant is wdNewBlankDocument.  
            Object Visible = true;//Optional Object. True to open the document in a visible window. If this value is False, Microsoft Word opens the document but sets the Visible property of the document window to False. The default value is True.  

            try
            {
                Word.Document wordDoc = thisApplication.Documents.Add(ref Template, ref NewTemplate, ref DocumentType, ref Visible);

                thisDocument = wordDoc;
                wDoc = wordDoc;
                WApp = thisApplication;
                return true;
            }
            catch (Exception ex)
            {
                string err = string.Format("创建Word文档出错，错误原因：{0}", ex.Message);
                throw new Exception(err, ex);
            }
        }
        #endregion

        #region 文档另存为其他文件名
        /// <summary>  
        /// 文档另存为其他文件名  
        /// </summary>  
        /// <param name="fileName">文件名</param>  
        /// <param name="wDoc">Document对象</param>  
        public bool SaveAs(string fileName)
        {
            try
            {
                return SaveAs(fileName, wDoc);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 文档另存为其他文件名
        /// <summary>  
        /// 文档另存为其他文件名  
        /// </summary>  
        /// <param name="fileName">文件名</param>  
        /// <param name="wDoc">Document对象</param>  
        public static bool SaveAs(string fileName, Word.Document wDoc)
        {
            Object FileName = fileName; // 文档的名称。默认值是当前文件夹名和文件名。如果文档在以前没有保存过，则使用默认名称（例如，Doc1.doc）。如果已经存在具有指定文件名的文档，则会在不先提示用户的情况下改写文档。  
            Object FileFormat = Word.WdSaveFormat.wdFormatDocument; // 文档的保存格式。可以是任何 WdSaveFormat 值。要以另一种格式保存文档，请为 SaveFormat 属性指定适当的值。  
            Object LockComments = false; // 如果为 true，则锁定文档以进行注释。默认值为 false。  
            Object Password = System.Type.Missing; // 用来打开文档的密码字符串。（请参见下面的备注。）  
            Object AddToRecentFiles = false; // 如果为 true，则将该文档添加到“文件”菜单上最近使用的文件列表中。默认值为 true。  
            Object WritePassword = System.Type.Missing; // 用来保存对文件所做更改的密码字符串。（请参见下面的备注。）  
            Object ReadOnlyRecommended = false; // 如果为 true，则让 Microsoft Office Word 在打开文档时建议只读状态。默认值为 false。  
            Object EmbedTrueTypeFonts = false; //如果为 true，则将 TrueType 字体随文档一起保存。如果省略的话，则 EmbedTrueTypeFonts 参数假定 EmbedTrueTypeFonts 属性的值。  
            Object SaveNativePictureFormat = true; // 如果图形是从另一个平台（例如，Macintosh）导入的，则 true 表示仅保存导入图形的 Windows 版本。  
            Object SaveFormsData = false; // 如果为 true，则将用户在窗体中输入的数据另存为数据记录。  
            Object SaveAsAOCELetter = false; // 如果文档附加了邮件程序，则 true 表示会将文档另存为 AOCE 信函（邮件程序会进行保存）。  
            Object Encoding = System.Type.Missing; // MsoEncoding。要用于另存为编码文本文件的文档的代码页或字符集。默认值是系统代码页。  
            Object InsertLineBreaks = true; // 如果文档另存为文本文件，则 true 表示在每行文本末尾插入分行符。  
            Object AllowSubstitutions = false; //如果文档另存为文本文件，则 true 允许 Word 将某些符号替换为外观与之类似的文本。例如，将版权符号显示为 (c)。默认值为 false。  
            Object LineEnding = Word.WdLineEndingType.wdCRLF;// Word 在另存为文本文件的文档中标记分行符和换段符。可以是任何 WdLineEndingType 值。  
            Object AddBiDiMarks = true;//如果为 true，则向输出文件添加控制字符，以便保留原始文档中文本的双向布局。  
            try
            {
                wDoc.SaveAs(ref FileName, ref FileFormat, ref LockComments, ref Password, ref AddToRecentFiles, ref WritePassword
                        , ref ReadOnlyRecommended, ref EmbedTrueTypeFonts, ref SaveNativePictureFormat
                        , ref SaveFormsData, ref SaveAsAOCELetter, ref Encoding, ref InsertLineBreaks, ref AllowSubstitutions
                        , ref LineEnding, ref AddBiDiMarks);
                return true;
            }
            catch (Exception ex)
            {
                string err = string.Format("另存文件出错，错误原因：{0}", ex.Message);
                throw new Exception(err, ex);
            }
        }
        #endregion

        #region 关闭文档
        /// <summary>  
        /// 关闭文档  
        /// </summary>  
        public void Close()
        {
            Close(wDoc, wApp);
            wDoc = null;
            wApp = null;
        }
        #endregion

        #region 关闭文档
        /// <summary>  
        /// 关闭文档  
        /// </summary>  
        /// <param name="wDoc">Document对象</param>  
        /// <param name="WApp">Application对象</param>  
        public static void Close(Word.Document wDoc, Word.Application WApp)
        {
            Object SaveChanges = Word.WdSaveOptions.wdSaveChanges;// 指定文档的保存操作。可以是下列 WdSaveOptions 值之一：wdDoNotSaveChanges、wdPromptToSaveChanges 或 wdSaveChanges。  
            Object OriginalFormat = Word.WdOriginalFormat.wdOriginalDocumentFormat;// 指定文档的保存格式。可以是下列 WdOriginalFormat 值之一：wdOriginalDocumentFormat、wdPromptUser 或 wdWordDocument。  
            Object RouteDocument = false;// 如果为 true，则将文档传送给下一个收件人。如果没有为文档附加传送名单，则忽略此参数。  
            try
            {
                if (wDoc != null) wDoc.Close(ref SaveChanges, ref OriginalFormat, ref RouteDocument);
                if (WApp != null) WApp.Quit(ref SaveChanges, ref OriginalFormat, ref RouteDocument);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 填充书签
        /// <summary>  
        /// 填充书签  
        /// </summary>  
        /// <param name="bookmark">书签</param>  
        /// <param name="value">值</param>  
        public void Replace(string bookmark, string value)
        {
            try
            {
                object bkObj = bookmark;
                if (wApp.ActiveDocument.Bookmarks.Exists(bookmark) == true)
                {
                    wApp.ActiveDocument.Bookmarks.get_Item(ref bkObj).Select();
                }
                else return;
                wApp.Selection.TypeText(value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public bool FindTable(string bookmarkTable)
        {
            try
            {
                object bkObj = bookmarkTable;
                if (wApp.ActiveDocument.Bookmarks.Exists(bookmarkTable) == true)
                {
                    wApp.ActiveDocument.Bookmarks.get_Item(ref bkObj).Select();
                    return true;
                }
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MoveNextCell()
        {
            try
            {
                Object unit = Word.WdUnits.wdCell;
                Object count = 1;
                wApp.Selection.Move(ref unit, ref count);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SetCellValue(string value)
        {
            try
            {
                wApp.Selection.TypeText(value);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void MoveNextRow()
        {
            try
            {
                Object extend = Word.WdMovementType.wdExtend;
                Object unit = Word.WdUnits.wdCell;
                Object count = 1;
                wApp.Selection.MoveRight(ref unit, ref count, ref extend);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void TableAddRow(int iTableNo)
        {
            Object missing = System.Reflection.Missing.Value;
            Word.Table characterTable = wDoc.Tables[iTableNo];
            characterTable.Rows.Add(ref missing);
        }

        public void TableOperate(int iTableNo, int iRow, int iCol, string strValue)
        {
            Object missing = System.Reflection.Missing.Value;
            Word.Table characterTable = wDoc.Tables[iTableNo];
            //talbe的第一个单元格为cell(1,1)
            characterTable.Cell(iRow, iCol).Range.Text = strValue;
            characterTable.Cell(iRow, iCol).Range.Font.Bold = 0;
            wApp.Selection.ParagraphFormat.Alignment = WdParagraphAlignment.wdAlignParagraphCenter;
        }

        public void MergeTableCell(string[] MergeAry)
        {
            Word.Table characterTable = wDoc.Tables[1];
            //foreach (string s in sCol.Split(','))
            //{
            //    characterTable.Cell(iRowBegin, int.Parse(s)).Merge(characterTable.Cell(iRowEnd, int.Parse(s)));
            //    characterTable.Cell(iRowBegin, int.Parse(s)).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;                
            //}

            foreach (string mStr in MergeAry)
            {
                string[] cellStr = mStr.Split(',');
                if (int.Parse(cellStr[0]) != int.Parse(cellStr[2]) || int.Parse(cellStr[1]) != int.Parse(cellStr[3]))
                {
                    characterTable.Cell(int.Parse(cellStr[0]), int.Parse(cellStr[1]))
                    .Merge(characterTable.Cell(int.Parse(cellStr[2]), int.Parse(cellStr[3])));
                    characterTable.Cell(int.Parse(cellStr[0]), int.Parse(cellStr[1])).VerticalAlignment = WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                }                
            }
        }

        #region - 页面设置 -


        public void SetPage(WOrientation orientation, double width, double height, double topMargin,
            double leftMargin, double rightMargin, double bottomMargin)
        {
            wDoc.PageSetup.PageWidth = wApp.CentimetersToPoints((float)width);
            wDoc.PageSetup.PageHeight = wApp.CentimetersToPoints((float)height);
            if (orientation == WOrientation.横板)
            {
                wDoc.PageSetup.Orientation = Microsoft.Office.Interop.Word.WdOrientation.wdOrientLandscape;
            }
            wDoc.PageSetup.TopMargin = (float)(topMargin * 25);//上边距      
            wDoc.PageSetup.LeftMargin = (float)(leftMargin * 25);//左边距         
            wDoc.PageSetup.RightMargin = (float)(rightMargin * 25);//右边距  
            wDoc.PageSetup.BottomMargin = (float)(bottomMargin * 25);//下边距       
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
            para = wDoc.Content.Paragraphs.Add(ref missing);
            object pBreak = (int)WdBreakType.wdSectionBreakNextPage;
            para.Range.InsertBreak(ref pBreak);
        }
        #endregion


        public bool InsertTable(DataTable dt, bool haveBorder, double[] colWidths, string[] MergeAry)
        {
            try
            {
                object Nothing = System.Reflection.Missing.Value;
                int lenght = wDoc.Characters.Count - 1;
                object start = lenght;
                object end = lenght;
                //表格起始坐标                
                Microsoft.Office.Interop.Word.Range tableLocation = wDoc.Range(ref start, ref end);
                //添加Word表格                     
                Microsoft.Office.Interop.Word.Table table =
                    wDoc.Tables.Add(tableLocation, dt.Rows.Count, dt.Columns.Count, ref Nothing, ref Nothing);
                if (colWidths != null)
                {
                    for (int i = 0; i < colWidths.Length; i++)
                    {
                        table.Columns[i + 1].Width = (float)(28.5F * colWidths[i]);
                    }
                }

                ///设置TABLE的样式                
                table.Rows.HeightRule = Microsoft.Office.Interop.Word.WdRowHeightRule.wdRowHeightAtLeast;
                table.Rows.Height = wApp.CentimetersToPoints(float.Parse("0.8"));
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
            return InsertTable(dt, haveBorder, null, null);
        }
        public bool InsertTable(DataTable dt)
        {
            return InsertTable(dt, true, null, null);
        }
    }  

}
