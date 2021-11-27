using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using WeLoveBooks.DataAccess.Models;

namespace WeLoveBooks.DataAccess.Data.Seeder;

public interface ISeeder
{
    Task Initialize();
}
