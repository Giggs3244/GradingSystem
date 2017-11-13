using Escuela.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Escuela.Controllers
{
    [RoutePrefix("api/Course")]
    public class CourseController : ApiController
    {
        private AuthRepository _repo = null;
        private GeneralErrorMessages generalErrorMessages;

        public CourseController()
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
                List<Course> courses = _repo.GetAllCourses().ToList();
                if (courses != null && courses.Count > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, courses);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound, generalErrorMessages.CoursesNotFound);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }

    }
}