using Business.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Utilities.Results;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using System.ComponentModel.DataAnnotations;
using Core.CrossCuttingConcerns.Validation;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        private IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }
        //Cross Cutting  Concerns   -  Validation, Cache, Log, Performance, Auth, Transaction
        //AOP - Aspect Oriented Programming
        //[ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product product)
        {
            //Business Code
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded); 
        }

        public IResult Delete(Product product)
        {
            _productDal.Delete(product);
            return new SuccessResult(Messages.ProductDeleted);
        }

        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(filter: p => p.ProductId == productId)) ;
        }

        public IDataResult<List<Product>> GetList()
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetList().ToList()) ;
        }

        public IDataResult<List<Product>> GetListByCategory(int categoryId)
        {
            return new SuccessDataResult<List<Product>> (_productDal.GetList(filter: p => p.CategoryId == categoryId).ToList());
        }

        public IResult Update(Product product)
        {
             _productDal.Update(product);
            return new SuccessResult(Messages.ProductUpdated);
        }
    }
}
