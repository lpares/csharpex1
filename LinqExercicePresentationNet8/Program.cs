using DataSources;
using System.Text;

var allAlbums = ListAlbumsData.ListAlbums;

void PrintAllAlbums()
{
    var request = from album in allAlbums
                  select $"Album n°{album.AlbumId} : {album.Title}";
    foreach (string albumString in request)
    {
        Console.WriteLine(albumString);
    }
}

void SearchAlbumNameQuerySyntax()
{
    Console.Write("Saisissez le nom de l'album : ");
    string? searchString = Console.ReadLine();

    var request = from album in allAlbums
                  where album.Title.Contains(searchString, StringComparison.InvariantCultureIgnoreCase)
                  orderby album.Title
                  select $"Album n°{album.AlbumId} : {album.Title}"; ;

    foreach (string album in request)
    {
        Console.WriteLine(album);
    }
}

void SearchAlbumNameQuerySyntaxDesc()
{
    Console.Write("Saisissez le nom de l'album : ");
    string? searchString = Console.ReadLine();

    var request = from album in allAlbums
                  where album.Title.Contains(searchString, StringComparison.InvariantCultureIgnoreCase)
                  orderby album.Title descending
                  select $"Album n°{album.AlbumId} : {album.Title}"; ;

    foreach (string album in request)
    {
        Console.WriteLine(album);
    }
}

void SearchAlbumNameMethodSyntax()
{
    Console.Write("Saisissez le nom de l'album : ");
    string? searchString = Console.ReadLine();

    var request = allAlbums
        .Where(album => album.Title.Contains(searchString, StringComparison.InvariantCultureIgnoreCase))
        .OrderBy(album => album.Title)
        .Select(album => $"Album n°{album.AlbumId} : {album.Title}");

    foreach (string album in request)
    {
        Console.WriteLine(album);
    }
}

void SearchAlbumNameMethodSyntaxDesc()
{
    Console.Write("Saisissez le nom de l'album : ");
    string? searchString = Console.ReadLine();

    var request = allAlbums
        .Where(album => album.Title.Contains(searchString, StringComparison.InvariantCultureIgnoreCase))
        .OrderByDescending(album => album.Title)
        .Select(album => $"Album n°{album.AlbumId} : {album.Title}");

    foreach (string album in request)
    {
        Console.WriteLine(album);
    }
}

void SearchAlbumNameQuerySyntaxGroupByArtist()
{
    Console.Write("Saisissez le nom de l'album : ");
    string? searchString = Console.ReadLine();

    var request =
        from album in allAlbums
        where album.Title.Contains(searchString, StringComparison.InvariantCultureIgnoreCase)
        group album by album.ArtistId into artistGroup
        select artistGroup;

    foreach (var group in request)
    {
        Console.WriteLine($"Artiste ID: {group.Key}");
        foreach (var album in group)
        {
            Console.WriteLine($"  Album n°{album.AlbumId} : {album.Title}");
        }
    }
}

void SearchAlbumNameQuerySyntaxGroupByArtist2()
{
    Console.Write("Saisissez le nom de l'album : ");
    string? searchString = Console.ReadLine();

    var request =
        from album in allAlbums
        where album.Title.Contains(searchString, StringComparison.InvariantCultureIgnoreCase)
        group album by album.ArtistId into artistGroup
        select new
        {
            artistId = artistGroup.Key,
            albums = artistGroup.ToList()
        };

    foreach (var group in request)
    {
        Console.WriteLine($"Artiste ID: {group.artistId}");
        foreach (var album in group.albums)
        {
            Console.WriteLine($" Album n°{album.AlbumId} : {album.Title}");
        }
    }
}

void SearchAlbumNameQuerySyntaxPagination()
{
    int paginationCount = 5; // Nombre d'albums par page
    int currentPage = 0; // Page actuelle (commence à 0)
    bool continuePagination = true;

    Console.Write("Saisissez le nom de l'album : ");
    string? searchString = Console.ReadLine();

    var filteredAlbums = allAlbums
        .Where(album => album.Title.Contains(searchString, StringComparison.InvariantCultureIgnoreCase))
        .ToList();

    while (continuePagination)
    {
        // Calculer le nombre total de pages
        int totalPages = (int)Math.Ceiling((double)filteredAlbums.Count / paginationCount);

        // Afficher les albums de la page actuelle
        var currentPageAlbums = filteredAlbums
            .Skip(currentPage * paginationCount)
            .Take(paginationCount);

        Console.WriteLine($"\n--- Page {currentPage + 1} / {totalPages} ---");
        foreach (var album in currentPageAlbums)
        {
            // Afficher le titre de l'album (troncature si nécessaire)
            string truncatedTitle = album.Title.Length > 20 ?
                album.Title.Substring(0, 20) + "..." :
                album.Title;
            Console.WriteLine($"Album n°{album.AlbumId} : {truncatedTitle}");
        }

        // Demander à l'utilisateur s'il veut passer à la page suivante ou précédente
        Console.WriteLine("\nChoisissez une option :");
        Console.WriteLine("1. Page suivante");
        Console.WriteLine("2. Page précédente");
        Console.WriteLine("3. Quitter");

        string? choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                if (currentPage < totalPages - 1)
                    currentPage++;
                else
                    Console.WriteLine("Vous êtes déjà sur la dernière page.");
                break;
            case "2":
                if (currentPage > 0)
                    currentPage--;
                else
                    Console.WriteLine("Vous êtes déjà sur la première page.");
                break;
            case "3":
                continuePagination = false;
                break;
            default:
                Console.WriteLine("Option invalide.");
                break;
        }
    }
}

void SearchInAlbumsTxt()
{
    // Lire le fichier Albums.txt ligne par ligne
    var albumLines = File.ReadAllLines(@".\Text\Albums.txt", Encoding.UTF8);

    // Demander à l'utilisateur de saisir un string
    Console.Write("Saisissez le nom de l'album : ");
    string? searchString = Console.ReadLine();

    // Filtrer les lignes qui contiennent le texte saisi (insensible à la casse)
    var matchingAlbums = albumLines
        .Where(line => line.IndexOf(searchString, StringComparison.InvariantCultureIgnoreCase) >= 0)
        .ToList();

    // Afficher les albums correspondants
    if (matchingAlbums.Any())
    {
        Console.WriteLine("\nAlbums trouvés :");
        foreach (var album in matchingAlbums)
        {
            Console.WriteLine(album);
        }
    }
    else
    {
        Console.WriteLine("Aucun album trouvé.");
    }
}


//Console.WriteLine("QUERY SYNTAX");
//SearchAlbumNameQuerySyntax();

//Console.WriteLine("QUERY SYNTAX DESCENDING");
//SearchAlbumNameQuerySyntaxDesc();

//Console.WriteLine("METHOD SYNTAX");
//SearchAlbumNameMethodSyntax();

//Console.WriteLine("METHOD SYNTAX DESCENDING");
//SearchAlbumNameMethodSyntaxDesc();

//SearchAlbumNameQuerySyntaxGroupByArtist();

//SearchAlbumNameQuerySyntaxGroupByArtist2();

//SearchAlbumNameQuerySyntaxPagination();

SearchInAlbumsTxt();