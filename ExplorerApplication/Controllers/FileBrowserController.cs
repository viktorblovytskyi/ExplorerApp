using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.IO;
using ExplorerApplication.Models;

namespace ExplorerApplication.Controllers
{
    public class FileBrowserController : Controller
    {

        /// <summary>
        /// This method displays all files and directorys. 
        /// </summary>
        /// <param name="path">Optional parameter directory's path</param>
        /// <returns>View with model and view's name</returns>
        public ActionResult Index(string path)
        {
            List<DriveModel> drives = this.GetDrives();
            var fixedDrives = from drive in drives
                              where drive.Type == "Fixed"
                              select drive;

            if (String.IsNullOrEmpty(path))
                path = fixedDrives.First().Name;
            
            ViewBag.path = path;
            DirectoryModel model = this.InitialDirectory(path); 
            return View("Index", model);
        }

        /// <summary>
        /// This method creates directory in path.
        /// </summary>
        /// <param name="path">Path for file</param>
        /// <param name="name">Name of new file</param>
        /// <param name="type"></param>
        /// <returns>View with model and view's name</returns>
        public ActionResult Create(string path, string name, string type)
        {
            if (!String.IsNullOrEmpty(name) && !String.IsNullOrEmpty(path))
            {                                   
                if(type == "directory")
                    this.CreateDirectory(path, name);
                if (type == "file")
                    this.CreateFile(path, name);               
            }                
            else
                ViewBag.Massage = "Name or path is empty or null!";

            ViewBag.path = path;
            DirectoryModel model = this.InitialDirectory(path);
            return View("Index", model);
        }      

        /// <summary>
        /// This method get all directorys and files in path and create DirectoryModel's object.
        /// </summary>
        /// <param name="path">String with directory's path.</param>
        /// <returns>DirectoryModel's object or null if path is incorrect</returns>
        private DirectoryModel InitialDirectory (string path)
        {
            List<FileModel> files = new List<FileModel>();
            List<DriveModel> drives = this.GetDrives();
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
            catch (UnauthorizedAccessException e ) { ViewBag.Massage = e.Message; }
             
            return null;
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

        /// <summary>
        /// This method creates file.
        /// </summary>
        /// <param name="path">Path for file</param>
        /// <param name="name">Name of new file</param>
        private void CreateFile(string path, string name)
        {
            if (!System.IO.File.Exists(Path.Combine(path, name)))
            {
                System.IO.File.Create(Path.Combine(path, name));
            }
            else
            {
                ViewBag.Massage = "File " + name + " alredy exists.";               
            }
        }

        /// <summary>
        /// This creates directory.
        /// </summary>
        /// <param name="path">Path for file</param>
        /// <param name="name">Name of new file</param>      
        private void CreateDirectory(string path, string name)
        {
            try
            {
                Directory.CreateDirectory(Path.Combine(path, name));
            }
            catch (IOException e) { ViewBag.Massage = e.Message; }
            catch (Exception e) { ViewBag.Massage = e.Message; }
        }
    }
}