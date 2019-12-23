using AspNetCorePropertyPro.Api.Resources;
using AspNetCorePropertyPro.Api.Resources.PropertyType;
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

            CreateMap<SavePropertyTypeResource, PropertyType>();
        }
    }
}
