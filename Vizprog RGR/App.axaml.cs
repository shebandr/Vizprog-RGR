using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Vizprog_RGR.ViewModels;
using Vizprog_RGR.Views;
using System.IO;

namespace Vizprog_RGR
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
                desktop.MainWindow = new StartWindow();

            base.OnFrameworkInitializationCompleted();
            IncrementBuildNum();
        }

        private static void IncrementBuildNum()
        {
            if (lock_inc_build) return;

            string path = "../../../../build.num";
            int num;
            try { num = int.Parse(File.ReadAllText(path)); }
            catch (FileNotFoundException) { num = 0; }
            num++;
            File.WriteAllText(path, num.ToString());
        }

        /*
         * ��� ������������
         */

        public static bool lock_inc_build = false;
    }
}
