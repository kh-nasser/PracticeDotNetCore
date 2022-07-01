using ProjectRepositoryPattern_DataLayer.Services;
using ProjectRepositoryPattern_ModelClass.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectRepositoryPattern_DataLayer.Context
{
    public class UnitOfWork : IDisposable
    {
        private ProjectRepositoryPatternContext _db = new ProjectRepositoryPatternContext();

        #region Person
        private MyGenericRepository<Person> _personRepository;
        public MyGenericRepository<Person> PersonRepository
        {
            get
            {
                if (_personRepository == null)
                {
                    _personRepository = new MyGenericRepository<Person>(_db);
                }
                return _personRepository;
            }
        }
        #endregion

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
