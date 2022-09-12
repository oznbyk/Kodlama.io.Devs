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

namespace Application.Features.SocialMediaProfiles.Commands.DeleteSocialMediaProfile
{
    public class DeleteSocialMediaProfileCommand : IRequest<DeletedSocialMediaProfileDto>
    {
        public int Id { get; set; }
        public class DeleteSocialMediaProfileCommandHandler : IRequestHandler<DeleteSocialMediaProfileCommand, DeletedSocialMediaProfileDto>
        {
            private readonly ISocialMediaProfileRepository _socialMediaProfileRepository;
            private readonly IMapper _mapper;
            private readonly SocialMediaProfileBusinessRules _socialMediaProfileBusinessRules;

            public DeleteSocialMediaProfileCommandHandler(ISocialMediaProfileRepository socialMediaProfileRepository, IMapper mapper, SocialMediaProfileBusinessRules socialMediaProfileBusinessRules)
            {
                _socialMediaProfileRepository = socialMediaProfileRepository;
                _mapper = mapper;
                _socialMediaProfileBusinessRules = socialMediaProfileBusinessRules;
            }

            public async Task<DeletedSocialMediaProfileDto> Handle(DeleteSocialMediaProfileCommand request, CancellationToken cancellationToken)
            {
                SocialMediaProfile socialMediaProfile = await _socialMediaProfileRepository.GetAsync(p => p.Id == request.Id);

                _socialMediaProfileBusinessRules.SocialMediaProfileShouldExistWhenRequested(socialMediaProfile);

                SocialMediaProfile deletedSocialMediaProfile = await _socialMediaProfileRepository.DeleteAsync(socialMediaProfile);
                DeletedSocialMediaProfileDto deletedSocialMediaProfileDto = _mapper.Map<DeletedSocialMediaProfileDto>(deletedSocialMediaProfile);

                return deletedSocialMediaProfileDto;
            }
        }
    }
}
