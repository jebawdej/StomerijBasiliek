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
using System.Windows.Input;

namespace StomerijBasiliek.ViewModel
{
    public class WerkOrdersViewModel : ViewModelBase
	{
		private IFrameNavigationService _navigationService;
		private IWerkorderDataService _dataSrv;
		private bool _newKlant = false;

		public RelayCommand SearchCommand { get; set; }
		public RelayCommand CancelCommand { get; set; }
		public RelayCommand UpdateCommand { get; set; }
		public RelayCommand NewCommand { get; set; }
		public WerkOrdersViewModel(IWerkorderDataService dataSrv, IFrameNavigationService navigationService)
		{
			Caption = "WerkOrders";
			_navigationService = navigationService;
			if (IsInDesignMode)
			{
				// Code runs in Blend --> create design time data.
			}
			else
			{
				_dataSrv = dataSrv;
				Werkorders = _dataSrv.GetAll();

				CancelCommand = new RelayCommand(() => ExecuteCancelWerkorder(), () => CanCancelWerkorder);
				UpdateCommand = new RelayCommand(() => ExecuteUpdateWerkorder(), () => CanUpdateWerkorder);
				SearchCommand = new RelayCommand(() => ExecuteSearchWerkorder(), () => CanSearchWerkorder);
				NewCommand    = new RelayCommand(() => ExecuteNewWerkorder   (), () => CanNewWerkorder);

				if(Werkorders.Count > 0)
					SelectedWerkorder = Werkorders[Werkorders.Count-1];

			}
		}

		private bool _canNewWerkorder;
		public bool CanNewWerkorder
		{
			get
			{
				return _canNewWerkorder;
			}
			set
			{
				if (value != _canNewWerkorder)
				{
					_canNewWerkorder = value;
					RaisePropertyChanged("CanNewWerkorder");
				}
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
			}
		}

		private void ExecuteNewWerkorder()
		{
			_newKlant = true;
			
			SelectedWerkorder = new WerkorderDTO();
			//SelectedWerkorder.Voor = (int)_dataSrv.MaxKlantNr() + 1;

			CanUpdateWerkorder = CanCancelWerkorder = true;
			CanNewWerkorder = CanSearchWerkorder = false;
		}

		private void ExecuteSearchWerkorder()
		{
			CanUpdateWerkorder = CanCancelWerkorder = false;
			CanNewWerkorder = CanSearchWerkorder = true;

		}

		private void ExecuteUpdateWerkorder()
		{

			CanUpdateWerkorder = CanCancelWerkorder = false;
			CanNewWerkorder = CanSearchWerkorder = true;

		}

		private void ExecuteCancelWerkorder()
		{
			Werkorders = _dataSrv.GetAll();
			if (Werkorders.Count > 0)
				SelectedWerkorder = Werkorders[Werkorders.Count-1];
			CanUpdateWerkorder = CanCancelWerkorder = false;
			CanNewWerkorder = CanSearchWerkorder = true;
			_newKlant = false;
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
		private object canCancelWerkorder;

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
				CanCancelWerkorder = false;
				CanUpdateWerkorder = false;
				CanNewWerkorder = true;
				CanSearchWerkorder = true;
				SelectedWerkorder.WerkorderChanged -= WerkorderChanged;
				SelectedWerkorder.WerkorderChanged += WerkorderChanged;
			}
		}

		private void WerkorderChanged(object sender, EventArgs e)
		{
			CanUpdateWerkorder = CanCancelWerkorder= true;
			CanNewWerkorder = CanSearchWerkorder = false;
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
			}
			else
			{
				Debug.WriteLine("Werkorders page loaded, no param");
			}
		}

	}
}
