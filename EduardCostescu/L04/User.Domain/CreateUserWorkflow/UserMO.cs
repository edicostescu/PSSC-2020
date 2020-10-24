using System.ComponentModel.DataAnnotations;

namespace User.Domain.CreateUserWorkflow {
    public struct UserMO {
        [Required]
        public string firstName_ { get; set; }
        
        [Required]
        public string lastName_ { get; set; }

        [Required]
        public string email_ {get; set; }

        public UserMO (string firstName, string lastName, string email) {
            firstName_ = firstName;
            lastName_ = lastName;
            email_ = email;
        }
    };
}