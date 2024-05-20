
using CashFlow.Application.UseCases.Expenses.Reports.Pdf.Colors;
using CashFlow.Application.UseCases.Expenses.Reports.Pdf.Fonts;
using CashFlow.Domain.Enums;
using CashFlow.Domain.Extensions;
using CashFlow.Domain.Reports.GenerationMessage;
using CashFlow.Domain.Repositories.Expenses;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using PdfSharp.Fonts;
using System.Reflection;

namespace CashFlow.Application.UseCases.Expenses.Reports.Pdf;

public class GenerateExpenseReportPdfUseCase : IGenerateExpenseReportPdfUseCase
{
    private const string CURRENCY_SYMBOL = "R$";
    private const int HEIGHT_LINE_SIZE = 25;

    private readonly IExpensesReadOnlyRepositories _respository;

    public GenerateExpenseReportPdfUseCase(IExpensesReadOnlyRepositories respository)
    {
        _respository = respository;

        GlobalFontSettings.FontResolver = new ExpenseReportFontResolver();
    }
    public async Task<byte[]> Execute(DateTime month)
    {
        var expenses = await _respository.GetExpensesByMonth(month);

        if (expenses.Count == 0)
        {
            return [];
        }

        var document = CreateDocument(month);
        var page = CreatePage(document);

        CreateHeaderWithUserInfoAndIconImage(page);

        var paragraph = page.AddParagraph();
        var totalExpense = expenses.Sum(expense => expense.Amount);
        CreateTotalSpentSection(paragraph, month, totalExpense);

        foreach(var expense in expenses)
        {
            var table = CreateExpenseTable(page);

            AddHeaderExpenseTable(table, expense.Title);
            AddExpenseInfo(month, expense.PaymentType, expense.Amount, table);
            if (expense.Description is not null)
            {
                AddDescription(expense.Description, table);
            }
            AddWhiteSpace(table);
        }

        return RenderDocument(document);
    }

    private Document CreateDocument(DateTime month)
    {
        var document = new Document();

        document.Info.Title = $"{ResourceReportGenerationMessage.EXPENSES_FOR} {month:Y}";
        document.Info.Author = "Cash Flow";

        var style = document.Styles["Normal"];
        style!.Font.Name = FontHelper.DEFAULT;

        return document;
    }

    private Section CreatePage(Document document)
    {
        var section = document.AddSection();
        section.PageSetup = document.DefaultPageSetup.Clone();

        section.PageSetup.PageFormat = PageFormat.A4;

        section.PageSetup.BottomMargin = 80;
        section.PageSetup.TopMargin = 80;
        section.PageSetup.RightMargin = 40;
        section.PageSetup.LeftMargin = 40;

        return section;
    }

    private void CreateHeaderWithUserInfoAndIconImage(Section page)
    {
        var table = page.AddTable();

        table.AddColumn();
        table.AddColumn("300");

        var row = table.AddRow();

        var assembly = Assembly.GetExecutingAssembly();
        var directoryName = Path.GetDirectoryName(assembly.Location);
        var pathFile = Path.Combine(directoryName!, "Logo", "ProfilePhoto.png");

        row.Cells[0].AddImage(pathFile);

        row.Cells[1].AddParagraph("Hey, User");
        row.Cells[1].Format.Font = new Font { Name = FontHelper.RALEWAY_BLACK, Size = 16 };
        row.Cells[1].VerticalAlignment = VerticalAlignment.Center;
    }

    private void CreateTotalSpentSection(Paragraph paragraph, DateTime month, decimal totalExpense)
    {
        paragraph.Format.SpaceAfter = "40";
        paragraph.Format.SpaceBefore = "40";

        var title = string.Format(ResourceReportGenerationMessage.TOTAL_SPENT_IN, month.ToString("Y"));

        paragraph.AddFormattedText(title, new Font { Name = FontHelper.RALEWAY_REGULAR, Size = 15 });

        paragraph.AddLineBreak();

        
        paragraph.AddFormattedText($"{totalExpense} {CURRENCY_SYMBOL}", new Font { Name = FontHelper.WORKSANS_BLACK, Size = 50 });
    }

