using ApplicationLayer.HorarioService;
using EnterpriseLayer_Entities;
using FluentValidation;
using FluentValidation.Results;
using InterfaceAdapter___Mappers.DTOs.Requests;
using InterfaceAdapters_Presenters.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using ValidationException = FluentValidation.ValidationException;
using ValidationResult = FluentValidation.Results.ValidationResult;

namespace CleanArchitecture.Controllers
{
    [ApiController]
    [Route("api/horarios")]
    public class HorariosControllers : ControllerBase
    {
        private readonly GetByIdHorarioService _getByIdHorarioService;
        private readonly GetHorarioService<Horario, HorarioViewModel> _getHorarioService;
        private readonly AddHorarioService<HorarioRequestDTO> _addHorarioService;
        private readonly UpdateHorarioService<HorarioRequestDTO> _updateHorarioService;
        private readonly DeleteHorarioService _deleteHorarioService;
        private readonly IValidator<HorarioRequestDTO> _validator;

        public HorariosControllers(GetByIdHorarioService getByIdHorarioService,
                                    GetHorarioService<Horario, HorarioViewModel> getHorarioService,
                                    AddHorarioService<HorarioRequestDTO> addHorarioService,
                                    UpdateHorarioService<HorarioRequestDTO> updateHorarioService,
                                    DeleteHorarioService deleteHorarioService,
                                    IValidator<HorarioRequestDTO> validator)
        {
            _getByIdHorarioService = getByIdHorarioService;
            _getHorarioService = getHorarioService;
            _addHorarioService = addHorarioService;
            _updateHorarioService = updateHorarioService;
            _deleteHorarioService = deleteHorarioService;
            _validator = validator;
        }

        [HttpGet("{id:int}")]
        public async Task<Horario> Get(int id)
        {
            return await _getByIdHorarioService.ExecuteAsync(id);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HorarioViewModel>>> Get()
        {
            var horarios = await _getHorarioService.ExecuteAsync();
            return Ok(horarios);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] HorarioRequestDTO horarioRequestDTO)
        {
            ValidationResult result = await _validator.ValidateAsync(horarioRequestDTO);

            if (!result.IsValid)
            {
                return ValidationProblem(result.ToString());
            }

            await _addHorarioService.ExecuteAsync(horarioRequestDTO);
            return Created();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, HorarioRequestDTO horarioRequestDTO)
        {
            ValidationResult result = await _validator.ValidateAsync(horarioRequestDTO);

            if (!result.IsValid)
            {
                return ValidationProblem(result.ToString());
            }

            try
            {
                await _updateHorarioService.ExecuteAsync(id, horarioRequestDTO);
                return NoContent();

            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _deleteHorarioService.ExecuteAsync(id);
                return NoContent();
            }
            catch (ValidationException ex)
            {

                return BadRequest(ex.Message);
            }

        }
    }
}
