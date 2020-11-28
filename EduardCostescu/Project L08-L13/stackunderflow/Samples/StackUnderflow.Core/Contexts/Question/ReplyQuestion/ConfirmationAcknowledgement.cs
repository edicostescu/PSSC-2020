using System;
using System.Collections.Generic;
using System.Text;

namespace StackUnderflow.Domain.Core.Contexts.Question.ReplyQuestion
{
    public class ConfirmationAcknowledgement
    {
        public string receipt_ { get; private set; }
        public ConfirmationAcknowledgement(string receipt) => receipt_ = receipt;
    }
}
