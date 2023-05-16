using Avalonia.Controls;
using Avalonia.Interactivity;
using Vizprog_RGR.ViewModels;

namespace Vizprog_RGR.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }

    }

}
