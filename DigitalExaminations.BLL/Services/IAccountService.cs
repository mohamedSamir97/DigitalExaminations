using DigitalExaminations.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalExaminations.BLL.Services
{
    public interface IAccountService
    {
        LoginViewModel Login(LoginViewModel vm);
        Task<bool> AddUser(UserViewModel vm);

       bool UserExistsAsync(string username, int roleId);

        PagedResult<UserViewModel> GetAllTeachers(int pageNumber, int pageSize);
    }
}
