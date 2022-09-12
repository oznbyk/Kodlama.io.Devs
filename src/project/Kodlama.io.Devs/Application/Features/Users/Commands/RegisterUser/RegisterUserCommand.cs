using Application.Features.Users.Dtos;
using Application.Features.Users.Rules;
using Application.Services;
using Application.Services.Repositories;
using AutoMapper;
using Core.Security.Dtos;
using Core.Security.Entities;
using Core.Security.Hashing;
using Core.Security.JWT;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands.Register
{
    public class RegisterUserCommand : IRequest<RegisterResponseDto>
    {
        public UserForRegisterDto UserForRegisterDto { get; set; }

        public class RegisterCommandHandler : IRequestHandler<RegisterUserCommand, RegisterResponseDto>
        {
            private readonly IMapper _mapper;
            private readonly IUserRepository _userRepository;
            private readonly ITokenHelper _tokenHelper;
            private readonly UserBusinessRules _userBusinessRules;

            public RegisterCommandHandler(IMapper mapper, IUserRepository userRepository, ITokenHelper tokenHelper, UserBusinessRules userBusinessRules)
            {
                _mapper = mapper;
                _userRepository = userRepository;
                _tokenHelper = tokenHelper;
                _userBusinessRules = userBusinessRules;
            }

            public async Task<RegisterResponseDto> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
            {
                await _userBusinessRules.UserEmailCannotBeDuplicated(request.UserForRegisterDto.Email);

               HashingHelper.CreatePasswordHash(request.UserForRegisterDto.Password, out byte[] passwordHash, out byte[] passwordSalt);

                User user = new()
                {
                    AuthenticatorType = 0,
                    Email = request.UserForRegisterDto.Email,
                    FirstName = request.UserForRegisterDto.FirstName,
                    LastName = request.UserForRegisterDto.LastName,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    Status = true
                };

                User registeredUser = await _userRepository.AddAsync(user);
                RegisterResponseDto result = _mapper.Map<RegisterResponseDto>(registeredUser);
                //var token =  _tokenHelper.CreateToken(registeredUser,)
                return result;

            }
        }
    }
}
