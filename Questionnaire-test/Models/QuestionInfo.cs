using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Questionnaire_test.Models
{
    public class QuestionInfo
    {
        public string Description { get; set; }
        public int QuestionType { get; set; }

        public Option Options { get; set; }
    }

   
}