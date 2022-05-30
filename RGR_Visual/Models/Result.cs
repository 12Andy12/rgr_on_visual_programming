using System;
using System.Collections.Generic;

namespace RGR_Visual.Models
{
    public partial class Result
    {
        public string? Horse { get; set; }
        public string? Date { get; set; }
        public long? RaceNumber { get; set; }
        public string? Racecourse { get; set; }
        public long? FinishPosition { get; set; }
        public string? DistanceBetween { get; set; }
        public string? Jockey { get; set; }

        public virtual Horse? HorseNavigation { get; set; }
        public virtual Jockey? JockeyNavigation { get; set; }
        public virtual Race? Race { get; set; }

        public Result()
        {
            Horse = "";
            Date = "";
            RaceNumber = 0;
            Racecourse = "";
            FinishPosition = 0;
            DistanceBetween = "";
            Jockey = "";
        }
        public static string[] GetAttr()
        {
            return new[] { "Result: FinishPosition", "Result: DistanceBetween", "Result: Horse", "Result: Date", "Result: RaceNumber", "Result: Racecourse", "Result: Jockey" };
        }
        public object? this[string property]
        {
            get
            {
                switch (property)
                {
                    case "Horse": return Horse;
                    case "Date": return Date;
                    case "RaceNumber": return RaceNumber;
                    case "Racecourse": return Racecourse;
                    case "FinishPosition": return FinishPosition;
                    case "DistanceBetween": return DistanceBetween;
                    case "Jockey": return Jockey;
                }
                return null;
            }
        }
    }
}
