using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TruongVanDat_Tuan3.Models;

namespace TruongVanDat_Tuan3.Controllers
{
    public class SachController : Controller
    {
        MyDataDataContext data = new MyDataDataContext();

        // GET: Sach
        public ActionResult Index()
        {
            var books = data.Saches.ToList();
            return View(books);
        }

        //-------------Create-------------------
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection, Sach sach)
        {
            var ten = collection["tenSach"];
            var maLoai = collection["maLoai"];
            var hinh = collection["hinh"];
            if (string.IsNullOrEmpty(ten))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                sach.tensach = ten;
                sach.masach = Int32.Parse(maLoai);
                sach.hinh = hinh;
                data.Saches.InsertOnSubmit(sach);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Create();
        }
        //-------------Details-------------------
        public ActionResult Detail(int id)
        {
            var sach = data.Saches.First(m => m.masach == id);
            return View(sach);
        }
        //-------------Edit-------------------
        public ActionResult Edit(int id)
        {
            var sach = data.Saches.First(m => m.masach == id);
            return View(sach);
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var sach = data.Saches.First(m => m.masach == id);
            var E_tensach = collection["tenSach"];
            var E_maloai = collection["maLoai"];
            var E_hinh = collection["hinh"];
            sach.maloai = id;
            if (string.IsNullOrEmpty(E_tensach))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                sach.tensach = E_tensach;
                sach.masach = Int32.Parse(E_maloai);
                sach.hinh = E_hinh;
                UpdateModel(sach);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Edit(id);
        }
        //-------------Delete-------------------
        public ActionResult Delete(int id)
        {
            var D_Sach = data.Saches.First(m => m.masach == id);
            return View(D_Sach);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var D_sach = data.Saches.Where(m => m.masach == id).First();
            data.Saches.DeleteOnSubmit(D_sach);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }
    }
}