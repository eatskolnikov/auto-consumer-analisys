﻿using System;
using System.Drawing;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.IO.Compression;
using ACAPackagesListener.API.Models.Entities;
using ACAPackagesListener.API.Models.Repositories;
using ACAPackagesListener.API;
namespace acaweb.Controllers
{
    public class MapController : Controller
    {
        private IMapRepository _mapRepository;

        public MapController()
        {
            _mapRepository = new NHMapRepository();
        }

        public ActionResult Index()
        {
            var maps = _mapRepository.GetAll();

            return View(maps);
        }

        public JsonResult Select(int id)
        {
            var map = _mapRepository.GetById(id);
            if (map == null)
            {
                return Json(new { success = false, messages = new string[] { "El mapa seleccionado no existe" } }, JsonRequestBehavior.AllowGet);
            }
            _mapRepository.ChangeSelected(id);
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Delete(int id)
        {
            var map = _mapRepository.GetById(id);
            if (map.Selected)
            {
                return Json(new { success = false, messages=new string[]{"No puedes eliminar el mapa actualmente seleccionado"} }, JsonRequestBehavior.AllowGet);
            }
            _mapRepository.Remove(map);
            return Json(new{success=true}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Add() { return View(); }

        [HttpPost]
        public ActionResult Add(MallMap map, HttpPostedFileBase mapfile)
        {
            try
            {
                var newFolderName = Utils.GetMd5Hash(mapfile.FileName + DateTime.Now);
                var filename = Path.GetFileName(mapfile.FileName);
                var path = Server.MapPath("~/Content/maps/" + newFolderName);
                var file = Path.Combine(path, filename);
                var myDirectoryInfo = Directory.CreateDirectory(path);
                var self = WindowsIdentity.GetCurrent();
                var ds = myDirectoryInfo.GetAccessControl();
                ds.AddAccessRule(new FileSystemAccessRule(self.Name, FileSystemRights.FullControl,
                    InheritanceFlags.ObjectInherit | InheritanceFlags.ContainerInherit, PropagationFlags.None,
                    AccessControlType.Allow));
                myDirectoryInfo.SetAccessControl(ds);
                mapfile.SaveAs(file);
                ZipFile.ExtractToDirectory(file, path);
                var fi = new FileInfo(file);
                fi.Delete();
                var currentFloor = 1;
                var sideSize = 256;
                var files = Directory.GetFiles(path);
                foreach (var f in files)
                {
                    var imagePath = path + "\\" + currentFloor + "\\";
                    Directory.CreateDirectory(imagePath);
                    var bmp = new Bitmap(f);

                    for (var currentRow = 0; currentRow < 3; ++currentRow)
                    {
                        for (var currentColumn = 0; currentColumn < 4; ++currentColumn)
                        {
                            var newBmp = new Bitmap(sideSize, sideSize);
                            for (var i = 0; i < 256; ++i)
                            {
                                for (var j = 0; j < 256; ++j)
                                {
                                    newBmp.SetPixel(j, i,
                                        bmp.GetPixel((currentColumn * sideSize) + j, (currentRow * sideSize) + i));
                                }
                            }
                            Directory.CreateDirectory(imagePath + currentRow);
                            newBmp.Save(imagePath + currentRow + "\\" + currentColumn + ".png");
                        }
                    }
                    ++currentFloor;
                }
                map.FloorsCount = files.Length;
                map.TilesSource = "Content/maps/" + newFolderName + "/";
                _mapRepository.Add(map);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }
            return RedirectToAction("Index");
        }
    }
}
