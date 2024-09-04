using Party_Management.Data;
using Party_Management.DTOs;
using ServiceContract;
using Party_Management.Models;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class PartyService : IPartyService
    {
        private readonly ApplicationDbContext _db;

        public PartyService(ApplicationDbContext db)
        {
            _db = db;
        }

        public PartyResponseDTO CreateParty(PartyRequestDTO request)
        {
            var party = new Party()
            {
                PartyName = request.PartyName,
                ReceiveNotification = request.ReceiveNotifications,
                EmailAddress = request.EmailAddress,
            };

            _db.Parties.Add(party);
            _db.SaveChanges();
            Console.WriteLine("Party Id : " + party.PartyId);
            return party.ToPartyResponse();
        }

        public PartyResponseDTO DeleteParty(int id)
        {
            var party = _db.Parties.Find(id);
            if (party == null)
            {
                throw new KeyNotFoundException("Party not found");
            }
            _db.Parties.Remove(party);
            _db.SaveChanges();

            return party.ToPartyResponse();

        }

        public IEnumerable<PartyResponseDTO> GetAllParty()
        {
            var parties = _db.Parties.Select(p => p.ToPartyResponse()).ToList();

            return parties;
        }

        public PartyResponseDTO? GetPartyById(int id)
        {
            var party = _db.Parties.Where(p => p.PartyId == id).FirstOrDefault()?.ToPartyResponse();
            return party;

        }

        public PartyResponseDTO UpdateParty(PartyResponseDTO request)
        {
            var party = _db.Parties.Find(request.PartyId);
            if (party == null)
            {
                throw new KeyNotFoundException("Party not found");
            }
            party.PartyName = request.PartyName;
            party.ReceiveNotification = request.ReceiveNotifications;

            _db.SaveChanges();

            return party.ToPartyResponse();
        }

        public PartyResponseDTO? GetPartyByName(string name)
        {
            var party = _db.Parties.Find(name);
            return party.ToPartyResponse();
        }

        public PartyResponseDTO? GetPartyByEmail(string EmailAddress)
        {
            var party = _db.Parties.Where(p => p.EmailAddress==EmailAddress).FirstOrDefault();
            return party.ToPartyResponse();
        }
    }
}
