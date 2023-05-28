using Avalonia.Controls;
using Vizprog_RGR.ViewModels;

namespace Vizprog_RGR.Views
{
    public partial class StartWindow : Window
    {
        readonly StartWindowViewModel swvm;

        public StartWindow()
        {
            InitializeComponent();
            swvm = new StartWindowViewModel();
            DataContext = swvm;
            swvm.AddWindow(this);
        }

        public void DTapped(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            swvm.DTapped(sender, e);
        }
    }
}
//ZRADA V DUPI PECHE