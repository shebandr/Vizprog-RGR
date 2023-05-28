using Avalonia.Controls;
using Avalonia.Controls.Presenters;
using Avalonia.Input;
using ReactiveUI;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reactive;
using Vizprog_RGR.Models;
using Vizprog_RGR.Views;
using Vizprog_RGR.Views.Shapes;


namespace Vizprog_RGR.ViewModels
{
    public class Log
    {
        static readonly List<string> logs = new();
        static readonly string path = "../../../Log.txt";
        static bool first = true;

        static readonly bool use_file = false;

        public static MainWindowViewModel? Mwvm { private get; set; }
        public static void Write(string message, bool without_update = false)
        {
            if (!without_update)
            {
                foreach (var mess in message.Split('\n')) logs.Add(mess);
                while (logs.Count > 45) logs.RemoveAt(0);

                if (Mwvm != null) Mwvm.Logg = string.Join('\n', logs);
            }

            if (use_file)
            {
                if (first) File.WriteAllText(path, message + "\n");
                else File.AppendAllText(path, message + "\n");
                first = false;
            }
        }
    }

    public class MainWindowViewModel : ViewModelBase
    {
        private string log = "";
        public string Logg { get => log; set => this.RaiseAndSetIfChanged(ref log, value); }

        public MainWindowViewModel()
        {
            Log.Mwvm = this;
            Comm = ReactiveCommand.Create<string, Unit>(n => { FuncComm(n); return new Unit(); });
            NewItem = ReactiveCommand.Create<Unit, Unit>(_ => { FuncNewItem(); return new Unit(); });
        }

        private Window? mw;
        public void AddWindow(Window window)
        {
            var canv = window.Find<Canvas>("Canvas");

            mw = window;
            map.canv = canv;
            if (canv == null) return;

            canv.Children.Add(map.Marker);
            canv.Children.Add(map.Marker2);

            var panel = (Panel?)canv.Parent;
            if (panel == null) return;

            panel.PointerPressed += (object? sender, PointerPressedEventArgs e) =>
            {
                if (e.Source != null && e.Source is Control @control) map.Press(@control, e.GetCurrentPoint(canv).Position);
            };
            panel.PointerMoved += (object? sender, PointerEventArgs e) =>
            {
                if (e.Source != null && e.Source is Control @control) map.Move(@control, e.GetCurrentPoint(canv).Position);
            };
            panel.PointerReleased += (object? sender, PointerReleasedEventArgs e) =>
            {
                if (e.Source != null && e.Source is Control @control)
                {
                    int mode = map.Release(@control, e.GetCurrentPoint(canv).Position);
                    bool tap = map.tapped;
                    if (tap && mode == 1)
                    {
                        var pos = map.tap_pos;
                        if (canv == null) return;

                        var newy = map.GenSelectedItem();
                        newy.Move(pos);
                        map.AddItem(newy);
                    }
                }
            };
            panel.PointerWheelChanged += (object? sender, PointerWheelEventArgs e) =>
            {
                if (e.Source != null && e.Source is Control @control) map.WheelMove(@control, e.Delta.Y, e.GetCurrentPoint(canv).Position);
            };
            mw.KeyDown += (object? sender, KeyEventArgs e) =>
            {
                if (e.Source != null && e.Source is Control @control) map.KeyPressed(@control, e.Key);
            };
        }

        public static IGate[] ItemTypes { get => map.item_types; }
        public static int SelectedItem { get => map.SelectedItem; set => map.SelectedItem = value; }


        Grid? cur_grid;
        TextBlock? old_b_child;
        object? old_b_child_tag;
        string? prev_scheme_name;

        public static string ProjName { get => CurrentProj == null ? "???" : CurrentProj.Name; }

        public static ObservableCollection<Scheme> Schemes { get => CurrentProj == null ? new() : CurrentProj.schemes; }



        public void DTapped(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var src = (Control?)e.Source;

            if (src is ContentPresenter cp && cp.Child is Border bord) src = bord;
            if (src is Border bord2 && bord2.Child is Grid g2) src = g2;
            if (src is Grid g3 && g3.Children[0] is TextBlock tb2) src = tb2;

            if (src is not TextBlock tb) return;

            var p = tb.Parent;
            if (p == null) return;

            if (old_b_child != null)
                if (cur_grid != null) cur_grid.Children[0] = old_b_child;

            if (p is not Grid g) return;
            cur_grid = g;

            old_b_child = tb;
            old_b_child_tag = tb.Tag;
            prev_scheme_name = tb.Text;

            var newy = new TextBox { Text = tb.Text };


            cur_grid.Children[0] = newy;


            newy.KeyUp += (object? sender, KeyEventArgs e) =>
            {
                if (e.Key != Key.Return) return;

                if (newy.Text != prev_scheme_name)
                {
                    if ((string?)tb.Tag == "p_name") CurrentProj?.ChangeName(newy.Text);
                    else if (old_b_child_tag is Scheme scheme) scheme.ChangeName(newy.Text);
                }

                cur_grid.Children[0] = tb;
                cur_grid = null; old_b_child = null;
            };
        }

        public void Update()
        {
            Log.Write("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n    ������� ������:\n" + CurrentProj);

            map.ImportScheme();

            this.RaisePropertyChanged(new(nameof(ProjName)));
            this.RaisePropertyChanged(new(nameof(Schemes)));
            this.RaisePropertyChanged(new(nameof(CanSave)));
            if (mw != null) mw.Width++;
        }

        public static bool CanSave { get => CurrentProj != null && CurrentProj.CanSave(); }

        public void FuncComm(string Comm)
        {
            switch (Comm)
            {
                case "Create":
                    var newy = map.filer.CreateProject();
                    CurrentProj = newy;
                    Update();
                    break;
                case "Open":
                    if (mw == null) break;
                    var selected = map.filer.SelectProjectFile(mw);
                    if (selected != null)
                    {
                        CurrentProj = selected;
                        Update();
                    }
                    break;
                case "Save":
                    map.Export();

                    File.WriteAllText("../../../for_test.json", Utils.Obj2json((map.current_scheme ?? throw new System.Exception("׸?!")).Export()));
                    break;
                case "SaveAs":
                    map.Export();
                    if (mw != null) CurrentProj?.SaveAs(mw);
                    this.RaisePropertyChanged(new(nameof(CanSave)));
                    break;
                case "ExitToLauncher":
                    new StartWindow().Show();
                    mw?.Hide();
                    break;
                case "Exit":
                    mw?.Close();
                    break;
            }
        }

        public ReactiveCommand<string, Unit> Comm { get; }

        private static void FuncNewItem()
        {
            CurrentProj?.AddScheme(null);
        }

        public ReactiveCommand<Unit, Unit> NewItem { get; }

        public static bool LockSelfConnect { get => map.lock_self_connect; set => map.lock_self_connect = value; }
    }
}