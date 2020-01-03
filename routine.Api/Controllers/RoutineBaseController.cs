using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using routine.Api.Services;
using routine.Api.Vo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace routine.Api.Controllers
{
    [ApiController]
    public abstract class RoutineBaseController<T,E,F> : ControllerBase
        where T:DbContext
        where E:class
    {
        private readonly IMapper _mapper;
        private readonly DbContext _dbContext;
        private readonly Repository<T,E> _repository;

        public RoutineBaseController(IMapper mapper, T t, Repository<T, E> repository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _dbContext = t ?? throw new ArgumentNullException(nameof(_dbContext));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet]
        public virtual async Task<Result> get([FromQuery] PageInfo info, [FromQuery] E entity ,[FromQuery] List<Condition> conditions)
        {
           
          
            int itemcount;
            var _query = _repository.GetAsync(info, entity, conditions, out itemcount);

            var list = await _query.ToListAsync();
            var result = _mapper.Map<List<F>>(list);
            Page page = new Page();
            if (  (info.page == 0) && (info.rows == 0))
            {
                return Result.SUCCESS().setData(result);

            }
            page.total = itemcount;
            page.rows = result;
            return Result.SUCCESS().setData(page);

           
        }

    }
}
