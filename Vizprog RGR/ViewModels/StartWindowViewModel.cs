using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using Vizprog_RGR.Views;
using Vizprog_RGR.ViewModels;

namespace Vizprog_RGR.ViewModels
{ 
    public class StartWindowViewModel : ViewModelBase
    {
        Window? me;
        private static readonly MainWindow mw = new();

        public StartWindowViewModel()
        {
            Create = ReactiveCommand.Create<Unit, Unit>(_ => { FuncCreate(); return new Unit(); });
            Exit = ReactiveCommand.Create<Unit, Unit>(_ => { FuncExit(); return new Unit(); });
        }
        public void AddWindow(Window lw) => me = lw;

        void FuncCreate()
        {

            mw.Show();
            mw.Update();
            me?.Close();
        }
        void FuncExit()
        {
            me?.Close();
        }

        public ReactiveCommand<Unit, Unit> Create { get; }
        public ReactiveCommand<Unit, Unit> Exit { get; }



        public void DTapped(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var src = (Control?)e.Source;


            mw.Show();
            mw.Update();
            me?.Close();
        }
    }
}
