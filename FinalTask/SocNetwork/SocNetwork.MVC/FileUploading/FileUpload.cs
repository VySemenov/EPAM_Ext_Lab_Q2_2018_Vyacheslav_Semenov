namespace SocNetwork.FileUploading
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.IO;
    using System.Linq;
    using System.Web;

    public static class FileUpload
    {
        private static readonly int Diameter = 200;
        private static readonly string UploadsFolderName = "Uploads";
        private static readonly string UserAvatarsFolderName = "UserAvatars";
        private static readonly string RoundedAvatarsFolderName = "UserAvatarsRounded";
        private static readonly List<string> FileFormat = new List<string>() { ".png", ".jpeg", ".jpg" };

        private static readonly char DirSeparator = Path.DirectorySeparatorChar;
        private static readonly string FilesPath = HttpContext.Current.Server.MapPath("~\\Content" + DirSeparator + UploadsFolderName + DirSeparator + UserAvatarsFolderName + DirSeparator);

        /// <summary>
        /// Upload the user's avatar.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="userId">Users's id for name of file</param>
        /// <returns></returns>
        public static string UploadFile(HttpPostedFileBase file, int userId)
        {
            if (file == null)
            {
                return string.Empty;
            }

            if (!(file.ContentLength > 0))
            {
                return string.Empty;
            }

            string fileExt = Path.GetExtension(file.FileName);
            string fileName = userId.ToString() + fileExt;

            if (fileExt == null || !FileFormat.Contains(fileExt))
            {
                return string.Empty;
            }

            if (!Directory.Exists(FilesPath))
            {
                Directory.CreateDirectory(FilesPath);
            }

            string path = FilesPath + DirSeparator + fileName;
            file.SaveAs(Path.GetFullPath(path));

            RoundingImage(file, fileName);

            return fileName;
        }

        /// <summary>
        /// Generates the path to the file and call RemoveFile(path)
        /// </summary>
        /// <param name="fileName">Name of file</param>
        public static void DeleteFile(string fileName)
        {
            if (fileName == null || fileName.Length == 0)
            {
                return;
            }

            string path = FilesPath + DirSeparator + fileName;
            string thumbPath = FilesPath + DirSeparator + RoundedAvatarsFolderName + DirSeparator + fileName;

            RemoveFile(path);
            RemoveFile(thumbPath);
        }

        /// <summary>
        /// Remove file on path
        /// </summary>
        /// <param name="path">Path</param>
        private static void RemoveFile(string path)
        {
            if (File.Exists(Path.GetFullPath(path)))
            {
                File.Delete(Path.GetFullPath(path));
            }
        }

        /// <summary>
        /// Gets from the original image a circle with a specified diameter centered in the center of the original image.
        /// </summary>
        /// <param name="file">File</param>
        /// <param name="fileName">Name of file</param>
        private static void RoundingImage(HttpPostedFileBase file, string fileName)
        {
            string thumbnailDirectory = string.Format(@"{0}{1}{2}", FilesPath, DirSeparator, RoundedAvatarsFolderName);

            if (!Directory.Exists(thumbnailDirectory))
            {
                Directory.CreateDirectory(thumbnailDirectory);
            }

            string imagePath = string.Format(@"{0}{1}{2}", thumbnailDirectory, DirSeparator, fileName);

            using (FileStream stream = new FileStream(Path.GetFullPath(imagePath), FileMode.OpenOrCreate))
            {
                using (Image origImage = Image.FromStream(file.InputStream))
                {
                    int diameter = Diameter;
                    using (Bitmap roundedImage = new Bitmap(origImage.Width, origImage.Height))
                    {
                        using (Graphics gr = Graphics.FromImage(roundedImage))
                        {
                            gr.Clear(Color.Transparent);
                            gr.SmoothingMode = SmoothingMode.AntiAlias;
                            gr.CompositingQuality = CompositingQuality.HighQuality;
                            gr.InterpolationMode = InterpolationMode.HighQualityBicubic;

                            using (Brush brush = new TextureBrush(origImage))
                            {
                                using (GraphicsPath gp = new GraphicsPath())
                                {
                                    int x = (origImage.Width / 2) - (diameter / 2);
                                    int y = (origImage.Height / 2) - (diameter / 2);
                                    gp.AddArc(x, y, diameter, diameter, 180, 90);
                                    gp.AddArc(x, y, diameter, diameter, 270, 90);
                                    gp.AddArc(x, y, diameter, diameter, 0, 90);
                                    gp.AddArc(x, y, diameter, diameter, 90, 90);

                                    gr.FillPath(brush, gp);

                                    using (Bitmap finalImage = roundedImage.Clone(new Rectangle(x, y, diameter, diameter), roundedImage.PixelFormat))
                                    {
                                        finalImage.Save(stream, origImage.RawFormat);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}