using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nhom09_Chieu4_BaiTapTuan3.Models;

namespace Nhom09_Chieu4_BaiTapTuan3.Controllers
{
    public class TheLoaiController : Controller
    {

        MyDataDataContext data = new MyDataDataContext();
        // GET: TheLoai
        public ActionResult Index(string searchTerm)
        {
            // Truy vấn lấy thông tin tất cả các thẻ loại
            var all_theloai = from tl in data.TheLoais select tl;
            if (!string.IsNullOrEmpty(searchTerm))
            {
                all_theloai = all_theloai.Where(tl => tl.tenloai.ToLower().Contains(searchTerm.ToLower()));
            }
            ViewBag.searchTerm = searchTerm;
            return View(all_theloai);

        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            var detail_theloai = data.TheLoais.Where(d => d.maloai == id).First();
            return View(detail_theloai);
        }
        public ActionResult Create()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection, TheLoai tl)
        {
            var ten = collection["tenloai"];
            if (string.IsNullOrEmpty(ten))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                tl.tenloai = ten;
                data.TheLoais.InsertOnSubmit(tl);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Create();
        }
        //-------------Edit-------------------
        public ActionResult Edit(int id)
        {
            var E_category = data.TheLoais.First(m => m.maloai == id);
            return View(E_category);
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var theloai = data.TheLoais.First(m => m.maloai == id);
            var E_tenloai = collection["tenloai"];
            theloai.maloai = id;
            if (string.IsNullOrEmpty(E_tenloai))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                theloai.tenloai = E_tenloai;
                UpdateModel(theloai);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Edit(id);
        }
        //-------------Delete-------------------
        public ActionResult Delete(int id)
        {
            var D_theloai = data.TheLoais.First(m => m.maloai == id);
            return View(D_theloai);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var D_theloai = data.TheLoais.Where(m => m.maloai == id).First();
            data.TheLoais.DeleteOnSubmit(D_theloai);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }
    }
}