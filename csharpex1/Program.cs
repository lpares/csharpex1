using System.Data;

void calculatrice() {
    Console.WriteLine("Entrez votre calcul : ");
    string? saisie = Console.ReadLine();

    double resultat = Convert.ToDouble(new DataTable().Compute(saisie, null));

    Console.WriteLine($"Résultat : {resultat}");
}



calculatrice();