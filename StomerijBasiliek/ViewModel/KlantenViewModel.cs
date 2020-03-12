using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using StomerijBasiliek.Service;
using StomerijBasiliek.DTO;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StomerijBasiliek.Common;
using System.Windows.Navigation;
using System.Windows;
using System.Windows.Input;

namespace StomerijBasiliek.ViewModel
{
    public class KlantenViewModel : ViewModelBase
    {
        readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private IKlantDataService _dataSrv;
        private IFrameNavigationService _navigationService;
        private string _caption;
        private bool _newKlant = false;
        public RelayCommand SearchCommand { get; set; }
        public RelayCommand CancelCommand { get; set; }
        public RelayCommand UpdateCommand { get; set; }
        public RelayCommand NewCommand { get; set; }
        public KlantenViewModel(IKlantDataService datasrv, IFrameNavigationService navigationService) 
		{
            _dataSrv = datasrv;
            _navigationService = navigationService;
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.FileVersion;

            Caption = String.Format("Stomerij App- V{0}", version);
            if (IsInDesignMode)
            {
                // Code runs in Blend --> create design time data.
            }
            else
            {
                Klanten = _dataSrv.GetAll();

                CancelCommand = new RelayCommand(() => ExecuteCancelKlant(), () => CanCancelKlant);
                UpdateCommand = new RelayCommand(() => ExecuteUpdateKlant(), () => CanUpdateKlant);
                SearchCommand = new RelayCommand(() => ExecuteSearchKlant(), () => CanSearchKlant);
                NewCommand = new RelayCommand(() => ExecuteNewKlant(), () => CanNewKlant);

                SelectedKlant = Klanten.FirstOrDefault(k => k.KlantNummer == _dataSrv.MaxKlantNr());

            }
		}

