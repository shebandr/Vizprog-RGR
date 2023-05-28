using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.ReactiveUI;
using Avalonia.Threading;
using Vizprog_RGR;
using Vizprog_RGR.Views;


namespace Test_RGR
{
    public static class AvaloniaApp
    {
        // DI registrations
        /* public static void RegisterDependencies() =>
             Bootstrapper.Register(AvaloniaLocator.CurrentMutable, AvaloniaLocator.Current);*/

        // stop app and cleanup
        public static void Stop()
        {
            var app = GetApp();
            if (app is IDisposable disposable) Dispatcher.UIThread.Post(disposable.Dispose);

            Dispatcher.UIThread.Post(() => app.Shutdown());
        }

        public static StartWindow GetMainWindow() => (StartWindow)GetApp().MainWindow;

        public static IClassicDesktopStyleApplicationLifetime GetApp()
        {
            var app = Application.Current ?? throw new Exception("Приложение не найдено");
            var life = app.ApplicationLifetime ?? throw new Exception("Приложение не найдено");
            return (IClassicDesktopStyleApplicationLifetime)life;
        }

        public static AppBuilder BuildAvaloniaApp()
        {
            App.lock_inc_build = true;
            return AppBuilder
                .Configure<App>()
                .UsePlatformDetect()
                .UseReactiveUI();
                
        }
    }
}
