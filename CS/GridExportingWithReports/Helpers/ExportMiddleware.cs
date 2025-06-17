using System.Linq;
using System.Threading.Tasks;
using GridExportingWithReports.Data;
using DevExpress.XtraReports.UI;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Data.Helpers;
using System.IO;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;

namespace GridExportingWithReports.Helpers {
    public class ExportMiddleware : IMiddleware {
        WeatherForecastService weatherForecastService;
        public ExportMiddleware(WeatherForecastService _weatherForecastService) {
            weatherForecastService = _weatherForecastService;
        }
        public Task InvokeAsync(HttpContext context, RequestDelegate next) {
            if (context.Request.Path.ToString().StartsWith("/exportMht")) {
                return ExportResult(mht, GetOptionsFromQuery(context.Request.QueryString.ToString()), context);
            }
            else if (context.Request.Path.ToString().StartsWith("/exportHtml")) {
                return ExportResult(html, GetOptionsFromQuery(context.Request.QueryString.ToString()), context);
            }
            else if (context.Request.Path.ToString().StartsWith("/exportDocx")) {
                return ExportResult(docx, GetOptionsFromQuery(context.Request.QueryString.ToString()), context);
            }
            return next(context);
        }
        private DataSourceLoadOptionsBase GetOptionsFromQuery(string query) {
            DataSourceLoadOptionsBase options = new DataSourceLoadOptionsBase();
            IDictionary<string, StringValues> myQUeryString = QueryHelpers.ParseQuery(query);
            DataSourceLoadOptionsParser.Parse(options, key => {
                if (myQUeryString.ContainsKey(key))
                    return myQUeryString[key];
                return null;
            });
            return options;
        }
        private readonly string mht = "mht";
        private readonly string html = "html";
        private readonly string docx = "docx";
        private async Task ExportResult(string format, DataSourceLoadOptionsBase dataOptions, HttpContext context) {
            XtraReport report = new XtraReport();
            dataOptions.Skip = 0;
            dataOptions.Take = 0;
            var loadedData = DataSourceLoader.Load(await weatherForecastService.GetForecastAsync(), dataOptions);
            report.DataSource = loadedData.data.Cast<WeatherForecast>();
            ReportHelper.CreateReport(report, new string[] { "TemperatureC", "TemperatureF", "Summary", "Date" });
            report.CreateDocument();
            using (MemoryStream fs = new MemoryStream()) {
                if (format == mht)
                    report.ExportToMht(fs);
                else if (format == html)
                    report.ExportToHtml(fs);
                else if (format == docx)
                    report.ExportToDocx(fs);
                context.Response.Clear();
                context.Response.Headers.Append("Content-Type", "application/" + format);
                context.Response.Headers.Append("Content-Transfer-Encoding", "binary");
                context.Response.Headers.Append("Content-Disposition", "attachment; filename=ExportedDocument." + format);
                await context.Response.Body.WriteAsync(fs.ToArray(), 0, fs.ToArray().Length);
                await context.Response.CompleteAsync();
            }
        }
    }
}
