namespace aplikacja_webowa___praca_inzynierska.Model
{
    //TODO: wywalić niepotrzebne pola

    public class TaxonomyItem
    {
        public string TaxonomyID { get; set; } 
        public string ScientificNameID { get; set; }
        public string LocalID { get; set; }
        public string ScientificName { get; set; }
        public string TaxonRank { get; set; }
        public string ScientificNameAuthor { get; set; }
        public string Family { get; set; }
        public string Subfamily { get; set; }
        public string Genus { get; set; }
        public string SpecificEpithet { get; set; }
        public string NamePublishedIn { get; set; }
        public string TaxonomicStatus { get; set; }
        public string AcceptedNameUsageID { get; set; }
    }
}
