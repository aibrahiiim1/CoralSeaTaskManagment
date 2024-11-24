using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CoralSeaTaskManagment.Model.Models.Domain
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName =>FirstName + LastName;
        public string Username { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Role { get; set; }
        public int DepartmentId { get; set; }
        [ValidateNever]
        public Department? Departments { get; set; }
        public int HotelId { get; set; }
        [ValidateNever]
        public Hotel? Hotels { get; set; }
        public byte[]? Pic { get; set; }

        [JsonIgnore]
        public string PasswordHash { get; set; }
    }
}
