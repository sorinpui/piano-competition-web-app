﻿using CompetitionWebApi.Domain.Enums;

namespace CompetitionWebApi.Application.Requests;

public class RegisterRequest
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public List<RoleType> Roles { get; set; }
}
