using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;

namespace TruongVanDat_Tuan2.Models
{
    // Declare structure of a book
    public class Book
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Phải nhập tên sách")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Phải nhập tên tác giả")]
        [StringLength(20, ErrorMessage = "Chiều dài tối đa 20 ký tự")]
        public string Author { get; set; }
        [Range(1900, 2024, ErrorMessage = "Năm phải nằm trong khoảng từ 1900 đến hiện tại")]
        public int PublicYear { get; set; }
        public double Price { get; set; }
        public string Cover { get; set; }

        [Display(Name = "Upload image")]
        [Required(ErrorMessage = "File image is required")]
        public HttpPostedFileBase CoverFile { get; set; }
    }
}