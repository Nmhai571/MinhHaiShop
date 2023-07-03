using Microsoft.AspNetCore.Mvc;
using MinhHaiShop.Model.Models;
using MinhHaiShop.Service;
using MinhHaiShop.Web.Infrastructure.Core;
using System.Net;

namespace MinhHaiShop.Web.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostCategoryController : ApiControllerBase
    {
        IPosstCategoryService _postCategoryService;
        public PostCategoryController(IErrorService errorService, IPosstCategoryService postCategoryService) : base(errorService)
        {
            _postCategoryService = postCategoryService;
        }

        public HttpResponseMessage Create(PostCategory postCategory)
        {
            return CreateHttpResponse(() =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    string modelStateError = string.Join(" | ", ModelState.Values);
                    response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                    response.Content = new StringContent(modelStateError);
                }
                else
                {
                    var category = _postCategoryService.Add(postCategory);
                    _postCategoryService.SaveChanges();
                    response = new HttpResponseMessage(HttpStatusCode.Created);
                    response.Content = new StringContent(category.ToString());
                }
                return response;
            });
        }

        public HttpResponseMessage Update(PostCategory postCategory)
        {
            return CreateHttpResponse(() =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    string modelStateError = string.Join(" | ", ModelState.Values);
                    response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                    response.Content = new StringContent(modelStateError);
                }
                else
                {
                    _postCategoryService.Update(postCategory);
                    _postCategoryService.SaveChanges();
                    response = new HttpResponseMessage(HttpStatusCode.OK);
                }
                return response;
            });
        }


        public HttpResponseMessage Delete(int id)
        {
            return CreateHttpResponse(() =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    string modelStateError = string.Join(" | ", ModelState.Values);
                    response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                    response.Content = new StringContent(modelStateError);
                }
                else
                {
                    _postCategoryService.Delete(id);
                    _postCategoryService.SaveChanges();
                    response = new HttpResponseMessage(HttpStatusCode.OK);
                }
                return response;
            });
        }


        public HttpResponseMessage GetAll()
        {
            return CreateHttpResponse(() =>
            {
                HttpResponseMessage response = null;
                if (ModelState.IsValid)
                {
                    string modelStateError = string.Join(" | ", ModelState.Values);
                    response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                    response.Content = new StringContent(modelStateError);
                }
                else
                {
                    var listPostCategory = _postCategoryService.GetAll();
                    response = new HttpResponseMessage(HttpStatusCode.OK);
                    response.Content = new StringContent(listPostCategory.ToString());
                }
                return response;
            });
        }
    }
}
