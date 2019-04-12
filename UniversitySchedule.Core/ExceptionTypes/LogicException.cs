using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using UniversitySchedule.Core.Enums;

namespace UniversitySchedule.Core.ExceptionTypes
{
    public class LogicException : Exception
    {
        public ExceptionMessage? MessageType
        {
            get
            {
                return _messageType;
            }
        }
        private readonly ExceptionMessage? _messageType;
        public LogicException()
        {

        }

        public LogicException(ExceptionMessage message)
        {
            _messageType = message;
        }

        public LogicException(string message) : base(message)
        {
        }

        protected LogicException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
