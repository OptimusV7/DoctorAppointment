﻿using DoctorAppointment.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DoctorAppointment.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly IAppointmentService _appointmentService;
        public AppointmentController(IAppointmentService appointmentService )
        {
            _appointmentService = appointmentService;
        }
        public IActionResult Index()
        {
            _appointmentService.GetDoctorList();
            return View();
        }
    }
}