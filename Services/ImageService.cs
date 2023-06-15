using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Bmp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Formats.Tiff;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Processors.Transforms;
using System.Drawing.Imaging;
using Org.BouncyCastle.Utilities.Zlib;

namespace RecipeAZ.Services {
    public class ImageService {

        private IImageEncoder GetEncoder(string imageFormat) {
            switch (imageFormat.ToLower()) {
                case ".jpeg":
                case ".jpg":
                    return new JpegEncoder();
                case ".png":
                    return new PngEncoder();
                case ".bmp":
                    return new BmpEncoder();
                case ".tiff":
                case ".tif":
                    return new TiffEncoder();
                default:
                    throw new NotSupportedException($"Image format '{imageFormat}' is not supported.");
            }
        }

        public bool AdjustImageToMaxFileSize(Image image, int maxSizeInBytes, string imageFormat) {
            Console.WriteLine("beginning adjustment");
            using (var outputStream = new MemoryStream()) {
                var encoder = GetEncoder(imageFormat);

                if ((encoder is JpegEncoder jpegEncoder && AdjustJpegQuality(image, outputStream, jpegEncoder, maxSizeInBytes))
                    || (encoder is PngEncoder pngEncoder && AdjustPngCompression(image, outputStream, pngEncoder, maxSizeInBytes))
                    || AdjustDimensionsToMaxFileSize(image, maxSizeInBytes, encoder)) {
                    Console.WriteLine("Adjusted");
                    return true;
                }
                return false;                   
            }
        }

        private bool AdjustJpegQuality(Image image, MemoryStream outputStream, JpegEncoder encoder, int maxSizeInBytes) {
            int quality = 100;
            int stepSize = 10;

            do {
                encoder = new JpegEncoder { Quality = quality };
                image.Save(outputStream, encoder);

                if (outputStream.Length <= maxSizeInBytes)
                    return true;

                quality -= stepSize;

            } while (quality >= 0 && stepSize > 0);
            return false;
            
        }


        private bool AdjustPngCompression(Image image, MemoryStream outputStream, PngEncoder encoder, int maxSizeInBytes) {
            return false;
            Console.WriteLine("Starting PNG compression");
            int compressionLevelValue = 0;
            int stepSize = 1;

            do {
                var compressionLevel = (PngCompressionLevel)compressionLevelValue;
                encoder = new PngEncoder { CompressionLevel = compressionLevel };
                image.Save(outputStream, encoder);
                Console.WriteLine(encoder.CompressionLevel + " " + outputStream.Length);
                if (outputStream.Length <= maxSizeInBytes)
                    return true;

                compressionLevelValue += stepSize;

            } while (compressionLevelValue <= 9 && stepSize > 0);
            return false;
        }


        private bool AdjustDimensionsToMaxFileSize(Image image, int maxSizeInBytes, IImageEncoder encoder) {
            using var outputStream = new MemoryStream();
            int width = image.Width;
            int height = image.Height;
            float ratio = width / height;
            while (outputStream.Length > maxSizeInBytes) {
                width = width - (int)Math.Max(width * 0.25f, 1);
                height = (int)Math.Max(width * ratio, 1);

                image.Mutate(x => x.Resize(new ResizeOptions {
                    Size = new Size(width, height),
                    Mode = ResizeMode.Max
                }));
                image.Save(outputStream, encoder);
                outputStream.Position = 0;
                image = Image.Load(outputStream);
                outputStream.SetLength(0);
            }
            return true;
        }
    }
}