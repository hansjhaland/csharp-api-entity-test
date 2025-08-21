﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace workshop.wwwapi.Models
{
    //TODO: decorate class/columns accordingly
    [Table("appointments")]
    public class Appointment
    {
        [Key]
        public int AppointmentId { get; set; }
        [Column("doctor_id")]
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        [Column("patient_id")]
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        [Column("booking_date")]
        public DateTime Booking { get; set; }

    }
}
