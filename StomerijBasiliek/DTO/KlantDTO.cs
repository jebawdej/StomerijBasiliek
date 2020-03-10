using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StomerijBasiliek.DTO
{
    public class KlantDTO : ObservableObject
    {
        public event EventHandler KlantChanged;

        public KlantDTO()
        {
            this.WerkorderDTO = new HashSet<WerkorderDTO>();
        }
        private Guid _id;
        public Guid Id
        {
            get
            {
                return _id;
            }
            set
            {
                Set<Guid>(() => this.Id, ref _id, value);
            }
        }

        private int _klantNummer;
        public int KlantNummer
        {
            get
            {
                return _klantNummer;
            }
            set
            {
                Set<int>(() => this.KlantNummer, ref _klantNummer, value);
                RaiseKlantChanged();
            }
        }

        private string _voornaam;
        public string Voornaam
        {
            get
            {
                return _voornaam;
            }
            set
            {
                Set<string>(() => this.Voornaam, ref _voornaam, value);
                RaiseKlantChanged();
            }
        }

        private string _voorvoegsel;
        public string Voorvoegsel
        {
            get
            {
                return _voorvoegsel;
            }
            set
            {
                Set<string>(() => this.Voorvoegsel, ref _voorvoegsel, value);
                RaiseKlantChanged();
            }
        }

        private string _achternaam;
        public string Achternaam
        {
            get
            {
                return _achternaam;
            }
            set
            {
                Set<string>(() => this.Achternaam, ref _achternaam, value);
                RaiseKlantChanged();
            }
        }

        private string _straat;
        public string Straat
        {
            get
            {
                return _straat;
            }
            set
            {
                Set<string>(() => this.Straat, ref _straat, value);
                RaiseKlantChanged();
            }
        }
        private string _huisnr;
        public string Huisnr
        {
            get
            {
                return _huisnr;
            }
            set
            {
                Set<string>(() => this.Huisnr, ref _huisnr, value);
                RaiseKlantChanged();
            }
        }

        private string _postCode;
        public string PostCode
        {
            get
            {
                return _postCode;
            }
            set
            {
                Set<string>(() => this.PostCode, ref _postCode, value);
                RaiseKlantChanged();
            }
        }

        private string _plaats;
        public string Plaats
        {
            get
            {
                return _plaats;
            }
            set
            {
                Set<string>(() => this.Plaats, ref _plaats, value);
                RaiseKlantChanged();
            }
        }

        private string _telefoonvast;
        public string Telefoonvast
        {
            get
            {
                return _telefoonvast;
            }
            set
            {
                Set<string>(() => this.Telefoonvast, ref _telefoonvast, value);
                RaiseKlantChanged();
            }
        }

        private string _telefoonMobiel;
        public string TelefoonMobiel
        {
            get
            {
                return _telefoonMobiel;
            }
            set
            {
                Set<string>(() => this.TelefoonMobiel, ref _telefoonMobiel, value);
                RaiseKlantChanged();
            }
        }

        private string _email;
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                Set<string>(() => this.Email, ref _email, value);
                RaiseKlantChanged();
            }
        }

        private int _voorkeur;
        public int Voorkeur
        {
            get
            {
                return _voorkeur;
            }
            set
            {
                Set<int>(() => this.Voorkeur, ref _voorkeur, value);
                RaiseKlantChanged();
            }
        }


        public ICollection<WerkorderDTO> WerkorderDTO { get; set; }

        private void RaiseKlantChanged()
        {
            KlantChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
