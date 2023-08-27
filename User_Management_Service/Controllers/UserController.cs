using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using User_Management_Service.Models;
using User_Management_Service.Models.DTO;
using User_Management_Service.Repository.IRepository;

namespace User_Management_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IUserRepository _user;
        private readonly IMapper _mapping;
        public UserController(IUserRepository user, IMapper mapping)
        {
            _user = user;
            this._response = new();
            _mapping = mapping;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetUsers()
        {
            try
            {
                IEnumerable<UserDTO> users = await _user.GetList();
                _response.Result = users;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception e)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.Messages = new List<string>() { e.ToString() };
            }

            return _response;
        }


        [HttpGet("{id:int}", Name = "GetUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetUser(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var user = await _user.GetUser(ss => ss.UserId == id && !ss.IsDelete);


                if (user == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                _response.Result = user;
                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (Exception e)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.Messages = new List<string>() { e.ToString() };
            }

            return _response;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> Create([FromBody] UserCreateDTO createDTO)
        {
            try
            {
                var message = new List<string>();
                // custom error with modelstate
                if (await _user.GetUser(ss => ss.Username.ToLower() == createDTO.Username.ToLower() && !ss.IsDelete) != null)
                {
                    message.Add("Username already exists");
                }
                if (await _user.GetUser(ss => ss.Email.ToLower() == createDTO.Email.ToLower() && !ss.IsDelete) != null)
                {
                    message.Add("Email already exists");
                }

                if(message.Count > 0)
                {
                    _response.StatusCode = HttpStatusCode.Conflict;
                    _response.Messages = message;

                    return _response;
                }


                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }

                var user = _mapping.Map<UserDTO>(createDTO);
                await _user.Create(user);

                _response.Result = user;
                _response.StatusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetUser", new { id = user.UserId}, _response);
            }
            catch (Exception e)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.Messages = new List<string>() { e.ToString() };
            }

            return _response;
        }

        [HttpPut("{id:int}", Name = "UpdateUser")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> UpdateUser(int id, [FromBody] UserUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.UserId)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;

                    return BadRequest(_response);
                }

                var message = new List<string>();


                if (await _user.GetUser(ss => ss.Username.ToLower() == updateDTO.Username.ToLower() && ss.UserId != updateDTO.UserId && !ss.IsDelete) != null)
                {
                    message.Add("Username already exists");
                }
                if (await _user.GetUser(ss => ss.Email.ToLower() == updateDTO.Email.ToLower() && ss.UserId != updateDTO.UserId && !ss.IsDelete) != null)
                {
                    message.Add("Email already exists");
                }

                if (message.Count > 0)
                {
                    _response.StatusCode = HttpStatusCode.Conflict;
                    _response.Messages = message;

                    return _response;
                }

                UserDTO model = _mapping.Map<UserDTO>(updateDTO);
                await _user.Update(model);
                _response.StatusCode = HttpStatusCode.NoContent;

                return Ok(_response);
            }
            catch (Exception e)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.Messages = new List<string>() { e.ToString() };
            }

            return _response;
        }

        [HttpDelete("{id:int}", Name = "DeleteProduct")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> DeleteProduct(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;

                    return BadRequest(_response);
                }

                // custom error with modelstate
                if (await _user.GetUser(ss => ss.UserId == id && !ss.IsDelete) == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.Messages = new List<string>() { "User not already exists !" };

                    return BadRequest(_response);
                }

                await _user.Delete(id);
                _response.StatusCode = HttpStatusCode.NoContent;

                return Ok(_response);
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
