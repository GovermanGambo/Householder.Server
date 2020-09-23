using Householder.Server.Users;

namespace Householder.Server.Authorization
{
    public interface ITokenManager
    {
        string GenerateToken(UserLoginDTO userDTO);
    }
}