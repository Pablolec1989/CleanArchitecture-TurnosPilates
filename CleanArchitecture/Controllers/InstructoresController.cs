

using ApplicationLayer;
using ApplicationLayer.InstructorService;
using EnterpriseLayer_Entities;
using FluentValidation;
using FluentValidation.Results;
using InterfaceAdapter___Mappers.DTOs.Requests;
using InterfaceAdapters_Presenters.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Controllers
{
    [ApiController]
    [Route("api/instructores")]
    public class InstructoresController : ControllerBase
    {
        private readonly GetByIdInstructorService _getByIdInstructorService;
        private readonly GetInstructorService<Instructor, InstructorViewModel> _getInstructorService;
        private readonly AddInstructorService<InstructorRequestDTO> _addInstructorService;
        private readonly UpdateInstructorService<InstructorRequestDTO> _updateInstructorService;
        private readonly DeleteInstructorService _deleteInstructorService;
        private readonly IRepository<Instructor> _repository;
        private readonly IMapper<InstructorRequestDTO, Instructor> _mapper;
        private readonly IValidator<InstructorRequestDTO> _validator;

        public InstructoresController(  GetByIdInstructorService getByIdInstructorService,
                                        GetInstructorService<Instructor, InstructorViewModel> getInstructorService,
                                        AddInstructorService<InstructorRequestDTO> addInstructorService,
                                        UpdateInstructorService<InstructorRequestDTO> updateInstructorService,
                                        DeleteInstructorService deleteInstructorService,
                                        IRepository<Instructor> repository,
                                        IMapper<InstructorRequestDTO, Instructor> mapper,
                                        IValidator<InstructorRequestDTO> validator)
        {
            _getByIdInstructorService = getByIdInstructorService;
            _getInstructorService = getInstructorService;
            _addInstructorService = addInstructorService;
            _updateInstructorService = updateInstructorService;
            _deleteInstructorService = deleteInstructorService;
            _repository = repository;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpGet("{id:int}")]
        public async Task<Instructor> Get(int id)
        {
            return await _getByIdInstructorService.ExecuteAsync(id);
        }

        [HttpGet]
        public async Task<IEnumerable<InstructorViewModel>> Get()
        {
            return await _getInstructorService.ExecuteAsync();
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] InstructorRequestDTO instructorRequestDTO)
        {
            ValidationResult result = await _validator.ValidateAsync(instructorRequestDTO);

            if (!result.IsValid)
            {
                return ValidationProblem(result.ToString());
            }

            await _addInstructorService.ExecuteAsync(instructorRequestDTO);
            return Created();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromForm] InstructorRequestDTO instructorRequestDTO)
        {
            ValidationResult result = await _validator.ValidateAsync(instructorRequestDTO);

            if(!result.IsValid)
            {
                return ValidationProblem(result.ToString());
            }

            try
            {
                await _updateInstructorService.ExecuteAsync(id, instructorRequestDTO);
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
                await _deleteInstructorService.ExecuteAsync(id);
                return NoContent();
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
