﻿using MinhHaiShop.Data.Infrastructure;
using MinhHaiShop.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhHaiShop.Data.Repositories
{
    public  interface IPostCategotyRepository : IRepository<PostCategory> { }
    public  class PostCategoryRepository : RepositoryBase<PostCategory>, IPostCategotyRepository
    {
        public PostCategoryRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }
}
