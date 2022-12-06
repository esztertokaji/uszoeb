using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace UszoEb.Models
{
    public partial class Orszagok
    {
        public Orszagok()
        {
            Versenyzoks = new HashSet<Versenyzok>();
        }

        public int Id { get; set; }
        public string Nev { get; set; }
        public string Nobid { get; set; }

        [JsonIgnore]

        public virtual ICollection<Versenyzok> Versenyzoks { get; set; }
    }
}
