using Exception_Handling;
using Sources;
using System.Data;
using System.IO;
using UserInterface;

namespace FileHandling
{
    public class IncidentLogs : Information
    {
        string searchOption, searchDate;
        public void WriteIncidents()
        {
            Display display = new Display();
            Admin admin = new Admin();
            bool validIncident = true, validOption = true, validDate= true;
            string IncidentPath, ChosenIncident;
            int n = 12;

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(0, 5);
            display.Header("admin");
            Console.SetCursorPosition(88, 20);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Choose which incident to view");
            Console.SetCursorPosition(0, 23);
            display.Header("incidentchoice");
            while (validIncident)
            {
                try
                {               
                    string[] boxLayout = File.ReadAllLines(AppContext.BaseDirectory + @"\Headers\box.txt");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(100, 28);
                    Console.Write("<< ");
                    int incidentChoice = int.Parse(Console.ReadLine());
                    Thread.Sleep(800);
                    switch (incidentChoice)
                    {
                        case 1:
                            ChosenIncident = "fire";
                            break;
                        case 2:
                            ChosenIncident = "medical";
                            break;
                        case 3:
                            ChosenIncident = "crime";
                            break;
                        default:
                            throw new Exception("Invalid Incident Input");
                    }
                    IncidentPath = FilePath(ChosenIncident);
                    Console.Clear();
                    Console.SetCursorPosition(0, 1);
                    display.Header(ChosenIncident);


                    // Get the current date in the same format as the file (MM/dd/yyyy)
                    string currentDate = DateTime.Now.ToString("MM/dd/yyyy");
                    List<string> currentLogs = new List<string>();

                    // Read and filter logs for the current date
                    if (File.Exists(IncidentPath))
                    {
                        using (StreamReader reader = File.OpenText(IncidentPath))
                        {
                            string line;
                            bool isCurrentLog = false;
                            while ((line = reader.ReadLine()) != null)
                            {
                                if (line.Contains($"Date & Time: {currentDate}"))
                                {
                                    isCurrentLog = true;
                                    currentLogs.Add(line); // Add the matching line
                                }
                                else if (isCurrentLog && line.StartsWith("+-------- INCIDENT LOG --------+"))
                                {
                                    // End of the current log entry
                                    isCurrentLog = false;
                                }
                                else if (isCurrentLog)
                                {
                                    currentLogs.Add(line); // Add the rest of the log details
                                }
                            }
                        }

                        // Display logs for the current date with scrolling support
                        Console.SetCursorPosition(60, n);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Incident Logs for Today ({currentDate}):");

                        if (currentLogs.Count > 0)
                        {
                            int pageSize = Console.WindowHeight - (n + 5); // Calculate available space for logs
                            int startIndex = 0;
                            bool exitScrolling = false;

                            // Method to display the logs
                            void DisplayLogs(int start)
                            {
                                Console.SetCursorPosition(60, n);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine($"Incident Logs for Today ({currentDate}):");

                                int displayedLines = 0;
                                for (int i = start; i < currentLogs.Count && displayedLines < pageSize; i++, displayedLines++)
                                {
                                    Console.SetCursorPosition(60, n + 2 + displayedLines);
                                    Console.WriteLine(currentLogs[i]);
                                }

                                Console.SetCursorPosition(60, Console.WindowHeight - 2);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("Use Up/Down arrows to scroll, Esc to Search/Exit");
                            }

                            DisplayLogs(startIndex);

                            // Handle scrolling
                            while (!exitScrolling)
                            {
                                if (Console.KeyAvailable)
                                {
                                    ConsoleKeyInfo key = Console.ReadKey(true);
                                    switch (key.Key)
                                    {
                                        case ConsoleKey.DownArrow:
                                            if (startIndex + pageSize < currentLogs.Count)
                                            {
                                                startIndex++;
                                                DisplayLogs(startIndex);
                                            }
                                            break;
                                        case ConsoleKey.UpArrow:
                                            if (startIndex > 0)
                                            {
                                                startIndex--;
                                                DisplayLogs(startIndex);
                                            }
                                            break;
                                        case ConsoleKey.Escape:
                                            exitScrolling = true;
                                            break;
                                    }
                                }
                            }
                            Console.Clear();
                        }
                        else
                        {
                            n += 2;
                            Console.SetCursorPosition(60, n);
                            Console.WriteLine("No logs found for today.");
                        }
                    }
                    else
                    {
                        Console.SetCursorPosition(60, n);
                        Console.WriteLine("Error: Incident file not found.");
                        return;
                    }


                    // Ask if the user wants to search a specific date
                    n += 2;
                    Console.SetCursorPosition(60, n);
                    Console.WriteLine("Do you want to search for logs of a specific date? ");
                    while (validOption)
                    {
                        try
                        {
                            Console.ForegroundColor = ConsoleColor.White;
                            Console.SetCursorPosition(60, n+5);
                            searchOption = ErrorHandling.ReadYesNoInput("<< ");
                            n += 2;
                            Console.SetCursorPosition(137, n);
                            Console.WriteLine(searchOption);
                            Console.SetCursorPosition(60, n + 3);
                            Console.WriteLine(new string(' ', searchOption.Length+3));
                            Console.SetCursorPosition(83, n + 6);
                            Console.WriteLine(new string(' ', Console.WindowWidth - 83));
                            validOption = false;
                        }
                        catch (Exception ex)
                        {
                            Console.SetCursorPosition(83, n + 6);
                            Console.WriteLine(ex.Message);
                            Console.SetCursorPosition(60, n + 3);
                            Console.WriteLine(new string(' ', Math.Max(ChosenIncident?.Length ?? 100, 100)));
                        }
                    }
                    
                   if (searchOption.ToLower() == "yes")
                   {
                        while (validDate)
                        {
                            try
                            {
                                n += 2;
                                Console.SetCursorPosition(60, n);
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.Write("Enter the date to search (MM/DD/YYYY): ");
                                Console.ForegroundColor = ConsoleColor.White;
                                Console.SetCursorPosition(60, n+5);
                                Console.Write("<< ");
                                searchDate = Console.ReadLine()?.Trim();
                                n += 2;
                                Console.SetCursorPosition(137, n);
                                Console.WriteLine(searchDate);
                                Console.SetCursorPosition(60, n + 3);
                                Console.WriteLine(new string(' ', searchDate.Length + 3));
                                Console.SetCursorPosition(83, n + 6);
                                Console.WriteLine(new string(' ', Console.WindowWidth - 83));
                                Thread.Sleep(1500);
                                // Validate date input
                                if (!DateTime.TryParseExact(searchDate, "MM/dd/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
                                {
                                    throw new ArgumentException("Invalid date format. Please use MM/DD/YYYY.");
                                }
                                validDate = false;
                            }
                            catch (ArgumentException ex)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.SetCursorPosition(83, n + 6);
                                Console.WriteLine(ex.Message);
                                Console.SetCursorPosition(60, n + 3);
                                Console.WriteLine(new string(' ', Math.Max(ChosenIncident?.Length ?? 100, 100)));
                            }
                        }                                       

                        // Search and display logs for the entered date
                        List<string> filteredLogs = new List<string>();
                        using (StreamReader reader = File.OpenText(IncidentPath))
                        {
                            string line;
                            bool isMatchingLog = false;

                            // Configure console buffer and window size
                            int consoleWidth = Console.WindowWidth;
                            int consoleHeight = Console.WindowHeight;
                            Console.Clear();
                            Console.BufferHeight = Math.Max(1000, consoleHeight); // Allow scrolling for long outputs

                            // Header for centered display
                            int headerX = consoleWidth / 2 - 10; // Adjusted for center alignment
                            Console.SetCursorPosition(headerX-7, 2);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("INCIDENT LOGS FOR THE SELECTED DATE");
                            Console.ResetColor();

                            int logY = 4; // Start displaying logs below the header

                            while ((line = reader.ReadLine()) != null)
                            {
                                if (line.Contains($"Date & Time: {searchDate}"))
                                {
                                    isMatchingLog = true;
                                    filteredLogs.Add(line); // Add the matching line
                                }
                                else if (isMatchingLog && line.StartsWith("+----- INCIDENT LOG -----+"))
                                {
                                    // End of the current log entry
                                    isMatchingLog = false;
                                }
                                else if (isMatchingLog)
                                {
                                    filteredLogs.Add(line); // Add the rest of the log details
                                }
                            }

                            // Display logs
                            foreach (var log in filteredLogs)
                            {
                                // Center-align each log entry
                                int logX = consoleWidth / 2 - log.Length / 2;
                                if (logY >= Console.BufferHeight - 1)
                                {
                                    Console.BufferHeight += 50; // Extend buffer if logs exceed current buffer height
                                }
                                Console.SetCursorPosition(logX, logY);
                                Console.WriteLine(log);
                                logY++;
                            }

                            // Footer to prompt user
                            int footerX = consoleWidth / 2 - 20;
                            Console.SetCursorPosition(footerX, logY + 2);
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Press any key to return to the main menu...");
                            Console.ResetColor();
                            Console.ReadKey();
                            Console.Clear();
                        }                      
                    }
                    else
                    {
                        n += 2;
                        Console.SetCursorPosition(53, n);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Returning to the main menu...");
                        Thread.Sleep(1500);
                        Console.Clear();
                    }

                    validIncident = false;
                }
                catch (ArgumentException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(100, 31);
                    Console.WriteLine(ex.Message);
                    Console.SetCursorPosition(0, 28);
                    Console.Write(new string(' ', Console.WindowWidth));
                }
                catch (FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(87, 31);
                    Console.WriteLine("Invalid Input. Enter numbers 1-3 only.");
                    Console.SetCursorPosition(0, 28);
                    Console.Write(new string(' ', Console.WindowWidth));
                }
            }
        }
    }

