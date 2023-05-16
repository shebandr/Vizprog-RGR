using Avalonia.Controls;
using Avalonia.Interactivity;
using Vizprog_RGR.ViewModels;

namespace Vizprog_RGR.Views
{
    public partial class MainWindow : Window
    {
        readonly MainWindowViewModel MWVM;
        public MainWindow()
        {
            InitializeComponent();
            MWVM = new MainWindowViewModel();
            DataContext = MWVM;
            MWVM.AddWindow(this);
        }

        public void Update() => MWVM.Update();
    }

}
