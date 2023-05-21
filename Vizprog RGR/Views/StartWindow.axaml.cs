using Avalonia.Controls;
namespace Vizprog_RGR.Views;
using Vizprog_RGR.ViewModels;

    public partial class StartWindow : Window
    {
    readonly StartWindowViewModel lwvm;
        public StartWindow() {
            InitializeComponent();
            lwvm = new StartWindowViewModel();
            DataContext = lwvm;
            lwvm.AddWindow(this);
        }

        public void DTapped(object? sender, Avalonia.Interactivity.RoutedEventArgs e) {
            lwvm.DTapped(sender, e);
        }
    }

