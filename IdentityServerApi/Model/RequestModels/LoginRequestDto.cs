namespace IdentityServerApi.Model.RequestModels
{
    public class LoginRequestModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}