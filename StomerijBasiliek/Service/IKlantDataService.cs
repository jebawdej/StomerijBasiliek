using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StomerijBasiliek.DTO;

namespace StomerijBasiliek.Service
{
    public interface IKlantDataService
    {
        KlantDTO Get(Guid id);
        ObservableCollection<KlantDTO> GetAll();

        bool Add(KlantDTO klant);
        bool Update(KlantDTO klant);
        bool Delete(Guid id);

        int MaxKlantNr();

        ObservableCollection<KlantDTO> Search(string postCode, string huisNr);
    }
}
