/*******************************************************************************
 * Copyright © 2016 NFine.Framework 版权所有
 * Author: Sdaulld
 * Description: NFine快速开发平台
 * Website：http://www.nfine.cn
*********************************************************************************/
using Newtonsoft.Json.Linq;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NFine.Code
{
    public class OfficeHelper
    {
        private static bool ReadExcelToDataTable(ISheet sheet, ref string strMsg, out DataTable data)
        {
            bool bRet = true;
            //定义要返回的datatable对象
            data = new DataTable();
            try
            {
                if (sheet != null)
                {
                    data.TableName = sheet.SheetName;
                    int cellCount = 0;
                    //数据开始行(排除标题行)
                    int startRow = sheet.FirstRowNum;
                    IRow firstRow = sheet.GetRow(0);
                    //一行最后一个cell的编号 即总的列数
                    cellCount = firstRow.LastCellNum;
                    //如果第一行是标题列名

                    try
                    {
                        if (cellCount <= 0)
                        {
                            bRet = false;
                            strMsg = "找不到代表表头的行信息";
                            return bRet;
                        }
                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                        {
                            ICell cell = firstRow.GetCell(i);
                            if (cell != null)
                            {
                                string cellValue = cell.StringCellValue;
                                if (cellValue != null)
                                {
                                    DataColumn column = new DataColumn(cellValue);
                                    data.Columns.Add(column);
                                }
                            }
                        }
                    }
                    catch (DuplicateNameException colDE)
                    {
                        bRet = false;
                        strMsg = "Excel存在重复的列";
                        return bRet;
                    }
                    startRow = sheet.FirstRowNum + 1;

                    //最后一列的标号
                    int rowCount = sheet.LastRowNum;
                    for (int i = startRow; i <= rowCount; ++i)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null || row.Cells.Count == 0) continue; //没有数据的行默认是null　　　　　　　

                        DataRow dataRow = data.NewRow();
                        for (int j = row.FirstCellNum; j < cellCount; ++j)
                        {
                            var celldata = row.GetCell(j);
                            if (row.GetCell(j) != null)
                            {
                                switch (celldata.CellType)
                                {
                                    case CellType.Blank: //空数据类型处理
                                        dataRow[j] = "";
                                        break;
                                    case CellType.String: //字符串类型
                                        dataRow[j] = celldata.StringCellValue.Trim();
                                        break;
                                    case CellType.Numeric: //数字类型
                                        if (DateUtil.IsCellDateFormatted(celldata))
                                        {
                                            dataRow[j] = celldata.DateCellValue.ToString("yyyy/MM/dd HH:mm:ss");
                                        }
                                        else
                                        {
                                            dataRow[j] = celldata.NumericCellValue;
                                        }
                                        break;

                                    default:
                                        dataRow[j] = celldata.StringCellValue.Trim();
                                        break;
                                }
                            }
                        }
                        data.Rows.Add(dataRow);
                    }
                }
            }
            catch (Exception ex)
            {
                strMsg = "读取Excel文件失败。";
            }
            return bRet;
        }

        /// <summary>
        /// 将excel文件内容读取到DataTable数据表中
        /// </summary>
        /// <param name="fileName">文件完整路径名</param>
        /// <param name="data">返回的DataTable数据表</param>
        /// <param name="strMsg">返回的错误消息</param>
        /// <param name="sheetName">指定读取excel工作薄sheet的名称</param>
        /// <param name="isFirstRowColumn">第一行是否是DataTable的列名：true=是，false=否</param>
        /// <param name="isDeleteFile">是否删除已读取的文件</param>
        /// <returns>是否正确读取文件</returns>
        /// <summary>
        public static bool ReadExcelToDataTable(string fileName, out DataTable data, ref string strMsg, string sheetName = null, bool isFirstRowColumn = true, bool isDeleteFile = false)
        {
            bool bRet = true;
            //定义要返回的datatable对象
            data = new DataTable();
            //excel工作表
            ISheet sheet = null;
            try
            {
                if (!File.Exists(fileName))
                {
                    strMsg = $"找不到文件[{fileName}]";
                    return false;
                }
                //根据指定路径读取文件
                using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    //根据文件流创建excel数据结构
                    IWorkbook workbook = WorkbookFactory.Create(fs);
                    //IWorkbook workbook = new HSSFWorkbook(fs);
                    //如果有指定工作表名称
                    if (!string.IsNullOrEmpty(sheetName))
                    {
                        sheet = workbook.GetSheet(sheetName);
                        //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                        if (sheet == null)
                        {
                            sheet = workbook.GetSheetAt(0);
                        }
                    }
                    else
                    {
                        //如果没有指定的sheetName，则尝试获取第一个sheet
                        try
                        {

                            sheet = workbook.GetSheetAt(0);
                        }
                        catch (Exception ex)
                        {

                            // throw;
                        }
                    }

                    if (sheet != null)
                    {
                        bRet &= ReadExcelToDataTable(sheet, ref strMsg, out data);
                    }
                    else
                    {
                        strMsg = $"找不到sheet页{sheetName}";
                        bRet = false;
                    }
                }
                if (isDeleteFile && File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
            }
            catch (Exception ex)
            {
                strMsg = "Excel文件不正确，打开失败";
                bRet = false;
            }
            return bRet;
        }

        /// <summary>
        /// 将特定行文本读取到数组中
        /// </summary>
        /// <param name="fileName">文件完整路径名</param>
        /// <param name="sheetName">指定读取excel工作薄sheet的名称</param>
        /// <param name="rowIndex">行索引</param>
        /// <returns>数组</returns>
        public static string[] ReadExcelOneRow(string fileName, string sheetName = null, int rowIndex = 0)
        {
            //定义要返回的datatable对象
            List<string> data = new List<string>();
            //excel工作表
            ISheet sheet = null;
            try
            {
                if (!File.Exists(fileName))
                {
                    return null;
                }
                //根据指定路径读取文件
                using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    //根据文件流创建excel数据结构
                    IWorkbook workbook = WorkbookFactory.Create(fs);

                    //IWorkbook workbook = new HSSFWorkbook(fs);
                    //如果有指定工作表名称
                    if (!string.IsNullOrEmpty(sheetName))
                    {
                        sheet = workbook.GetSheet(sheetName);
                        //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                        if (sheet == null)
                        {
                            sheet = workbook.GetSheetAt(0);
                        }
                    }
                    else
                    {
                        //如果没有指定的sheetName，则尝试获取第一个sheet
                        sheet = workbook.GetSheetAt(0);
                    }
                }

                if (sheet != null)
                {
                    IRow firstRow = sheet.GetRow(rowIndex);
                    //一行最后一个cell的编号 即总的列数
                    int cellCount = firstRow.LastCellNum;

                    for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                    {
                        ICell cell = firstRow.GetCell(i);
                        if (cell != null)
                        {
                            string cellValue = cell.StringCellValue;
                            data.Add(cellValue);
                        }
                        else
                        {
                            data.Add(string.Empty);
                        }
                    }
                }

                return data.ToArray();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 将excel文件内容读取到DataTable数据表中
        /// </summary>
        /// <param name="fileName">文件完整路径名</param>
        /// <param name="sheetName">指定读取excel工作薄sheet的名称</param>
        /// <param name="isFirstRowColumn">第一行是否是DataTable的列名：true=是，false=否</param>
        /// <param name="isDeleteFile">是否删除已读取的文件</param>
        /// <returns>DataTable数据表</returns>
        public static Dictionary<string,DataTable> ReadAllSheetToDataTable(string fileName, bool isFirstRowColumn = true, bool isDeleteFile = false, int firstRowIndex = 0)
        {
            //定义要返回的datatable对象
            Dictionary<string, DataTable> dataTables = new Dictionary<string, DataTable>();
            //excel工作表
            List<ISheet> sheets = new List<ISheet>();

            try
            {
                if (!File.Exists(fileName))
                {
                    return null;
                }
                //根据指定路径读取文件
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                //根据文件流创建excel数据结构
                IWorkbook workbook = WorkbookFactory.Create(fs);
                //IWorkbook workbook = new HSSFWorkbook(fs);
                //如果有指定工作表名称
                int sheetCount = workbook.NumberOfSheets;
                if (sheetCount < 0)
                    return null;
                for (int i = 0; i < sheetCount; i++)
                {
                    sheets.Add(workbook.GetSheetAt(i));
                }
                sheets.ForEach(sheet =>
                {
                    DataTable data = TranslateSheetToDataTable(sheet, isFirstRowColumn, firstRowIndex);
                    if (dataTables.ContainsKey(sheet.SheetName) == false)
                        dataTables.Add(sheet.SheetName,data);
                });
                if (isDeleteFile)
                {
                    File.Delete(fileName);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dataTables;
        }

        private static DataTable TranslateSheetToDataTable(ISheet sheet, bool isFirstRowColumn, int firstRowIndex)
        {
            //数据开始行(排除标题行)
            int startRow = 0;
            DataTable data = new DataTable();
            if (sheet != null)
            {
                IRow firstRow = sheet.GetRow(0);
                //一行最后一个cell的编号 即总的列数
                int cellCount = firstRow.LastCellNum;
                //如果第一行是标题列名
                if (isFirstRowColumn)
                {
                    startRow = sheet.FirstRowNum + 1;
                }
                else
                {
                    var tempFirstRow = sheet.GetRow(firstRowIndex);
                    if (tempFirstRow != null)
                    {
                        firstRow = tempFirstRow;
                    }
                    startRow = firstRowIndex + 1;
                }

                for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                {
                    ICell cell = firstRow.GetCell(i);
                    if (cell != null)
                    {
                        string cellValue = cell.StringCellValue;
                        if (cellValue != null)
                        {
                            DataColumn column = new DataColumn(cellValue);
                            data.Columns.Add(column);
                        }
                    }
                }
                //最后一列的标号
                int rowCount = sheet.LastRowNum;
                for (int i = startRow; i <= rowCount; ++i)
                {
                    IRow row = sheet.GetRow(i);
                    if (row == null || row.Cells.Count == 0) continue; //没有数据的行默认是null　　　　　　　

                    DataRow dataRow = data.NewRow();
                    for (int j = row.FirstCellNum; j < cellCount; ++j)
                    {
                        var celldata = row.GetCell(j);
                        if (row.GetCell(j) != null)
                        {
                            switch (celldata.CellType)
                            {
                                case CellType.Blank: //空数据类型处理
                                    dataRow[j] = "";
                                    break;
                                case CellType.String: //字符串类型
                                    dataRow[j] = celldata.StringCellValue.Trim();
                                    break;
                                case CellType.Numeric: //数字类型
                                    if (DateUtil.IsCellDateFormatted(celldata))
                                    {
                                        dataRow[j] = celldata.DateCellValue.ToString("yyyy/MM/dd HH:mm:ss");
                                    }
                                    else
                                    {
                                        dataRow[j] = celldata.NumericCellValue;
                                    }
                                    break;

                                default:
                                    dataRow[j] = celldata.StringCellValue.Trim();
                                    //同理，没有数据的单元格都默认是null
                                    //dataRow[j] = row.GetCell(j).ToString().Trim();
                                    break;
                            }
                        }
                    }
                    data.Rows.Add(dataRow);
                }
            }

            return data;
        }

        /// <summary>
        /// 将excel文件内容读取到DataTable数据表中
        /// </summary>
        /// <param name="fileName">文件完整路径名</param>
        /// <param name="sheetName">指定读取excel工作薄sheet的名称</param>
        /// <param name="isFirstRowColumn">第一行是否是DataTable的列名：true=是，false=否</param>
        /// <param name="isDeleteFile">是否删除已读取的文件</param>
        /// <returns>DataTable数据表</returns>
        public static DataTable ReadExcelToDataTable(string fileName, string sheetName = null, bool isFirstRowColumn = true, bool isDeleteFile = false, int firstRowIndex = 0)
        {
            //定义要返回的datatable对象
            DataTable data = new DataTable();
            //excel工作表
            ISheet sheet = null;
            try
            {
                if (!File.Exists(fileName))
                {
                    return null;
                }
                //根据指定路径读取文件
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                //根据文件流创建excel数据结构
                IWorkbook workbook = WorkbookFactory.Create(fs);
                //IWorkbook workbook = new HSSFWorkbook(fs);
                //如果有指定工作表名称
                if (!string.IsNullOrEmpty(sheetName))
                {
                    sheet = workbook.GetSheet(sheetName);
                    //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                    if (sheet == null)
                    {
                        sheet = workbook.GetSheetAt(0);
                    }
                }
                else
                {
                    //如果没有指定的sheetName，则尝试获取第一个sheet
                    sheet = workbook.GetSheetAt(0);
                }
                if (sheet != null)
                {
                    data = TranslateSheetToDataTable(sheet, isFirstRowColumn, firstRowIndex);
                }
                if (isDeleteFile)
                {
                    File.Delete(fileName);
                }
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 将excel文件内容读取到DataTable数据表中
        /// </summary>
        /// <param name="fileName">文件完整路径名</param>
        /// <param name="sheetName">指定读取excel工作薄sheet的名称</param>
        /// <param name="isDeleteFile">是否删除已读取的文件</param>
        /// <returns>DataTable数据表</returns>
        public static bool ReadExcelToDataSet(string fileName, out DataSet alldata, ref string strMsg, bool isDeleteFile = false)
        {
            bool bRet = true;
            //定义要返回的datatable对象
            alldata = new DataSet();
            //数据开始行(排除标题行)
            int startRow = 0;
            try
            {
                if (!File.Exists(fileName))
                {
                    strMsg = $"找不到文件[{fileName}]";
                    bRet = false;
                    return bRet;
                }
                //根据指定路径读取文件
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                //根据文件流创建excel数据结构
                IWorkbook workbook = WorkbookFactory.Create(fs);
                int len = workbook.NumberOfSheets;
                for (int sindex = 0; sindex < len; sindex++)
                {
                    ISheet sheet = workbook.GetSheetAt(sindex);

                    if (sheet != null)
                    {
                        bRet &= ReadExcelToDataTable(sheet, ref strMsg, out DataTable data);
                        data.TableName = sheet.SheetName;
                        alldata.Tables.Add(data);
                    }
                    else
                    {
                        strMsg = $"找不到第[{sindex + 1}]个sheet页";
                        bRet = false;
                        return bRet;
                    }
                }
                if (isDeleteFile && File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
            }
            catch (DuplicateNameException colEx)
            {
                strMsg = "Excel的sheet页名称重复";
                bRet = false;
            }
            catch (Exception ex)
            {
                strMsg = $"发生未知错误，读取Excel失败.错误原因{ex.Message}";
                bRet = false;
            }
            return bRet;
        }

        /// <summary>
        /// 将excel文件内容读取到DataTable数据表中
        /// </summary>
        /// <param name="fileName">文件完整路径名</param>
        /// <param name="sheetName">指定读取excel工作薄sheet的名称</param>
        /// <param name="isDeleteFile">是否删除已读取的文件</param>
        /// <returns>DataTable数据表</returns>
        public static DataSet ReadExcelToDataSet(string fileName, bool isDeleteFile = false)
        {
            //定义要返回的datatable对象
            DataSet alldata = new DataSet();
            //数据开始行(排除标题行)
            int startRow = 0;
            try
            {
                if (!File.Exists(fileName))
                {
                    return null;
                }
                //根据指定路径读取文件
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                //根据文件流创建excel数据结构
                IWorkbook workbook = WorkbookFactory.Create(fs);
                int len = workbook.NumberOfSheets;
                for (int sindex = 0; sindex < len; sindex++)
                {
                    ISheet sheet = workbook.GetSheetAt(sindex);
                    DataTable data = new DataTable();

                    if (sheet != null)
                    {
                        data.TableName = sheet.SheetName;
                        IRow firstRow = sheet.GetRow(0);
                        //一行最后一个cell的编号 即总的列数
                        int cellCount = firstRow.LastCellNum;
                        //如果第一行是标题列名
                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                        {
                            ICell cell = firstRow.GetCell(i);
                            if (cell != null)
                            {
                                string cellValue = cell.StringCellValue;
                                if (cellValue != null)
                                {
                                    DataColumn column = new DataColumn(cellValue);
                                    data.Columns.Add(column);
                                }
                            }
                        }
                        data.Columns.Add(new DataColumn("SheetRowNum"));//行号
                        startRow = sheet.FirstRowNum + 1;

                        //最后一列的标号
                        int rowCount = sheet.LastRowNum;
                        for (int i = startRow; i <= rowCount; ++i)
                        {
                            IRow row = sheet.GetRow(i);
                            if (row == null || row.Cells.Count == 0) continue; //没有数据的行默认是null　　　　　　　

                            DataRow dataRow = data.NewRow();
                            dataRow[data.Columns.Count - 1] = i.ToString();
                            for (int j = row.FirstCellNum; j < cellCount; ++j)
                            {
                                if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
                                {
                                    ICell celldata = row.GetCell(j);
                                    switch (celldata.CellType)
                                    {
                                        case CellType.Blank: //空数据类型处理
                                            dataRow[j] = "";
                                            break;
                                        case CellType.String: //字符串类型
                                            dataRow[j] = celldata.StringCellValue.Trim();
                                            break;
                                        case CellType.Numeric: //数字类型
                                            if (DateUtil.IsCellDateFormatted(celldata))
                                            {
                                                dataRow[j] = celldata.DateCellValue.ToString("yyyy/MM/dd HH:mm:ss");
                                            }
                                            else
                                            {
                                                dataRow[j] = celldata.NumericCellValue;
                                            }
                                            break;

                                        default:
                                            dataRow[j] = celldata.StringCellValue.Trim();
                                            //同理，没有数据的单元格都默认是null
                                            //dataRow[j] = row.GetCell(j).ToString().Trim();
                                            break;
                                    }
                                }
                            }
                            int flagnull = 1;//改行是否全部为空的标志
                            foreach (DataColumn item in dataRow.Table.Columns)
                            {
                                if (dataRow[item.ColumnName] != null && dataRow[item.ColumnName] != DBNull.Value && dataRow[item.ColumnName].ToString().Trim() != "")
                                {
                                    flagnull = 0;
                                    break;
                                }
                            }
                            if (flagnull == 1)//改行全部为空
                            {
                                continue;
                            }
                            data.Rows.Add(dataRow);
                        }
                    }
                    alldata.Tables.Add(data);
                }
                if (isDeleteFile)
                {
                    File.Delete(fileName);
                }
                return alldata;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 将文件流读取到DataTable数据表中 第一行是DataTable的列名
        /// </summary>
        /// <param name="fileStream">文件流</param>
        /// <param name="sheetName">指定读取excel工作薄sheet的名称</param>
        /// <returns>DataTable数据表</returns>
        public static bool ReadStreamToDataTable(Stream fileStream, out DataTable data, ref string strMsg, string sheetName = null)
        {
            bool bRet = true;
            //定义要返回的datatable对象
            data = new DataTable();
            //excel工作表
            ISheet sheet = null;
            try
            {
                //根据文件流创建excel数据结构,NPOI的工厂类WorkbookFactory会自动识别excel版本，创建出不同的excel数据结构
                IWorkbook workbook = WorkbookFactory.Create(fileStream);
                //如果有指定工作表名称
                if (!string.IsNullOrEmpty(sheetName))
                {
                    sheet = workbook.GetSheet(sheetName);
                    //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                    if (sheet == null)
                    {
                        sheet = workbook.GetSheetAt(0);
                    }
                }
                else
                {
                    //如果没有指定的sheetName，则尝试获取第一个sheet
                    sheet = workbook.GetSheetAt(0);
                }

                if (sheet != null)
                {
                    bRet &= ReadExcelToDataTable(sheet, ref strMsg, out data);
                }
                else
                {
                    strMsg = $"Excel中找不到sheet页{sheetName}";
                    bRet = false;
                    return bRet;
                }
            }
            catch (DuplicateNameException colEx)
            {
                strMsg = "Excel的sheet页名称重复";
                bRet = false;
            }
            catch (Exception ex)
            {
                strMsg = $"发生未知错误，读取Excel失败.";
                bRet = false;
            }
            return bRet;
        }


        /// <summary>
        /// 将文件流读取到DataTable数据表中
        /// </summary>
        /// <param name="fileStream">文件流</param>
        /// <param name="sheetName">指定读取excel工作薄sheet的名称</param>
        /// <param name="isFirstRowColumn">第一行是否是DataTable的列名：true=是，false=否</param>
        /// <returns>DataTable数据表</returns>
        public static DataTable ReadStreamToDataTable(Stream fileStream, string sheetName = null, bool isFirstRowColumn = true)
        {
            //定义要返回的datatable对象
            DataTable data = new DataTable();
            //excel工作表
            ISheet sheet = null;
            //数据开始行(排除标题行)
            int startRow = 0;
            try
            {
                //根据文件流创建excel数据结构,NPOI的工厂类WorkbookFactory会自动识别excel版本，创建出不同的excel数据结构
                IWorkbook workbook = WorkbookFactory.Create(fileStream);
                //如果有指定工作表名称
                if (!string.IsNullOrEmpty(sheetName))
                {
                    sheet = workbook.GetSheet(sheetName);
                    //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                    if (sheet == null)
                    {
                        sheet = workbook.GetSheetAt(0);
                    }
                }
                else
                {
                    //如果没有指定的sheetName，则尝试获取第一个sheet
                    sheet = workbook.GetSheetAt(0);
                }
                if (sheet != null)
                {
                    IRow firstRow = sheet.GetRow(0);
                    //一行最后一个cell的编号 即总的列数
                    int cellCount = firstRow.LastCellNum;
                    //如果第一行是标题列名
                    if (isFirstRowColumn)
                    {
                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                        {
                            ICell cell = firstRow.GetCell(i);
                            if (cell != null)
                            {
                                string cellValue = cell.StringCellValue;
                                if (cellValue != null)
                                {
                                    DataColumn column = new DataColumn(cellValue);
                                    data.Columns.Add(column);
                                }
                            }
                        }
                        startRow = sheet.FirstRowNum + 1;
                    }
                    else
                    {
                        startRow = sheet.FirstRowNum;
                    }
                    //最后一列的标号
                    int rowCount = sheet.LastRowNum;
                    for (int i = startRow; i <= rowCount; ++i)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null || row.FirstCellNum < 0) continue; //没有数据的行默认是null　　　　　　　

                        DataRow dataRow = data.NewRow();
                        for (int j = row.FirstCellNum; j < cellCount; ++j)
                        {
                            //同理，没有数据的单元格都默认是null
                            ICell cell = row.GetCell(j);
                            if (cell != null)
                            {
                                if (cell.CellType == CellType.Numeric)
                                {
                                    //判断是否日期类型
                                    if (DateUtil.IsCellDateFormatted(cell))
                                    {
                                        dataRow[j] = row.GetCell(j).DateCellValue;
                                    }
                                    else
                                    {
                                        dataRow[j] = row.GetCell(j).ToString().Trim();
                                    }
                                }
                                else
                                {
                                    dataRow[j] = row.GetCell(j).ToString().Trim();
                                }
                            }
                        }
                        data.Rows.Add(dataRow);
                    }
                }
                return data;
            }
            catch (DuplicateNameException colEx)
            {
                throw new Exception("请使用模板导入正确的Excel文件", colEx);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 将Excel文件流读取到DataSet数据集中
        /// </summary>
        /// <param name="fileStream">文件流</param>
        /// <param name="sheetName">指定读取excel工作薄sheet的名称</param>
        /// <returns>DataSet数据集</returns>
        public static bool ReadStreamToDataSet(Stream fileStream, out DataSet alldata, ref string strMsg)
        {
            bool bRet = true;
            //定义要返回的dataset对象
            alldata = new DataSet();
            try
            {
                //根据文件流创建excel数据结构,NPOI的工厂类WorkbookFactory会自动识别excel版本，创建出不同的excel数据结构
                IWorkbook workbook = WorkbookFactory.Create(fileStream);
                int len = workbook.NumberOfSheets;
                for (int sindex = 0; sindex < len; sindex++)
                {
                    ISheet sheet = workbook.GetSheetAt(sindex);
                    DataTable data = new DataTable();

                    if (sheet != null)
                    {
                        bRet &= ReadExcelToDataTable(sheet, ref strMsg, out data);
                        data.TableName = sheet.SheetName;
                    }
                    else
                    {
                        strMsg = $"找不到第[{sindex + 1}]个sheet页";
                        bRet = false;
                        return bRet;
                    }
                    alldata.Tables.Add(data);
                }

            }
            catch (DuplicateNameException colEx)
            {
                strMsg = "Excel的sheet页名称重复";
                bRet = false;
            }
            catch (Exception ex)
            {
                strMsg = $"发生未知错误，读取Excel失败.";
                bRet = false;
            }
            return bRet;
        }

        /// <summary>
        /// 将Excel文件流读取到DataSet数据集中
        /// </summary>
        /// <param name="fileStream">文件流</param>
        /// <param name="sheetName">指定读取excel工作薄sheet的名称</param>
        /// <returns>DataSet数据集</returns>
        public static DataSet ReadStreamToDataSet(Stream fileStream)
        {
            //定义要返回的datatable对象
            DataSet alldata = new DataSet();
            //数据开始行(排除标题行)
            int startRow = 0;
            try
            {
                //根据文件流创建excel数据结构,NPOI的工厂类WorkbookFactory会自动识别excel版本，创建出不同的excel数据结构
                IWorkbook workbook = WorkbookFactory.Create(fileStream);
                int len = workbook.NumberOfSheets;
                for (int sindex = 0; sindex < len; sindex++)
                {
                    ISheet sheet = workbook.GetSheetAt(sindex);
                    DataTable data = new DataTable();

                    if (sheet != null)
                    {
                        data.TableName = sheet.SheetName;
                        IRow firstRow = sheet.GetRow(0);
                        //一行最后一个cell的编号 即总的列数
                        int cellCount = firstRow.LastCellNum;
                        //如果第一行是标题列名

                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                        {
                            ICell cell = firstRow.GetCell(i);
                            if (cell != null)
                            {
                                string cellValue = cell.StringCellValue;
                                if (cellValue != null)
                                {
                                    DataColumn column = new DataColumn(cellValue);
                                    data.Columns.Add(column);
                                }
                            }
                        }
                        startRow = sheet.FirstRowNum + 1;

                        //最后一列的标号
                        int rowCount = sheet.LastRowNum;
                        for (int i = startRow; i <= rowCount; ++i)
                        {
                            IRow row = sheet.GetRow(i);
                            if (row == null || row.FirstCellNum < 0) continue; //没有数据的行默认是null　　　　　　　

                            DataRow dataRow = data.NewRow();
                            for (int j = row.FirstCellNum; j < cellCount; ++j)
                            {
                                //同理，没有数据的单元格都默认是null
                                ICell cell = row.GetCell(j);
                                if (cell != null)
                                {
                                    if (cell.CellType == CellType.Numeric)
                                    {
                                        //判断是否日期类型
                                        if (DateUtil.IsCellDateFormatted(cell))
                                        {
                                            dataRow[j] = row.GetCell(j).DateCellValue;
                                        }
                                        else
                                        {
                                            dataRow[j] = row.GetCell(j).ToString().Trim();
                                        }
                                    }
                                    else
                                    {
                                        dataRow[j] = row.GetCell(j).ToString().Trim();
                                    }
                                }
                            }
                            data.Rows.Add(dataRow);
                        }
                    }

                    alldata.Tables.Add(data);
                }

                return alldata;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 导出Excel文件
        /// </summary>
        /// <typeparam name="T">实体的类型</typeparam>
        /// <param name="entitys">实体list列表</param>
        /// <param name="colName">导出的列名</param>
        /// <returns></returns>
        public static byte[] OutputExcel<T>(List<T> entitys, string[] colName)
        {
            IWorkbook workbook = new XSSFWorkbook();
            ISheet sheet = workbook.CreateSheet("sheet");
            IRow Title = null;
            IRow rows = null;
            ICellStyle style = workbook.CreateCellStyle();
            style.Alignment = HorizontalAlignment.Center;//设置水平居中
            if (entitys.Count <= 0)
            {
                Title = sheet.CreateRow(0);
                Title.CreateCell(0).SetCellValue("序号");
                sheet.SetColumnWidth(0, 6 * 256);
                ICell cellTitle0 = sheet.GetRow(0).GetCell(0);
                cellTitle0.CellStyle = style;
                for (int k = 1; k < colName.Length + 1; k++)
                {
                    var width = (colName[k - 1].Length * 2 + 2) * 256;
                    if (width > 255 * 256)
                    {
                        width = 60000;
                    }
                    sheet.SetColumnWidth(k, width);//设置列宽
                    Title.CreateCell(k).SetCellValue(colName[k - 1]);
                    ICell cellTitle = sheet.GetRow(0).GetCell(k);
                    cellTitle.CellStyle = style;
                }

            }
            else
            {
                Type entityType = entitys[0].GetType();
                PropertyInfo[] entityProperties = entityType.GetProperties();

                for (int i = 0; i <= entitys.Count; i++)
                {
                    if (i == 0)
                    {
                        Title = sheet.CreateRow(0);
                        Title.CreateCell(0).SetCellValue("序号");
                        sheet.SetColumnWidth(0, 6 * 256);
                        ICell cellTitle0 = sheet.GetRow(0).GetCell(0);
                        cellTitle0.CellStyle = style;
                        for (int k = 1; k < colName.Length + 1; k++)
                        {
                            var width = (colName[k - 1].Length * 2 + 2) * 256;
                            if (width > 255 * 256)
                            {
                                width = 60000;
                            }
                            sheet.SetColumnWidth(k, width);//设置列宽
                            Title.CreateCell(k).SetCellValue(colName[k - 1]);
                            ICell cellTitle = sheet.GetRow(0).GetCell(k);
                            cellTitle.CellStyle = style;
                        }

                        continue;
                    }
                    else
                    {
                        rows = sheet.CreateRow(i);

                        object entity = entitys[i - 1];
                        for (int j = 1; j <= entityProperties.Length; j++)
                        {
                            object[] entityValues = new object[entityProperties.Length];
                            entityValues[j - 1] = entityProperties[j - 1].GetValue(entity);
                            rows.CreateCell(0).SetCellValue(i);
                            ICell cellTitle0 = rows.GetCell(0);
                            cellTitle0.CellStyle = style;
                            string cellValue = entityValues[j - 1] == null ? "" : entityValues[j - 1].ToString();
                            var width = (cellValue.Length * 2 + 2) * 256;
                            if (width > 255 * 256)
                            {
                                width = 60000;
                            }
                            if ((cellValue.Length * 2 + 2) * 256 > sheet.GetColumnWidth(j))
                            {
                                sheet.SetColumnWidth(j, width);//设置列宽
                            }
                            rows.CreateCell(j).SetCellValue(cellValue);
                            ICell cell = rows.GetCell(j);
                            cell.CellStyle = style;
                        }
                    }
                }
            }

            byte[] buffer = new byte[1024 * 2];
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                buffer = ms.ToArray();
                ms.Close();
            }

            return buffer;
        }


        /// <summary>
        /// 导出Excel文件
        /// </summary>
        /// <typeparam name="T">实体的类型</typeparam>
        /// <param name="entitys">实体list列表</param>
        /// <param name="colName">导出的列名</param>
        /// <param name="halign">各列水平对齐方式</param>
        /// <returns></returns>
        public static byte[] OutputExcel<T>(List<T> entitys, Dictionary<string, string> dicColName, HorizontalAlignment halign = HorizontalAlignment.Center)
        {
            var colName = dicColName.Keys.ToArray();
            IWorkbook workbook = new XSSFWorkbook();
            ISheet sheet = workbook.CreateSheet("sheet");
            IRow Title = null;
            IRow row = null;
            ICellStyle headstyle = workbook.CreateCellStyle();
            headstyle.Alignment = HorizontalAlignment.Center;
            ICellStyle rowNostyle = workbook.CreateCellStyle();
            rowNostyle.Alignment = HorizontalAlignment.Center;
            ICellStyle rowstyle = workbook.CreateCellStyle();
            rowstyle.Alignment = halign;//设置水平居中

            // 创建首列表头
            if (colName.Length > 0)
            {
                Title = sheet.CreateRow(0);
                Title.CreateCell(0).SetCellValue("序号");
                sheet.SetColumnWidth(0, 6 * 256);
                ICell cellTitle0 = sheet.GetRow(0).GetCell(0);
                cellTitle0.CellStyle = rowNostyle;

                for (int k = 1; k < colName.Length + 1; k++)
                {
                    var width = (colName[k - 1].Length * 2 + 2) * 256;
                    if (width > 255 * 256)
                    {
                        width = 60000;
                    }
                    sheet.SetColumnWidth(k, width);//设置列宽
                    Title.CreateCell(k).SetCellValue(dicColName[colName[k - 1]]);
                    ICell cellTitle = sheet.GetRow(0).GetCell(k);
                    cellTitle.CellStyle = headstyle;
                }
            }

            if (entitys.Count > 0)
            {
                Type entityType = entitys.First().GetType();
                PropertyInfo[] entityProperties = entityType.GetProperties();
                int rowlen = entitys.Count;
                int collen = entityProperties.Length;
                for (int i = 0; i < rowlen; i++)
                {
                    row = sheet.CreateRow(i + 1);
                    T entity = entitys[i];
                    row.CreateCell(0).SetCellValue(i + 1);
                    ICell cellTitle0 = row.GetCell(0);
                    cellTitle0.CellStyle = rowNostyle;

                    for (int k = 1; k < colName.Length + 1; k++)
                    {
                        foreach (var property in entityProperties)
                        {
                            if (colName[k - 1] == property.Name)
                            {
                                object cellValue = property.GetValue(entity);
                                string strCellValue = cellValue == null ? "" : cellValue.ToString();
                                var width = (strCellValue.Length + 2) * 256;
                                if (width > 255 * 256)
                                {
                                    width = 60000;
                                }
                                if (width > sheet.GetColumnWidth(k))
                                {
                                    sheet.SetColumnWidth(k, width);//设置列宽
                                }
                                row.CreateCell(k).SetCellValue(strCellValue);
                                ICell cell = row.GetCell(k);
                                cell.CellStyle = rowstyle;
                            }
                        }
                    }
                }
            }

            byte[] buffer = null;
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                buffer = ms.ToArray();
                ms.Close();
            }

            return buffer;
        }

        private static bool FillSheetWithDatatable(IWorkbook workbook, DataTable dt, Dictionary<string, string> dicColName = null, HorizontalAlignment halign = HorizontalAlignment.Center)
        {
            bool bRet = false;
            try
            {
                ICellStyle headstyle = workbook.CreateCellStyle();
                headstyle.Alignment = HorizontalAlignment.Center;
                ICellStyle rowNostyle = workbook.CreateCellStyle();
                rowNostyle.Alignment = HorizontalAlignment.Center;
                ICellStyle rowstyle = workbook.CreateCellStyle();
                rowstyle.Alignment = halign;//设置水平居中

                ISheet sheet = workbook.CreateSheet(dt.TableName);
                IRow Title = null;
                IRow row = null;
                // 创建首列表头
                if (dt.Columns.Count > 0)
                {
                    Title = sheet.CreateRow(0);
                    Title.CreateCell(0).SetCellValue("序号");
                    sheet.SetColumnWidth(0, 6 * 256);
                    ICell cellTitle0 = sheet.GetRow(0).GetCell(0);
                    cellTitle0.CellStyle = rowNostyle;

                    for (int k = 1; k < dt.Columns.Count + 1; k++)
                    {
                        var dtColName = dt.Columns[k - 1].ColumnName;
                        if (dicColName != null && dicColName.ContainsKey(dtColName))
                        {
                            dtColName = dicColName[dtColName];
                        }
                        var width = (dtColName.Length * 2 + 2) * 256;
                        if (width > 255 * 256)
                        {
                            width = 60000;
                        }
                        sheet.SetColumnWidth(k, width);//设置列宽
                        Title.CreateCell(k).SetCellValue(dtColName);
                        ICell cellTitle = sheet.GetRow(0).GetCell(k);
                        cellTitle.CellStyle = headstyle;
                    }
                }

                if (dt.Rows.Count > 0)
                {
                    int rowlen = dt.Rows.Count;
                    int collen = dt.Columns.Count;
                    for (int i = 0; i < rowlen; i++)
                    {
                        row = sheet.CreateRow(i + 1);

                        row.CreateCell(0).SetCellValue(i + 1);
                        ICell cellTitle0 = row.GetCell(0);
                        cellTitle0.CellStyle = rowNostyle;

                        for (int j = 0; j < collen; j++)
                        {
                            object cellValue = dt.Rows[i][j];
                            string strCellValue = cellValue == null ? "" : cellValue.ToString();
                            var width = (strCellValue.Length + 2) * 256;
                            if (width > 255 * 256)
                            {
                                width = 60000;
                            }
                            if (width > sheet.GetColumnWidth(j + 1))
                            {
                                sheet.SetColumnWidth(j + 1, width);//设置列宽
                            }
                            row.CreateCell(j + 1).SetCellValue(strCellValue);
                            ICell cell = row.GetCell(j + 1);
                            cell.CellStyle = rowstyle;
                        }
                    }
                }
                bRet = true;
            }
            catch (Exception ex)
            {
                bRet = false;
            }
            return bRet;
        }

        /// <summary>
        /// 将dataset转换为excel字节数组
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="dicPropColName">
        /// 数据表名-列名属性字典的字典
        /// Key：数据表名
        /// Value：数据列名-Sheet页中显示列名字典
        /// Value>Key：数据列名
        /// Value>Value：在excel中显示的列名
        /// </param>
        /// <param name="halign">对齐方式枚举</param>
        /// <returns></returns>
        public static byte[] OutPutExcel(DataSet ds, Dictionary<string, Dictionary<string, string>> dicPropColName = null, HorizontalAlignment halign = HorizontalAlignment.Center)
        {

            IWorkbook workbook = new XSSFWorkbook();
            foreach (DataTable dt in ds.Tables)
            {
                Dictionary<string, string> dicColName = null;
                if (dicPropColName != null)
                {
                    dicColName = dicPropColName.ContainsKey(dt.TableName) ? dicPropColName[dt.TableName] : null;
                }

                if (!FillSheetWithDatatable(workbook, dt, dicColName, halign))
                {
                    return null;
                }
            }

            byte[] buffer = null;
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                buffer = ms.ToArray();
                ms.Close();
            }

            return buffer;
        }
        public static byte[] OutPutExcel(List<ExportSheetPram> sheetList, HorizontalAlignment halign = HorizontalAlignment.Center)
        {

            IWorkbook workbook = new XSSFWorkbook();
            foreach (var item in sheetList)
            {
                if (!FillSheetWithDatatable(workbook, item, halign))
                {
                    return null;
                }
            }

            byte[] buffer = null;
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                buffer = ms.ToArray();
                ms.Close();
            }

            return buffer;
        }
        private static bool FillSheetWithDatatable(IWorkbook workbook, ExportSheetPram sheetPram, HorizontalAlignment halign = HorizontalAlignment.Center)
        {
            bool bRet = false;
            try
            {
                ICellStyle headstyle = workbook.CreateCellStyle();
                headstyle.Alignment = HorizontalAlignment.Center;
                ICellStyle rowNostyle = workbook.CreateCellStyle();
                rowNostyle.Alignment = HorizontalAlignment.Center;
                ICellStyle rowstyle = workbook.CreateCellStyle();
                rowstyle.Alignment = halign;//设置水平居中

                ISheet sheet = workbook.CreateSheet(sheetPram.dt.TableName);
                IRow Title = null;
                IRow row = null;
                //处理描述信息
                if (!string.IsNullOrWhiteSpace(sheetPram.msg) && sheetPram.row > 0)
                {
                    IRow msgRow = sheet.CreateRow(0);
                    msgRow.CreateCell(0).SetCellValue(sheetPram.msg);
                    ICell msgCell = sheet.GetRow(0).GetCell(0);
                    msgCell.CellStyle = rowNostyle;
                    CellRangeAddress region = new CellRangeAddress(0, sheetPram.row - 1, 0, sheetPram.column - 1);
                    sheet.AddMergedRegion(region);

                }
                // 创建首列表头
                if (sheetPram.dt.Columns.Count > 0)
                {
                    Title = sheet.CreateRow(sheetPram.row);
                    Title.CreateCell(0).SetCellValue("序号");
                    sheet.SetColumnWidth(0, 6 * 256);
                    ICell cellTitle0 = sheet.GetRow(sheetPram.row).GetCell(0);
                    cellTitle0.CellStyle = rowNostyle;

                    for (int k = 1; k < sheetPram.dt.Columns.Count + 1; k++)
                    {
                        var dtColName = sheetPram.dt.Columns[k - 1].ColumnName;
                        if (sheetPram.dicColName != null && sheetPram.dicColName.ContainsKey(dtColName))
                        {
                            dtColName = sheetPram.dicColName[dtColName];
                        }
                        var width = (dtColName.Length * 2 + 2) * 256;
                        if (width > 255 * 256)
                        {
                            width = 60000;
                        }
                        sheet.SetColumnWidth(k, width);//设置列宽
                        Title.CreateCell(k).SetCellValue(dtColName);
                        ICell cellTitle = sheet.GetRow(sheetPram.row).GetCell(k);
                        cellTitle.CellStyle = headstyle;
                    }
                }

                if (sheetPram.dt.Rows.Count > 0)
                {
                    int rowlen = sheetPram.dt.Rows.Count;
                    int collen = sheetPram.dt.Columns.Count;
                    for (int i = sheetPram.row; i < rowlen + sheetPram.row; i++)
                    {
                        row = sheet.CreateRow(i + 1);

                        row.CreateCell(0).SetCellValue(i + 1 - sheetPram.row);
                        ICell cellTitle0 = row.GetCell(0);
                        cellTitle0.CellStyle = rowNostyle;

                        for (int j = 0; j < collen; j++)
                        {
                            object cellValue = sheetPram.dt.Rows[i - sheetPram.row][j];
                            string strCellValue = cellValue == null ? "" : cellValue.ToString();
                            var width = (strCellValue.Length + 2) * 256;
                            if (width > 255 * 256)
                            {
                                width = 60000;
                            }
                            if (width > sheet.GetColumnWidth(j + 1))
                            {
                                sheet.SetColumnWidth(j + 1, width);//设置列宽
                            }
                            row.CreateCell(j + 1).SetCellValue(strCellValue);
                            ICell cell = row.GetCell(j + 1);
                            cell.CellStyle = rowstyle;
                        }
                    }
                }
                bRet = true;
            }
            catch (Exception ex)
            {
                bRet = false;
            }
            return bRet;
        }


        /// <summary>
        /// 将List转换为DataTable
        /// </summary>
        /// <param name="list">请求数据</param>
        /// <returns></returns>
        public static DataTable ListToDataTable<T>(List<T> list, string tableName)
        {
            if (list != null)
            {
                //创建一个名为"tableName"的空表
                DataTable dt = new DataTable(tableName ?? "");

                foreach (var item in typeof(T).GetProperties())
                {
                    dt.Columns.Add(item.Name);
                }

                //循环存储
                foreach (var item in list)
                {
                    //新加行
                    DataRow value = dt.NewRow();
                    //根据DataTable中的值，进行对应的赋值
                    foreach (DataColumn dtColumn in dt.Columns)
                    {
                        int i = dt.Columns.IndexOf(dtColumn);

                        var objValue = item.GetType().GetProperty(dtColumn.ColumnName).GetValue(item);
                        if (objValue != null)
                        {
                            value[i] = objValue.ToString();
                        }
                        else
                        {
                            value[i] = string.Empty;
                        }

                        ////基元元素，直接复制，对象类型等，进行序列化
                        //if (value.GetType().IsPrimitive)
                        //{
                        // value[i] = item.GetType().GetProperty(dtColumn.ColumnName).GetValue(item);
                        //}
                        //else
                        //{
                        // // 序列化为json
                        // value[i] = JsonConvert.SerializeObject(item.GetType().GetProperty(dtColumn.ColumnName).GetValue(item));
                        //}
                    }
                    dt.Rows.Add(value);
                }
                return dt;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 将List转换为DataTable
        /// </summary>
        /// <param name="list">请求数据</param>
        /// <returns></returns>
        public static DataTable ListToDataTable<T>(List<T> list, string[] persistCol, string tableName)
        {
            if (list != null)
            {
                //创建一个名为"tableName"的空表
                DataTable dt = new DataTable(tableName ?? "");


                foreach (var item in typeof(T).GetProperties())
                {
                    if (persistCol.Contains(item.Name))
                    {
                        dt.Columns.Add(item.Name);
                    }
                }

                //循环存储
                foreach (var item in list)
                {
                    //新加行
                    DataRow value = dt.NewRow();
                    //根据DataTable中的值，进行对应的赋值
                    foreach (DataColumn dtColumn in dt.Columns)
                    {
                        int i = dt.Columns.IndexOf(dtColumn);

                        var objValue = item.GetType().GetProperty(dtColumn.ColumnName).GetValue(item);
                        if (objValue != null)
                        {
                            value[i] = objValue.ToString();
                        }
                        else
                        {
                            value[i] = string.Empty;
                        }

                        ////基元元素，直接复制，对象类型等，进行序列化
                        //if (value.GetType().IsPrimitive)
                        //{
                        // value[i] = item.GetType().GetProperty(dtColumn.ColumnName).GetValue(item);
                        //}
                        //else
                        //{
                        // // 序列化为json
                        // value[i] = JsonConvert.SerializeObject(item.GetType().GetProperty(dtColumn.ColumnName).GetValue(item));
                        //}
                    }
                    dt.Rows.Add(value);
                }
                return dt;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 将JObject转换为DataTable
        /// </summary>
        /// <param name="list">请求数据</param>
        /// <param name="persistCol"></param>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static DataTable JsonToDataTable(List<JObject> list, string[] persistCol, string tableName)
        {
            if (list != null)
            {
                //创建一个名为"tableName"的空表
                DataTable dt = new DataTable(tableName ?? "");

                foreach (var item in persistCol)
                {
                    dt.Columns.Add(item);
                }

                //循环存储
                foreach (var item in list)
                {
                    //新加行
                    DataRow value = dt.NewRow();
                    //根据DataTable中的值，进行对应的赋值
                    foreach (DataColumn dtColumn in dt.Columns)
                    {
                        int i = dt.Columns.IndexOf(dtColumn);

                        var objValue = item.GetValue(dtColumn.ColumnName);
                        if (objValue != null)
                        {
                            value[i] = objValue.ToString();
                        }
                        else
                        {
                            value[i] = string.Empty;
                        }

                        ////基元元素，直接复制，对象类型等，进行序列化
                        //if (value.GetType().IsPrimitive)
                        //{
                        // value[i] = item.GetType().GetProperty(dtColumn.ColumnName).GetValue(item);
                        //}
                        //else
                        //{
                        // // 序列化为json
                        // value[i] = JsonConvert.SerializeObject(item.GetType().GetProperty(dtColumn.ColumnName).GetValue(item));
                        //}
                    }
                    dt.Rows.Add(value);
                }
                return dt;
            }
            else
            {
                return null;
            }
        }


        /// <summary>
        /// 利用反射和泛型
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static List<T> DataTableToList<T>(DataTable dt) where T : new()
        {
            // 定义集合
            List<T> ts = new List<T>();
            // 获得此模型的类型
            Type type = typeof(T);
            //定义一个临时变量
            string tempName = string.Empty;
            //遍历DataTable中所有的数据行
            foreach (DataRow dr in dt.Rows)
            {
                T t = new T();
                // 获得此模型的公共属性
                PropertyInfo[] propertys = t.GetType().GetProperties();
                //遍历该对象的所有属性
                foreach (PropertyInfo pi in propertys)
                {
                    tempName = pi.Name;//将属性名称赋值给临时变量
                                       //检查DataTable是否包含此列（列名==对象的属性名）  
                    if (dt.Columns.Contains(tempName))
                    {
                        // 判断此属性是否有Setter
                        if (!pi.CanWrite) continue;//该属性不可写，直接跳出
                                                   //取值
                        object value = dr[tempName];
                        //如果非空，则赋给对象的属性
                        if (value != DBNull.Value)
                            pi.SetValue(t, value, null);
                    }
                }
                //对象添加到泛型集合中
                ts.Add(t);
            }
            return ts;
        }


        public static byte[] TemplateOutPutExcel(IEnumerable<FillSheetDataSource> dataSource, string templateFileName, ref string strMsg)
        {
            templateFileName = Path.Combine(Directory.GetCurrentDirectory(), templateFileName);
            if (!File.Exists(templateFileName))
            {
                return null;
            }
            //根据指定路径读取文件
            FileStream fs = new FileStream(templateFileName, FileMode.Open, FileAccess.Read);
            //根据文件流创建excel数据结构
            IWorkbook workbook = WorkbookFactory.Create(fs);
            foreach (FillSheetDataSource ds in dataSource)
            {
                if (!string.IsNullOrWhiteSpace(ds.SheetName))
                {
                    if (!FillTemplateExcel(workbook, ds, ref strMsg))
                    {
                        return null;
                    }
                }
            }

            byte[] buffer = null;
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                buffer = ms.ToArray();
                ms.Close();
            }
            return buffer;
        }

        private static bool FillTemplateExcel(IWorkbook workbook, FillSheetDataSource ds, ref string strMsg)
        {
            bool bRet = true;
            var sheet = workbook.GetSheet(ds.SheetName);
            if (sheet != null)
            {
                #region 解析 样式相关配置
                // 每个sheet页的样式解析一次
                // 各种模式的各属性样式
                Dictionary<string, Dictionary<string, ICellStyle>> styleDic = new Dictionary<string, Dictionary<string, ICellStyle>>();
                foreach (var dsitem in ds.DataSource)
                {
                    if (dsitem.Key.IterationRegionStyleConfig == null)
                    {
                        continue;
                    }
                    foreach (string k in dsitem.Key.IterationRegionStyleConfig.Keys)
                    {
                        var dicItem = new Dictionary<string, ICellStyle>();
                        foreach (var item in dsitem.Key.IterationRegionStyleConfig[k])
                        {
                            if (item.Row > -1 && item.Column > -1)
                            {
                                if (!string.IsNullOrWhiteSpace(item.PropName))
                                {
                                    if (dicItem.ContainsKey(item.PropName))
                                    {
                                        bRet = false;
                                        strMsg = $"同种模式的样式配置 行：{item.Row}，列{item.Column},属性{item.PropName} 重复";
                                        return bRet;
                                    }
                                    else
                                    {
                                        dicItem.Add(item.PropName, sheet.GetRow(item.Row).GetCell(item.Column).CellStyle);
                                    }
                                }
                                else
                                {
                                    bRet = false;
                                    strMsg = $"样式配置行：{item.Row}，列{item.Column},属性{item.PropName}不正确";
                                    return bRet;
                                }
                            }
                            else
                            {
                                bRet = false;
                                strMsg = $"样式配置行：{item.Row}，列{item.Column}不正确";
                                return bRet;
                            }
                        }

                        styleDic.Add(k, dicItem);
                    }
                }
                #endregion 解析 样式相关配置

                foreach (var dsitem in ds.DataSource)
                {
                    if (dsitem.Value.Rows.Count > 0)
                    {

                        var baseConfig = dsitem.Key;
                        //ICell cellTitle0 = sheet.GetRow(0).GetCell(0);
                        //if (cellTitle0 != null)
                        //{
                        //var baseConfig = JsonConvert.DeserializeObject<FillSheetBaseConfig>(cellTitle0.StringCellValue);
                        if (baseConfig != null)
                        {
                            switch (baseConfig.FillSheetModal)
                            {
                                case FillSheetModal.loaction:
                                    bRet &= FillTemplateSheetLocationModal(sheet, baseConfig, dsitem.Value, ref strMsg);
                                    break;
                                case FillSheetModal.iterationList:
                                    bRet &= FillTemplateSheetIterationModal(sheet, baseConfig, dsitem.Value, styleDic, ref strMsg);
                                    break;
                                default:
                                    // 配置不正确
                                    bRet = false;
                                    strMsg = $"模板sheet页{ds.SheetName}配置FillSheetModal不正确";
                                    break;
                            }
                        }
                        else
                        {
                            // 配置不正确
                            bRet = false;
                            strMsg = $"模板sheet页{ds.SheetName}配置不正确";
                        }
                        //}
                        //else
                        //{
                        // // 读不到配置
                        // bRet = false;
                        // strMsg = $"读不到sheet页{ds.SheetName}配置";
                        //}

                    }
                    else
                    {
                        // 读不到sheet
                        bRet = false;
                        strMsg = $"{ds.SheetName}的数据源不能为空";
                    }
                }
            }
            else
            {
                // 读不到sheet
                bRet = false;
                strMsg = $"读不到模板的sheet页{ds.SheetName}";
            }
            return bRet;
        }

        /// <summary>
        /// 按迭代填充
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="baseConfig"></param>
        /// <param name="dataSource"></param>
        /// <param name="strMsg"></param>
        /// <returns></returns>
        private static bool FillTemplateSheetIterationModal(ISheet sheet, FillSheetBaseConfig baseConfig, DataTable dataSource, Dictionary<string, Dictionary<string, ICellStyle>> styleDic, ref string strMsg)
        {
            bool bRet = true;
            try
            {
                var dicPropLocation = baseConfig.ValueBeginRegion.ToDictionary(o => o.PropName, o => o);
                switch (baseConfig.Vector)
                {
                    case IterationVector.column:
                        foreach (DataRow item in dataSource.Rows)
                        {
                            var objStyleModalValue = item[baseConfig.StylePropertyName];
                            string strStyleModalValue = objStyleModalValue == null ? string.Empty : objStyleModalValue.ToString();
                            Dictionary<string, ICellStyle> modelStyle = null;
                            if (!string.IsNullOrWhiteSpace(strStyleModalValue) && styleDic.ContainsKey(strStyleModalValue))
                            {
                                modelStyle = styleDic[strStyleModalValue];
                            }
                            foreach (var loaction in dicPropLocation)
                            {
                                var cellValue = item[loaction.Key];
                                string strCellValue = cellValue == null ? "" : cellValue.ToString();
                                var row = sheet.GetRow(loaction.Value.Row);
                                if (row == null)
                                {
                                    row = sheet.CreateRow(loaction.Value.Row);
                                }
                                var cell = row.GetCell(loaction.Value.Column);
                                if (cell == null)
                                {
                                    cell = row.CreateCell(loaction.Value.Column);
                                }
                                if (cell == null)
                                {
                                    cell = row.CreateCell(loaction.Value.Column);
                                }
                                cell.SetCellValue(strCellValue);
                                if (modelStyle != null && modelStyle.ContainsKey(loaction.Key))
                                {
                                    cell.CellStyle = modelStyle[loaction.Key];
                                }
                                loaction.Value.Column++;
                            }
                        }
                        break;
                    case IterationVector.row:
                        foreach (DataRow item in dataSource.Rows)
                        {
                            var objStyleModalValue = item[baseConfig.StylePropertyName];
                            string strStyleModalValue = objStyleModalValue == null ? string.Empty : objStyleModalValue.ToString();
                            Dictionary<string, ICellStyle> modelStyle = null;
                            if (!string.IsNullOrWhiteSpace(strStyleModalValue) && styleDic.ContainsKey(strStyleModalValue))
                            {
                                modelStyle = styleDic[strStyleModalValue];
                            }
                            foreach (var loaction in dicPropLocation)
                            {
                                var cellValue = item[loaction.Key];
                                string strCellValue = cellValue == null ? "" : cellValue.ToString();
                                var row = sheet.GetRow(loaction.Value.Row);
                                if (row == null)
                                {
                                    row = sheet.CreateRow(loaction.Value.Row);
                                }
                                var cell = row.GetCell(loaction.Value.Column);
                                if (cell == null)
                                {
                                    cell = row.CreateCell(loaction.Value.Column);
                                }
                                cell.SetCellValue(strCellValue);
                                if (modelStyle != null && modelStyle.ContainsKey(loaction.Key))
                                {
                                    cell.CellStyle = modelStyle[loaction.Key];
                                }
                                loaction.Value.Row++;
                            }
                        }
                        break;
                    default:
                        bRet = false;
                        strMsg = $"模板sheet页{sheet.SheetName}配置Vector不正确";
                        break;
                }
            }
            catch (Exception ex)
            {
                bRet = false;
                strMsg = ex.Message;
            }
            return bRet;
        }

        /// <summary>
        /// 按定位方式给sheet页赋值
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="baseConfig"></param>
        /// <param name="dataSource"></param>
        /// <param name="strMsg"></param>
        /// <returns></returns>
        private static bool FillTemplateSheetLocationModal(ISheet sheet, FillSheetBaseConfig baseConfig, DataTable dataSource, ref string strMsg)
        {
            bool bRet = true;
            var realData = dataSource.Rows[0];
            if (realData != null)
            {
                try
                {
                    foreach (var location in baseConfig.CellLocations)
                    {
                        object cellValue = realData[location.PropName];
                        string strCellValue = cellValue == null ? "" : cellValue.ToString();
                        sheet.GetRow(location.Row).GetCell(location.Column).SetCellValue(strCellValue);
                    }
                }
                catch (Exception ex)
                {
                    bRet = false;
                    // 读不到属性
                    strMsg = $"读取数据时发生错误：{ex.Message}";
                }
            }
            else
            {
                bRet = false;
                // 读不到sheet
                strMsg = $"{sheet.SheetName}的数据源不能为空";
            }

            return bRet;
        }
    }

    /// <summary>
    /// 填充数据源
    /// </summary>
    public class FillSheetDataSource
    {
        /// <summary>
        /// sheet页名称
        /// </summary>
        public string SheetName { get; set; }

        public Dictionary<FillSheetBaseConfig, DataTable> DataSource { get; set; }
        // public IEnumerable<T> Data { get; set; }

        //public FillSheetBaseConfig BaseConfig { get; set; }
    }

    public class FillSheetBaseConfig
    {
        /// <summary>
        /// 填充sheet页所采用的模式
        /// </summary>
        public FillSheetModal FillSheetModal { get; set; }
        /// <summary>
        /// 位置填充时各位置需要填入的属性
        /// </summary>
        public IEnumerable<CellLocation> CellLocations { get; set; }
        /// <summary>
        /// 迭代填充时的方向
        /// </summary>
        public IterationVector Vector { get; set; }
        /// <summary>
        /// 值填充开始位置
        /// </summary>
        public IEnumerable<CellLocation> ValueBeginRegion { get; set; }

        /// <summary>
        /// 数据源中，样式模式的属性名称
        /// </summary>
        public string StylePropertyName { get; set; }
        /// <summary>
        /// 填充样式来源。 key：样式模式的属性的值；value：各属性样式对应的位置,因NPOI；
        /// </summary>
        public Dictionary<string, IEnumerable<CellLocation>> IterationRegionStyleConfig { get; set; }

        public FitColWidth FitColWidth { get; set; }
    }

    public enum FillSheetModal
    {
        /// <summary>
        /// 使用位置信息填充相应属性
        /// </summary>
        loaction = 1,
        /// <summary>
        /// 使用对象列表填充
        /// </summary>
        iterationList = 2
    }

    /// <summary>
    /// 自适应列宽
    /// </summary>
    public enum FitColWidth
    {
        /// <summary>
        /// 自适应
        /// </summary>
        fit = 1,
        /// <summary>
        /// 继承模板
        /// </summary>
        inherit = 2

    }
    /// <summary>
    /// 迭代方向
    /// </summary>
    public enum IterationVector
    {
        /// <summary>
        /// 纵向迭代
        /// </summary>
        row = 1,
        /// <summary>
        /// 横向迭代
        /// </summary>
        column = 2

    }

    public class CellLocation
    {
        public int Row { get; set; }

        public int Column { get; set; }

        public string PropName { get; set; }
    }
    /// <summary>
    /// 导出sheet 的参数类
    /// </summary>
    public class ExportSheetPram
    {
        public ExportSheetPram()
        {
            dt = new DataTable();
            dicColName = new Dictionary<string, string>();
            msg = null;
            row = 0;
            column = 0;
        }
        /// <summary>
        /// 导出数据
        /// </summary>
        public DataTable dt { get; set; }
        /// <summary>
        /// 描述信息
        /// </summary>
        public Dictionary<string, string> dicColName { get; set; }
        /// <summary>
        /// 描述信息
        /// </summary>
        public string msg { get; set; }
        /// <summary>
        /// 描述信息所占行数
        /// </summary>
        public int row { get; set; }
        /// <summary>
        /// 描述信息所占列数
        /// </summary>
        public int column { get; set; }
    }
}
