﻿using Svg;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace AudioBand
{
    internal static class Extensions
    {
        public static Bitmap ToBitmap(this SvgDocument svg)
        {
            return ToBitmap(svg, (int)svg.Width.Value, (int)svg.Height.Value);
        }

        public static Bitmap ToBitmap(this SvgDocument svg, int width, int height)
        {
            var bmp = new Bitmap(width, height);
            using (var graphics = Graphics.FromImage(bmp))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                svg.Draw(graphics);
                return bmp;
            }
        }

        public static Image Resize(this Image image, int width, int height)
        {
            // Padding issues
            var rect = new Rectangle(0, 0, width - 2, height - 2);
            var newImage = new Bitmap(width - 2, height - 2);
            using (var graphics = Graphics.FromImage(newImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphics.DrawImage(image, rect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel);
            }

            return newImage;
        }

        public static Image Scale(this Image image, int targetWidth, int targetHeight)
        {
            var ratiow = (float)image.Width / targetWidth;
            var ratioh = (float)image.Height / targetHeight;

            if (ratiow > ratioh)
            {
                // The width is bigger so the new width will become the target's width and the height will be scaled with the ratio between the original width and the target width
                return Resize(image, targetWidth, (int)(image.Height / ratiow));
            }
            else
            {
                return Resize(image, (int) (image.Width / ratioh), targetHeight);
            }
        }

        public static SvgDocument SizeFix(this SvgDocument svg)
        {
            svg.Width -= 3;
            svg.Height -= 3;
            return svg;
        }
    }
}
