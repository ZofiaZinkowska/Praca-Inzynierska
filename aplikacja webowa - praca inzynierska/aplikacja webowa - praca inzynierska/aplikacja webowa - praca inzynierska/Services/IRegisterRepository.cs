using aplikacja_webowa___praca_inzynierska.Model;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace aplikacja_webowa___praca_inzynierska.Services
{
    public interface IRegisterRepository
    {
        RegisterEntry FindById(int id);
        void Update(RegisterEntry entry);
        bool Delete(int id);
        IEnumerable<RegisterEntry> FindAll();
        IEnumerable<RegisterEntry> Find(Expression<Func<RegisterEntry, bool>> predicate);
        void Insert(RegisterEntry entry);
    }
}
