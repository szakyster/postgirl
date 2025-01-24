using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Input;
using Postgirl.ViewModels;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Postgirl
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindowViewModel ViewModel { get; }
        public MainWindow(MainWindowViewModel viewModel)
        {
            this.InitializeComponent();
            this.RootGrid.DataContext = this.ViewModel;
            this.ViewModel = viewModel;
        }
        private void TextBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key != Windows.System.VirtualKey.Enter || !ViewModel.SendRequestCommand.CanExecute(null))
            {
                return;
            }

            ViewModel.Url = Url.Text;
            ViewModel.SendRequestCommand.Execute(null);
        }
    }
}
