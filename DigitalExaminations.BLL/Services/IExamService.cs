using DigitalExaminations.DataAccess;
using DigitalExaminations.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalExaminations.BLL.Services
{
    public interface IExamService
    {

        PagedResult<ExamViewModel> GetAll(int pageNumber, int pageSize);
        Task<ExamViewModel> AddAsync(ExamViewModel examVM);
        IEnumerable<Exams> GetAllExams();
    }
}
