using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinhHaiShop.Model.Models;
using MinhHaiShop.Service;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.Net;

namespace MinhHaiShop.Web.Infrastructure.Core
{
    /*    [Route("api/[controller]")]
        [ApiController]*/
    public class ApiControllerBase : ControllerBase
    {
        private IErrorService _errorService;
        public ApiControllerBase(IErrorService errorService)
        {
            _errorService = errorService;
        }
        protected HttpResponseMessage CreateHttpResponse(Func<HttpResponseMessage> function)
        {
            HttpResponseMessage response = null;
            try
            {
                response = function.Invoke();

            }
            catch (DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    Trace.WriteLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation error.");
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Trace.WriteLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                    }
                }
                LogError(ex);
                response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                response.Content = new StringContent(ex.InnerException.Message);
            }
            catch (DbUpdateException dbEx)
            {
                LogError(dbEx);
                response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                response.Content = new StringContent(dbEx.InnerException.Message);
            }
            catch (Exception ex)
            {
                LogError(ex);
                response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                response.Content = new StringContent(ex.Message);
            }
            return response;
        }

        private void LogError(Exception ex)
        {
            try
            {
                Error error = new Error();
                error.Message = ex.Message;
                error.CreatedDate = DateTime.Now;
                error.StackTrace = error.StackTrace;
                _errorService.Create(error);
                _errorService.Save();
            }
            catch { }
        }
    }
}
