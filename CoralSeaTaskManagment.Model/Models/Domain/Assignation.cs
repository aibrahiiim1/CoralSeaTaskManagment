﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CoralSeaTaskManagment.Model.Models.Domain
{
    public class Assignation
    {
        
        public int Id { get; set; }
        public int AssignId { get; set; }
    
        public int OrderId { get; set; }

        public int HotelId { get; set; }

        public string? TechName { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public string? ApplicationUserId { get; set; }
        //[ValidateNever]
        //public ApplicationUser? ApplicationUser { get; set; }


        // Navigation Properties
        public Order? Orders { get; set; }
        public Hotel? Hotels { get; set; }
        public Assign? Assigns { get; set; }
    }
}