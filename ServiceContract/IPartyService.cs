﻿

using Party_Management.DTOs;

namespace ServiceContract
{
    public interface IPartyService
    {
        public IEnumerable<PartyResponseDTO> GetAllParty();

        public PartyResponseDTO GetPartyById(int id);

        public PartyResponseDTO? GetPartyByName(string PartyName);

        public PartyResponseDTO CreateParty(PartyRequestDTO request);

        public PartyResponseDTO UpdateParty(PartyResponseDTO request);

        public PartyResponseDTO DeleteParty(int id);

        public PartyResponseDTO? GetPartyByEmail(string EmailAddress);


    }
}
