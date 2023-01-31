namespace DeveloperWepApi1.Model.Entities
{
    public interface IUserService
    {
        bool ValidateCredentials(string username, string password);  
    }
}