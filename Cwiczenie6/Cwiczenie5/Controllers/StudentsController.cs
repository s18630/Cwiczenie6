using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cwiczenie5.Models;
using Cwiczenie5.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cwiczenie5.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private IStudentsDbService _service;

        public StudentsController (IStudentsDbService service)
        {
            _service = service;
        }


        [HttpGet]

        public IActionResult GetStudents()
        {

            try
            {
                IEnumerable<Student> students = _service.GetStudents();

                
                return Ok(students);
            }

            catch (Exception ex)
            {

                return BadRequest();
            }

        }



    }
}
