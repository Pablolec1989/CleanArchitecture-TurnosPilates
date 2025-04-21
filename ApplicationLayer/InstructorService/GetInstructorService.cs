using EnterpriseLayer_Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.InstructorService
{
    public class GetInstructorService<TEntity, TOutput>
    {
        private readonly IRepository<TEntity> _instructorRepository;
        private readonly IPresenter<TEntity, TOutput> _presenter;

        public GetInstructorService(IRepository<TEntity> instructorRepository, IPresenter<TEntity, TOutput> presenter)
        {
            _instructorRepository = instructorRepository;
            _presenter = presenter;
        }

        public async Task<IEnumerable<TOutput>> ExecuteAsync()
        {
            var instructores = await _instructorRepository.GetAllAsync();
            return _presenter.Present(instructores);
        }
    }
}
