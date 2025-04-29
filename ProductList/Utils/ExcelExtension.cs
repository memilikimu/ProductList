using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
namespace ProductList.Utils
{
    public static class ExcelExtension
    {
        public static void CellLevelColor(this IXLCell cell,int value, Dictionary<int, XLColor> levels)
        {
            foreach(var l in levels)
            {
                if(value <= l.Key)
                {
                    cell.Style.Fill.BackgroundColor = l.Value;
                    break;
                }
                else if(levels.Last().Key == l.Key && value > l.Key)
                {
                    cell.Style.Fill.BackgroundColor = l.Value;
                }
            }
        }
    }
}
