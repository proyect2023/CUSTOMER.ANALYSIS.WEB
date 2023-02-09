
//using NLog;
//using NLog.Config;
//using NLog.Targets;
using System;
using System.Threading.Tasks;

namespace CUSTOMER.ANALYSIS.CROSSCUTTING.LOG.Services
{
    public class LogDataBaseInfraServices //: ILogInfraServices
    {
        //protected Logger LOG;
        //protected readonly bool LogHabilitar;
        //protected readonly string Provider;
        //protected readonly string Server;
        //protected readonly string DbName;
        //protected readonly string TbName;
        //protected readonly string User;
        //protected readonly string Pass;
        //public LogDataBaseInfraServices(bool LogHabilitar,string Provider,string Server, string DbName,string TbName,string User,string Pass)
        //{
        //    this.LogHabilitar = LogHabilitar;
        //    this.Provider = Provider;
        //    this.Server = Server;
        //    this.DbName = DbName;
        //    this.TbName = TbName;
        //    this.User = User;
        //    this.Pass = Pass;
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
        //        string msj = InfraUtilities.GenerateLineLog(mensaje, ref CodigoSeguimiento);
        //        GlobalDiagnosticsContext.Set("Mensaje", msj);
        //        LOG.Info(msj);
        //        return CodigoSeguimiento;
        //    }
        //    return null;
        //}

        //public string AddLog(Exception ex)
        //{
        //    if (LogHabilitar)
        //    {
        //        string CodigoSeguimiento = "";
        //        string Mensaje = InfraUtilities.GenerateLineLog(ex, ref CodigoSeguimiento);
        //        GlobalDiagnosticsContext.Set("Mensaje", Mensaje);
        //        LOG.Info(Mensaje);
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
        //    var target = new DatabaseTarget()
        //    {
        //        DBHost=Server,
        //        DBDatabase=DbName,
        //        DBUserName=User,
        //        DBPassword=Pass,
        //        DBProvider=Provider,
        //        CommandText = "INSERT INTO "+TbName+ " (FechayHora,CodigoSeguimiento,Aplicacion,Mensaje) VALUES('${longdate}','${gdc:item=CodigoSeguimiento}','${gdc:item=Application}','${gdc:item=Mensaje}')",
        //    };
        //    config.AddRule(LogLevel.Debug, LogLevel.Fatal, target, "*");
        //    LogManager.Configuration = config;
        //    LOG = LogManager.GetCurrentClassLogger();
        //}
    }
}
