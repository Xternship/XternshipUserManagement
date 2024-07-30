using AutoMapper;
using Grpc.Core;
using System.Threading.Tasks;
using UserManagementService.Data;
using UserManagementService.Data.Entities;
using UserManagementService.Data.Exceptions;
using UserManagementService.Data.Repositories;
using UserManagementService.Dtos;
using UserManagementService.Protos;

namespace UserManagementService.Services
{
    public class UserManagementServiceImpl : UserServiceProto.UserServiceProtoBase, IUserManagementService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserManagementServiceImpl(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public override async Task<RegisterUserResponse> RegisterUser(RegisterUserRequest request, ServerCallContext context)
        {
            var userDto = new UserDto
            {
                Username = request.Username,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Role = request.Role
            };

            return await RegisterUserAsync(userDto);
        }

        public async Task<RegisterUserResponse> RegisterUserAsync(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);

            try
            {
                var existingUser = await _userRepository.GetUserByUsernameAsync(user.Username);
                if (existingUser != null)
                {
                    return new RegisterUserResponse
                    {
                        Success = false,
                        Message = "User already exists"
                    };
                }

                await _userRepository.AddUserAsync(user);
                await _userRepository.SaveChangesAsync();

                // We are not sending email here, it will be handled by the API Gateway

                return new RegisterUserResponse
                {
                    Success = true,
                    Message = "User registered successfully"
                };
            }
            catch (UserNotFoundException ex)
            {
                return new RegisterUserResponse
                {
                    Success = false,
                    Message = ex.Message
                };
           
            }
        }

        public override async Task<UpdateUserResponse> UpdateUser(UpdateUserRequest request, ServerCallContext context)
        {
            try
            {
                var userId = int.Parse(request.UserId); // Convert user_id to int
                var user = await _userRepository.GetUserByIdAsync(userId);

                if (user == null)
                {
                    return new UpdateUserResponse
                    {
                        Success = false,
                        Message = "User not found"
                    };
                }

                user.Role = request.Role;
                await _userRepository.UpdateUserAsync(user);
                await _userRepository.SaveChangesAsync();

                return new UpdateUserResponse
                {
                    Success = true,
                    Message = "User updated successfully"
                };
            }
            catch (Exception ex)
            {
                return new UpdateUserResponse
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}