using System;
using System.Collections.Generic;

namespace QuizData.Models
{
    // Déclaration de la classe CarGroup et de ses propriété
    public class CarGroup
    {
        public string GroupName { get; set; }
        public List<Car> Cars { get; set; }
    }
}