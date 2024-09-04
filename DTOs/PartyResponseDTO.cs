using Party_Management.DTOs;
using Party_Management.Models;
using System.Diagnostics.Metrics;

namespace Party_Management.DTOs
{
    public class PartyResponseDTO
    {
        public int PartyId { get; set; }
        public string PartyName { get; set; }

        public string EmailAddress { get; set; }

        public bool ReceiveNotifications { get; set; }
    }
}

public static class PartyExtension
{
    public static PartyResponseDTO ToPartyResponse(this Party? party)
    {
        return new PartyResponseDTO()
        {
            PartyId = party.PartyId,
            PartyName = party.PartyName,
            ReceiveNotifications = party.ReceiveNotification,
            EmailAddress = party.EmailAddress,
        };
    }
}
