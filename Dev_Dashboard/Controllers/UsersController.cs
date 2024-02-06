using Dev_Dashboard.DTO;
using Dev_Dashboard.Model;
using Dev_Dashboard.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Dev_Dashboard.Controllers
{
    [Route("api/[controller]")]
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
        [HttpPut("CreateAction")]
        public Task<CommonResponseModel> CreateAction(UserDetailDTO userDetailDTO)
        {
            return _commenService.CreateAction(userDetailDTO);
        }
    }
}
