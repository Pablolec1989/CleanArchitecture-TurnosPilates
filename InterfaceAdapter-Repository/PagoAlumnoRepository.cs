using ApplicationLayer.PagoAlumnoService;
using EnterpriseLayer_Entities;
using InterfaceAdapters___Models;
using InterfaceAdapters_Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceAdapter_Repository
{
    public class PagoAlumnoRepository : IPagoAlumnoRepository
    {
        private readonly AppDbContext _dBcontext;

        public PagoAlumnoRepository(AppDbContext context)
        {
            _dBcontext = context;
        }

        public async Task AddAsync(PagoAlumno pagoAlumno)
        {
            var pagoAlumnoModel = new PagoAlumnoModel
            {
                AlumnoId = pagoAlumno.AlumnoId,
                TarifaId = pagoAlumno.TarifaId,
                Monto = pagoAlumno.Monto,
            };
            await _dBcontext.PagoAlumno.AddAsync(pagoAlumnoModel);
            await _dBcontext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int alumnoId)
        {
            var pagoAlumno = await _dBcontext.PagoAlumno.FirstOrDefaultAsync(pa => pa.AlumnoId == alumnoId);
            if (pagoAlumno != null)
            {
                _dBcontext.PagoAlumno.Remove(pagoAlumno);
                await _dBcontext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<PagoAlumno>> GetAllAsync()
        {
            var pagosAlumnos = await _dBcontext.PagoAlumno
                .Include(pa => pa.Tarifa)
                .Include(pa => pa.Alumno)
                .ToListAsync();

            return pagosAlumnos.Select(pa => new PagoAlumno
            {
                AlumnoId = pa.AlumnoId,
                Alumno = new Alumno
                {
                    Nombre = pa.Alumno.Nombre,
                    Apellido = pa.Alumno.Apellido,
                    NroTelefono = pa.Alumno.NroTelefono,
                },
                TarifaId = pa.TarifaId,
                Tarifa = new Tarifa
                {
                    Frecuencia_turno = pa.Tarifa.Frecuencia_turno,
                    Precio = pa.Tarifa.Precio,
                } ?? null,
                Monto = pa.Monto,
                Pagado = pa.Pagado,

            }).ToList();
        }

        public async Task<PagoAlumno?> GetByAlumnoIdAsync(int alumnoId)
        {
            var pagoAlumnoModel = await _dBcontext.PagoAlumno
                .Include(pa => pa.Tarifa)
                .Include(pa => pa.Alumno)
                .FirstOrDefaultAsync(pa => pa.AlumnoId == alumnoId);

            if (pagoAlumnoModel == null)
            {
                return null;
            }

            return new PagoAlumno
            {
                AlumnoId = pagoAlumnoModel.AlumnoId,
                Alumno = new Alumno
                {
                    Nombre = pagoAlumnoModel.Alumno.Nombre,
                    Apellido = pagoAlumnoModel.Alumno.Apellido,
                    NroTelefono = pagoAlumnoModel.Alumno.NroTelefono,
                },
                TarifaId = pagoAlumnoModel.TarifaId,
                Tarifa = new Tarifa
                {
                    Frecuencia_turno = pagoAlumnoModel.Tarifa.Frecuencia_turno,
                    Precio = pagoAlumnoModel.Tarifa.Precio,

                } ?? null,

                Monto = pagoAlumnoModel.Monto,
                Pagado = pagoAlumnoModel.Pagado,
            };
        }

        public async Task UpdateAsync(PagoAlumno pagoAlumno)
        {
            var pagoAlumnoModel = await _dBcontext.PagoAlumno
                .FirstOrDefaultAsync(pa => pa.AlumnoId == pagoAlumno.AlumnoId);

            if (pagoAlumnoModel != null)
            {
                pagoAlumnoModel.TarifaId = pagoAlumno.TarifaId;
                pagoAlumnoModel.Monto = pagoAlumno.Monto;
                pagoAlumnoModel.Pagado = pagoAlumno.Pagado;
            }

            await _dBcontext.SaveChangesAsync();

        }
    }
}
