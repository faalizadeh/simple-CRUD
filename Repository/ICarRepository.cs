using OnlineShopping.Models;
using OnlineShopping.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static OnlineShopping.Models.CarShopping;
using static OnlineShopping.Models.ViewModel.BrandViewModel;
using static OnlineShopping.Models.ViewModel.CarViewModel;
using static OnlineShopping.Models.ViewModel.CategoryViewModel;
using static OnlineShopping.Models.ViewModel.ColorViewModel;

namespace OnlineShopping.Repository
{
    public interface ICarRepository
    {
        //----------------------------Brand-------------------------

        Task<List<BrandViewModel>> GetAllBrands();
        Task<bool> AddNewBrand(BrandViewModel viewModel);
        Task<BrandVm> GetBrand(int? id);
        Task<bool> UpdateBrand(BrandViewModel viewModel);
        Task<bool> DeleteBrand(BrandViewModel viewModel);
        //----------------------------Model-------------------------

        //Task<List<ModelViewModel>> GetAllModels();
        //Task<bool> AddNewModel(ModelViewModel viewModel);
        //Task<ModelVm> GetModel(int? id);
        //Task<bool> UpdateModel(ModelViewModel viewModel);
        //Task<bool> DeleteModel(ModelViewModel viewModel);
        //----------------------------Color-------------------------

        Task<List<ColorViewModel>> GetAllColors();
        Task<bool> AddNewColor(ColorViewModel viewModel);
        Task<ColorVM> GetColor(int? id);
        Task<bool> UpdateColor(ColorViewModel viewModel);
        Task<bool> DeleteColor(ColorViewModel viewModel);
        //----------------------------Category-------------------------

        Task<List<CategoryViewModel>> GetAllCategory();
        Task<bool> AddNewCategory(CategoryViewModel viewModel);
        Task<CategoryVm> GetCategory(int? id);
        Task<bool> UpdateCategory(CategoryViewModel viewModel);
        Task<bool> DeleteCategory(CategoryViewModel viewModel);
        //-------------------------------Car-------------------------
        Task<bool> AddnewCar(CarViewModel viewModel);
        Task<List<CarViewModel>> GetAllCar();
        Task<CarVm> GetCar(int? id);
        Task<bool> UpdateCar(CarViewModel viewModel);
        Task<bool> DeleteCar(CarViewModel viewModel);
    }
}
