using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using routine.Api.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using routine.Api.Entities;
using routine.Api.Models;
using routine.Api.Vo;
using System.Reflection;
using routine.Api.Services;

namespace routine.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeviceMainController : RoutineBaseController<DeviceDbContext, DeviceMain,DeviceMainDto>
    {
        private readonly DeviceDbContext _deviceDbContext;
        private readonly IMapper _mapper;
        private readonly Repository<DeviceDbContext, DeviceMain> _repository;

        public DeviceMainController(IMapper mapper, DeviceDbContext deviceDbContext, Repository<DeviceDbContext, DeviceMain> repository) 
            : base(mapper, deviceDbContext, repository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _deviceDbContext = deviceDbContext ?? throw new ArgumentNullException(nameof(_deviceDbContext));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet]
        public override Task<Result> get([FromQuery] PageInfo info, [FromQuery] DeviceMain entity, [FromQuery] List<Condition> conditions)
        {
           
            return base.get(info, entity, conditions);
        }

        //[HttpGet]
        //public async Task<Result> GetDevices([FromQuery] DeviceMain device)
        //{
        //    // throw new Exception("test error!!!!!!!!!!!!!!!!!!!");

        //    var items = _deviceDbContext.deviceMains as IQueryable<DeviceMain>;

        //    long[] a = { 27,29};

        //    if (!string.IsNullOrWhiteSpace(device.name))
        //    {
        //      //  items = items.Where(x => x.name.Contains(device.name) && a.Contains(x.id) );

        //    }

        //    List<Condition> conditions = new List<Condition>();
        //    foreach(PropertyInfo property in typeof(DeviceMain).GetProperties())
        //    {
        //        var obj=property.GetValue(device);
        //        if (obj != null)
        //        {
        //            conditions.Add(new Condition { Key = property.Name, QuerySymbol = ConditionSymbolEnum.Equal, Value = obj });
        //        }
        //    }
        //    int itemcount = 0;
        //    var _query = QueryableExtensions.QueryConditions( items ,conditions).Pager(1,2,out itemcount);

        //    var devices = await _query
        //        .Include(x => x.DeviceFiles)
        //        .Include(x => x.DeviceType)
        //        .Include(x => x.DeviceStatus)
        //        .ToListAsync();
        //   //var devices = await _deviceDbContext.deviceMains.FirstOrDefaultAsync();
        //    var deviceDtos = _mapper.Map<List<DeviceMainDto>>(devices);
        //    return Result.SUCCESS()
        //        .setData(deviceDtos)
        //        .setMsg("Ok的雅痞");
        //}
    }
}
