namespace UserInterface
{
    public class EmergencyCallScreen
    {
        public void DisplayCallingScreen()
        {
            Console.Clear();

            int consoleWidth = Console.WindowWidth;
            int consoleHeight = Console.WindowHeight;

            // Borders and headers for UI
            string borderLine = new string('═', consoleWidth);
            string header = "!! EMERGENCY RESPONSE SYSTEM !!";
            string message = "Calling Emergency Services";

            int centeredHeaderLeft = (consoleWidth - header.Length) / 2;
            int centeredMessageLeft = (consoleWidth - message.Length) / 2;
            int centeredMessageTop = consoleHeight / 2;

            // Display header and top border
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(0, 22);
            Console.WriteLine(borderLine);

            Console.SetCursorPosition(centeredHeaderLeft, 24);
            Console.WriteLine(header);

            Console.SetCursorPosition(0, 26);
            Console.WriteLine(borderLine);

            // Display the calling message with an animation effect
            Console.ForegroundColor = ConsoleColor.White;
            for (int i = 0; i < 3; i++)
            {
                Console.SetCursorPosition(centeredMessageLeft, centeredMessageTop);
                Console.Write(message);

                // Add ellipses animation
                for (int dots = 0; dots < 3; dots++)
                {
                    Thread.Sleep(400); // Pause briefly for each dot
                    Console.Write(".");
                }
                Thread.Sleep(400); // Pause between animations
            }

            // Clear the calling message and show "Connected"
            Console.Clear();

            // Display header and top border again
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(0, 22);
            Console.WriteLine(borderLine);
            string connectedMessage = "Connected to Emergency Services";
            Console.SetCursorPosition(centeredHeaderLeft, 24);
            Console.WriteLine(connectedMessage.ToUpper());

            Console.SetCursorPosition(0, 26);
            Console.WriteLine(borderLine);

            // Display connection message with a box

            int connectedMessageLeft = (consoleWidth - connectedMessage.Length) / 2;



            // Hold the connection screen for effect
            Thread.Sleep(2000);
            Console.Clear();
        }

        public void DisplayAdminScreen()
        {
            int consoleWidth = Console.WindowWidth;
            int consoleHeight = Console.WindowHeight;

            // Borders and headers for UI
            string borderLine = new string('═', consoleWidth);
            string header = "WELCOME TO THE ADMIN PANEL";

            int centeredHeaderLeft = (consoleWidth - header.Length) / 2;
            int centeredMessageTop = consoleHeight / 2;

            // Display header and top border
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(0, 22);
            Console.WriteLine(borderLine);

            Console.SetCursorPosition(centeredHeaderLeft, 24);
            Console.WriteLine(header);

            Console.SetCursorPosition(0, 26);
            Console.WriteLine(borderLine);
            Thread.Sleep(4000);
        }

        public void DisplayArchiveScreen(string Incident)
        {
            int consoleWidth = Console.WindowWidth;
            int consoleHeight = Console.WindowHeight;

            // Borders and headers for UI
            string borderLine = new string('═', consoleWidth);
            string header = $"{Incident} Incidents Archived Successfully";

            int centeredHeaderLeft = (consoleWidth - header.Length) / 2;
            int centeredMessageTop = consoleHeight / 2;

            // Display header and top border
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(0, 22);
            Console.WriteLine(borderLine);

            Console.SetCursorPosition(centeredHeaderLeft, 24);
            Console.WriteLine(header);

            Console.SetCursorPosition(0, 26);
            Console.WriteLine(borderLine);
            Thread.Sleep(4000);
        }
    }
    public class Message
    {
        public void IntroDisplay()
        {
            string IncidentPath = AppContext.BaseDirectory + @"\SWIFT AID.txt";

            try
            {
                using (StreamReader reader = File.OpenText(IncidentPath))
                {
                    string line;
                    int n = 0;
                    while ((line = reader.ReadLine()) != null)
                    {
                        n++;
                        if (n >= 15)
                        {
                            Thread.Sleep(130);
                        }

                        Console.WriteLine(line);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to read file");
            }
        }

        public void OutroDisplay()
        {
            Console.Clear();
            int consoleWidth = Console.WindowWidth;
            int consoleHeight = Console.WindowHeight;

            // Borders and headers for UI
            string borderLine = new string('═', consoleWidth);
            string header = "THANK YOU FOR USING SWIFT AID KEEP SAFE";

            int centeredHeaderLeft = (consoleWidth - header.Length) / 2;
            int centeredMessageTop = consoleHeight / 2;

            // Display header and top border
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(0, 22);
            Console.WriteLine(borderLine);

            Console.SetCursorPosition(centeredHeaderLeft, 24);
            Console.WriteLine(header);

            Console.SetCursorPosition(0, 26);
            Console.WriteLine(borderLine);
            Thread.Sleep(4000);
        }
    }

    public class Display
    {
        private string HeaderName;
        public void Header(string word)
        {
            if(word == "main")
            {
                HeaderName = AppContext.BaseDirectory + @"\Headers\main header.txt";
            }
            else if(word == "emergency")
            {
                HeaderName = AppContext.BaseDirectory + @"\Headers\emergency header.txt";
            }
            else if(word == "fire")
            {
                HeaderName = AppContext.BaseDirectory + @"\Headers\fire header.txt";
            }
            else if (word == "medical")
            {
                HeaderName = AppContext.BaseDirectory + @"\Headers\medical header.txt";
            }
            else if (word == "crime")
            {
                HeaderName = AppContext.BaseDirectory + @"\Headers\crime header.txt";
            }
            else if (word == "incident")
            {
                HeaderName = AppContext.BaseDirectory + @"\Headers\incident header.txt";
            }
            else if (word == "admin")
            {
                HeaderName = AppContext.BaseDirectory + @"\Headers\admin header.txt";
            }
            else if(word == "update")
            {
                HeaderName = AppContext.BaseDirectory + @"\Headers\update header.txt";
            }
            else if(word == "archive")
            {
                HeaderName = AppContext.BaseDirectory + @"\Headers\archive header.txt";
            }
            else if(word == "choice1")
            {
                HeaderName = AppContext.BaseDirectory + @"\Headers\choice1.txt";
            }
            else if (word == "box")
            {
                HeaderName = AppContext.BaseDirectory + @"\Headers\box.txt";
            }
            else if (word == "box1")
            {
                HeaderName = AppContext.BaseDirectory + @"\Headers\box1.txt";
            }
            else if (word == "adminchoice")
            {
                HeaderName = AppContext.BaseDirectory + @"\Headers\adminchoice.txt";
            }
            else if (word == "incidentchoice")
            {
                HeaderName = AppContext.BaseDirectory + @"\Headers\incident choice.txt";
            }
            else if (word == "archive choice")
            {
                HeaderName = AppContext.BaseDirectory + @"\Headers\archive choice.txt";
            }


            try
            {
                using (StreamReader reader = File.OpenText(HeaderName))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        Console.WriteLine(line);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to read file");
            }
        }
    }
}
