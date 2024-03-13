using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TruongVanDat_Tuan4.Models;

namespace TruongVanDat_Tuan4.Controllers
{
    public class HomeController : Controller
    {
        MyDataDataContext data = new MyDataDataContext();

        public ActionResult Index(int? page)
        {
            if (page == null)
            {
                page = 1;
            }
            // Default page size is 2 books
            int pageSize = 2;
            int pageNum = page ?? 1;
            var all_sach = from sach in data.Saches select sach;
            return View(all_sach.ToPagedList(pageNum, pageSize));
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
    }
}