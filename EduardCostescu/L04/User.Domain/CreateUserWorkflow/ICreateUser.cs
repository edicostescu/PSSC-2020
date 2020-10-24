using System;
using System.Collections.Generic;

namespace User.Domain.CreateUserWorkflow {
    public interface ICreateUser {
        public static ICreateUser CreateUser(UserMO receivedRegistration) {
            if (string.IsNullOrWhiteSpace(receivedRegistration.firstName_) ||
                string.IsNullOrWhiteSpace(receivedRegistration.lastName_)  ||
                string.IsNullOrWhiteSpace(receivedRegistration.email_))    {
                    IEnumerable<string> errors = new List<string>() {
                      "First name empty",
                      "Last name empty",
                      "Email address empty"  
                    };
                    return new UserNotValidated(errors);
            }

            return new UserCreated(receivedRegistration, Guid.NewGuid());
        }
    }
}