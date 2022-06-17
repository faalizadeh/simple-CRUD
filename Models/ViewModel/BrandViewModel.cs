using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Models.ViewModel
{
    public class BrandViewModel
    {
        public int ID { get; set; }

        [Display(Name = "نام برند")]
        [Required(ErrorMessage = "وارد نمودن {0}  اجباری است")]
        public string BrandName { get; set; }
        public string ImageUrl { get; set; }

        public IFormFileCollection formFiles { get; set; }
        public IFormFile File { get; set; }

        

        public class BrandVm
        {
            public BrandViewModel brand { get; set; }
            public bool IsAccept { get; set; }
        }

        public class BrandFile
        {
            public BrandViewModel brand { get; set; }
            public bool Isexist { get; set; }
        }
    }
}
