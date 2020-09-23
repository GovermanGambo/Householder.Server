using System;

namespace Householder.Server.Users
{
    public class UserLoginDTO
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string HashedPassword { get; set; }
    }
}