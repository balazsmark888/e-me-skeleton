namespace e_me.Mobile.ViewModels
{
    public class LoginViewModel : ILoginViewModel
    {
        public string LoginName { get; set; }

        public string Password { get; set; }

        public string PublicKey { get; set; }
    }

    public interface ILoginViewModel
    {

    }
}
