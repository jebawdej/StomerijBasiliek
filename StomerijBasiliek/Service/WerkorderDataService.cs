using StomerijBasiliek.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StomerijBasiliek.Service;

namespace StomerijBasiliek.Service
{
    public class WerkorderDataService : IWerkorderDataService
    {
        readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private StomerijBasiliekEntities _context;
        private WerkorderMapper _werkorderMapper;
        private KlantMapper _klantMapper;
        public WerkorderDataService()
        {
            _context = new StomerijBasiliekEntities();
            _werkorderMapper = new WerkorderMapper();
            _klantMapper = new KlantMapper();
        }
        public bool Add(WerkorderDTO werkorder)
        {
            bool retVal = false;
            try
            {
                Werkorder w  = _werkorderMapper.ConvertFromDTO(werkorder);
                w.Id = Guid.NewGuid();
                _context.Werkorder.Add(w);
                _context.SaveChanges();
                log.InfoFormat("Nieuw werkorder met bonnummer {0}, toegevoegd", w.Bon);
                //log.DebugFormat("\r\nNaam: {0} {1} {2}\r\nAdres: {3} {4} {5} {6}\r\nContact: V:{7} M:{8} E:{9}", k.Voornaam, k.Voorvoegsel, k.Achternaam, k.Straat, k.Huisnr, k.PostCode, k.Plaats, k.Telefoonvast, k.TelefoonMobiel, k.Email);
                retVal = true;
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                if (e.InnerException != null)
                {
                    log.Error(e.InnerException);
                }
            }
            return retVal;
        }

        public bool Delete(Guid id)
        {
            bool retVal = false;
            try
            {
                Werkorder w = _context.Werkorder.FirstOrDefault(wo => wo.Id == id);
                if (w != null)
                {
                    _context.Werkorder.Remove(w);
                    _context.SaveChanges();
                    log.InfoFormat("Werkorder met bon {0} van KlantID {1}, verwijderd", w.Bon, w.KlantID);
                    retVal = true;
                }
                else
                {
                    log.ErrorFormat("Werkorder met id={0}, niet gevonden", id);
                }
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                if (e.InnerException != null)
                {
                    log.Error(e.InnerException);
                }
            }
            return retVal;
        }

        public WerkorderDTO Get(Guid id)
        {
            WerkorderDTO werkorder = null;
            try
            {
                Werkorder w = _context.Werkorder.FirstOrDefault(wo => wo.Id == id);
                if (w != null)
                {
                    log.InfoFormat("Werkorder met bon nummer {0}, verwijderd", w.Bon);
                    werkorder = _werkorderMapper.ConvertToDTO(w);
                }
                else
                {
                    log.ErrorFormat("Werkorder met id={0}, niet gevonden", id);
                }
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                if (e.InnerException != null)
                {
                    log.Error(e.InnerException);
                }
            }
            return werkorder;

        }

        public ObservableCollection<WerkorderDTO> GetAll()
        {
            ObservableCollection<WerkorderDTO> werkorders = null;
            try
            {
                werkorders = _werkorderMapper.ConvertToDTO(_context.Werkorder.Include("Klant"));
                if (werkorders != null)
                {
                    log.Info("Werkorders opgehaald");
                }
                else
                {
                    log.ErrorFormat("Geen werkorders gevonden");
                }

            }
            catch (Exception e)
            {
                log.Error(e.Message);
                if (e.InnerException != null)
                {
                    log.Error(e.InnerException);
                }
            }
            return werkorders;
        }


        public WerkorderDTO Search(string BonNr)
        {
            Werkorder w = null;

            w = _context.Werkorder.FirstOrDefault(wo => wo.Bon == BonNr);
            return _werkorderMapper.ConvertToDTO(w);
        }

        public bool Update(WerkorderDTO werkorder)
        {
            bool retVal = false;
            try
            {
                Werkorder w = _werkorderMapper.ConvertFromDTO(werkorder);
                w.DatumLaatstAangepast = DateTime.Now;
                var entity = _context.Werkorder.Find(werkorder.Id);

                _context.Entry(entity).CurrentValues.SetValues(w);
                _context.SaveChanges();
                log.InfoFormat("Werkorder met Bon: {0} aangepast", w.Bon);
                //log.DebugFormat("\r\nNaam: {0} {1} {2}\r\nAdres: {3} {4} {5} {6}\r\nContact: V:{7} M:{8} E:{9}", k.Voornaam, k.Voorvoegsel, k.Achternaam, k.Straat, k.Huisnr, k.PostCode, k.Plaats, k.Telefoonvast, k.TelefoonMobiel, k.Email);
                retVal = true;
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                if (e.InnerException != null)
                {
                    log.Error(e.InnerException);
                }
            }
            return retVal;
        }

        public DateTime? LaatstToegevoegd()
        {
            DateTime? max = null;
            try
            {
                max = _context.Werkorder.OrderByDescending(w => w.DatumStart).FirstOrDefault().DatumStart;

            }
            catch (Exception e)
            {
                log.Error(e.Message);
                if (e.InnerException != null)
                {
                    log.Error(e.InnerException);
                }
            }
            return max;
        }

        public Guid LastKlantId()
        {
            int max = _context.Klant.Select(k => k.KlantNummer).DefaultIfEmpty(0).Max();
            Klant kl = (Klant)_context.Klant.Where(k => k.KlantNummer == max).FirstOrDefault();
            return kl.Id;
        }

    }
}
