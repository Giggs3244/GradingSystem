using Escuela.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Escuela.Controllers
{
    [RoutePrefix("api/Student")]
    public class StudentController : ApiController
    {
        private AuthRepository _repo = null;
        private GeneralErrorMessages generalErrorMessages;

        public StudentController()
        {
            generalErrorMessages = new GeneralErrorMessages();
            _repo = new AuthRepository();
        }

        [Authorize]
        [Route("get")]
        // GET api/<controller>
        public HttpResponseMessage Get()
        {
            try
            {
                List<Student> students = _repo.GetAllStudents().ToList();
                if (students != null && students.Count > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, students);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, generalErrorMessages.StudentsNotFound);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Authorize]
        [Route("get/{id:int}")]
        // GET api/<controller>/5
        public HttpResponseMessage Get(int id)
        {
            try
            {
                Student student = _repo.GetStudent(id);

                if (student == null) Request.CreateErrorResponse(HttpStatusCode.BadRequest, generalErrorMessages.StudentNotFound);

                return Request.CreateResponse(HttpStatusCode.OK, student);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Authorize]
        [Route("post")]
        // POST api/<controller>
        public HttpResponseMessage Post([FromBody] Student student)
        {
            try
            {
                if (student == null) Request.CreateErrorResponse(HttpStatusCode.BadRequest, generalErrorMessages.StudentNotReadBody);

                if (_repo.InsertStudent(student) && _repo.SaveAll())
                {
                    return Request.CreateResponse(HttpStatusCode.Created, student);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, generalErrorMessages.GeneralErrorMessage);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Authorize]
        [Route("put/{id:int}")]
        // PUT api/<controller>/5
        public HttpResponseMessage Put(int id, [FromBody] Student student)
        {
            try
            {
                if (student == null) Request.CreateErrorResponse(HttpStatusCode.BadRequest, generalErrorMessages.StudentNotReadBody);

                var originalStudent = _repo.GetStudent(id);

                if (originalStudent == null || originalStudent.Id != id)
                {
                    return Request.CreateResponse(HttpStatusCode.NotModified, generalErrorMessages.StudentNotModified);
                }
                else
                {
                    student.Id = id;
                }

                if (_repo.UpdateStudent(originalStudent, student) && _repo.SaveAll())
                {
                    return Request.CreateResponse(HttpStatusCode.OK, student);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotModified, generalErrorMessages.StudentNotModified);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

        [Authorize]
        [Route("delete/{id:int}")]
        // DELETE api/<controller>/5
        public HttpResponseMessage Delete(int id)
        {
            try
            {
                var student = _repo.GetStudent(id);

                if (student == null)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, generalErrorMessages.StudentsNotFound);
                }

                if (_repo.DeleteStudent(id) && _repo.SaveAll())
                {
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, generalErrorMessages.GeneralErrorMessage);
                }

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}