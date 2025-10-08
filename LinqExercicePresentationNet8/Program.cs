using LinqExercicePresentationNet8;

ExerciceConverter converter = new ExerciceConverter();

Console.Write("Saisissez l'emplacement du fichier CSV à convertir (chemin absolu) : ");
string? sourcePath = Console.ReadLine();

Console.Write("Saisissez l'emplacement du fichier JSON converti (chemin absolu) : ");
string? destPath = Console.ReadLine();

Console.Write("Voulez-vous rechercher un élément en particulier dans le fichier ? (O/N)");
string? choice = Console.ReadLine();
string? stringToSearch = "";
if (choice.ToUpper() == "O")
{
    Console.Write("Tapez votre texte à rechercher dans le fichier : ");
    stringToSearch = Console.ReadLine();
}

Console.Write("Trier par ordre décroissant ? (O/N)");
choice = Console.ReadLine();
bool isDesc = false;
if (choice.ToUpper() == "O")
{
    isDesc = true;
}

converter.ConvertCSVToJSON(sourcePath, destPath, stringToSearch, isDesc);