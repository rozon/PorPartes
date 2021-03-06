﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElCarro.Web.Models
{
    [Table("Vehicle")]
    public class Vehicle
    {
        public Vehicle()
        {
        }

        public int ID { get; set; }

        [Required]
        [MaxLength(50)]
        public virtual Model Model { get; set; }

        [Required]
        public DateTime Year { get; set; }
    }
}
