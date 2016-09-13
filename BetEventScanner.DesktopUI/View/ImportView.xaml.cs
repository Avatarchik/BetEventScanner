using System.ComponentModel;
using System.Runtime.CompilerServices;
using BetEventScanner.DesktopUI.Annotations;
using BetEventScanner.DesktopUI.ViewModel;

namespace BetEventScanner.DesktopUI.View
{
    public partial class ImportView : INotifyPropertyChanged
    {
        private ImportViewModel _viewModel;

        public ImportView()
        {
            InitializeComponent();
            ViewModel = new ImportViewModel();
        }

        public ImportViewModel ViewModel
        {
            get { return _viewModel; }
            set
            {
                _viewModel = value; 
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
