﻿using System.ComponentModel.DataAnnotations.Schema;

namespace CashSystemMVC.Models
{
    [Table("Identity")]
    public class Identity
    {
        public Identity(string userName,
            string firstName,
            string lastName,
            string postAddress,
            string postcode,
            string mobile,
            string email)
        {
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            PostAddress = postAddress;
            Postcode = postcode;
            Mobile = mobile;
            Email = email;
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PostAddress { get; set; }
        public string Postcode { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }

        public void Update(string firstName, string lastName, string address, string postcode, string mobile,
            string email)
        {
            FirstName = firstName;
            LastName = lastName;
            PostAddress = address;
            Postcode = postcode;
            Mobile = mobile;
            Email = email;
        }
    }
}