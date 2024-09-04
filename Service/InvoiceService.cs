

using Microsoft.EntityFrameworkCore;
using Party_Management.Data;
using Party_Management.DTOs;
using Party_Management.Models;
using Party_Management.ServiceContract;
using ServiceContract;

namespace Party_Management.Service
{
    public class InvoiceService : IInvoiceService
    {

        private readonly ApplicationDbContext _db;
        private readonly IPartyService _partyService;
        public InvoiceService(ApplicationDbContext db, IPartyService partyService)
        {
            _db = db;
            _partyService = partyService;
        }

        public InvoiceResponseDTO AddInvoice(IEnumerable<InvoiceRequestDTO> invoiceRequest, int partyId)
        {
            if (invoiceRequest == null)
            {
                throw new ArgumentNullException(nameof(invoiceRequest));
            }

            decimal TotalPrice = invoiceRequest.Select(x => x.Price * x.Quantity).Sum();

            Console.WriteLine(TotalPrice);

            Invoice invoice = new Invoice()
            {
                PartyId = partyId,
                InvoiceDate = DateTime.Now,
            };

            _db.Invoices.Add(invoice);
            _db.SaveChanges();

            foreach (var item in invoiceRequest)
            {
                InvoiceDetails invoiceDetails = new InvoiceDetails()
                {
                    InvoiceId = invoice.InvoiceId,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    Price = item.Price,
                };
                _db.InvoicesDetails.Add(invoiceDetails);
            }
            _db.SaveChanges();

            return new InvoiceResponseDTO()
            {
                PartyId = partyId,
                InvoiceId = invoice.InvoiceId,
                ProductCount = invoiceRequest.Count(),
                Total = TotalPrice,
            };
        }

        public IEnumerable<InvoiceResponseDTO> GetAllInvoice()
        {
            var invoiceSummaries = _db.Invoices
                .Select(invoice => new InvoiceResponseDTO
                {
                    InvoiceId = invoice.InvoiceId,
                    PartyId = invoice.PartyId,
                    PartyName = _db.Parties.Where(p => p.PartyId == invoice.PartyId).FirstOrDefault().PartyName,
                    ProductCount = invoice.InvoiceDetails.Count(),
                    Total = invoice.InvoiceDetails.Sum(detail => detail.Quantity * detail.Price)
                })
                .ToList();

            return invoiceSummaries;
        }

        public IEnumerable<InvoiceResponseDTO> GetInvoiceByPartyId(int partyId)
        {
            var partyInvoice = _db.Invoices.Where(invoice => invoice.PartyId == partyId)
                .Select(invoice => new InvoiceResponseDTO
                {
                    InvoiceId = invoice.InvoiceId,
                    PartyId = invoice.PartyId,
                    PartyName = _db.Parties.Where(p => p.PartyId == invoice.PartyId).FirstOrDefault().PartyName,
                    ProductCount = invoice.InvoiceDetails.Count(),
                    Total = invoice.InvoiceDetails.Sum(detail => detail.Quantity * detail.Price),
                    InvoiceDate = invoice.InvoiceDate,

                }).ToList();

            return partyInvoice;
        }

        public InvoiceDetailDTO GetInvoiceDetailsByInvoiceId(int invoiceId)
        {
            try
            {
                var Invoice = _db.Invoices.Find(invoiceId);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            InvoiceDetailDTO InvoiceDetail = _db.Invoices.Where(invoice => invoice.InvoiceId == invoiceId).Select(invoice => new InvoiceDetailDTO
            {
                InvoiceId = invoice.InvoiceId,
                PartyId = invoice.PartyId,
                PartyName = _db.Parties.Where(party => party.PartyId == invoice.PartyId).FirstOrDefault().PartyName,
                InvoiceDate = invoice.InvoiceDate,
                invoiceItems = invoice.InvoiceDetails.Select(item => new InvoiceItemDTO
                {
                    ProductId = item.ProductId,
                    ProductName = _db.Products.Where(product => product.ProductId == item.ProductId).FirstOrDefault().Name,
                    Quantity = item.Quantity,
                    Price = item.Price,
                })
            }).First();

            return InvoiceDetail;
        }
    }
}
