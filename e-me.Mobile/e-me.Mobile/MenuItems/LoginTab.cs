using e_me.Mobile.Views;
using Xamarin.Forms;

namespace e_me.Mobile.MenuItems
{
    public class LoginTab : Tab
    {
        public LoginTab( LoginPage loginPage)
        {
            Items.Clear();
            Items.Add(new ShellContent
            {
                Content = loginPage,
                Title = "Login"
            });
            Title = "Login";
        }
    }
}
