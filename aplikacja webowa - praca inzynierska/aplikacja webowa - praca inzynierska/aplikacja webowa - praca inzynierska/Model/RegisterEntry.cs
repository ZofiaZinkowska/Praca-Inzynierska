using System;

namespace aplikacja_webowa___praca_inzynierska.Model
{
    public class RegisterEntry
    {
        public int Id { get; set; }
        public string ScientificNameID { get; set; }
        //public string Name { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime ModificationDate { get; set;}
    }
}