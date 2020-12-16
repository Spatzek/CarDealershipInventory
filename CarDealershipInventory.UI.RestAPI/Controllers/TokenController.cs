using CarDealershipInventory.Core.ApplicationServices;
using CarDealershipInventory.Core.DomainServices;
using CarDealershipInventory.Core.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarDealershipInventory.UI.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private IUserService _userService;
        private IAuthenticationHelper _authHelper;

        public TokenController(IUserService userService, IAuthenticationHelper authHelper)
        {
            _userService = userService;
            _authHelper = authHelper;
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginInputModel model)
        {
            var user = _userService.GetUserFromLoginInput(model);

            // check if username exists
            if (user == null)
            {
                return Unauthorized("Login fejlede. Tjek venligst at brugernavn og adgangskode er korrekt.");
            }

            // check if password is correct
            if (!_authHelper.VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt))
            {
                return Unauthorized("Login fejlede. Tjek venligst at brugernavn og adgangskode er korrekt.");
            }

            // Authentication successful
            return Ok(new
            {
                username = user.Username,
                token = _authHelper.GenerateToken(user)
            });
        }
    }
}
