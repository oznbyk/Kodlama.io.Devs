using Application.Features.SocialMediaProfiles.Dtos;
using Application.Features.SocialMediaProfiles.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SocialMediaProfiles.Commands.CreateSocialMediaProfile
{
    public class CreateSocialMediaProfileCommand: IRequest<CreatedSocialMediaProfileDto>
    {
        public string Name { get; set; }
        public int UserId { get; set; }
        public string Url { get; set; }
        public class CreateSocialMediaProfileCommandHandler : IRequestHandler<CreateSocialMediaProfileCommand, CreatedSocialMediaProfileDto>
        {
            private readonly ISocialMediaProfileRepository _socialMediaProfileRepository;
            private readonly IMapper _mapper;
            private readonly SocialMediaProfileBusinessRules _socialMediaProfileBusinessRules;

            public CreateSocialMediaProfileCommandHandler(ISocialMediaProfileRepository socialMediaProfileRepository, IMapper mapper, SocialMediaProfileBusinessRules socialMediaProfileBusinessRules)
            {
                _socialMediaProfileRepository = socialMediaProfileRepository;
                _mapper = mapper;
                _socialMediaProfileBusinessRules = socialMediaProfileBusinessRules;
            }

            public async Task<CreatedSocialMediaProfileDto> Handle(CreateSocialMediaProfileCommand request, CancellationToken cancellationToken)
            {
                await _socialMediaProfileBusinessRules.SocialMediaProfileNameCannotBeDuplicatedWhenInserted(request.Name);

                SocialMediaProfile mappedSocialMediaProfile = _mapper.Map<SocialMediaProfile>(request);
                SocialMediaProfile createdSocialMediaProfile = await _socialMediaProfileRepository.AddAsync(mappedSocialMediaProfile);
                CreatedSocialMediaProfileDto createdSocialMediaProfileDto = _mapper.Map<CreatedSocialMediaProfileDto>(createdSocialMediaProfile);

                return createdSocialMediaProfileDto;
            }
        }
    }
}
