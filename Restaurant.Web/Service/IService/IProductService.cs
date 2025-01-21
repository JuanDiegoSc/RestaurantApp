﻿using Restaurant.Web.Models;

namespace Restaurant.Web.Service.IService
{
    public interface IProductService
    {
        Task<ResponseDto?> GetProductAsync(string productName);
        Task<ResponseDto?> GetAllProductsAsync();
        Task<ResponseDto?> GetProductByIdAsync(int id);
        Task<ResponseDto?> CreateProductAsync(ProductDto couponDto);
        Task<ResponseDto?> UpdateProductAsync(ProductDto couponDto);
        Task<ResponseDto?> DeleteProductAsync(int id);

    }
}
