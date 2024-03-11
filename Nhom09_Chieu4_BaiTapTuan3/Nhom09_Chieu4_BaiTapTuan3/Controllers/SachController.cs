using Nhom09_Chieu4_BaiTapTuan3.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace Nhom09_Chieu4_BaiTapTuan3.Controllers
{
    public class SachController : Controller
    {
        MyDataDataContext data = new MyDataDataContext();
        // GET: Sach
        public ActionResult Index(string searchTerm)
        {
            var all_sach = from s in data.Saches select s;
            if (!string.IsNullOrEmpty(searchTerm))
            {
                all_sach = all_sach.Where(s => s.tensach.ToLower().Contains(searchTerm.ToLower()));
            }

            if (all_sach == null)
            {
                return RedirectToAction("Error");
            }
            ViewBag.searchTerm = searchTerm;
            return View(all_sach);
        }

        private object RemoveDiacritics(string searchTerm)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var detail_s = data.Saches.Where(d => d.masach == id).First();
            return View(detail_s);
        }
        public ActionResult Create()
        {
            ViewBag.categories = data.TheLoais.ToList();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FormCollection collection, Sach s)
        {
            var ten = collection["tensach"];
            var hinhanh = collection["hinh"];
            if (string.IsNullOrEmpty(ten))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                s.tensach = ten;
                s.hinh = hinhanh;
                data.Saches.InsertOnSubmit(s);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View(s);
        }
        public ActionResult Edit(int id)
        {
            var E_category = data.Saches.First(m => m.masach == id);
            return View(E_category);
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var sach = data.Saches.First(m => m.masach == id);
            var E_tensach = collection["tensach"];
            sach.masach = id;
            var E_hinh = collection["hinh"];
            if (string.IsNullOrEmpty(E_tensach))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                sach.tensach = E_tensach;
                sach.hinh = "/Content/images/" + E_hinh;
                UpdateModel(E_tensach);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Edit(id);
        }
        public ActionResult Delete(int id)
        {
            var D_sach = data.Saches.First(m => m.masach == id);
            return View(D_sach);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var D_sach = data.Saches.Where(m => m.masach == id).First();
            data.Saches.DeleteOnSubmit(D_sach);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }
        static string RemovePunctuationAndDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark && !char.IsPunctuation(c))
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }
    }
}