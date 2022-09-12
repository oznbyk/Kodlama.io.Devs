using Application.Features.Users.Dtos;
using Application.Features.Users.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.JWT;
using MediatR;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Queries
{
    public class LoginUserQuery : IRequest<LoginResponseDto>
    {
        public UserForLoginDto UserForLoginDto { get; set; }

        public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, LoginResponseDto>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;
            private readonly ITokenHelper _tokenHelper;
            private readonly UserBusinessRules _userBusinessRules;

            public LoginUserQueryHandler(IUserRepository userRepository, IMapper mapper, ITokenHelper tokenHelper, UserBusinessRules userBusinessRules)
            {
                _userRepository = userRepository;
                _mapper = mapper;
                _tokenHelper = tokenHelper;
                _userBusinessRules = userBusinessRules;
            }

            public async Task<LoginResponseDto> Handle(LoginUserQuery request, CancellationToken cancellationToken)
            {
                User? userToLogin = await _userRepository.GetAsync(u => u.Email == request.UserForLoginDto.Email);

                _userBusinessRules.UserShouldExistWhenRequested(userToLogin);
                _userBusinessRules.ComparePassword(request.UserForLoginDto.Password,userToLogin.PasswordHash,userToLogin.PasswordSalt);

                List<OperationClaim> operationClaims = new List<OperationClaim>();

                foreach (var userOperationClaim in userToLogin.UserOperationClaims)
                {
                    operationClaims.Add(userOperationClaim.OperationClaim);
                }

                AccessToken accessToken = _tokenHelper.CreateToken(userToLogin, operationClaims);
                LoginResponseDto loginResponseDto = _mapper.Map<LoginResponseDto>(accessToken);

                return loginResponseDto;
            }
        }
    }
}
