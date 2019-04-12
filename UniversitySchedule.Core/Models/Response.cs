using System;
using System.Collections.Generic;
using System.Text;
using UniversitySchedule.Core.Enums;

namespace UniversitySchedule.Core.Models
{
    public class Response
    {
        public static Response GetInvalidDataFormatResponse()
        {
            return new Response
            {
                Status = ResponseStatus.InvalidDataFormat,
                ErrorMessage = "Data is not in json format",
            };
        }

        public ResponseStatus Status { get; set; }
        public string ErrorMessage { get; set; }
        public string OkMessage { get; set; }
        public object Result { get; set; }
        public ExceptionMessage? ErrorCode { get; set; }
    }
}
