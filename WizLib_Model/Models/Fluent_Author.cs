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

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string Address { get; set; }

        public string FullName { get
            {
                return $"{FirstName} {LastName}";
            }
        }

        public ICollection<Fluent_BookAuthor> Fluent_BookAuthors { get; set; }

    }
}
