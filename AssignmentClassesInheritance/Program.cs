using System.Globalization;
using System.Security.Cryptography.X509Certificates;

namespace AssignmentClassesInheritance
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ModernAppliances modernAppliances = new ModernAppliances();
            ModernAppliances.Options option = ModernAppliances.Options.None;

            modernAppliances.PopulateAppliances();

            while (option != ModernAppliances.Options.SaveExit)
            {
                modernAppliances.DisplayMenu();

                option = Enum.Parse<ModernAppliances.Options>(Console.ReadLine());

                switch (option)
                {
                    case ModernAppliances.Options.Checkout:
                        {
                            modernAppliances.Checkout();

                            break;
                        }
                    case ModernAppliances.Options.Find:
                        {
                            modernAppliances.Find();

                            break;
                        }
                    case ModernAppliances.Options.DisplayType:
                        {
                            modernAppliances.DisplayType();

                            break;
                        }

                    case ModernAppliances.Options.RandomList:
                        {
                            modernAppliances.RandomList();
                            break;
                        }
                    case ModernAppliances.Options.SaveExit:
                        {
                            modernAppliances.Save();
                            break;
                        }
                    //case ModernAppliances.Options.display:
                    //    {
                    //        modernAppliances.DisplayQuantityForItemNumber();
                    //        break;
                    //    }
                    default:
                        {
                            Console.WriteLine("Invalid option entered. Please try again.");
                            break;
                        }
                }
            }


        }
    }

}