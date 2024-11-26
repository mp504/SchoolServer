using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ServerOfSchool.Data;
using ServerOfSchool.Dto;
using ServerOfSchool.Interfaces;
using ServerOfSchool.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ServerOfSchool.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        private readonly IStudentRepository _studentRepository;

        private readonly ICourseRepository _courseRepository;
        private readonly ITeacherRepository _teacherRepository;

        public AccountController(UserManager<ApplicationUser> userManager,
        IConfiguration configuration,
        DataContext context, 
        IMapper mapper,
        IStudentRepository studentRepository,
        ICourseRepository courseRepository,
        ITeacherRepository teacherRepository)
        {
            _configuration = configuration;
            _userManager = userManager;
            _context = context;
            _mapper = mapper;
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
            _teacherRepository = teacherRepository;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateOfBirth = model.DateOfBirth
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            // Assign role
            await _userManager.AddToRoleAsync(user, model.Role);
            
            // Create profile
            if (model.Role == "Student")
            {
                StudentAddress address = _mapper.Map<StudentAddress>(model.address);
                var student = new Student
                {
                    ApplicationUserId = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    DateOfBirth = user.DateOfBirth,
                    Address = address
                    // Set other properties as needed
                };
                _context.Students.Add(student);
            }
            else if (model.Role == "Teacher")
            {
                var teacher = new TeacherDto
                {
                    ApplicationUserId = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    DateOfBirth = user.DateOfBirth
                    // Set other properties as needed
                };
                var teacherMap = _mapper.Map<Teacher>(teacher);
                _teacherRepository.AddAsync(teacherMap);
            }

            await _context.SaveChangesAsync();

            return Ok(new { Message = "User registered successfully" });
        }



        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var tokenString = GenerateJWTToken(user);
                return Ok(new { Token = tokenString });
            }
            return Unauthorized();
        }

        private string GenerateJWTToken(ApplicationUser user)
        {
            var userRoles = _userManager.GetRolesAsync(user).Result;

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
            new Claim(JwtRegisteredClaimNames.Email, user.Email)
        };

            // Add roles to claims
            foreach (var role in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(3),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
    public class RegisterRequest
    {
        public RegisterModel Model { get; set; }
        public StudentAddressDto AddressDto { get; set; }
    }
}

