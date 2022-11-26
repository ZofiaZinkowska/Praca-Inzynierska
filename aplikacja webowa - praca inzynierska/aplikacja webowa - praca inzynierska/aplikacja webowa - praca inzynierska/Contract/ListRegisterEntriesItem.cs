using System;

namespace aplikacja_webowa___praca_inzynierska.Contract
{
    public class ListRegisterEntriesItem
    {
        public int Id { get; set; }
        public string ScientificName { get; set; }
        public string ScientificNameAuthor { get; set; }
        public DateTime AddDate { get; set; }
        public string TaxonomyID { get; set; }
    }
}
