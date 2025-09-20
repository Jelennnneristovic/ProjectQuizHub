using QuizHubDomain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizHubInfrastructure.Interfaces
{
    public interface ICategoryRepository
    {
        Category? GetCategory(string name);

        void CreateCategory (Category category);

        void DeleteCategory (Category category);

        void UpdateCategory(Category category);

        Category? GetCategory(int id);
        List<Category> GetCategories();


    }
}
