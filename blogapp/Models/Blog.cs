using System;
using System.ComponentModel.DataAnnotations;

namespace blogapp.Models{
  public class Blog{
    [Key]
    public int bId{ get; set; }
    public string bTitle { get; set; }
    public string bDescription { get; set; }
    public int? imgId { get; set; }
    public int cId { get; set; }
    public int uId { get; set; }
    public int isDeleted { get; set; }
    public int isArchived { get; set; }
    public DateTime createdDate { get; set; }

    //public User user { get; set;}

    //public Image image { get; set; }

    //public Category category { get; set; }

  }
}