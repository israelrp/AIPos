using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AIPos.WebLayer.Helpers
{
    public enum GridViewExportType { None, Pdf, Xls, Xlsx, Rtf, Csv }

    public delegate ActionResult ExportMethod(GridViewSettings settings, object dataObject);

    public class ExportType
    {
        public string Title { get; set; }
        public ExportMethod Method { get; set; }
    }
    public class GridViewExportHelper
    {
        static Dictionary<string, ExportType> exportTypes;
        public static Dictionary<string, ExportType> ExportTypes
        {
            get
            {
                if (exportTypes == null)
                    exportTypes = CreateExportTypes();
                return exportTypes;
            }
        }
        static Dictionary<string, ExportType> CreateExportTypes()
        {
            Dictionary<string, ExportType> types = new Dictionary<string, ExportType>();
            types.Add("PDF", new ExportType { Title = "Export to PDF", Method = GridViewExtension.ExportToPdf });
            types.Add("XLS", new ExportType { Title = "Export to XLS", Method = GridViewExtension.ExportToXls });
            types.Add("XLSX", new ExportType { Title = "Export to XLSX", Method = GridViewExtension.ExportToXlsx });
            types.Add("RTF", new ExportType { Title = "Export to RTF", Method = GridViewExtension.ExportToRtf });
            types.Add("CSV", new ExportType { Title = "Export to CSV", Method = GridViewExtension.ExportToCsv });
            return types;
        }
    }

    public abstract class GridViewSettingExportHelper
    {
        GridViewSettings exportGridViewSettings;
        public GridViewSettings ExportGridViewSettings
        {
            get
            {
                if (exportGridViewSettings == null)
                    exportGridViewSettings = CreateExportGridViewSettings();
                return exportGridViewSettings;
            }
        }

        public abstract GridViewSettings CreateExportGridViewSettings();
    }
}