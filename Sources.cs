using System.Globalization;

namespace Sources
{
    public interface IAsk
    {
        void AskQuestions(string location);
    }

    public abstract class Information
    {
        private List<string> name = new List<string>();
        private List<string> location = new List<string>();
        private List<string> incident = new List<string>();
        private List<string> response = new List<string>();
        public ConsoleColor consoleColor = ConsoleColor.White;
        //public ConsoleColor consoleColor2 = ConsoleColor.Red;

        public List<string> Name
        {
            get { return name; }
            set { name = value; }
        }

        public List<string> Location
        {
            get { return location; }
            set { location = value; }
        }

        public List<string> Incident
        {
            get { return incident; }
            set { incident = value; }
        }

        public List<string> Response
        {
            get { return response; }
            set { response = value; }
        }

        public void AddName(string names)
        {
            if (!string.IsNullOrWhiteSpace(names))
            {
                name.Add(names);
            }
        }

        public void AddLocation(string locations)
        {
            if (!string.IsNullOrWhiteSpace(locations))
            {
                location.Add(locations);
            }
        }

        public void AddIncident(string incidents)
        {
            if (!string.IsNullOrWhiteSpace(incidents))
            {
                incident.Add(incidents);
            }
        }

        public void AddResponse(string responses)
        {
            if (!string.IsNullOrWhiteSpace(responses))
            {
                response.Add(responses);
            }
        }

        public string FilePath(string incident)
        {
            string error = "Invalid Incident";
            if (incident.ToLower() == "fire")
            {
                string filePath = @"C:\Users\Shan Michael\OneDrive\文档\2nd Year 1st Sem\OOP\swift aid\Incidents\fire incidents.txt";
                return filePath;

            }
            else if (incident.ToLower() == "medical")
            {
                string filePath = @"C:\Users\Shan Michael\OneDrive\文档\2nd Year 1st Sem\OOP\swift aid\Incidents\medical incidents.txt";
                return filePath;
            }
            else if (incident.ToLower() == "crime")
            {
                string filePath = @"C:\Users\Shan Michael\OneDrive\文档\2nd Year 1st Sem\OOP\swift aid\Incidents\crime incidents.txt";
                return filePath;
            }
            else
            {
                return error;
            }
        }

        public void SaveIncident(string incident, int position, string filePath)
        {
            if (File.Exists(filePath))
            {
                try
                {
                    using (StreamWriter writer = new StreamWriter(filePath, true))
                    {

                        writer.WriteLine("+-------- INCIDENT LOG --------+");
                        writer.WriteLine($"Date & Time: {DateTime.Now}");
                        writer.WriteLine($"Name: {name[position]}");
                        writer.WriteLine($"Location: {location[position]}");
                        writer.WriteLine($"Incident: {incident}");
                        writer.WriteLine("Status: In Progress");
                        UpdateStatus(filePath,incident);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error saving incident: {ex.Message}");
                }
            }
            else
            {
                try
                {
                    using (StreamWriter writer = File.CreateText(filePath))
                    {

                        writer.WriteLine("\n+-------- INCIDENT LOG --------+");
                        writer.WriteLine($"Date & Time: {DateTime.Now}");
                        writer.WriteLine($"Name: {name[position]}");
                        writer.WriteLine($"Location: {location[position]}");
                        writer.WriteLine($"Incident: {incident}");
                        writer.WriteLine("Status: In Progress");
                        UpdateStatus(filePath,incident);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error saving incident: {ex.Message}");
                }
            }
        }

        public void SaveResponses(string currentIncident, string currentFilePath)
        {
            try
            {
                using (StreamWriter writer1 = new StreamWriter(currentFilePath, true))
                {
                    writer1.WriteLine("\nResponses:");
                    for (int i = 0; i < response.Count; i++)
                    {
                        writer1.WriteLine($"\t{i + 1}: {response[i]}");
                    }
                    writer1.WriteLine();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving response: {ex.Message}");
            }
        }

        private async Task UpdateStatus(string filePath,string incident)
        {
            if(incident.ToLower() == "fire")
            {
                await Task.Delay(300000); //5 minutes [60*5*1000]
            }
            else if(incident.ToLower() == "medical")
            {
                await Task.Delay(30000); //30 seconds [30*1000]
            }
            else if(incident.ToLower() == "crime")
            {
                await Task.Delay(600000); //10 minuutes [60*10*1000]
            }
            string[] lines = File.ReadAllLines(filePath);

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (string line in lines)
                {
                    // Check if the line contains the "Status: In Progress" text
                    if (line.Contains("Status: In Progress"))
                    {
                        // Replace "In Progress" with "Completed"
                        writer.WriteLine("Status: Resolved");
                    }
                    else
                    {
                        // Write the original line if no replacement is needed
                        writer.WriteLine(line);
                    }
                }
            }
        }
    }
}