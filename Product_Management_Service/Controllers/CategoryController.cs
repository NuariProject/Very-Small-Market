using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product_Management_Service.Context;
using Product_Management_Service.Models;
using Product_Management_Service.Models.DTO;
using Product_Management_Service.Repository.IRepository;
using System.Net;

namespace Product_Management_Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        protected APIResponse _response;
        private readonly ICategoryRepository _category;
        private readonly IMapper _mapping;

        public CategoryController(ICategoryRepository category, IMapper mapping)
        {
            _category = category;
            _mapping = mapping;
            this._response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetCategory()
        {
            try
            {
                IEnumerable<Category> categories = await _category.GetList();
                _response.Result = _mapping.Map<List<CategoryDTO>>(categories);
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


        [HttpGet("{id:int}", Name = "GetCategory")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetCategory(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var category = await _category.GetCategory(ss => ss.CategoryId == id && !ss.IsDelete);


                if (category == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                _response.Result = _mapping.Map<CategoryDTO>(category);
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
        public async Task<ActionResult<APIResponse>> Create([FromBody] CategoryCreatedDTO createDTO)
        {
            try
            {

                // custom error with modelstate
                if (await _category.GetCategory(ss => ss.Name.ToLower() == createDTO.Name.ToLower() && !ss.IsDelete) != null)
                {
                    _response.StatusCode = HttpStatusCode.Conflict;
                    _response.Messages = new List<string>() { "Category already exists" };

                    return _response;
                }

                if (createDTO == null)
                {

                    return BadRequest(createDTO);
                }

                Category category = _mapping.Map<Category>(createDTO);
                await _category.Create(category);

                _response.Result = _mapping.Map<CategoryDTO>(category);
                _response.StatusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetCategory", new { id = category.CategoryId }, _response);
            }
            catch (Exception e)
            {
                _response.StatusCode = HttpStatusCode.InternalServerError;
                _response.IsSuccess = false;
                _response.Messages = new List<string>() { e.ToString() };
            }

            return _response;
        }

        [HttpPut("{id:int}", Name = "UpdateCategory")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> UpdateCategory(int id, [FromBody] CategoryUpdatedDTO updateDTO)
        {
            try
            {
                if (updateDTO == null || id != updateDTO.CategoryId)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsSuccess = false;

                    return BadRequest(_response);
                }

                Category model = _mapping.Map<Category>(updateDTO);
                await _category.Update(model);
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

        [HttpDelete("{id:int}", Name = "DeleteCategory")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> DeleteCategory(int id)
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
                if (await _category.GetCategory(ss => ss.CategoryId == id && !ss.IsDelete) == null)
                {
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.Messages = new List<string>() { "Category not already exists !" };

                    return BadRequest(_response);
                }

                await _category.Delete(id);
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
