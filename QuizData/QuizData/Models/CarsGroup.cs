using System;
using System.Collections.Generic;

namespace QuizData.Models
{
    public class CarGroup
    {
        public string GroupName { get; set; }
        public List<Car> Cars { get; set; }
    }
}