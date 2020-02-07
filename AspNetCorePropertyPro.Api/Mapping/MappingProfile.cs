using AspNetCorePropertyPro.Api.Resources;
using AspNetCorePropertyPro.Api.Resources.DealType;
using AspNetCorePropertyPro.Api.Resources.Flags;
using AspNetCorePropertyPro.Api.Resources.Propertys;
using AspNetCorePropertyPro.Api.Resources.PropertyType;
using AspNetCorePropertyPro.Api.Resources.Users;
using AspNetCorePropertyPro.Core.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCorePropertyPro.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Resource to domian model
            CreateMap<RegisterResource, ApplicationUser>()
                .ForMember(d => d.UserName, opt => opt.MapFrom(s => s.Email));
            CreateMap<EditUserResource, ApplicationUser>();

            CreateMap<SavePropertyTypeResource, PropertyType>();
            CreateMap<SaveDealTypeResource, DealType>();
            CreateMap<SaveEditPropertyResource, Property>();
            CreateMap<PropertyImageResource, PropertyImage>();
            CreateMap<FlagSaveEditResource, Flag>();



            //Domain Model to Resource
            CreateMap<ApplicationUser, UserResource>();
            CreateMap<PropertyImage, PropertyImageResource>();
            CreateMap<PropertyType, PropertyTypeResource>();
            CreateMap<DealType, DealTypeResource>();
            CreateMap<Property, PropertyDetailResource>()
                .ForMember(d => d.Photos, opt => opt.MapFrom(s => s.PropertyImages));
            CreateMap<Property, PropertyResource>()
                .ForMember(d => d.ImageUrl, opt => 
                    opt.MapFrom(s => s.PropertyImages.SingleOrDefault(p => p.IsMain).Secure_Url));

            CreateMap<Flag, FlagResource>();
        }
    }
}
