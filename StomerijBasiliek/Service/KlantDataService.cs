using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using StomerijBasiliek.DTO;
using System.Linq;

namespace StomerijBasiliek.Service
{
    public class KlantDataService : IKlantDataService
    {
        readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private StomerijBasiliekEntities _context;
        private KlantMapper _klantMapper;
        public KlantDataService()
        {
            _context = new StomerijBasiliekEntities();
            _klantMapper = new KlantMapper();
        }
        public bool Add(KlantDTO klant)
        {
            bool retVal = false;
            try
            {
                Klant k = _klantMapper.ConvertFromDTO(klant);
                k.Id = Guid.NewGuid();
                _context.Klant.Add(k);
                _context.SaveChanges();
                log.InfoFormat("Nieuwe klant met klantnummer {0}, toegevoegd", k.KlantNummer);
                log.DebugFormat("\r\nNaam: {0} {1} {2}\r\nAdres: {3} {4} {5} {6}\r\nContact: V:{7} M:{8} E:{9}", k.Voornaam, k.Voorvoegsel, k.Achternaam, k.Straat, k.Huisnr, k.PostCode, k.Plaats, k.Telefoonvast, k.TelefoonMobiel, k.Email);
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
                Klant k = _context.Klant.FirstOrDefault(klant => klant.Id == id);
                if(k != null)
                {
                    _context.Klant.Remove(k);
                    _context.SaveChanges();
                    log.InfoFormat("Klant met klantnummer {0}, verwijderd", k.KlantNummer);
                }
                else
                {
                    log.ErrorFormat("Klant met id={0}, niet gevonden", id);
                }
                log.DebugFormat("\r\nNaam: {0} {1} {2}\r\nAdres: {3} {4} {5} {6}\r\nContact: V:{7} M:{8} E:{9}", k.Voornaam, k.Voorvoegsel, k.Achternaam, k.Straat, k.Huisnr, k.PostCode, k.Plaats, k.Telefoonvast, k.TelefoonMobiel, k.Email);
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

        public KlantDTO Get(Guid id)
        {
            KlantDTO klant = null;
            try
            {
                Klant k = _context.Klant.FirstOrDefault(kl => kl.Id == id);
                if (k != null)
                {
                    log.InfoFormat("Klant met klantnummer {0}, verwijderd", k.KlantNummer);
                    klant = _klantMapper.ConvertToDTO(k);
                }
                else
                {
                    log.ErrorFormat("Klant met id={0}, niet gevonden", id);
                }
                log.DebugFormat("\r\nNaam: {0} {1} {2}\r\nAdres: {3} {4} {5} {6}\r\nContact: V:{7} M:{8} E:{9}", k.Voornaam, k.Voorvoegsel, k.Achternaam, k.Straat, k.Huisnr, k.PostCode, k.Plaats, k.Telefoonvast, k.TelefoonMobiel, k.Email);
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                if (e.InnerException != null)
                {
                    log.Error(e.InnerException);
                }
            }
            return klant;
        }

        public ObservableCollection<KlantDTO> GetAll()
        {
            ObservableCollection<KlantDTO> klanten = null;
            try
            {
                klanten = _klantMapper.ConvertToDTO(_context.Klant);
                if (klanten != null)
                {
                    log.Info("Klanten opgehaald");
                }
                else
                {
                    log.ErrorFormat("Geen klanten gevonden gevonden");
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
            return klanten;
        }

        public int MaxKlantNr()
        {
            int max = 0;
            try
            {
                max = _context.Klant.Select(k => k.KlantNummer).DefaultIfEmpty(0).Max();
            }
            catch(Exception e)
            {
                log.Error(e.Message);
                if (e.InnerException != null)
                {
                    log.Error(e.InnerException);
                }
            }
            return max;
        }

        public bool Update(KlantDTO klant)
        {
            bool retVal = false;
            try
            {
                Klant k = _klantMapper.ConvertFromDTO(klant);
                var entity = _context.Klant.Find(klant.Id);
                //_context.Klant.Add(k);
                //_context.Entry(k).State = System.Data.Entity.EntityState.Modified;
                _context.Entry(entity).CurrentValues.SetValues(k);
                _context.SaveChanges();
                log.InfoFormat("Klant met klantnummer: {0} aangepast",k.KlantNummer);
                log.DebugFormat("\r\nNaam: {0} {1} {2}\r\nAdres: {3} {4} {5} {6}\r\nContact: V:{7} M:{8} E:{9}", k.Voornaam, k.Voorvoegsel, k.Achternaam, k.Straat, k.Huisnr, k.PostCode, k.Plaats, k.Telefoonvast, k.TelefoonMobiel, k.Email);
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

        ObservableCollection<KlantDTO> IKlantDataService.Search(string postCode, string huisNr)
        {
            IEnumerable<Klant> klanten = null;
            if (!String.IsNullOrEmpty(postCode))
            {
                if (String.IsNullOrEmpty(huisNr))
                {
                    klanten = _context.Klant.Where(k => k.PostCode.Replace(" ","").ToLower().Contains(postCode.Replace(" ", "").ToLower()));
                }
                else
                {
                    klanten = _context.Klant.Where(k => k.PostCode == postCode && k.Huisnr == huisNr);
                }
            }
            else
            {
                klanten = _context.Klant;
            }
            return _klantMapper.ConvertToDTO(klanten);
        }
    }
}
