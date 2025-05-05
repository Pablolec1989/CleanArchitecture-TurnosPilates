
using ApplicationLayer;
using ApplicationLayer.AlumnoService;
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
    [Route("api/alumnos")]
    public class AlumnosController : ControllerBase
    {
        private readonly GetByIdAlumnoService<Alumno, AlumnoEnTurnoViewModel> _getByIdAlumnoService;
        private readonly GetAlumnoService<Alumno, AlumnoViewModel> _getAlumnoService;
        private readonly AddAlumnoService<AlumnoRequestDTO> _addAlumnoService;
        private readonly UpdateAlumnoService<AlumnoRequestDTO> _updateAlumnoService;
        private readonly DeleteAlumnoService _deleteAlumnoService;
        private readonly IValidator<AlumnoRequestDTO> _validator;

        public AlumnosController(GetByIdAlumnoService<Alumno, AlumnoEnTurnoViewModel> getByIdAlumnoService,
                                GetAlumnoService<Alumno, AlumnoViewModel> getAlumnoService,
                                AddAlumnoService<AlumnoRequestDTO> addAlumnoService,
                                UpdateAlumnoService<AlumnoRequestDTO> updateAlumnoService,
                                DeleteAlumnoService deleteAlumnoService,
                                IValidator<AlumnoRequestDTO> validator)
        {
            _getByIdAlumnoService = getByIdAlumnoService;
            _getAlumnoService = getAlumnoService;
            _validator = validator;
            _addAlumnoService = addAlumnoService;
            _updateAlumnoService = updateAlumnoService;
            _deleteAlumnoService = deleteAlumnoService;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Alumno>> Get(int id)
        {
            var alumno = await _getByIdAlumnoService.ExecuteAsync(id);

            if (alumno is null)
            {
                throw new ValidationException("Alumno no encontrado");
            }

            return Ok(alumno);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Alumno>>> Get()
        {
            var alumnos = await _getAlumnoService.ExecuteAsync();
            return Ok(alumnos);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] AlumnoRequestDTO alumnoRequest)
        {
            ValidationResult result = await _validator.ValidateAsync(alumnoRequest);

            if (!result.IsValid)
            {
                return ValidationProblem(result.ToString());
            }

            await _addAlumnoService.ExecuteAsync(alumnoRequest);
            return Created();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromForm] AlumnoRequestDTO alumnoRequest)
        {
            ValidationResult result = await _validator.ValidateAsync(alumnoRequest);

            if (!result.IsValid)
            {
                return ValidationProblem(result.ToString());
            }

            try
            {
                await _updateAlumnoService.ExecuteAsync(id, alumnoRequest);
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
                await _deleteAlumnoService.ExecuteAsync(id);
                return NoContent();
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }

        }



    }
}
