using AutoMapper;
using routine.Api.Entities;
using routine.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace routine.Api.Profiles
{
    public class DeviceFileProfile :Profile
    {
        public DeviceFileProfile()
        {
            CreateMap<DeviceFile, DeviceFileDto>();
        }
    }
}
