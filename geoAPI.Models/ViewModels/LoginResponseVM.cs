using IdentityModel.Client;


namespace geoAPI.Models.ViewModels
{
    public class LoginResponseVM
    {
        public LoginResponseVM(TokenResponse tokenResponse, UserVM user)
        {
            AccessToken = tokenResponse.AccessToken;
            Expires = tokenResponse.ExpiresIn;
            User = user;
        }

        public string AccessToken { get; set; }

        public long Expires { get; set; }

        public UserVM User { get; set; }
    }
}
