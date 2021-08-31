using DoctorAppointment.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorAppointment.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDBContext _db;

        public AccountController(ApplicationDBContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
