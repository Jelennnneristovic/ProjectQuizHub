using QuizHubDomain.Entities;
using QuizHubInfrastructure.Data;
using QuizHubInfrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizHubInfrastructure.Repositories
{
    public class CategoryRepository (QuizHubDbContext context): ICategoryRepository
    {
        private readonly QuizHubDbContext _context = context;


        public void CreateCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
        }

        public Category? GetCategory(string name)
        {
           return _context.Categories.FirstOrDefault(x => x.Name == name && x.IsActive);
             
        }


        public void DeleteCategory(Category category)
        {
            category.IsActive= false;
            _context.Categories.Update(category);
             _context.SaveChanges();

        }

        public void UpdateCategory(Category category)
        {
            
            _context.Categories.Update(category);
            _context.SaveChanges();

        }

        public Category? GetCategory(int id)
        {
            return _context.Categories.FirstOrDefault(x => x.Id == id && x.IsActive);

        }

        public List<Category> GetCategories()
        {
            return [.. context.Categories.Where(x => x.IsActive)
                .Select(c => new Category
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description,
                    IsActive = c.IsActive

                })];
        }
    }
}
