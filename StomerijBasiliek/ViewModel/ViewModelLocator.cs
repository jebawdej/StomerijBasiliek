/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:StomerijBasiliek"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using StomerijBasiliek.Common;
using StomerijBasiliek.Service;
using StomerijBasiliek.ViewModel;
using System;


namespace StomerijBasiliek.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SetupNavigation();

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<LoginViewModel>();
            SimpleIoc.Default.Register<NoteViewModel>();
            SimpleIoc.Default.Register<KlantenViewModel>();
            SimpleIoc.Default.Register<WerkOrdersViewModel>();

            if (ViewModelBase.IsInDesignModeStatic)
            {
                // Create design time view services and models
                //SimpleIoc.Default.Register<IDataService, DesignDataService>();
            }
            else
            {
                // Create run time view services and models
                SimpleIoc.Default.Register<IKlantDataService, KlantDataService>();
                SimpleIoc.Default.Register<IWerkorderDataService, WerkorderDataService>();
            }
        }
        private static void SetupNavigation()
        {
            var navigationService = new FrameNavigationService();

            navigationService.Configure("LoginView",      new Uri("../Views/LoginView.xaml",  UriKind.Relative));
            navigationService.Configure("NotesView",      new Uri("../Views/NotesView.xaml",  UriKind.Relative));
            navigationService.Configure("KlantenView",    new Uri("../Views/Klanten.xaml",    UriKind.Relative));
            navigationService.Configure("WerkordersView", new Uri("../Views/WerkOrders.xaml", UriKind.Relative));

            SimpleIoc.Default.Unregister<IFrameNavigationService>();
            SimpleIoc.Default.Register<IFrameNavigationService>(() => navigationService);
        }
        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }
        public LoginViewModel Login
        {
            get
            {
                return ServiceLocator.Current.GetInstance<LoginViewModel>();
            }
        }

        public NoteViewModel Notes
        {
            get
            {
                return ServiceLocator.Current.GetInstance<NoteViewModel>();
            }
        }
        public KlantenViewModel Klanten
        {
            get
            {
                return ServiceLocator.Current.GetInstance<KlantenViewModel>();
            }
        }

        public WerkOrdersViewModel Werkorders
        {
            get
            {
                return ServiceLocator.Current.GetInstance<WerkOrdersViewModel>();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}