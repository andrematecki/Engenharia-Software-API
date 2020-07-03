using Engenharia_Software.Domain.Entities;

namespace Engenharia_Software.Presentation.ResponseModel
{
    public class UserResponse
    {
        public string Username { get; set; }
        public string Email { get; set; }

        public UserResponse(User user)
        {
            if (user != null)
            {
                Username = user.Username;
                Email = user.Email;
            }
        }
    }
}
