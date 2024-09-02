using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Party_Management.Models
{
    public class Invoice
    {
        [Key]
        public int InvoiceId { get; set; }


        [Required(ErrorMessage = "Party is Required")]
        public int PartyId { get; set; }

        [ForeignKey("PartyId")]
        public Party Party { get; set; }


        [DataType(DataType.Date)]
        public DateTime InvoiceDate { get; set; } = DateTime.Now;

        public ICollection<InvoiceDetails>? InvoiceDetails { get; set; }
    }
}


