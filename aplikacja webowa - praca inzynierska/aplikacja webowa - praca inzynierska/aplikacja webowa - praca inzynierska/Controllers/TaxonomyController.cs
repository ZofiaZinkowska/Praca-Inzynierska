using aplikacja_webowa___praca_inzynierska.Contract;
using aplikacja_webowa___praca_inzynierska.Model;
using aplikacja_webowa___praca_inzynierska.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace aplikacja_webowa___praca_inzynierska.Controllers
{
    [Route("Taxonomy")]
    public class TaxonomyController : Controller
    {
        private readonly ITaxonomyProvider _taxonomyProvider;

        public TaxonomyController(ITaxonomyProvider taxonomyProvider)
        {
            _taxonomyProvider = taxonomyProvider;
        }

        [HttpPut("AddCode")]
        public ActionResult AddCode([FromBody] SaveTaxonomyCodeRequest saveTaxonomyCodeRequest)
        {
            var taxonomyCode = new TaxonomyCode
            {
                Code = saveTaxonomyCodeRequest.Code,
                TaxonomyID = saveTaxonomyCodeRequest.TaxonomyID,
            };
            _taxonomyProvider.Insert(taxonomyCode);
            return NoContent();
        }

        [HttpGet("Find")]
        public ActionResult<IEnumerable<SearchTaxonomyItem>> Find(string code)
        {
            var matchingItems = _taxonomyProvider.Find(code);
            var results = matchingItems.Select(x => MapSearchItem(x));
            return Ok(results);
        }

        [HttpGet("Details")]
        public ActionResult<TaxonomyItemDetails> Details(string id)
        {
            var items = _taxonomyProvider.GetTaxonomy();
            var matchingItem = items.FirstOrDefault(x => x.TaxonomyID == id);
            if (matchingItem == null)
                return NotFound();
            var result = new TaxonomyItemDetails
            {
                //TODO: dodać link na stronce
                AcceptedNameUsageID = matchingItem.AcceptedNameUsageID,
                Family = matchingItem.Family,
                Genus = matchingItem.Genus,
                Id = matchingItem.TaxonomyID,
                NamePublishedIn = matchingItem.NamePublishedIn,
                ScientificName = matchingItem.ScientificName,
                ScientificNameAuthor = matchingItem.ScientificNameAuthor,
                SpecificEpithet = matchingItem.SpecificEpithet,
                Subfamily = matchingItem.Subfamily,
                TaxonomicStatus = matchingItem.TaxonomicStatus
            };
            return Ok(result);
        }

        [HttpGet("Search")]
        public ActionResult<IEnumerable<SearchTaxonomyItem>> Search(string keyword, int? count)
        {
            var items = _taxonomyProvider.GetTaxonomy();
            var matchingItems = items.Where(x => 
                x.ScientificName.Contains(keyword, System.StringComparison.OrdinalIgnoreCase) ||
                x.ScientificNameAuthor.Contains(keyword, System.StringComparison.OrdinalIgnoreCase))
                .OrderBy(x => x.ScientificName)
                .ThenBy(x => x.ScientificNameAuthor)
                .AsEnumerable();
            if (count.HasValue)
                matchingItems = matchingItems.Take(count.Value);
            var results = matchingItems.Select(x => MapSearchItem(x));
            return Ok(results);
        }
        private static SearchTaxonomyItem MapSearchItem(TaxonomyItem x)
        {
            return new SearchTaxonomyItem
            {
                TaxonomyID = x.TaxonomyID,
                ScientificNameAuthor = x.ScientificNameAuthor,
                ScientificName = x.ScientificName,
            };
        }
    }
}
