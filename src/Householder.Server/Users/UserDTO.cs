using System;

namespace Householder.Server.Users
{
    public class UserDTO
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public bool IsAdmin { get; set; }
        public DateTime RegisterDate { get; set; }
    }
}