using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StomerijBasiliek.Common;

namespace StomerijBasiliek.ViewModel
{

    public class NoteViewModel : ViewModelBase
    {
        private string _noteText;
        public string NoteText
        {
            get
            {
                return _noteText;
            }
            set
            {
                _noteText = value;
                RaisePropertyChanged(() => NoteText);
            }
        }
        public NoteViewModel(IFrameNavigationService fns)
        {
            NoteText = "Lorem Consectetuer Fermentum\r\nUrna lobortis sodales neque dui iaculis. Libero feugiat feugiat rhoncus ut et platea molestie penatibus. Erat est. Morbi imperdiet. Luctus facilisis faucibus odio nonummy sed nullam placerat purus Curae; nullam nec conubia phasellus, ultrices. Nec quam. Ridiculus taciti duis.\r\n\r\nEget eu nulla maecenas elementum Semper maecenas nostra, interdum cras nonummy, lorem auctor potenti purus dis metus. Velit varius pede penatibus. Dapibus ipsum ipsum mattis. Dignissim nisl\r\n\r\nErat Ac\r\nBibendum turpis rutrum Commodo. Justo. Placerat hymenaeos elementum pretium est semper. Habitasse nunc amet. Nam. Nulla lectus leo. Donec, pede vitae quisque vivamus porttitor sociis donec accumsan amet. Massa pretium accumsan pretium sociosqu eu.";
        }
    }
}
