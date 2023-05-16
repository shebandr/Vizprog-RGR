using Avalonia.VisualTree;
using Avalonia.Controls;

namespace Test_RGR
{
    public class UnitTest1
    {
        [Fact]
        public async void Test1()
        {
            var app = AvaloniaApp.GetApp();
            var mainWindow = AvaloniaApp.GetMainWindow();
            string oz = "12";
            await Task.Delay(1000);

            var button = mainWindow.GetVisualDescendants().OfType<Button>().First(b => b.Name == "button1");
            var textb = mainWindow.GetVisualDescendants().OfType<TextBlock>().First();

            button.Command.Execute(button.CommandParameter);

            await Task.Delay(100);
            var text = textb.Text;

            Assert.True(text.Equals(oz), "12==12");
        }
    }
}