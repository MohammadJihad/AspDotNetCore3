using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Business.Helpers;
using Test.Business.Services.Interfaces;
using Test.DataAccess.Repositories.Interfaces;
using Test.Entities.Entities;

namespace Test.Business.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public User Login(string username, string password)
        {
            var user = _userRepository.GetByUserName(username);

            if (user == null)
                return null;

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }
        public async Task<User> GetById(int key)
        {
            return await _userRepository.GetById(key);
        }
        public async Task<User> Create(User entity)
        {
            // validation
            if (string.IsNullOrWhiteSpace(entity.Password))
                throw new AppException("Password is required");

            if (UserExists(entity.LoginName))
                throw new AppException("LoginName \"" + entity.LoginName + "\" is already taken");


            byte[] passwordSalt;
            CreatePasswordHash(entity.Password, out byte[] passwordHash, out passwordSalt);

            entity.PasswordHash = passwordHash;
            entity.PasswordSalt = passwordSalt;

            var isCreate = await _userRepository.Create(entity);
            if (!isCreate)
            {
                throw new AppException("Faild create user");
            }
            return entity;
        }
        public IQueryable<User> GetAll()
        {
            return _userRepository.GetAll();
        }
        public async Task<bool> Delete(User user)
        {
            return await _userRepository.Delete(user);
        }
        public User GetByUserName(string userName)
        {
            return _userRepository.GetByUserName(userName);
        }
        public Task<bool> Update(User entity)
        {
            var user = _userRepository.GetByUserName(entity.LoginName);
            if (user == null)
                throw new AppException("User not found");

            if (entity.LoginName != user.LoginName)
            {
                // LoginName has changed so check if the new LoginName is already taken
                var isUserUsed = _userRepository.GetByUserName(entity.LoginName);
                if (isUserUsed != null)
                    throw new AppException("LoginName " + entity.LoginName + " is already taken");
            }

            // update password if it was entered
            if (!string.IsNullOrWhiteSpace(entity.Password))
            {
                CreatePasswordHash(entity.Password, out byte[] passwordHash, out byte[] passwordSalt);
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            return _userRepository.Update(entity);
        }
        public Task<bool> UpdateImagePath(string LoginName, string imagePath)
        {
            var user = _userRepository.GetByUserName(LoginName);
            if (user == null)
                throw new AppException("User not found");
            user.ProfilePicture = imagePath;

            return _userRepository.Update(user);
        }
        public bool UserExists(string username)
        {
            var result = _userRepository.GetByUserName(username);
            if (result != null)
                return true;
            return false;
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
                return true;
            }
        }
        public void DeleteByUserName(string loginName)
        {
            var user = _userRepository.GetByUserName(loginName);
            _userRepository.Delete(user);
        }
    }
}
