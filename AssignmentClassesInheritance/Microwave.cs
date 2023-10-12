using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentClassesInheritance
{
    internal class Microwave : Appliance
    {
        private char roomtype;
        public double Capacity { get; set; }
        public RoomType RoomType { get; set; }

        public Microwave() { }

        public Microwave(long itemNumber, string brand, int quantity, int wattage, string color, double price, double capacity, RoomType roomType)
            : base(itemNumber, brand, quantity, wattage, color, price)
        {
            Capacity = capacity;
            RoomType = roomType;
        }

        public override string ToString()
        {
            return base.ToString() +
                $"Capacity: {Capacity} cu. ft." +
                $"\nRoom Type: {RoomType}";
        }
    }

    public enum RoomType
    {
        Any,
        Kitchen,
        WorkSite
    }
}
