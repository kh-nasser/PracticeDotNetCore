using eshop_webapi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eshop_webapi.Contracts
{
    public interface ISalesPersonsRepository
    {
        IEnumerable<SalePerson> GetAll();
        Task<SalePerson> Add(SalePerson sales);
        Task<SalePerson> Find(int id);
        Task<SalePerson> Update(SalePerson sales);
        Task<SalePerson> Remove(int id);
        Task<bool> IsExists(int id);
    }
}
