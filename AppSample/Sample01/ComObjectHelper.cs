using Microsoft.Office.Interop.Excel;

using System;
using System.IO;

using ExcelApp = Microsoft.Office.Interop.Excel.Application;

namespace Sample01
{
    public static class ComObjectHelper
    {
        public static void ReadExcelFromFile(string filePath)
        {
            if (!IsValidFile(filePath))
            {
                Console.WriteLine("You tried to open a invalid file!!");
                return;
            }

            ExcelApp xlApp;
            Workbook xlWorkBook;
            Worksheet xlWorkSheet;
            Range range;

            string str;
            int rowCount = 0; // 열 갯수
            int columnCount = 0; // 행 갯수

            xlApp = new ExcelApp();
            xlWorkBook = xlApp.Workbooks.Open(filePath, 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1); // 첫번째 시트를 가져 옴.

            range = xlWorkSheet.UsedRange; // 가져 온 시트의 데이터 범위 값

            for (rowCount = 1; rowCount <= range.Rows.Count; rowCount++)
            {
                var cell = range.Cells[rowCount, columnCount] as Range;

                for (columnCount = 1; columnCount <= range.Columns.Count; columnCount++)
                {
                    str = (string)(range.Cells[rowCount, columnCount] as Range).Value2; // 열과 행에 해당하는 데이터를 문자열로 반환
                    Console.WriteLine(str);
                }
            }

            xlWorkBook.Close(true, null, null);
            xlApp.Quit();

            ReleaseObject(xlWorkSheet);
            ReleaseObject(xlWorkBook);
            ReleaseObject(xlApp);
        }
        private static bool IsValidFile(string filePath)
        {
            FileInfo info = new FileInfo(filePath);
            try
            {
                if (info.Exists)
                {
                    if (info.Extension == ".xlsx" || info.Extension == ".xls")
                    {
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        private static void ReleaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch (Exception ex)
            {
                obj = null;
                Console.WriteLine("Unable to release the Object " + ex.ToString());
            }
            finally
            {
                GC.Collect();
            }
        }
    }
}
