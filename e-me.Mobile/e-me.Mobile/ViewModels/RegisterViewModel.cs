namespace e_me.Mobile.ViewModels
{
    public class RegisterViewModel : IRegisterViewModel
    {
        public string FullName { get; set; }

        public string LoginName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }

    public interface IRegisterViewModel
    {

    }
}
