﻿namespace TiendaServicios.Api.Identity.Models;

public class User
{
    public int UserId { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? UserGuid { get; set; }
}