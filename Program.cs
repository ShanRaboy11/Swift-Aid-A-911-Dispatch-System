using UserInterface;
using Sources;
using FileHandling;
using System.ComponentModel.Design;
using Exception_Handling;
using System.Runtime.CompilerServices;
using System;


namespace SWIFT_AID_RABOY
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MainMenu mainMenu = new MainMenu();

            mainMenu.RenderMainMenu();
        }
    }

    internal class MainMenu
    {
        protected internal void RenderMainMenu()
        {
            bool repeat = true;
            Message message = new Message();
            Emergency emergency = new Emergency();
            MainMenu menu = new MainMenu();
            IncidentLogs incidentLogs = new IncidentLogs();
            Admin admin = new Admin();
            EmergencyCallScreen emergencyCallScreen = new EmergencyCallScreen();
            Display display = new Display();
            Console.ForegroundColor = ConsoleColor.Green;


            message.IntroDisplay();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            message.IntroDisplay();
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(85, 27);
            Console.Write("Press any key to continue: ");
            Console.ReadKey();
            Console.Clear();
            while (repeat)
            {
                try
                {                  
                    Console.SetCursorPosition(0, 14);
                    ConsoleColor consoleColor = ConsoleColor.Cyan;
                    Console.ForegroundColor = consoleColor;
                    display.Header("main");
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    display.Header("choice1");
                    Console.ForegroundColor = ConsoleColor.White;                  

                    Console.SetCursorPosition(100, 29);
                    Console.Write("<< ");
                    int choice = int.Parse(Console.ReadLine());
                    Thread.Sleep(800);
                    Console.SetCursorPosition(0, 0);
                    switch (choice)
                    {
                        case 1:
                            emergencyCallScreen.DisplayCallingScreen();
                            emergency.display();
                            break;
                        case 2:
                            admin.Security();
                            break;
                        case 3:
                            message.OutroDisplay();
                            repeat = false;
                            break;
                        default:
                            throw new ArgumentException("Invalid Input.");
                    }
                }
                catch (ArgumentException ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(100, 31);
                    Console.WriteLine(ex.Message);
                    ClearConsoleLine(29);
                }
                catch (FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(87, 31);
                    Console.WriteLine("Invalid Input. Enter numbers 1-3 only.");
                    ClearConsoleLine(29);
                }
                catch (StackOverflowException)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(100, 31);
                    Console.WriteLine("Invalid Input.");
                    ClearConsoleLine(29);
                }
            }
        }

        protected internal void PromptToContinue()
        {
            Console.Write("Press any key to continue. ");
            Console.SetCursorPosition(53, 49);
            Console.ForegroundColor = ConsoleColor.White;
            Console.ReadKey();
            Console.Clear();
        }

        protected internal void ClearConsoleLine(int lineNumber)
        {
            Console.SetCursorPosition(0, lineNumber); 
            Console.Write(new string(' ', Console.WindowWidth)); 
        }
    }

    internal class Emergency : Information
    {
        public string InputLocation;
        public string InputIncident;
        public string filepath;
        public string InputName;
        public int IncidentPosition = 0;
        public void display()
        {
            LocationChecker locationChecker = new LocationChecker();
            MainMenu menu = new MainMenu();
            Display display = new Display();
            bool validEmergency = true;
            bool validLocation = true;           

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.SetCursorPosition(0, 3);
            display.Header("emergency");
            Console.SetCursorPosition(0, 11);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            display.Header("box");
            Console.SetCursorPosition(53, 15);
            Console.ForegroundColor = ConsoleColor.Green;         
            Console.WriteLine("What's your emergency? [ FIRE | MEDICAL | CRIME ]");
            while (validEmergency)
            {
                try
                {
                    Console.ForegroundColor = consoleColor;
                    Console.SetCursorPosition(53, 49);
                    InputIncident = ErrorHandling.ReadLetterInput("");
                    if (InputIncident.ToLower() != "fire" && InputIncident.ToLower() != "medical" && InputIncident.ToLower() != "crime")
                    {
                        Console.SetCursorPosition(83, 54);
                        Console.WriteLine(new string(' ', Console.WindowWidth - 83));
                        Console.ForegroundColor = ConsoleColor.Red;
                        throw new ArgumentException("Invalid Incident. Please select valid incident.");
                    }
                    else
                    {
                        AddIncident(InputIncident);
                        Console.SetCursorPosition(130, 17);
                        Console.WriteLine(InputIncident);
                        Console.SetCursorPosition(53, 49);
                        Console.WriteLine(new string(' ', InputIncident.Length));
                        validEmergency = false;
                    }
                }
                catch (Exception ex)
                {
                    Console.SetCursorPosition(83, 54);
                    Console.WriteLine(ex.Message);
                    Console.SetCursorPosition(53, 49);
                    Console.WriteLine(new string(' ', Math.Max(InputName?.Length ?? 100, 100)));
                }
            }
            Console.SetCursorPosition(53, 19);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Enter Name: ");
            while (true)
            {
                try
                {
                    Console.ForegroundColor = consoleColor;
                    Console.SetCursorPosition(53, 49);
                    InputName = ErrorHandling.ReadLetterInput("");
                    AddName(InputName);
                    Console.SetCursorPosition(130, 21);
                    Console.WriteLine(InputName);

                    Console.SetCursorPosition(53, 49);
                    Console.WriteLine(new string(' ', InputName.Length));
                    Console.SetCursorPosition(83, 54);
                    Console.WriteLine(new string(' ', Console.WindowWidth - 83));
                    break;
                }
                catch (Exception ex)
                {
                    Console.SetCursorPosition(83, 54);
                    Console.WriteLine(ex.Message);
                    Console.SetCursorPosition(53, 49);
                    Console.WriteLine(new string(' ', Math.Max(InputName?.Length ?? 100, 100)));
                }
            }
            Console.SetCursorPosition(53, 23);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Kindly provide your current location so we can check the nearest responders: ");
            while (validLocation)
            {
                try
                {
                    Console.ForegroundColor = consoleColor;
                    Console.SetCursorPosition(53, 49);
                    InputLocation = Console.ReadLine(); 
                    if (locationChecker.DataReading(InputLocation) == 1 && InputLocation.ToLower() != "house" && InputLocation.ToLower() != "in my house" && InputLocation.ToLower() != "street")
                    {
                        Console.SetCursorPosition(83, 54);
                        Console.WriteLine(new string(' ', Console.WindowWidth - 83));
                        Console.ForegroundColor = ConsoleColor.Red;
                        throw new ArgumentException("Invalid Location. Please be specific");
                    }
                    else if (InputLocation.ToLower() == "house" || InputLocation.ToLower() == "in my house" || InputLocation.ToLower() == "street")
                    {
                        Console.SetCursorPosition(130, 25);
                        Console.WriteLine(InputLocation);
                        Console.SetCursorPosition(53, 49);
                        Console.WriteLine(new string(' ', Math.Max(InputLocation?.Length ?? 100, 100)));
                        Console.SetCursorPosition(83, 54);
                        Console.WriteLine(new string(' ', Console.WindowWidth - 83));
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.SetCursorPosition(63, 54);
                        Console.WriteLine("Kindly be specific with your location so we can send you the emergency units precisely.");
                    }
                    else
                    {
                        Console.SetCursorPosition(130, 25);
                        Console.WriteLine(new string(' ', InputLocation.Length+10));
                        validLocation = false;
                        AddLocation(InputLocation);
                        Console.SetCursorPosition(130, 25);
                        Console.WriteLine(InputLocation);
                        Console.SetCursorPosition(53, 49);
                        Console.WriteLine(new string(' ', InputLocation.Length));
                        Console.SetCursorPosition(83, 54);
                        Console.WriteLine(new string(' ', Console.WindowWidth - 83));
                    }
                }
                catch (Exception ex)
                {
                    Console.SetCursorPosition(83, 54);
                    Console.WriteLine(ex.Message);
                    Console.SetCursorPosition(53, 49);
                    Console.WriteLine(new string(' ', Math.Max(InputLocation?.Length ?? 100, 100)));
                }
            }
            Thread.Sleep(750);
            filepath = FilePath(InputIncident);
            SaveIncident(InputIncident, IncidentPosition, filepath);
            IncidentPosition++;
            Console.Clear();

            if (InputIncident.ToLower() == "fire")
            {
                Fire fire = new Fire();
                fire.AskQuestions(InputLocation);
            }
            else if (InputIncident.ToLower() == "medical")
            {
                Medical medical = new Medical();
                medical.AskQuestions(InputLocation);
            }
            else if (InputIncident.ToLower() == "crime")
            {
                Crime crime = new Crime();
                crime.AskQuestions(InputLocation);
            }
            else
                throw new ArgumentException("Invalid Input");
        }    
    }
    #region Fire
    internal class Fire : Information, IAsk
    {
        public void AskQuestions(string CurrentLocation)
        {
            EmergencyResponseSystem emergencyResponseSystem = new EmergencyResponseSystem();
            IncidentException incidentException = new IncidentException();
            string onFire, CurrentFilePath, callerInside, callerSafe, flames, smoke, fast;
            string CurrentIncident = "fire";
            bool validOnFire = true;
            MainMenu mainMenu = new MainMenu();
            Display display = new Display();

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.SetCursorPosition(0, 1);
            display.Header(CurrentIncident);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            display.Header("box");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(53, 15);
            Console.WriteLine("I will now ask a few questions to assist responders efficiently.\n");
            Thread.Sleep(700);
            Console.SetCursorPosition(53, 17);
            Console.WriteLine("What exactly is on fire? ");
            while (validOnFire)
            {
                try
                {
                    Console.ForegroundColor = consoleColor;
                    Console.SetCursorPosition(53, 49);
                    onFire = ErrorHandling.ReadLetterInput("");
                    if (onFire.ToLower() != "house" && onFire.ToLower() != "building" && onFire.ToLower() != "car")
                    {
                        mainMenu.ClearConsoleLine(54);
                        Console.ForegroundColor = ConsoleColor.Red;
                        throw new Exception("\t            Invalid Input.");
                    }
                    else
                    {
                        AddResponse(onFire);
                        Console.SetCursorPosition(130, 19);
                        Console.WriteLine(onFire);
                        Console.SetCursorPosition(53, 49);
                        Console.WriteLine(new string(' ', onFire.Length));
                        mainMenu.ClearConsoleLine(54);
                        validOnFire = false;
                    }
                }
                catch (Exception ex)
                {
                    Console.SetCursorPosition(83, 54);
                    Console.WriteLine(ex.Message);
                    Console.SetCursorPosition(53, 49);
                    Console.WriteLine(new string(' ', Math.Max(CurrentIncident?.Length ?? 100, 100)));
                }
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(53, 21);
            Console.WriteLine($"Are you inside the {Response[Response.Count - 1]}? ");
            while(true)
            {
                try
                {
                    Console.ForegroundColor = consoleColor;
                    Console.SetCursorPosition(53, 49);
                    callerInside = ErrorHandling.ReadYesNoInput("");
                    AddResponse(callerInside);
                    Console.SetCursorPosition(130, 23);
                    Console.WriteLine(callerInside);
                    Console.SetCursorPosition(53, 49);
                    Console.WriteLine(new string(' ', callerInside.Length));
                    mainMenu.ClearConsoleLine(54);
                    break;
                }
                catch (Exception ex)
                {
                    Console.SetCursorPosition(83, 54);
                    Console.WriteLine(ex.Message);
                    Console.SetCursorPosition(53, 49);
                    Console.WriteLine(new string(' ', Math.Max(CurrentIncident?.Length ?? 100, 100)));
                }
            }
            
            emergencyResponseSystem.AutomaticResponseSystem(CurrentIncident, CurrentLocation);
            if (callerInside.ToLower() == "yes")
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(53, 25);
                Console.WriteLine("Try to stay low to avoid smoke inhalation and please try to");
                Console.SetCursorPosition(53, 27);
                Console.WriteLine("reach the nearest exit without danger since the responders");
                Console.SetCursorPosition(53, 29);
                Console.WriteLine("are coming your way.");
                Console.SetCursorPosition(53, 33);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Thank you for your prompt response on the current fire situation in your area.");
                Console.SetCursorPosition(53, 35);
                mainMenu.PromptToContinue();
                Console.SetCursorPosition(53, 49);
                Console.WriteLine(new string(' ', CurrentIncident.Length));
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(53, 25);
                Console.WriteLine("Don't panic, the fire trucks are coming your way");
                Console.SetCursorPosition(53, 27);
                Console.WriteLine("please find a safe place for you and the other people.");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(53, 31);
                Console.WriteLine("Thank you for your prompt response on the current fire situation in your area.");
                Console.SetCursorPosition(53, 33);
                mainMenu.PromptToContinue();
                Console.SetCursorPosition(53, 49);
                Console.WriteLine(new string(' ', CurrentIncident.Length));
            }                  
            CurrentFilePath = FilePath(CurrentIncident);
            SaveResponses(CurrentIncident,CurrentFilePath);            
        }
    }
    #endregion

    #region Medical
    internal class Medical : Information, IAsk
    {
        private int medicalcode;
        public void AskQuestions(string CurrentLocation)
        {
            MainMenu mainMenu = new MainMenu();
            MedicalEmergency medicalEmergency1 = new MedicalEmergency();
            EmergencyResponseSystem emergencyResponseSystem = new EmergencyResponseSystem();
            IncidentException incidentException = new IncidentException();
            bool validEmergency = true;
            string CurrentIncident = "medical";
            string CurrentFilePath, isCallerPatient, conscious, breathing, cpr, isVomiting, isVomiting1, isFainting;
            bool validRelation = true;
            Display display = new Display();

            Console.SetCursorPosition(0, 3);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            display.Header(CurrentIncident);
            Console.ForegroundColor = ConsoleColor.Cyan;
            display.Header("box");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(53, 15);
            Console.WriteLine("I will now ask a few questions to assist responders efficiently.\n");
            Console.SetCursorPosition(53, 17);
            Thread.Sleep(700);
            Console.WriteLine("Are you the person experiencing the medical emergency?");
            while (true)
            {
                try
                {
                    Console.ForegroundColor = consoleColor;
                    Console.SetCursorPosition(53, 49);
                    isCallerPatient = ErrorHandling.ReadYesNoInput("");
                    AddResponse(isCallerPatient);
                    Console.SetCursorPosition(130, 19);
                    Console.WriteLine(isCallerPatient);
                    Console.SetCursorPosition(53, 49);
                    Console.WriteLine(new string(' ', isCallerPatient.Length));
                    mainMenu.ClearConsoleLine(54);
                    break;
                }
                catch (Exception ex)
                {
                    Console.SetCursorPosition(83, 54);
                    Console.WriteLine(ex.Message);
                    Console.SetCursorPosition(53, 49);
                    Console.WriteLine(new string(' ', Math.Max(CurrentIncident?.Length ?? 100, 100)));
                }
            }           

            if (isCallerPatient.ToLower() == "no")
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(53, 21);
                Console.WriteLine("What is your relation to the patient? ");
                while (validRelation)
                {
                    try
                    {
                        Console.ForegroundColor = consoleColor;
                        Console.SetCursorPosition(53, 49);
                        string relation = ErrorHandling.ReadLetterInput("");
                        if (incidentException.MedicalRelation(relation) == 1)
                        {
                            throw new Exception("Invalid Input. Please provide your relation to the patient");
                        }                       
                        AddResponse(relation);
                        Console.SetCursorPosition(130, 23);
                        Console.WriteLine(relation);
                        Console.SetCursorPosition(53, 49);
                        Console.WriteLine(new string(' ', relation.Length));
                        mainMenu.ClearConsoleLine(54);
                        validRelation = false;
                        
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.SetCursorPosition(83, 54);
                        Console.WriteLine(ex.Message);
                        Console.SetCursorPosition(53, 49);
                        Console.WriteLine(new string(' ', Math.Max(CurrentIncident?.Length ?? 100, 100)));
                    }
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(53, 25);
                Console.WriteLine("What's the medical emergency? ");
                while (validEmergency)
                {
                    try
                    {
                        Console.ForegroundColor = consoleColor;
                        Console.SetCursorPosition(53, 49);
                        string medicalemergency = Console.ReadLine();
                        AddResponse(medicalemergency);
                        medicalcode = medicalEmergency1.medicalEmergency(medicalemergency);
                        if (medicalcode >= 0)
                        {
                            Console.SetCursorPosition(130, 27);
                            Console.WriteLine(medicalemergency);
                            Console.SetCursorPosition(53, 49);
                            Console.WriteLine(new string(' ', medicalemergency.Length));
                            mainMenu.ClearConsoleLine(54);
                            validEmergency = false;
                        }
                        else
                        {
                            throw new Exception("Invalid Input");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.SetCursorPosition(83, 54);
                        Console.WriteLine(ex.Message);
                        Console.SetCursorPosition(53, 49);
                        Console.WriteLine(new string(' ', Math.Max(CurrentIncident?.Length ?? 100, 100)));
                    }
                }
                emergencyResponseSystem.AutomaticResponseSystem(CurrentIncident, CurrentLocation);
                if (medicalcode >= 1 && medicalcode <=9)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(53, 29);
                    Console.WriteLine("Is the patient conscious? ");
                    while (true)
                    {
                        try
                        {
                            Console.ForegroundColor = consoleColor;
                            Console.SetCursorPosition(53, 49);
                            conscious = ErrorHandling.ReadYesNoInput("");
                            AddResponse(conscious);
                            Console.SetCursorPosition(130, 31);
                            Console.WriteLine(conscious);
                            Console.SetCursorPosition(53, 49);
                            Console.WriteLine(new string(' ', conscious.Length));
                            break;
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            mainMenu.ClearConsoleLine(54);
                            mainMenu.ClearConsoleLine(55);
                            Console.SetCursorPosition(83, 54);
                            Console.WriteLine(ex.Message);
                            Console.SetCursorPosition(53, 49);
                            Console.WriteLine(new string(' ', Math.Max(CurrentIncident?.Length ?? 100, 100)));
                        }
                    }                                      

                    if (conscious.ToLower() == "no")
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.SetCursorPosition(53, 33);
                        Console.WriteLine("Can you detect any breathing? ");
                        while (true)
                        {
                            try
                            {
                                Console.ForegroundColor = consoleColor;
                                Console.SetCursorPosition(53, 49);
                                breathing = ErrorHandling.ReadYesNoInput("");
                                AddResponse(breathing);
                                Console.SetCursorPosition(130, 35);
                                Console.WriteLine(breathing);
                                Console.SetCursorPosition(53, 49);
                                Console.WriteLine(new string(' ', breathing.Length));
                                mainMenu.ClearConsoleLine(54);
                                break;
                            }
                            catch (Exception ex)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                mainMenu.ClearConsoleLine(54);
                                mainMenu.ClearConsoleLine(55);
                                Console.SetCursorPosition(83, 54);
                                Console.WriteLine(ex.Message);
                                Console.SetCursorPosition(53, 49);
                                Console.WriteLine(new string(' ', Math.Max(CurrentIncident?.Length ?? 100, 100)));
                            }
                        }
                      
                        if (breathing.ToLower() == "no")
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.SetCursorPosition(53, 37);
                            Console.WriteLine("Do you know how to administer CPR? ");
                            while (true)
                            {
                                try
                                {
                                    Console.ForegroundColor = consoleColor;
                                    Console.SetCursorPosition(53, 49);
                                    cpr = ErrorHandling.ReadYesNoInput("");
                                    AddResponse(cpr);
                                    Console.SetCursorPosition(130, 39);
                                    Console.WriteLine(cpr);
                                    Console.SetCursorPosition(53, 49);
                                    Console.WriteLine(new string(' ', cpr.Length));
                                    Thread.Sleep(750);
                                    Console.Clear();
                                    mainMenu.ClearConsoleLine(54);
                                    break;
                                }
                                catch (Exception ex)
                                {
                                    mainMenu.ClearConsoleLine(54);
                                    mainMenu.ClearConsoleLine(55);
                                    Console.SetCursorPosition(83, 54);
                                    Console.WriteLine(ex.Message);
                                    Console.SetCursorPosition(53, 49);
                                    Console.WriteLine(new string(' ', Math.Max(CurrentIncident?.Length ?? 100, 100)));
                                }
                            }                           

                            if(cpr.ToLower() == "yes")
                            {
                                Console.SetCursorPosition(0, 3);
                                Console.ForegroundColor = ConsoleColor.DarkCyan;
                                display.Header(CurrentIncident);
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                display.Header("box");
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.SetCursorPosition(53, 15);
                                Console.WriteLine("Kindly do the following:");
                                Console.SetCursorPosition(53, 17);
                                Console.WriteLine("1. Tilt their head back");
                                Console.SetCursorPosition(53, 19);
                                Console.WriteLine("2. Seal your mouth over their mouth and pinch their nose.");
                                Console.SetCursorPosition(53, 21);
                                Console.WriteLine("3. Blow five times into their mouth");
                                Console.SetCursorPosition(53, 23);
                                Console.WriteLine("4. Begin CPR");
                                Console.SetCursorPosition(53, 25);
                                Console.WriteLine("5. If the person is moving, coughing, or breathing, this is a good sign.");
                                Console.SetCursorPosition(53, 27);
                                Console.WriteLine("   If none of these things happen, continue giving CPR until emergency assistance arrives.");
                                Console.SetCursorPosition(53, 31);
                                Console.WriteLine("Thank you for your response regarding the medical situation.");
                                Console.SetCursorPosition(53, 33);
                                Console.ForegroundColor = ConsoleColor.Green;
                                mainMenu.PromptToContinue();
                            }
                            else
                            {
                                Console.SetCursorPosition(0, 3);
                                Console.ForegroundColor = ConsoleColor.DarkCyan;
                                display.Header(CurrentIncident);
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                display.Header("box");
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.SetCursorPosition(53, 15);
                                Console.WriteLine("Kindly do the following:");
                                Console.SetCursorPosition(53, 17);
                                Console.WriteLine("1. Tilt their head back");
                                Console.SetCursorPosition(53, 19);
                                Console.WriteLine("2. Seal your mouth over their mouth and pinch their nose.");
                                Console.SetCursorPosition(53, 21);
                                Console.WriteLine("3. Blow five times into their mouth");
                                Console.SetCursorPosition(53, 23);
                                Console.WriteLine("4. Kneel next to their shoulders, so your torso is over their chest.");
                                Console.SetCursorPosition(53, 25);
                                Console.WriteLine("5. Put the palm and heel of your hand in the center of their chest.");
                                Console.SetCursorPosition(53, 27);
                                Console.WriteLine("6. Place your other hand directly on top of the first hand and interlock your fingers.");
                                Console.SetCursorPosition(53, 29);
                                Console.WriteLine("7. Keeping your elbows straight, bring your shoulders forward over your hands to"); 
                                Console.SetCursorPosition(53, 31);
                                Console.WriteLine("   give you more upper body strength.");                         
                                Console.SetCursorPosition(53, 33);
                                Console.WriteLine("8. Using the weight and force of your upper body, push straight down on their chest. Press it down at least ");                               
                                Console.SetCursorPosition(53, 35);
                                Console.WriteLine("   2.0 to 2.4 inches, then release the pressure. This is one compression.");                              
                                Console.SetCursorPosition(53, 37);
                                Console.WriteLine("9. Do continual sets of 30 compressions at a rate of about two per second, or 100–120 compressions");
                                Console.SetCursorPosition(53, 39);
                                Console.WriteLine("   per minute.");
                                Console.SetCursorPosition(53, 42);
                                Console.WriteLine("Thank you for your response regarding the medical situation.");                      
                                Console.SetCursorPosition(53, 43);
                                Console.ForegroundColor = ConsoleColor.Green;
                                mainMenu.PromptToContinue();
                            }
                        }
                        else
                        {
                            Thread.Sleep(700);
                            Console.Clear();
                            Console.SetCursorPosition(0, 3);
                            Console.ForegroundColor = ConsoleColor.DarkCyan;
                            display.Header(CurrentIncident);
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            display.Header("box");
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.SetCursorPosition(53, 15);
                            Console.WriteLine("Place the patient in the recovery position. Kindly follow these steps:");
                            Console.SetCursorPosition(53, 17);
                            Console.WriteLine("1. Kneel beside the person.");
                            Console.SetCursorPosition(53, 19);
                            Console.WriteLine("2. Straighten their arms and legs.");
                            Console.SetCursorPosition(53, 21);
                            Console.WriteLine("3. Fold the arm closest to you over their chest.");
                            Console.SetCursorPosition(53, 23);
                            Console.WriteLine("4. Place the other arm at a right angle to their body.");
                            Console.SetCursorPosition(53, 25);
                            Console.WriteLine("5. Get the leg closest to you and bend the knee.");
                            Console.SetCursorPosition(53, 27);
                            Console.WriteLine("6. While supporting the person’s head and neck, gently take the bent knee");
                            Console.SetCursorPosition(53, 29);
                            Console.WriteLine("   closest to you and very gently roll the person away from you.");
                            Console.SetCursorPosition(53, 31);
                            Console.WriteLine("   Adjust the upper leg, so both the hip and knee are bent at right angles.");
                            Console.SetCursorPosition(53, 33);
                            Console.WriteLine("   Ensure the person is steady and cannot roll.");
                            Console.SetCursorPosition(53, 35);
                            Console.WriteLine("7. Tilt the head back and make sure the airways are clear and open.");
                            Console.SetCursorPosition(53, 37);
                            Console.WriteLine("Place them in recovery position while waiting for the ambulance to arrive. Don't worry help is on the way");
                            Console.SetCursorPosition(53, 41);
                            Console.WriteLine("Thank you for your response regarding the medical situation.");
                            Console.SetCursorPosition(53, 43);
                            Console.ForegroundColor = ConsoleColor.Green;
                            mainMenu.PromptToContinue();
                        }
                    }
                    else
                    {
                        Thread.Sleep(700);
                        Console.Clear();
                        Console.SetCursorPosition(0, 3);
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        display.Header(CurrentIncident);
                        Console.ForegroundColor = ConsoleColor.Cyan;
                        display.Header("box");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.SetCursorPosition(53, 15);
                        Console.WriteLine("Place the patient in the recovery position. Kindly follow these steps:");
                        Console.SetCursorPosition(53, 17);
                        Console.WriteLine("1. Kneel beside the person.");
                        Console.SetCursorPosition(53, 19);
                        Console.WriteLine("2. Straighten their arms and legs.");
                        Console.SetCursorPosition(53, 21);
                        Console.WriteLine("3. Fold the arm closest to you over their chest.");
                        Console.SetCursorPosition(53, 23);
                        Console.WriteLine("4. Place the other arm at a right angle to their body.");
                        Console.SetCursorPosition(53, 25);
                        Console.WriteLine("5. Get the leg closest to you and bend the knee.");
                        Console.SetCursorPosition(53, 27);
                        Console.WriteLine("6. While supporting the person’s head and neck, gently take the bent knee");
                        Console.SetCursorPosition(53, 29);
                        Console.WriteLine("   closest to you and very gently roll the person away from you.");
                        Console.SetCursorPosition(53, 31);
                        Console.WriteLine("   Adjust the upper leg, so both the hip and knee are bent at right angles.");
                        Console.SetCursorPosition(53, 33);
                        Console.WriteLine("   Ensure the person is steady and cannot roll.");
                        Console.SetCursorPosition(53, 35);
                        Console.WriteLine("7. Tilt the head back and make sure the airways are clear and open.");
                        Console.SetCursorPosition(53, 37);
                        Console.WriteLine("Place them in recovery position while waiting for the ambulance to arrive. Don't worry help is on the way");
                        Console.SetCursorPosition(53, 41);
                        Console.WriteLine("Thank you for your response regarding the medical situation.");
                        Console.SetCursorPosition(53, 43);
                        Console.ForegroundColor = ConsoleColor.Green;
                        mainMenu.PromptToContinue();
                    }
                }
                else if (medicalcode >= 10 && medicalcode <= 14)
                {
                    Thread.Sleep(700);
                    Console.Clear();
                    Console.SetCursorPosition(0, 3);
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    display.Header(CurrentIncident);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    display.Header("box");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(53, 15);
                    Console.WriteLine("Kindly do the following: ");
                    Console.SetCursorPosition(53, 17);
                    Console.WriteLine("1. Loosen any tight clothing of the patient");
                    Console.SetCursorPosition(53, 19);
                    Console.WriteLine("2. Do not give them any food or drink");
                    Console.SetCursorPosition(53, 21);
                    Console.WriteLine("3. Make the patient relax their neck and muscles");
                    Console.SetCursorPosition(53, 23);
                    Console.WriteLine("4. Let them slowly breathe in through their nose for two counts, keeping their mouth closed.");
                    Console.SetCursorPosition(53, 25);
                    Console.WriteLine("5. Let them purse their lips as if they're about to whistle.");
                    Console.SetCursorPosition(53, 27);
                    Console.WriteLine("6. Make them breathe out slowly and gently through their pursed lips to the count of four.");
                    Console.SetCursorPosition(53, 29);
                    Console.WriteLine("7. Do continual sets until the ambulance arrives. Don't worry help is on the way.");
                    Console.SetCursorPosition(53, 33);
                    Console.WriteLine("Thank you for your response regarding the medical situation.");
                    Console.SetCursorPosition(53, 35);
                    Console.ForegroundColor = ConsoleColor.Green;
                    mainMenu.PromptToContinue();
                }
                else if (medicalcode >= 15 && medicalcode <= 16)
                {
                    Thread.Sleep(700);
                    Console.Clear();
                    Console.SetCursorPosition(0, 3);
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    display.Header(CurrentIncident);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    display.Header("box");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(53, 15);
                    Console.WriteLine("Kindly do the following: ");
                    Console.SetCursorPosition(53, 17);
                    Console.WriteLine("1. Bend the person over");
                    Console.SetCursorPosition(53, 19);
                    Console.WriteLine("2. Stand behind the person.");
                    Console.SetCursorPosition(53, 21);
                    Console.WriteLine("3. Strike five separate times between the person's shoulder blades with the heel of your hand.");
                    Console.SetCursorPosition(53, 23);
                    Console.WriteLine("4. Make a fist with one hand and put it just above the person's belly button.");
                    Console.SetCursorPosition(53, 25);
                    Console.WriteLine("5. Grasp the fist with the other hand.");
                    Console.SetCursorPosition(53, 27);
                    Console.WriteLine("6. Press into the stomach with a quick, upward thrust — as if trying to lift the person up.");
                    Console.SetCursorPosition(53, 29);
                    Console.WriteLine("7. Give five abdominal thrusts");
                    Console.SetCursorPosition(53, 31);
                    Console.WriteLine("8. Repeat until the obstruction is relieved, but don't worry help is on the way");
                    Console.SetCursorPosition(53, 35);
                    Console.WriteLine("Thank you for your response regarding the medical situation.");
                    Console.SetCursorPosition(53, 37);
                    Console.ForegroundColor = ConsoleColor.Green;
                    mainMenu.PromptToContinue();
                }
                else if (medicalcode >= 17 && medicalcode <= 19)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(53, 29);
                    Console.WriteLine("Is the patient vomiting? ");
                    while (true)
                    {
                        try
                        {
                            Console.ForegroundColor = consoleColor;
                            Console.SetCursorPosition(53, 49);
                            isVomiting = ErrorHandling.ReadYesNoInput("");
                            AddResponse(isVomiting);
                            Console.SetCursorPosition(130, 31);
                            Console.WriteLine(isVomiting);
                            Console.SetCursorPosition(53, 49);
                            Console.WriteLine(new string(' ', isVomiting.Length));
                            mainMenu.ClearConsoleLine(54);
                            break;
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            mainMenu.ClearConsoleLine(54);
                            mainMenu.ClearConsoleLine(55);
                            Console.SetCursorPosition(83, 54);
                            Console.WriteLine(ex.Message);
                            Console.SetCursorPosition(53, 49);
                            Console.WriteLine(new string(' ', Math.Max(CurrentIncident?.Length ?? 100, 100)));
                        }
                    }                   
                    
                    if(isVomiting.ToLower() == "yes")
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.SetCursorPosition(53, 33);
                        Console.WriteLine("Kindly let the patient drink small sips of water, sport drink, or juice");
                        Console.SetCursorPosition(53, 35);
                        Console.WriteLine("The ambulance is coming your way!");
                        Console.SetCursorPosition(53, 39);
                        Console.WriteLine("Thank you for your response regarding the medical situation.");
                        Console.SetCursorPosition(53, 41);
                        Console.ForegroundColor = ConsoleColor.Green;
                        mainMenu.PromptToContinue();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.SetCursorPosition(53, 33);
                        Console.WriteLine("Kindly let the patient drink lots of water, sport drink, or juice.");
                        Console.SetCursorPosition(53, 35);
                        Console.WriteLine("The ambulance is coming your way!");
                        Console.SetCursorPosition(53, 39);
                        Console.WriteLine("Thank you for your response regarding the medical situation.");
                        Console.SetCursorPosition(53, 41);
                        Console.ForegroundColor = ConsoleColor.Green;
                        mainMenu.PromptToContinue();
                    }   
                }
                else if (medicalcode >= 20 && medicalcode <= 21)
                {
                    Thread.Sleep(750);
                    Console.Clear();
                    Console.SetCursorPosition(0, 3);
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    display.Header(CurrentIncident);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    display.Header("box");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(53, 15);
                    Console.WriteLine("Kindly give the patient oral medication but if the symptoms persists do the following:");
                    Console.SetCursorPosition(53, 17);
                    Console.WriteLine("1. Keep the patient calm.");
                    Console.SetCursorPosition(53, 19);
                    Console.WriteLine("2. Get the patient to lay flat on their back with their feet raised about a foot above the ground");
                    Console.SetCursorPosition(53, 21);
                    Console.WriteLine("3. Make sure the person’s clothing is loose, or remove constricting clothing.");
                    Console.SetCursorPosition(53, 23);
                    Console.WriteLine("4. Do not give them anything to drink or eat, even if they ask for it");
                    Console.SetCursorPosition(53, 25);
                    Console.WriteLine("Help is on the way! please stay calm");
                    Console.SetCursorPosition(53, 29);
                    Console.WriteLine("Thank you for your response regarding the medical situation.");
                    Console.SetCursorPosition(53, 31);
                    Console.ForegroundColor = ConsoleColor.Green;
                    mainMenu.PromptToContinue();
                }
                else if (medicalcode == 22)
                {
                    Thread.Sleep(750);
                    Console.Clear();
                    Console.SetCursorPosition(0, 3);
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    display.Header(CurrentIncident);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    display.Header("box");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(53, 15);
                    Console.WriteLine("Kindly do the following: ");
                    Console.SetCursorPosition(53, 17);
                    Console.WriteLine("1. Stop any bleeding. Apply pressure to the wound with a sterile bandage,");
                    Console.SetCursorPosition(53, 19);
                    Console.WriteLine("   a clean cloth or a clean piece of clothing.");
                    Console.SetCursorPosition(53, 21);
                    Console.WriteLine("2. Keep the injured area from moving. Don't try to realign the bone or push");
                    Console.SetCursorPosition(53, 23);
                    Console.WriteLine("   a bone that's sticking out back in.");
                    Console.SetCursorPosition(53, 25);
                    Console.WriteLine("3. Apply ice packs to limit swelling and help relieve pain.Don't apply ice");
                    Console.SetCursorPosition(53, 27);
                    Console.WriteLine("   directly to the skin. Wrap the ice in a towel, a piece of cloth or some other material.");
                    Console.SetCursorPosition(53, 29);
                    Console.WriteLine("Is the patient breathing in short and rapid manner? ");
                    while (true)
                    {
                        try
                        {
                            Console.ForegroundColor = consoleColor;
                            Console.SetCursorPosition(53, 49);
                            isFainting = ErrorHandling.ReadYesNoInput("");
                            AddResponse(isFainting);
                            Console.SetCursorPosition(130, 31);
                            Console.WriteLine(isFainting);
                            Console.SetCursorPosition(53, 49);
                            Console.WriteLine(new string(' ', isFainting.Length));
                            break;
                        }
                        catch (Exception ex)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            mainMenu.ClearConsoleLine(54);
                            mainMenu.ClearConsoleLine(55);
                            Console.SetCursorPosition(83, 54);
                            Console.WriteLine(ex.Message);
                            Console.SetCursorPosition(53, 49);
                            Console.WriteLine(new string(' ', Math.Max(CurrentIncident?.Length ?? 100, 100)));
                        }
                    }

                    if (isFainting.ToLower() == "yes")
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.SetCursorPosition(53, 33);
                        Console.WriteLine("Kindly lay the patient down with the head slightly lower than the trunk.");
                        Console.SetCursorPosition(53, 35);
                        Console.WriteLine("If you can, raise the legs.");
                        Console.SetCursorPosition(53, 37);
                        Console.WriteLine("Keep the patient in this position, until the ambulance arrives");
                        Console.SetCursorPosition(53, 41);
                        Console.WriteLine("Thank you for your response regarding the medical situation.");
                        Console.SetCursorPosition(53, 43);
                        Console.ForegroundColor = ConsoleColor.Green;
                        mainMenu.PromptToContinue();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.SetCursorPosition(53, 33);
                        Console.WriteLine("Good to know. Don't worry help is on the way.");
                        Console.SetCursorPosition(53, 37);
                        Console.WriteLine("Thank you for your response regarding the medical situation.");
                        Console.SetCursorPosition(53, 39);
                        Console.ForegroundColor = ConsoleColor.Green;
                        mainMenu.PromptToContinue();
                    }
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(53, 21);
                Console.WriteLine("What's your medical emergency? ");
                while (validEmergency)
                {
                    try
                    {
                        Console.ForegroundColor = consoleColor;
                        Console.SetCursorPosition(53, 49);
                        string medicalEmergency = Console.ReadLine();
                        AddResponse(medicalEmergency);
                        medicalcode = medicalEmergency1.medicalEmergency(medicalEmergency);
                        if (medicalcode >= 0)
                        {
                            Console.SetCursorPosition(130, 23);
                            Console.WriteLine(medicalEmergency);
                            Console.SetCursorPosition(53, 49);
                            Console.WriteLine(new string(' ', medicalEmergency.Length));
                            mainMenu.ClearConsoleLine(54);
                            validEmergency = false;
                        }
                        else
                        {
                            throw new Exception("Invalid Input");
                        }
                    }
                    catch (Exception ex)
                    {
                        mainMenu.ClearConsoleLine(54);
                        mainMenu.ClearConsoleLine(55);
                        Console.SetCursorPosition(83, 54);
                        Console.WriteLine(ex.Message);
                        Console.SetCursorPosition(53, 49);
                        Console.WriteLine(new string(' ', Math.Max(CurrentIncident?.Length ?? 100, 100)));
                    }
                }
                
                if(medicalcode >= 1 && medicalcode <= 9)
                {
                    Thread.Sleep(750);
                    Console.Clear();
                    Console.SetCursorPosition(0, 3);
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    display.Header(CurrentIncident);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    display.Header("box");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(53, 15);
                    Console.WriteLine("Kindly do the following:");
                    Console.SetCursorPosition(53, 17);
                    Console.WriteLine("1. Lie down on the floor.");
                    Console.SetCursorPosition(53, 19);
                    Console.WriteLine("2. Straighten your arms and legs.");
                    Console.SetCursorPosition(53, 21);
                    Console.WriteLine("3. Fold your arm over your chest.");
                    Console.SetCursorPosition(53, 23);
                    Console.WriteLine("4. Place your other arm at a right angle to your body.");
                    Console.SetCursorPosition(53, 25);
                    Console.WriteLine("5. Bend one of your knees.");
                    Console.SetCursorPosition(53, 27);
                    Console.WriteLine("6. Very gently roll to the side of your straighten leg.");
                    Console.SetCursorPosition(53, 29);
                    Console.WriteLine("   Adjust your upper leg, so both the hip and knee are bent at right angles.");
                    Console.SetCursorPosition(53, 31);
                    Console.WriteLine("7. Tilt your head back and make sure your airways are clear and open.");
                    Console.SetCursorPosition(53, 33);
                    Console.WriteLine("Please stay in your recovery position while waiting for the ambulance to arrive.");
                    Console.SetCursorPosition(53, 35);
                    Console.WriteLine("Don't worry help is on the way");
                    Console.SetCursorPosition(53, 39);
                    Console.WriteLine("Thank you for your response regarding the medical situation.");
                    Console.SetCursorPosition(53, 41);
                    Console.ForegroundColor = ConsoleColor.Green;
                    mainMenu.PromptToContinue();
                }
                else if(medicalcode >= 10 && medicalcode <= 14)
                {
                    Thread.Sleep(750);
                    Console.Clear();
                    Console.SetCursorPosition(0, 3);
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    display.Header(CurrentIncident);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    display.Header("box");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(53, 15);
                    Console.WriteLine("Kindly do the following: ");
                    Console.SetCursorPosition(53, 17);
                    Console.WriteLine("1. Loosen any tight clothing.");
                    Console.SetCursorPosition(53, 19);
                    Console.WriteLine("2. Do not give take any food or drink.");
                    Console.SetCursorPosition(53, 21);
                    Console.WriteLine("3. Keep your neck and musclese relaxed.");
                    Console.SetCursorPosition(53, 23);
                    Console.WriteLine("4. Slowly breathe in through your nose for two counts, while keeping your mouth closed.");
                    Console.SetCursorPosition(53, 25);
                    Console.WriteLine("5. Purse your lips as if you're about to whistle.");
                    Console.SetCursorPosition(53, 27);
                    Console.WriteLine("6. Breathe out slowly and gently through your pursed lips to the count of four.");
                    Console.SetCursorPosition(53, 29);
                    Console.WriteLine("7. Do continual sets until the ambulance arrives. Don't worry help is on the way.");
                    Console.SetCursorPosition(53, 33);
                    Console.WriteLine("Thank you for your response regarding the medical situation.");
                    Console.SetCursorPosition(53, 35);
                    Console.ForegroundColor = ConsoleColor.Green;
                    mainMenu.PromptToContinue();
                }
                else if(medicalcode >= 15 && medicalcode <= 16)
                {
                    Thread.Sleep(750);
                    Console.Clear();
                    Console.SetCursorPosition(0, 3);
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    display.Header(CurrentIncident);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    display.Header("box");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(53, 15);
                    Console.WriteLine("Kindly call someone for help and let them execute the Heimlich Maneuver or let them do the following:");
                    Console.SetCursorPosition(53, 17);
                    Console.WriteLine("1. Bend the person over");
                    Console.SetCursorPosition(53, 19);
                    Console.WriteLine("2. Stand behind the person.");
                    Console.SetCursorPosition(53, 21);
                    Console.WriteLine("3. Strike five separate times between the person's shoulder blades with the heel of your hand.");
                    Console.SetCursorPosition(53, 23);
                    Console.WriteLine("4. Make a fist with one hand and put it just above the person's belly button.");
                    Console.SetCursorPosition(53, 25);
                    Console.WriteLine("5. Grasp the fist with the other hand.");
                    Console.SetCursorPosition(53, 27);
                    Console.WriteLine("6. Press into the stomach with a quick, upward thrust — as if trying to lift the person up.");
                    Console.SetCursorPosition(53, 29);
                    Console.WriteLine("7. Give five abdominal thrusts");
                    Console.SetCursorPosition(53, 31);
                    Console.WriteLine("8. Repeat until the obstruction is relieved, but don't worry help is on the way");
                    Console.SetCursorPosition(53, 35);
                    Console.WriteLine("Thank you for your response regarding the medical situation.");
                    Console.SetCursorPosition(53, 37);
                    Console.ForegroundColor = ConsoleColor.Green;
                    mainMenu.PromptToContinue();
                }
                else if(medicalcode >= 17 && medicalcode <= 19)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(53, 25);
                    Console.WriteLine("Did you vomit or do you feel nauseous? ");
                    while (true)
                    {
                        try
                        {
                            Console.ForegroundColor = consoleColor;
                            Console.SetCursorPosition(53, 49);
                            isVomiting1 = ErrorHandling.ReadYesNoInput("");
                            Console.SetCursorPosition(130, 27);
                            Console.WriteLine(isVomiting1);
                            Console.SetCursorPosition(53, 49);
                            Console.WriteLine(new string(' ', isVomiting1.Length));
                            mainMenu.ClearConsoleLine(54);
                            AddResponse(isVomiting1);
                            break;
                        }
                        catch (Exception ex)
                        {
                            mainMenu.ClearConsoleLine(54);
                            mainMenu.ClearConsoleLine(55);
                            Console.SetCursorPosition(83, 54);
                            Console.WriteLine(ex.Message);
                            Console.SetCursorPosition(53, 49);
                            Console.WriteLine(new string(' ', Math.Max(CurrentIncident?.Length ?? 100, 100)));
                        }
                    }                    

                    if (isVomiting1.ToLower() == "yes")
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.SetCursorPosition(53, 29);
                        Console.WriteLine("Kindly drink small sips of water, sport drink, or juice");
                        Console.SetCursorPosition(53, 31);
                        Console.WriteLine("The ambulance is coming your way!");
                        Console.SetCursorPosition(53, 35);
                        Console.WriteLine("Thank you for your response regarding the medical situation.");
                        Console.SetCursorPosition(53, 37);
                        Console.ForegroundColor = ConsoleColor.Green;
                        mainMenu.PromptToContinue();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.SetCursorPosition(53, 29);
                        Console.WriteLine("Kindly drink lots of water, sport drink, or juice.");
                        Console.SetCursorPosition(53, 31);
                        Console.WriteLine("The ambulance is coming your way!");
                        Console.SetCursorPosition(53, 35);
                        Console.WriteLine("Thank you for your response regarding the medical situation.");
                        Console.SetCursorPosition(53, 37);
                        Console.ForegroundColor = ConsoleColor.Green;
                        mainMenu.PromptToContinue();
                    }
                }
                else if(medicalcode >= 20 && medicalcode <= 21)
                {
                    Thread.Sleep(750);
                    Console.Clear();
                    Console.SetCursorPosition(0, 3);
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    display.Header(CurrentIncident);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    display.Header("box");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(53, 15);
                    Console.WriteLine("Kindly take your prescibed oral medication but if the symptoms persists do the following:");
                    Console.SetCursorPosition(53, 17);
                    Console.WriteLine("1. Stay calm.");
                    Console.SetCursorPosition(53, 19);
                    Console.WriteLine("2. Lay flat on your back with your feet raised about a foot above the ground");
                    Console.SetCursorPosition(53, 21);
                    Console.WriteLine("3. Make sure youur clothing is loose, or remove constricting clothing.");
                    Console.SetCursorPosition(53, 23);
                    Console.WriteLine("4. Do not drink or eat anything.");
                    Console.SetCursorPosition(53, 25);
                    Console.WriteLine("Help is on the way! Please stay calm");
                    Console.SetCursorPosition(53, 29);
                    Console.WriteLine("Thank you for your response regarding the medical situation.");
                    Console.SetCursorPosition(53, 31);
                    Console.ForegroundColor = ConsoleColor.Green;
                    mainMenu.PromptToContinue();
                }
                else if(medicalcode >= 22)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(53, 25);
                    Console.WriteLine("Kindly refrain from moving as this may worsen your situation");
                    Console.SetCursorPosition(53, 27);
                    Console.WriteLine("Just stay calm. Don't worry help is on the way!");
                    Console.SetCursorPosition(53, 31);
                    Console.WriteLine("Thank you for your response regarding the medical situation.");
                    Console.SetCursorPosition(53, 33);
                    Console.ForegroundColor = ConsoleColor.Green;
                    mainMenu.PromptToContinue();
                }
            }
            CurrentFilePath = FilePath(CurrentIncident);
            SaveResponses(CurrentIncident, CurrentFilePath);
        }
    }
    #endregion

    #region Crime
    internal class Crime : Information, IAsk
    {
        private int crimecode;
        public void AskQuestions(string CurrentLocation)
        {
            MainMenu mainMenu = new MainMenu();
            CrimeEmergency crimeEmergency1 = new CrimeEmergency();
            EmergencyResponseSystem emergencyResponseSystem = new EmergencyResponseSystem();
            bool validEmergency1 = true;
            string CurrentIncident = "crime";
            string CurrentFilePath, injuries, suspectPresent, medicalAssistance, isSecure, incidentDetails, intruderPresent, safeLocation, isSafeLocation, othersPresent;
            string activityOngoing, immediateDanger, immediateDanger1, witnessAvailable, recentContact, heldAgainstWill, reportedToBank, infoAvailable, unauthorizedTransactions;
            string disturbanceLocation, people;
            Display display = new Display();

            Console.SetCursorPosition(0, 3);
            Console.ForegroundColor = ConsoleColor.Cyan;
            display.Header(CurrentIncident);
            Console.ForegroundColor = ConsoleColor.Yellow;
            display.Header("box");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(53, 15);
            Console.WriteLine("I will now ask a few questions to assist responders efficiently.");
            Console.SetCursorPosition(53, 49);
            Thread.Sleep(750);
            Console.SetCursorPosition(53, 17);
            Console.WriteLine("What's the specic crime emergency? ");
            while (validEmergency1)
            {
                try
                {
                    Console.ForegroundColor = consoleColor;
                    Console.SetCursorPosition(53, 49);
                    string crimeEmergency2 = Console.ReadLine();
                    AddResponse(crimeEmergency2);
                    crimecode = crimeEmergency1.crimeEmergency(crimeEmergency2);
                    
                    if (crimecode >= 0)
                    {
                        Console.SetCursorPosition(130, 19);
                        Console.WriteLine(crimeEmergency2);
                        Console.SetCursorPosition(53, 49);
                        Console.WriteLine(new string(' ', crimeEmergency2.Length));
                        validEmergency1 = false;
                    }
                    else
                    {
                        throw new Exception("Invalid Input");
                    }
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.SetCursorPosition(90, 54);
                    Console.WriteLine(ex.Message);
                    Console.SetCursorPosition(53, 49);
                    Console.WriteLine(new string(' ', Math.Max(CurrentIncident?.Length ?? 100, 100)));
                }
            }
            emergencyResponseSystem.AutomaticResponseSystem(CurrentIncident, CurrentLocation);
            if (crimecode >= 1 && crimecode <= 3) // Assault, Battery, Domestic Violence
            { 
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(53, 21);
                Console.WriteLine("Are there any injuries requiring immediate attention? ");
                while (true)
                {
                    try
                    {
                        Console.ForegroundColor = consoleColor;
                        Console.SetCursorPosition(53, 49);
                        injuries = ErrorHandling.ReadYesNoInput("");
                        AddResponse(injuries);
                        Console.SetCursorPosition(130, 23);
                        Console.WriteLine(injuries);
                        break;
                    }
                    catch (Exception ex)
                    {
                        mainMenu.ClearConsoleLine(54);
                        mainMenu.ClearConsoleLine(55);
                        Console.SetCursorPosition(83, 54);
                        Console.WriteLine(ex.Message);
                        Console.SetCursorPosition(53, 49);
                        Console.WriteLine(new string(' ', Math.Max(CurrentIncident?.Length ?? 100, 100)));
                    }
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(53, 25);
                Console.WriteLine("Try to stay calm. If possible, ask for help from others nearby.");

                if (injuries.ToLower() == "yes")
                {
                    Console.SetCursorPosition(53, 27);
                    Console.WriteLine("Medical assistance is on the way.");
                }
                else
                {
                    Console.SetCursorPosition(53, 27);
                    Console.WriteLine("Good to know.");
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(53, 29);
                Console.WriteLine("Is the suspect still on the scene? ");
                while (true)
                {
                    try
                    {
                        Console.ForegroundColor = consoleColor;
                        Console.SetCursorPosition(53, 49);
                        suspectPresent = ErrorHandling.ReadYesNoInput("");
                        AddResponse(suspectPresent);
                        Console.SetCursorPosition(130, 31);
                        Console.WriteLine(injuries);
                        Console.SetCursorPosition(53, 49);
                        Console.WriteLine(new string(' ', suspectPresent.Length));
                        break;
                    }
                    catch (Exception ex)
                    {
                        mainMenu.ClearConsoleLine(54);
                        mainMenu.ClearConsoleLine(55);
                        Console.SetCursorPosition(83, 54);
                        Console.WriteLine(ex.Message);
                        Console.SetCursorPosition(53, 49);
                        Console.WriteLine(new string(' ', Math.Max(CurrentIncident?.Length ?? 100, 100)));
                    }
                }
                
                if (suspectPresent.ToLower() == "yes")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(53, 33);
                    Console.WriteLine("Please move to a safe location and avoid confrontation.");
                    Console.SetCursorPosition(53, 35);
                    Console.WriteLine("Do not panic, police officers are on the way.");
                    Console.SetCursorPosition(53, 39);
                    Console.WriteLine("Thank you for providing information about the crime.");
                    Console.SetCursorPosition(53, 41);
                    Console.ForegroundColor = ConsoleColor.Green;
                    mainMenu.PromptToContinue();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(53, 33);
                    Console.WriteLine("Okay, please stay on your current location. Police officers are on the way");
                    Console.SetCursorPosition(53, 37);
                    Console.WriteLine("Thank you for providing information about the crime.");
                    Console.SetCursorPosition(53, 39);
                    Console.ForegroundColor = ConsoleColor.Green;
                    mainMenu.PromptToContinue();
                }
            }
            else if (crimecode >= 4 && crimecode <= 5) // Sexual Assault, Rape
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(53, 21);
                Console.WriteLine("Do you need medical assistance immediately? ");
                while (true)
                {
                    try
                    {
                        Console.ForegroundColor = consoleColor;
                        Console.SetCursorPosition(53, 49);
                        medicalAssistance = ErrorHandling.ReadYesNoInput("");
                        AddResponse(medicalAssistance);
                        Console.SetCursorPosition(130, 23);
                        Console.WriteLine(medicalAssistance);
                        Console.SetCursorPosition(53, 49);
                        Console.WriteLine(new string(' ', medicalAssistance.Length));
                        break;
                    }
                    catch (Exception ex)
                    {
                        mainMenu.ClearConsoleLine(54);
                        mainMenu.ClearConsoleLine(55);
                        Console.SetCursorPosition(83, 54);
                        Console.WriteLine(ex.Message);
                        Console.SetCursorPosition(53, 49);
                        Console.WriteLine(new string(' ', Math.Max(CurrentIncident?.Length ?? 100, 100)));
                    }
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(53, 25);
                Console.WriteLine("Help is on its way. Take deep breaths and try to stay calm.");

                if (medicalAssistance.ToLower() == "yes")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(53, 27);
                    Console.WriteLine("Please remain where you are. Medical units are on the way.");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(53, 29);
                    Console.WriteLine("Okay that's noted.");
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(53, 31);               
                Console.WriteLine("If possible, kindly find a safe place to wait for assistance.");
                Console.SetCursorPosition(53, 35);
                Console.WriteLine("Thank you for providing information about the crime.");
                Console.SetCursorPosition(53, 37);
                Console.ForegroundColor = ConsoleColor.Green;
                mainMenu.PromptToContinue();               
            }
            else if (crimecode >= 6 && crimecode <= 9) // Murder, Homicide, Gunshots
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(53, 21);
                Console.WriteLine("Stay as calm as possible. If you are in immediate danger,");
                Console.SetCursorPosition(53, 23);
                Console.WriteLine("try to find a safe place where you can hide or distance yourself from the scene.");
                Console.SetCursorPosition(53, 25);
                Console.WriteLine("Don't worry the police officers and the medical units are coming your way");
                Console.SetCursorPosition(53, 27);             
                Console.WriteLine("Find any cover or hidden area to protect yourself.");
                Console.SetCursorPosition(53, 29);
                Console.WriteLine("Can you provide any specific details about what occurred? ");
                while (true)
                {
                    try
                    {
                        Console.ForegroundColor = consoleColor;
                        Console.SetCursorPosition(53, 49);
                        incidentDetails = ErrorHandling.ReadYesNoInput("");
                        AddResponse(incidentDetails);
                        Console.SetCursorPosition(130, 31);
                        Console.WriteLine(incidentDetails);
                        Console.SetCursorPosition(53, 49);
                        Console.WriteLine(new string(' ', incidentDetails.Length));
                        break;
                    }
                    catch (Exception ex)
                    {
                        mainMenu.ClearConsoleLine(54);
                        mainMenu.ClearConsoleLine(55);
                        Console.SetCursorPosition(83, 54);
                        Console.WriteLine(ex.Message);
                        Console.SetCursorPosition(53, 49);
                        Console.WriteLine(new string(' ', Math.Max(CurrentIncident?.Length ?? 100, 100)));
                    }
                }               

                if (incidentDetails.ToLower() != "yes")
                {
                    Thread.Sleep(750);
                    Console.Clear();
                    Console.SetCursorPosition(0, 3);
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    display.Header(CurrentIncident);
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    display.Header("box");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(53, 15);
                    Console.WriteLine("Okay! Please cooperate and tell that to the police officers once the situation have calm down.");
                    Console.SetCursorPosition(53, 17);
                    Console.WriteLine("Remember, try not to touch or disturb anything at the scene.");
                    Console.SetCursorPosition(53, 19);
                    Console.WriteLine("Authorities are on their way and will arrive as soon as possible.");
                    Console.SetCursorPosition(53, 21);
                    Console.WriteLine("Okay, please stay low and avoid confrontation. Help is on the way");
                    Console.SetCursorPosition(53, 25);
                    Console.WriteLine("Thank you for providing information about the crime.");
                    Console.SetCursorPosition(53, 27);
                    Console.ForegroundColor = ConsoleColor.Green;
                    mainMenu.PromptToContinue();
                }
                else
                {
                    Console.SetCursorPosition(53, 33);
                    Console.WriteLine("That's okay, please stay low and avoid confrontation. Help is on the way");
                    Console.SetCursorPosition(53, 37);
                    Console.WriteLine("Thank you for providing information about the crime.");
                    Console.SetCursorPosition(53, 39);
                    Console.ForegroundColor = ConsoleColor.Green;
                    mainMenu.PromptToContinue();
                }

            }
            else if (crimecode >= 10 && crimecode <= 13) // Theft, Burglary, Robbery, Arson
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(53, 21);
                Console.WriteLine("Is the intruder still in the area? ");
                while (true)
                {
                    try
                    {
                        Console.ForegroundColor = consoleColor;
                        Console.SetCursorPosition(53, 49);
                        intruderPresent = ErrorHandling.ReadYesNoInput("");
                        AddResponse(intruderPresent);
                        Console.SetCursorPosition(130, 23);
                        Console.WriteLine(intruderPresent);
                        Console.SetCursorPosition(53, 49);
                        Console.WriteLine(new string(' ', intruderPresent.Length));
                        break;
                    }
                    catch (Exception ex)
                    {
                        mainMenu.ClearConsoleLine(54);
                        mainMenu.ClearConsoleLine(55);
                        Console.SetCursorPosition(83, 54);
                        Console.WriteLine(ex.Message);
                        Console.SetCursorPosition(53, 49);
                        Console.WriteLine(new string(' ', Math.Max(CurrentIncident?.Length ?? 100, 100)));
                    }
                }

                if (intruderPresent.ToLower() == "yes")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(53, 25);
                    Console.WriteLine("For your safety, try to stay out of sight and avoid confrontation. Help is on the way.");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(53, 25);
                    Console.WriteLine("Okay, please remain calm, help is on the way.");
                }
                    
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(53, 27);
                Console.WriteLine("Is there anyone else with you who might need medical assistance? ");
                while (true)
                {
                    try
                    {
                        Console.ForegroundColor = consoleColor;
                        Console.SetCursorPosition(53, 49);
                        othersPresent = ErrorHandling.ReadYesNoInput("");
                        AddResponse(othersPresent);
                        Console.SetCursorPosition(130, 29);
                        Console.WriteLine(intruderPresent);
                        Console.SetCursorPosition(53, 49);
                        Console.WriteLine(new string(' ', othersPresent.Length));
                        break;
                    }
                    catch (Exception ex)
                    {
                        mainMenu.ClearConsoleLine(54);
                        mainMenu.ClearConsoleLine(55);
                        Console.SetCursorPosition(83, 54);
                        Console.WriteLine(ex.Message);
                        Console.SetCursorPosition(53, 49);
                        Console.WriteLine(new string(' ', Math.Max(CurrentIncident?.Length ?? 100, 100)));
                    }
                }

                if (othersPresent.ToLower() == "yes")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(53, 31);
                    Console.WriteLine("Ensure they are also safe and ask them to remain calm. ");
                    Console.SetCursorPosition(53, 33);
                    Console.WriteLine("Medical unit is on the way. Do not panic.");
                    Console.SetCursorPosition(53, 35);
                    Console.WriteLine("Stay put, and avoid touching anything to preserve evidence."); 
                    Console.SetCursorPosition(53, 39);
                    Console.WriteLine("Thank you for providing information about the crime.");
                    Console.SetCursorPosition(53, 41);
                    Console.ForegroundColor = ConsoleColor.Green;
                    mainMenu.PromptToContinue();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(53, 31);
                    Console.WriteLine("Do not panic. Stay put, and avoid touching anything to preserve evidence. Assistance will arrive shortly.");
                    Console.SetCursorPosition(53, 35);
                    Console.WriteLine("Thank you for providing information about the crime.");
                    Console.SetCursorPosition(53, 37);
                    Console.ForegroundColor = ConsoleColor.Green;
                    mainMenu.PromptToContinue();
                }       
            }
            else if (crimecode >= 14 && crimecode <= 16) // Illegal Drug Trade, Drugs, Illegal Drugs
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(53, 21);
                Console.WriteLine("Is the illegal activity still ongoing? ");
                while (true)
                {
                    try
                    {
                        Console.ForegroundColor = consoleColor;
                        Console.SetCursorPosition(53, 49);
                        activityOngoing = ErrorHandling.ReadYesNoInput("");
                        AddResponse(activityOngoing);
                        Console.SetCursorPosition(130, 23);
                        Console.WriteLine(activityOngoing);
                        Console.SetCursorPosition(53, 49);
                        Console.WriteLine(new string(' ', activityOngoing.Length));
                        break;
                    }
                    catch (Exception ex)
                    {
                        mainMenu.ClearConsoleLine(54);
                        mainMenu.ClearConsoleLine(55);
                        Console.SetCursorPosition(83, 54);
                        Console.WriteLine(ex.Message);
                        Console.SetCursorPosition(53, 49);
                        Console.WriteLine(new string(' ', Math.Max(CurrentIncident?.Length ?? 100, 100)));
                    }
                }               

                if (activityOngoing.ToLower() == "yes")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(53, 25);
                    Console.WriteLine("Avoid any interaction with those involved. Stay safe and police officers will be there soon.");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(53, 27);
                    Console.WriteLine("Is there any immediate danger to you or others? ");
                    while (true)
                    {
                        try
                        {
                            immediateDanger = ErrorHandling.ReadYesNoInput("");
                            AddResponse(immediateDanger);
                            Console.SetCursorPosition(130, 29);
                            Console.WriteLine(activityOngoing);
                            Console.SetCursorPosition(53, 49);
                            Console.WriteLine(new string(' ', activityOngoing.Length));
                            break;
                        }
                        catch (Exception ex)
                        {
                            mainMenu.ClearConsoleLine(54);
                            mainMenu.ClearConsoleLine(55);
                            Console.SetCursorPosition(83, 54);
                            Console.WriteLine(ex.Message);
                            Console.SetCursorPosition(53, 49);
                            Console.WriteLine(new string(' ', Math.Max(CurrentIncident?.Length ?? 100, 100)));
                        }
                    }

                    if (immediateDanger.ToLower() == "yes")
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.SetCursorPosition(53, 31);
                        Console.WriteLine("For your safety, try to stay out of sight and avoid confrontation. Help is on the way.");
                        Console.SetCursorPosition(53, 35);
                        Console.WriteLine("Thank you for providing information about the crime.");
                        Console.SetCursorPosition(53, 37);
                        Console.ForegroundColor = ConsoleColor.Green;
                        mainMenu.PromptToContinue();
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.SetCursorPosition(53, 31);
                        Console.WriteLine("Good to know please do not attract their attention and do unnecessary actions. Police officers will arrive shortly.");
                        Console.SetCursorPosition(53, 35);
                        Console.WriteLine("Thank you for providing information about the crime.");
                        Console.SetCursorPosition(53, 37);
                        Console.ForegroundColor = ConsoleColor.Green;
                        mainMenu.PromptToContinue();
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(53, 27);
                    Console.WriteLine("Thank you for the information. Officers are in route to investigate and ensure safety.");
                    Console.SetCursorPosition(53, 29);
                    Console.ForegroundColor = ConsoleColor.Green;
                    mainMenu.PromptToContinue();
                }              
            }
            else if (crimecode >= 17 && crimecode <= 19) // Kidnapping, Abduction, Missing Person
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(53, 21);
                Console.WriteLine("Can you describe the last known location of the missing person? ");
                Console.ForegroundColor = consoleColor;
                Console.SetCursorPosition(53, 49);
                string lastLocation = Console.ReadLine();
                AddResponse(lastLocation);
                Console.SetCursorPosition(130, 23);
                Console.WriteLine(lastLocation);
                Console.SetCursorPosition(53, 49);
                Console.WriteLine(new string(' ', lastLocation.Length));
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(53, 25);
                Console.WriteLine("Okay that's noted, has the missing person been in contact with you or anyone else recently? ");
                while (true)
                {
                    try
                    {
                        Console.SetCursorPosition(53, 49);
                        Console.ForegroundColor = consoleColor;
                        recentContact = ErrorHandling.ReadYesNoInput("");
                        AddResponse(recentContact);
                        Console.SetCursorPosition(130, 27);
                        Console.WriteLine(recentContact);
                        Console.SetCursorPosition(53, 49);
                        Console.WriteLine(new string(' ', recentContact.Length));
                        break;
                    }
                    catch (Exception ex)
                    {
                        mainMenu.ClearConsoleLine(54);
                        mainMenu.ClearConsoleLine(55);
                        Console.SetCursorPosition(83, 54);
                        Console.WriteLine(ex.Message);
                        Console.SetCursorPosition(53, 49);
                        Console.WriteLine(new string(' ', Math.Max(CurrentIncident?.Length ?? 100, 100)));
                    }
                }
                if (recentContact.ToLower() == "yes")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(53, 29);
                    Console.WriteLine("Kindly provide the details on the police officers for further assistance. ");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(53, 29);
                    Console.WriteLine("Okay, please try to check his whereabouts and report them to the officer that's coming your way.");
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(53, 31);
                Console.WriteLine("Avoid sharing information publicly until help arrives.");
                Console.SetCursorPosition(53, 35);
                Console.WriteLine("Thank you for the information. Officers are en route to investigate and ensure safety.");
                Console.SetCursorPosition(53, 37);
                Console.ForegroundColor = ConsoleColor.Green;
                mainMenu.PromptToContinue();
            }
            else if (crimecode == 20) // Fraud
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(53, 21);
                Console.WriteLine("Have you reported this incident to your bank or the relevant financial institution? ");
                while (true)
                {
                    try
                    {
                        Console.SetCursorPosition(53, 49);
                        Console.ForegroundColor = consoleColor;
                        reportedToBank = ErrorHandling.ReadYesNoInput("");
                        AddResponse(reportedToBank);
                        Console.SetCursorPosition(130, 23);
                        Console.WriteLine(reportedToBank);
                        Console.SetCursorPosition(53, 49);
                        Console.WriteLine(new string(' ', reportedToBank.Length));
                        break;
                    }
                    catch (Exception ex)
                    {
                        mainMenu.ClearConsoleLine(54);
                        mainMenu.ClearConsoleLine(55);
                        Console.SetCursorPosition(83, 54);
                        Console.WriteLine(ex.Message);
                        Console.SetCursorPosition(53, 49);
                        Console.WriteLine(new string(' ', Math.Max(CurrentIncident?.Length ?? 100, 100)));
                    }
                }                

                if (reportedToBank.ToLower() == "no")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(53, 25);
                    Console.WriteLine("You have to report this immediately to the bank in order for them to freeze your account and prevent further transactions.");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(53, 25);
                    Console.WriteLine("That's good.");
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(53, 27);
                Console.WriteLine("Do you have any information about the person or organization involved in this fraud? ");
                while (true)
                {
                    try
                    {
                        Console.SetCursorPosition(53, 49);
                        Console.ForegroundColor = consoleColor;
                        infoAvailable = ErrorHandling.ReadYesNoInput("");
                        AddResponse(infoAvailable);
                        Console.SetCursorPosition(130, 29);
                        Console.WriteLine(infoAvailable);
                        Console.SetCursorPosition(53, 49);
                        Console.WriteLine(new string(' ', infoAvailable.Length));
                        break;
                    }
                    catch (Exception ex)
                    {
                        mainMenu.ClearConsoleLine(54);
                        mainMenu.ClearConsoleLine(55);
                        Console.SetCursorPosition(83, 54);
                        Console.WriteLine(ex.Message);
                        Console.SetCursorPosition(53, 49);
                        Console.WriteLine(new string(' ', Math.Max(CurrentIncident?.Length ?? 100, 100)));
                    }
                }                

                if (infoAvailable.ToLower() == "yes")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(53, 31);
                    Console.WriteLine("Please share any details you have, such as names, phone numbers, etc. on the police officer that's coming your way.");
                    Console.SetCursorPosition(53, 33);
                    Console.WriteLine("Such information would be very helpful in your current case.");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(53, 31);
                    Console.WriteLine("It's okay please try to check your recent online activity");
                    Console.SetCursorPosition(53, 33);
                    Console.WriteLine("Kindly report to the police officers once you've found something suspicious");
                }
                Thread.Sleep(6000);
                Console.Clear();
                Console.SetCursorPosition(0, 3);
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                display.Header(CurrentIncident);
                Console.ForegroundColor = ConsoleColor.Cyan;
                display.Header("box");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(53, 15);
                Console.WriteLine("Have you experienced any unauthorized transactions on your accounts? ");
                while (true)
                {
                    try
                    {
                        Console.SetCursorPosition(53, 49);
                        Console.ForegroundColor = consoleColor;
                        unauthorizedTransactions = ErrorHandling.ReadYesNoInput("");
                        AddResponse(unauthorizedTransactions);
                        Console.SetCursorPosition(130, 17);
                        Console.WriteLine(unauthorizedTransactions);
                        Console.SetCursorPosition(53, 49);
                        Console.WriteLine(new string(' ', unauthorizedTransactions.Length));
                        break;
                    }
                    catch (Exception ex)
                    {
                        mainMenu.ClearConsoleLine(54);
                        mainMenu.ClearConsoleLine(55);
                        Console.SetCursorPosition(83, 54);
                        Console.WriteLine(ex.Message);
                        Console.SetCursorPosition(53, 49);
                        Console.WriteLine(new string(' ', Math.Max(CurrentIncident?.Length ?? 100, 100)));
                    }
                }

                if (unauthorizedTransactions.ToLower() == "yes")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(53, 19);
                    Console.WriteLine("Keep a record of all unauthorized transactions,");
                    Console.SetCursorPosition(53, 21);
                    Console.WriteLine("including dates and amounts, to assist with your report.");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(53, 19);
                    Console.WriteLine("That's good to hear, however please do not be complacent"); 
                    Console.SetCursorPosition(53, 21);
                    Console.WriteLine("and keep track of your account");
                }
                Console.SetCursorPosition(53, 23);
                Console.WriteLine("Stay vigilant and monitor your accounts for any further suspicious activity.");
                Console.SetCursorPosition(53, 25);
                Console.WriteLine("It’s crucial to act quickly to safeguard your information.");
                Console.SetCursorPosition(53, 29);
                Console.WriteLine("Thank you for the information. Officers are in route to investigate and ensure safety.");
                Console.SetCursorPosition(53, 31);
                Console.ForegroundColor = ConsoleColor.Green;
                mainMenu.PromptToContinue();

            }
            else if (crimecode == 21) //disturbance
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(53, 21);
                Console.WriteLine("Kindly describe the nature of the disturbance? ");
                Console.SetCursorPosition(53, 22);
                Console.WriteLine("e.g., loud noises, aggressive behavior, ongoing fights)");
                Console.ForegroundColor = consoleColor;
                Console.SetCursorPosition(53, 49);
                string disturbanceType = Console.ReadLine();
                AddResponse(disturbanceType);
                Console.SetCursorPosition(130, 24);
                Console.WriteLine(disturbanceType);
                Console.SetCursorPosition(53, 49);
                Console.WriteLine(new string(' ', disturbanceType.Length));

                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(53, 26);
                Console.WriteLine("Is this disturbance occurring nearby? ");
                while (true)
                {
                    try
                    {
                        Console.ForegroundColor = consoleColor;
                        Console.SetCursorPosition(53, 49);
                        disturbanceLocation = ErrorHandling.ReadYesNoInput("");
                        AddResponse(disturbanceLocation);
                        Console.SetCursorPosition(130, 28);
                        Console.WriteLine(disturbanceLocation);
                        Console.SetCursorPosition(53, 49);
                        Console.WriteLine(new string(' ', disturbanceLocation.Length));
                        break;
                    }
                    catch (Exception ex)
                    {
                        mainMenu.ClearConsoleLine(54);
                        mainMenu.ClearConsoleLine(55);
                        Console.SetCursorPosition(83, 54);
                        Console.WriteLine(ex.Message);
                        Console.SetCursorPosition(53, 49);
                        Console.WriteLine(new string(' ', Math.Max(CurrentIncident?.Length ?? 100, 100)));
                    }
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(53, 30);
                Console.WriteLine("How long has the disturbance been occurring? Please provide an estimate in minutes.");
                Console.SetCursorPosition(53, 49);
                Console.ForegroundColor = consoleColor;
                string disturbanceDuration = Console.ReadLine();
                AddResponse(disturbanceDuration);
                Console.SetCursorPosition(130, 32);
                Console.WriteLine(disturbanceLocation);
                Console.SetCursorPosition(53, 49);
                Console.WriteLine(new string(' ', disturbanceDuration.Length));

                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(53, 34);
                Console.WriteLine("Are there any people involved that you can see? ");
                while (true)
                {
                    try
                    {
                        Console.SetCursorPosition(53, 49);
                        Console.ForegroundColor = consoleColor;
                        people = ErrorHandling.ReadYesNoInput("");
                        AddResponse(people);
                        Console.SetCursorPosition(130, 34);
                        Console.WriteLine(disturbanceLocation);
                        Console.SetCursorPosition(53, 49);
                        Console.WriteLine(new string(' ', disturbanceLocation.Length));
                        break;
                    }
                    catch (Exception ex)
                    {
                        mainMenu.ClearConsoleLine(54);
                        mainMenu.ClearConsoleLine(55);
                        Console.SetCursorPosition(83, 54);
                        Console.WriteLine(ex.Message);
                        Console.SetCursorPosition(53, 49);
                        Console.WriteLine(new string(' ', Math.Max(CurrentIncident?.Length ?? 100, 100)));
                    }
                }

                if(people.ToLower() == "yes")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(53, 36);
                    Console.WriteLine("How many people can you see? (approximate number will do)");
                    Console.SetCursorPosition(53, 49);
                    string number = Console.ReadLine();
                    Console.ForegroundColor = consoleColor;
                    AddResponse(number);
                    Console.SetCursorPosition(130, 38);
                    Console.WriteLine(number);
                    Console.SetCursorPosition(53, 49);
                    Console.WriteLine(new string(' ', number.Length));
                }
                Thread.Sleep(5000);
                Console.SetCursorPosition(0, 3);
                Console.ForegroundColor = ConsoleColor.Cyan;
                display.Header(CurrentIncident);
                Console.ForegroundColor = ConsoleColor.Yellow;
                display.Header("box");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(53, 15);
                Console.WriteLine("Is anyone in immediate danger or in need of urgent help? ");
                while (true)
                {
                    try
                    {
                        Console.SetCursorPosition(53, 49);
                        Console.ForegroundColor = consoleColor;
                        immediateDanger1 = ErrorHandling.ReadYesNoInput("");
                        AddResponse(immediateDanger1);
                        Console.SetCursorPosition(130, 17);
                        Console.WriteLine(disturbanceLocation);
                        Console.SetCursorPosition(53, 49);
                        Console.WriteLine(new string(' ', disturbanceLocation.Length));
                        break;
                    }
                    catch (Exception ex)
                    {
                        mainMenu.ClearConsoleLine(54);
                        mainMenu.ClearConsoleLine(55);
                        Console.SetCursorPosition(83, 54);
                        Console.WriteLine(ex.Message);
                        Console.SetCursorPosition(53, 49);
                        Console.WriteLine(new string(' ', Math.Max(CurrentIncident?.Length ?? 100, 100)));
                    }
                }               

                if (immediateDanger1.ToLower() == "yes")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(53, 19);
                    Console.WriteLine("Please stay on a safe location. Medical units are being called to respond.");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(53, 19);
                    Console.WriteLine("Good to hear.");
                }
                Console.SetCursorPosition(53, 21);
                Console.WriteLine("Stay away from the disturbance if possible, and ensure your safety while we send assistance.");
                Console.SetCursorPosition(53, 25);
                Console.WriteLine("Thank you for the information. Officers are in route to investigate and ensure safety.");
                Console.SetCursorPosition(53, 27);
                Console.ForegroundColor = ConsoleColor.Green;
                mainMenu.PromptToContinue();
            }
            else if (crimecode == 22)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(53, 21);
                Console.WriteLine("Kindly describe the vandalism? (e.g., property damage, graffiti, broken windows)");
                Console.ForegroundColor = consoleColor;
                Console.SetCursorPosition(53, 49);
                string vandalismType = Console.ReadLine();
                AddResponse(vandalismType);
                Console.SetCursorPosition(130, 23);
                Console.WriteLine(vandalismType);
                Console.SetCursorPosition(53, 49);
                Console.WriteLine(new string(' ', vandalismType.Length));

                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(53, 25);
                Console.WriteLine("Is the vandalism currently ongoing, or has it already occurred? ");
                Console.ForegroundColor = consoleColor;
                Console.SetCursorPosition(53, 49);
                string vandalismStatus = Console.ReadLine();
                AddResponse(vandalismStatus);
                Console.SetCursorPosition(130, 27);
                Console.WriteLine(vandalismStatus);
                Console.SetCursorPosition(53, 49);
                Console.WriteLine(new string(' ', vandalismStatus.Length));

                if (vandalismType.ToLower() == "graffiti")
                {
                    Console.SetCursorPosition(53, 29);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Okay that's noted.");
                }
                else
                {
                    Console.SetCursorPosition(53, 29);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Okay that's noted. Please secure your own safety.");
                }

                if (vandalismStatus.ToLower() == "ongoing" || vandalismStatus.ToLower() == "still ongoing")
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(53, 31);
                    Console.WriteLine("The police officers have been alterted. Don't worry help is on the way.");
                    Console.SetCursorPosition(53, 35);
                    Console.WriteLine("Thank you for the information. Officers are en route to investigate and ensure safety.");
                    Console.SetCursorPosition(53, 37);
                    Console.ForegroundColor = ConsoleColor.Green;
                    mainMenu.PromptToContinue();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(53, 25);
                    Console.WriteLine("The police officers will investigate once they arrive.");
                    Console.SetCursorPosition(53, 29);
                    Console.WriteLine("Thank you for the information. Officers are en route to investigate and ensure safety.");
                    Console.SetCursorPosition(53, 31);
                    Console.ForegroundColor = ConsoleColor.Green;
                    mainMenu.PromptToContinue();
                }

               
            }
            CurrentFilePath = FilePath(CurrentIncident);
            SaveResponses(CurrentIncident, CurrentFilePath);           
        }
    }
    #endregion
    internal class EmergencyResponseSystem
    {
        public async Task AutomaticResponseSystem(string incident, string location)
        {
            MainMenu mainMenu  = new MainMenu();
            await Task.Delay(2000);
            Console.SetCursorPosition(78, 54);
            Console.ForegroundColor = ConsoleColor.Yellow;
            if (incident.ToLower() == "fire")
            {
                Console.WriteLine($"System: Fire fighters are responding to a fire in {location}. \n");
                Console.SetCursorPosition(86, 55);
                Console.WriteLine("They are coming your way, please stay calm.");
                Console.SetCursorPosition(53, 49);
                Console.ForegroundColor = ConsoleColor.White;
                await Task.Delay(5000);
            }
            else if(incident.ToLower() == "medical")
            {
                Console.WriteLine($"System: Medical units are responding to a medical emergency in {location}. \n");
                Console.SetCursorPosition(86, 55);
                Console.WriteLine("They are coming your way, please stay calm.");
                Console.SetCursorPosition(53, 49);
                Console.ForegroundColor = ConsoleColor.White;
            }
            else if(incident.ToLower() == "crime")
            {
                Console.WriteLine($"System: Police team are responding to an incident in {location}. \n");
                Console.SetCursorPosition(86, 55);
                Console.WriteLine("They are coming your way, please stay calm.");
                Console.SetCursorPosition(53, 49);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }

    /*
    Reference:
        general https://www.eugene-or.gov/2892/9-1-1-Call-Scripts
 
        crime https://www.crimescene.com/casefiles-garrett/2226-home-911
              https://www.nyc.gov/assets/nypd/downloads/pdf/public_information/911-transcripts-police-involved-shooting-040418.pdf
              https://victimconnect.org/learn/types-of-crime/assault/
              https://www.womenslaw.org/about-abuse/forms-abuse/sexual-abuse-and-exploitation/sexual-assault-rape/steps-take-after-sexual-4

        fire https://en.wikisource.org/wiki/9/11_Dispatcher_transcript

        medical 
                https://offices.austincc.edu/emergency-management/medical-emergency-reporting-and-response-procedures/
                https://www.medicalnewstoday.com/articles/322872#cpr-steps
                https://medlineplus.gov/ency/article/000022.htm
                https://www.healthline.com/health/home-treatments-for-shortness-of-breath#home-treatments
                https://positivechoices.org.au/teachers/how-to-put-someone-in-the-recovery-position
                https://medlineplus.gov/ency/article/000007.htm
                https://www.mayoclinic.org/first-aid/first-aid-choking/basics/art-20056637
                https://www.nhs.uk/conditions/poisoning/treatment/
                https://www.medicalnewstoday.com/articles/321500#treatment
                https://www.mayoclinic.org/first-aid/first-aid-fractures/basics/art-20056641
    */
}
