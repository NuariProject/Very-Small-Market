using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using User_Management_Service.Context;
using User_Management_Service.Models;
using User_Management_Service.Models.DTO;
using User_Management_Service.Repository.IRepository;

namespace User_Management_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IUserRepository _user;
        private readonly IAuthRepository _auth;
        public AuthController(IUserRepository user, IMapper mapping, IAuthRepository auth)
        {
            _user = user;
            this._response = new();
            _auth = auth;
        }

        [HttpPost("Login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> Login([FromBody] AuthDTO authDTO)
        {
            try
            {
                var message = new List<string>();

                var dataUser = await _user.GetUser(ss => ss.Username.ToLower() == authDTO.username.ToLower() && !ss.IsDelete);

                if (dataUser == null)
                {
                    _response.StatusCode = HttpStatusCode.Conflict;
                    _response.Messages = new List<string> { "Username not founf"};

                    return _response;
                }

                if (authDTO == null)
                {
                    return BadRequest(authDTO);
                }
                var res = await _auth.Authentication(authDTO, dataUser);

                if (res.Equals("0"))
                {
                    _response.StatusCode = HttpStatusCode.Conflict;
                    _response.Messages = new List<string> { "Password incorrect" };

                    return _response;
                }

                _response.Result = res;
                _response.StatusCode = HttpStatusCode.OK;

                return _response;
            }
            catch (Exception e)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.Messages = new List<string>() { e.ToString() };
            }

            return _response;
        }

    }
}
