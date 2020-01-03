using AutoMapper;
using routine.Api.Entities;
using routine.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace routine.Api.Profiles
{
    public class DeviceMainProfile  : Profile
    {
        public DeviceMainProfile()
        {
            CreateMap<DeviceMain, DeviceMainDto>()
                .ForMember(
                      //dto类，字段名
                    dest => dest.deviceTypeName,
                    //目标实体 字段名
                    opt => opt.MapFrom(src => src.DeviceType.name))
                .ForMember(
                dest => dest.deviceStatus,
                opt => opt.MapFrom(src => src.DeviceStatus.name)
                );
        }
    }
}
