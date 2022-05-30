using System;
using System.Collections.Generic;

namespace RGR_Visual.Models
{
    public partial class Horse : Table
    {
        public string? Name { get; set; } = null!;
        public string? Gender { get; set; }
        public long? Age { get; set; }
        public string? Weight { get; set; }
        public string? Trainer { get; set; }
        public string Owner { get; set; } = null!;

        public virtual Owner OwnerNavigation { get; set; } = null!;
        public virtual Trainer? TrainerNavigation { get; set; }

        public Horse()
        {
            Name = "";
            Gender = "";
            Age = 0;
            Weight = "";
            Trainer = "";
            Owner = "";
            OwnerNavigation = null!;
            TrainerNavigation = null;
        }

        public object? this[string property]
        {
            get
            {
                switch (property)
                {
                    case "Name": return Name;
                    case "Gender": return Gender;
                    case "Age": return Age;
                    case "Weight": return Weight;
                    case "Trainer": return Trainer;
                    case "Owner": return Owner;
                }
                return null;
            }
        }

        public bool Equals(Horse? other)
        {
            return (this.Name == other.Name);
        }
        public static string[] GetAttr()
        {
            return new[] { "Horse: Name", "Horse: Gender", "Horse: Age", "Horse: Weight", "Horse: Trainer", "Horse: Owner" };
        }
    }
}
