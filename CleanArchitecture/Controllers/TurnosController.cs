using ApplicationLayer;
using ApplicationLayer.TurnoService;
using EnterpriseLayer_Entities;
using FluentValidation;
using FluentValidation.Results;
using InterfaceAdapter___Mappers.DTOs.Requests;
using InterfaceAdapters___Models;
using InterfaceAdapters_Presenters.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Controllers
{
    [ApiController]
    [Route("api/turnos")]
    public class TurnosController : ControllerBase
    {
        private readonly GetByIdTurnoService _getByIdTurnoService;
        private readonly GetTurnoService<Turno, TurnoViewModel> _getTurnoService;
        private readonly AddTurnoService<TurnoRequestDTO> _addTurnoService;
        private readonly UpdateTurnoService<TurnoRequestDTO> _updateTurnoService;
        private readonly DeleteTurnoService _deleteTurnoService;
        private readonly IValidator<TurnoRequestDTO> _validator;


        public TurnosController(GetByIdTurnoService getByIdTurnoService,
                                GetTurnoService<Turno, TurnoViewModel> getTurnoService,
                                AddTurnoService<TurnoRequestDTO> addTurnoService,
                                UpdateTurnoService<TurnoRequestDTO> updateTurnoService,
                                DeleteTurnoService deleteTurnoService,
                                IValidator<TurnoRequestDTO> validator)
        {
            _getByIdTurnoService = getByIdTurnoService;
            _getTurnoService = getTurnoService;
            _addTurnoService = addTurnoService;
            _updateTurnoService = updateTurnoService;
            _deleteTurnoService = deleteTurnoService;
            _validator = validator;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Turno>> Get(int id)
        {
            var turno = await _getByIdTurnoService.ExecuteAsync(id);

            if (turno is null)
            {
                return NotFound("turno no encontrado");
            }

            return Ok(turno);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TurnoViewModel>>> Get()
        {
            var turnos = await _getTurnoService.ExecuteAsync();
            return Ok(turnos);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] TurnoRequestDTO turnoRequest)
        {
            ValidationResult result = await _validator.ValidateAsync(turnoRequest);

            if (!result.IsValid)
            {
                return ValidationProblem(result.ToString());
            }

            await _addTurnoService.ExecuteAsync(turnoRequest);
            return Created();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put (int id, [FromForm] TurnoRequestDTO turnoRequest)
        {
            ValidationResult result = await _validator.ValidateAsync(turnoRequest);

            if (!result.IsValid)
            {
                return ValidationProblem(result.ToString());
            }
            try
            {
                await _updateTurnoService.ExecuteAsync(id, turnoRequest);
                return NoContent();
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _deleteTurnoService.ExecuteAsync(id);
                return NoContent();
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