        public string Caption
        {
            get
            {
                return _caption;
            }
            set
            {
                if (value != _caption)
                {
                    _caption = value;
                    RaisePropertyChanged("Caption");
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
 
        private void KlantChanged(object sender, EventArgs e)
        {
            CanUpdateKlant = CanCancelKlant = true;
            CanNewKlant = CanSearchKlant = false;
        }

        private void ExecuteNewKlant()
        {
            _newKlant = true;
            SelectedKlant = new KlantDTO();
            SelectedKlant.KlantNummer = (int)_dataSrv.MaxKlantNr() + 1;
            CanUpdateKlant = CanCancelKlant = true;
            CanNewKlant = CanSearchKlant = false;
        }

        private void ExecuteSearchKlant()
        {
            CanNewKlant = CanSearchKlant = true;
            CanUpdateKlant = CanCancelKlant = false;

            Klanten = _dataSrv.Search(ZoekPostCode, ZoekHuisNr);
            if (Klanten.Count > 0)
                SelectedKlant = Klanten[0];
            ZoekHuisNr = "";
            ZoekPostCode = "";
        }
        private void ExecuteUpdateKlant()
        {
            if (_newKlant)
            {
                if (_dataSrv.Add(SelectedKlant))
                {
                    Klanten = _dataSrv.GetAll();
                    if (Klanten.Count > 0)
                        SelectedKlant = Klanten[Klanten.Count - 1];
                    CanNewKlant = CanSearchKlant = true;
                    CanUpdateKlant = CanCancelKlant = false;
                    _newKlant = false;
                }
                else
                {
                    System.Windows.MessageBox.Show("Nieuwe klant toevoegen mislukt, controleer alle velden en probeer opnieuw", "Error");
                }
            }
            else
            {
                if (_dataSrv.Update(SelectedKlant))
                {
                    if (Klanten.Count > 0)
                        SelectedKlant = _dataSrv.Get(SelectedKlant.Id);
                    CanNewKlant = CanSearchKlant = true;
                    CanUpdateKlant = CanCancelKlant = false;
                    _newKlant = false;
                }
                else
                {
                    System.Windows.MessageBox.Show("Klant aanpassen mislukt, controleer alle velden en probeer opnieuw", "Error");
                }
            }
        }

        private void ExecuteCancelKlant()
        {
            Klanten = _dataSrv.GetAll();
            if (Klanten.Count > 0)
                SelectedKlant = Klanten[0];
            CanNewKlant = CanSearchKlant = true;
            CanUpdateKlant = CanCancelKlant = false;
            _newKlant = false;
        }
        private bool _canNewKlant;
        public bool CanNewKlant
        {
            get
            {
                return _canNewKlant;
            }
            set
            {
                if (value != _canNewKlant)
                {
                    _canNewKlant = value;
                    RaisePropertyChanged("CanNewKlant");
                }
                NewCommand.RaiseCanExecuteChanged();
            }
        }

        private bool _canSearchKlant;
        public bool CanSearchKlant
        {
            get
            {
                return _canSearchKlant;
            }
            set
            {
                if (value != _canSearchKlant)
                {
                    _canSearchKlant = value;
                    RaisePropertyChanged("CanSearchKlant");
                }
                SearchCommand.RaiseCanExecuteChanged();
            }
        }

        private bool _canUpdateKlant;
        public bool CanUpdateKlant
        {
            get
            {
                return _canUpdateKlant;
            }
            set
            {
                if (value != _canUpdateKlant)
                {
                    _canUpdateKlant = value;
                    RaisePropertyChanged("CanUpdateKlant");
                }
                UpdateCommand.RaiseCanExecuteChanged();
            }
        }

        private bool _canCancelKlant;
        public bool CanCancelKlant
        {
            get
            {
                return _canCancelKlant;
            }
            set
            {
                if (value != _canCancelKlant)
                {
                    _canCancelKlant = value;
                    RaisePropertyChanged("CanCancelKlant");
                }

                CancelCommand.RaiseCanExecuteChanged();
            }
        }
        private string _zoekHuisNr;
        public string ZoekHuisNr
        {
            get
            {
                return _zoekHuisNr;
            }
            set
            {
                if (value != _zoekHuisNr)
                {
                    _zoekHuisNr = value;
                    RaisePropertyChanged("ZoekHuisNr");
                }
            }
        }

        private string _zoekPostCode;
        public string ZoekPostCode
        {
            get
            {
                return _zoekPostCode;
            }
            set
            {
                if (value != _zoekPostCode)
                {
                    _zoekPostCode = value;
                    RaisePropertyChanged("ZoekPostCode");
                }
            }
        }

        private ObservableCollection<KlantDTO> _klanten;
        public ObservableCollection<KlantDTO> Klanten
        {
            get
            {
                return _klanten;
            }
            set
            {
                if (value != _klanten)
                {
                    _klanten = value;
                    RaisePropertyChanged("Klanten");
                }
            }
        }

        private KlantDTO _selectedKlant;
        public KlantDTO SelectedKlant
        {
            get
            {
                return _selectedKlant;
            }
            set
            {
                if (value != _selectedKlant)
                {
                    //_previousKlant = kMapper.CopyDTO(value);
                    _selectedKlant = value;
                    RaisePropertyChanged("SelectedKlant");
                }

                CanCancelKlant = false;
                CanUpdateKlant = false;
                CanNewKlant = true;
                CanSearchKlant = true;
                SelectedKlant.KlantChanged -= KlantChanged;
                SelectedKlant.KlantChanged += KlantChanged;
                CancelCommand.RaiseCanExecuteChanged();
            }
        }

        public object canExecuteGetVehicleData { get; private set; }

        public ICommand OnLoadedCommand
        {
            get
            {
                return new RelayCommand(() => OnLoadedPage());
            }
        }

        private void OnLoadedPage()
        {
            if(_navigationService.Parameter != null)
            {
                Debug.WriteLine("Klanten page loaded, param= {0}", _navigationService.Parameter);
            }
            else
            {
                Debug.WriteLine("Klanten page loaded, no param");
            }
            
        }
    }
}