    public class Admin : Information
    {
        private string UpdateIncident;

        public void Security()
        {
            Display display = new Display();
            EmergencyCallScreen emergencyCallScreen = new EmergencyCallScreen();
            bool validPIN = true;
            string correctPIN = "swiftaid123";
            int attempts = 3;

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(0, 6);
            display.Header("admin");
            Console.SetCursorPosition(0, 20);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            display.Header("box1");
            Console.SetCursorPosition(77, 25);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Enter Admin PIN");            
            
            while (attempts > 0 && validPIN)
            {
                string inputPIN = "";
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(77, 32);
                ConsoleKeyInfo key;
                do
                {
                    key = Console.ReadKey(intercept: true);
                    if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                    {
                        inputPIN += key.KeyChar;
                        Console.Write("*");
                    }
                    else if (key.Key == ConsoleKey.Backspace && inputPIN.Length > 0)
                    {
                        inputPIN = inputPIN.Substring(0, inputPIN.Length - 1);
                        Console.Write("\b \b");
                    }
                } while (key.Key != ConsoleKey.Enter && attempts > 0);

                if (inputPIN == correctPIN)
                {
                    Console.SetCursorPosition(0, 36);
                    Console.Write(new string(' ', Console.WindowWidth));
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.SetCursorPosition(98, 36);
                    Console.WriteLine("Access Granted");
                    Thread.Sleep(3000);
                    Console.Clear();                  
                    //emergencyCallScreen.DisplayAdminScreen();
                    validPIN = false;
                    AdminDsiplay();
                }
                else
                {
                    Console.SetCursorPosition(77, 32);
                    Console.Write(new string(' ', inputPIN.Length));
                    Console.SetCursorPosition(0, 36);
                    Console.Write(new string(' ', Console.WindowWidth));
                    attempts--;
                    Console.ForegroundColor = ConsoleColor.Red;
                    if(attempts > 1)
                    {
                        Console.SetCursorPosition(85, 36);
                        Console.WriteLine($"Incorrect PIN. You have {attempts} attempts left.");

                    }
                    else
                    {
                        Console.SetCursorPosition(86, 36);
                        Console.WriteLine($"Incorrect PIN. You have {attempts} attempt left.");
                    }
                }
            }
        }

