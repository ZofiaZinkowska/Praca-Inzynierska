using aplikacja_webowa___praca_inzynierska.Contract;
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

        [HttpGet("Details")]
        public ActionResult<TaxonomyItemDetails> Details(string id)
        {
            var items = _taxonomyProvider.GetTaxonomy();
            var matchingItem = items.FirstOrDefault(x => x.TaxonID == id);
            if (matchingItem == null)
                return NotFound();
            return Ok(matchingItem);
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
            var results = matchingItems.Select(x => new SearchTaxonomyItem 
            {
                TaxonomyID = x.TaxonID,
                ScientificNameAuthor = x.ScientificNameAuthor,
                ScientificName = x.ScientificName,
            });
            return Ok(results);
        }
    }
}
