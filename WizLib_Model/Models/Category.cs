using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WizLib_Model.Models
{
   public class Category
    {
        [Key]
        public int Category_Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }
    }
}
