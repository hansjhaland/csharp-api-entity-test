using Microsoft.EntityFrameworkCore;
using workshop.wwwapi.Data;
using workshop.wwwapi.DTOs;
using workshop.wwwapi.Models;

namespace workshop.wwwapi.Repository
{
    public class Repository : IRepository
    {
        private DatabaseContext _databaseContext;
        public Repository(DatabaseContext db)
        {
            _databaseContext = db;
        }
        public async Task<IEnumerable<Patient>> GetPatients()
        {
            return await _databaseContext.Patients.Include(p => p.Appointments).ThenInclude(a => a.Doctor).ToListAsync();
        }
        public async Task<IEnumerable<Doctor>> GetDoctors()
        {
            return await _databaseContext.Doctors.Include(d => d.Appointments).ThenInclude(a => a.Patient).ToListAsync();
        }
        public async Task<IEnumerable<Appointment>> GetAppointmentsByDoctor(int id)
        {
            return await _databaseContext.Appointments.Where(a => a.DoctorId==id).ToListAsync();
        }

        public async Task<Patient> GetPatient(int id)
        {
            return await _databaseContext.Patients.Where(p => p.Id == id).Include(p => p.Appointments).ThenInclude(a => a.Doctor).FirstOrDefaultAsync();
        }

        public async Task<Patient> CreatePatient(PatientPost patient)
        {
            var newPatient = new Patient() { FullName = patient.FullName };
            await _databaseContext.Patients.AddAsync(newPatient);
            _databaseContext.SaveChanges();
            return newPatient;
        }

        public async Task<Doctor> GetDoctor(int id)
        {
            return await _databaseContext.Doctors.Where(d => d.Id == id).Include(d => d.Appointments).ThenInclude(a => a.Patient).FirstOrDefaultAsync();
        }

        public async Task<Doctor> CreateDoctor(DoctorPost doctor)
        {
            var newDoctor = new Doctor() { FullName = doctor.FullName };
            await _databaseContext.Doctors.AddAsync(newDoctor);
            _databaseContext.SaveChanges();
            return newDoctor;
        }

        public async Task<IEnumerable<Appointment>> GetAppointments()
        {
            return await _databaseContext.Appointments.Include(a => a.Doctor).Include(a => a.Patient).ToListAsync();
        }
    }
}
