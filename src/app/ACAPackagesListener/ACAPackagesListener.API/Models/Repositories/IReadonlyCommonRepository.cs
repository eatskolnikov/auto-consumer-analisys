using System.Collections.Generic;
using System.Linq;

namespace ACAPackagesListener.API.Models.Repositories
{
    public interface IReadonlyCommonRepository<out TElement>
    {
        IEnumerable<TElement> GetAll();
        TElement GetById<TKey>(TKey id);
    }
}
