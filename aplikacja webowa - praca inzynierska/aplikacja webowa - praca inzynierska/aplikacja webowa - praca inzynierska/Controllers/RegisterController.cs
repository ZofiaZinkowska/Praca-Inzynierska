using aplikacja_webowa___praca_inzynierska.Model;
using LiteDB;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System;
using aplikacja_webowa___praca_inzynierska.Contract;
using aplikacja_webowa___praca_inzynierska.Services;

namespace aplikacja_webowa___praca_inzynierska.Controllers
{
    [Route("Register")]

    public class RegisterController : Controller
    {

        public RegisterController(ITaxonomyProvider taxonomyProvider, IRegisterRepository registerRepository)
        {
            _taxonomyProvider = taxonomyProvider;
            _registerRepository = registerRepository;
        }

        private readonly ITaxonomyProvider _taxonomyProvider;
        private readonly IRegisterRepository _registerRepository;

        [HttpPost("Save")]
        public ActionResult<RegisterEntry> Save(int id, [FromBody]SaveRegisterEntryRequest saveRegisterEntryRequest)
        {
                var registerEntry = _registerRepository.FindById(id);
                if (registerEntry == null)
                    return NotFound();

                //registerEntry.Name = saveRegisterEntryRequest.Name;
                registerEntry.ScientificNameID =saveRegisterEntryRequest.ScientificNameID;

                registerEntry.ModificationDate = DateTime.UtcNow;

                //Update RegisterEntry
                _registerRepository.Update(registerEntry);

                return registerEntry;
        }

        [HttpGet("Get")]

        public ActionResult<RegisterEntry> Get(int id)
        {
                //Find item by id
                var registerEntry = _registerRepository.FindById(id);

                if (registerEntry is null) return NotFound();

                return registerEntry;
        }

        [HttpDelete("Delete")]

        public ActionResult<bool> Delete(int id)
        {
                //Delete item by id
                var isDeleted = _registerRepository.Delete(id);

                return isDeleted;
        }

        [HttpGet("List")]

        public ActionResult<IEnumerable<ListRegisterEntriesItem>> List(string keyword, string sortBy, string sortDirection)
        {
                //List all items
                var registerEntries = string.IsNullOrEmpty(keyword)? 
                    _registerRepository.FindAll().ToList():
                    Search(keyword);

                registerEntries = Sort(registerEntries, sortBy, sortDirection);
                var listRegisterEntriesItems = GetListItems(registerEntries);

                return Ok(listRegisterEntriesItems);
        }

        private IEnumerable<ListRegisterEntriesItem> GetListItems(IEnumerable<RegisterEntry> registerEntries)
        {
            var scientificNameIDs = registerEntries.Select(x => x.ScientificNameID).Distinct().ToHashSet();
            var taxonomyItems = _taxonomyProvider.GetTaxonomy()
                .Where(x => scientificNameIDs.Contains(x.ScientificNameID))
                .ToDictionary(x => x.ScientificNameID);
            var items = registerEntries.Select(entry => {
                var item = new ListRegisterEntriesItem
                {
                    AddDate = entry.AddDate,
                    Id = entry.Id,
                    ModificationDate = entry.ModificationDate,
                    //Name = entry.Name,
                };
                if (entry.ScientificNameID != null && taxonomyItems.TryGetValue(entry.ScientificNameID, out var taxonomyItem))
                {
                    item.ScientificName = taxonomyItem.ScientificName;
                    item.ScientificNameAuthor = taxonomyItem.ScientificNameAuthor;
                }
                return item;
            });
            return items;
        }

        [HttpPut("Add")]
        public ActionResult<RegisterEntry> Add([FromBody]SaveRegisterEntryRequest saveRegisterEntryRequest)
        {
                var now = DateTime.UtcNow;
                var registerEntry = new RegisterEntry
                {
                    AddDate=now,
                    ModificationDate=now,
                    //Name= saveRegisterEntryRequest.Name,
                    ScientificNameID=saveRegisterEntryRequest.ScientificNameID,
                };
                //Insert RegisterEntry 
                _registerRepository.Insert(registerEntry);

                return registerEntry;
        }

        private IEnumerable<RegisterEntry> Search(string keyword)
        {
            return _registerRepository.Find(x => x.ScientificNameID.Contains(keyword, System.StringComparison.OrdinalIgnoreCase)).ToList();
        }

        private static IEnumerable<RegisterEntry> Sort(IEnumerable<RegisterEntry> collection, string sortBy, string sortDirection)
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

        private static Func<RegisterEntry,object> ParseSortBy(string sortBy)
        {
           return sortBy?.ToLower() switch
           {
               //"name" => (x => x.Name),
               "date" => (x => x.AddDate),
               _ => null,
           };
        }
    }
}
