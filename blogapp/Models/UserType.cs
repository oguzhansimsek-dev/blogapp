using System;
using System.ComponentModel.DataAnnotations;

namespace blogapp.Models{
  public class UserType {
    [Key]
    public int typeId { get; set; }
    public string typeName { get; set; }
  }
}