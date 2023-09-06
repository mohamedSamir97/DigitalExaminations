using DigitalExaminations.DataAccess;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Xml.Linq;

namespace DigitalExaminations.ViewModels
{
    public class UserViewModel
    {
        public UserViewModel()
        {

        }
        //instead we can use auto mapper
        public UserViewModel(Users model)
        {
            Id = model.Id;
            Name = model.Name ?? "";
            Password = model.Password;
            UserName = model.UserName;
            Role = model.Role;
        }


        public Users ConvertViewModel(UserViewModel vm)
        {
            return new Users
            {
                Id = vm.Id,
                Name = vm.Name ?? "",
                UserName = vm.UserName,
                Password = vm.Password,
                Role = vm.Role

            };
        
        }

        public int Id { get; set; }
        [Required]
        [Display(Name = "Name")]

        public string Name { get; set; }
        [Required]
        [Display(Name = "User Name")]

        public string UserName { get; set; }
        [Required]

        public string Password { get; set; }

        public int Role { get; set; }
        public List<UserViewModel>? UserList{ get; set; }
        public int TotalCount { get; set; }
    }
}