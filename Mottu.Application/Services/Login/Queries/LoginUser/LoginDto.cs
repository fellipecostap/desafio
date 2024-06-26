﻿using Desafio.Application.Services.Client.Queries.GetClient;

namespace Desafio.Application.Services.Login.Queries.LoginUser;
public class LoginDto
{

    public bool Authenticated { get; set; } = false;

    public string? Token { get; set; }

    public string? RefreshToken { get; set; }

    public string? UserName { get; set; }

    public string? UserMail { get; set; }

    public string? UserType { get; set; }

    public string? CreateDate { get; set; }

    public string? ExpirationDate { get; set; }

    public ClientDto? Client { get; set; }

    public bool? Notifications { get; set; }

}
