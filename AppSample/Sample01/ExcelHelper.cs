using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using NPOI.XSSF.UserModel;
using Sample01.Model;

namespace Sample01
{
    /// <summary>
    /// main thread 에서 생성해야 함
    /// </summary>
    public static class ExcelHelper
    {
        public static XSSFWorkbook OpenWorkbook(string fileName)
        {
            try
            {
                using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    
                    XSSFWorkbook workBook  = new XSSFWorkbook(fs);
                    return workBook;
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine($"Failure open file <{fileName}> :: {e.Message}");
                return null;
            }
        }
        
        public static IEnumerable<IncomingTag> GetTagsInFile(string fileName)
        {
            var workbook = OpenWorkbook(fileName);

            var sheet = workbook.GetSheet("tags");
            var rowCount = sheet.LastRowNum;

            for (var i = 1; i <= rowCount; i++)
            {
                var rlow = sheet.GetRow(i);
                var name =  rlow.Cells[0].StringCellValue;
                Enum.TryParse<TagDeviceType>(rlow.Cells[1].StringCellValue, out var deviceType);
                Enum.TryParse<Channel>(rlow.Cells[2].StringCellValue, out var channel);
                var channelIndex = (ushort)rlow.Cells[3].NumericCellValue;
                Enum.TryParse<ElementaryDataType>(rlow.Cells[4].StringCellValue, out var dataType);

                var tag = new IncomingTag
                {
                    Name = name,
                    DeviceType = deviceType,
                    Channel = channel,
                    ChannelId = channelIndex,
                    DataType = dataType
                };

                yield return tag;
            }
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
