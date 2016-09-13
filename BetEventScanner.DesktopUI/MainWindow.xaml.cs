using System.ComponentModel;
using System.Runtime.CompilerServices;
using BetEventScanner.DesktopUI.Annotations;
using BetEventScanner.DesktopUI.ViewModel;

namespace BetEventScanner.DesktopUI
{
    public partial class MainWindow : INotifyPropertyChanged
    {
        private MainWindowViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindowViewModel ViewModel
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
