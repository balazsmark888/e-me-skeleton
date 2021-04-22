using e_me.Mobile.Views;
using Xamarin.Forms;

namespace e_me.Mobile.MenuItems
{
    public class MainTab : Tab
    {
        public MainTab(MainPage mainPage)
        {
            Items.Clear();
            Items.Add(new ShellContent
            {
                Content = mainPage,
                Title = "Main page"
            });
            Title = "Main page";
        }
    }
}
