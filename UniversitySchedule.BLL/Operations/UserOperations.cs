using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UniversitySchedule.Core.Abstractions;
using UniversitySchedule.Core.Abstractions.OperationInterfaces;
using UniversitySchedule.Core.Entites;
using UniversitySchedule.Core.ExceptionTypes;

namespace UniversitySchedule.BLL.Operations
{
    public class UserOperations : IUserOperations
    {
        private readonly IRepositoryManager _repositoryManager;

        public UserOperations(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<User> AuthenticateAsync(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = await _repositoryManager.Users.GetSingleAsync(x => x.UserName == username);

            if (user == null)
                return null;

            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _repositoryManager.Users.FindAsync();
        }
        public User GetById(int id)
        {
            return _repositoryManager.Users.GetSingle(x => x.Id == id);
        }
        public async Task<User> GetByIdAsync(int id)
        {
            return await _repositoryManager.Users.GetSingleAsync(x => x.Id == id);
        }
        public async Task<User> CreateAsync(User user, string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new LogicException("Password is required");

            if (_repositoryManager.Users.Any(x => x.UserName == user.UserName))
                throw new LogicException("Username \"" + user.UserName + "\" is already taken");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _repositoryManager.Users.Add(user);
            await _repositoryManager.CompleteAsync();

            return user;
        }
        public async Task UpdateAsync(User userParam, string password = null)
        {
            var user = await _repositoryManager.Users.GetSingleAsync(x => x.Id == userParam.Id);

            if (user == null)
                throw new LogicException("User not found");

            user.FirstName = userParam.FirstName ?? user.FirstName;
            user.LastName = userParam.LastName ?? user.LastName;
            user.Phone = userParam.Phone ?? user.Phone;
            user.PostalCode = userParam.PostalCode ?? user.PostalCode;
            user.State = userParam.State ?? user.State;
            user.Address = userParam.Address ?? user.Address;
            user.Email = userParam.Email ?? user.Email;
            user.City = userParam.City ?? user.City;
            user.Country = userParam.Country ?? user.Country;

            if (!string.IsNullOrWhiteSpace(password))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            _repositoryManager.Users.Update(user);
            await _repositoryManager.CompleteAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var user = await _repositoryManager.Users.GetSingleAsync(x => x.Id == id);
            if (user != null)
            {
                _repositoryManager.Users.Remove(user);
                await _repositoryManager.CompleteAsync();
            }
        }

        #region -- helper methods --

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }

        #endregion -- helper methods --
    }
}
