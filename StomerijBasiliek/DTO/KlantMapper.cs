using AutoMapper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StomerijBasiliek.DTO
{
    public class KlantMapper
    {

        private IMapper mapper;
        public KlantMapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Klant, KlantDTO>().ForMember(x => x.WerkorderDTO, opt => opt.Ignore());
                cfg.CreateMap<KlantDTO, Klant>().ForMember(x => x.Werkorder, opt => opt.Ignore());
                cfg.CreateMap<KlantDTO, KlantDTO>().ForMember(x => x.WerkorderDTO, opt => opt.Ignore());
                cfg.CreateMap<Werkorder, WerkorderDTO>();
                cfg.CreateMap<WerkorderDTO, Werkorder>();
            });
            mapper = config.CreateMapper();
        }

        public Klant ConvertFromDTO(KlantDTO klantDTO)
        {
            Klant entity = mapper.Map<Klant>(klantDTO);
            foreach (WerkorderDTO wo in klantDTO.WerkorderDTO)
            {
                entity.Werkorder.Add(mapper.Map<Werkorder>(wo));
            }
            return entity;
        }

        //public ObservableCollection<Klant> ConvertFromDTO(ObservableCollection<KlantDTO> KlantDto)
        //{
        //    ObservableCollection<Klant> persons = new ObservableCollection<Klant>();
        //    foreach (KlantDTO dto in KlantDto)
        //    {
        //        persons.Add(ConvertFromDTO(dto));
        //    }

        //    return persons;
        //}

        public KlantDTO ConvertToDTO(Klant Klant)
        {
            KlantDTO dto = mapper.Map<KlantDTO>(Klant);
            foreach (Werkorder wo in Klant.Werkorder)
            {
                dto.WerkorderDTO.Add(mapper.Map<WerkorderDTO>(wo));
            }
            return dto;
        }

        public ObservableCollection<KlantDTO> ConvertToDTO(IEnumerable<Klant> Klants)
        {
            ObservableCollection<KlantDTO> KlantsDtos = new ObservableCollection<KlantDTO>();

            foreach (Klant c in Klants)
            {
                KlantsDtos.Add(ConvertToDTO(c));
            }

            return KlantsDtos;
        }
        public KlantDTO CopyDTO(KlantDTO klantToCopy)
        {
            KlantDTO dto = mapper.Map<KlantDTO>(klantToCopy);
            foreach (WerkorderDTO wo in klantToCopy.WerkorderDTO)
            {
                dto.WerkorderDTO.Add(mapper.Map<WerkorderDTO>(wo));
            }
            return dto;
        }
    }
}
