using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using StomerijBasiliek.Common;
using StomerijBasiliek.DTO;
using StomerijBasiliek.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace StomerijBasiliek.ViewModel
{
    public class WerkOrdersViewModel : ViewModelBase
	{
        readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private IFrameNavigationService _navigationService;
		private IWerkorderDataService _dataSrvWO;
		private IKlantDataService _dataSrvKlant;
		private bool _newKlant = false;

		public RelayCommand SearchCommand { get; set; }
		public RelayCommand CancelCommand { get; set; }
		public RelayCommand UpdateCommand { get; set; }
		public RelayCommand DeleteCommand { get; set; }
		public WerkOrdersViewModel(IWerkorderDataService dataSrvWo, IKlantDataService dataSrvKlant, IFrameNavigationService navigationService)
		{
			Caption = "WerkOrders";
			_navigationService = navigationService;
			if (IsInDesignMode)
			{
				// Code runs in Blend --> create design time data.
			}
			else
			{
				_dataSrvWO = dataSrvWo;
				_dataSrvKlant = dataSrvKlant;
				Werkorders = _dataSrvWO.GetAll();

				CancelCommand = new RelayCommand(() => ExecuteCancelWerkorder(), () => CanCancelWerkorder);
				UpdateCommand = new RelayCommand(() => ExecuteUpdateWerkorder(), () => CanUpdateWerkorder);
				SearchCommand = new RelayCommand(() => ExecuteSearchWerkorder(), () => CanSearchWerkorder);
				DeleteCommand    = new RelayCommand(() => ExecuteDeleteWerkorder(), () => CanDeleteWerkorder);
                if(Werkorders!=null)
                {
                    if (Werkorders.Count > 0)
                        SelectedWerkorder = Werkorders[Werkorders.Count - 1];
                }
                else
                {
                    log.Error("Kan Werkorder database niet bereiken");
                    MessageBox.Show("Kan Werkorder database niet bereiken", "ERROR");
                }
            }
		}

		private bool _canDeleteWerkorder;
		public bool CanDeleteWerkorder
		{
			get
			{
				return _canDeleteWerkorder;
			}
			set
			{
				if (value != _canDeleteWerkorder)
				{
					_canDeleteWerkorder = value;
					RaisePropertyChanged("CanDeleteWerkorder");
				}
				DeleteCommand.RaiseCanExecuteChanged();
			}
		}

		private bool _canSearchWerkorder;
		public bool CanSearchWerkorder
		{
			get
			{
				return _canSearchWerkorder;
			}
			set
			{
				if (value != _canSearchWerkorder)
				{
					_canSearchWerkorder = value;
					RaisePropertyChanged("CanSearchWerkorder");
				}
				SearchCommand.RaiseCanExecuteChanged();
			}
		}

		private bool _canUpdateWerkorder;
		public bool CanUpdateWerkorder
		{
			get
			{
				return _canUpdateWerkorder;
			}
			set
			{
				if (value != _canUpdateWerkorder)
				{
					_canUpdateWerkorder = value;
					RaisePropertyChanged("CanUpdateWerkorder");
				}
				UpdateCommand.RaiseCanExecuteChanged();
			}
		}


		private bool _canCancelWerkorder;
		public bool CanCancelWerkorder
		{
			get
			{
				return _canCancelWerkorder;
			}
			set
			{
				if (value != _canCancelWerkorder)
				{
					_canCancelWerkorder = value;
					RaisePropertyChanged("canCancelWerkorder");
				}
				CancelCommand.RaiseCanExecuteChanged();
			}
		}

		private void ExecuteNewWerkorder()
		{
			_newKlant = true;
			
			SelectedWerkorder = new WerkorderDTO();
			CurrentDateTime = DateTime.Now;
			SelectedWerkorder.DatumStart = DateTime.Now;
			SelectedWerkorder.DatumLaatstAangepast = DateTime.Now;
			SelectedWerkorder.DatumGereed = DateTime.Now.AddDays(1);
			SelectedWerkorder.Status = 0;

			CanUpdateWerkorder = CanCancelWerkorder = true;
			CanDeleteWerkorder = CanSearchWerkorder = false;
		}

		private void ExecuteSearchWerkorder()
		{
			CanUpdateWerkorder = CanCancelWerkorder = false;
			CanDeleteWerkorder = CanSearchWerkorder = true;

		}

		private void ExecuteUpdateWerkorder()
		{
			if (SelectedWerkorder.Id == Guid.Empty)
			{
				SelectedWerkorder.Id = Guid.NewGuid();
				if (_dataSrvWO.Add(SelectedWerkorder))
				{
					Werkorders = _dataSrvWO.GetAll();
					if (Werkorders.Count > 0)
						SelectedWerkorder = Werkorders[Werkorders.Count - 1];
					CanDeleteWerkorder = CanSearchWerkorder = true;
					CanUpdateWerkorder = CanCancelWerkorder = false;
				}
				else
				{
					SelectedWerkorder.Id = Guid.Empty;
					System.Windows.MessageBox.Show("Nieuwe order toevoegen mislukt, controleer alle velden en probeer opnieuw", "Error");
				}
			}
			else
			{
				if (_dataSrvWO.Update(SelectedWerkorder))
				{
					if (Werkorders.Count > 0)
						SelectedWerkorder = _dataSrvWO.Get(SelectedWerkorder.Id);
					CanDeleteWerkorder = CanSearchWerkorder = true;
					CanUpdateWerkorder = CanCancelWerkorder = false;
					_newKlant = false;
				}
				else
				{
					System.Windows.MessageBox.Show("Werkorder aanpassen mislukt, controleer alle velden en probeer opnieuw", "Error");
				}
			}
		}

		private void ExecuteCancelWerkorder()
		{
			Werkorders = _dataSrvWO.GetAll();
			if (Werkorders.Count > 0)
				SelectedWerkorder = Werkorders[Werkorders.Count-1];
			CanUpdateWerkorder = CanCancelWerkorder = false;
			CanDeleteWerkorder = CanSearchWerkorder = true;
			_newKlant = false;
		}
		
		private void ExecuteDeleteWerkorder()
		{
			if(SelectedWerkorder != null)
			{
				string text = String.Format("Weet je zeker dat je werkorder met bon nummer {0} van klant met nummer {1} , wilt verwijderen?", SelectedWerkorder.Bon, SelectedWerkorder.KlantDTO.KlantNummer);
				if(MessageBox.Show(text, "Waarschuwing",MessageBoxButton.YesNo) == MessageBoxResult.Yes)
				{
					if(!_dataSrvWO.Delete(SelectedWerkorder.Id))
					{
						MessageBox.Show("Kan werkorder niet verwijderen, probeer het nogmaals", "Error");
					}
					else
					{
						Werkorders = _dataSrvWO.GetAll();
						if (Werkorders.Count > 0)
							SelectedWerkorder = Werkorders[Werkorders.Count - 1];
					}
				}
			}

			CanDeleteWerkorder = CanSearchWerkorder = true;
			CanUpdateWerkorder = CanCancelWerkorder = false;
		}

		private DateTime _currentDateTime;
		public DateTime CurrentDateTime
		{
			get
			{
				return _currentDateTime;
			}
			set
			{
				if (value != _currentDateTime)
				{
					_currentDateTime = value;
					RaisePropertyChanged("CurrentDateTime");
				}
			}
		}

		private string _caption;
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

		private ObservableCollection<WerkorderDTO> _werkorders;
		public ObservableCollection<WerkorderDTO> Werkorders
		{
			get
			{
				return _werkorders;
			}
			set
			{
				if (value != _werkorders)
				{
					_werkorders = value;
					RaisePropertyChanged("Werkorders");
				}
			}
		}

		private WerkorderDTO _selectedWerkorder;
		public WerkorderDTO SelectedWerkorder
		{
			get
			{
				return _selectedWerkorder;
			}
			set
			{
				if (value != _selectedWerkorder)
				{
					_selectedWerkorder = value;
					RaisePropertyChanged("SelectedWerkorder");
				}
				CanCancelWerkorder = CanUpdateWerkorder = false;
				CanDeleteWerkorder = true;
				CanSearchWerkorder = true;
				SelectedWerkorder.WerkorderChanged -= WerkorderChanged;
				SelectedWerkorder.WerkorderChanged += WerkorderChanged;
			}
		}

		private void WerkorderChanged(object sender, EventArgs e)
		{
			//if((SelectedWerkorder.DatumTijdGereed.Value.Hour < 9) || (SelectedWerkorder.DatumTijdGereed.Value.Hour > 17))
			//{
			//	GereedNaTijdIndex = 3;
			//}
			//else
			//{
			//	GereedNaTijdIndex = SelectedWerkorder.DatumTijdGereed.Value.Hour - 9;
			//}
			CanUpdateWerkorder = CanCancelWerkorder= true;
			CanDeleteWerkorder = CanSearchWerkorder = false;
		}

		public ICommand OnLoadedCommand
		{
			get
			{
				return new RelayCommand(() => OnLoadedPage());
			}
		}

		private void OnLoadedPage()
		{
			if (_navigationService.Parameter != null)
			{
				Debug.WriteLine("Werkorders page loaded, param= {0}", _navigationService.Parameter);
				KlantDTO klant = _dataSrvKlant.Get((Guid)_navigationService.Parameter);
				if (klant != null)
				{
					ExecuteNewWerkorder();
					SelectedWerkorder.Voorkeur = klant.Voorkeur;
					SelectedWerkorder.KlantID = (Guid)_navigationService.Parameter;
					SelectedWerkorder.KlantDTO = klant;
				}
			}
			else
			{
				Debug.WriteLine("Werkorders page loaded, no param");
			}
		}

	}
}
