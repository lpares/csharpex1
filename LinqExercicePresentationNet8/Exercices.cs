using DataSources;
using System.Text;
using System.Xml.Linq;

namespace LinqExercicePresentationNet8
{
    public class Exercices
    {

        List<Album> allAlbums = ListAlbumsData.ListAlbums;

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

                Console.WriteLine("Appuyez sur entrée pour passer à la page suivante...");
                string? choice = Console.ReadLine();
                currentPage++;
            }
        }

        void SearchInAlbumsTxt()
        {
            // Lire le fichier Albums.txt ligne par ligne
            var albumLines = File.ReadAllLines(@".\Text\Albums.txt", Encoding.UTF8);

            // Demander à l'utilisateur de saisir un string
            Console.Write("Saisissez le nom de l'album : ");
            string? searchString = Console.ReadLine();

            // Filtrer les lignes qui contiennent le texte saisi
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
        }
        void AlbumsListToXML()
        {
            var albumLines = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), "Text", "Albums.txt"), Encoding.UTF8);

            var root = new XElement("Root",
                new XElement("Albums",
                    from line in albumLines
                    let parts = line.Split(':')
                    where parts.Length >= 2
                    select new XElement("Album",
                        new XElement("AlbumId", parts[0].Trim()),
                        new XElement("AlbumTitle", parts[1].Trim())
                    )
                )
            );

            Console.WriteLine(root);
        }

        void JSONFileToXML()    // PAS FAIT !
        {
            var albumFile = Path.Combine(Directory.GetCurrentDirectory(), "Json", "Albums.json");

            //var 
        }

    }
}
