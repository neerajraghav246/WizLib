using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace WizLib_Model.Models
{
    public class Fluent_Author
    {
        public int Author_Id { get; set; }

        //[MaxLength(50)]
        //[Required]
        public string FirstName { get; set; }

        //[MaxLength(50)]
        //[Required]
        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        //[MaxLength(250)]
        public string Address { get; set; }

        //[NotMapped]
        public string FullName { get
            {
                return $"{FirstName} {LastName}";
            }
        }

        //public ICollection<BookAuthor> BookAuthors { get; set; }
    }
}
