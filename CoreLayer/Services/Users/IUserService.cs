using CoreLayer.DTOs.User;
using CoreLayer.DTOs.Users;
using CoreLayer.Utilities;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLayer.Services.Users
{
    public interface IUserService
    {
        OperationResult RegisterUser(UserRegisterDto registerDto);
        UserDto UserLogin(UserLoginDto loginDto);
    }
}
