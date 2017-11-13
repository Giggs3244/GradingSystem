using Escuela.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Escuela.Controllers
{
    [RoutePrefix("api/Enrollment")]
    public class EnrollmentController : ApiController
    {
        private AuthRepository _repo = null;
        private GeneralErrorMessages generalErrorMessages;

        public EnrollmentController()
        {
            generalErrorMessages = new GeneralErrorMessages();
            _repo = new AuthRepository();
        }

        [Authorize]
        [Route("post/{studentId:int}/{courseId:int}")]
        public HttpResponseMessage Post(int studentId, int courseId, HttpRequestMessage request)
        {
            try
            {
                string description = request.Content.ReadAsStringAsync().Result;

                if (description == null) description = "";

                if (studentId < 1 || courseId < 1) Request.CreateErrorResponse(HttpStatusCode.BadRequest, generalErrorMessages.EnrollmentNotReadBody);

                int resultInsert = _repo.EnrollStudentInCourse(studentId, courseId, description);

                if (resultInsert == 1)
                {
                    return Request.CreateResponse(HttpStatusCode.Created);
                }
                else if (resultInsert == 2)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, generalErrorMessages.StudentIsEnrollment);
                } // 0
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

    }
}