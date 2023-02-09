using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Questionnaire_test
{
    public enum QuestionTypeEnum
    {
        [Display(Name = "Single Choice")]
        SingleChoice =1,
        [Display(Name = "Multiple Choice")]
        MultipleChoice = 2,
        [Display(Name = "Fill in the blanks")]
        FillInTheBlank =3

    }
}