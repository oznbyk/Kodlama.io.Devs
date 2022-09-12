using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Queries.GetByIdProgrammingLanguage;
using Application.Features.ProgrammingLanguages.Rules;
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

namespace Application.Features.SocialMediaProfiles.Queries.GetByIdSocialMediaProfile
{
    public class GetByIdSocialMediaProfileQuery : IRequest<SocialMediaProfileGetByIdDto>
    {
        public int Id { get; set; }

        public class GetByIdSocialMediaProfileQueryHandler : IRequestHandler<GetByIdSocialMediaProfileQuery, SocialMediaProfileGetByIdDto>
        {
            private readonly ISocialMediaProfileRepository _socialMediaProfileRepository;
            private readonly IMapper _mapper;
            private readonly SocialMediaProfileBusinessRules _socialMediaProfileBusinessRules;

            public GetByIdSocialMediaProfileQueryHandler(ISocialMediaProfileRepository socialMediaProfileRepository, IMapper mapper, SocialMediaProfileBusinessRules socialMediaProfileBusinessRules)
            {
                _socialMediaProfileRepository = socialMediaProfileRepository;
                _mapper = mapper;
                _socialMediaProfileBusinessRules = socialMediaProfileBusinessRules;
            }

            public async Task<SocialMediaProfileGetByIdDto> Handle(GetByIdSocialMediaProfileQuery request, CancellationToken cancellationToken)
            {
                SocialMediaProfile? socialMediaProfile = await _socialMediaProfileRepository.GetAsync(p => p.Id == request.Id);

                _socialMediaProfileBusinessRules.SocialMediaProfileShouldExistWhenRequested(socialMediaProfile);

                SocialMediaProfileGetByIdDto socialMediaProfileGetByIdDto = _mapper.Map<SocialMediaProfileGetByIdDto>(socialMediaProfile);
                return socialMediaProfileGetByIdDto;
            }
        }
    }
}
