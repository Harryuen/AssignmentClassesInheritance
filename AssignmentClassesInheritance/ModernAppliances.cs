using AssignmentClassesInheritance;

namespace AssignmentClassesInheritance
{
    /// <summary>
    /// Manager class for Modern Appliances
    /// </summary>
    /// <remarks>Author: </remarks>
    /// <remarks>Date: </remarks>
    /// <remarks>Author: Ki Fung Yuen</remarks>
    /// <remarks>Date: Oct 10, 2023</remarks>
    internal sealed class ModernAppliances
    {
        /// <summary>
        /// Location of appliances.txt file
        /// </summary>
        public const string APPLIANCES_TEXT_FILE = "appliances.txt";

        /// <summary>
        /// Options user can choose
        /// </summary>
        public enum Options
        {
            None,
            Checkout = 1,
            Find = 2,
            DisplayType = 3,
            RandomList = 4,
            SaveExit = 5,
        }

        /// <summary>
        /// Holds list of appliances
        /// </summary>
        private List<Appliance> appliances;

        /// <summary>
        /// Provides immutable list of appliances
        /// </summary>
        public List<Appliance> Appliances
        {
            get
            {
                return this.appliances.ToList();
            }
        }

        /// <summary>
        /// Called when ModernAppliances instance is created
        /// </summary>
        public ModernAppliances()
        {
            this.appliances = new List<Appliance>();
        }

        // TODO: Code the following methods:

        /// <summary>
        /// Populates appliances from text file
        /// </summary>
        public void PopulateAppliances()
        {
            // Read all lines/rows from appliances text file.
            string[] lines = File.ReadAllLines(APPLIANCES_TEXT_FILE);

            // Loop through each line/row
            foreach (string line in lines)
            {
                // Split row into columns
                string[] columns = line.Split(';');

                // Use ID to determine appliance type
                long ItemNumber = long.Parse(columns[0]);
                string Brand = columns[1];
                int Quantity = int.Parse(columns[2]);
                int Wattage = int.Parse(columns[3]);
                string Color = columns[4];
                double Price = double.Parse(columns[5]);



                char firstDigitOfItemNumber = ItemNumber.ToString()[0];

                // Create special appliance object based on the type
                Appliance appliance = null;
                if (firstDigitOfItemNumber == '1')
                {
                    // Create a Refrigerators object
                    int numOfDoors = int.Parse(columns[6]);
                    double height = double.Parse(columns[7]);
                    double width = double.Parse(columns[8]);
                    appliance = new Refrigerator(ItemNumber, Brand, Quantity, Wattage, Color, Price, numOfDoors, height, width);
                }
                else if (firstDigitOfItemNumber == '2')
                {
                    // Create a Vacuum object
                    string grade = columns[6];
                    int batteryVoltage = int.Parse(columns[7]);
                    appliance = new Vacuum(ItemNumber, Brand, Quantity, Wattage, Color, Price, grade, batteryVoltage);
                }
                else if (firstDigitOfItemNumber == '3')
                {
                    // Create a Microwave object
                    double capacity = double.Parse(columns[6]);
                    char roomTypeChar = columns[7][0]; // Trim

                    // Map the roomTypeChar to an enum value
                    RoomType roomType = roomTypeChar == 'K' ? RoomType.Kitchen : RoomType.WorkSite;
                    appliance = new Microwave(ItemNumber, Brand, Quantity, Wattage, Color, Price, capacity, roomType);
                }
                else if (firstDigitOfItemNumber == '4' || firstDigitOfItemNumber == '5')
                {
                    // Create a Dishwasher object
                    string feature = columns[6];
                    string soundRatingStr = columns[7].Trim().ToUpper();

                    // Map the soundRatingStr to an enum value
                    SoundRating soundRating;
                    switch (soundRatingStr)
                    {
                        case "QT":
                            soundRating = SoundRating.Qt;
                            break;
                        case "QR":
                            soundRating = SoundRating.Qr;
                            break;
                        case "QU":
                            soundRating = SoundRating.Qu;
                            break;
                        case "M":
                            soundRating = SoundRating.M;
                            break;
                        default:
                            throw new ArgumentException("Invalid sound rating value.");
                    }
                    appliance = new Dishwasher(ItemNumber, Brand, Quantity, Wattage, Color, Price, feature, soundRating);
                }

                // Add the created appliance object to the appliances list
                appliances.Add(appliance);
            }
        }


