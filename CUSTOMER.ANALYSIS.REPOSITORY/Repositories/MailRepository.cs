
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Contants;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Entities;
using CUSTOMER.ANALYSIS.APPLICATION.CORE.Interfaces.Repositories;
using CUSTOMER.ANALYSIS.REPOSITORY.Data;
using System.Collections.Generic;
using System.Linq;

namespace CUSTOMER.ANALYSIS.INFRA.DATA.REPOSITORY.Repositories
{
    public class MailRepository : Repository<Correo>, IMailRepository
    {
        public MailRepository(EFContext context) : base(context)
        {

        }
        public List<Correo> ConsultarCorreosReenviados()
        {
            return _context.Correos.Where(x => x.Estado == (int)EstadosEnvioMail.Enviado && x.IntentosEnvio != null).ToList();
        }

        public List<Correo> ConsultarErroresDeReenvio()
        {
            return _context.Correos.Where(x => x.Error != null && x.IntentosEnvio != null).ToList();
        }

        public List<Correo> ConsultarCorreosNoEnviados()
        {
            return _context.Correos.Where(x => x.Estado == (int)EstadosEnvioMail.PendienteEnvio).ToList();
        }

        public bool AddMail(Correo correo)
        {
            _context.Correos.Add(correo);
            return _context.SaveChanges() > 0;
        }
    }
}
