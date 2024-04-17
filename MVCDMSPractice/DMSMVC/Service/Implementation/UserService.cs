using DMSMVC.Models.DTOs;
using DMSMVC.Models.Entities;
using DMSMVC.Models.RequestModel;
using DMSMVC.Repository.Implementation;
using DMSMVC.Repository.Interface;
using DMSMVC.Service.Interface;
using Microsoft.AspNetCore.Identity;
using Org.BouncyCastle.Asn1.Ocsp;
using System.Linq.Expressions;

namespace DMSMVC.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        //private readonly UserManager<User> _userManager;

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {

            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }





        public async Task<UserDTO?> GetUserAsyn(string email)
        {
            var user = await _userRepository.GetAsync(a => a.Email == email);
            var userDTO = user != null ? new UserDTO
            {
                Email = user.Email,
                Id = user.Id,
                Password = user.Password,
            } : null;

            return null;
        }


        public async Task<string> ReturnPassWord(string email)
        {
            var user = await _userRepository.GetAsync(a => a.Email == email);
            return user != null ? user.Password : string.Empty;
        }


        public async Task<string> ReturnSecurityQuestionForgottenID(string email)
        {
            var user = await _userRepository.GetAsync(a => a.Email == email);
            return user != null ? user.SecurityQuestion! : string.Empty;
        }

        public async Task DeleteUser(string id)
        {
            var user = await _userRepository.GetAsync(a => a.Id == id);
            _userRepository.Delete(user);
            await _unitOfWork.SaveAsync();
        }


        public async Task<UserDTO?> CreateUser(UserRegisterModel request)
        {
            var user = await _userRepository.GetAsync(a => a.Email == request.Email);
            if (user != null)
            {
                return null;
            }
            var newUser = new User
            {
                Email = request.Email,
                Password = request.Password,
                SecurityAnswer = request.SecurityAnswer,
                SecurityQuestion = request.SecurityQuestion,
            };
            var userReturned = await _userRepository.CreateAsync(newUser);
            return new UserDTO
            {
                Email = userReturned.Email,
                Password = userReturned.Password,
                Id = userReturned.Id,
            };

        }

        public async Task<UserDTO?> UpdateEmailPassword(string id, UserUpdateRequest request)
        {
            var user = await _userRepository.GetAsync(a => a.Id == id);
            if (user == null) return null;
            user.Email = request.Email ?? user.Email;
            if (request.Password != request.ConfirmPassword) return null;
            user.Password = request.Password;
            await _unitOfWork.SaveAsync();
            return new UserDTO
            {
                Email = user.Email,
                Password = user.Password,
                Id = id,
            };
        }

    }
}
