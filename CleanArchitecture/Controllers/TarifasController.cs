using ApplicationLayer.TarifaService;
using EnterpriseLayer_Entities;
using FluentValidation;
using FluentValidation.Results;
using InterfaceAdapter___Mappers.DTOs.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Controllers
{
    [ApiController]
    [Route("api/tarifas")]
    public class TarifasController : ControllerBase
    {
        private readonly GetByIdTarifaService _getByIdTarifaService;
        private readonly GetTarifaService _getTarifaService;
        private readonly AddTarifaService<TarifaRequestDTO> _addTarifaService;
        private readonly UpdateTarifaService<TarifaRequestDTO> _updateTarifaService;
        private readonly DeleteTarifaService _deleteTarifaService;
        private readonly IValidator<TarifaRequestDTO> _validator;

        public TarifasController(GetByIdTarifaService getByIdTarifaService,
                                GetTarifaService getTarifaService,
                                AddTarifaService<TarifaRequestDTO> addTarifaService,
                                UpdateTarifaService<TarifaRequestDTO> updateTarifaService,
                                DeleteTarifaService deleteTarifaService,
                                IValidator<TarifaRequestDTO> validator)
        {
            _getByIdTarifaService = getByIdTarifaService;
            _getTarifaService = getTarifaService;
            _addTarifaService = addTarifaService;
            _updateTarifaService = updateTarifaService;
            _deleteTarifaService = deleteTarifaService;
            _validator = validator;
        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<Tarifa>> GetById(int id)
        {
            return Ok(await _getByIdTarifaService.ExecuteAsync(id));
            
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tarifa>>> Get()
        {
            return Ok(await _getTarifaService.Get());
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] TarifaRequestDTO tarifaRequestDTO)
        {
            ValidationResult result = await _validator.ValidateAsync(tarifaRequestDTO);

            if (!result.IsValid)
            {
                return ValidationProblem(result.ToString());
            }

            await _addTarifaService.ExecuteAsync(tarifaRequestDTO);
            return Created();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id,[FromForm] TarifaRequestDTO tarifaRequestDTO)
        {
            ValidationResult result = await _validator.ValidateAsync(tarifaRequestDTO);

            if (!result.IsValid)
            {
                return ValidationProblem(result.ToString());
            }

            try
            {
                await _updateTarifaService.ExecuteAsync(id, tarifaRequestDTO);
                return NoContent();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _deleteTarifaService.ExecuteAsync(id);
            return NoContent();
        }


    }
}
