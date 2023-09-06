using DigitalExaminations.DataAccess;
using DigitalExaminations.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalExaminations.BLL.Services
{
    public interface IGroupService
    {

        PagedResult<GroupViewModel> GetAllGroups(int pageNumber, int pageSize);

        Task<GroupViewModel> AddGroupAsync(GroupViewModel groupVM);

        IEnumerable<Groups> GetAllGroups();

        GroupViewModel GetById(int groupId);
    }
}
