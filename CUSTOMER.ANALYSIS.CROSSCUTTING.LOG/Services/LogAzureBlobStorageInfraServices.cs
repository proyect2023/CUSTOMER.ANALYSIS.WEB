
//using NLog;
//using NLog.Config;
//using NLog.Targets;
using System;
using System.Threading.Tasks;

namespace CUSTOMER.ANALYSIS.CROSSCUTTING.LOG.Services
{
    public class LogAzureBlobStorageInfraServices //: ILogInfraServices
    {
        //protected Logger LOG;
        //protected readonly bool LogHabilitar;
        //protected readonly string ConnectionString;
        //protected readonly string Container;
        //protected readonly string LogRuta;
        //protected readonly string LogLayout;
        //protected readonly string LogFileName;
        //public LogAzureBlobStorageInfraServices(bool LogHabilitar,string ConnectionString,string Container,string LogRuta,string LogLayout,string LogFileName)
        //{
        //    this.LogHabilitar = LogHabilitar;
        //    this.ConnectionString = ConnectionString;
        //    this.Container = Container;
        //    this.LogRuta = LogRuta;
        //    this.LogLayout = LogLayout;
        //    this.LogFileName = LogFileName;
        //    if (LogManager.Configuration == null)
        //        Config();
        //    else
        //        LOG = LogManager.GetCurrentClassLogger();
        //}
        //public string AddLog(string mensaje)
        //{
        //    if (LogHabilitar)
        //    {
        //        string CodigoSeguimiento = "";
        //        LOG.Info(InfraUtilities.GenerateLineLog(mensaje, ref CodigoSeguimiento));
        //        return CodigoSeguimiento;
        //    }
        //    return null;
        //}

        //public string AddLog(Exception ex)
        //{
        //    if (LogHabilitar)
        //    {
        //        string CodigoSeguimiento = "";
        //        LOG.Info(InfraUtilities.GenerateLineLog(ex, ref CodigoSeguimiento));
        //        return CodigoSeguimiento;
        //    }
        //    return null;
        //}

        //public Task<string> AddLogAsync(string mensaje)
        //{
        //    var t = new Task<string>(
        //    () =>
        //    {
        //        return AddLog(mensaje);
        //    });
        //    t.Start();
        //    t.Wait();
        //    return t;
        //}

        //public Task<string> AddLogAsync(Exception ex)
        //{
        //    var t = new Task<string>(
        //    () =>
        //    {
        //        return AddLog(ex);
        //    });
        //    t.Start();
        //    t.Wait();
        //    return t;
        //}

        //private void Config()
        //{
        //    var config = new LoggingConfiguration();
        //    var target = new BlobStorageTarget()
        //    {
        //        Name="Azure",
        //        ConnectionString= ConnectionString,
        //        BlobName=$"{LogRuta}{LogFileName}.log",
        //        Container= Container,
        //        Layout =LogLayout
        //    };
        //    config.AddRule(LogLevel.Debug, LogLevel.Fatal, target, "*");
        //    LogManager.Configuration = config;
        //    LOG = LogManager.GetCurrentClassLogger();
        //}
    }
}
