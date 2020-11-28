using System.Collections.Generic;
using StackUnderflow.EF.Models;

namespace StackUnderflow.Domain.Core.Contexts.Question
{
    public class QuestionWriteContext
    {
        public ICollection<Post> questions_ { get; }

        public QuestionWriteContext(ICollection<Post> questionSummary)
        {
            questions_ = questionSummary ?? new List<Post>(0);
        }
    }
}
