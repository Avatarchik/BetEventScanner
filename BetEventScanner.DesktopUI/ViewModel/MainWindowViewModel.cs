using System.Windows.Input;
using BetEventScanner.DesktopUI.View;
using DevExpress.Mvvm;

namespace BetEventScanner.DesktopUI.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            OpenImport = new DelegateCommand(OnOpenImport);
        }

        private void OnOpenImport()
        {
            var importView = new ImportView();
            importView.ShowDialog();
        }

        public ICommand OpenImport { get; set; }
    }
}
