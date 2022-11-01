using aplikacja_webowa___praca_inzynierska.Model;
using LiteDB;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Security.Cryptography.X509Certificates;

namespace aplikacja_webowa___praca_inzynierska.Controllers
{
    [Route("Register")]

    public class RegisterController : Controller
    {
        private const string DbName = "Register.db";
        private const string RegisterEntryCollectionName = "RegisterEntries";

        [HttpPost("Save")]
        public ActionResult<RegisterEntry> Save([FromBody]RegisterEntry registerEntry)
        {
            //Open database connection
            using (var db = new LiteDatabase(DbName))
            {
                //Get RegisterEntry collection
                var collection = db.GetCollection<RegisterEntry>(RegisterEntryCollectionName);

                registerEntry.ModificationDate = DateTime.UtcNow;

                //Update RegisterEntry
                var updated = collection.Update(registerEntry);
                if (!updated)
                    return NotFound();

                return registerEntry;
            }
        }

        [HttpGet("Get")]

        public ActionResult<RegisterEntry> Get(int id)
        {
            //Open database connection
            using (var db = new LiteDatabase(DbName))
            {
                //Get RegisterEntry collection
                var collection = db.GetCollection<RegisterEntry>(RegisterEntryCollectionName);

                //Find item by id
                var registerEntry = collection.FindById(id);

                if (registerEntry is null) return NotFound();

                return registerEntry;
            }
        }

        [HttpDelete("Delete")]

        public ActionResult<bool> Delete(int id)
        {
            //Open database connection
            using (var db = new LiteDatabase(DbName))
            {
                //Get RegisterEntry collection
                var collection = db.GetCollection<RegisterEntry>(RegisterEntryCollectionName);

                //Delete item by id
                var isDeleted = collection.Delete(id);

                return isDeleted;
            }
        }

        [HttpGet("List")]

        public ActionResult<IEnumerable<RegisterEntry>> List(string keyword, string sortBy, string sortDirection)
        {
            //Open database connection
            using (var db = new LiteDatabase(DbName))
            {
                //Get RegisterEntry collection
                var collection = db.GetCollection<RegisterEntry>(RegisterEntryCollectionName);

                //List all items
                var registerEntries = string.IsNullOrEmpty(keyword)? 
                    collection.FindAll().ToList():
                    Search(collection, keyword);

                registerEntries = Sort(registerEntries, sortBy, sortDirection); 

                return Ok(registerEntries);
            }
        }

        [HttpPut("Add")]
        public ActionResult<RegisterEntry> Add([FromBody]NewRegisterEntry newRegisterEntry)
        {
            //Open database connection
            using (var db = new LiteDatabase(DbName))
            {
                //Get RegisterEntry collection
                var collection = db.GetCollection<RegisterEntry>(RegisterEntryCollectionName);
                var now = DateTime.UtcNow;
                var registerEntry = new RegisterEntry
                {
                    AddDate=now,
                    ModificationDate=now,
                    Name=newRegisterEntry.Name,
                };
                //Insert RegisterEntry 
                collection.Insert(registerEntry);

                return registerEntry;
            }
        }

        private IEnumerable<RegisterEntry> Search(ILiteCollection<RegisterEntry> collection, string keyword)
        {
           return collection.Find(x => x.Name.Contains(keyword, System.StringComparison.OrdinalIgnoreCase)).ToList();
        }

        private IEnumerable<RegisterEntry> Sort(IEnumerable<RegisterEntry> collection, string sortBy, string sortDirection)
        {
           var sortFunction = ParseSortBy(sortBy);
            if (sortFunction == null)
            {
                return collection;
            }

            return sortDirection?.ToLower() switch
            {
                "asc" => collection.OrderBy(sortFunction),
                "desc" => collection.OrderByDescending(sortFunction),
                _ => collection,
            };
        }

        private Func<RegisterEntry,Object> ParseSortBy(string sortBy)
        {
           return sortBy?.ToLower() switch
           {
               "name" => (x => x.Name),
               "date" => (x => x.AddDate),
               _ => null,
           };
        }
    }
}
