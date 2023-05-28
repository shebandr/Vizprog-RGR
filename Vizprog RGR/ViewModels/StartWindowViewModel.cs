using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using ReactiveUI;
using System.Reactive;
using Vizprog_RGR.Models;
using Vizprog_RGR.Views;

namespace Vizprog_RGR.ViewModels
{
    public class StartWindowViewModel : ViewModelBase
    {
        Window? me;

        private static readonly MainWindow MV = new() ;

        public StartWindowViewModel()
        {
            CreateW = ReactiveCommand.Create<Unit, Unit>(_ => { FuncCreate(); return new Unit(); });
            OpenW = ReactiveCommand.Create<Unit, Unit>(_ => { FuncOpen(); return new Unit(); });
            ExitW = ReactiveCommand.Create<Unit, Unit>(_ => { FuncExit(); return new Unit(); });
        }
        public void AddWindow(Window lw) => me = lw;

        void FuncCreate()
        {

            var newy = map.filer.CreateProject();
            CurrentProj = newy;

            MV.Show();

            MV.Update();
            me?.Close();
        }
        void FuncOpen()
        {
            if (me == null) return;

            var selected = map.filer.SelectProjectFile(me);
            if (selected == null) return;

            CurrentProj = selected;
            MV.Show();
            MV.Update();
            me?.Close();
        }
        void FuncExit()
        {
            me?.Close();
            MV.Close();
        }

        public ReactiveCommand<Unit, Unit> CreateW { get; }
        public ReactiveCommand<Unit, Unit> OpenW { get; }
        public ReactiveCommand<Unit, Unit> ExitW { get; }


        public static Project[] ProjectList { get => map.filer.GetSortedProjects(); }

        public void DTapped(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var src = (Control?)e.Source;

            if (src is ContentPresenter cp && cp.Child is Border bord) src = bord;
            if (src is Border bord2 && bord2.Child is TextBlock tb2) src = tb2;

            if (src is not TextBlock tb || tb.Tag is not Project proj) return;

            CurrentProj = proj;

            MV.Show();
            MV.Update();
            me?.Close();
        }

        /*
         * Для тестирования
         */

        public static MainWindow GetMW => MV;
    }
}
