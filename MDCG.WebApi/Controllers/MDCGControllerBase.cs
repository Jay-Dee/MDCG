using MDCG.WebApi.Models;
using MDCG.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace MDCG.WebApi.Controllers
{
    public abstract class MDCGControllerBase<TEntity, TDatamanagementService, TDataValidationService> : ControllerBase
        where TEntity : class, IEntity
        where TDatamanagementService : IDataManagementService<TEntity> 
        where TDataValidationService : IDataValidationService<TEntity> {

        private readonly TDatamanagementService _datamanagementService;
        private readonly TDataValidationService _dataValidationService;
        private readonly ILogger<TEntity> _logger;

        public MDCGControllerBase(TDatamanagementService datamanagementService, TDataValidationService dataValidationService, ILogger<TEntity> logger) {
            _datamanagementService = datamanagementService;
            _dataValidationService = dataValidationService;
            _logger = logger;
        }


        // GET: api/[controller]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TEntity>>> Get() {
            try {
                return Ok(await _datamanagementService.GetAll());
            } catch(Exception ex) {
                _logger.LogError(ex.Message, ex.StackTrace);
                return Problem(ex.Message);
            }
        }

        // GET: api/[controller]/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TEntity>> Get(int id) {
            try {
                var entity = await _datamanagementService.Get(id);
                if (entity == null) {
                    return NotFound($"Unable to find with id = {id}");
                } else {
                    return Ok(entity);
                }
            } catch(Exception ex) {
                _logger.LogError(ex.Message, ex.StackTrace);
                return Problem(ex.Message);
            }
        }

        // PUT: api/[controller]/5
        [HttpPut("{id}")]
        // TODO : Pending invesitgation for test Update_ForValidExistingFxSpotMarketData_UpdatesSuccessfully
        public async Task<IActionResult> Put(int id, TEntity entity) {
            try {
                if (id != entity.Id) {
                    return BadRequest();
                } else {
                    if (_dataValidationService.Validate(entity).ValidityStatus == ValidationResultType.Success) {
                        var result = await _datamanagementService.Update(entity);
                        return NoContent();
                    } else {
                        return ValidationProblem("Failed validation");
                    }
                }
            } catch(Exception ex) {
                _logger.LogError(ex.Message, ex.StackTrace);
                return Problem(ex.Message);
            }
        }

        // POST: api/[controller]
        [HttpPost]
        public async Task<ActionResult<TEntity>> Post(TEntity entity) {
            try {
                if (_dataValidationService.Validate(entity).ValidityStatus == ValidationResultType.Success) {
                    var result = await _datamanagementService.Add(entity);
                    return CreatedAtAction("Get", new { id = entity.Id }, result);
                } else {
                    return ValidationProblem("Failed validation");
                }
            } catch(Exception ex) {
                _logger.LogError(ex.Message, ex.StackTrace);
                return Problem(ex.Message);
            }
        }

        // DELETE: api/[controller]/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            try {
                var entity = await _datamanagementService.Delete(id);
                if (entity == null) {
                    return NotFound();
                } else {
                    return NoContent();
                }
            } catch(Exception ex) {
                _logger.LogError(ex.Message, ex.StackTrace);
                return Problem(ex.Message);
            }
        }
    }
}
