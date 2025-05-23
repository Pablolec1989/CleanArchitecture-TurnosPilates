using ApplicationLayer;
using EnterpriseLayer_Entities;
using InterfaceAdapters___Models;
using InterfaceAdapters_Data;
using InterfaceAdapters_Presenters.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceAdapter_Repository
{
    public class RepositoryTurno : ICrudRepository<Turno>
    {
        private readonly AppDbContext _dbContext;

        public RepositoryTurno(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Turno> GetByIdAsync(int id)
        {

            var turnoModel = await _dbContext.Turnos
                .Include(t => t.Horario)
                .Include(t => t.Instructor)
                .Include(t => t.TurnoAlumno).ThenInclude(ta => ta.Alumno)
                .FirstOrDefaultAsync(t => t.Id ==id);
            
            if (turnoModel is null)
            {
                return null;
            }

            return new Turno
            {
                Id = turnoModel.Id,
                HorarioId = turnoModel.HorarioId,
                InstructorId = turnoModel.InstructorId,
                Horario = turnoModel.Horario == null ? null : new Horario
                {
                    Id = turnoModel.HorarioId,
                    Dia = turnoModel.Horario.Dia,
                    Hora = turnoModel.Horario.Hora
                },
                Instructor = turnoModel.Instructor == null ? null : new Instructor
                {
                    Nombre = turnoModel.Instructor.Nombre,
                    Apellido = turnoModel.Instructor.Apellido,
                    NroTelefono = turnoModel.Instructor.NroTelefono,
                    PorcentajeDePago = turnoModel.Instructor.PorcentajeDePago,
                },
                Alumnos = turnoModel.TurnoAlumno?.Select(ta => new TurnoAlumno
                {
                    TurnoId = ta.TurnoId,
                    AlumnoId = ta.AlumnoId,
                    Alumno = ta.Alumno == null ? null : new Alumno
                    {
                        Id = ta.Alumno.Id,
                        Nombre = ta.Alumno.Nombre,
                        Apellido = ta.Alumno.Apellido,
                        Observaciones = ta.Alumno.Observaciones,
                        NroTelefono = ta.Alumno.NroTelefono
                    }

                }).ToList() ?? new List<TurnoAlumno>(),
                Capacidad = turnoModel.Capacidad,
                Disponibilidad = turnoModel.Capacidad - (turnoModel.TurnoAlumno?.Count() ?? 0),
            };
        }

        public async Task<IEnumerable<Turno>> GetAllAsync()
        {
            //Convierto TurnoModel a Turno
            var turnos = await _dbContext.Turnos
                .Include(t => t.Horario)
                .Include(t => t.Instructor)
                .Include(t => t.TurnoAlumno).ThenInclude(ta => ta.Alumno)
                .ToListAsync();

            return turnos.Select(turnoModel => new Turno
            {
                Id = turnoModel.Id,
                HorarioId = turnoModel.HorarioId,
                InstructorId = turnoModel.InstructorId,
                Horario = turnoModel.Horario == null ? null : new Horario
                {
                    Id = turnoModel.HorarioId,
                    Dia = turnoModel.Horario.Dia,
                    Hora = turnoModel.Horario.Hora
                },
                Instructor = turnoModel.Instructor == null ? null : new Instructor
                {
                    Id = turnoModel.Instructor.Id,
                    Nombre = turnoModel.Instructor.Nombre,
                    Apellido = turnoModel.Instructor.Apellido,
                    NroTelefono = turnoModel.Instructor.NroTelefono,
                    PorcentajeDePago = turnoModel.Instructor.PorcentajeDePago,
                },
                Alumnos = turnoModel.TurnoAlumno?.Select(ta => new TurnoAlumno
                {
                    TurnoId = ta.TurnoId,
                    AlumnoId = ta.AlumnoId,
                    Alumno = ta.Alumno == null ? null : new Alumno
                    {
                        Id = ta.Alumno.Id,
                        Nombre = ta.Alumno.Nombre,
                        Apellido = ta.Alumno.Apellido,
                        Observaciones = ta.Alumno.Observaciones,
                        NroTelefono = ta.Alumno.NroTelefono
                    }
                }).ToList() ?? new List<TurnoAlumno>(),
                Capacidad = turnoModel.Capacidad,
                Disponibilidad = turnoModel.Capacidad - (turnoModel.TurnoAlumno?.Count() ?? 0),
            }).ToList();
        }

        public async Task AddAsync(Turno turno)
        {
            var turnoModel = new TurnoModel
            {
                HorarioId = turno.HorarioId,
                InstructorId = turno.InstructorId,
                Capacidad = turno.Capacidad,
                Disponibilidad = turno.Capacidad - (turno.Alumnos.Count()),
            };

            await _dbContext.AddAsync(turnoModel);
            await _dbContext.SaveChangesAsync();

            turno.Id = turnoModel.Id;

        }

        public async Task UpdateAsync(int id, Turno turno)
        {
            var turnoModel = await _dbContext.Turnos
                .Include(t => t.Horario)
                .Include(t => t.Instructor)
                .Include(t => t.TurnoAlumno)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (turnoModel != null)
            {
                turnoModel.InstructorId = turno.InstructorId;
                turnoModel.HorarioId = turno.HorarioId;
                turnoModel.Capacidad = turno.Capacidad;
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _dbContext.Turnos.Where(t => t.Id == id).ExecuteDeleteAsync();
            await _dbContext.SaveChangesAsync();
        }
    }
}

