using System;
using System.ComponentModel.DataAnnotations;

namespace blogapp.Models{
  public class Category{
    [Key]
    public int cId { get; set; }
    public string cName { get; set; }
  }
}