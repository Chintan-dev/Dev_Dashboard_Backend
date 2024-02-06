using Dev_Dashboard.DTO;
using Dev_Dashboard.Model;

namespace Dev_Dashboard.Services.Interface
{
    public interface ICommonService
    {
        //public Task<List<Menu>> GetMenuAsync(int id);
        //public Task<UserDetails> GetUserDetailsAsync(int id);
        public Task<CommonResponseModel> CreateRole(RoleDTO roleDTO);
        public Task<CommonResponseModel> RoleAction(RoleDTO roleDTO);
        public Task<CommonResponseModel> CreateAction(UserDetailDTO userDetailDTO);
        public Task<CommonResponseModel> CreateUser(UserDetailDTO userDetailDTO);
        public Task<CommonResponseModel> CreateMenu(UserMenuDTO userMenuDTO);
        public Task<CommonResponseModel> GetRole();
        public Task<CommonResponseModel> GetUser();
    }
}
