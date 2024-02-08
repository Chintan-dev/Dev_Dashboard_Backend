using Dev_Dashboard.DTO;
using Dev_Dashboard.Model;
using Microsoft.AspNetCore.Mvc;

namespace Dev_Dashboard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Menus : ControllerBase
    {
        //private readonly UsersDB _db;
        //public Menus(UsersDB db)
        //{
        //    _db = db;
        //}
        //test

        [Route("GetUserLists")]
        [HttpGet]
        public IActionResult GetUserLists()
        {
            var Users = new List<UserDetail>()
            {
                new UserDetail() {Username="DEV_1",Password="123",RoleId=1},
                new UserDetail() {Username="DEV_2",Password="123",RoleId=2},
                new UserDetail() {Username="DEV_3",Password="123",RoleId=3}
            };
            return Ok(User);
        }
        [Route("GetRoleList")]
        [HttpGet]
        public CommonResponseModel GetRoleList()
        {
            var roles = new List<RoleDTO>()
            {
                new RoleDTO(){ Id = 1,RoleName="Admin"},
                new RoleDTO(){ Id = 2,RoleName="Manager "},
                new RoleDTO(){ Id = 3,RoleName="User"},
                new RoleDTO(){ Id = 4,RoleName="DEV"},
            };
            return new CommonResponseModel(200, true, "ok", roles);
        }
    }
}
