using aplikacja_webowa___praca_inzynierska.Model;
using System.Collections.Generic;
using System.IO;

namespace aplikacja_webowa___praca_inzynierska.Services
{
    public class TaxonomyProvider : ITaxonomyProvider
    {
        public IEnumerable<TaxonomyItem> GetTaxonomy()
        {
            using var input = new StreamReader(File.OpenRead("content/classification.txt"));
            input.ReadLine();
            var items = new List<TaxonomyItem>();
            while (!input.EndOfStream)
            {
                var line = input.ReadLine();
                if (string.IsNullOrEmpty(line))continue;
                var values = line.Split('\t');
                var item = new TaxonomyItem
                {
                    TaxonID = values[0],
                    ScientificNameID = values[1],
                    LocalID = values[2],
                    ScientificName = values[3],
                    TaxonRank = values[4],
                };
                items.Add(item);
            }
            return items;
        }
    }
}
