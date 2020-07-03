using System;
using Engenharia_Software.Domain.Entities;

namespace Engenharia_Software.Presentation.RequestModel
{
    public class UserRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);
        }

        public User ToModel()
        {
            return new User { Username = Username, Password = Password };
        }
    }
}