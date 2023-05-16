using Avalonia.Controls;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using Vizprog_RGR.Views;

namespace Vizprog_RGR.ViewModels
{
    internal class StartWindowViewModel
    {
        Window? me;
        private static readonly MainWindow mw = new();

        public StartWindowViewModel()
        {
            Create = ReactiveCommand.Create<Unit, Unit>(_ => {return new Unit(); });
            Exit = ReactiveCommand.Create<Unit, Unit>(_ => { FuncExit(); return new Unit(); });
        }
        public void AddWindow(Window lw) => me = lw;

        void FuncExit()
        {
            me?.Close();
        }

        public ReactiveCommand<Unit, Unit> Create { get; }
        public ReactiveCommand<Unit, Unit> Exit { get; }
    }
}
