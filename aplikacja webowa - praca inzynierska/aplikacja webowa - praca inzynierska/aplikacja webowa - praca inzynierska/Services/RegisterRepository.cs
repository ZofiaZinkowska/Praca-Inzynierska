using aplikacja_webowa___praca_inzynierska.Model;
using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace aplikacja_webowa___praca_inzynierska.Services
{
    public class RegisterRepository : IRegisterRepository
    {
        private const string RegisterEntryCollectionName = "RegisterEntries";
        private readonly ILiteDatabase _db;

        private readonly ILiteCollection<RegisterEntry> _registries;
        public RegisterRepository(ILiteDatabase db)
        {
            _db = db;
            _registries = _db.GetCollection<RegisterEntry>(RegisterEntryCollectionName);
        }

        public bool Delete(int id)
        {
            return _registries.Delete(id);
        }

        public IEnumerable<RegisterEntry> Find(Expression<Func<RegisterEntry, bool>> predicate)
        {
            return _registries.Find(predicate);
        }

        public IEnumerable<RegisterEntry> FindAll()
        {
            return _registries.FindAll();
        }

        public RegisterEntry FindById(int id)
        {
            return _registries.FindById(id);
        }

        public void Insert(RegisterEntry entry)
        {
            _registries.Insert(entry);
        }

        public void Update(RegisterEntry entry)
        {
            _registries.Update(entry);
        }
    }
}
