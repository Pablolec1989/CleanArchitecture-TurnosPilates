using ApplicationLayer;
using ApplicationLayer.AlumnoService;
using ApplicationLayer.HorarioService;
using ApplicationLayer.InstructorService;
using ApplicationLayer.TarifaService;
using ApplicationLayer.TurnoService;
using CleanArchitecture.Middlewares;
using CleanArchitecture.Validators;
using EnterpriseLayer_Entities;
using FluentValidation;
using FluentValidation.AspNetCore;
using InterfaceAdapter___Mappers;
using InterfaceAdapter___Mappers.DTOs;
using InterfaceAdapter___Mappers.DTOs.Requests;
using InterfaceAdapter_Repository;
using InterfaceAdapters_Data;
using InterfaceAdapters_Presenters.Presenters;
using InterfaceAdapters_Presenters.ViewModels;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Controladores
builder.Services.AddControllers();

//Validadores
builder.Services.AddValidatorsFromAssemblyContaining<AlumnoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<InstructorValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<HorarioValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<TurnoValidator>();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddFluentValidationClientsideAdapters();


// Dependencias
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

//Alumnos
builder.Services.AddScoped<GetByIdAlumnoService<Alumno, AlumnoEnTurnoViewModel>>();
builder.Services.AddScoped<GetAlumnoService<Alumno, AlumnoViewModel>>();
builder.Services.AddScoped<AddAlumnoService<AlumnoRequestDTO>>();
builder.Services.AddScoped<UpdateAlumnoService<AlumnoRequestDTO>>();
builder.Services.AddScoped<DeleteAlumnoService>();
builder.Services.AddScoped<ICrudRepository<Alumno>, RepositoryAlumno>();
builder.Services.AddScoped<IPresenter<Alumno, AlumnoViewModel>, AlumnoPresenter>();
builder.Services.AddScoped<IPresenterById<Alumno, AlumnoEnTurnoViewModel>, AlumnoEnTurnoPresenter>();
builder.Services.AddScoped<IMapper<AlumnoRequestDTO, Alumno>, AlumnoRequestMapper>();

//Instructores
builder.Services.AddScoped<GetByIdInstructorService>();
builder.Services.AddScoped<GetInstructorService<Instructor, InstructorViewModel>>();
builder.Services.AddScoped<AddInstructorService<InstructorRequestDTO>>();
builder.Services.AddScoped<UpdateInstructorService<InstructorRequestDTO>>();
builder.Services.AddScoped<DeleteInstructorService>();
builder.Services.AddScoped<ICrudRepository<Instructor>, RepositoryInstructor>();
builder.Services.AddScoped<IPresenter<Instructor, InstructorViewModel>, InstructorPresenter>();
builder.Services.AddScoped<IMapper<InstructorRequestDTO, Instructor>, InstructorMapper>();

//Horarios
builder.Services.AddScoped<GetByIdHorarioService>();
builder.Services.AddScoped<GetHorarioService<Horario, HorarioViewModel>>();
builder.Services.AddScoped<AddHorarioService<HorarioRequestDTO>>();
builder.Services.AddScoped<UpdateHorarioService<HorarioRequestDTO>>();
builder.Services.AddScoped<DeleteHorarioService>();
builder.Services.AddScoped<ICrudRepository<Horario>, RepositoryHorario>();
builder.Services.AddScoped<IPresenter<Horario, HorarioViewModel>, HorarioPresenter>();
builder.Services.AddScoped<IMapper<HorarioRequestDTO, Horario>, HorarioMapper>();

//Turnos
builder.Services.AddScoped<GetByIdTurnoService>();
builder.Services.AddScoped<GetTurnoService<Turno, TurnoViewModel>>();
builder.Services.AddScoped<AddTurnoService<TurnoRequestDTO>>();
builder.Services.AddScoped<UpdateTurnoService<TurnoRequestDTO>>();
builder.Services.AddScoped<DeleteTurnoService>();
builder.Services.AddScoped<ICrudRepository<Turno>, RepositoryTurno>();
builder.Services.AddScoped<IPresenter<Turno, TurnoViewModel>, TurnoPresenter>();
builder.Services.AddScoped<IMapper<TurnoRequestDTO, Turno>, TurnoMapper>();

//TurnoAlumno
builder.Services.AddScoped<ITurnoAlumnoRepository, RepositoryTurnoAlumno>();

//Tarifas
builder.Services.AddScoped<GetByIdTarifaService>();
builder.Services.AddScoped<GetTarifaService>();
builder.Services.AddScoped<AddTarifaService<TarifaRequestDTO>>();
builder.Services.AddScoped<UpdateTarifaService<TarifaRequestDTO>>();
builder.Services.AddScoped<DeleteTarifaService>();
builder.Services.AddScoped<ICrudRepository<Tarifa>, RepositoryTarifa>();
builder.Services.AddScoped<IMapper<TarifaRequestDTO, Tarifa>, TarifaRequestMapper>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionMiddleware>();

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();

