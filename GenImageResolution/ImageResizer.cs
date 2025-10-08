using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using System.Drawing;
using System.IO;

namespace GenImageResolution
{
    public class ImageResizer
    {
        public byte[] ResizeImage(byte[] sourceImage, int width, int height)
        {
            byte[] destImage;
            using (MemoryStream ms = new MemoryStream(sourceImage))
            {
                using (var imageOriginale = SixLabors.ImageSharp.Image.Load(ms))
                {
                    using (var resizedImage = imageOriginale.Clone(ctx =>
                    {
                        ctx.Resize(new ResizeOptions
                        {
                            Size = new SixLabors.ImageSharp.Size(width, height),
                            Mode = ResizeMode.Stretch
                        });
                    }))
                    {
                        using (MemoryStream resizedMs = new MemoryStream())
                        {
                            resizedImage.Save(resizedMs, new JpegEncoder { Quality = 85 });
                            destImage = resizedMs.ToArray();
                        }
                    }
                }
            }
            return destImage;
        }

        public void SaveResized(SixLabors.ImageSharp.Image src, int maxSize, string outputPath)
        {
            using var clone = src.Clone(ctx =>
            {
                ctx.Resize(new ResizeOptions
                {
                    Mode = ResizeMode.Max,
                    Size = new SixLabors.ImageSharp.Size(maxSize, maxSize),
                });
            });
            var encoder = new JpegEncoder { Quality = 85 };
            clone.Save(outputPath, encoder);
        }
    }
}