using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using SkokieIceCream.Web.Areas.HelpPage.Models;
using SkokieIceCream.Entity;
using SkokieIceCream.Service;

namespace SkokieIceCream.Web.Areas.HelpPage.Controllers
{
    [EnableCors(origins: "http://localhost:55058", headers: "*", methods: "*")]
    public class StudentInformationController : ApiController
    {
        private readonly IStudentService _studentService;

        public StudentInformationController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        // GET api/studentinformation
        // GET api/ptemployees
        [Route("api/studentInfo")]
        public HttpResponseMessage Get()
        {
            var student = _studentService.GetAllData();
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, student);
            return response;
        }

        // GET api/ptemployees/5
        [Route("api/studentInfo/{id?}")]
        public HttpResponseMessage Get(Guid id)
        {
            var student = _studentService.GetStudentById(id);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, student);
            return response;
        }

        [Route("api/studentInfo/{name:alpha}")]
        public HttpResponseMessage Get(string name)
        {
            var student = _studentService.GetStudentByName(name);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, student);
            return response;
        }

        [Route("api/studentInfo")]
        public HttpResponseMessage Post(Student e)
        {
            var student = _studentService.InsertStudent(e);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, student);
            if (response.StatusCode.ToString() == "OK")
            {
                response.Content = new StringContent("Saved Successfully.");
            }
            else
            {
                response.Content = new StringContent("Error.");
            }
            return response;
        }

        [Route("api/studentInfo")]
        public HttpResponseMessage Put(Student e)
        {
            var student = _studentService.UpdateEmployee(e);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, student);
            if(response.StatusCode.ToString() == "OK")
            {
                response.Content = new StringContent("Saved Successfully.");
            }
            else
            {
                response.Content = new StringContent("Error.");
            }
            return response;
        }

        [Route("api/studentInfo/Delete/{id}")]
        public HttpResponseMessage Delete(Guid id)
        {
            var student = _studentService.DeleteEmployee(id);
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, student); 
            if (response.StatusCode.ToString() == "OK")
            {
                response.Content = new StringContent("Deleted.");
            }
            else
            {
                response.Content = new StringContent("Error.");
            }
            return response;
        }
    }
}
