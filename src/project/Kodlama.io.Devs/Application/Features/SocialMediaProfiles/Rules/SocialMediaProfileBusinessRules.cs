using Application.Services.Repositories;
using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SocialMediaProfiles.Rules
{
    public class SocialMediaProfileBusinessRules
    {
        private readonly ISocialMediaProfileRepository _socialMediaProfileRepository;

        public SocialMediaProfileBusinessRules(ISocialMediaProfileRepository socialMediaProfileRepository)
        {
            _socialMediaProfileRepository = socialMediaProfileRepository;
        }

        public async Task SocialMediaProfileNameCannotBeDuplicatedWhenInserted(string name)
        {
            IPaginate<SocialMediaProfile> result = await _socialMediaProfileRepository.GetListAsync(b => b.Name == name);
            if (result.Items.Any())
            {
                throw new BusinessException("Social media profile name already exists.");
            }
        }

        public void SocialMediaProfileShouldExistWhenRequested(SocialMediaProfile socialMediaProfile)
        {
            if (socialMediaProfile == null)
            {
                throw new BusinessException("Requested social media profile does not exist.");
            }
        }

    }
}
