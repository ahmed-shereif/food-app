﻿using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.UserDtos
{
    public class LoginOrchestratorDto
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Token { get; set; }

    }
}
