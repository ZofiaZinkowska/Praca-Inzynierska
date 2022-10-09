using aplikacja_webowa___praca_inzynierska.Model;
using LiteDB;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;

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

                //Upsert RegisterEntry (insert or update existing)
                collection.Upsert(registerEntry);

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

        public ActionResult<IEnumerable<RegisterEntry>> List()
        {
            //Open database connection
            using (var db = new LiteDatabase(DbName))
            {
                //Get RegisterEntry collection
                var collection = db.GetCollection<RegisterEntry>(RegisterEntryCollectionName);

                //List all items
                var registerEntries = collection.FindAll().ToList();

                return Ok(registerEntries);
            }
        }

    }
}
