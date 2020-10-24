using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace User.Domain.CreateUserWorkflow {
    public class UserCreated : ICreateUser {
        [Required]
        public UserMO user_ { get; private set; }

        [Required]
        public Guid userId_ {get; private set; }

        public UserCreated(UserMO user, Guid userId) {
            user_ = user;
            userId_ = userId;
        }
    };

    public class UserNotCreated : ICreateUser {
        [Required]
        public string reason_ { get; private set; }

        public UserNotCreated(string reason) {
            reason_ = reason;
        }
    };

    public class UserNotValidated : ICreateUser {
        [Required]
        public IEnumerable<string> validationErrors_ { get; private set; }

        public UserNotValidated(IEnumerable<string> validationErrors) {
            validationErrors_ = validationErrors.AsEnumerable();
        }
    };
}