using Dev_Dashboard.DTO;
using Dev_Dashboard.Model;
using Dev_Dashboard.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;
using System.Text;

namespace Dev_Dashboard.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private ICommonService _commenService;
        public UsersController(ICommonService commenService)
        {
            _commenService = commenService;
        }

        [HttpGet("GetRole")]
        public Task<CommonResponseModel> GetRole()
        {
            return _commenService.GetRole();
        }
        [HttpPost("CreateRole")]
        public Task<CommonResponseModel> Addrole(RoleDTO roleDTO)
        {
            return _commenService.CreateRole(roleDTO);
        }
        [HttpPut("RoleAction")]
        public Task<CommonResponseModel> RoleAction(RoleDTO roleDTO)
        {
            return _commenService.RoleAction(roleDTO);
        }
        [HttpPost("CreateUser")]
        public Task<CommonResponseModel> AddUser(UserDetailDTO userDetailDTO)
        {
            return _commenService.CreateUser(userDetailDTO);
        }

        [HttpGet("GetUser")]
        public Task<CommonResponseModel> GetUser()
        {
            return _commenService.GetUser();
        }

        [HttpGet("GetAllUser")]
        public Task<CommonResponseModel> GetAllUser()
        {
            return _commenService.GetAllUser();
        }

        [HttpPut("UserAction")]
        public Task<CommonResponseModel> UserAction(UserDetailDTO userDetailDTO)
        {
            return _commenService.UserAction(userDetailDTO);
        }

        [HttpPost("CreateMenu")]
        public Task<CommonResponseModel> AddMenu(UserMenuDTO userMenuDTO)
        {
            return _commenService.CreateMenu(userMenuDTO);
        }

        [HttpGet("GetMenu")]
        public Task<CommonResponseModel> GetMenu()
        {
            return _commenService.GetMenu();
        }

        [HttpPost("CreateUserAssignMenu")]
        public Task<CommonResponseModel> AddUserAssignMenu(UserAssignMenuDTO userAssignMenuDTO)
        {
            return _commenService.CreateUserAssignMenu(userAssignMenuDTO);
        }

        [HttpGet("GetUserAssignMenu")]
        public Task<CommonResponseModel> GetUserAssignMenu(int User_id)
        {
            return _commenService.GetUserAssignMenu(User_id);
        }

        [HttpDelete("RemoveUserAssignMenu")]
        public Task<CommonResponseModel> RemoveUserAssignMenu(int id)
        {
            return _commenService.RemoveUserAssignMenu(id);
        }

        [HttpGet("AdminBroadcast")]
        public Task<CommonResponseModel> AdminBroadcast()
        {
            return _commenService.WebSockets();
        }
    }
}
