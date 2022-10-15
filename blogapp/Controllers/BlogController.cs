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
  public class BlogController : Controller {

    public BlogDbContext _dbContext;

    public BlogController(BlogDbContext dbContext){
      _dbContext = dbContext;
    }

    [HttpGet("GetBlogs")]
    public IActionResult GetBlogs(){
      try{
        List<Blog> blogs = _dbContext.Blogs.ToList();

        if(blogs.Count == 0){
          return StatusCode(404, "No blog found.");
        }
        return Ok(blogs);

      }catch(Exception e){
        return StatusCode(500, e.Message);
      }
    }

    [HttpPost("CreateBlog")]
    public IActionResult CreateBlog([FromBody] BlogForCreateDto blogDto ){
      Blog blog = new Blog();
      try{
        blog.bTitle = blogDto.bTitle;
        blog.bDescription = blogDto.bDescription;
        blog.imgId = blogDto.imgId;
        blog.cId = blogDto.cId;
        blog.uId = blogDto.uId;
        blog.isDeleted = 0;
        blog.isArchived = 0;
        blog.createdDate = DateTime.Now;

        _dbContext.Blogs.Add(blog);
        _dbContext.SaveChanges();

        return Ok(blog);

      }catch(Exception e){
        return StatusCode(500, e.Message);
      }
    }

    [HttpPut("UpdateBlog")]
    public IActionResult UpdateBlog([FromBody] BlogForUpdateDto blogDto){
      try{
        Blog blog = _dbContext.Blogs.FirstOrDefault(b => b.bId == blogDto.bId);

        if(blog != null){
          blog.bTitle = blogDto.bTitle;
          blog.bDescription = blogDto.bDescription;
          blog.cId = blogDto.cId;
          blog.imgId = blogDto.imgId;
          blog.uId = blogDto.uId;

          _dbContext.Entry(blog).State = EntityState.Modified;
          _dbContext.SaveChanges();

          return Ok(blog);
        }else{
          return StatusCode(500, "Blog bulunamadı.");
        }

      }catch(Exception e){
        return StatusCode(500, e.Message);
      }
    }

    [HttpDelete("DeleteBlog/{bId}")]
    public IActionResult DeleteBlog([FromRoute] int bId){
      Blog blog = new Blog();

      try{
        blog = _dbContext.Blogs.FirstOrDefault(b => b.bId == bId );
        if(blog == null){
          return StatusCode(404,"Blog is not found");
        }

        _dbContext.Entry(blog).State = EntityState.Deleted;
        _dbContext.SaveChanges();
        
        return Ok("Successfully deleted");

      }catch(Exception e){
        return StatusCode(500, e.Message);
      }

    }

    
  }


} 