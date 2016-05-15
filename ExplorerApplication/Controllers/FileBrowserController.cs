using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
        /// <param name="path">Directory's path</param>
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
        /// <param name="type"></param>
        /// <param name="name">Name of new file</param>
        /// <returns>View with model and view's name</returns>
        public ActionResult Create(string path, string name)
        {
            if (!String.IsNullOrEmpty(name) && !String.IsNullOrEmpty(path))
            {                
                try
                {
                    //Directory.CreateDirectory(path +@"\"+name);
                    Directory.CreateDirectory(System.IO.Path.Combine(path, name));
                    ViewBag.path = path;
                    DirectoryModel model = this.InitialDirectory(path);
                    return View("Index", model);
                }
                catch (IOException e) { ViewBag.Massage = e.Message; }
                catch(Exception e) { ViewBag.Massage = e.Message; }                    
                    
            }                
            else
                ViewBag.Massage = "Name or path is empty or null!";
            return View();
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
    }
}