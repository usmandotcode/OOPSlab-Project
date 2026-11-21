using System;
using System.IO;

namespace SmartLostAndFound
{
    //
    // SECTION 1: BASE CLASS (Item)
    // OOP Concepts: Class, Encapsulation, Constructors, Polymorphism (virtual)
    // 
    public class Item
    {
        // Private fields (Data Hiding) - yahan data hide hoga "Encapsulation"
        private int itemId;
        private string itemName;
        private string category;
        private string location;
        private string description;
        private string status;
        private string dateReported;

        // Public Properties with validation          
        public int ItemId
        {
            get { return itemId; }
            set
            {
                if (value > 0)
                {
                    itemId = value;
                }
                else
                {
                    itemId = 1;
                }
            }
        }

        public string ItemName
        {
            get { return itemName; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    itemName = value.Trim();
                }
                else
                {
                    itemName = "Unnamed Item";
                }
            }
        }

        public string Category
        {
            get { return category; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    category = value.Trim();
                }
                else
                {
                    category = "General";
                }
            }
        }

        public string Location
        {
            get { return location; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    location = value.Trim();
                }
                else
                {
                    location = "Campus";
                }
            }
        }

        public string Description
        {
            get { return description; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    description = value.Trim();
                }
                else
                {
                    description = "No description provided";
                }
            }
        }

        public string Status
        {
            get { return status; }
            set
            {
                if (value == "Pending" || value == "Recovered" || value == "Claimed")
                {
                    status = value;
                }
                else
                {
                    status = "Pending";
                }
            }
        }

        public string DateReported
        {
            get { return dateReported; }
            set { dateReported = value; }
        }

        //  Constructor ye default hota hai mtlb ye khd banta hai
        public Item()
        {
            ItemId = 1;
            ItemName = "Unknown";
            Category = "General";
            Location = "Campus";
            Description = "None";
            Status = "Pending";
            DateReported = DateTime.Now.ToShortDateString();
        }

        // Parameterized Constructor
        public Item(int id, string name, string category, string location, string desc)
        {
            ItemId = id;
            ItemName = name;
            Category = category;
            Location = location;
            Description = desc;
            Status = "Pending";
            DateReported = DateTime.Now.ToShortDateString();
        }

        // Virtual Methods for Runtime Polymorphism - Polymorphism wala step
        public virtual string GetItemType()
        {
            return "GENERAL";
        }

        public virtual void DisplaySummary()
        {
            Console.WriteLine("[" + GetItemType() + "] ID: " + ItemId + " | " + ItemName + " | Status: " + Status);
        }

        public virtual void DisplayFullDetails()
        {
            Console.WriteLine("...........................");
            Console.WriteLine("Item ID       : " + ItemId);
            Console.WriteLine("Type          : " + GetItemType());
            Console.WriteLine("Item Name     : " + ItemName);
            Console.WriteLine("Category      : " + Category);
            Console.WriteLine("Location      : " + Location);
            Console.WriteLine("Description   : " + Description);
            Console.WriteLine("Date Reported : " + DateReported);
            Console.WriteLine("Current Status: " + Status);
        }

        // Converts object data into a single line for saving to file
        public virtual string ToFileLine()
        {
            return GetItemType() + "|" + ItemId + "|" + ItemName + "|" + Category + "|" + Location + "|" + Description + "|" + Status + "|" + DateReported;
        }
    }

    // SECTION 2: CHILD CLASS 1 (LostItem)
    // OOP Concepts: Inheritance (: Item), Constructor Chaining (: base), Overriding
    public class LostItem : Item
    {
        private string ownerName;
        private string ownerPhone;
        private string ownerEmail;
        private double rewardOffered;

        public string OwnerName
        {
            get { return ownerName; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    ownerName = value.Trim();
                }
                else
                {
                    ownerName = "Anonymous Owner";
                }
            }
        }

        public string OwnerPhone
        {
            get { return ownerPhone; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    ownerPhone = value.Trim();
                }
                else
                {
                    ownerPhone = "Not Provided";
                }
            }
        }

        public string OwnerEmail
        {
            get { return ownerEmail; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    ownerEmail = value.Trim();
                }
                else
                {
                    ownerEmail = "Not Provided";
                }
            }
        }

        public double RewardOffered
        {
            get { return rewardOffered; }
            set
            {
                if (value >= 0)
                {
                    rewardOffered = value;
                }
                else
                {
                    rewardOffered = 0;
                }
            }
        }

        // Constructor calling base class constructor
        public LostItem(int id, string name, string cat, string loc, string desc, string owner, string phone, string email, double reward)
            : base(id, name, cat, loc, desc)
        {
            OwnerName = owner;
            OwnerPhone = phone;
            OwnerEmail = email;
            RewardOffered = reward;
        }

        public override string GetItemType()
        {
            return "LOST";
        }

        public override void DisplaySummary()
        {
            Console.WriteLine("[LOST]  ID: " + ItemId + " | " + ItemName + " (" + Category + ") | Owner: " + OwnerName + " | Status: " + Status);
        }

        public override void DisplayFullDetails()
        {
            base.DisplayFullDetails();
            Console.WriteLine("Owner Name    : " + OwnerName);
            Console.WriteLine("Owner Phone   : " + OwnerPhone);
            Console.WriteLine("Owner Email   : " + OwnerEmail);
            Console.WriteLine("Reward Offered: Rs. " + RewardOffered);
            Console.WriteLine("..........");
        }

        public override string ToFileLine()
        {
            return base.ToFileLine() + "|" + OwnerName + "|" + OwnerPhone + "|" + OwnerEmail + "|" + RewardOffered;
        }
    }
    // SECTION 3: CHILD CLASS 2 (FoundItem)
    // OOP Concepts: Inheritance (: Item), Polymorphism (override)
    public class FoundItem : Item
    {
        private string finderName;
        private string finderPhone;
        private string storageDesk;
        private string verificationHint;

        public string FinderName
        {
            get { return finderName; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    finderName = value.Trim();
                }
                else
                {
                    finderName = "Good Samaritan";
                }
            }
        }

        public string FinderPhone
        {
            get { return finderPhone; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    finderPhone = value.Trim();
                }
                else
                {
                    finderPhone = "Not Provided";
                }
            }
        }

        public string StorageDesk
        {
            get { return storageDesk; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    storageDesk = value.Trim();
                }
                else
                {
                    storageDesk = "Main Admin Office";
                }
            }
        }

        public string VerificationHint
        {
            get { return verificationHint; }
            set
            {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    verificationHint = value.Trim();
                }
                else
                {
                    verificationHint = "Ask for serial number or color";
                }
            }
        }

        public FoundItem(int id, string name, string cat, string loc, string desc, string finder, string phone, string desk, string hint)
            : base(id, name, cat, loc, desc)
        {
            FinderName = finder;
            FinderPhone = phone;
            StorageDesk = desk;
            VerificationHint = hint;
        }

        public override string GetItemType()
        {
            return "FOUND";
        }

        public override void DisplaySummary()
        {
            Console.WriteLine("[FOUND] ID: " + ItemId + " | " + ItemName + " (" + Category + ") | Kept At: " + StorageDesk + " | Status: " + Status);
        }

        public override void DisplayFullDetails()
        {
            base.DisplayFullDetails();
            Console.WriteLine("Found By      : " + FinderName);
            Console.WriteLine("Finder Phone  : " + FinderPhone);
            Console.WriteLine("Storage Desk  : " + StorageDesk);
            Console.WriteLine("Verify Hint   : " + VerificationHint);
            Console.WriteLine(".................................");
        }

        public override string ToFileLine()
        {
            return base.ToFileLine() + "|" + FinderName + "|" + FinderPhone + "|" + StorageDesk + "|" + VerificationHint;
        }
    }

    // SECTION 4: USER & ADMIN HIERARCHY
    // OOP Concepts: Secondary Inheritance Tree & Role-Based Access

    public class User
    {
        private string userName;
        private string userId;
        private string department;

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        public string UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        public string Department
        {
            get { return department; }
            set { department = value; }
        }

        public User(string name, string id, string dept)
        {
            UserName = name;
            UserId = id;
            Department = dept;
        }

        public virtual string GetRole()
        {
            return "Standard Student/Staff";
        }

        public virtual void ShowBanner()
        {
            Console.WriteLine("Logged in as: " + UserName + " (" + UserId + ") | Role: " + GetRole());
        }
    }

    public class Admin : User
    {
        private string adminPin;

        public string AdminPin
        {
            get { return adminPin; }
            set { adminPin = value; }
        }

        public Admin(string name, string id, string dept, string pin)
            : base(name, id, dept)
        {
            AdminPin = pin;
        }

        public override string GetRole()
        {
            return "System Administrator";
        }

        public bool VerifyPin(string enteredPin)
        {
            return AdminPin == enteredPin;
        }

        public override void ShowBanner()
        {
            Console.WriteLine("[ADMIN ACCESS] " + UserName + " | Dept: " + Department + " | Full Privileges Enabled");
        }
    }
    

    // SECTION 5: INPUT VALIDATOR (Helper Class)
    // Purpose: Safely reads strings, integers, doubles, and choices without crashes

    public class InputValidator
    {
        public static string ReadNonEmptyString(string prompt)
        {
            string input = "";
            while (string.IsNullOrWhiteSpace(input))
            {
                Console.Write(prompt);
                input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("  [!] Input cannot be empty. Please try again.");
                }
            }
            return input.Trim();
        }

        public static string ReadOptionalString(string prompt, string defaultValue)
        {
            Console.Write(prompt);
            string input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                return defaultValue;
            }
            return input.Trim();
        }

        public static int ReadValidInt(string prompt, int min, int max)
        {
            int number = 0;
            bool isValid = false;

            while (!isValid)
            {
                Console.Write(prompt);
                string raw = Console.ReadLine();

                if (int.TryParse(raw, out number))
                {
                    if (number >= min && number <= max)
                    {
                        isValid = true;
                    }
                    else
                    {
                        Console.WriteLine("  [!] Please enter a number between " + min + " and " + max + ".");
                    }
                }
                else
                {
                    Console.WriteLine("  [!] Invalid number format. Enter digits only.");
                }
            }
            return number;
        }

        public static double ReadValidDouble(string prompt, double min)
        {
            double number = 0;
            bool isValid = false;

            while (!isValid)
            {
                Console.Write(prompt);
                string raw = Console.ReadLine();

                if (double.TryParse(raw, out number))
                {
                    if (number >= min)
                    {
                        isValid = true;
                    }
                    else
                    {
                        Console.WriteLine("  [!] Value cannot be less than " + min + ".");
                    }
                }
                else
                {
                    Console.WriteLine("  [!] Invalid amount. Enter numeric value (e.g., 0 or 500).");
                }
            }
            return number;
        }

        public static string SelectCategory()
        {
            Console.WriteLine("\n  Select Item Category:");
            Console.WriteLine("    1. Electronics (Phone, Laptop, Charger, Earbuds)");
            Console.WriteLine("    2. Documents & ID Cards (CNIC, Student Card, Wallet)");
            Console.WriteLine("    3. Books & Stationery");
            Console.WriteLine("    4. Keys & Accessories (Watch, Glasses, Bag)");
            Console.WriteLine("    5. Other");

            int catChoice = ReadValidInt("  Enter Category Choice (1-5): ", 1, 5);

            if (catChoice == 1) return "Electronics";
            if (catChoice == 2) return "Documents/ID";
            if (catChoice == 3) return "Books/Stationery";
            if (catChoice == 4) return "Keys/Accessories";
            return "Other";
        }
    }

    // SECTION 6: FILE MANAGER (Persistence Layer)
    // Purpose: Saves and loads all Lost & Found records to/from a text file
    public class FileManager
    {
        private string filePath;

        public FileManager(string path)
        {
            filePath = path;
        }

        public void SaveAllRecords(Item[] records, int count)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    for (int i = 0; i < count; i++)
                    {
                        if (records[i] != null)
                        {
                            writer.WriteLine(records[i].ToFileLine());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("  [ERROR] Could not save data to file: " + ex.Message);
            }
        }

        public int LoadAllRecords(Item[] records)
        {
            int loadedCount = 0;

            if (!File.Exists(filePath))
            {
                return 0;
            }

            try
            {
                string[] lines = File.ReadAllLines(filePath);

                for (int i = 0; i < lines.Length; i++)
                {
                    if (loadedCount >= records.Length)
                    {
                        break;
                    }

                    string line = lines[i];
                    if (string.IsNullOrWhiteSpace(line))
                    {
                        continue;
                    }

                    string[] parts = line.Split('|');

                    if (parts.Length >= 12)
                    {
                        string type = parts[0];
                        int id = int.Parse(parts[1]);
                        string name = parts[2];
                        string cat = parts[3];
                        string loc = parts[4];
                        string desc = parts[5];
                        string status = parts[6];
                        string date = parts[7];

                        if (type == "LOST")
                        {
                            string owner = parts[8];
                            string phone = parts[9];
                            string email = parts[10];
                            double reward = double.Parse(parts[11]);

                            LostItem lost = new LostItem(id, name, cat, loc, desc, owner, phone, email, reward);
                            lost.Status = status;
                            lost.DateReported = date;

                            records[loadedCount] = lost;
                            loadedCount++;
                        }
                        else if (type == "FOUND")
                        {
                            string finder = parts[8];
                            string phone = parts[9];
                            string desk = parts[10];
                            string hint = parts[11];

                            FoundItem found = new FoundItem(id, name, cat, loc, desc, finder, phone, desk, hint);
                            found.Status = status;
                            found.DateReported = date;

                            records[loadedCount] = found;
                            loadedCount++;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("  [ERROR] Failed while loading saved records: " + ex.Message);
            }

            return loadedCount;
        }
    }
    // SECTION 7: REPORT GENERATOR & SMART AUTO-MATCHER
    // Purpose: Generates system analytics and suggests matching lost/found items
    public class ReportGenerator
    {
        public static void ShowSystemStatistics(Item[] records, int count)
        {
            int totalLost = 0;
            int totalFound = 0;
            int recoveredCount = 0;
            int pendingCount = 0;

            for (int i = 0; i < count; i++)
            {
                if (records[i] == null) continue;

                if (records[i].GetItemType() == "LOST")
                {
                    totalLost++;
                }
                else if (records[i].GetItemType() == "FOUND")
                {
                    totalFound++;
                }

                if (records[i].Status == "Recovered" || records[i].Status == "Claimed")
                {
                    recoveredCount++;
                }
                else
                {
                    pendingCount++;
                }
            }

            double recoveryRate = 0;
            if (count > 0)
            {
                recoveryRate = ((double)recoveredCount / count) * 100;
            }

            Console.WriteLine("\n.................................................");
            Console.WriteLine("          CAMPUS LOST & FOUND ANALYTICS           ");
            Console.WriteLine(".................................................");
            Console.WriteLine("Total Records Logged : " + count);
            Console.WriteLine("Total Lost Reports   : " + totalLost);
            Console.WriteLine("Total Found Reports  : " + totalFound);
            Console.WriteLine("Items Recovered      : " + recoveredCount);
            Console.WriteLine("Still Pending        : " + pendingCount);
            Console.WriteLine("Recovery Rate        : " + Math.Round(recoveryRate, 2) + "%");
            Console.WriteLine(".................................................");
        }

        // Smart Feature: Checks if a newly reported item matches existing opposite records
        public static void CheckSmartMatches(Item newItem, Item[] records, int count)
        {
            Console.WriteLine("\n  [SMART SCAN] Checking database for potential matches...");
            bool matchFound = false;

            string targetType = (newItem.GetItemType() == "LOST") ? "FOUND" : "LOST";
            string keyword = newItem.ItemName.ToLower();

            for (int i = 0; i < count; i++)
            {
                if (records[i] == null || records[i].ItemId == newItem.ItemId)
                {
                    continue;
                }

                if (records[i].GetItemType() == targetType && records[i].Status == "Pending")
                {
                    bool nameMatches = records[i].ItemName.ToLower().Contains(keyword) || keyword.Contains(records[i].ItemName.ToLower());
                    bool categoryMatches = records[i].Category == newItem.Category;

                    if (nameMatches || categoryMatches)
                    {
                        if (!matchFound)
                        {
                            Console.WriteLine("  [!] POTENTIAL MATCH FOUND IN DATABASE:");
                            matchFound = true;
                        }
                        records[i].DisplaySummary();
                    }
                }
            }

            if (!matchFound)
            {
                Console.WriteLine("  [-] No immediate matches found yet. Record saved for future matching.");
            }
        }
    }

    // SECTION 8: MAIN CONTROLLER CLASS (Program)
    // Purpose: Controls menus, user sessions, search filters, and status updates
    public class Program
    {
        static Item[] records = new Item[200];
        static int recordCount = 0;
        static FileManager fileManager = new FileManager("lost_and_found_data.txt");
        static User currentUser;

        static void Main(string[] args)
        {
            Console.Title = "Smart Lost & Found Management System";

            // Load previous records from file if available
            recordCount = fileManager.LoadAllRecords(records);

            // Seed sample data if database is completely empty
            if (recordCount == 0)
            {
                SeedInitialData();
            }

            SetupUserSession();
            RunMainMenu();
        }

        static void SeedInitialData()
        {
            records[0] = new LostItem(101, "Black Leather Wallet", "Documents/ID", "Main Cafeteria", "Contains university ID and cash", "Ali Raza", "0300-1234567", "ali@uni.edu", 1000);
            records[1] = new FoundItem(102, "HP Laptop Charger", "Electronics", "CS Lab 3", "65W black pin charger", "Sara Khan", "0321-9876543", "CS Department Desk", "Check serial sticker");
            records[2] = new LostItem(103, "Scientific Calculator", "Books/Stationery", "Library 2nd Floor", "Casio fx-991ES Plus", "Bilal Ahmed", "0333-4567890", "bilal@uni.edu", 0);
            recordCount = 3;
            fileManager.SaveAllRecords(records, recordCount);
        }

        static void SetupUserSession()
        {
            Console.WriteLine(".................................................");
            Console.WriteLine("     SMART LOST & FOUND MANAGEMENT SYSTEM         ");
            Console.WriteLine(".................................................");
            Console.WriteLine("Select Login Mode:");
            Console.WriteLine("  1. Student / Staff User");
            Console.WriteLine("  2. System Administrator (Requires PIN)");

            int mode = InputValidator.ReadValidInt("Enter Choice (1-2): ", 1, 2);
            string name = InputValidator.ReadNonEmptyString("Enter Your Name: ");
            string id = InputValidator.ReadNonEmptyString("Enter University ID: ");
            string dept = InputValidator.ReadOptionalString("Enter Department (Default: CS): ", "CS");

            if (mode == 2)
            {
                string pin = InputValidator.ReadNonEmptyString("Enter Admin PIN (Default is 1234): ");
                Admin admin = new Admin(name, id, dept, "1234");

                if (admin.VerifyPin(pin))
                {
                    currentUser = admin;
                    Console.WriteLine("  [+] Admin authentication successful.");
                }
                else
                {
                    Console.WriteLine("  [!] Incorrect PIN. Logging in as Standard User.");
                    currentUser = new User(name, id, dept);
                }
            }
            else
            {
                currentUser = new User(name, id, dept);
            }
        }

        static void RunMainMenu()
        {
            int choice = 0;

            while (choice != 8)
            {
                Console.WriteLine("\n--------------------------------------------------");
                currentUser.ShowBanner();
                Console.WriteLine("--------------------------------------------------");
                Console.WriteLine("  1. Report a LOST Item");
                Console.WriteLine("  2. Report a FOUND Item");
                Console.WriteLine("  3. View All Records");
                Console.WriteLine("  4. Search Items (By Name / Category / Location)");
                Console.WriteLine("  5. View Full Details of an Item (By ID)");
                Console.WriteLine("  6. Mark Item as Recovered / Claimed");
                Console.WriteLine("  7. View System Statistics & Analytics");
                Console.WriteLine("  8. Save & Exit System");
                Console.WriteLine("--------------------------------------------------");

                choice = InputValidator.ReadValidInt("Select an Option (1-8): ", 1, 8);

                switch (choice)
                {
                    case 1:
                        HandleReportLostItem();
                        break;
                    case 2:
                        HandleReportFoundItem();
                        break;
                    case 3:
                        DisplayAllRecords();
                        break;
                    case 4:
                        HandleSearchMenu();
                        break;
                    case 5:
                        ViewSingleItemDetails();
                        break;
                    case 6:
                        UpdateItemStatus();
                        break;
                    case 7:
                        ReportGenerator.ShowSystemStatistics(records, recordCount);
                        break;
                    case 8:
                        fileManager.SaveAllRecords(records, recordCount);
                        Console.WriteLine("\nAll records saved to disk. Shutting down system...");
                        break;
                }
            }
        }

        static int GenerateNextId()
        {
            int maxId = 100;
            for (int i = 0; i < recordCount; i++)
            {
                if (records[i] != null && records[i].ItemId > maxId)
                {
                    maxId = records[i].ItemId;
                }
            }
            return maxId + 1;
        }

        static void HandleReportLostItem()
        {
            if (recordCount >= records.Length)
            {
                Console.WriteLine("  [!] Database storage full!");
                return;
            }

            Console.WriteLine("\n--- REPORT A LOST ITEM ---");
            int newId = GenerateNextId();
            string name = InputValidator.ReadNonEmptyString("Item Name: ");
            string category = InputValidator.SelectCategory();
            string location = InputValidator.ReadNonEmptyString("Where did you lose it?: ");
            string desc = InputValidator.ReadNonEmptyString("Color / Brand / Identifying Marks: ");
            string owner = InputValidator.ReadNonEmptyString("Owner Full Name: ");
            string phone = InputValidator.ReadNonEmptyString("Owner Phone Number: ");
            string email = InputValidator.ReadOptionalString("Owner Email (Optional): ", "N/A");
            double reward = InputValidator.ReadValidDouble("Reward Offered in Rs. (0 if none): ", 0);

            LostItem newLost = new LostItem(newId, name, category, location, desc, owner, phone, email, reward);
            records[recordCount] = newLost;
            recordCount++;

            fileManager.SaveAllRecords(records, recordCount);
            Console.WriteLine("\n  [+] Lost Item Registered Successfully! Assigned ID: " + newId);

            ReportGenerator.CheckSmartMatches(newLost, records, recordCount);
        }

        static void HandleReportFoundItem()
        {
            if (recordCount >= records.Length)
            {
                Console.WriteLine("  [!] Database storage full!");
                return;
            }

            Console.WriteLine("\n--- REPORT A FOUND ITEM ---");
            int newId = GenerateNextId();
            string name = InputValidator.ReadNonEmptyString("Item Name: ");
            string category = InputValidator.SelectCategory();
            string location = InputValidator.ReadNonEmptyString("Where did you find it?: ");
            string desc = InputValidator.ReadNonEmptyString("Brief Description: ");
            string finder = InputValidator.ReadNonEmptyString("Your Name (Finder): ");
            string phone = InputValidator.ReadNonEmptyString("Your Contact Number: ");
            string desk = InputValidator.ReadNonEmptyString("Where is the item kept now? (e.g., Admin Office): ");
            string hint = InputValidator.ReadNonEmptyString("Security Question/Hint to verify real owner: ");

            FoundItem newFound = new FoundItem(newId, name, category, location, desc, finder, phone, desk, hint);
            records[recordCount] = newFound;
            recordCount++;

            fileManager.SaveAllRecords(records, recordCount);
            Console.WriteLine("\n  [+] Found Item Registered Successfully! Assigned ID: " + newId);

            ReportGenerator.CheckSmartMatches(newFound, records, recordCount);
        }

        static void DisplayAllRecords()
        {
            Console.WriteLine("\n--- ALL LOST & FOUND RECORDS (" + recordCount + ") ---");
            if (recordCount == 0)
            {
                Console.WriteLine("  No records found in the system.");
                return;
            }

            for (int i = 0; i < recordCount; i++)
            {
                records[i].DisplaySummary();
            }
        }

        static void HandleSearchMenu()
        {
            Console.WriteLine("\n--- SEARCH ITEMS ---");
            Console.WriteLine("  1. Search by Item Name");
            Console.WriteLine("  2. Search by Category");
            Console.WriteLine("  3. Search by Location");
            Console.WriteLine("  4. Show Only Pending Items");

            int searchType = InputValidator.ReadValidInt("Select Search Filter (1-4): ", 1, 4);
            string query = "";

            if (searchType == 2)
            {
                query = InputValidator.SelectCategory().ToLower();
            }
            else if (searchType != 4)
            {
                query = InputValidator.ReadNonEmptyString("Enter Search Keyword: ").ToLower();
            }

            Console.WriteLine("\n--- SEARCH RESULTS ---");
            int matches = 0;

            for (int i = 0; i < recordCount; i++)
            {
                bool isMatch = false;

                if (searchType == 1 && records[i].ItemName.ToLower().Contains(query))
                {
                    isMatch = true;
                }
                else if (searchType == 2 && records[i].Category.ToLower() == query)
                {
                    isMatch = true;
                }
                else if (searchType == 3 && records[i].Location.ToLower().Contains(query))
                {
                    isMatch = true;
                }
                else if (searchType == 4 && records[i].Status == "Pending")
                {
                    isMatch = true;
                }

                if (isMatch)
                {
                    records[i].DisplaySummary();
                    matches++;
                }
            }

            if (matches == 0)
            {
                Console.WriteLine("  No matching items found.");
            }
        }

        static void ViewSingleItemDetails()
        {
            int targetId = InputValidator.ReadValidInt("\nEnter Item ID to inspect: ", 1, 99999);

            for (int i = 0; i < recordCount; i++)
            {
                if (records[i].ItemId == targetId)
                {
                    records[i].DisplayFullDetails();
                    return;
                }
            }

            Console.WriteLine("  [!] No item found with ID: " + targetId);
        }

        static void UpdateItemStatus()
        {
            int targetId = InputValidator.ReadValidInt("\nEnter Item ID to mark as Recovered: ", 1, 99999);

            for (int i = 0; i < recordCount; i++)
            {
                if (records[i].ItemId == targetId)
                {
                    if (records[i].Status == "Recovered")
                    {
                        Console.WriteLine("  [!] This item is already marked as Recovered.");
                        return;
                    }

                    records[i].DisplayFullDetails();
                    string confirm = InputValidator.ReadNonEmptyString("Confirm recovery & status update? (yes/no): ").ToLower();

                    if (confirm == "yes" || confirm == "y")
                    {
                        records[i].Status = "Recovered";
                        fileManager.SaveAllRecords(records, recordCount);
                        Console.WriteLine("  [+] Success! Item ID " + targetId + " is now marked as RECOVERED.");
                    }
                    else
                    {
                        Console.WriteLine("  [-] Status update cancelled.");
                    }
                    return;
                }
            }

            Console.WriteLine("  [!] No item found with ID: " + targetId);
        }
    }
}
