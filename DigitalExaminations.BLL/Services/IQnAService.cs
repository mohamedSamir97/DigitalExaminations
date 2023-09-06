using DigitalExaminations.DataAccess;
using DigitalExaminations.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalExaminations.BLL.Services
{
    public interface IQnAService
    {
        PagedResult<QnAsViewModel> GetAll(int pageNumber, int pageSize);
        Task<QnAsViewModel> AddAsync(QnAsViewModel QnAVM);
        IEnumerable<QnAsViewModel> GetAllQnAByExam(int examId);
        bool IsExamAttended(int examId, int studentId);
    }
}
