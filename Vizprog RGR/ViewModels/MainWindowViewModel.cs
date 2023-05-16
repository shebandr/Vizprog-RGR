using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Vizprog_RGR.ViewModels;

namespace Vizprog_RGR.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        string bt = "1";
        public MainWindowViewModel()
        {
            BC = ReactiveCommand.Create<string, string>(str => BT += str);
        }
        public string BT
        {
            get => bt; 
            set
            {
                this.RaiseAndSetIfChanged(ref bt, value);

            }
        }
        public ReactiveCommand<string, string> BC { get; }
    }
}
