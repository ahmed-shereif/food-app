﻿using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOS.UserDtos
{
    public class LoginUserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public Role Role { get; set; }



      
    }
}
