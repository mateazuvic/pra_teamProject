using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kwizzotronic.Models
{
    public class Creator
    {
        public int? IDCreator { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public Creator(string firstName, string lastName, string email, string password)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.Password = password;
        }

        public Creator(int idCreator, string firstName, string lastName, string email, string password)
        {
            this.IDCreator = idCreator;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Email = email;
            this.Password = password;
        }

    }
}