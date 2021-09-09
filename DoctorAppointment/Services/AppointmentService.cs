using DoctorAppointment.Models;
using DoctorAppointment.Models.ViewModels;
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
        public List<DoctorVM> GetDoctorList()
        {
            var users = (from user in _db.Users
                           select new DoctorVM
                           {
                               Id = user.Id,
                               Name = user.Name
                           }
                           ).ToList();
            return users;
        }

        public List<PatientVM> GetPatientList()
        {
            throw new NotImplementedException();
        }
    }
}
