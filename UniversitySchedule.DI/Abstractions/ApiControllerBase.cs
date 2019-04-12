using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using UniversitySchedule.DI.Managers;

namespace UniversitySchedule.DI.Abstractions
{
    public abstract class ApiControllerBase : ControllerBase
    {
        public long ClientId
        {
            get
            {
                return TokenManager.GetClientId(User);
            }
        }

        public int UserTimeCorrection
        {
            get
            {
                return RequestManager.GetUserTimeCorrection(Request);
            }
        }

        public long PartnerId
        {
            get
            {
                return TokenManager.GetPartnerId(User);
            }
        }

        public string Language
        {
            get
            {
                return RequestManager.GetLanguage(Request);
            }
        }
    }
}
