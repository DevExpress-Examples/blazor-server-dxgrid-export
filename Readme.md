<!-- default badges list -->
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T854755)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
*Files to look at*:

* [Index.razor](./CS/DxDataGridExportingWithReports/Pages/Index.razor)
* [ExportMiddleware.cs](./CS/DxDataGridExportingWithReports/Helpers/ExportMiddleware.cs)
* [ReportHelper.cs](./CS/DxDataGridExportingWithReports/Helpers/ReportHelper.cs)
* [ExportButtons.razor](./CS/DxDataGridExportingWithReports/Components/ExportButtons.razor)

# DataGrid for Blazor - How to use DevExpress Reporting tools to implement export in a server application

This example illustrates how to use DevExpress Reporting tools to export Data Grid's content to different file formats (*.pdf*/*.xlsx*/*.docx*) in a Blazor Server application.

[Export a Table from Data Grid to PDF](exported-pdf.png)

To export information, apply the [ExportMiddleware](./CS/DxDataGridExportingWithReports/Helpers/ExportMiddleware.cs) type to the application request pipeline. The **ExportMiddleware** handles requests. The response returns the file of the corresponding type.

The [ExportButtons](./CS/DxDataGridExportingWithReports/Components/ExportButtons.razor) component contains export buttons. Each of the export buttons contains an [URI to this project](./CS/DxDataGridExportingWithReports/Pages/Index.razor#L32). The URI contains the Data Grid options. That is why the created report contains only data that is visible in the grid after sorting and filtering operations. The **ExportMiddleware** processes the request with the URI.

Use the [ReportHelper.CreateReport](./CS/DxDataGridExportingWithReports/Helpers/ReportHelper.cs#L9) method together with the [ExportToPdf(String)](https://docs.devexpress.com/XtraReports/DevExpress.XtraReports.UI.XtraReport.ExportToPdf(System.String-DevExpress.XtraPrinting.PdfExportOptions))/[ExportToXlsx(Stream)](https://docs.devexpress.com/XtraReports/DevExpress.XtraReports.UI.XtraReport.ExportToXls(System.IO.Stream-DevExpress.XtraPrinting.XlsExportOptions))/[ExportToDocx(Stream)](https://docs.devexpress.com/XtraReports/DevExpress.XtraReports.UI.XtraReport.ExportToDocx(System.IO.Stream-DevExpress.XtraPrinting.DocxExportOptions)) methods to create a report that is exported to the file of the corresponding type.

<!-- default file list -->

## Files to look at

* [Index.razor](./CS/DxDataGridExportingWithReports/Pages/Index.razor)
* [ExportMiddleware.cs](./CS/DxDataGridExportingWithReports/Helpers/ExportMiddleware.cs)
* [ReportHelper.cs](./CS/DxDataGridExportingWithReports/Helpers/ReportHelper.cs)
* [ExportButtons.razor](./CS/DxDataGridExportingWithReports/Components/ExportButtons.razor)

## Documentation

* [XtraReport](https://docs.devexpress.com/XtraReports/DevExpress.XtraReports.UI.XtraReport)
* [Data Grid: Data Binding](http://docs.devexpress.devx/Blazor/DevExpress.Blazor.DxDataGrid-1.Data)

## More Examples

[DataGrid for Blazor - Client side - How to implement the exporting functionality using DevExpress Reporting tools](https://github.com/DevExpress-Examples/blazor-webassembly-dxdatagrid-export)

[How to use DevExpress Reporting Components in Blazor applications](https://github.com/DevExpress-Examples/how-to-use-reporting-components-in-blazor-applications)
