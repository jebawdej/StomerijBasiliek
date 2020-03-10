using StomerijBasiliek.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StomerijBasiliek.Service
{
    public interface IWerkorderDataService
    {
        WerkorderDTO Get(Guid id);
        ObservableCollection<WerkorderDTO> GetAll();

        bool Add(WerkorderDTO werkorder);
        bool Update(WerkorderDTO werkorder);
        bool Delete(Guid id);
        WerkorderDTO Search(string BonNr);

        DateTime? LaatstToegevoegd();

        Guid LastKlantId();
    }
}
