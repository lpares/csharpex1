namespace GenImageResolution
{
    public class ImageGenerator
    {
        public byte[] CreateNewFileFromFile(string sourceImagePath, int width, int height)
        {
            byte[] sourceImage = File.ReadAllBytes(sourceImagePath);
            ImageResizer resizer = new ImageResizer();
            byte[] resizedImage = resizer.ResizeImage(sourceImage, width, height);
            return resizedImage;
        }

        public void SaveFileToPath(string genImagesFolderPath, string imageName, int resolution, byte[] resizedImage, string fileExtension)
        {
            Directory.CreateDirectory(genImagesFolderPath);

            string outputPath = Path.Combine(genImagesFolderPath, $"{imageName}_{resolution}.{fileExtension}");
            try
            {
                File.WriteAllBytes(outputPath, resizedImage);
            }
            catch (DirectoryNotFoundException)
            {
                Console.WriteLine($"Chemin {outputPath} non trouvé !");
            }
        }

        public async Task AsyncSaveFileToPath(string genImagesFolderPath, string imageName, int resolution, byte[] resizedImage, string fileExtension)
        {
            Directory.CreateDirectory(genImagesFolderPath);
            string outputPath = Path.Combine(genImagesFolderPath, $"{imageName}_{resolution}.{fileExtension}");

            // Ensure the file is not being used by another process
            using (var fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                await fileStream.WriteAsync(resizedImage, 0, resizedImage.Length);
            }
        }
    }
}
