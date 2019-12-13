using System;
using System.Collections.Generic;
using System.Text;

namespace UpgFisi.Common.Domain
{
    public class ResponseDto
    {
        public int HttpStatusCode { get; set; }
        public object Response { get; set; }
    }
}
