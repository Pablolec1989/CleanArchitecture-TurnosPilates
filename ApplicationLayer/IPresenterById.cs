using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer
{
    public interface IPresenterById<TEntity, TOutput>
    {
        public TOutput Present(TEntity entity);
    }

}
