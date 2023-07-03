using MinhHaiShop.Data.Infrastructure;
using MinhHaiShop.Data.Repositories;
using MinhHaiShop.Model.Models;

namespace MinhHaiShop.Service
{
    public interface IPostCategoryService
    {
        PostCategory Add(PostCategory postCategory);

        void Update(PostCategory postCategory);

        void Delete(int id);

        IEnumerable<PostCategory> GetAll();

        IEnumerable<PostCategory> GetAllByParentId(int parentId);

        PostCategory GetById(int id);

        void SaveChanges();
    }
    public class PostCategoryService : IPostCategoryService
    {
        IPostCategotyRepository _postCategoryRepository;
        IUnitOfWork _unitOfWork;

        public PostCategoryService(IPostCategotyRepository postCategotyRepository, IUnitOfWork unitOfWork)
        {
            _postCategoryRepository = postCategotyRepository;
            _unitOfWork = unitOfWork;
        }
        public PostCategory Add(PostCategory postCategory)
        {
            return _postCategoryRepository.Create(postCategory);
        }

        public void Delete(int id)
        {
            _postCategoryRepository.Delete(id);
        }

        public IEnumerable<PostCategory> GetAll()
        {
            return _postCategoryRepository.GetAll();
        }

        public IEnumerable<PostCategory> GetAllByParentId(int parentId)
        {
            return _postCategoryRepository.GetMulti(x => x.Status && x.ParentID == parentId);
        }

        public PostCategory GetById(int id)
        {
            return _postCategoryRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(PostCategory postCategory)
        {
            _postCategoryRepository.Update(postCategory);
        }
    }
}
