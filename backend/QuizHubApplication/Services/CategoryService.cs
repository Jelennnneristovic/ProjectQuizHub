using Microsoft.Identity.Client;
using QuizHubApplication.Configuration;
using QuizHubApplication.DTOs.Requests;
using QuizHubApplication.DTOs.Responses;
using QuizHubApplication.Exceptions;
using QuizHubApplication.Interfaces;
using QuizHubDomain.Entities;
using QuizHubInfrastructure.Interfaces;
using QuizHubInfrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace QuizHubApplication.Services
{
    public class CategoryService(ICategoryRepository categoryRepository) : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository = categoryRepository;

        public CategoryDto CreateCategory(CreateCategoryDto createCategoryDto)
        {
            Category? category = _categoryRepository.GetCategory(createCategoryDto.Name);

            if (category is not null)
            {
                throw new EntityAlreadyExists(string.Format("The category '{0}' already exists.", category.Name));
            }

            Category newCategory = new (createCategoryDto.Name, createCategoryDto.Description);
            _categoryRepository.CreateCategory(newCategory);
            return new CategoryDto(newCategory.Id, newCategory.Name, newCategory.Description); 

        }

        public CategoryDto? GetCategory(string name)
        {
            Category? category = _categoryRepository.GetCategory(name);
            if (category is null)

                { return null; }

            return new CategoryDto(category.Id, category.Name, category.Description);
        }

        public CategoryDto? DeleteCategory(string name)
        {
            Category? category = _categoryRepository.GetCategory(name);

            //greska, brise nesto sto ne postoji
            if (category == null)
            {
                return null;
            }
            // ako nije null brise ga
            _categoryRepository.DeleteCategory(category);
            return new CategoryDto(category.Id, category.Name, category.Description);
        
        }

        public CategoryDto? UpdateCategory(UpdateCategoryDto updateCategoryDto)
        {

            //nije pga pronasao, ovde su mi svi podaci tog objekta
            Category? category = _categoryRepository.GetCategory(updateCategoryDto.OldName);
           
            if (category is null)

            { return null; }

            // update polje samo ako je IsNullOrWhiteSpace, inace ostavlja staru vrednost

            if (!string.IsNullOrWhiteSpace(updateCategoryDto.NewName))
                category.Name = updateCategoryDto.NewName;

            if (!string.IsNullOrWhiteSpace(updateCategoryDto.Description))
                category.Description = updateCategoryDto.Description;

            
            _categoryRepository.UpdateCategory(category);

            return new CategoryDto(category.Id,category.Name, category.Description);

        }

        public CategoryDto? GetCategory(int id)
        {
            Category? category = _categoryRepository.GetCategory(id);
            if (category is null)

            { return null; }

            return new CategoryDto(category.Id,category.Name, category.Description);
        }

        public Category? GetCategoryById(int id)
        {
            
            return _categoryRepository.GetCategory(id);
        }

        public List<CategoryDto> GetCategories()
        {
            List<Category> categories= _categoryRepository.GetCategories();
            List<CategoryDto> result = [];

            foreach (Category category in categories)
            {
                CategoryDto categoryDto = new (
                    category.Id,
                    category.Name,
                    category.Description
                );
                result.Add(categoryDto);
            } 
            return result;
        }
    }
}
