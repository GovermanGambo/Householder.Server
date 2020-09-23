namespace Householder.Server.Authentication
{
    public interface IPasswordPolicy
    {
        void ApplyPolicy(string password, string confirmPassword);
    }
}