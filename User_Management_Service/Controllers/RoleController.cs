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
    public class RoleController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IRoleRepository _role;
        private readonly IMapper _mapping;

        public RoleController(IRoleRepository role, IMapper mapping)
        {
            _role = role;
            _mapping = mapping;
            this._response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetRole()
        {
            try
            {
                IEnumerable<Role> role = await _role.GetList();
                _response.Result = _mapping.Map<List<RoleDTO>>(role);
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

        [HttpGet("{id:int}", Name = "GetRole")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetRole(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var role = await _role.GetRole(ss => ss.RoleId == id && !ss.IsDelete);


                if (role == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                _response.Result = _mapping.Map<RoleDTO>(role);
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
        public async Task<ActionResult<APIResponse>> Create([FromBody] RoleCreateDTO createDTO)
        {
            try
            {

                // custom error with modelstate
                if (await _role.GetRole(ss => ss.RoleName.ToLower() == createDTO.RoleName.ToLower() && !ss.IsDelete) != null)
                {
                    _response.StatusCode = HttpStatusCode.Conflict;
                    _response.Messages = new List<string>() { "Role already exists" };

                    return _response;
                }

                if (createDTO == null)
                {
                    return BadRequest(createDTO);
                }

                var role = _mapping.Map<Role>(createDTO);
                await _role.Create(role);

                _response.Result = _mapping.Map<RoleDTO>(role);
                _response.StatusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetRole", new { id = role.RoleId }, _response);
            }
            catch (Exception e)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.Messages = new List<string>() { e.ToString() };
            }

            return _response;
        }

        [HttpPut("{id:int}", Name = "UpdateRole")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> UpdateRole(int id, [FromBody] RoleUpdateDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.RoleId)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;

                    return BadRequest(_response);
                }

                var model = _mapping.Map<Role>(updateDTO);
                await _role.Update(model);
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

        [HttpDelete("{id:int}", Name = "DeleteRole")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> DeleteRole(int id)
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
                if (await _role.GetRole(ss => ss.RoleId == id && !ss.IsDelete) == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.Messages = new List<string>() { "Role not already exists !" };

                    return BadRequest(_response);
                }

                await _role.Delete(id);
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
