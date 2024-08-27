using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace resful_project.DTO.Response
{
    public class Response
    {
        public HttpStatusCode status { get; set; }
        public Object Result { get; set; } = new Object();
    }
}