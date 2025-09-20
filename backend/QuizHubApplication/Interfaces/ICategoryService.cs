using QuizHubApplication.DTOs.Requests;
using QuizHubApplication.DTOs.Responses;
using QuizHubDomain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizHubApplication.Interfaces
{
    public interface ICategoryService
    {
        CategoryDto? GetCategory(string name); 
        CategoryDto CreateCategory(CreateCategoryDto createCategoryDto); 
        CategoryDto? DeleteCategory(string name); 
        CategoryDto? UpdateCategory(UpdateCategoryDto updateCategoryDto); 
        CategoryDto? GetCategory(int id);
        Category? GetCategoryById(int id);
        List<CategoryDto> GetCategories();



        //Category UpdateCategory(string name, string description);

    }
}
