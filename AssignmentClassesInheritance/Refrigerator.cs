using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentClassesInheritance
{
    internal class Refrigerator : Appliance
    {
        private int numberOfDoors;

        public int NumberOfDoors
        {
            get { return numberOfDoors; }
            set
            {
                if (value == 2 || value == 3 || value == 4)
                {
                    numberOfDoors = value;
                }
                else
                {
                    throw new ArgumentException("Number of doors must be 2, 3, or 4.");
                }
            }
        }

        public double Height { get; set; }
        public double Width { get; set; }

        public Refrigerator() { }

        public Refrigerator(long itemNumber, string brand, int quantity, int wattage, string color, double price, int numberOfDoors, double height, double width)
            : base(itemNumber, brand, quantity, wattage, color, price)
        {
            NumberOfDoors = numberOfDoors;
            Height = height;
            Width = width;
        }



        public override string ToString()
        {
            //if(NumberOfDoors == 2)
            //{
            //    return base.ToString() +
            //    $"\nNumber of Doors: Double Door" +
            //    $"\nHeight: {Height} inches" +
            //    $"\nWidth: {Width} inches";
            //}
            //else if (NumberOfDoors == 3)
            //{
            //    return base.ToString() +
            //    $"\nNumber of Doors: Three Door" +
            //    $"\nHeight: {Height} inches" +
            //    $"\nWidth: {Width} inches";
            //}
            //else if (NumberOfDoors == 4)
            //{
            //    return base.ToString() +
            //    $"\nNumber of Doors: Four Door" +
            //    $"\nHeight: {Height} inches" +
            //    $"\nWidth: {Width} inches";
            //}
            return base.ToString() +
                $"\nNumber of Doors: {NumberOfDoors}" +
                $"\nHeight: {Height} inches" +
                $"\nWidth: {Width} inches";
        }
    }


}

