using MyShop.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;

namespace MyShop.DataAccess.InMemory
{
    public class ProductCategoryRepository
    {
        ObjectCache cache = MemoryCache.Default;
        List<ProductCategory> productCategory;

        public ProductCategoryRepository()
        {
            productCategory = cache["productCategories"] as List<ProductCategory>;
            if (productCategory == null)
            {
                productCategory = new List<ProductCategory>();
            }
        }

        public void Commit()
        {
            cache["productCategories"] = productCategory;
        }

        public void Insert(ProductCategory model)
        {
            productCategory.Add(model);
        }

        public void Update(ProductCategory model)
        {
            ProductCategory ProductCategoryToUpdate = productCategory.Find(pc => pc.Id == model.Id);
            if (ProductCategoryToUpdate != null)
            {
                ProductCategoryToUpdate = model;
            }
            else
            {
                throw new Exception("Product category not found.");
            }
        }

        public ProductCategory Find(string Id)
        {
            ProductCategory ProductCategoryToFind = productCategory.Find(pc => pc.Id == Id);
            if (ProductCategoryToFind != null)
            {
                return ProductCategoryToFind;
            }
            else
            {
                throw new Exception("Product category not found.");
            }
        }

        public IQueryable<ProductCategory> Collection()
        {
            return productCategory.AsQueryable();
        }

        public void Delete(string Id)
        {
            ProductCategory ProductCategoryToDelete = productCategory.Find(pc => pc.Id == Id);
            if (ProductCategoryToDelete != null)
            {
                productCategory.Remove(ProductCategoryToDelete);
            }
            else
            {
                throw new Exception("Product category not found.");
            }

        }
    }
}
