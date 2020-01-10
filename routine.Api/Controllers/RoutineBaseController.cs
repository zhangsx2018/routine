using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using routine.Api.Helps;
using routine.Api.Services;
using routine.Api.Vo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace routine.Api.Controllers
{
    /**
     * T  dbcontext 对象
     * E entity对象
     * F dto mapper对象
     */
    [ApiController]
    public abstract class RoutineBaseController<T, E, F> : ControllerBase
        where T : DbContext
        where E : class
    {
        private readonly IMapper _mapper;
        private readonly DbContext _dbContext;
        private readonly Repository<T, E> _repository;

        public RoutineBaseController(IMapper mapper, T t, Repository<T, E> repository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _dbContext = t ?? throw new ArgumentNullException(nameof(_dbContext));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        [HttpGet]
        public virtual async Task<Result> Get([FromQuery] PageInfo info, [FromQuery] E entity, [FromQuery] List<Condition> conditions)
        {


            int itemcount;
            var _query = _repository.GetAsync(info, entity, conditions, out itemcount);
            //提取query，外部可修改，做连接等复杂查询用
            _query = ExpandQuery(_query);
            var list = await _query.ToListAsync();
            var result = _mapper.Map<List<F>>(list);
            Page page = new Page();
            if ((info.page == 0) && (info.rows == 0))
            {
                return Result.SUCCESS().setData(result);

            }
            page.total = itemcount;
            page.rows = result;
            return Result.SUCCESS().setData(page);
        }
        #region 删除

        [HttpDelete("{ids}")]
        public virtual async Task<Result> Delete(
            [FromRoute]
            [ModelBinder(BinderType  = typeof(ArrayModelBingder))]
            IEnumerable<long> ids)
        {
            if (ids == null)
            {
                return Result.SUCCESS();
            }
            int count = 0;
            foreach (var id in ids)
            {
                var entity = await _dbContext.Set<E>().FindAsync(id);
                if (entity != null)
                    _repository.Delete(entity);
            }
            count = await _repository.SaveAsync();

            return Result.SUCCESS().setData(count);


        }
        [HttpDelete]
        public virtual async Task<Result> Delete([FromQuery] E entity)
        {
            var id = typeof(E).GetProperties().Where(x => x.Name == "id").FirstOrDefault().GetValue(entity);
            entity = await _dbContext.Set<E>().FindAsync(id);

            int count = 0;

            _repository.Delete(entity);
            count = await _repository.SaveAsync();
            return Result.SUCCESS().setData(count);
        }
        #endregion
        #region 修改
        [HttpPut]
        [Consumes("application/json")]
        public virtual async Task<Result> UpdateJson([FromBody] E entity)
        {

            return await UpdateEntity(entity);
        }
        [HttpPut]
        public virtual async Task<Result> Update([FromForm] E entity)
        {
            return await UpdateEntity(entity);
        }

        private async Task<Result> UpdateEntity(E entity)
        {
            var id = typeof(E).GetProperties().Where(x => x.Name == "id").FirstOrDefault().GetValue(entity);
            var model = _dbContext.Entry<E>(entity);
            model.State = EntityState.Unchanged;


            foreach (PropertyInfo property in typeof(E).GetProperties())
            {
                var value = property.GetValue(entity);
                if (value != null && value.ToString() != "" && property.Name != "id")
                {
                    model.Property(property.Name).IsModified = true;
                }
            }
            int count = await _repository.SaveAsync();

            model.State = EntityState.Detached;
            var newEntity = await _dbContext.Set<E>().FindAsync(id);
            var entityMapper = _mapper.Map<F>(newEntity);
            return Result.SUCCESS().setData(entityMapper);
        }
        #endregion
        #region 新增
        [HttpPost]
       // [Consumes("application/json")]
        public virtual async Task<Result> AddJson([FromBody] List<E> entities)
        {

            foreach (var entity in entities)
            {
                _repository.Add(entity);
            }
            int count = await _repository.SaveAsync();
            if (count ==1)
            {
                Result.SUCCESS().setData(entities[0]);
            }
            return Result.SUCCESS().setData(entities);

        }
        [HttpPost]
        [Consumes("multipart/form-data")]
        public virtual async Task<Result> Add([FromForm] E entity)
        {
            _repository.Add(entity);
            await _repository.SaveAsync();
            return Result.SUCCESS().setData(entity);
        }
        #endregion


        public virtual IQueryable<E> ExpandQuery(IQueryable<E> query)
        {
            return query;
        }
    }
}
