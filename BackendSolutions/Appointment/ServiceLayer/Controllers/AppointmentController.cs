﻿using BussinessLogic;
using DataEntities.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ServiceLayer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private  readonly IAppointment _logic;
        
        public AppointmentController(IAppointment logic)
        {
            _logic = logic;
            
        }

        [HttpGet("getappointmentsbypatientid")]
        public ActionResult GetAppointmentByPatientId([FromHeader] Guid id)
        {
            try
            {

                var appointments = _logic.GetAppointmentsByPatient(id);
                if (appointments != null)
                {
                    return Ok(appointments);
                }
                else
                {
                    return NoContent();
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpGet("getappointmentsbyDoctorid")]
        public ActionResult GetAppointmentsByDoctorId([FromHeader] Guid id)
        {
            try
            {

                var appointments = _logic.GetAppointmentsByDoctor(id);
                if (appointments != null)
                {
                    return Ok(appointments);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("BookAppiontment")] // Trying to create a resource on the server
        public ActionResult AddAppointment([FromBody] Models.Appointment a)
        {
            try
            {
               

                var result=_logic.AddAppointment(a);
                return Ok(result);
            }
            catch (SqlException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
        [HttpGet("getappointmentsbyDate")]
        public ActionResult GetAppointmentByDate([FromHeader] string date1)
        {
            try
            {
                var date = DateTime.Parse(date1);
                var appointment = _logic.GetAppointmentsByDate(date);
                if (appointment != null)
                {
                    return Ok(appointment);
                }
                else
                {
                    return NoContent();
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("getappointmentsByStatus")]
        public ActionResult GetAppointmentByStatus([FromHeader] string date1)
        {
            try
            {
                var date = DateTime.Parse(date1);
                var appointment = _logic.GetAppointmentsBystatus(date);
                if (appointment != null)
                {
                    return Ok(appointment);
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPut("Update")]
        public  ActionResult UpdateStatus(Guid AppointmentId ,string Status)
        {
            try
            {

               var result= _logic.UpdateStatus(AppointmentId, Status);
                return Ok(result);
            }
            catch (SqlException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("Email_Notification")]

        public IActionResult SendEmail([FromHeader]string Email, [FromHeader]string date1, [FromHeader]string status)
        {
            try
            {
                var date = DateTime.Parse(date1);
                _logic.EmailFunc(Email, date, status);
                return Ok("Email Sent");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message+"Email not sent");
            }
            
        }

    }
}
