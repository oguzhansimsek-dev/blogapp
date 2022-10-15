using System;
using System.ComponentModel.DataAnnotations;

namespace blogapp.Dtos{
  public class UserForLoginDto {
    public string nickname { get; set; }
    public string password { get; set; }
  }
}