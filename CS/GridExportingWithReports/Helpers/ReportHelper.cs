using DevExpress.XtraReports.UI;
using System;
using System.Drawing;

namespace GridExportingWithReports.Helpers {
    public class ReportHelper {
        public static void CreateReport(XtraReport report, string[] fields) {
            PageHeaderBand pageHeader = new PageHeaderBand() { HeightF = 23, Name = "pageHeaderBand" };
            int tableWidth = report.PageWidth - report.Margins.Left - report.Margins.Right;
            XRTable headerTable = XRTable.CreateTable(
                                  new Rectangle(0,    // rect X
                                                0,          // rect Y
                                                tableWidth, // width
                                                40),        // height
                                                1,          // table row count
                                                0);         // table column count
            headerTable.Borders = DevExpress.XtraPrinting.BorderSide.All;
            headerTable.BackColor = Color.Gainsboro;
            headerTable.Font = new Font("Verdana", 10, FontStyle.Bold);
            headerTable.Rows.FirstRow.Width = tableWidth;
            headerTable.BeginInit();
            foreach (string field in fields) {
                XRTableCell cell = new XRTableCell();
                cell.Width = 100;
                cell.Text = field;
                cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                headerTable.Rows.FirstRow.Cells.Add(cell);
            }
            headerTable.EndInit();
            headerTable.AdjustSize();
            pageHeader.Controls.Add(headerTable);

            DetailBand detail = new DetailBand() { HeightF = 23, Name = "detailBand" };
            XRTable detailTable = XRTable.CreateTable(
                            new Rectangle(0,    // rect X
                                            0,          // rect Y
                                            tableWidth, // width
                                            40),        // height
                                            1,          // table row count
                                            0);         // table column count

            detailTable.Width = tableWidth;
            detailTable.Rows.FirstRow.Width = tableWidth;
            detailTable.Borders = DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right | DevExpress.XtraPrinting.BorderSide.Bottom;
            detailTable.BeginInit();
            foreach (string field in fields) {
                XRTableCell cell = new XRTableCell();
                ExpressionBinding binding = new ExpressionBinding("BeforePrint", "Text", String.Format("[{0}]", field));
                cell.ExpressionBindings.Add(binding);
                cell.Width = 100;
                cell.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                if (field.Contains("Date"))
                    cell.TextFormatString = "{0:MM/dd/yyyy}";
                detailTable.Rows.FirstRow.Cells.Add(cell);
            }
            detailTable.Font = new Font("Verdana", 8F);
            detailTable.EndInit();
            detailTable.AdjustSize();
            detail.Controls.Add(detailTable);
            report.Bands.AddRange(new Band[] { detail, pageHeader });
        }
    }
}
