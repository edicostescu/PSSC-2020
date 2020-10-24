using System;
using System.Collections.Generic;
using User.Domain.CreateUserWorkflow;
using Question.Domain.CreateQuestionWorkflow;
using static User.Domain.CreateUserWorkflow.ICreateUser;
using static Question.Domain.CreateQuestionWorkflow.ICreateQuestion;

namespace Question.Domain.Test
{
    class Program
    {
        static void Main(string[] args)
        {
           List<string> tags = new List<string>() {
               "C++",
               "C#",
           };

            var userMO = new UserMO("Eduard", "Costescu", "edi.costescu@gmail.com");
            var userResultMO = CreateUser(userMO);

            var questionMO = new QuestionMO("Static function", "C++", tags, "What is a static function?");
            var questionResultMO = CreateQuestion(questionMO, (UserCreated)userResultMO);
            Console.WriteLine(questionResultMO.GetType());
            
            ((QuestionPublished)questionResultMO).VoteQuestion((UserCreated)userResultMO, true);
            ((QuestionPublished)questionResultMO).VoteQuestion((UserCreated)userResultMO, false);
            Console.WriteLine(((QuestionPublished)questionResultMO).votes_);
        }
    }
}
