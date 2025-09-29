using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizHubApplication.DTOs.Requests;
using QuizHubApplication.DTOs.Responses;
using QuizHubApplication.Exceptions;
using QuizHubApplication.Interfaces;
using QuizHubApplication.Services;

namespace QuizHubApi.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController(ICategoryService categoryService) : ControllerBase
    {
        //privatan _, public je velikim slovom
        private readonly ICategoryService _categoryService = categoryService;

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult<CategoryDto> CreateCategory([FromBody] CreateCategoryDto createCategoryDto)
        {
            try
            {
                CategoryDto? categoryDto = _categoryService.CreateCategory(createCategoryDto);

                return Ok(categoryDto);
            }
            catch (EntityAlreadyExists ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [Authorize(Roles = "Admin, User")]
        [HttpGet("{name}")]
        public ActionResult<CategoryDto> GetCategory(string name)
        {
            CategoryDto? categoryDto = _categoryService.GetCategory(name);

            if (categoryDto is null)

            { return NotFound(string.Format("The category '{0}' does not exists.", name)); }

            return Ok(categoryDto);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{name}")]
        public ActionResult<CategoryDto> DeleteCategory(string name)
        {
            CategoryDto? categoryDto = _categoryService.DeleteCategory(name);
            if (categoryDto is null)
            
            { return BadRequest(string.Format("The category '{0}' can not be deleted! It does not exists.", name)); }
           
            return Ok(categoryDto);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public ActionResult<CategoryDto> UpdateCategory([FromBody] UpdateCategoryDto updateCategoryDto)
        {

            CategoryDto? category = _categoryService.UpdateCategory(updateCategoryDto);

            if (category is null)

            { return BadRequest(string.Format("The category '{0}' can not be updated! It does not exists.", updateCategoryDto.OldName)); }


            return Ok(category);


        }

        [Authorize(Roles = "Admin, User")]
        [HttpGet]
        public ActionResult<List<CategoryDto>> GetCategories()
        {

            return Ok(_categoryService.GetCategories());

        }

    }
}
