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
  public class UserController : Controller {

    private BlogDbContext _dbContext;

    public UserController(BlogDbContext dbContext){
      _dbContext = dbContext;
    }

    [HttpGet("GetUsers")]
    public IActionResult GetUsers(){
      try {
        List<User>? users = _dbContext.Users.ToList();

        if(users.Count() == 0){
          return StatusCode(404, "No found users");
        }

        return Ok(users);

      }catch(Exception e){
        return StatusCode(500, e.Message);
      }
    }

    [HttpPost("Register")]
    public IActionResult Register([FromBody] UserForRegisterDto userDto){
      
      User user = new User();
      byte[] passwordHash, passwordSalt;

      try{
        if(UserExists(userDto.nickname)){
          return StatusCode(409, "Kullanıcı adı daha önce alınmış.");
        }else{
          if(PasswordCheck(userDto.password)){
            return StatusCode(500, "Şifre en az 8 karakter olmalı.");
          }else{
            user.nickname = userDto.nickname;
            user.firstname = userDto.firstname;
            user.lastname = userDto.lastname;
            user.email = userDto.email;
            user.uType = userDto.uType;
            user.registerDate = DateTime.Now;
            
            CreatePasswordHash(userDto.password , out passwordHash, out passwordSalt);
            user.passHash = passwordHash;
            user.passSalt = passwordSalt;
            
          }
        }

        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();

        return Ok(user);

      }catch(Exception e){
        return StatusCode(500, e.Message);
      }
    }

    [HttpGet("Login")]
    public IActionResult Login([FromBody] UserForLoginDto userDto){
      User user = new User();
      try{
        
        if(UserExists(userDto.nickname)){
          user = _dbContext.Users.FirstOrDefault(u => u.nickname == userDto.nickname);
          bool result = VerifyPasswordHash(userDto.password, user.passHash, user.passSalt);

          if(result){
            return Ok(user);
          }else{
            return StatusCode(500, "Incorrect password");
          }

        }else{
          return StatusCode(500, "User not found");
        }
        
      }catch(Exception e){
        return StatusCode(500, e.Message);
      }
    }

    [HttpGet("GetUserTypes")]
    public IActionResult GetTypes(){ 
      try{

        List<UserType> types = _dbContext.UserTypes.ToList();

        if(types.Count == 0){
          return StatusCode(404, "No Type Found");
        }

        return Ok(types);

      }catch(Exception e){
        return StatusCode(500, e.Message);
      }
    }

    private void CreatePasswordHash(
            string password,
            out byte[] passwordHash,
            out byte[] passwordSalt
        )
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

    private bool VerifyPasswordHash(
        string password,
        byte[] userPasswordHash,
        byte[] userPasswordSalt
    )
    {
        using (var hmac = new System.Security.Cryptography.HMACSHA512(userPasswordSalt))
        {
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != userPasswordHash[i])
                {
                    return false;
                }
            }
            return true;
        }
    }

    private bool UserExists(string nickname)
    {
        if (_dbContext.Users.FirstOrDefault(u => u.nickname == nickname) != null)
        {
            return true;
        }
        return false;
    }
    
    private bool PasswordCheck(string password)
    {
        if (password.Length < 8)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
  }
}