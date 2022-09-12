using Application.Features.ProgrammingLanguages.Dtos;
using Application.Features.ProgrammingLanguages.Models;
using Application.Features.SocialMediaProfiles.Commands.CreateSocialMediaProfile;
using Application.Features.SocialMediaProfiles.Commands.DeleteSocialMediaProfile;
using Application.Features.SocialMediaProfiles.Commands.UpdateSocialMediaProfile;
using Application.Features.SocialMediaProfiles.Dtos;
using Application.Features.SocialMediaProfiles.Models;
using AutoMapper;
using Core.Persistence.Paging;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SocialMediaProfiles.Profiles
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<CreateSocialMediaProfileCommand, SocialMediaProfile>().ReverseMap();
            CreateMap<DeleteSocialMediaProfileCommand, SocialMediaProfile>().ReverseMap();
            CreateMap<UpdateSocialMediaProfileCommand, SocialMediaProfile>().ReverseMap();
            CreateMap<CreatedSocialMediaProfileDto, SocialMediaProfile>().ReverseMap();
            CreateMap<DeletedSocialMediaProfileDto, SocialMediaProfile>().ReverseMap();
            CreateMap<UpdatedSocialMediaProfileDto, SocialMediaProfile>().ReverseMap();
            CreateMap<IPaginate<SocialMediaProfile>, SocialMediaProfileListModel>().ReverseMap();
            CreateMap<SocialMediaProfile, SocialMediaListDto>().ReverseMap();
            CreateMap<SocialMediaProfile, SocialMediaProfileGetByIdDto>().ReverseMap();

        }
    }
}
