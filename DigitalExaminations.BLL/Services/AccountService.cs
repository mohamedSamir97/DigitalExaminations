using DigitalExaminations.DataAccess;
using DigitalExaminations.DataAccess.UnitOfWork;
using DigitalExaminations.ViewModels;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalExaminations.BLL.Services
{
    public class AccountService : IAccountService
    {
        IUnitOfWork _unitOfWork;
        ILogger<StudentService> _iLogger;
        public AccountService()
        {

        }

        public AccountService(IUnitOfWork unitOfWork, ILogger<StudentService> iLogger)
        {
            _unitOfWork = unitOfWork;
            _iLogger = iLogger;
        }

        public async Task<bool> AddUser(UserViewModel vm)
        {
            try
            {
                Users obj = new Users()
                {

                    Name = vm.Name,
                    UserName = vm.UserName,
                    Password = vm.Password,
                    Role = vm.Role
                };
                await _unitOfWork.GenericRepository<Users>().AddAsync(obj);

                if (vm.Role == (int)EnumRoles.Student)
                {
                    var student = new Students
                    {
                        Name = vm.Name,
                        UserName = vm.UserName,
                        Password = vm.Password,
                        GroupsId =1, //default group 
                        Contact = "",
                        CVFileName = "",
                        PictureFileName = ""

                    };
                    await _unitOfWork.GenericRepository<Students>().AddAsync(student);
                }

                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {

                _iLogger.LogError(ex.Message);
                return false;
            }
            return true;
        }


        public PagedResult<UserViewModel> GetAllTeachers(int pageNumber, int pageSize)
        {
            var model = new UserViewModel();
            try
            {
                int ExcludeRecords = (pageSize * pageNumber) - pageSize;
                List<UserViewModel> detailList = new List<UserViewModel>();
                var modelList = _unitOfWork.GenericRepository<Users>().GetAll()
                    .Where(x => x.Role == (int)EnumRoles.Teacher)
                    .Skip(ExcludeRecords)
                    .Take(pageSize).ToList();

                detailList = ListInfo(modelList);
                if (detailList != null)
                {
                    model.UserList = detailList;
                    model.TotalCount = _unitOfWork.GenericRepository<Users>().GetAll()
                        .Count(x => x.Role == (int)EnumRoles.Teacher);
                }
            }
            catch (Exception ex)
            {

                _iLogger.LogError(ex.Message);
            }

            var results = new PagedResult<UserViewModel>
            {
                Data = model.UserList,
                TotalItems = model.TotalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return results;

        }

        private List<UserViewModel> ListInfo(List<Users> modelList)
        {
            return modelList.Select(o => new UserViewModel(o)).ToList();
        }

        public LoginViewModel Login(LoginViewModel vm)
        {
            if (vm.Role == (int)EnumRoles.Admin || vm.Role == (int)EnumRoles.Teacher)
            {
                var user = _unitOfWork.GenericRepository<Users>().GetAll().
                    FirstOrDefault(a => a.UserName == vm.UserName.Trim()
                    && a.Password == vm.Password.Trim() && a.Role == vm.Role);
                if (user != null)
                {
                    vm.Id = user.Id;
                    return vm;
                }
            }
            else
            {
                var student = _unitOfWork.GenericRepository<Users>().GetAll()
                    .FirstOrDefault(a => a.UserName == vm.UserName.Trim()
                    && a.Password == vm.Password.Trim());
                if (student != null)
                {
                    vm.Id = student.Id;

                }
                return vm;

            }

            return null;
        }


        public  bool UserExistsAsync(string username, int roleId)
        {
            return _unitOfWork.GenericRepository<Users>().UserExistsAsync(username, roleId);
        }
    }
}
