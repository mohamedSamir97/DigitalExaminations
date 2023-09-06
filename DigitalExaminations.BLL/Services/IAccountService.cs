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
        bool AddTeacher(UserViewModel vm);

        PagedResult<UserViewModel> GetAllTeachers(int pageNumber, int pageSize);
    }
}
