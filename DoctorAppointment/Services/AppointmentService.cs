using DoctorAppointment.Models;
using DoctorAppointment.Models.ViewModels;
using DoctorAppointment.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorAppointment.Services
{
    public class AppointmentService : IAppointmentService
    {
        private readonly ApplicationDBContext _db;
        public AppointmentService(ApplicationDBContext db)
        {
            _db = db;
        }

        public async Task<int> AddUpdate(AppointmentVM model)
        {
            var startDate = DateTime.Parse(model.StartDate);
            var endDate = DateTime.Parse(model.StartDate).AddMinutes(Convert.ToDouble(model.Duration));
            var patient = _db.Users.FirstOrDefault(u => u.Id == model.PatientId);
            var doctor = _db.Users.FirstOrDefault(u => u.Id == model.DoctorId);
            if (model != null && model.Id > 0)
            {
                //update
                var appointment = _db.Appointments.FirstOrDefault(x => x.Id == model.Id);
                appointment.Title = model.Title;
                appointment.Description = model.Description;
                appointment.StartDate = startDate;
                appointment.EndDate = endDate;
                appointment.Duration = model.Duration;
                appointment.DoctorId = model.DoctorId;
                appointment.PatientId = model.PatientId;
                appointment.IsDoctorApproved = false;
                appointment.AdminId = model.AdminId;
                await _db.SaveChangesAsync();
                return 1;
            }
            else
            {
                //create
                Appointment appointment = new Appointment()
                {
                    Title = model.Title,
                    Description = model.Description,
                    StartDate = startDate,
                    EndDate = endDate,
                    Duration = model.Duration,
                    DoctorId = model.DoctorId,
                    PatientId = model.PatientId,
                    IsDoctorApproved = false,
                    AdminId = model.AdminId
                };
                /*await _emailSender.SendEmailAsync(doctor.Email, "Appointment Created",
                    $"Your appointment with {patient.Name} is created and in pending status");
                await _emailSender.SendEmailAsync(patient.Email, "Appointment Created",
                    $"Your appointment with {doctor.Name} is created and in pending status");*/
                _db.Appointments.Add(appointment);
                await _db.SaveChangesAsync();
                return 2;
            }

        }

        public List<DoctorVM> GetDoctorList()
        {
            var doctors = (from user in _db.Users
                           join userRoles in _db.UserRoles on user.Id equals userRoles.UserId
                           join roles in _db.Roles.Where(x => x.Name == Helper.Doctor) on userRoles.RoleId equals roles.Id
                           select new DoctorVM
                           {
                               Id = user.Id,
                               Name = user.Name
                           }
                           ).ToList();
            return doctors;
        }

        public List<PatientVM> GetPatientList()
        {
            var patients = (from user in _db.Users
                           join userRoles in _db.UserRoles on user.Id equals userRoles.UserId
                           join roles in _db.Roles.Where(x => x.Name == Helper.Patient) on userRoles.RoleId equals roles.Id
                           select new PatientVM
                           {
                               Id = user.Id,
                               Name = user.Name
                           }
                           ).ToList();
            return patients;
        }
    }
}
