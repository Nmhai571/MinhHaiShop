using MinhHaiShop.Data.Infrastructure;
using MinhHaiShop.Data.Repositories;
using MinhHaiShop.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhHaiShop.Service
{
    public interface IErrorService
    {
        void Create(Error error);
        void Save();
    }
    public class ErrorService : IErrorService
    {
        IErrorRepository _errorRepository;
        IUnitOfWork _unitOfWork;

        public ErrorService(IErrorRepository errorRepository, IUnitOfWork unitOfWork)
        {
            _errorRepository = errorRepository;
            _unitOfWork = unitOfWork;
        }
        public void Create(Error error)
        {
             _errorRepository.Add(error);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }
    }
}
