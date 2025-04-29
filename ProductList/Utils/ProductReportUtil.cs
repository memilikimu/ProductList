using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using ProductList.Data.Entities;
using ProductList.Models;

namespace ProductList.Utils
{
    public class ProductReportUtil
    {
        public XLWorkbook Generate(List<SalesView> data)
        {
            int amount = 0;
            int amountSold = 0;
            int total = 0;
            int totalSold = 0;

            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Laporan Penjualan Produk");

            //set standar font color
            worksheet.Range(1, 1, data.Count() + 11, 6)
               .Style.Font.SetFontColor(XLColor.FromHtml(ReportConstant.TextColor))
               .Alignment.SetVertical(XLAlignmentVerticalValues.Center);
            worksheet.Range("A1:F3").Style.Font.Bold = true;

            worksheet.Columns().Width = 11;
            worksheet.Column("A").Width = 20;
            worksheet.Column("F").Width = 16;

            worksheet.Range("A1:F1")
            .Merge()
            .SetValue(ReportConstant.Title)
            .Style.Font.SetFontSize(16)
            .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

            worksheet.Row(1).Height = 48;
            worksheet.Row(2).Height = 23;
            worksheet.Row(3).Height = 10;
            worksheet.Row(4).Height = 30;

            worksheet.Cell("E2").SetValue("Tanggal");
            worksheet.Cell("F2").SetValue(DateTime.Now.Date);
            worksheet.Range("E2:F2").Style.Border
                .SetBottomBorder(XLBorderStyleValues.Thick)
                .Border.SetBottomBorderColor(XLColor.FromHtml(ReportConstant.BorderColor));


            worksheet.Range("A4:F4").Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin)
                .Border.SetOutsideBorderColor(XLColor.FromHtml(ReportConstant.BorderColor))
                .Fill.SetBackgroundColor(XLColor.FromHtml(ReportConstant.BorderColor))
                .Font.SetFontColor(XLColor.White).Font.SetBold(true)
                .Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center)
                .Alignment.SetVertical(XLAlignmentVerticalValues.Center);

            worksheet.Cell(4, 1).Value = "Produk";
            worksheet.Cell(4, 2).Value = "Harga Jual";
            worksheet.Cell(4, 3).Value = "Harga Beli";
            worksheet.Cell(4, 4).Value = "Jumlah";
            worksheet.Cell(4, 5).Value = "Jml Terjual";
            worksheet.Cell(4, 6).Value = "Total Terjual";


            int indRow = 5;
            foreach (var item in data)
            {
                amount += item.Amount;
                amountSold += item.SellAmount;
                total += item.Total;
                totalSold += item.TotalSold;

                worksheet.Range(indRow, 1, indRow, 6).Style.Border.SetBottomBorder(XLBorderStyleValues.Thin)
                    .Border.SetBottomBorderColor(XLColor.FromHtml(ReportConstant.BorderColor));
                    
                worksheet.Row(indRow).Height = 20;

                worksheet.Cell(indRow, 1).Value = item.Product;
                worksheet.Cell(indRow, 2).SetValue(item.Price)
                    .Style.NumberFormat.SetFormat("\"Rp\" #,##0");
                worksheet.Cell(indRow, 3).SetValue(item.SellPrice)
                    .Style.NumberFormat.SetFormat("\"Rp\" #,##0");
                worksheet.Cell(indRow, 4).Value = item.Amount;
                worksheet.Cell(indRow, 5).Value = item.SellAmount;
                worksheet.Cell(indRow, 6).SetValue(item.TotalSold)
                    .Style.NumberFormat.SetFormat("\"Rp\" #,##0");
                    

                //var cell = worksheet.Cell(indRow, 2);
                //cell.CellLevelColor(item.Amount, ColorLevelOption(1));

                indRow++;
            }

            var priceRange = worksheet.Range(indRow, 5, data.Count() + indRow - 1, 5);
            priceRange.AddConditionalFormat()
                .DataBar(XLColor.FromArgb(83, 141, 213), false)
                .Minimum(XLCFContentType.Number, 0)
                .HighestValue();

