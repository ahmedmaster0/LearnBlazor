﻿using System.Security.Claims;

namespace BlazorApp1.Authentication
{
    public class UserSession
    {
        public string? UserName { get; set; }
        public string? Role { get; set; }
        public string Permissions { get; set; }
    }
}