    private Table CreateExpenseTable(Section page)
    {
        var table = page.AddTable();

        table.AddColumn("195").Format.Alignment = ParagraphAlignment.Left;
        table.AddColumn("80").Format.Alignment = ParagraphAlignment.Center;
        table.AddColumn("120").Format.Alignment = ParagraphAlignment.Center;
        table.AddColumn("120").Format.Alignment = ParagraphAlignment.Right;

        return table;
    }

    private void AddHeaderExpenseTable(Table table, string title)
    {
        var row = table.AddRow();
        row.Height = HEIGHT_LINE_SIZE;

        row.Cells[0].AddParagraph(title);
        row.Cells[0].Format.Font = new Font { Name = FontHelper.WORKSANS_BLACK, Size = 14 };
        row.Cells[0].Shading.Color = ColorsHelper.RED_LIGHT;
        row.Cells[0].VerticalAlignment = VerticalAlignment.Center;
        row.Cells[0].MergeRight = 2;
        row.Cells[0].Format.LeftIndent = 20;

        row.Cells[3].AddParagraph(ResourceReportGenerationMessage.AMOUNT);
        row.Cells[3].Format.Font = new Font { Name = FontHelper.WORKSANS_BLACK, Size = 14, Color = ColorsHelper.WHITE };
        row.Cells[3].Shading.Color = ColorsHelper.RED_DARK;
        row.Cells[3].VerticalAlignment = VerticalAlignment.Center;
        row.Cells[3].Format.RightIndent = 10;
    }

    private void AddExpenseInfo(DateTime month, PaymentType paymentType, decimal amount, Table table)
    {
        var row = table.AddRow();
        row.Height = HEIGHT_LINE_SIZE;

        row.Cells[0].AddParagraph(month.ToString("Y"));
        SetStyleBaseForExpenseInformation(row.Cells[0]);
        row.Cells[0].Format.LeftIndent = 20;

        row.Cells[1].AddParagraph(month.ToString("t"));
        SetStyleBaseForExpenseInformation(row.Cells[1]);

        row.Cells[2].AddParagraph(paymentType.PaymentTypeToString());
        SetStyleBaseForExpenseInformation(row.Cells[2]);

        row.Cells[3].AddParagraph($"- {amount} {CURRENCY_SYMBOL}");
        row.Cells[3].Format.Font = new Font { Name = FontHelper.WORKSANS_REGULAR, Size = 14 };
        row.Cells[3].Shading.Color = ColorsHelper.WHITE;
        row.Cells[3].VerticalAlignment = VerticalAlignment.Center;
        row.Cells[3].Format.RightIndent = 10;

    }

    private void AddDescription(string description, Table table)
    {
        var row = table.AddRow();
        row.Height = HEIGHT_LINE_SIZE;

        row.Cells[0].AddParagraph(description);
        row.Cells[0].Format.Font = new Font { Name = FontHelper.WORKSANS_REGULAR, Size = 12 };
        row.Cells[0].Shading.Color = ColorsHelper.GREEN_LIGHT;
        row.Cells[0].VerticalAlignment = VerticalAlignment.Center;
        row.Cells[0].Format.LeftIndent = 20;
        row.Cells[0].MergeRight = 3;
    }

    private void SetStyleBaseForExpenseInformation(Cell cell)
    {
        cell.Format.Font = new Font { Name = FontHelper.WORKSANS_REGULAR, Size = 12 };
        cell.Shading.Color = ColorsHelper.GREEN_DARK;
        cell.VerticalAlignment = VerticalAlignment.Center;
    }

    private void AddWhiteSpace(Table table)
    {
        var row = table.AddRow();
        row.Height = 30;
        row.Borders.Visible = false;
    }

    private byte[] RenderDocument(Document document)
    {
        var render = new PdfDocumentRenderer
        {
            Document = document,
        };

        render.RenderDocument();

        using var file = new MemoryStream();
        render.PdfDocument.Save(file);

        return file.ToArray();
    }
}
