using System.Collections.Generic;
using StackUnderflow.EF.Models;

namespace StackUnderflow.Domain.Core.Contexts.Question
{
    class QuestionReadContext
    {
        public IEnumerable<Post> questions_ { get; }

        public QuestionReadContext(IEnumerable<Post> questions)
        {
            questions_ = questions;
        }
    }
}
