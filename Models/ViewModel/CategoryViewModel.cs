using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Models.ViewModel
{
    public class CategoryViewModel
    {
        public int ID { get; set; }
        [Display(Name="نام دسته بندی")]
        [Required(ErrorMessage = "وارد نمودن {0}  اجباری است")]
        public string CategoryName { get; set; }
        public IFormFileCollection formFiles { get; set; }
        public IFormFile File { get; set; }

        public string ImageUrl { get; set; }

        public class CategoryVm
        {
            public CategoryViewModel Category { get; set; }

            public bool IsAccept { get; set; }
        }

        public class CategoryFilename
        {
            public string filename { get; set; }
            public string url { get; set; }
            public bool FileExists { get; set; }
        }
    }
}
