using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using ExplorerApplication.Models;

namespace ExplorerApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string path)
        {
                       
            if (path == null)
            {
                path = @"C:\";
            }
            ViewBag.Message = path;
            DirectoryModel model = this.InitialDirectory(path); 
            return View(model);
        }

        //public ActionResult Change(string path)
        //{
        //    ViewBag.Message = path;
        //    DirectoryModel model = this.InitialDirectory();
        //    return View(model);
        //    //return View();
        //}


        /// <summary>
        /// This method get all directorys and files in path and create DirectoryModel's object.
        /// </summary>
        /// <param name="path">String with directory's path.</param>
        /// <returns>DirectoryModel's object or null if path is incorrect</returns>
        private DirectoryModel InitialDirectory (string path)
        {
            List<FileModel> files = new List<FileModel>();
            try
            {
                if (Directory.Exists(path))
                {
                    DirectoryInfo dir = new DirectoryInfo(path);

                    DirectoryInfo[] directories = dir.GetDirectories();
                    foreach (var directory in directories)
                        files.Add(new FileModel() { Name = directory.Name, Date = directory.CreationTime, Type = "directory", FullName = directory.FullName });

                    FileInfo[] directoryFiles = dir.GetFiles();
                    foreach (var file in directoryFiles)
                        files.Add(new FileModel() { Name = file.Name, Date = file.CreationTime, Size = file.Length, Type = file.Extension, FullName = file.FullName });

                    return new DirectoryModel() { Name = path, Files = files };
                }
            }
            catch (UnauthorizedAccessException) { }
             
            return new DirectoryModel() { Name = path, Files = files};
        }

        /// <summary>
        /// This method create list of drives.
        /// </summary>
        /// <returns>List of DriveModel</returns>
        private List<DriveModel> GetDrives()
        {
            List<DriveModel> drivesList = new List<DriveModel>();
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (var drive in drives)
            {
                drivesList.Add(new DriveModel() { Name = drive.Name, Type = drive.DriveType.ToString()});
            }
            return drivesList;
        }




    }
}