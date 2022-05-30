using System;
using System.Collections.Generic;

namespace RGR_Visual.Models
{
    public partial class Racecourse
    {
        public Racecourse()
        {
            Races = new HashSet<Race>();
            Name = "";
        }

        public string Name { get; set; } = null!;

        public virtual ICollection<Race> Races { get; set; }
        public static string GetAttr()
        {
            return "Racecourse: Name";
        }
        public object? this[string property]
        {
            get
            {
                switch (property)
                {
                    case "Name": return Name;
                }
                return null;
            }
        }
    }
}
