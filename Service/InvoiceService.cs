

using Party_Management.Data;
using Party_Management.ServiceContract;

namespace Party_Management.Service
{
    public class InvoiceService : IInvoiceService
    {

        private readonly ApplicationDbContext _db;
        public InvoiceService(ApplicationDbContext db)
        {
            _db = db;
        }

    }
}
