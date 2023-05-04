using ClassRoom.MVCP.Helper;
using ClassRoom.MVCP.Models;
using ClassRoomData.Context;
using ClassRoomData.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace ClassRoom.MVCP.Controllers
{
    [Authorize]
    public class SchoolsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserHelper _userProvider;
        public SchoolsController(AppDbContext context,UserHelper userHelper)
        {
            _context = context;
            _userProvider = userHelper;
        }



        public async  Task<IActionResult> Index()
        {
            var schools =  await _context.Schools
                .Include(school=> school.UserSchools)
                .ToListAsync();
            return View(schools);
        }

        [HttpGet]
        public IActionResult CreateSchool()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> CreateSchool(CreateSchoolModel createSchoolModel)
        {
            if (!ModelState.IsValid)
            {
                return View(createSchoolModel);
            }


            var school = new School()
            {
                Name = createSchoolModel.Name,
                Description =  createSchoolModel.Description,
            };

            if (createSchoolModel.Photo != null)
            {
                school.PhotoUrl = await FileHelper.SaveSchoolImage(createSchoolModel.Photo);
            }

            school.UserSchools = new List<UserSchool>
            {
                new UserSchool()
                {
                    UserId = _userProvider.UserId,
                    Type = EUserSchool.Creater
                }
            };

            _context.Schools.Add(school);
            await  _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> GetSchoolById(Guid Id)
        {
            var school = await _context.Schools
                .Include(school => school.UserSchools).ThenInclude(userSchool => userSchool.User).FirstOrDefaultAsync(x => x.Id == Id);
        return View (school);   
        }

        public async Task<IActionResult> JoinSchool(Guid id)
        {
            var school = await _context.Schools
                .Include(school => school.UserSchools)
                .ThenInclude(userSchool => userSchool.User)
                .FirstOrDefaultAsync(x => x.Id == id);
            var userId = _userProvider.UserId;
            var isUserExistsInSchool = school.UserSchools.Any(x => x.UserId == userId);
            if (!isUserExistsInSchool)
            {
                school.UserSchools.Add(new UserSchool()
                {
                    UserId = userId,
                    Type = EUserSchool.Student
                });
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("GetSchoolById",new {id = school.Id});
        }
    }
}
