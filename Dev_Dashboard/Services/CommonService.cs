using AutoMapper;
using Dev_Dashboard.DTO;
using Dev_Dashboard.Model;
using Dev_Dashboard.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

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
        public async Task<CommonResponseModel> GetUser()
        {
            try
            {
                List<UserDetail>? data = await _context.UserDetails.ToListAsync();
                List<UserDetailDTO> roles = _mapper.Map<List<UserDetailDTO>>(data);
                return new CommonResponseModel(StatusCode: 200, Success: true, Message: "GET done", Data: roles);
            }
            catch (Exception ex)
            {
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
        //i have to change CreateAction to UserAcrion
        public async Task<CommonResponseModel> CreateAction(UserDetailDTO userDetailDTO)
        {
            try
            {
                var userDetail = _mapper.Map<UserDetail>(userDetailDTO);
                if (userDetail == null)
                {
                    return new CommonResponseModel(StatusCode: 400, Success: false, Message: "Data is null", Data: null);
                }
                _context.UserDetails.Update(userDetail);
                await _context.SaveChangesAsync();
                return new CommonResponseModel(StatusCode: 200, Success: true, Message: "userDetail Update", Data: null);
            }
            catch (Exception ex)
            {
                return new CommonResponseModel(StatusCode: 500, Success: false, Message: ex.Message, Data: null);
            }
        }
    }
}
