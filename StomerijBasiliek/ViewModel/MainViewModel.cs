using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using StomerijBasiliek.Common;
using StomerijBasiliek.ViewModels.Common;
using System;
using System.Diagnostics;
using System.Timers;
using System.Windows.Input;

namespace StomerijBasiliek.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private IFrameNavigationService _navigationService;
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IFrameNavigationService fns)
        {
            MainTitle = "Stomerij de Basiliek";
            _navigationService = fns;
        }

        public MyCommand GotoHomeCommand
        {
            get { return new MyCommand(x => _navigationService.Clear()); }
        }
        public MyCommand GotoLoginCommand
        {
            
            get { return new MyCommand(x => _navigationService.NavigateTo("LoginView")); }
        }
        public MyCommand GotoNotesCommand
        {
            get { return new MyCommand(x => _navigationService.NavigateTo("NotesView")); }
        }
        public MyCommand GotoKlantenCommand
        {
            get { return new MyCommand(x => _navigationService.NavigateTo("KlantenView")); }
        }
        public MyCommand GotoWerkordersCommand
        {
            get { return new MyCommand(x => _navigationService.NavigateTo("WerkordersView")); }
        }

        private string _mainTitle;
        public string MainTitle
        {
            get
            {
                return _mainTitle;
            }
            set
            {
                _mainTitle = value;
                RaisePropertyChanged(() => MainTitle);
            }
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
                Debug.WriteLine("Main page loaded, param= {0}", _navigationService.Parameter);
            }
            else
            {
                Debug.WriteLine("Main page loaded, no param");
            }
            _navigationService.NavigateTo("KlantenView");
        }
    }
}