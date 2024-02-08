using AutoMapper;
using Dev_Dashboard.DTO;
using Dev_Dashboard.Model;
using Dev_Dashboard.Services.Interface;
using Microsoft.EntityFrameworkCore;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Dev_Dashboard.Services
{
    public class CommonService : ICommonService
    {
        private readonly IMapper _mapper;
        private readonly DevDashboardContext _context;
        public CommonService(DevDashboardContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CommonResponseModel> GetRole()
        {
            try
            {
                List<Role>? data = await _context.Roles.ToListAsync();
                List<RoleDTO> roles = _mapper.Map<List<RoleDTO>>(data);
                return new CommonResponseModel(StatusCode: 200, Success: true, Message: "GET done", Data: roles);
            }
            catch (Exception ex)
            {
                return new CommonResponseModel(StatusCode: 500, Success: false, Message: ex.Message, null);
            }
        }
        public async Task<CommonResponseModel> CreateRole(RoleDTO roleDTO)
        {
            try
            {
                var role = _mapper.Map<Role>(roleDTO);
                if (role == null)
                {
                    return new CommonResponseModel(StatusCode: 400, Success: false, Message: "Data is null", Data: null);
                }
                var check = _context.Roles.Any(x => x.RoleName.ToLower() == role.RoleName.ToLower());
                if (check)
                {
                    // Same role already exists
                    return new CommonResponseModel(StatusCode: 400, Success: false, Message: "Role with the same name already exists.", Data: null);
                }
                await _context.Roles.AddAsync(role);
                await _context.SaveChangesAsync();
                return new CommonResponseModel(StatusCode: 200, Success: true, Message: "Role Saved", Data: null);
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "An error occurred while processing the role.");
                //return new CommonResponseModel(StatusCode: 500, Success: false, Message: "An error occurred while processing the role.", Data: null);
                return new CommonResponseModel(StatusCode: 500, Success: false, Message: ex.Message, null);
            }
        }
        public async Task<CommonResponseModel> RoleAction(RoleDTO roleDTO)
        {
            try
            {
                var role = _mapper.Map<Role>(roleDTO);
                if (role == null)
                {
                    return new CommonResponseModel(StatusCode: 400, Success: false, Message: "Data is null", Data: null);
                }
                _context.Roles.Update(role);
                await _context.SaveChangesAsync();
                return new CommonResponseModel(StatusCode: 200, Success: true, Message: "Role Update", Data: null);
            }
            catch (Exception ex)
            {
                return new CommonResponseModel(StatusCode: 500, Success: false, Message: ex.Message, Data: null);
            }
        }

        public async Task<CommonResponseModel> GetUser()
        {
            try
            {
                List<UserDetail>? data = await _context.UserDetails.Where(x=>x.Active == true).ToListAsync();
                if (data == null)
                {
                    return new CommonResponseModel(StatusCode: 400, Success: false, Message: "Data is null", Data: null);
                }
                List<Role>? roles = await _context.Roles.ToListAsync();
                List<UserDetailDTO> userDetails = _mapper.Map<List<UserDetailDTO>>(data);

                userDetails = userDetails.Join(roles, userDetail => userDetail.RoleId, role => role.Id,
                (userDetail, role) =>
                {
                    userDetail.RoleName = role.RoleName;
                    return userDetail;
                }).ToList();


                return new CommonResponseModel(StatusCode: 200, Success: true, Message: "GET done", Data: userDetails);
            }
            catch (Exception ex)
            {
                return new CommonResponseModel(StatusCode: 500, Success: false, Message: ex.Message, null);
            }
        }
        public async Task<CommonResponseModel> GetAllUser()
        {
            try
            {
                List<UserDetail>? data = await _context.UserDetails.ToListAsync();
                if (data == null)
                {
                    return new CommonResponseModel(StatusCode: 400, Success: false, Message: "Data is null", Data: null);
                }
                List<Role>? roles = await _context.Roles.ToListAsync();
                List<UserDetailDTO> userDetails = _mapper.Map<List<UserDetailDTO>>(data);

                userDetails = userDetails.Join(roles, userDetail => userDetail.RoleId, role => role.Id,
                (userDetail, role) =>
                {
                    userDetail.RoleName = role.RoleName;
                    return userDetail;
                }).ToList();


                return new CommonResponseModel(StatusCode: 200, Success: true, Message: "GET done", Data: userDetails);
            }
            catch (Exception ex)
            {
                return new CommonResponseModel(StatusCode: 500, Success: false, Message: ex.Message, null);
            }
        }
        public async Task<CommonResponseModel> CreateUser(UserDetailDTO userDetailDTO)
        {
            try
            {
                var userDetail = _mapper.Map<UserDetail>(userDetailDTO);
                if (userDetail == null)
                {
                    return new CommonResponseModel(StatusCode: 400, Success: false, Message: "Data is null", Data: null);
                }
                var check = _context.UserDetails.Any(x => x.Email.ToLower().Trim() == userDetail.Email.ToLower() || x.Username.ToLower().Trim() == userDetail.Username.ToLower());
                if (check)
                {
                    // Same userDetail already exists
                    return new CommonResponseModel(StatusCode: 400, Success: false, Message: "userDetail with the same Email or Username already exists.", Data: null);
                }
                await _context.UserDetails.AddAsync(userDetail);
                await _context.SaveChangesAsync();
                return new CommonResponseModel(StatusCode: 200, Success: true, Message: "User Saved", Data: null);
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "An error occurred while processing the role.");
                //return new CommonResponseModel(StatusCode: 500, Success: false, Message: "An error occurred while processing the role.", Data: null);
                return new CommonResponseModel(StatusCode: 500, Success: false, Message: ex.Message, null);
            }
        }
        public async Task<CommonResponseModel> UserAction(UserDetailDTO userDetailDTO)
        {
            try
            {
                var data = await _context.UserDetails.FirstOrDefaultAsync(x => x.Id == userDetailDTO.Id);
                var userDetail = _mapper.Map<UserDetail>(userDetailDTO);

                if (userDetail == null || data == null)
                {
                    return new CommonResponseModel(StatusCode: 400, Success: false, Message: "Data is null", Data: null);
                }
                data.Active = userDetailDTO.Active;
                _context.UserDetails.Update(data);
                await _context.SaveChangesAsync();
                return new CommonResponseModel(StatusCode: 200, Success: true, Message: "userDetail Update", Data: null);
            }
            catch (Exception ex)
            {
                return new CommonResponseModel(StatusCode: 500, Success: false, Message: ex.Message, Data: null);
            }
        }

        public async Task<CommonResponseModel> GetMenu()
        {
            try
            {
                List<UserMenu>? data = await _context.UserMenus.ToListAsync();
                List<UserMenuDTO> roles = _mapper.Map<List<UserMenuDTO>>(data);
                return new CommonResponseModel(StatusCode: 200, Success: true, Message: "GET done", Data: roles);
            }
            catch (Exception ex)
            {
                return new CommonResponseModel(StatusCode: 500, Success: false, Message: ex.Message, null);
            }
        }
        public async Task<CommonResponseModel> CreateMenu(UserMenuDTO userMenuDTO)
        {
            try
            {
                var UserMenu = _mapper.Map<UserMenu>(userMenuDTO);
                if (UserMenu == null)
                {
                    return new CommonResponseModel(StatusCode: 400, Success: false, Message: "Data is null", Data: null);
                }
                var check = _context.UserMenus.Any(x => x.MenuName.ToLower().Trim() == UserMenu.MenuName.ToLower());
                if (check)
                {
                    // Same userDetail already exists
                    return new CommonResponseModel(StatusCode: 400, Success: false, Message: "UserMenu with the same name already exists.", Data: null);
                }
                await _context.UserMenus.AddAsync(UserMenu);
                await _context.SaveChangesAsync();
                return new CommonResponseModel(StatusCode: 200, Success: true, Message: "User Saved", Data: null);
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "An error occurred while processing the role.");
                //return new CommonResponseModel(StatusCode: 500, Success: false, Message: "An error occurred while processing the role.", Data: null);
                return new CommonResponseModel(StatusCode: 500, Success: false, Message: ex.Message, null);
            }
        }

        public async Task<CommonResponseModel> CreateUserAssignMenu(UserAssignMenuDTO userAssignMenuDTO)
        {
            try
            {
                var userAssignMenu = _mapper.Map<UserAssignMenu>(userAssignMenuDTO);
                if (userAssignMenu == null)
                {
                    return new CommonResponseModel(StatusCode: 400, Success: false, Message: "Data is null", Data: null);
                }
                var check = _context.UserAssignMenus.Any(x => x.UserId == userAssignMenu.UserId && x.MenuId == userAssignMenu.MenuId);
                if (check)
                {
                    // Same UserAssignMenu already exists
                    return new CommonResponseModel(StatusCode: 400, Success: false, Message: "Menu with the same name already exists.", Data: null);
                }
                await _context.UserAssignMenus.AddAsync(userAssignMenu);
                await _context.SaveChangesAsync();
                return new CommonResponseModel(StatusCode: 200, Success: true, Message: "User Saved", Data: null);
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "An error occurred while processing the role.");
                //return new CommonResponseModel(StatusCode: 500, Success: false, Message: "An error occurred while processing the role.", Data: null);
                return new CommonResponseModel(StatusCode: 500, Success: false, Message: ex.Message, null);
            }
        }
        public async Task<CommonResponseModel> GetUserAssignMenu(int User_id)
        {
            try
            {
                var data = await _context.UserAssignMenus
                            .Where(x => x.UserId == User_id)
                            .Join(_context.UserMenus,
                                userAssignMenu => userAssignMenu.MenuId,
                                userMenu => userMenu.Id,
                                (userAssignMenu, userMenu) => new { userAssignMenu, userMenu })
                            .Join(_context.UserDetails,
                                joinedTables => joinedTables.userAssignMenu.UserId,
                                userDetails => userDetails.Id,
                                (joinedTables, userDetails) => new UserAssignMenuDTO
                                {
                                    Id = joinedTables.userAssignMenu.Id,
                                    UserName = userDetails.Username,
                                    MenuName = joinedTables.userMenu.MenuName,
                                    UserId = joinedTables.userAssignMenu.UserId,  
                                    MenuId = joinedTables.userMenu.Id,
                                    MenuDescription = joinedTables.userMenu.MenuDescription,
                                    Path = joinedTables.userMenu.Path
                                })
                            .ToListAsync();
                if (data == null)
                {
                    return new CommonResponseModel(StatusCode: 400, Success: false, Message: "Data is null", Data: null);
                }
                return new CommonResponseModel(StatusCode: 200, Success: true, Message: "GET done", Data: data);
            }
            catch (Exception ex)
            {
                return new CommonResponseModel(StatusCode: 500, Success: false, Message: ex.Message, null);
            }
        }
        public async Task<CommonResponseModel> RemoveUserAssignMenu(int UserAssignMenu_id)
        {
            var userAssignMenu = await _context.UserAssignMenus.FirstOrDefaultAsync(x => x.Id == UserAssignMenu_id);

            if (userAssignMenu == null)
            {
                return new CommonResponseModel(StatusCode: 400, Success: false, Message: "Data is null", Data: null);
            }
            _context.UserAssignMenus.Remove(userAssignMenu);
            await _context.SaveChangesAsync();

            return new CommonResponseModel(StatusCode: 200, Success: true, Message: "UserAssignMenus Remove", Data: null);
        }

        public async Task<CommonResponseModel> Login(LoginDTO userDetail)
        {
            //Authentication()
            return new CommonResponseModel(StatusCode: 200, Success: true, Message: "Login", Data: null);
        }
    }
}
