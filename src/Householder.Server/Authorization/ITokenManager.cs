using Householder.Server.Users;

namespace Householder.Server.Authorization
{
    public interface ITokenManager
    {
        string GenerateToken(UserDTO userDTO);
    }
}