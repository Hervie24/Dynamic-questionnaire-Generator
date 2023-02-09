using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Questionnaire_test.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Questionnaire_test.Controllers
{
    public class QuestionController : Controller
    {
        // GET: Question
        public ActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(Models.Question question)
        {
            try
            {
                var hasOption = question.QuestionTypeEnum == QuestionTypeEnum.FillInTheBlank;

                if (hasOption)
                {
                    question.Answer = string.Empty;
                }
                if (ModelState.IsValid)
                {
                    using (var db = new SampleDataBaseEntities())
                    {

                        var nquestion = db.Questions.Create();
                        nquestion.Description = question.Description;
                        nquestion.Answer = question.Answer;
                        nquestion.QuestionType = (int)question.QuestionTypeEnum; 
                        nquestion.CreatedDate = DateTime.Now;
                        nquestion.ModifiedDate = DateTime.Now;
                        db.Questions.Add(nquestion);
                        db.SaveChanges();
                        return RedirectToAction("Create", "Question");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Data is not correct");
                }
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            return View();
        }


        [HttpGet]
        public ActionResult Questions()
        {
            var questionInfo = new List<QuestionInfo>();
            try
            {
                var questions = new List<Question>();
                using (var db = new SampleDataBaseEntities())
                {

                    questions = db.Questions.ToList();
                }

              
                if(questions.Count()>0)
                {
                    foreach(var question in questions)
                    {
                        questionInfo.Add( new QuestionInfo() { Description = question.Description, QuestionType = question.QuestionType, Options = JsonConvert.DeserializeObject<Option>(question.Answer) });
                    }

                }
                ViewBag.Questions = questionInfo;

            }
           
            catch (Exception)
            {

                throw;
            }

            return View(questionInfo);
        }

    }
}