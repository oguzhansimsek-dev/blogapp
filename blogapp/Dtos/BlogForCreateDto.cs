using System;
using System.ComponentModel.DataAnnotations;

namespace blogapp.Dtos{
  public class BlogForCreateDto {
      public string bTitle { get; set; }
      public string bDescription { get; set; }
      public int? imgId { get; set; }
      public int cId { get; set; }
      public int uId { get; set; }
  }
}