using GalaSoft.MvvmLight;
using StomerijBasiliek.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StomerijBasiliek.ViewModel
{

    public class LoginViewModel : ViewModelBase
    {
        private string _helloWorld;
        public string HelloWorld
        {
            get
            {
                return _helloWorld;
            }
            set
            {
                _helloWorld = value;
                RaisePropertyChanged(() => HelloWorld);
            }
        }

        private bool _isNotAuthenticated;
        public bool IsNotAuthenticated
        {
            get
            {
                return _isNotAuthenticated;
            }
            set
            {
                _isNotAuthenticated = value;
                RaisePropertyChanged(() => IsNotAuthenticated);
            }
        }
        private string _userName;
        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
                RaisePropertyChanged(() => UserName);
            }
        }

        private string _password;
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                RaisePropertyChanged(() => Password);
            }
        }
        public LoginViewModel(IFrameNavigationService fns)
        {
            HelloWorld = "Login View";
        }
    }
}
