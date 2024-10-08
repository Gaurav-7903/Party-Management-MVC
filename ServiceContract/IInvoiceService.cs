﻿using Party_Management.DTOs;

namespace Party_Management.ServiceContract
{
    public interface IInvoiceService
    {
        public InvoiceResponseDTO AddInvoice(IEnumerable<InvoiceRequestDTO> invoiceRequest, int PartyId);

        public IEnumerable<InvoiceResponseDTO> GetAllInvoice();

        public IEnumerable<InvoiceResponseDTO> GetInvoiceByPartyId(int partyId);

        public InvoiceDetailDTO GetInvoiceDetailsByInvoiceId(int invoiceId);
    }
}
