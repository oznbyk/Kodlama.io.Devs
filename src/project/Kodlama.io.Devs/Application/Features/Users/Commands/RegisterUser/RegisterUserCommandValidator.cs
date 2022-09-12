using Application.Features.ProgrammingLanguages.Commands.CreateProgrammingLanguage;
using Application.Features.Users.Commands.Register;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands.RegisterUser
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.UserForRegisterDto.Email).NotEmpty().EmailAddress();

        }
    }
}
