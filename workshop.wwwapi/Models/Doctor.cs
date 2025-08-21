﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace workshop.wwwapi.Models
{
    //TODO: decorate class/columns accordingly    
    [Table("doctors")]
    public class Doctor
    {
        [Key]
        public int Id { get; set; }
        [Column("doctor_name")]
        public string FullName { get; set; }
        public ICollection<Appointment> Appointments { get; set; }
    }
}