        public void AdminDsiplay()
        {
            Display display = new Display();
            IncidentLogs incidentLogs = new IncidentLogs();
            bool validAction = true;
            int AdminChoice;

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(0, 6);
            display.Header("admin");
            Console.SetCursorPosition(0, 22);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            display.Header("adminchoice");
            Console.SetCursorPosition(77, 25);
            
            while (validAction)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(100, 28);
                Console.Write("<< ");
                try
                {
                    AdminChoice = int.Parse(Console.ReadLine());
                    Thread.Sleep(800);
                    switch (AdminChoice)
                    {
                        case 1:
                            incidentLogs.WriteIncidents();
                            validAction = false;
                            break;
                        case 2:
                            UpdateFile();
                            validAction = false;
                            break;
                        case 3:
                            ArchiveFiles();
                            validAction = false;
                            break;
                        default:
                            throw new ArgumentException("Invalid Input");
                    }
                }
                catch (ArgumentException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(100, 31);
                    Console.WriteLine(ex.Message);
                    Console.SetCursorPosition(0, 28);
                    Console.Write(new string(' ', Console.WindowWidth));
                }
                catch (FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(87, 31);
                    Console.WriteLine("Invalid Input. Enter numbers 1-3 only.");
                    Console.SetCursorPosition(0, 28);
                    Console.Write(new string(' ', Console.WindowWidth));
                }
            }
        }

