using MDCG.WebApi.Models;
using MDCG.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace MDCG.WebApi.Controllers
{
    public abstract class MDCGControllerBase<TEntity, TService> : ControllerBase
        where TEntity : class, IEntity
        where TService : IDataManagementService<TEntity> {
        private readonly TService _service;

        public MDCGControllerBase(TService repository) {
            _service = repository;
        }


        // GET: api/[controller]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TEntity>>> Get() {
            return await _service.GetAll();
        }

        // GET: api/[controller]/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TEntity>> Get(int id) {
            var entity = await _service.Get(id);
            if (entity == null) {
                return NotFound();
            }
            return entity;
        }

        // PUT: api/[controller]/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, TEntity entity) {
            if (id != entity.Id) {
                return BadRequest();
            }
            await _service.Update(entity);
            return NoContent();
        }

        // POST: api/[controller]
        [HttpPost]
        public async Task<ActionResult<TEntity>> Post(TEntity entity) {
            await _service.Add(entity);
            return CreatedAtAction("Get", new { id = entity.Id }, entity);
        }

        // DELETE: api/[controller]/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TEntity>> Delete(int id) {
            var entity = await _service.Delete(id);
            if (entity == null) {
                return NotFound();
            }
            return entity;
        }
    }
}
