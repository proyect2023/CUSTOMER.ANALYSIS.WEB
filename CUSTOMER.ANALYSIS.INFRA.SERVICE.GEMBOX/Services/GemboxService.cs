
using GemBox.Spreadsheet;
using GS.TOOLS;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.Services;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.DTOs.Services;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Entities;

namespace CUSTOMER.ANALYSIS.INFRA.SERVICE.GEMBOX.Services
{
    public class GemboxService : IGemboxService
    {
        public readonly string FormatoFechaHora = "dd/MM/yyyy HH:mm:ss";

        public GemboxService()
        {
            SpreadsheetInfo.SetLicense("FREE-LIMITED-KEY");
        }

        private bool InsertarValorCelda(ExcelWorksheet ws, string clave, object valor, ref int Row, ref int Col)
        {
            try
            {
                Row = 0; Col = 0;
                ws.Cells.FindText(clave, true, true, out Row, out Col);
                if (Row >= 0 && Col >= 0)
                {
                    ws.Cells[Row, Col].Value = valor;
                    ws.Cells[Row, Col].Style.WrapText = true;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private static SaveOptions GetSaveOptions(string format)
        {
            switch (format.ToUpper())
            {
                case "XLSX":
                    return SaveOptions.XlsxDefault;
                case "XLS":
                    return SaveOptions.XlsDefault;
                case "ODS":
                    return SaveOptions.OdsDefault;
                case "CSV":
                    return SaveOptions.CsvDefault;
                case "HTML":
                    return SaveOptions.HtmlDefault;
                case "PDF":
                    {
                        //GemBox.Document.Orientation = GemBox.Document.Orientation.
                        PdfSaveOptions optionsPdf = new PdfSaveOptions() { SelectionType = SelectionType.EntireFile };
                        optionsPdf.ConformanceLevel = PdfConformanceLevel.PdfA1a;
                        return optionsPdf;// SaveOptions.PdfDefault;
                    }
                    

                case "XPS":
                case "PNG":
                case "JPG":
                case "GIF":
                case "TIF":
                case "BMP":
                case "WMP":
                    throw new InvalidOperationException("To enable saving to XPS or image format, add 'Microsoft.WindowsDesktop.App' framework reference.");

                default:
                    throw new NotSupportedException();
            }
        }

        private static LoadOptions GetLoadOptions(string format)
        {
            switch (format.ToUpper())
            {
                case "XLSX":
                    return LoadOptions.XlsxDefault;
                case "XLS":
                    return LoadOptions.XlsDefault;
                case "ODS":
                    return LoadOptions.OdsDefault;
                case "CSV":
                    return LoadOptions.CsvDefault;
                case "HTML":
                    return LoadOptions.HtmlDefault;
                default:
                    return LoadOptions.XlsxDefault;
            }
        }

        public MethodResponseDto ConstruirReporte(List<ClientePlan> reportes, string NombreArchivo, string format, string ExtensionFormato = "xlsx")
        {
            MethodResponseDto responseDto = new();
            try
            {
                string path = $@"wwwroot/formatos/{NombreArchivo}";

                LoadOptions loadOptions = GetLoadOptions((ExtensionFormato));
                ExcelFile workbook = ExcelFile.Load(path, loadOptions);

                int Row = 0; int Col = 0;
                ExcelWorksheet ws = workbook.Worksheets.FirstOrDefault(x => x.Name == "Reporte");

                InsertarValorCelda(ws, "{Total_resultados}", reportes.Count, ref Row, ref Col);

                if (InsertarValorCelda(ws, "{Detalle_Item}", "", ref Row, ref Col))
                {
                    if (reportes.Any())
                    {
                        ConstruirDetalleReporte(reportes, ref ws, ref Row, ref Col);
                    }
                }

                SaveOptions options = GetSaveOptions(format);

                using MemoryStream stream = new MemoryStream();
                workbook.Save(stream, options);
                responseDto.Data = stream.ToArray();
                responseDto.Estado = true;

            }
            catch (Exception ex)
            {
                responseDto.MensajeError = string.Format("{0} => {1}", this.GetCaller(), GSConversions.ExceptionToString(ex));
                responseDto.TieneErrores = true;
            }

            return responseDto;
        }

        public void ConstruirDetalleReporte(List<ClientePlan> detalles, ref ExcelWorksheet ws, ref int Row, ref int Col)
        {
            int cantidadLineas = detalles?.Count ?? 0;
            ws.Rows.InsertCopy(Row, cantidadLineas, ws.Rows[Row]);
            int cont = 0;

            foreach (var line in detalles)
            {
                cont++;

                ws.Cells[Row, 0].Value = line.IdClienteNavigation?.RazonSocial;
                ws.Cells[Row, 0].Style.WrapText = true;

                ws.Cells[Row, 1].Value = line.IdPlanNavigation?.Nombre;
                ws.Cells[Row, 1].Style.WrapText = true;

                ws.Cells[Row, 2].Value = line.IdPlanNavigation?.IdTipoPlanNavigation?.Nombre;
                ws.Cells[Row, 2].Style.WrapText = true;

                ws.Cells[Row, 3].Value = line.IdClienteNavigation?.IdSectorNavigation?.Nombre;
                ws.Cells[Row, 3].Style.WrapText = true;

                ws.Cells[Row, 4].Value = line.FechaContratacion.Value.ToString("dd/MM/yyyy");
                ws.Cells[Row, 4].Style.WrapText = true;

                ws.Cells[Row, 5].Value = line.PagoMensual ?? 0;
                ws.Cells[Row, 5].Style.WrapText = true;

                Row++;
            }
        }

    }
}
