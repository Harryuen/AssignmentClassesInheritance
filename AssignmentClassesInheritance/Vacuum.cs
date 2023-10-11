using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AssignmentClassesInheritance
{
    internal class Vacuum : Appliance
    {
        public string Grade { get; set; } = "";
        public int BatteryVoltage { get; set; }

        public Vacuum() { }

        public Vacuum(long itemNumber, string brand, int quantity, int wattage, string color, double price, string grade, int batteryVoltage)
            : base(itemNumber, brand, quantity, wattage, color, price)
        {
            this.Grade = grade;
            this.BatteryVoltage = batteryVoltage;
        }

        public override string ToString()
        {
            return base.ToString() +
                $"\nGrade: {Grade}" +
                $"\nBattery Voltage: {BatteryVoltage} V";
        }
    }


}
