using System;
using System.Collections.Generic;

namespace RGR_Visual.Models
{
    public partial class Jockey
    {
        public string Name { get; set; } = null!;
        public Jockey()
        {
            Name = "";
        }
        public static string GetAttr()
        {
            return "Jockey: Name";
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
