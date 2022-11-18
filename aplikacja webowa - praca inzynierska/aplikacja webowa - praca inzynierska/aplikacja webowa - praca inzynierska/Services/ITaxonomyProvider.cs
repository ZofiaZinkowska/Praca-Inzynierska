using aplikacja_webowa___praca_inzynierska.Model;
using System.Collections.Generic;

namespace aplikacja_webowa___praca_inzynierska.Services
{
    public interface ITaxonomyProvider
    {
        IEnumerable<TaxonomyItem> GetTaxonomy();

        void Insert(TaxonomyCode code);

        IEnumerable<TaxonomyItem> Find(string code);
    }
}
