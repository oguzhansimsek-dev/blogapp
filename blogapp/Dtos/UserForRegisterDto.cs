using System;
using System.ComponentModel.DataAnnotations;

namespace blogapp.Dtos{
  public class UserForRegisterDto {
    public string nickname { get; set;}
    public string password { get; set;}
    public string firstname { get; set;}
    public string lastname { get; set;}
    public string email { get; set;}
    public int uType { get; set;}
  }
}