﻿using DigitalExaminations.DataAccess;
using DigitalExaminations.DataAccess.UnitOfWork;
using DigitalExaminations.ViewModels;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DigitalExaminations.BLL.Services
{
    public class ExamService : IExamService
    {
        IUnitOfWork _unitOfWork;
        ILogger<ExamService> _iLogger;

        public ExamService(IUnitOfWork unitOfWork, ILogger<ExamService> iLogger)
        {
            _unitOfWork = unitOfWork;
            _iLogger = iLogger;
        }

        public async Task<ExamViewModel> AddAsync(ExamViewModel examVM)
        {
            try
            {
                Exams objExam = examVM.ConvertViewModel(examVM);
                await _unitOfWork.GenericRepository<Exams>().AddAsync(objExam);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {

                _iLogger.LogError(ex.Message);
                return null;
            }
            return examVM;
        }

        public PagedResult<ExamViewModel> GetAll(int pageNumber, int pageSize)
        {
            var model = new ExamViewModel();
            try
            {
                int ExcludeRecords = (pageSize * pageNumber) - pageSize;
                List<ExamViewModel> detailList = new List<ExamViewModel>();
                var modelList = _unitOfWork.GenericRepository<Exams>().GetAll()
                    .Skip(ExcludeRecords).Take(pageSize).ToList();
                var totalCount = _unitOfWork.GenericRepository<Exams>().GetAll()
                    .ToList();
                detailList = ListInfo(modelList);
                if (detailList != null)
                {
                    model.ExamList = detailList;
                    model.TotalCount = totalCount.Count();
                }
            }
            catch (Exception ex)
            {

                _iLogger.LogError(ex.Message);
            }
            var result = new PagedResult<ExamViewModel>
            {
                Data = model.ExamList,
                TotalItems = model.TotalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return result;
        }

        private List<ExamViewModel> ListInfo(List<Exams> modelList)
        {
            return modelList.Select(o => new ExamViewModel(o)).ToList();
        }

        public IEnumerable<Exams> GetAllExams()
        {
            try
            {
                var exams = _unitOfWork.GenericRepository<Exams>().GetAll();
                return exams;
            }
            catch (Exception ex)
            {

                _iLogger.LogError(ex.Message);
            }
            return Enumerable.Empty<Exams>();
        }
    }
}
