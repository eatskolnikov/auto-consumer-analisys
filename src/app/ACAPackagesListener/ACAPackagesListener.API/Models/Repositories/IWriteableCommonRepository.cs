using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ACAPackagesListener.API.Models.Repositories;

namespace ACAPackagesListener.API.Models.Repositories
{
    public interface IWriteableCommonRepository<TElement> : IReadonlyCommonRepository<TElement>
    {
        void Add(TElement element);

        void Update(TElement element);

        void Remove(TElement element);
    }
}
