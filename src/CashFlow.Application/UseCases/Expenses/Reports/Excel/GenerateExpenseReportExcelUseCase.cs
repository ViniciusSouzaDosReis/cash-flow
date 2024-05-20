using CashFlow.Domain.Entities;
using CashFlow.Domain.Extensions;
using CashFlow.Domain.Reports.GenerationMessage;
using CashFlow.Domain.Repositories.Expenses;
using ClosedXML.Excel;

namespace CashFlow.Application.UseCases.Expenses.Reports.Excel;

public class GenerateExpenseReportExcelUseCase : IGenerateExpenseReportExcelUseCase
{
    private const string CURRENCY_SYMBOL = "R$";
    private readonly IExpensesReadOnlyRepositories _respository;

    public GenerateExpenseReportExcelUseCase(IExpensesReadOnlyRepositories respository)
    {
        _respository = respository;
    }

    public async Task<byte[]> Execute(DateTime month)
    {
        var expenses = await _respository.GetExpensesByMonth(month);

        if(expenses.Count == 0)
        {
            return [];
        }

        using var workbook = new XLWorkbook()
        {
            Author = "CashFlow"
        };

        var worksheet = workbook.Worksheets.Add(month.ToString("Y"));

        InsertHeader(worksheet);

        var raw = 2;
        foreach (Expense expense in expenses)
        {
            worksheet.Cell($"A{raw}").Value = expense.Title;
            worksheet.Cell($"B{raw}").Value = expense.Date;
            worksheet.Cell($"C{raw}").Value = expense.PaymentType.PaymentTypeToString();

            worksheet.Cell($"D{raw}").Value = expense.Amount;
            worksheet.Cell($"D{raw}").Style.NumberFormat.Format = $"-{CURRENCY_SYMBOL} #,##0.00";
            
            worksheet.Cell($"E{raw}").Value = expense.Description;

            raw++;
        }

        worksheet.Columns().AdjustToContents();

        var file = new MemoryStream();
        workbook.SaveAs(file);

        return file.ToArray();
    }

    private void InsertHeader(IXLWorksheet worksheet)
    {
        worksheet.Cell("A1").Value = ResourceReportGenerationMessage.TITLE;
        worksheet.Cell("B1").Value = ResourceReportGenerationMessage.DATE;
        worksheet.Cell("C1").Value = ResourceReportGenerationMessage.PAYMENT_TYPE;
        worksheet.Cell("D1").Value = ResourceReportGenerationMessage.AMOUNT;
        worksheet.Cell("E1").Value = ResourceReportGenerationMessage.DESCRIPTION;

        worksheet.Cells("A1:E1").Style.Font.Bold = true;

        worksheet.Cells("A1:E1").Style.Fill.BackgroundColor = XLColor.FromHtml("F5C2B6");

        worksheet.Cells("A1:C1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
        worksheet.Cell("D1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
        worksheet.Cell("E1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
    }
}
