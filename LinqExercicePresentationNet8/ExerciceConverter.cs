using Newtonsoft.Json.Linq;

namespace LinqExercicePresentationNet8
{
    public class ExerciceConverter
    {
        // Fonction principale
        public void ConvertCSVToJSON(string sourcePath, string destPath = "", string stringToSearch = "", bool isDesc = false)
        {
            string[] lines = ReadCSVFile(sourcePath);
            if (lines == null || lines.Length == 0)
            {
                return;
            }

            var headers = ExtractHeaders(lines);
            var filteredLines = FilterLines(lines, stringToSearch, isDesc);
            var jsonObjects = CreateJsonObjects(headers, filteredLines);

            Console.WriteLine(jsonObjects.ToString());

            if (destPath != "")
            {
                ExportToJsonFile(destPath, jsonObjects);
            }
        }

        private string[] ReadCSVFile(string sourcePath)
        {
            try
            {
                return File.ReadAllLines(sourcePath);
            }
            catch (DirectoryNotFoundException e)
            {
                Console.WriteLine("Dossier source non trouvé !");
                return null;
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("Fichier source non trouvé !");
                return null;
            }
        }

        private string[] ExtractHeaders(string[] lines)
        {
            if (lines.Length == 0)
            {
                Console.WriteLine("Le fichier CSV est vide.");
                return null;
            }
            return lines[0].Split(',');
        }

        private IEnumerable<string> FilterLines(string[] lines, string stringToSearch, bool isDesc)
        {
            if (stringToSearch == "")
            {
                return from line in lines.Skip(1)
                       select line;
            }
            else
            {
                IEnumerable<string> query;
                if (isDesc)
                {
                    query = from line in lines.Skip(1)
                            where line.Contains(stringToSearch, StringComparison.InvariantCultureIgnoreCase)
                            orderby line descending
                            select line;
                }
                else
                {
                    query = from line in lines.Skip(1)
                            where line.Contains(stringToSearch, StringComparison.InvariantCultureIgnoreCase)
                            select line;
                }

                return query;
            }
        }

        private JArray CreateJsonObjects(string[] headers, IEnumerable<string> filteredLines)
        {
            var jsonObjects = new JArray();
            foreach (var line in filteredLines)
            {
                var values = line.Split(',');
                var jsonObject = new JObject();
                for (int i = 0; i < headers.Length; i++)
                {
                    jsonObject[headers[i]] = values[i];
                }
                jsonObjects.Add(jsonObject);
            }
            return jsonObjects;
        }

        private void ExportToJsonFile(string destPath, JArray jsonObjects)
        {
            Console.WriteLine($"Exporter le fichier dans {destPath} ? (O/N)");
            var choice = Console.ReadLine()?.Trim().ToUpperInvariant();
            switch (choice)
            {
                case "O":
                    try
                    {
                        File.WriteAllText(destPath, jsonObjects.ToString());
                        Console.WriteLine("Fichier exporté avec succès.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Erreur lors de l'export : {ex.Message}");
                    }
                    break;
                case "N":
                    Console.WriteLine("Export annulé.");
                    break;
                default:
                    Console.WriteLine("Choix invalide. L'export a été annulé.");
                    break;
            }
        }
    }
}