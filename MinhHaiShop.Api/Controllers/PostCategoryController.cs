using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MinhHaiShop.Model.Models;
using MinhHaiShop.Service;
using MinhHaiShop.Web.Infrastructure.Core;
using MinhHaiShop.Web.Models;
using System.Net;

namespace MinhHaiShop.Web.API
{
    [Route("api/[controller]/")]
    [ApiController]
    public class PostCategoryController : ApiControllerBase
    {
        private readonly IPostCategoryService _postCategoryService;
        public PostCategoryController(IErrorService errorService, IPostCategoryService postCategoryService) : base(errorService)
        {
            _postCategoryService = postCategoryService;
        }


        [HttpPost]
        [Route("add")]
        public HttpResponseMessage Create(PostCategoryViewModel postCategoryViewModel)
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
                    
                    var category = _postCategoryService.Add(Mapper.Map<PostCategory>(postCategoryViewModel));
                    _postCategoryService.SaveChanges();
                    response = new HttpResponseMessage(HttpStatusCode.Created);
                    response.Content = new StringContent(category.ToString());
                }
                return response;
            });
        }

        [HttpPost]
        [Route("update")]
        public HttpResponseMessage Update(PostCategoryViewModel postCategoryViewModel)
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
                    var postCategory = _postCategoryService.GetById(postCategoryViewModel.ID);
                    _postCategoryService.Update(Mapper.Map<PostCategory>(postCategory));
                    _postCategoryService.SaveChanges();
                    response = new HttpResponseMessage(HttpStatusCode.OK);
                }
                return response;
            });
        }

        [HttpPost]
        [Route("delete")]
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

        [HttpGet]
        [Route("getall")]
        public HttpResponseMessage GetAll()
        {
            return CreateHttpResponse(() =>
            {
                var listPostCategory = _postCategoryService.GetAll();
                var listPostCategoryViewModel = Mapper.Map<List<PostCategoryViewModel>>(listPostCategory);
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StringContent(listPostCategoryViewModel.ToString());
                return response;
            });
        }
    }
}
