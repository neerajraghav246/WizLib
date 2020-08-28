using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WizLib_Model.Models
{
   public class Genre
    {
        public int GenreId { get; set; }

        [StringLength(100)]
        public string GenreName { get; set; }
        //public int DisplayOrder { get; set; }
    }
}
