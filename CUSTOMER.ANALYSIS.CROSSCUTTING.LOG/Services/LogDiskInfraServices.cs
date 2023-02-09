
using CUSTOMER.ANALYSIS.CROSSCUTTING.Interfaces;
using CUSTOMER.ANALYSIS.CROSSCUTTING.LOG.Utilities;
using NLog;
using NLog.Config;
using NLog.Targets;
using System;
using System.Threading.Tasks;

namespace CUSTOMER.ANALYSIS.CROSSCUTTING.LOG.Services
{
    public class LogDiskInfraServices : ILogInfraServices
    {
        private readonly Logger LOG;
        private readonly bool LogHabilitar;
        private readonly string LogRuta;
        private readonly string LogLayout;
        private readonly string LogFileName;

        public LogDiskInfraServices(bool LogHabilitar, string LogRuta, string LogLayout, string LogFileName)
        {
            this.LogHabilitar = LogHabilitar;
            this.LogRuta = LogRuta;
            this.LogLayout = LogLayout;
            this.LogFileName = LogFileName;
            if (LogManager.Configuration == null)
                LOG = Config();
            else
                LOG = LogManager.GetCurrentClassLogger();
        }

        public string AddLog(string origen, string mensaje)
        {
            if (LogHabilitar)
            {
                string CodigoSeguimiento = "";
                LOG.Info(origen + " => " + InfraUtilities.GenerateLineLog(mensaje, ref CodigoSeguimiento));
                return CodigoSeguimiento;
            }
            return "";
        }

        public string AddLog(string origen, string mensaje, string usuario, string compania)
        {
            if (LogHabilitar)
            {
                string CodigoSeguimiento = "";
                LOG.Info(origen + " => " + InfraUtilities.GenerateLineLog(mensaje, usuario, compania, ref CodigoSeguimiento));
                return CodigoSeguimiento;
            }
            return "";
        }

        public string AddLog(string origen, Exception ex)
        {
            if (LogHabilitar)
            {
                string CodigoSeguimiento = "";
                LOG.Info(origen + " => " + InfraUtilities.GenerateLineLog(ex, ref CodigoSeguimiento));
                return CodigoSeguimiento;
            }
            return "";
        }

        public Task<string> AddLogAsync(string origen, string mensaje)
        {
            var t = new Task<string>(
            () =>
            {
                if (LogHabilitar)
                {
                    string CodigoSeguimiento = "";
                    LOG.Info(origen + " => " + InfraUtilities.GenerateLineLog(mensaje, ref CodigoSeguimiento));
                    return CodigoSeguimiento;
                }
                return "";
            });
            t.Start();
            t.Wait();
            return t;
        }

        public Task<string> AddLogAsync(string origen, Exception ex)
        {
            var t = new Task<string>(
            () =>
            {
                if (LogHabilitar)
                {
                    string CodigoSeguimiento = "";
                    LOG.Info(origen + " => " + InfraUtilities.GenerateLineLog(ex, ref CodigoSeguimiento));
                    return CodigoSeguimiento;
                }
                return "";
            });
            t.Start();
            t.Wait();
            return t;
        }

        private Logger Config()
        {
            var config = new LoggingConfiguration();
            var fileTarget = new FileTarget()
            {
                ArchiveEvery = FileArchivePeriod.Day,
                ArchiveNumbering = ArchiveNumberingMode.Rolling,
                ArchiveFileName = $"{LogRuta}//{LogFileName}.zip",
                EnableArchiveFileCompression = true,
                FileName = $"{LogRuta}{LogFileName}.log",
                Layout = LogLayout
            };
            config.AddRule(LogLevel.Debug, LogLevel.Fatal, fileTarget, "*");
            LogManager.Configuration = config;
            return LogManager.GetCurrentClassLogger();
        }
    }
}
