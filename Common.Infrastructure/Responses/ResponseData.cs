using System;
using System.Collections.Generic;
using System.Text;

namespace Common.Infrastructure.Responses
{
    public class ResponseData
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }

        public string Gender { get; set; }

        public string Location { get; set; }
    }
}
