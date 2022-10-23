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

        [HttpGet("Search")]
        public ActionResult<IEnumerable<TaxonomyItem>> Search(string keyword)
        {
            var items = _taxonomyProvider.GetTaxonomy();
            var matchingItems = items.Where(x => x.ScientificName.Contains(keyword, System.StringComparison.OrdinalIgnoreCase));
            return Ok(matchingItems);
        }
    }
}