            int startRow = data.Count() + 7;
            var row = worksheet.Row(startRow);
            row.Style.Font.SetFontSize(12).Font.SetBold(true);

            row.Cell(1).Value = ReportConstant.DiffAmount;
            worksheet.Range(startRow, 4, startRow, 5).Merge().SetValue(ReportConstant.DiffMargin);
            worksheet.Range(startRow, 1, startRow, 2).Style.Border.SetBottomBorder(XLBorderStyleValues.Thick)
                .Border.SetBottomBorderColor(XLColor.FromHtml(ReportConstant.BorderColor));
            worksheet.Range(startRow, 4, startRow, 6).Style.Border.SetBottomBorder(XLBorderStyleValues.Thick)
                .Border.SetBottomBorderColor(XLColor.FromHtml(ReportConstant.BorderColor));

            startRow += 1;
            row = worksheet.Row(startRow);
            row.Cell(1).Value = ReportConstant.TotAmount;
            row.Cell(2).Value = amount;
            worksheet.Range(startRow, 4, startRow, 5).Merge().SetValue(ReportConstant.TotBuy);
            row.Cell(6).SetValue(total)
                    .Style.NumberFormat.SetFormat("\"Rp\" #,##0");

            startRow += 1;
            row = worksheet.Row(startRow);
            row.Cell(1).Value = ReportConstant.TotSoldAmount;
            row.Cell(2).Value = amountSold;
            worksheet.Range(startRow, 4, startRow, 5).Merge().SetValue(ReportConstant.DiffSold);
            row.Cell(6).SetValue(totalSold)
                    .Style.NumberFormat.SetFormat("\"Rp\" #,##0");

            startRow += 2;
            worksheet.Range(startRow, 1, startRow, 7).Style.Font.SetBold(true);
            row = worksheet.Row(startRow);
            row.Cell(1).Value = ReportConstant.TotBeforeSold;
            row.Cell(2).Value = amount - amountSold;
            worksheet.Range(startRow, 4, startRow, 5).Merge().SetValue(ReportConstant.TotalDiffMargin);
            row.Cell(6).SetValue(totalSold - total)
                    .Style.NumberFormat.SetFormat("\"Rp\" #,##0");
            row.Cell(6).AddConditionalFormat().WhenEqualOrLessThan(0).Font.SetFontColor(XLColor.Red);
            row.Cell(6).AddConditionalFormat().WhenGreaterThan(0).Font.SetFontColor(XLColor.Green);
            worksheet.Range(startRow, 1, startRow, 2).Style.Border.SetTopBorder(XLBorderStyleValues.Thick)
                .Border.SetTopBorderColor(XLColor.FromHtml(ReportConstant.BorderColor));
            worksheet.Range(startRow, 4, startRow, 6).Style.Border.SetTopBorder(XLBorderStyleValues.Thick)
                .Border.SetTopBorderColor(XLColor.FromHtml(ReportConstant.BorderColor));

            return workbook;
        }


        public static Dictionary<int, XLColor> ColorLevelOption(int option)
        {
            Dictionary<int, XLColor> amountColors = new Dictionary<int, XLColor>();
            if (option == 1)
            {
                amountColors.Add(0, XLColor.Red);
                amountColors.Add(10, XLColor.Orange);
                amountColors.Add(30, XLColor.Yellow);
                amountColors.Add(60, XLColor.FromArgb(196, 215, 155));
                amountColors.Add(80, XLColor.FromArgb(141, 180, 226));
            }
            else
            {
                amountColors.Add(0, XLColor.Red);
                amountColors.Add(10, XLColor.Orange);
                amountColors.Add(30, XLColor.Yellow);
                amountColors.Add(60, XLColor.FromArgb(196, 215, 155));
                amountColors.Add(80, XLColor.FromArgb(141, 180, 226));
            }
            return amountColors;
        }
    }
}
