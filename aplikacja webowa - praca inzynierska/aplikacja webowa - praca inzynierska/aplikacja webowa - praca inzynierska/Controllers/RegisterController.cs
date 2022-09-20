using aplikacja_webowa___praca_inzynierska.Model;
using LiteDB;
using Microsoft.AspNetCore.Mvc;


namespace aplikacja_webowa___praca_inzynierska.Controllers
{
    public class RegisterController : Controller
    {
        private const string DbName = "Register.db";
        private const string RegisterEntryCollectionName = "RegisterEntries";

        [HttpPost]
        public ActionResult<RegisterEntry> Save(RegisterEntry registerEntry)
        {
            //Open database connection
            using (var db = new LiteDatabase(DbName))
            {
                //Get RegisterEntry collection
                var collection = db.GetCollection<RegisterEntry>(RegisterEntryCollectionName);

                //Insert new RegisterEntry
                collection.Insert(registerEntry);

                return registerEntry;
            }
        }

        public ActionResult<RegisterEntry> Get(int id)
        {
            //Open database connection
            using (var db = new LiteDatabase(DbName))
            {
                //Get RegisterEntry collection
                var collection = db.GetCollection<RegisterEntry>(RegisterEntryCollectionName);

                //Find item by id
                var registerEntry = collection.FindOne(e => e.Id == id);

                return registerEntry;
            }
        }
    }
}
