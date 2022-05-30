using System;
using System.Collections.Generic;

namespace RGR_Visual.Models
{
    public partial class Race
    {
        public string Date { get; set; } = null!;
        public long Number { get; set; }
        public string Racecourse { get; set; } = null!;
        public string? Distance { get; set; }
        public string? Type { get; set; }

        public virtual Racecourse RacecourseNavigation { get; set; } = null!;
        public Race()
        {
            Date = "";
            Number = 0;
            Racecourse = "";
            Distance = "";
            Type = "";
        }
        public static string[] GetAttr()
        {
            return new[] { "Race: Date", "Race: Number", "Race: Distance", "Race: Type", "Race: Racecourse" };
        }
        public object? this[string property]
        {
            get
            {
                switch (property)
                {
                    case "Date": return Date;
                    case "Number": return Number;
                    case "Racecourse": return Racecourse;
                    case "Distance": return Distance;
                    case "Type": return Type;
                }
                return null;
            }
        }
    }
}
