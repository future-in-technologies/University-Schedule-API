using System;
using System.Collections.Generic;
using System.Text;
using UniversitySchedule.Core.Abstractions;

namespace UniversitySchedule.Core.Abstractions
{
    public class EntityBaseWithId : EntityBase
    {
        public long Id { get; set; }
    }
}
