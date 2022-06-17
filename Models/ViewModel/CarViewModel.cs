using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Models.ViewModel
{
    public class CarViewModel
    {
        public int ID { get; set; }

        [Display(Name = "نام ماشین")]
        [Required(ErrorMessage = "وارد نمودن {0}  اجباری است")]
        public string Name { get; set; }
        public DateTime CreateDate { get; set; }
        [Display(Name = "قیمت")]

        [Required(ErrorMessage = "وارد نمودن {0}  اجباری است")]
        public string Price { get; set; }

        public int ColorID { get; set; }
        public int ModelID { get; set; }
        [Display(Name = "دسته بندی")]

        [Required(ErrorMessage = "وارد نمودن {0}  اجباری است")]
        public int CategoryID { get; set; }
        [Display(Name = "برند")]

        [Required(ErrorMessage = "وارد نمودن {0}  اجباری است")]
        public int BrandId { get; set; }

        [Display(Name = "توضیحات")]
        public string Description { get; set; }

        public SelectList ColorList { get; set; }
        public SelectList CategoryList { get; set; }
        public SelectList ModelList { get; set; }
        public SelectList BrandList { get; set; }

        public string ColorName { get; set; }

        public string CategoryName { get; set; }
        public string ModelName { get; set; }
        
        public string BrandName { get; set; }
        public List<string> Imagelist { get; set; }

        public IFormFileCollection ImageCar { get; set; }
        public IFormFile File { get; set; }
        public string ImageUrl { get; set; }


        public class CarVm
        {
            public CarViewModel Car { get; set; }
            public bool IsAccept { get; set; }
        }


    }
}
