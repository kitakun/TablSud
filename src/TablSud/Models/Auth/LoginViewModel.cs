namespace TablSud.Web.Models.Auth
{
    public class LoginViewModel
    {
        public LoginViewModel()
        {
            
        }

        public LoginViewModel(string error)
        {
            Error = error;
        }

        public string Error { get; set; }
    }
}
