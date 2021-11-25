using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using wstest.Models;
using webServices.DAO;

namespace wsteste.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        //postman
        //localhost/WeatherForecast
        //localhost/api/Values

        //Creates an obj UserDAO, to call the class methods
        UserDAO uDAO = new UserDAO();

        [HttpGet]

        public IActionResult getAllUsers(){
            List<User> users = new List<User>();
            users = uDAO.Users();
            if(users != null){
                return Ok(users);
            }
            return NotFound("Impossible to load users list. No user registered");
        }


        [HttpGet("{cpf}")]
        public IActionResult getUser(string cpf){
            User user = new User();
            user = uDAO.getUser(cpf);
            if(user!=null){
            return Ok(user);
            }
            return NotFound("User not found");
        }

        [HttpPost("{name}/{cpf}/{age}")]
        public IActionResult addUser(string cpf, string name, string age){
            bool add = uDAO.addUser(cpf, name, age);
            if(add){
            return Ok("User added successfully");
            }
            return NotFound("User registration unsuccessul. Invalid informations");
        }

        [HttpDelete("{cpf}")]
        public IActionResult removeUser(string cpf){
            bool remove = uDAO.removeUser(cpf);
            if(remove){
            return Ok("User removed successfully");
            }
            return NotFound("User not found");
        }

        [HttpPut("{cpf}/{name}/{age}")]
        public IActionResult updateUser(string cpf, string name, string age){
            bool update = uDAO.updateUser(cpf,name,age);
            if(update){
            return Ok("User Update successful!");
            }
            return NotFound("User Update unsuccessful. Invalid informations");
        }
    }
}
