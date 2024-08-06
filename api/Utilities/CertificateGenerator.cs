using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QRCoder;
using SixLabors.Fonts;

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Drawing.Processing;
using System.IO;

namespace api.Utilities
{
    public class CertificateGenerator
    {
        public byte[] GenerateCertificateWithQR(
            string participantName,
            double textXPosition,
            double textYPosition,
            string alignment,
            string uniqueCode,
            string certificateImagePath,
            double qrXPosition,
            double qrYPosition,
            bool isBold,
            bool isItalic,
            bool isUnderlined,
            string fontFamily,
            double qrCodeSize = 2,
            int fontSize = 24,
            Rgba32 fontColor = default)
        {

            var fontStyle = "";
            if (isBold && isItalic) fontStyle = "BoldItalic";
            else if (isBold)
                fontStyle = "Bold";
            else if (isItalic)
                fontStyle = "Italic";
            else 
                fontStyle = "Regular";
            

            var fontPath = Path.Combine("wwwroot", "fonts", $"{fontFamily}", $"{fontStyle}.ttf");
            var fontCollection = new FontCollection();
            var fontFamilyLoaded = fontCollection.Add(fontPath);

            // Load the font
            var font = fontFamilyLoaded.CreateFont(fontSize, FontStyle.Regular);

            QRCodeGenerator qrGenerator = new();
            string certificateLink = "http://192.168.1.8:3000/validate/" + uniqueCode;
            QRCodeData qrData = qrGenerator.CreateQrCode(certificateLink, QRCodeGenerator.ECCLevel.Q);
            PngByteQRCode qrCode = new(qrData);
            byte[] qrCodeBytes = qrCode.GetGraphic((int)qrCodeSize);

            using Image<Rgba32> certificateImage = Image.Load<Rgba32>(certificateImagePath);

            using var qrImageStream = new MemoryStream(qrCodeBytes);
            using Image<Rgba32> qrImage = Image.Load<Rgba32>(qrImageStream);

            certificateImage.Mutate(ctx => ctx.DrawImage(qrImage, new Point((int)qrXPosition, (int)qrYPosition), 1f));

            var textOptions = new TextOptions(font)
            {
                HorizontalAlignment = HorizontalAlignment.Left
            };

            var textSize = TextMeasurer.MeasureSize(participantName, textOptions);
            var participantPlaceholderSize = TextMeasurer.MeasureSize("{{ Participant Name }}", textOptions);

            switch (alignment.ToLower())
            {
                case "center":
                    textXPosition -= textSize.Width / 2;
                    break;
                case "right":
                    textXPosition = textXPosition + participantPlaceholderSize.Width - textSize.Width;
                    break;
            }

            certificateImage.Mutate(ctx => ctx.DrawText(participantName, font, Color.Black, new PointF((float)textXPosition, (float)textYPosition)));

            // if (isUnderlined)
            // {
                
            //     var underlineThickness = 2; // Thickness of the underline
            //     var underlineYPosition = textYPosition + textSize.Height + 10; // Position of the underline

            //     var underlinePen = new SolidPen(fontColor, underlineThickness);
            //     certificateImage.Mutate(ctx => ctx.DrawLine(
            //         underlinePen,
            //         new PointF((float)textXPosition, (float)underlineYPosition),
            //         new PointF((float)(textXPosition + textSize.Width), (float)underlineYPosition)
            //     ));
            // }

            using var resultStream = new MemoryStream();
            certificateImage.Save(resultStream, new PngEncoder());
            return resultStream.ToArray();
        }
    }
}