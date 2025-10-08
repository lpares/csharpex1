using GenImageResolution;
using System.Diagnostics;

string IMAGES_FOLDER_PATH = "../../../images";
string GEN_IMAGES_FOLDER_PATH = "../../../gen_images";

// Définition des résolutions à générer pour chaque image
int[] RESOLUTIONS = { 1080, 720, 480 };

ImageGenerator generator = new ImageGenerator();

var sw = Stopwatch.StartNew();

void UnoptimizedGenResolution(string imagesFolderPath, string genImagesFolderPath, int[] resolutions, string genImagesFormat)
{
    // Récupérer toutes les images du dossier
    var images = Directory.GetFiles(imagesFolderPath);

    // Initialiser la liste d'images générées (en sortie)
    List<byte[]> genImages = new List<byte[]>();

    // Pour chaque image récupérée
    foreach (var image in images)
    {
        // Générer chaque résolution d'image
        foreach (int resolution in resolutions)
        {
            genImages.Add(generator.CreateNewFileFromFile(image, resolution, resolution));
        }
    }

    // Enregistrer les images générées dans le dossier source
    int imageIndex = 0;
    foreach (var image in genImages)
    {
        string imageName = $"gen_{imageIndex / resolutions.Length}";
        int resolution = resolutions[imageIndex % resolutions.Length];
        generator.SaveFileToPath(GEN_IMAGES_FOLDER_PATH, imageName, resolution, image, genImagesFormat);
        imageIndex++;
    }

    sw.Stop();
    Console.WriteLine($"Temps de calcul : {sw.ElapsedMilliseconds} ms");
}

//UnoptimizedGenResolution(IMAGES_FOLDER_PATH, GEN_IMAGES_FOLDER_PATH, RESOLUTIONS, "jpg");

void OptimizedGenResolution(string imagesFolderPath, string genImagesFolderPath, int[] resolutions, string genImagesFormat)
{
    // Récupérer toutes les images du dossier
    var images = Directory.GetFiles(imagesFolderPath);

    // Initialiser la liste d'images générées (en sortie)
    List<byte[]> genImages = new List<byte[]>();

    // Pour chaque image récupérée
    Parallel.ForEach(images, image =>
    {
        // Générer chaque résolution d'image
        Parallel.ForEach(resolutions, resolution =>
        {
            genImages.Add(generator.CreateNewFileFromFile(image, resolution, resolution));
        });
    });

    // Enregistrer les images générées dans le dossier source
    int imageIndex = 0;
    Parallel.ForEach(genImages, image =>
    {
        string imageName = $"gen_{imageIndex / resolutions.Length}";
        int resolution = resolutions[imageIndex % resolutions.Length];
        generator.AsyncSaveFileToPath(GEN_IMAGES_FOLDER_PATH, imageName, resolution, image, genImagesFormat);
        imageIndex++;
    });

    sw.Stop();
    Console.WriteLine($"Temps de calcul : {sw.ElapsedMilliseconds} ms");
}

OptimizedGenResolution(IMAGES_FOLDER_PATH, GEN_IMAGES_FOLDER_PATH, RESOLUTIONS, "jpg");