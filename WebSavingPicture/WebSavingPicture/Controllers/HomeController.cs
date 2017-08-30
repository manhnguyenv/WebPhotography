using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSavingPicture.Models;
using System.Data.Entity;

namespace WebSavingPicture.Controllers
{
    public class HomeController : Controller
    {
        private PictureDbContext db = new PictureDbContext();
        public ActionResult Index()
        {
            return View(db.Pictures.ToList());
        }

        public FileContentResult ReadPicture(int? Id)
        {
            if (Id == null)
                return null;

            Picture p = db.Pictures.Find(Id);
            if (p == null)
                return null;

            return File(p.PictureBin, p.PictureType);
        }

        public ActionResult CreatePicture()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePicture(Picture p, HttpPostedFileBase file)
        {
            if (file == null)
            {
                ModelState.AddModelError("fileError", "file not chosen");
            }

            if (ModelState.IsValid)
            {
                p.PictureBin = new byte[file.ContentLength];
                p.PictureType = file.ContentType;
                System.IO.Stream sw = file.InputStream;
                sw.Read(p.PictureBin, 0, file.ContentLength);
                try
                {
                    db.Entry(p).State = EntityState.Added;
                    db.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception xcp)
                {
                    db.Entry(p).State = EntityState.Detached;
                    ViewBag.err = xcp.Message;
                    return View("Error");
                }
            }

            return View(p);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            Picture p = db.Pictures.Find(id);
            if(p==null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);

            return View(p);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Picture p,HttpPostedFileBase file)
        {
            if (file == null)
            {
                ModelState.AddModelError("fileError", "file not chosen");
            }

            if (ModelState.IsValid)
            {
                p.PictureBin = new byte[file.ContentLength];
                p.PictureType = file.ContentType;
                System.IO.Stream sw = file.InputStream;
                sw.Read(p.PictureBin, 0, file.ContentLength);
                try
                {
                    db.Entry(p).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception xcp)
                {
                    db.Entry(p).State = EntityState.Unchanged;
                    ViewBag.err = xcp.Message;
                    return View("Error");
                }
            }

            return View(p);
        }


        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            Picture p = db.Pictures.Find(id);
            if (p == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);

            return View(p);
        }

        public ActionResult Delete(int?id)
        {
            if (id == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            Picture p = db.Pictures.Find(id);
            if (p == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);

            return View(p);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id=0)
        {          
            Picture p = db.Pictures.Find(id);
            if (p == null)
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.NotFound);
         
            try
            {
                db.Entry(p).State = EntityState.Deleted;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            catch (Exception xcp)
            {
                db.Entry(p).State = EntityState.Unchanged;
                ViewBag.err = xcp.Message;
                return View("Error");
            }
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                db.Dispose();

            base.Dispose(disposing);
        }
    }
}