using aplikacja_webowa___praca_inzynierska.Model;
using LiteDB;
using Microsoft.AspNetCore.Mvc;


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
    }
}
