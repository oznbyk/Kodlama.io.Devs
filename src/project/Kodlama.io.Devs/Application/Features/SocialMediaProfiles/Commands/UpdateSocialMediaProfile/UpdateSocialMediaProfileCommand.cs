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

namespace Application.Features.SocialMediaProfiles.Commands.UpdateSocialMediaProfile
{
    public class UpdateSocialMediaProfileCommand : IRequest<UpdatedSocialMediaProfileDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public class UpdateSocialMediaProfileCommandHandler : IRequestHandler<UpdateSocialMediaProfileCommand, UpdatedSocialMediaProfileDto>
        {
            private readonly ISocialMediaProfileRepository _socialMediaProfileRepository;
            private readonly IMapper _mapper;
            private readonly SocialMediaProfileBusinessRules _socialMediaProfileBusinessRules;

            public UpdateSocialMediaProfileCommandHandler(ISocialMediaProfileRepository socialMediaProfileRepository, IMapper mapper, SocialMediaProfileBusinessRules socialMediaProfileBusinessRules)
            {
                _socialMediaProfileRepository = socialMediaProfileRepository;
                _mapper = mapper;
                _socialMediaProfileBusinessRules = socialMediaProfileBusinessRules;
            }

            public async Task<UpdatedSocialMediaProfileDto> Handle(UpdateSocialMediaProfileCommand request, CancellationToken cancellationToken)
            {
                SocialMediaProfile? socialMediaProfile = await _socialMediaProfileRepository.GetAsync(s => s.Id == request.Id);

                _socialMediaProfileBusinessRules.SocialMediaProfileShouldExistWhenRequested(socialMediaProfile);

                socialMediaProfile.Name = request.Name;

                SocialMediaProfile updatedSocialMediaProfile = await _socialMediaProfileRepository.UpdateAsync(socialMediaProfile);
                UpdatedSocialMediaProfileDto updatedSocialMediaProfileDto = _mapper.Map<UpdatedSocialMediaProfileDto>(updatedSocialMediaProfile);

                return updatedSocialMediaProfileDto;
            }
        }
    }
}
