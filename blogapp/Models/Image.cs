using System;
using System.ComponentModel.DataAnnotations;

namespace blogapp.Models{
  public class Image{
    [Key]
    public int imgId { get; set; }
    public string imgPath { get; set;}
    public DateTime createdDate { get; set; }
  }
}