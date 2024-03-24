using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace ImageCompression
{
    class Program
    {
        static void Main(string[] args)
        {
            string sourceFolder = "";
            string destinationFolder = "";
            int compressionLevel = 50; // Change the compression level as needed

            CompressImagesInFolder(sourceFolder, destinationFolder, compressionLevel);
        }

        static void CompressImagesInFolder(string sourceFolder, string destinationFolder, int compressionLevel)
        {
            if (!Directory.Exists(sourceFolder))
            {
                Console.WriteLine("Source folder does not exist.");
                return;
            }

            if (!Directory.Exists(destinationFolder))
            {
                Directory.CreateDirectory(destinationFolder);
            }

            string[] imageFiles = Directory.GetFiles(sourceFolder, "*.jpg");

            foreach (string imageFile in imageFiles)
            {
                using (Bitmap bitmap = new Bitmap(imageFile))
                {
                    ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);
                    Encoder myEncoder = Encoder.Quality;
                    EncoderParameters myEncoderParameters = new EncoderParameters(1);
                    EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, compressionLevel);
                    myEncoderParameters.Param[0] = myEncoderParameter;

                    string destFilePath = Path.Combine(destinationFolder, Path.GetFileName(imageFile));

                    bitmap.Save(destFilePath, jpgEncoder, myEncoderParameters);
                }
            }

            Console.WriteLine("Compression complete.");
        }

        static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();

            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }

            return null;
        }
    }
}
