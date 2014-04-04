using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.IO.Compression;
using ACAPackagesListener.API.Models.Entities;
using ACAPackagesListener.API.Models.Repositories;
using ACAPackagesListener.API;
using NHibernate.Hql.Ast.ANTLR;

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
        
        public ActionResult Add() { return View(); }

        [HttpPost]
        public ActionResult Add(Map map, HttpPostedFileBase mapfile)
        {
            try
            {
                var newFolderName = Utils.GetMd5Hash(mapfile.FileName + DateTime.Now);
                var filename = Path.GetFileName(mapfile.FileName);
                var path = Server.MapPath("~/Content/maps/" + newFolderName);
                var file = Path.Combine(path, filename);
                Directory.CreateDirectory(path);
                mapfile.SaveAs(path);
                var currentFloor = 1;
                var sidelength = 256;
                foreach(var f in Directory.GetFiles(path))
                {
                    var imagePath = path + "/" + currentFloor;
                    Directory.CreateDirectory(imagePath);

                    for(var currentRow=0;currentRow < 4;++currentRow)
                    {
                        for (var currentColumn = 0; currentColumn < 4; ++currentColumn)
                        {
                            var bmp = new Bitmap(f);
                            var rect = new Rectangle(0, 0, bmp.Width, bmp.Height);
                            var rawOriginal = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

                            int origByteCount = rawOriginal.Stride * rawOriginal.Height;
                            byte[] origBytes = new Byte[origByteCount];
                            Marshal.Copy(rawOriginal.Scan0, origBytes, 0, origByteCount);

                            //I want to crop a 100x100 section starting at 15, 15.
                            var startX = 0 + (256 * currentColumn);
                            var startY = 0 + (256 * currentRow);
                            var width = 256;
                            var height = 256;
                            var BPP = 4;        //4 Bpp = 32 bits, 3 = 24, etc.

                            byte[] croppedBytes = new Byte[width * height * BPP];

                            //Iterate the selected area of the original image, and the full area of the new image
                            for (var i = 0; i < height; i++)
                            {
                                for (var j = 0; j < width * BPP; j += BPP)
                                {
                                    int origIndex = (startX * rawOriginal.Stride) + (i * rawOriginal.Stride) + (startY * BPP) + (j);
                                    int croppedIndex = (i * width * BPP) + (j);

                                    //copy data: once for each channel
                                    for (int k = 0; k < BPP; k++)
                                    {
                                        croppedBytes[croppedIndex + k] = origBytes[origIndex + k];
                                    }
                                }
                            }

                            //copy new data into a bitmap
                            var croppedBitmap = new Bitmap(width, height);
                            var croppedData = croppedBitmap.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
                            Marshal.Copy(croppedBytes, 0, croppedData.Scan0, croppedBytes.Length);

                            bmp.UnlockBits(rawOriginal);
                            croppedBitmap.UnlockBits(croppedData);

                            croppedBitmap.Save(imagePath + (currentRow+currentColumn));
                        }
                    }
                    ++currentFloor;
                }

                map.TilesSource = Url.Content("~/Content/maps/" + newFolderName);

                ZipFile.ExtractToDirectory(file, path);
 //              _mapRepository.Add(map);
            }
            catch (Exception ex) { ModelState.AddModelError("", ex.Message); }
            return RedirectToAction("Index");
        }
    }
}
