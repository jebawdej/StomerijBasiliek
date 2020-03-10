using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StomerijBasiliek.DTO
{
    public class WerkorderDTO : ObservableObject
    {
        public event EventHandler WerkorderChanged;

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
                RaiseWerkorderChanged();
            }
        }

        private Guid _klantID;
        public Guid KlantID
        {
            get
            {
                return _klantID;
            }
            set
            {
                Set<Guid>(() => this.KlantID, ref _klantID, value);
                RaiseWerkorderChanged();
            }
        }

        private string _bon;
        public string Bon
        {
            get
            {
                return _bon;
            }
            set
            {
                Set<string>(() => this.Bon, ref _bon, value);
                RaiseWerkorderChanged();
            }
        }

        private int _werkzaamheden;
        public int Werkzaamheden
        {
            get
            {
                return _werkzaamheden;
            }
            set
            {
                Set<int>(() => this.Werkzaamheden, ref _werkzaamheden, value);
                RaiseWerkorderChanged();
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
                RaiseWerkorderChanged();
            }
        }

        private int _status;
        public int Status
        {
            get
            {
                return _status;
            }
            set
            {
                Set<int>(() => this.Status, ref _status, value);
                RaiseWerkorderChanged();
            }
        }

        private decimal? _prijs;
        public decimal? Prijs
        {
            get
            {
                return _prijs;
            }
            set
            {
                Set<decimal?>(() => this.Prijs, ref _prijs, value);
                RaiseWerkorderChanged();
            }
        }

        private string _commentaar;
        public string Commentaar
        {
            get
            {
                return _commentaar;
            }
            set
            {
                Set<string>(() => this.Commentaar, ref _commentaar, value);
                RaiseWerkorderChanged();
            }
        }

        private DateTime? _datumStart;
        public DateTime? DatumStart
        {
            get
            {
                return _datumStart;
            }
            set
            {
                Set<DateTime?>(() => this.DatumStart, ref _datumStart, value);
            }
        }

        private DateTime? _datumLaatstAangepast;
        public DateTime? DatumLaatstAangepast
        {
            get
            {
                return _datumLaatstAangepast;
            }
            set
            {
                Set<DateTime?>(() => this.DatumLaatstAangepast, ref _datumLaatstAangepast, value);
            }
        }
        public virtual KlantDTO KlantDTO { get; set; }

        private void RaiseWerkorderChanged()
        {
            WerkorderChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
