using DataBase;
using DataBase.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using System.Globalization;

namespace PruebaTecnica
{
    //Esta clase se maneja la insersion de data fija
    public static class SeedData
    {
        private static string[] formats = { "M/d/yyyy h:mm:ss tt", "M/d/yyyy hh:mm:ss tt", "MM/dd/yyyy h:mm:ss tt" }; // Define possible formats
        private static DateTime dateTime;

        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new EPSAContext(serviceProvider.GetRequiredService<DbContextOptions<EPSAContext>>()))
            {
                var filePath = "Files\\EPSA_Listado_Costos.xlsx";
                var excelPackage = new ExcelPackage(new FileInfo(filePath));

                //valido que la entity que se esta creando no tenga data ya creada asi no genero duplicados
                if (!context.Consumptions.Any())
                {
                    SeedConsumtions(context, excelPackage.Workbook.Worksheets["CONSUMO_POR_TRAMO"]);
                }

                if (!context.Costs.Any())
                {
                    SeedCost(context, excelPackage.Workbook.Worksheets["COSTOS_POR_TRAMO"]);
                }

                if (!context.Losses.Any())
                {
                    SeedLosses(context, excelPackage.Workbook.Worksheets["PERDIDAS_POR_TRAMO"]);
                }

                context.SaveChanges();
            }
        }

        public static void SeedConsumtions(EPSAContext context, ExcelWorksheet worksheet)
        {
            int rowCount = worksheet.Dimension.Rows;
            int colCount = worksheet.Dimension.Columns;

            for (int row = 2; row <= rowCount; row++)
            {

                var line = string.IsNullOrEmpty(worksheet.Cells[row, 1].Value?.ToString()) ? "" : worksheet.Cells[row, 1].Value?.ToString();
                var date = string.IsNullOrEmpty(worksheet.Cells[row, 2].Value?.ToString()) ? null : worksheet.Cells[row, 2].Value?.ToString();
                var residential = string.IsNullOrEmpty(worksheet.Cells[row, 3].Value?.ToString()) ? "0" : worksheet.Cells[row, 3].Value?.ToString();
                var comercial = string.IsNullOrEmpty(worksheet.Cells[row, 4].Value?.ToString()) ? "0" : worksheet.Cells[row, 4].Value?.ToString();
                var industrial = string.IsNullOrEmpty(worksheet.Cells[row, 5].Value?.ToString()) ? "0" : worksheet.Cells[row, 5].Value?.ToString();

                var newDate = 0;
                DateTime baseDate = new DateTime(1899, 12, 30);
                DateTime dateValue = new DateTime();

                if (DateTime.TryParseExact(date, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
                {
                    dateValue = dateTime;
                }
                else
                {
                    newDate = Convert.ToInt32(date);
                    dateValue = baseDate.AddDays(newDate);
                }


                context.Consumptions.Add(new Consumption
                {
                    Line = line,
                    Date = dateValue,
                    Residential_Wh = float.Parse(residential),
                    Commercial_Wh = float.Parse(comercial),
                    Industrial_Wh = float.Parse(industrial)
                });
            }
        }

        public static void SeedCost(EPSAContext context, ExcelWorksheet worksheet)
        {
            int rowCount = worksheet.Dimension.Rows;
            int colCount = worksheet.Dimension.Columns;

            for (int row = 2; row <= rowCount; row++)
            {
                var line = string.IsNullOrEmpty(worksheet.Cells[row, 1].Value?.ToString()) ? "" : worksheet.Cells[row, 1].Value?.ToString();
                var date = string.IsNullOrEmpty(worksheet.Cells[row, 2].Value?.ToString()) ? null : worksheet.Cells[row, 2].Value?.ToString();
                var residential = string.IsNullOrEmpty(worksheet.Cells[row, 3].Value?.ToString()) ? "0" : worksheet.Cells[row, 3].Value?.ToString();
                var comercial = string.IsNullOrEmpty(worksheet.Cells[row, 4].Value?.ToString()) ? "0" : worksheet.Cells[row, 4].Value?.ToString();
                var industrial = string.IsNullOrEmpty(worksheet.Cells[row, 5].Value?.ToString()) ? "0" : worksheet.Cells[row, 5].Value?.ToString();

                var newDate = 0;
                DateTime baseDate = new DateTime(1899, 12, 30);
                DateTime dateValue = new DateTime();

                if (DateTime.TryParseExact(date, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
                {
                    dateValue = dateTime;
                }
                else
                {
                    newDate = Convert.ToInt32(date);
                    dateValue = baseDate.AddDays(newDate);
                }


                context.Costs.Add(new Costs
                {
                    Line = line,
                    Date = dateValue,
                    Residential_Wh = float.Parse(residential),
                    Commercial_Wh = float.Parse(comercial),
                    Industrial_Wh = float.Parse(industrial)
                });
            }
        }

        public static void SeedLosses(EPSAContext context, ExcelWorksheet worksheet)
        {
            int rowCount = worksheet.Dimension.Rows;
            int colCount = worksheet.Dimension.Columns;

            for (int row = 2; row <= rowCount; row++)
            {

                var line = string.IsNullOrEmpty(worksheet.Cells[row, 1].Value?.ToString()) ? "" : worksheet.Cells[row, 1].Value?.ToString();
                var date = string.IsNullOrEmpty(worksheet.Cells[row, 2].Value?.ToString()) ? null : worksheet.Cells[row, 2].Value?.ToString();
                var residential = string.IsNullOrEmpty(worksheet.Cells[row, 3].Value?.ToString()) ? "0" : worksheet.Cells[row, 3].Value?.ToString();
                var comercial = string.IsNullOrEmpty(worksheet.Cells[row, 4].Value?.ToString()) ? "0" : worksheet.Cells[row, 4].Value?.ToString();
                var industrial = string.IsNullOrEmpty(worksheet.Cells[row, 5].Value?.ToString()) ? "0" : worksheet.Cells[row, 5].Value?.ToString();

                var newDate = 0;
                DateTime baseDate = new DateTime(1899, 12, 30);
                DateTime dateValue = new DateTime();

                if (DateTime.TryParseExact(date, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
                {
                    dateValue = dateTime;
                }
                else
                {
                    newDate = Convert.ToInt32(date);
                    dateValue = baseDate.AddDays(newDate);
                }


                context.Losses.Add(new Losses
                {
                    Line = line,
                    Date = dateValue,
                    Residential_Percentage = float.Parse(residential),
                    Commercial_Percentage = float.Parse(comercial),
                    Industrial_Percentage = float.Parse(industrial)
                });
            }
        }

    }
}
