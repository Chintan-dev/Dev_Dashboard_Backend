using Dev_Dashboard.DTO;
using Dev_Dashboard.Model;
using Dev_Dashboard.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dev_Dashboard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ICommonService _commenService;
        public LoginController(ICommonService commenService)
        {
            _commenService = commenService;
        }
        [HttpPost("Login")]
        public Task<CommonResponseModel> AddUser(LoginDTO userDetail)
        {
            return _commenService.Login(userDetail);
        }
    }
}
