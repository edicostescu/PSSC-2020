using System;
using Question.Domain.CreateQuestionWorkflow;
using static Question.Domain.CreateQuestionWorkflow.ICreateQuestion;

namespace Question.Domain.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            var questionMO = new QuestionMO("Static function", "C++", "C++", "What is a static function?");
            var questionResultMO = CreateQuestion(questionMO);

            Console.WriteLine(questionResultMO.GetType());
        }
    }
}
