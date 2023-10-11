using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentClassesInheritance
{
    internal class Dishwasher : Appliance
    {
        public string Feature { get; set; }
        public SoundRating SoundRating { get; set; }

        public Dishwasher() { }

        public Dishwasher(long itemNumber, string brand, int quantity, int wattage, string color, double price, string feature, SoundRating soundRating)
            : base(itemNumber, brand, quantity, wattage, color, price)
        {
            Feature = feature;
            SoundRating = soundRating;
        }

        public override string ToString()
        {
            return base.ToString() +
                $"\nFeature: {Feature}" +
                $"\nSound Rating: {SoundRating}";
        }
    }

    public enum SoundRating
    {
        Qt,
        Qr,
        Qu,
        M
    }
}
