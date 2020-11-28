using System;
using System.Collections.Generic;
using System.Text;

namespace StackUnderflow.Domain.Core.Contexts.Question.ReplyQuestion
{
    public class ConfirmationLetter
    {
        public string email_ { get; private set; }
        public string letter_ { get; private set; }
        public Uri confirmationLink_ { get; private set; }
        public ConfirmationLetter(string email, string letter, Uri link)
        {
            email_ = email;
            letter_ = letter;
            confirmationLink_ = link;
        }
    }
}
