using System.Net.Http;

namespace e_me.Mobile.ViewModels
{
    public class RegisterViewModel : IRegisterViewModel
    {
        private readonly HttpClient _httpClient;

        public string FullName { get; set; }

        public string LoginName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }


        public RegisterViewModel(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient();
        }
    }

    public interface IRegisterViewModel
    {

    }
}
