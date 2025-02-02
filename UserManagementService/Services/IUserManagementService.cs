﻿using UserManagementService.Dtos;
using UserManagementService.Protos;

namespace UserManagementService.Services
{
    public interface IUserManagementService
    {
        Task<RegisterUserResponse> RegisterUserAsync(UserDto userDto);
        Task<UpdateUserResponse> UpdateUserAsync(UpdateUserRequest request);
    }
}
