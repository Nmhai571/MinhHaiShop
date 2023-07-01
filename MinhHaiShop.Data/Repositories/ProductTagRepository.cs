﻿using MinhHaiShop.Data.Infrastructure;
using MinhHaiShop.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhHaiShop.Data.Repositories
{
    public interface IProductTagRepository { }
    public class ProductTagRepository : RepositoryBase<ProductTag>, IProductTagRepository
    {
        public ProductTagRepository(IDbFactory dbFactory) : base(dbFactory) { }
    }
}
