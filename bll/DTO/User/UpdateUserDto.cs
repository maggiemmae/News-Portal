﻿using System;

namespace bll.DTO.User
{
    public class UpdateUserDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

    }
}
