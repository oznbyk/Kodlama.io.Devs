using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Core.Security.Entities;
using Core.Security.Hashing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Rules
{
    public class UserBusinessRules
    {
        private readonly IUserRepository _userRepository;

        public UserBusinessRules(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task UserEmailCannotBeDuplicated(string email)
        {
            IPaginate<User> result = await _userRepository.GetListAsync(u => u.Email == email);
            if (result.Items.Any())
                throw new BusinessException("Email already exists");
        }

        public void UserShouldExistWhenRequested(User user)
        {
            if (user == null)
            {
                throw new BusinessException("User does not exist");
            }
        }

        public void ComparePassword(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            var result = HashingHelper.VerifyPasswordHash(password, passwordHash, passwordSalt);
            if (result == false)
            {
                throw new BusinessException("Password does not match!");
            }
        }



    }
}