        /// <summary>
        /// Option 1: Performs a checkout
        /// </summary>
        public void Checkout()
        {
            Console.Write("Enter the item number of an appliance: ");

            // Create a long variable to hold the item number
            long itemNumber;

            // Get user input as a string and try to convert it to a long
            if (long.TryParse(Console.ReadLine(), out itemNumber))
            {
                // Create a variable to hold the found appliance
                Appliance foundAppliance = null;

                // Loop through Appliances to find the matching item number
                foreach (var appliance in appliances)
                {
                    if (appliance.ItemNumber == itemNumber)
                    {
                        foundAppliance = appliance;
                        break; // Break out of the loop since we found what we need
                    }
                }

                // Check if the appliance was not found (foundAppliance is null)
                if (foundAppliance == null)
                {
                    Console.WriteLine("No appliances found with that item number.");
                }
                else
                {
                    // Check if the found appliance is available
                    if (foundAppliance.IsAvailable)
                    {
                        // Perform the checkout operation (you can define this logic)
                        // For example, you can reduce the quantity by 1 or update its availability status.

                        // Example logic:
                        foundAppliance.Quantity--; // Reduce the quantity by 1

                        Console.WriteLine("Appliance has been checked out."); 
                        Console.WriteLine($"Updated Quantity: {foundAppliance.Quantity}");
                    }
                    else
                    {
                        Console.WriteLine("The appliance is not available to be checked out.");
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid item number.");
            }
        }

        /// <summary>
        /// Option 2: Finds appliances
        /// </summary>
        public void Find()
        {
            // Prompt the user to enter a brand to search for
            Console.Write("Enter brand to search for: ");

            // Get user input as a string
            string enteredBrand = Console.ReadLine();

            // Create a list to hold found Appliance objects
            List<Appliance> foundAppliances = new List<Appliance>();

            // Iterate through loaded appliances
            foreach (var appliance in appliances)
            {
                // Test if the current appliance's brand matches what the user entered (case-insensitive)
                if (appliance.Brand.Equals(enteredBrand, StringComparison.OrdinalIgnoreCase))
                {
                    // Add the current appliance to the list of found appliances
                    foundAppliances.Add(appliance);
                }
            }

            // Display found appliances
            DisplayAppliancesFromList(foundAppliances, 0);
        }

        /// <summary>
        /// Displays Refridgerators
        /// </summary>
        public void DisplayRefrigerators()
        {
            // Display the possible options for the number of doors
            Console.WriteLine("Possible options:");
            Console.WriteLine("0 - Any");
            Console.WriteLine("2 - Double doors");
            Console.WriteLine("3 - Three doors");
            Console.WriteLine("4 - Four doors");

            // Prompt the user to enter the number of doors
            Console.Write("Enter the number of doors: ");

            // Create a variable to hold the entered number of doors
            int enteredNumberOfDoors;

            // Get user input as a string and assign it to the variable
            string userInput = Console.ReadLine();

            // Convert user input from string to int and store it as the number of doors variable
            if (int.TryParse(userInput, out enteredNumberOfDoors))
            {
                // Create a list to hold found Appliance objects
                List<Appliance> foundRefrigerators = new List<Appliance>();

                // Iterate through appliances
                foreach (var appliance in appliances)
                {
                    // Test if the current appliance is a refrigerator
                    if (appliance is Refrigerator refrigerator)
                    {
                        // Test if the user entered 0 or the refrigerator's doors match what the user entered
                        if (enteredNumberOfDoors == 0 || refrigerator.NumberOfDoors == enteredNumberOfDoors)
                        {
                            // Add the current appliance to the list of found refrigerators
                            foundRefrigerators.Add(appliance);
                        }
                    }
                }

                // Display found refrigerators
                DisplayAppliancesFromList(foundRefrigerators, 0);
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number of doors.");
            }
        }

        /// <summary>
        /// Displays Vacuums
        /// </summary>
        /// <param name="grade">Grade of vacuum to find (or null for any grade)</param>
        /// <param name="voltage">Vacuum voltage (or 0 for any voltage)</param>
        public void DisplayVacuums()
        {
            // Display possible options for vacuum grade
            Console.WriteLine("Possible options for grade:");
            Console.WriteLine("0 - Any");
            Console.WriteLine("1 - Residential");
            Console.WriteLine("2 - Commercial");

            // Prompt the user to enter the vacuum grade
            Console.Write("Enter grade: ");

            // Get user input as a string and assign it to a variable
            string gradeInput = Console.ReadLine();

            // Create a variable to hold the grade to find (Any, Residential, or Commercial)
            string grade = "";

            // Determine the grade based on user input
            switch (gradeInput)
            {
                case "0":
                    grade = "Any";
                    break;
                case "1":
                    grade = "Residential";
                    break;
                case "2":
                    grade = "Commercial";
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    return; // Return to the previous method
            }

            // Display possible options for vacuum voltage
            Console.WriteLine("Possible options for voltage:");
            Console.WriteLine("0 - Any");
            Console.WriteLine("1 - 18 Volt");
            Console.WriteLine("2 - 24 Volt");

            // Prompt the user to enter the vacuum voltage
            Console.Write("Enter voltage: ");

            // Get user input as a string
            string voltageInput = Console.ReadLine();

            // Create a variable to hold the voltage
            int voltage = 0;

            // Determine the voltage based on user input
            switch (voltageInput)
            {
                case "0":
                    voltage = 0;
                    break;
                case "1":
                    voltage = 18;
                    break;
                case "2":
                    voltage = 24;
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    return; // Return to the previous method
            }

            // Create a list to hold found Appliance objects
            List<Appliance> foundVacuums = new List<Appliance>();

            // Loop through Appliances
            foreach (var appliance in appliances)
            {
                // Check if the current appliance is a vacuum
                if (appliance is Vacuum vacuum)
                {
                    // Test if grade is "Any" or grade matches the current vacuum's grade,
                    // and if voltage is 0 or voltage matches the current vacuum's voltage
                    if ((grade == "Any" || grade == vacuum.Grade) &&
                        (voltage == 0 || voltage == vacuum.BatteryVoltage))
                    {
                        // Add the current appliance to the list of found vacuums
                        foundVacuums.Add(appliance);
                    }
                }
            }

            // Display found vacuums
            DisplayAppliancesFromList(foundVacuums, 0);
        }

        /// <summary>
        /// Displays microwaves
        /// </summary>
        public void DisplayMicrowaves()
        {
            // Display possible options for room type
            Console.WriteLine("Possible options for room type:");
            Console.WriteLine("0 - Any");
            Console.WriteLine("1 - Kitchen");
            Console.WriteLine("2 - Work site");

            // Prompt the user to enter the room type
            Console.Write("Enter room type: ");

            // Get user input as a string and assign it to a variable
            string roomTypeInput = Console.ReadLine();

            // Create a character variable that holds the room type
            char roomType;

            // Determine the room type based on user input
            switch (roomTypeInput)
            {
                case "0":
                    roomType = 'A'; // 'A' represents Any
                    break;
                case "1":
                    roomType = 'K'; // 'K' represents Kitchen
                    break;
                case "2":
                    roomType = 'W'; // 'W' represents Work site
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    return; // Return to the previous method
            }

            // Create a list to hold found Appliance objects
            List<Appliance> foundMicrowaves = new List<Appliance>();

            // Loop through Appliances
            foreach (var appliance in appliances)
            {
                // Check if the current appliance is a microwave
                if (appliance is Microwave microwave)
                {
                    // Test if room type equals 'A' (Any) or matches the current microwave's room type
                    if (roomType == 'A' || roomType == microwave.RoomType.ToString()[0])
                    {
                        // Add the current appliance to the list of found microwaves
                        foundMicrowaves.Add(appliance);
                    }
                }
            }
        }
        // Display found microwaves


        /// <summary>
        /// Displays dishwashers
        /// </summary>
        public void DisplayDishwashers()
            {
            // Create a variable that holds the sound rating
            SoundRating soundRating;

            // Determine the sound rating based on user input
            while (true)
            {
                // Display possible options for sound rating
                // Prompt the user to enter the sound rating
                Console.WriteLine("Enter the sound rating of the dishwasher: Qt (Quietest), Qr (Quieter), Qu(Quiet) or M (Moderate):");
                string input = Console.ReadLine();


                switch (input.Trim().ToUpper())
                {
                    case "QT":
                        soundRating = SoundRating.Qt;
                        break;
                    case "QR":
                        soundRating = SoundRating.Qr;
                        break;
                    case "QU":
                        soundRating = SoundRating.Qu;
                        break;
                    case "M":
                        soundRating = SoundRating.M;
                        break;
                    default:
                        Console.WriteLine("Invalid input.Please enter a valid option.");
                        continue;
                }
                break;
            }
            // Create a list to hold found Appliance objects
            List<Appliance> foundDishwashers = new List<Appliance>();

            // Loop through Appliances
            foreach (var appliance in appliances)
            {
                // Check if the current appliance is a dishwasher
                if (appliance is Dishwasher dishwasher)
                {
                    // Test if sound rating matches the current dishwasher's sound rating
                    if (soundRating == dishwasher.SoundRating)
                    {
                        // Add the current appliance to the list of found dishwashers
                        foundDishwashers.Add(appliance);
                    }
                }
            }

            // Display found dishwashers (up to max. number inputted)
            DisplayAppliancesFromList(foundDishwashers, 0);
            }

            /// <summary>
            /// Generates random list of appliances
            /// </summary>
            public void RandomList()
            {
                // Display possible appliance types
                Console.WriteLine("Appliance Types:");
                Console.WriteLine("0 - Any");
                Console.WriteLine("1 - Refrigerators");
                Console.WriteLine("2 - Vacuums");
                Console.WriteLine("3 - Microwaves");
                Console.WriteLine("4 - Dishwashers");

                // Prompt the user to enter the type of appliance
                Console.Write("Enter type of appliance: ");

                // Get user input as a string and assign it to an appliance type variable
                string applianceTypeInput = Console.ReadLine();

                // Prompt the user to enter the number of appliances
                Console.Write("Enter number of appliances: ");

                // Get user input as a string and assign it to a variable
                string numberOfAppliancesInput = Console.ReadLine();

                // Convert user input from string to int
                if (!int.TryParse(numberOfAppliancesInput, out int numberOfAppliances))
                {
                    Console.WriteLine("Invalid input for the number of appliances.");
                    return; // Return to the previous method
                }

                // Create a variable to hold a list of found appliances
                List<Appliance> foundAppliances = new List<Appliance>();

                // Loop through appliances
                foreach (var appliance in appliances)
                {
                    // Test if inputted appliance type is "0"
                    if (applianceTypeInput == "0")
                    {
                        // Add the current appliance to the list of found appliances
                        foundAppliances.Add(appliance);
                    }
                    // Test if inputted appliance type is "1"
                    else if (applianceTypeInput == "1" && appliance is Refrigerator)
                    {
                        // Add the current appliance to the list of found appliances
                        foundAppliances.Add(appliance);
                    }
                    // Test if inputted appliance type is "2"
                    else if (applianceTypeInput == "2" && appliance is Vacuum)
                    {
                        // Add the current appliance to the list of found appliances
                        foundAppliances.Add(appliance);
                    }
                    // Test if inputted appliance type is "3"
                    else if (applianceTypeInput == "3" && appliance is Microwave)
                    {
                        // Add the current appliance to the list of found appliances
                        foundAppliances.Add(appliance);
                    }
                    // Test if inputted appliance type is "4"
                    else if (applianceTypeInput == "4" && appliance is Dishwasher)
                    {
                        // Add the current appliance to the list of found appliances
                        foundAppliances.Add(appliance);
                    }
                }

                // Randomize the list of found appliances
                Shuffle(foundAppliances);

                // Display found appliances (up to the max. number inputted)
                DisplayAppliancesFromList(foundAppliances, numberOfAppliances);
            }

            // A method to shuffle a list randomly
            private void Shuffle<T>(List<T> list)
            {
                Random rng = new Random();
                int n = list.Count;
                while (n > 1)
                {
                    n--;
                    int k = rng.Next(n + 1);
                    T value = list[k];
                    list[k] = list[n];
                    list[n] = value;
                }
            }

            /// <summary>
            /// Displays menu options
            /// </summary>
            public void DisplayMenu()
        {
            Console.WriteLine("Welcome to Modern Appliances!");
            Console.WriteLine("How May We Assist You ?");
            Console.WriteLine("1 – Check out appliance");
            Console.WriteLine("2 – Find appliances by brand");
            Console.WriteLine("3 – Display appliances by type");
            Console.WriteLine("4 – Produce random appliance list");
            Console.WriteLine("5 – Save & exit");
        }

        /// <summary>
        /// Displays appliances with type
        /// </summary>
        public void DisplayType()
        {
            Console.WriteLine("Appliance Types");
            Console.WriteLine("1 – Refrigerators");
            Console.WriteLine("2 – Vacuums");
            Console.WriteLine("3 – Microwaves");
            Console.WriteLine("4 – Dishwashers");

            Console.Write("Enter type of appliance:");

            int applianceTypeNum;
            bool parsedApplianceType = int.TryParse(Console.ReadLine(), out applianceTypeNum);

            if (!parsedApplianceType || applianceTypeNum < 0 || applianceTypeNum > 4)
            {
                Console.WriteLine("Invalid appliance type entered.");
                return;
            }

            switch (applianceTypeNum)
            {
                case 1:
                    {
                        this.DisplayRefrigerators();

                        break;
                    }

                case 2:
                    {
                        this.DisplayVacuums();

                        break;
                    }

                case 3:
                    {
                        this.DisplayMicrowaves();

                        break;
                    }

                case 4:
                    {
                        this.DisplayDishwashers();

                        break;
                    }

                default:
                    {
                        Console.WriteLine("Invalid appliance type entered.");

                        break;
                    }
            }
        }

        /// <summary>
        /// Prints out appliances in list
        /// </summary>
        /// <param name="appliances">List of appliances</param>
        /// <param name="max">Maximum number of appliances to display (0 is unlimited)</param>
        public void DisplayAppliancesFromList(List<Appliance> appliances, int max)
        {
            if (appliances.Count > 0)
            {
                Console.WriteLine("Found appliances:");
                Console.WriteLine();

                // Display found appliances until either end of list is reached or number of appliances requested is shown.
                for (int i = 0; i < appliances.Count && (max == 0 || i < max); i++)
                {
                    Appliance appliance = appliances[i];
                    Console.WriteLine(appliance);
                    Console.WriteLine();
                }

            }
            else
            {
                Console.WriteLine("No appliances found.");
            }

            Console.WriteLine();
        }


        /// <summary>
        /// Saves appliances to text file
        /// </summary>
        public void Save()
        {
            Console.WriteLine("Saving...");

            // Declare the StreamWriter outside the using block
            StreamWriter writer = null;

            try
            {
                // Open text file for writing
                writer = new StreamWriter(APPLIANCES_TEXT_FILE);

                // Loop through each appliance
                foreach (var appliance in appliances)
                {
                    // Write appliance to text file in the proper format
                    string applianceData = $"{appliance.ItemNumber};{appliance.Brand};{appliance.Quantity};{appliance.Wattage};{appliance.Color};{appliance.Price}";


                    // Check the type of the appliance and add type-specific information
                    if (appliance is Refrigerator refrigerator)
                    {
                        applianceData += $";{refrigerator.NumberOfDoors};{refrigerator.Height};{refrigerator.Width}";
                        writer.WriteLine(applianceData);
                    }
                    else if (appliance is Vacuum vacuum)
                    {
                        applianceData += $";{vacuum.Grade};{vacuum.BatteryVoltage}";
                        writer.WriteLine(applianceData);
                    }
                    else if (appliance is Microwave microwave)
                    {
                        applianceData += $";{microwave.Capacity};{(microwave.RoomType == RoomType.Kitchen ? "Kitchen" : "WorkSite")}";
                        writer.WriteLine(applianceData);
                    }
                    else if (appliance is Dishwasher dishwasher)
                    {
                        applianceData += $";{dishwasher.Feature};{dishwasher.SoundRating}";
                        writer.WriteLine(applianceData);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions as needed
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                // Close the file in the finally block to ensure it's always closed
                if (writer != null)
                {
                    writer.Close();
                }
            }

            Console.WriteLine("DONE!");
        }

        //for debug//
        //public void DisplayQuantityForItemNumber()
        //{
        //    Console.Write("Enter the item number to check the quantity: ");
        //    if (long.TryParse(Console.ReadLine(), out long itemNumber))
        //    {
        //        // Find the appliance with the specified item number
        //        var appliance = appliances.FirstOrDefault(a => a.ItemNumber == itemNumber);

        //        if (appliance != null)
        //        {
        //            // Display the quantity of the found appliance
        //            Console.WriteLine($"Quantity of item {itemNumber}: {appliance.Quantity}");
        //        }
        //        else
        //        {
        //            // Appliance with the specified item number was not found
        //            Console.WriteLine($"Item {itemNumber} not found.");
        //        }
        //    }
        //    else
        //    {
        //        Console.WriteLine("Invalid item number. Please enter a valid item number.");
        //    }
        //}

    }
}