using workshop.wwwapi.DTOs;
using workshop.wwwapi.Models;

namespace workshop.wwwapi.Repository
{
    public interface IRepository
    {
        Task<IEnumerable<Patient>> GetPatients();
        Task<IEnumerable<Doctor>> GetDoctors();
        Task<Doctor> GetDoctor(int id);
        Task<IEnumerable<Appointment>> GetAppointmentsByDoctor(int id);
        Task<Patient> GetPatient(int id);
        Task<Patient> CreatePatient(PatientPost patient);
    }
}
