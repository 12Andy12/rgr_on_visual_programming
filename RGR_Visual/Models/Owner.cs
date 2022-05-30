using System;
using System.Collections.Generic;

namespace RGR_Visual.Models
{
    public partial class Owner
    {
        public Owner()
        {
            Horses = new HashSet<Horse>();
            Name = "";
        }

        public string Name { get; set; } = null!;

        public virtual ICollection<Horse> Horses { get; set; }
        public static string GetAttr()
        {
            return "Owner: Name";
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
