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
    public class WerkorderMapper
    {
        IMapper _mapper;
        public WerkorderMapper()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Werkorder, WerkorderDTO>();
                cfg.CreateMap<WerkorderDTO, Werkorder>();
                cfg.CreateMap<WerkorderDTO, WerkorderDTO>();
                cfg.CreateMap<Klant, KlantDTO>();
            });
            _mapper = config.CreateMapper();
        }

        public Werkorder ConvertFromDTO(WerkorderDTO WerkorderDto)
        {
            return _mapper.Map<Werkorder>(WerkorderDto);
        }

        //public ObservableCollection<Werkorder> ConvertFromDTO(ObservableCollection<WerkorderDTO> WerkorderDto)
        //{
        //    ObservableCollection<Werkorder> Werkorders = new ObservableCollection<Werkorder>();
        //    foreach (WerkorderDTO dto in WerkorderDto)
        //    {
        //        Werkorders.Add(ConvertFromDTO(dto));
        //    }
        //    return Werkorders;
        //}

        public WerkorderDTO ConvertToDTO(Werkorder Werkorder)
        {
            WerkorderDTO wdto = _mapper.Map<WerkorderDTO>(Werkorder);
            wdto.KlantDTO = _mapper.Map<KlantDTO>(Werkorder.Klant);
            return wdto;
        }
        public ObservableCollection<WerkorderDTO> ConvertToDTO(IQueryable<Werkorder> Werkorders)
        {
            ObservableCollection<WerkorderDTO> WerkorderDtos = new ObservableCollection<WerkorderDTO>();

            foreach (Werkorder w in Werkorders)
            {
                WerkorderDtos.Add(ConvertToDTO(w));
            }

            return WerkorderDtos;
        }
        public WerkorderDTO CopyDTO(WerkorderDTO werkorder2copy)
        {
            return _mapper.Map<WerkorderDTO>(werkorder2copy);
        }
    }
}
