using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroupInvest.Microservices.Participantes.API.V1.Dtos
{
    public class RegisterAppUserDtoUser
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }

    }

    public class RegisterAppUserDtoPassword
    {
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class RegisterAppUserDto
    {
        public RegisterAppUserDtoUser User { get; set; }
        public RegisterAppUserDtoPassword Password { get; set; }
    }
}
