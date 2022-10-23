using aplikacja_webowa___praca_inzynierska.Model;
using aplikacja_webowa___praca_inzynierska.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

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

        [HttpGet("Items")]
        public ActionResult<IEnumerable<TaxonomyItem>> Items()
        {
            var items = _taxonomyProvider.GetTaxonomy();
            return Ok(items);
        }
    }
}
