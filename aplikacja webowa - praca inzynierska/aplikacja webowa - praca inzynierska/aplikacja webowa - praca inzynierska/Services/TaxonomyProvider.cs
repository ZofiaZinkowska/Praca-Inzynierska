using aplikacja_webowa___praca_inzynierska.Model;
using LiteDB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace aplikacja_webowa___praca_inzynierska.Services
{
    public class TaxonomyProvider : ITaxonomyProvider
    {
        private readonly Lazy<IEnumerable<TaxonomyItem>> _items;
        private readonly ILiteDatabase _database;
        private readonly ILiteCollection<TaxonomyCode> _codes;
        public TaxonomyProvider(ILiteDatabase database)
        {
            _items = new Lazy<IEnumerable<TaxonomyItem>>(LoadTaxonomy);
            _database = database;
            _codes = _database.GetCollection<TaxonomyCode>();
        }

        public IEnumerable<TaxonomyItem> Find(string code)
        {
            var directMatches = GetTaxonomy().Where(x => x.TaxonomyID == code);
            if (directMatches.Any())
            {
                return directMatches;
            }
            var taxonomyCode = _codes.FindById(code);
            if (taxonomyCode == null)
            {
                return Enumerable.Empty<TaxonomyItem>();
            }
            return GetTaxonomy().Where(x => x.TaxonomyID == taxonomyCode.TaxonomyID);
        }

        public IEnumerable<TaxonomyItem> GetTaxonomy()
        {
            return _items.Value;
        }

        public void Upsert(TaxonomyCode code)
        {
            _codes.Upsert(code);
        }

        private IEnumerable<TaxonomyItem> LoadTaxonomy()
        {
            //Loading the file classification.txt
            using var input = new StreamReader(File.OpenRead("content/classification.txt"));
            
            //Skip first line with headings 
            input.ReadLine();

            var items = new List<TaxonomyItem>();
            while (!input.EndOfStream)
            {
                var line = input.ReadLine();
                if (string.IsNullOrEmpty(line))continue;
                var values = line.Split('\t');

                //Fields indexes are documented in the meta.xml file
                var item = new TaxonomyItem
                {
                    TaxonomyID = values[0],
                    ScientificNameID = values[1],
                    LocalID = values[2],
                    ScientificName = values[3],
                    TaxonRank = values[4],
                    ScientificNameAuthor = values[6],
                    Family = values[7],
                    Subfamily = values[8],
                    Genus = values[11],
                    SpecificEpithet = values[13],
                    NamePublishedIn = values[17],
                    TaxonomicStatus = values[18],
                    AcceptedNameUsageID = values[19],
                };

                items.Add(item);
            }
            return items;
        }
    }
}
