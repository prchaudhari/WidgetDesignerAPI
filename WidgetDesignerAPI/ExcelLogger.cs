using ClosedXML.Excel;


public class ExcelLogger
{
    private readonly string excelFilePath;

    public ExcelLogger(string excelFilePath)
    {
        this.excelFilePath = excelFilePath;
    }

    public void LogToExcel(string date, string time, string logLevel, string message,string filePath)
    {
        using (var workbook = File.Exists(excelFilePath) ? new XLWorkbook(excelFilePath) : new XLWorkbook())
        {
            IXLWorksheet worksheet = workbook.Worksheets.FirstOrDefault(w => w.Name == date);

            if (worksheet == null)
            {
                // If a worksheet for the current date doesn't exist, create a new one and add headers.
                worksheet = workbook.Worksheets.Add(date);
                worksheet.Cell(1, 1).Value = "Date";
                worksheet.Cell(1, 2).Value = "Time";
                worksheet.Cell(1, 3).Value = "LogLevel";
                worksheet.Cell(1, 4).Value = "Message";
                worksheet.Cell(1, 5).Value = "FilePath";
            }

            int newRow = worksheet.LastRowUsed()?.RowNumber() + 1 ?? 2;
            worksheet.Cell(newRow, 1).Value = date;
            worksheet.Cell(newRow, 2).Value = time;
            worksheet.Cell(newRow, 3).Value = logLevel;
            worksheet.Cell(newRow, 4).Value = message;
            worksheet.Cell(newRow, 5).Value = filePath;

            workbook.SaveAs(excelFilePath);
        }
    }

}
