using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShopping.Models.ViewModel
{
    public class ColorViewModel
    {

        public int ID { get; set; }

        [Display(Name = "نام رنگ")]
        [Required(ErrorMessage = "وارد نمودن {0}  اجباری است")]
        public string ColorName { get; set; }
        public string ColorNumber { get; set; }

        public class ColorVM
        {
            public ColorViewModel Color { get; set; }
            public bool IsAccept { get; set; }
        }
    }
}
