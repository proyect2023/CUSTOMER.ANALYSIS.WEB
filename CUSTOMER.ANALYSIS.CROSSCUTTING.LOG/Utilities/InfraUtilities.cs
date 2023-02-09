
using GS.TOOLS;
using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CUSTOMER.ANALYSIS.CROSSCUTTING.LOG.Utilities
{
    public static class InfraUtilities
    {
        public static string GenerateLineLog(string mensaje,ref string CodigoSeguimiento)
        {
            var sf = new StackFrame(2, true);
            var Origen_lo = $"{sf.GetMethod().DeclaringType.FullName}/{sf.GetMethod().Name}";
            MappedDiagnosticsLogicalContext.Set("Origen", Origen_lo);
            MappedDiagnosticsLogicalContext.Set("Linea", sf.GetFileLineNumber());
            var Usuario = GSConversions.DBNullToString(MappedDiagnosticsLogicalContext.Get("Usuario"));
            var IdCompania = GSConversions.DBNullToString(MappedDiagnosticsLogicalContext.Get("IdCompania"));
            if (string.IsNullOrEmpty(Usuario))
                MappedDiagnosticsLogicalContext.Set("Usuario", "System");
            if (string.IsNullOrEmpty(IdCompania))
                MappedDiagnosticsLogicalContext.Set("IdCompania", "***");
            var Codigo = $"E{Guid.NewGuid().ToString("N").ToUpper().Substring(0, 5)}{DateTime.Now.ToString("hhmmssffffff")}";
            MappedDiagnosticsLogicalContext.Set("CodigoSeguimiento", Codigo);
            CodigoSeguimiento = MappedDiagnosticsLogicalContext.Get("CodigoSeguimiento");
            return GSConversions.DBNullToString(mensaje).Replace(Environment.NewLine, " ");
        }

        public static string GenerateLineLog(string mensaje, string usuario, string compania, ref string CodigoSeguimiento)
        {
            var sf = new StackFrame(2, true);
            var Origen_lo = $"{sf.GetMethod().DeclaringType.FullName}/{sf.GetMethod().Name}";
            MappedDiagnosticsLogicalContext.Set("Origen", Origen_lo);
            MappedDiagnosticsLogicalContext.Set("Linea", sf.GetFileLineNumber());
            
            if (string.IsNullOrEmpty(usuario))
                MappedDiagnosticsLogicalContext.Set("Usuario", "System");
            if (string.IsNullOrEmpty(compania))
                MappedDiagnosticsLogicalContext.Set("IdCompania", "***");
            var Codigo = $"E{Guid.NewGuid().ToString("N").ToUpper().Substring(0, 5)}{DateTime.Now.ToString("hhmmssffffff")}";
            MappedDiagnosticsLogicalContext.Set("CodigoSeguimiento", Codigo);
            CodigoSeguimiento = MappedDiagnosticsLogicalContext.Get("CodigoSeguimiento");
            return GSConversions.DBNullToString(mensaje).Replace(Environment.NewLine, " ");
        }

        public static string GenerateLineLog(Exception ex, ref string CodigoSeguimiento)
        {
            var sf = new StackFrame(2, true);
            var Origen_lo = $"{sf.GetMethod().DeclaringType.FullName}/{sf.GetMethod().Name}";
            MappedDiagnosticsLogicalContext.Set("Origen", Origen_lo);
            MappedDiagnosticsLogicalContext.Set("Linea", sf.GetFileLineNumber());
            var Usuario = GSConversions.DBNullToString(MappedDiagnosticsLogicalContext.Get("Usuario"));
            var IdCompania = GSConversions.DBNullToString(MappedDiagnosticsLogicalContext.Get("IdCompania"));
            if (string.IsNullOrEmpty(Usuario))
                MappedDiagnosticsLogicalContext.Set("Usuario", "System");
            if (string.IsNullOrEmpty(IdCompania))
                MappedDiagnosticsLogicalContext.Set("IdCompania", "***");

            var Codigo = $"E{Guid.NewGuid().ToString("N").ToUpper().Substring(0, 5)}{DateTime.Now.ToString("hhmmssffffff")}";
            MappedDiagnosticsLogicalContext.Set("CodigoSeguimiento", Codigo);
            CodigoSeguimiento = MappedDiagnosticsLogicalContext.Get("CodigoSeguimiento");
            return GSConversions.DBNullToString(GSConversions.ExceptionToString(ex)).Replace(Environment.NewLine, " ");
        }
    }
}
