using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Test.Api.Models;
using Test.Business.Helpers;
using Test.Business.Services.Interfaces;
using Test.DTO.DTOEntities;
using Test.Entities.Entities;

namespace Test.Api.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ICountryService _countryService;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        public UsersController(IUserService userService,
            ICountryService countryService,
            IMapper mapper,
            IConfiguration config)
        {
            _userService = userService;
            _mapper = mapper;
            _config = config;
            _countryService = countryService;
        }

        [AllowAnonymous]
        [HttpPost("Register")]
        //POST : api/Users/Register
        public async Task<object> Register([FromBody]UserDTO userDTO)
        {
            try
            {
                var userToCreate = _mapper.Map<User>(userDTO);
                var createdUser = await _userService.Create(userToCreate);
                var userToReturn = _mapper.Map<UserDTO>(createdUser);
                return Ok(new CustomResult(true, userToReturn));
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new CustomResult(false, null, ex.Message));
            }
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        //POST : api/Users/Login
        public IActionResult Login(UserLoginDTO userDTO)
        {
            var user = _userService.Login(userDTO.LoginName, userDTO.Password);

            if (user == null)
                return BadRequest(new { message = "LoginName or password is incorrect" });

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config.GetSection("AppSettings:Token").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.LoginName)
                }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            var result = new AuthenticateDTO
            {
                LoginName = user.LoginName,
                AccessToken = tokenString,
                Expires = token.ValidTo
            };
            // return  user info (without password) and token to store client side
            return Ok(new CustomResult(true, result));
        }

        [HttpGet("GetAllUsers")]
        //GET : api/Users/GetAllUsers
        public IActionResult GetAll()
        {
            var allUsers = _userService.GetAll();
            var userDtos = _mapper.Map<List<UserDTO>>(allUsers);
            return Ok(new CustomResult(true, userDtos));
        }

        [AllowAnonymous]
        [HttpGet("GetCountries")]
        //GET : api/Users/GetCountries
        public IActionResult GetCountries()
        {
            var allCountries = _countryService.GetAll();
            var countryDtos = _mapper.Map<List<CountryDTO>>(allCountries);
            return Ok(new CustomResult(true, countryDtos));
        }

        [AllowAnonymous]
        [HttpPost("Upload"), DisableRequestSizeLimit]
        //POST : api/Users/Upload
        public IActionResult Upload(string loginName)
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    _userService.UpdateImagePath(loginName, dbPath);
                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetUserInfo")]
        //GET : api/Users/GetUserInfo
        public IActionResult GetByUserName(string loginName)
        {
            var user = _userService.GetByUserName(loginName);
            var userDTO = _mapper.Map<UserDTO>(user);
            return Ok(new CustomResult(true, userDTO));
        }

        [HttpPut("Update")]
        //Put : api/Users/GetUserInfo
        public IActionResult Update(UserDTO userDTO)
        {
            try
            {
                var userToUpdate = _mapper.Map<User>(userDTO);
                _userService.Update(userToUpdate);
                return Ok(new CustomResult(true, userDTO));
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("Delete")]
        //Delete : api/Users/Delete
        public IActionResult Delete(string loginName)
        {
            try
            {
                var user = _userService.GetByUserName(loginName);
                _userService.DeleteByUserName(loginName);
                return Ok(new CustomResult(true, null));
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}