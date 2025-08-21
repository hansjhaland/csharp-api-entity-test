using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Runtime.CompilerServices;
using workshop.wwwapi.DTOs;
using workshop.wwwapi.Repository;

namespace workshop.wwwapi.Endpoints
{
    public static class SurgeryEndpoint
    {
        //TODO:  add additional endpoints in here according to the requirements in the README.md 
        public static void ConfigurePatientEndpoint(this WebApplication app)
        {
            var surgeryGroup = app.MapGroup("surgery");

            surgeryGroup.MapGet("/patients", GetPatients);
            surgeryGroup.MapGet("/patients/{id}", GetPatient);
            surgeryGroup.MapPost("/patients", CreatePatient);
            surgeryGroup.MapGet("/doctors", GetDoctors);
            surgeryGroup.MapGet("/doctors/{id}", GetDoctor);
            surgeryGroup.MapPost("/doctors", CreateDoctor);
            surgeryGroup.MapGet("/appointmentsbydoctor/{id}", GetAppointmentsByDoctor);
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetPatients(IRepository repository)
        {
            var response = await repository.GetPatients();
            List<Object> result = new List<Object>();
            foreach (var entity in response)
            {
                result.Add(new PatientGet() { FullName = entity.FullName });
            }
            return TypedResults.Ok(result);
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetPatient(IRepository repository, int id)
        {
            var entity = await repository.GetPatient(id);
            if (entity is null) return TypedResults.NotFound();
            var result = new PatientGet() { FullName = entity.FullName };
            return TypedResults.Ok(result);
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> CreatePatient(IRepository repository, PatientPost patient) 
        { 
            if (patient.FullName == "") return TypedResults.BadRequest();
            var entity = repository.CreatePatient(patient);
            return TypedResults.Created();
            
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetDoctors(IRepository repository)
        {
            var response = await repository.GetDoctors();
            List<Object> result = new List<Object>();
            foreach (var entity in response)
            {
                var doctor = new DoctorGet() { FullName = entity.FullName };
                foreach (var appointment in entity.Appointments)
                {
                    doctor.Appointments.Add(new DoctorAppointmentGet() { 
                        PatientId = appointment.PatientId, 
                        PatientName = appointment.Patient.FullName, 
                        AppointmentDate = appointment.AppointmentDate 
                    });
                }
                result.Add(doctor);
            }
            return TypedResults.Ok(result);
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public static async Task<IResult> GetDoctor(IRepository repository, int id)
        {
            var entity = await repository.GetDoctor(id);
            if (entity is null) return TypedResults.NotFound();


            var doctor = new DoctorGet() { FullName = entity.FullName };
            foreach (var appointment in entity.Appointments)
            {
                doctor.Appointments.Add(new DoctorAppointmentGet()
                {
                    PatientId = appointment.PatientId,
                    PatientName = appointment.Patient.FullName,
                    AppointmentDate = appointment.AppointmentDate
                });
            }
            return TypedResults.Ok(doctor);
        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public static async Task<IResult> CreateDoctor(IRepository repository, DoctorPost doctor)
        {
            if (doctor.FullName == "") return TypedResults.BadRequest();
            var entity = repository.CreateDoctor(doctor);
            return TypedResults.Created();

        }
        [ProducesResponseType(StatusCodes.Status200OK)]
        public static async Task<IResult> GetAppointmentsByDoctor(IRepository repository, int id)
        {
            return TypedResults.Ok(await repository.GetAppointmentsByDoctor(id));
        }
    }
}
