using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace WizLib_Model.Models
{
    public class Fluent_Publisher
    {
        public int Publisher_Id { get; set; }

        //[Required]
        //[MaxLength(100)]
        public string Name { get; set; }

        //[Required]
        //[MaxLength(250)]
        public string Location { get; set; }

        //public List<Book> Books { get; set; }
    }
}
