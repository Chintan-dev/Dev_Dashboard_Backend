using Dev_Dashboard.Model;

namespace Dev_Dashboard.Services.Interface
{
    public interface IGenerateTokenService
    {
        public string GenerateToken(UserDetail userDetail);
    }
}
