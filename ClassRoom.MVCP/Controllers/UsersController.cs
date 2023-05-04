using ClassRoom.MVCP.Models;
using ClassRoomData.Context;
using ClassRoomData.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;
using Microsoft.SqlServer.Server;

namespace ClassRoom.MVCP.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public UsersController(UserManager<User> userMAnager, SignInManager<User> signInManager)
        {
            _userManager = userMAnager;
            _signInManager = signInManager;
        }

        
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }
       

        [HttpPost]
        public async Task<IActionResult> SignUp([FromForm] CreateUserDto createUserDto)
        {
            if (!ModelState.IsValid)
            {
                return View(createUserDto);
            }

            var user = new User()
            {
                Firstname = createUserDto.Firstname,
                Lastname = createUserDto.Lastname,
                UserName = createUserDto.Username,
                PhoneNumber = createUserDto.PhoneNumber,
            };
            var result =  await _userManager.CreateAsync(user,createUserDto.Password);

            if (!result.Succeeded)
            {
                ModelState.AddModelError("Username",result.Errors.First().Description);
                return View();
            }


            await _signInManager.SignInAsync(user, isPersistent: true);

            return RedirectToAction("Index","Home");

        }


        [Authorize]
        
        public async  Task<IActionResult> Profile()
        {
            var result = await _userManager.GetUserAsync(User);
            return View(result);
        }
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }


        
        [HttpPost]
        public async Task<IActionResult> SignIn([FromForm] SignInUserDto signInUserDto)
        {
            var resultUser = await _signInManager.PasswordSignInAsync(signInUserDto.Username, signInUserDto.Password, true, false);
            if (!resultUser.Succeeded)
            {
                ModelState.AddModelError("Username", "Username or Password is incorrect");
                return View();
            }

            return RedirectToAction("Profile");
        }
        [HttpGet]
        public IActionResult EditProfile()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public IActionResult EditProfile(EditProfileModel editProfileModel)
        {
            var db = new AppDbContext();
            var user = db.Users.FirstOrDefault(u => u.UserName == editProfileModel.Username);

            user.PhoneNumber = editProfileModel.PhoneNumber;

            db.SaveChanges();
            
                
           return  RedirectToAction("Profile");
        }
     }
}
