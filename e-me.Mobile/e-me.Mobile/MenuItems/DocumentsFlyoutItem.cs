using e_me.Mobile.Views;
using Xamarin.Forms;

namespace e_me.Mobile.MenuItems
{
    public class DocumentsFlyoutItem : FlyoutItem
    {
        public DocumentsFlyoutItem(DocumentsPage documentsPage)
        {
            Items.Clear();
            Items.Add(new ShellContent
            {
                Content = documentsPage,
                Title = "Documents"
            });
            Title = "Documents";
        }
    }
}
