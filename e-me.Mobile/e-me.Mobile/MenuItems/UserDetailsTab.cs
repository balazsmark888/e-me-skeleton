using e_me.Mobile.Views;
using Xamarin.Forms;

namespace e_me.Mobile.MenuItems
{
    public class UserDetailsTab : Tab
    {
        public UserDetailsTab(UserDetailPage userDetailPage)
        {
            Items.Clear();
            Items.Add(new ShellContent
            {
                Content = userDetailPage,
                Title = "Personal info"
            });
            Title = "Personal info";
        }
    }
}
