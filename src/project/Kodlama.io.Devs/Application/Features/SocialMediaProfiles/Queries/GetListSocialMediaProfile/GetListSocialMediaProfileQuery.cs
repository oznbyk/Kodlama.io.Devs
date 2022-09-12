using Application.Features.SocialMediaProfiles.Models;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Requests;
using Core.Persistence.Paging;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SocialMediaProfiles.Queries.GetListSocialMediaProfile
{
    public class GetListSocialMediaProfileQuery : IRequest<SocialMediaProfileListModel>
    {
        public PageRequest PageRequest { get; set; }

        public class GetListTechnologyQueryHandler : IRequestHandler<GetListSocialMediaProfileQuery, SocialMediaProfileListModel>
        {
            private readonly IMapper _mapper;
            private readonly ISocialMediaProfileRepository _socialMediaProfileRepository;

            public GetListTechnologyQueryHandler(IMapper mapper, ISocialMediaProfileRepository socialMediaProfileRepository)
            {
                _mapper = mapper;
                _socialMediaProfileRepository = socialMediaProfileRepository;
            }

            public async Task<SocialMediaProfileListModel> Handle(GetListSocialMediaProfileQuery request, CancellationToken cancellationToken)
            {
                IPaginate<SocialMediaProfile> socialMediaProfiles = await _socialMediaProfileRepository.GetListAsync(include: m => m.Include(c => c.User), index: request.PageRequest.Page, size: request.PageRequest.PageSize);

                SocialMediaProfileListModel mappedModels = _mapper.Map<SocialMediaProfileListModel>(socialMediaProfiles);
                return mappedModels;
            }
        }
    }
}
