using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TruongVanDat_Tuan2.Models;

namespace TruongVanDat_Tuan2.Controllers
{
    public class BooksController : Controller
    {
        private List<Book> listBooks;
        public BooksController()
        {
            listBooks = new List<Book>();
            listBooks.Add(new Book()
            {
                Id = 1,
                Title = "Đi tìm lẽ sống",
                Author = "Không biết",
                PublicYear = 2000,
                Price = 500000,
                Cover = "Content/images/book1.jpg"
            });
            listBooks.Add(new Book()
            {
                Id = 2,
                Title = "7 Thói quen hiệu quả",
                Author = "Không biết",
                PublicYear = 2010,
                Price = 700000,
                Cover = "Content/images/book2.jpg"
            });
            listBooks.Add(new Book()
            {
                Id = 3,
                Title = "Hạt giống tâm hồn",
                Author = "Quên rồi",
                PublicYear = 1999,
                Price = 900000,
                Cover = "Content/images/book3.jpg"
            });
            listBooks.Add(new Book()
            {
                Id = 4,
                Title = "Từ thất bại đến thành công",
                Author = "Không biết",
                PublicYear = 2020,
                Price = 300000,
                Cover = "Content/images/book4.jpg"
            });
        }

        // GET: Books
        public ActionResult ListBooks()
        {
            ViewBag.TitlePageName = "HUTECH BOOKS";
            return View(listBooks);
        }

        // GET: Detail of a book
        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Book book = listBooks.Find(x => x.Id == id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        //Bước 1: hiển thị chi tiết quyển sách cần sửa
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Book book = listBooks.Find(x => x.Id == id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //Bước 2: Chỉnh sửa thông tin chi tiết của quyển sách
        public ActionResult Edit(Book book)
        {
            if (ModelState.IsValid)//nếu dữ liệu người dùng nhập không bị lỗi
            {
                try
                {
                    var editBook = listBooks.Find(b => b.Id == book.Id);
                    editBook.Title = book.Title;
                    editBook.Author = book.Author;
                    editBook.Cover = book.Cover;
                    editBook.Price = book.Price;
                    editBook.PublicYear = book.PublicYear;
                    if (book.CoverFile != null)
                    {
                        editBook.Cover = "Content/images/" + book.CoverFile.FileName;
                    }
                    return View("ListBooks", listBooks);
                }
                catch (Exception ex)
                {
                    return HttpNotFound();
                }
            }
            else//nếu dữ liệu người dùng nhập bị lỗi
            {
                ModelState.AddModelError("", "Input Model Not Valide!");
                return View(book);
            }

        }

        // Delete a book
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Book book = listBooks.Find(x => x.Id == id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Book book)
        {

            try
            {
                listBooks.Remove(listBooks.Find(item => item.Id == book.Id));
                return View("ListBooks", listBooks);
            }
            catch (Exception ex)
            {
                return HttpNotFound();
            }
        }

        public ActionResult Add()
        {
            ViewBag.TitlePageName = "ADD A BOOK";
            return View();
        }

        [HttpPost]
        public ActionResult Add(Book book)
        {
            try
            {
                book.Id = listBooks.Count + 1;
                book.Cover = "Content/images/" + book.CoverFile.FileName;
                listBooks.Add(book);
                return View("ListBooks", listBooks);
            }
            catch (Exception ex)
            {
                return HttpNotFound();
            }
        }
    }
}