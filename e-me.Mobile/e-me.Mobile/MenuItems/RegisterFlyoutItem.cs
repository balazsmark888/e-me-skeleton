using e_me.Mobile.Views;
using Xamarin.Forms;

namespace e_me.Mobile.MenuItems
{
    public class RegisterFlyoutItem : FlyoutItem
    {
        public RegisterFlyoutItem(RegisterPage registerPage)
        {
            Items.Clear();
            Items.Add(new ShellContent
            {
                Content = registerPage,
                Title = "Register"
            });
            Title = "Register";
        }
    }
}
