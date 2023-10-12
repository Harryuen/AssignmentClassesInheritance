using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AssignmentClassesInheritance
{
    internal class Appliance
    {
        //// TODO: Create properties for each field

        
        public long ItemNumber { get; private set; }
        public string Brand { get; private set; }
        public int Quantity { get; set; }
        public int Wattage { get; private set; }
        public string Color { get; private set; }
        public double Price { get; private set; }
        
        
        public char Type => ItemNumber.ToString()[0];
        public bool IsAvailable => Quantity > 0;

        ///// <summary>
        ///// Constructor for appliance
        ///// </summary>

        public Appliance()
        {
            Brand = "";
            Color = "";
            ItemNumber = 0;
            Price = 0.00;
            Quantity = 0;
            Wattage = 0;
        }
        public Appliance(long itemNumber, string brand, int quantity, int wattage, string color, double price)
        {
            Brand = brand;
            Color = color;
            ItemNumber = itemNumber;
            Price = price;
            Quantity = quantity;
            Wattage = wattage;
        }

        public void Checkout()
        {
            if (Quantity > 0)
            {
                Quantity--;
                Console.WriteLine($"{Brand} {GetType().Name} has been checked out.");
            }
            else
            {
                Console.WriteLine($"Sorry, there are no more {Brand} {GetType().Name} available.");
            }
        }

        public override string ToString()
        {
            return  $"Item No:  {ItemNumber}\n" +
                    $"Brand:    {Brand}\n" +
                    $"Quantity: {Quantity}\n" +
                    $"Wattage:  {Wattage}W\n" +
                    $"Color:    {Color}\n" +
                    $"Price:    {Price:C}\n";
        }

    }
}
