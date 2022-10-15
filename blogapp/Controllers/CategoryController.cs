using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using blogapp.Data;
using blogapp.Models;
using blogapp.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace blogapp.Controllers {
  [Route("api/[controller]")]
  public class CategoryController : Controller{
    private BlogDbContext _dbContext;

    public CategoryController(BlogDbContext dbContext){
      _dbContext = dbContext;
    }

    [HttpGet("GetCategories")]
    public IActionResult GetCategories(){
      try{
        List<Category> categories = _dbContext.Categories.ToList();

        return Ok(categories);
      }catch(Exception e){
        return StatusCode(500, e.Message);
      }
    }

    [HttpPost("CreateCategory")]
    public IActionResult CreateCategory([FromBody] CategoryForCreateDto categoryDto){
      try{
        Category category = new Category();

        if( CategoryCheck(categoryDto.cName)){

          category.cName = categoryDto.cName;
          _dbContext.Categories.Add(category);
          _dbContext.SaveChanges();

          return Ok(category);
        }else{
          return StatusCode(500, "Kategori mevcut, farklı bir kategori ismi giriniz!!");
        }

      }catch(Exception e){
        return StatusCode(500, e.Message);
      }
    }
    [HttpDelete("DeleteCategory/{cId}")]
    public IActionResult DeleteCategory([FromRoute] int cId){
      Category category = _dbContext.Categories.FirstOrDefault(c => c.cId == cId);

      if(category == null){
        return StatusCode(404, "Category not found");
      }

      _dbContext.Entry(category).State = EntityState.Deleted;
      _dbContext.SaveChanges();
      
      return Ok("Successfully deleted");

    }

    private bool CategoryCheck(string category){
      //Kategorinin olup olmadağı kontrol ediliyor.
      if( _dbContext.Categories.FirstOrDefault(c => c.cName.ToLower() == category.ToLower()) != null){
        return false;
      }else{
        return true;
      }
    }

  }
}