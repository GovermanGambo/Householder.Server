using Microsoft.AspNetCore.Identity;

namespace Householder.Server.Authentication
{
    public class PasswordManager : IPasswordManager
    {
        private readonly PasswordHasher<PasswordManager> passwordHasher;

        public PasswordManager()
        {
            passwordHasher = new PasswordHasher<PasswordManager>();
        }

        public string HashPassword(string password)
        {
            return passwordHasher.HashPassword(this, password);
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            return passwordHasher.VerifyHashedPassword(this, hashedPassword, password) == PasswordVerificationResult.Success;
        }
    }
}