        public void UpdateFile()
        {
            bool validUpdate = true, validDate = true;
            string filepath, ChosenIncident, IncidentPath; 
            string[] lines1, lines2, lines3;
            int n = 5;
            Display display = new Display();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(0, 7);
            display.Header("update");
            Console.SetCursorPosition(80, 20);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Choose which incident you wish to update the status");
            Console.SetCursorPosition(0, 23);
            display.Header("incidentchoice");
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(100, 28);
            Console.Write("<< ");
            int UpdateIncident = int.Parse(Console.ReadLine());
            switch (UpdateIncident)
            {
                case 1:
                    ChosenIncident = "fire";
                    break;
                case 2:
                    ChosenIncident = "medical";
                    break;
                case 3:
                    ChosenIncident = "crime";
                    break;
                default:
                    throw new Exception("Invalid Incident Input");
            }
            IncidentPath = FilePath(ChosenIncident);
            lines1 = File.ReadAllLines(IncidentPath);
            Console.Clear();
            Console.SetCursorPosition(0, 1);
            display.Header(ChosenIncident);
            Console.SetCursorPosition(0, 10);
            display.Header("box");
            Console.SetCursorPosition(53, 15);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Enter the date to update (MM/DD/YYYY): ");

            string searchDate = string.Empty;
            while (validDate)
            {
                try
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(53, 48);
                    searchDate = Console.ReadLine()?.Trim();

                    // Validate date input
                    if (!DateTime.TryParseExact(searchDate, "MM/dd/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
                    {
                        throw new ArgumentException("Invalid date format. Please use MM/DD/YYYY.");
                    }
                    validDate = false; // Exit the loop once a valid date is entered
                }
                catch (ArgumentException ex)
                {
                    Console.SetCursorPosition(80, 52);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(ex.Message);
                }
            }

            Thread.Sleep(700);
            Console.Clear();

            try
            {
                using (StreamWriter writer = new StreamWriter(IncidentPath, false))
                {
                    bool statusUpdated = false;
                    foreach (string line in lines1)
                    {
                        if (line.Contains("Status: In Progress"))
                        {
                            Console.SetCursorPosition(80, n);
                            Console.Write("Status: ");
                            Console.ForegroundColor = ConsoleColor.White;
                            string status = Console.ReadLine()?.Trim();

                            if (string.IsNullOrEmpty(status))
                            {
                                status = "In Progress";
                            }

                            string updatedLine = $"Status: {status} (Updated on {searchDate})";
                            writer.WriteLine(updatedLine);
                            statusUpdated = true;
                        }
                        else
                        {
                            Console.SetCursorPosition(80, n);
                            Console.WriteLine(line);
                            writer.WriteLine(line);
                        }
                        n++;
                    }

                    CenterText(statusUpdated ? "Fire Incidents Updated Successfully" : "No status found to update.");
                    Thread.Sleep(2500);
                    Console.Clear();
                }
            }
            catch (Exception ex)
            {
                CenterText($"Error saving incident: {ex.Message}");
            }

        }

        public void ArchiveFiles()
        {
            Display display = new Display();
            EmergencyCallScreen emergencyCallScreen = new EmergencyCallScreen();
            string filepath, firefile, medicalfile, crimefile;
            string OriginalFilePath, originalfirefile, originalmedicalfile, originalcrimefile;
            bool validArchive = true;

            Console.Clear();
            Console.SetCursorPosition(0, 5);
            Console.ForegroundColor = ConsoleColor.Cyan;
            display.Header("archive");
            Console.SetCursorPosition(80, 17);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("Choose which incident you wish to archive");
            Console.SetCursorPosition(0, 20);
            display.Header("archive choice");
            while (validArchive)
            {
                try
                {                  
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(97, 30);
                    Console.Write("<< ");
                    int ArchiveIncident = int.Parse(Console.ReadLine());

                    switch(ArchiveIncident)
                    {
                        case 1:
                            filepath = AppContext.BaseDirectory + @"\Archive\Fire Incidents.txt";
                            OriginalFilePath = AppContext.BaseDirectory + @"\Incidents\fire incidents.txt";

                            Console.Clear();
                            if (!File.Exists(filepath))
                            {
                                using (StreamWriter create = File.CreateText(filepath))
                                { }
                            }
                            try
                            {
                                using (StreamReader sr = new StreamReader(OriginalFilePath))
                                {
                                    string line;
                                    while ((line = sr.ReadLine()) != null)
                                    {
                                        using (StreamWriter writer = new StreamWriter(filepath, true))
                                        {
                                            writer.WriteLine(line);
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error saving incident: {ex.Message}");
                            }
                            if (File.Exists(OriginalFilePath))
                            {
                                File.Delete(OriginalFilePath);
                            }
                            emergencyCallScreen.DisplayArchiveScreen("Fire");
                            validArchive = false;
                            break;
                        case 2:
                            filepath = AppContext.BaseDirectory + @"\Archive\Medical Incidents.txt";
                            OriginalFilePath = AppContext.BaseDirectory + @"\Incidents\medical incidents.txt";

                            Console.Clear();
                            if (!File.Exists(filepath))
                            {
                                using (StreamWriter create = File.CreateText(filepath))
                                { }
                            }
                            try
                            {
                                using (StreamReader sr = new StreamReader(OriginalFilePath))
                                {
                                    string line;
                                    while ((line = sr.ReadLine()) != null)
                                    {
                                        using (StreamWriter writer = new StreamWriter(filepath, true))
                                        {
                                            writer.WriteLine(line);
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error saving incident: {ex.Message}");
                            }
                            if (File.Exists(OriginalFilePath))
                            {
                                File.Delete(OriginalFilePath);
                            }
                            emergencyCallScreen.DisplayArchiveScreen("Medical");
                            validArchive = false;
                            break;
                        case 3:
                            filepath = AppContext.BaseDirectory + @"\Archive\Crime Incidents.txt";
                            OriginalFilePath = AppContext.BaseDirectory + @"\Incidents\crime incidents.txt";
                            
                            Console.Clear();
                            if (!File.Exists(filepath))
                            {
                                using (StreamWriter create = File.CreateText(filepath))
                                { }
                            }
                            try
                            {
                                using (StreamReader sr = new StreamReader(OriginalFilePath))
                                {
                                    string line;
                                    while ((line = sr.ReadLine()) != null)
                                    {
                                        using (StreamWriter writer = new StreamWriter(filepath, true))
                                        {
                                            writer.WriteLine(line);
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error saving incident: {ex.Message}");
                            }
                            if (File.Exists(OriginalFilePath))
                            {
                                File.Delete(OriginalFilePath);
                            }
                            emergencyCallScreen.DisplayArchiveScreen("Crime");
                            validArchive = false;
                            break;
                        case 4:
                            firefile = AppContext.BaseDirectory + @"\Archive\Fire Incidents.txt";
                            originalfirefile = AppContext.BaseDirectory + @"\Incidents\fire incidents.txt";
                         
                            Console.Clear();
                            if (!File.Exists(firefile))
                            {
                                using (StreamWriter create = File.CreateText(firefile))
                                { }
                            }
                            try
                            {
                                using (StreamReader sr = new StreamReader(originalfirefile))
                                {
                                    string line;
                                    while ((line = sr.ReadLine()) != null)
                                    {
                                        using (StreamWriter writer = new StreamWriter(firefile, true))
                                        {
                                            writer.WriteLine(line);
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error saving incident: {ex.Message}");
                            }
                            if (File.Exists(originalfirefile))
                            {
                                File.Delete(originalfirefile);
                            }

                            medicalfile = AppContext.BaseDirectory + @"\Archive\Medical Incidents.txt";
                            originalmedicalfile = AppContext.BaseDirectory + @"\Incidents\medical incidents.txt";

                            if (!File.Exists(medicalfile))
                            {
                                using (StreamWriter create = File.CreateText(medicalfile))
                                { }
                            }

                            try
                            {
                                using (StreamReader sr = new StreamReader(originalmedicalfile))
                                {
                                    string line;
                                    while ((line = sr.ReadLine()) != null)
                                    {
                                        using (StreamWriter writer = new StreamWriter(medicalfile, true))
                                        {
                                            writer.WriteLine(line);
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error saving incident: {ex.Message}");
                            }
                            if (File.Exists(originalmedicalfile))
                            {
                                File.Delete(originalmedicalfile);
                            }

                            crimefile = AppContext.BaseDirectory + @"\Archive\Crime Incidents.txt";
                            originalcrimefile = AppContext.BaseDirectory + @"\Incidents\crime incidents.txt";

                            if (!File.Exists(crimefile))
                            {
                                using (StreamWriter create = File.CreateText(crimefile))
                                { }
                            }

                            try
                            {
                                using (StreamReader sr = new StreamReader(originalcrimefile))
                                {
                                    string line;
                                    while ((line = sr.ReadLine()) != null)
                                    {
                                        using (StreamWriter writer = new StreamWriter(crimefile, true))
                                        {
                                            writer.WriteLine(line);
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error saving incident: {ex.Message}");
                            }
                            if (File.Exists(originalcrimefile))
                            {
                                File.Delete(originalcrimefile);
                            }

                            emergencyCallScreen.DisplayArchiveScreen("All");
                            validArchive = false;
                            break;
                        default:
                            throw new ArgumentException("Invalid Input");
                    }
                }
                catch (ArgumentException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(100, 34);
                    Console.WriteLine(ex.Message);
                    Console.SetCursorPosition(0, 30);
                    Console.Write(new string(' ', Console.WindowWidth));
                }
                catch (FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(83, 34);
                    Console.WriteLine("Invalid Input. Enter numbers 1-3 only.");
                    Console.SetCursorPosition(0, 30);
                    Console.Write(new string(' ', Console.WindowWidth));
                }
            }

        }

        void CenterText(string text)
        {
            int consoleWidth = Console.WindowWidth;
            int textX = (consoleWidth - text.Length) / 2;
            if (textX < 0) textX = 0; // Ensure text is not cut off if it's too long
            Console.SetCursorPosition(textX, Console.CursorTop);
            Console.WriteLine(text);
        }
    }

    public class LocationChecker : Information
    {
        public int DataReading(string location1)
        {
            string filePath = AppContext.BaseDirectory + @"\locations.txt";
            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (location1.IndexOf(line, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            return 0;
                        }
                    }
                }
                return 1;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading file: " + ex.Message);
                return 1;
            }
        }
    }

    public class MedicalEmergency
    {
        public int medicalEmergency(string emergencyname)
        {
            string medicalfile = AppContext.BaseDirectory + @"\medical emergency.txt";

            try
            {
                using (StreamReader medfile = new StreamReader(medicalfile))
                {
                    string line1;
                    int lineNumber = 0;

                    while ((line1 = medfile.ReadLine()) != null)
                    {
                        lineNumber++;

                        if (emergencyname.IndexOf(line1, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            return lineNumber;
                        }
                    }
                }
                return -1;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading file: " + ex.Message);
                return -1;
            }
        }
    }

    public class CrimeEmergency
    {
        public int crimeEmergency(string crimename)
        {
            string crimefile = AppContext.BaseDirectory + @"\crime emergency.txt";

            try
            {
                using (StreamReader crimefile1 = new StreamReader(crimefile))
                {
                    string line2;
                    int lineNumber1 = 0;

                    while ((line2 = crimefile1.ReadLine()) != null)
                    {
                        lineNumber1++;

                        if (crimename.IndexOf(line2, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            return lineNumber1;
                        }
                    }
                }
                return -1;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading file: " + ex.Message);
                return -1;
            }
        }
    }    
}
