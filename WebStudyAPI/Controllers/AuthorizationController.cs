using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using WebStudyAPI.DBContext;
using WebStudyAPI.Model;

namespace WebStudyAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorizationController : Controller
    {
        private readonly FiendContext _context;
        IConfiguration configuration;
        public AuthorizationController(FiendContext context, IConfiguration configuration)
        {
            _context = context;
            this.configuration = configuration;
        }

        // GET: AuthorizationController
        [HttpGet(Name = "GetAuthorization")]
        [AllowAnonymous]
        public IResult Get(string username, string password)
        {
            try { 
            List<User> user = _context.Users
                .Where(x => x.Username == username)
                .ToList();

            if (user[0].Username==username && user[0].Usr_password == password)
            {
                string stringToken = GeneratingJWTToken(user[0]);
                return Results.Ok(stringToken);
            }

            return Results.Unauthorized();
            }
            catch
            {
                return Results.Unauthorized();
            }
        }

        // POST: AuthorizationController/Create
        [HttpPost(Name = "PostRegistration")]
        [AllowAnonymous]
        public async Task<IResult> Post(string username, string password)
        {
            var msg = new HttpResponseMessage(HttpStatusCode.Unauthorized) { ReasonPhrase = "User Exist" };
            List<User> user = _context.Users
                .Where(x => x.Username == username)
                .ToList();
            int existing = user.Count();
            if (existing == 0) { 
                try
                {
                    User newuser = new User()
                    {
                        Username = username,
                        Usr_password = password,
                        Usr_role = "student",
                        Created_at = DateTime.UtcNow,
                        Sub_start_math_month = null,
                        Sub_start_math_reading_month = null,
                        Sub_start_reading_month = null,
                        Sub_start_ready_wright_month = null,

                    };

                    _context.Users.Add(newuser);
                    _context.SaveChanges();
                    
                    string stringToken = GeneratingJWTToken(newuser);

                    return Results.Ok(stringToken);
                }
                catch
                {
                   return Results.Content("problem with adding user to db");
                }
            } else
            {
                return Results.Problem("User Exists");
            }

        }

        string GeneratingJWTToken(User user)
        {
            var issuer = configuration["Jwt:Issuer"];
            var audience = configuration["Jwt:Audiensce"];
            var key = Encoding.ASCII.GetBytes(configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                        new Claim("Id", Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Sub, user.Username),
                        new Claim("math_sub",user.Sub_start_math_month.ToString()),
                        new Claim("math_read_sub",user.Sub_start_reading_month.ToString()),
                        new Claim("read_sub",user.Sub_start_reading_month.ToString()),
                        new Claim("ready_wright_sub",user.Sub_start_ready_wright_month.ToString()),
                        new Claim(JwtRegisteredClaimNames.Jti,
                        Guid.NewGuid().ToString())
                    }),
                Expires = DateTime.UtcNow.AddMinutes(120),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials
                    (new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = tokenHandler.WriteToken(token);
            var stringToken = tokenHandler.WriteToken(token);
            return stringToken;
        }

    }
}
