using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using routine.Api.Data;
using routine.Api.Entities;
using routine.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace routine.Api.Controllers
{
    [Route("api/deviceMain/{belongDeviceId}/deviceFiles")]
    public class DeviceFilesController :ControllerBase
    {
        private readonly IMapper  _mapper;
        private readonly DeviceDbContext _deviceDbContext;
        public DeviceFilesController(IMapper mapper, DeviceDbContext deviceDbContext)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _deviceDbContext = deviceDbContext ?? throw new ArgumentNullException(nameof(deviceDbContext));
        }

        [HttpGet]
        public async Task<Result> getFileForDeviceId(long belongDeviceId)
        {

            if (! await _deviceDbContext.deviceMains.AnyAsync(x => x.id == belongDeviceId))
            {
                return Result.FAIL().setMsg("该设备不存在");
            }

            var files = await _deviceDbContext.deviceFiles.Where(x => x.belongDeviceId == belongDeviceId).ToListAsync();
            var fileDtos = _mapper.Map<IEnumerable<DeviceFileDto>>(files);
            return Result.SUCCESS().setData(fileDtos);

        }
    }
}
