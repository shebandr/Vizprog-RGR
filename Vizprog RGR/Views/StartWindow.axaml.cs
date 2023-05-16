using Avalonia.Controls;
namespace Vizprog_RGR.Views;
using Vizprog_RGR.ViewModels;

    public partial class StartWindow : Window
    {
        readonly StartWindowViewModel SWVM;
        public StartWindow()
        {
            InitializeComponent();
            SWVM = new StartWindowViewModel();
            DataContext = SWVM;
            SWVM.AddWindow(this);
        }
    }

