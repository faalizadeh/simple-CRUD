using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShopping.Models;
using OnlineShopping.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static OnlineShopping.Models.CarShopping;
using static OnlineShopping.Models.ViewModel.BrandViewModel;
using static OnlineShopping.Models.ViewModel.CarViewModel;
using static OnlineShopping.Models.ViewModel.CategoryViewModel;
using static OnlineShopping.Models.ViewModel.ColorViewModel;

namespace OnlineShopping.Repository
{
    public class CarRepository : ICarRepository
    {
        private readonly CarShoppingDBContext _context;

        private readonly IHostingEnvironment hostEnvironment;

        public CarRepository(CarShoppingDBContext context, IHostingEnvironment hostEnvironment)
        {
            _context = context;
            this.hostEnvironment = hostEnvironment;
        }


        public async Task<List<BrandViewModel>> GetAllBrands()
        {
            var result = await _context.Brands.Select(p => new BrandViewModel()
            {
                ID = p.ID,
                BrandName = p.BrandName,
                ImageUrl = p.ImageUrl,

            }).ToListAsync();

            return result;
        }

        public async Task<bool> AddNewBrand(BrandViewModel viewModel)
        {
            try
            {
                string imageurl = "";

                //var objImage = AddImage(viewModel.ImageCar);
                if (viewModel.File != null)
                {
                    imageurl = await AddImage(viewModel.File);
                }

                var brand = new Brand()
                {
                    BrandName = viewModel.BrandName,
                    ImageUrl = imageurl,

                };
                await _context.Brands.AddAsync(brand);
                await _context.SaveChangesAsync();



                return true;

            }
            catch
            {
                return false;
            }

        }

        public async Task<BrandVm> GetBrand(int? id)
        {
            var obj = new BrandVm();
            try
            {
                BrandViewModel data = await _context.Brands.Where(p => p.ID == id).Select(s => new BrandViewModel()
                {
                    ID = s.ID,
                    BrandName = s.BrandName,
                    ImageUrl = s.ImageUrl,


                }).FirstOrDefaultAsync();
                obj.IsAccept = true;
                obj.brand = data;
            }
            catch
            {
                obj.IsAccept = false;

            }
            return obj;
        }

        public async Task<bool> UpdateBrand(BrandViewModel viewModel)
        {

            try
            {
                var brand = await _context.Brands.FirstOrDefaultAsync(s => s.ID == viewModel.ID);

                if (viewModel.File != null)
                {
                    string imageurl = await AddImage(viewModel.File);
                    RemoveImage(brand.ImageUrl);
                    brand.ImageUrl = imageurl;

                }

                brand.BrandName = viewModel.BrandName;

                _context.Brands.Update(brand);
                await _context.SaveChangesAsync();



                return true;
            }
            catch
            {
                return false;

            }
        }

        public async Task<bool> DeleteBrand(BrandViewModel viewModel)
        {

            try
            {

                var brand = await _context.Brands.FirstOrDefaultAsync(s => s.ID == viewModel.ID);
                if (brand.ImageUrl != null)
                {
                    string path = Path.Combine(hostEnvironment.WebRootPath, "Gallery", brand.ImageUrl);

                    File.Delete(path);
                }


                _context.Brands.Remove(brand);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                var error = e.Message;
                return false;

            }
        }


        //------------------------------Model----------------------------------------

        //public async Task<List<ModelViewModel>> GetAllModels()
        //{
        //    var AllModel = await _context.Models.Include(x => x.Brand).Select(p => new ModelViewModel()
        //    {

        //        ID = p.ID,
        //        ModelName = p.ModelName,
        //        BrandName = p.Brand.BrandName

        //    }).ToListAsync();
        //    return AllModel;
        //}

        //public async Task<bool> AddNewModel(ModelViewModel viewModel)
        //{
        //    try
        //    {
        //        var model = new Model()
        //        {
        //            ModelName = viewModel.ModelName,
        //            BrandID = viewModel.Brand_Id,
        //        };
        //        await _context.Models.AddAsync(model);
        //        await _context.SaveChangesAsync();
        //        return true;

        //    }
        //    catch
        //    {
        //        return false;
        //    }

        //}


        //public async Task<ModelVm> GetModel(int? id)
        //{
        //    var obj = new ModelVm();
        //    try
        //    {
        //        ModelViewModel data = await _context.Models.Include(x => x.Brand).Where(p => p.ID == id).Select(s => new ModelViewModel()
        //        {
        //            ID = s.ID,
        //            ModelName = s.ModelName,
        //            Brand_Id = s.BrandID,
        //            BrandName = s.Brand.BrandName



        //        }).FirstOrDefaultAsync();
        //        obj.IsAccept = true;
        //        obj.Model = data;
        //    }
        //    catch
        //    {
        //        obj.IsAccept = false;

        //    }
        //    return obj;
        //}


        //public async Task<bool> UpdateModel(ModelViewModel viewModel)
        //{

        //    try
        //    {

        //        var model = await _context.Models.FirstOrDefaultAsync(s => s.ID == viewModel.ID);
        //        model.ModelName = viewModel.ModelName;
        //        model.BrandID = viewModel.Brand_Id;

        //        _context.Models.Update(model);
        //        await _context.SaveChangesAsync();
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;

        //    }
        //}

        //[HttpPost]
        //public async Task<bool> DeleteModel(ModelViewModel viewModel)
        //{

        //    try
        //    {

        //        var model = await _context.Models.FirstOrDefaultAsync(s => s.ID == viewModel.ID);

        //        _context.Models.Remove(model);
        //        await _context.SaveChangesAsync();
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}



        //------------------------------Color----------------------------------------

        public async Task<List<ColorViewModel>> GetAllColors()
        {
            var result = await _context.Colors.Select(p => new ColorViewModel()
            {
                ID = p.ID,
                ColorName = p.ColorName,
                ColorNumber = p.ColorNumber,

            }).ToListAsync();

            return result;
        }


        public async Task<bool> AddNewColor(ColorViewModel viewModel)
        {
            try
            {
                var color = new Color()
                {
                    ColorName = viewModel.ColorName,
                    ColorNumber = viewModel.ColorNumber

                };
                await _context.Colors.AddAsync(color);
                await _context.SaveChangesAsync();
                return true;

            }
            catch
            {
                return false;
            }

        }


        public async Task<ColorVM> GetColor(int? id)
        {
            var obj = new ColorVM();
            try
            {
                ColorViewModel Data = await _context.Colors.Where(c => c.ID == id).Select(s => new ColorViewModel()
                {
                    ID = s.ID,
                    ColorName = s.ColorName,
                    ColorNumber = s.ColorNumber,
                }).FirstOrDefaultAsync();

                obj.Color = Data;
                obj.IsAccept = true;

            }
            catch
            {
                obj.IsAccept = false;

            }
            return obj;
        }

        public async Task<bool> UpdateColor(ColorViewModel viewModel)
        {
            try
            {
                var Data = await _context.Colors.FirstOrDefaultAsync(c => c.ID == viewModel.ID);
                Data.ColorName = viewModel.ColorName;
                Data.ColorNumber = viewModel.ColorNumber;

                _context.Colors.Update(Data);
                await _context.SaveChangesAsync();

                return true;

            }
            catch
            {
                return false;

            }

        }

        public async Task<bool> DeleteColor(ColorViewModel viewModel)
        {

            try
            {

                var color = await _context.Colors.FirstOrDefaultAsync(s => s.ID == viewModel.ID);

                _context.Colors.Remove(color);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;

            }
        }
        //------------------------------Category----------------------------------------
        public async Task<List<CategoryViewModel>> GetAllCategory()
        {
            var result = await _context.Categories.Select(c => new CategoryViewModel()
            {
                ID = c.ID,
                CategoryName = c.CategoryName,
                ImageUrl = c.ImageUrl


            }).ToListAsync();

            return result;
        }

        public async Task<bool> AddNewCategory(CategoryViewModel viewModel)
        {
            try
            {
                string Imageurl = "";

                //var objImage = AddImage(viewModel.ImageCar);
                if (viewModel.File != null)
                {
                    Imageurl = await AddImage(viewModel.File);
                }


                var Category = new Category()
                {
                    CategoryName = viewModel.CategoryName,
                    ImageUrl = Imageurl,
                };
                await _context.Categories.AddAsync(Category);
                await _context.SaveChangesAsync();


                return true;

            }
            catch
            {
                return false;
            }

        }

        public async Task<CategoryVm> GetCategory(int? id)
        {
            CategoryVm obj = new CategoryVm();
            try
            {
                var result = await _context.Categories.Where(c => c.ID == id).Select(p => new CategoryViewModel()
                {
                    ID = p.ID,
                    CategoryName = p.CategoryName,
                    ImageUrl = p.ImageUrl

                }).FirstOrDefaultAsync();

                obj.Category = result;
                obj.IsAccept = true;

            }
            catch (Exception e)
            {
                obj.IsAccept = false;
            }

            return obj;
        }

        public async Task<bool> UpdateCategory(CategoryViewModel viewModel)
        {
            try
            {
                CategoryVm obj = new CategoryVm();

                var Data = await _context.Categories.FirstOrDefaultAsync(c => c.ID == viewModel.ID);

                if (viewModel.File != null)
                {

                    var Imageurl = await AddImage(viewModel.File);
                    RemoveImage(Data.ImageUrl);
                    Data.ImageUrl = Imageurl;
                }


                Data.CategoryName = viewModel.CategoryName;
                _context.Update(Data);
                await _context.SaveChangesAsync();





                return true;


            }
            catch (Exception e)
            {
                return false;
            }

        }

        public async Task<bool> DeleteCategory(CategoryViewModel viewModel)
        {
            try
            {
                var data = await _context.Categories.FirstOrDefaultAsync(c => c.ID == viewModel.ID);
                if (data.ImageUrl != null)
                {
                    RemoveImage(data.ImageUrl);
                }
                _context.Remove(data);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        //------------------------------Car----------------------------------------

        public async Task<List<CarViewModel>> GetAllCar()
        {
            var Data = await _context.Cars.Include(b => b.Brand).Include(c => c.Category).Select(s => new CarViewModel()
            {
                ID = s.ID,
                CategoryName = s.Category.CategoryName,
                Name = s.Name,
                BrandName = s.Brand.BrandName,
                ImageUrl = s.ImageUrl,
                Price = s.Price,
            }).ToListAsync();

            return Data;
        }
        public async Task<bool> AddnewCar(CarViewModel viewModel)
        {
            try
            {
                string imageurl="";

                //var objImage = AddImage(viewModel.ImageCar);
                if (viewModel.File != null)
                {
                     imageurl = await AddImage(viewModel.File);
                }


                var car = new Car()
                {
                    Name = viewModel.Name,
                    CreateDate = DateTime.Now,
                    Price = viewModel.Price,
                    //ProductionDate = "",
                    //ColorID = viewModel.ColorID,
                    CategoryID = viewModel.CategoryID,
                    ImageUrl = imageurl,
                    Description = viewModel.Description,
                    BrandID = viewModel.BrandId,


                };
                await _context.Cars.AddAsync(car);
                await _context.SaveChangesAsync();


                return true;
            }
            catch (Exception m)
            {
                Console.WriteLine(m.InnerException.Message);
                return false;
            }

        }
        public async Task<CarVm> GetCar(int? id)
        {
            var obj = new CarVm();
            try
            {
                CarViewModel data = await _context.Cars.Include(b => b.Brand).Include(c => c.Category).Where(ca => ca.ID == id).Select(s => new CarViewModel()
                {
                    ID = s.ID,
                    Name = s.Name,
                    ImageUrl = s.ImageUrl,
                    BrandId = s.BrandID,
                    BrandName = s.Brand.BrandName,
                    CategoryName = s.Category.CategoryName,
                    CategoryID = s.CategoryID,
                    Description = s.Description,
                    Price = s.Price,



                }).FirstOrDefaultAsync();

                obj.IsAccept = true;
                obj.Car = data;
            }
            catch
            {
                obj.IsAccept = false;

            }
            return obj;
        }

        public async Task<bool> UpdateCar(CarViewModel viewModel)
        {

            try
            {
                var car = await _context.Cars.FirstOrDefaultAsync(s => s.ID == viewModel.ID);

                string imageurl = await UpdateImage(car, viewModel);


                car.Name = viewModel.Name;
                car.Description = viewModel.Description;
                car.Price = viewModel.Price;
                car.ImageUrl = imageurl;
                car.BrandID = viewModel.BrandId;
                car.CategoryID = viewModel.CategoryID;

                _context.Cars.Update(car);
                await _context.SaveChangesAsync();



                return true;
            }
            catch
            {
                return false;

            }
        }

        public async Task<bool> DeleteCar(CarViewModel viewModel)
        {

            try
            {

                var car = await _context.Cars.FirstOrDefaultAsync(s => s.ID == viewModel.ID);
                if (car.ImageUrl != null)
                {
                    string path = Path.Combine(hostEnvironment.WebRootPath, "Gallery", car.ImageUrl);

                    File.Delete(path);
                }


                _context.Cars.Remove(car);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                var error = e.Message;
                return false;

            }
        }


        //------------------------------Image-----------------------------------



        public async Task<string> UpdateImage(Car oldmodel, CarViewModel newmodel)
        {
            if (newmodel.File != null)
            {

                string url = await AddImage(newmodel.File);
                RemoveImage(oldmodel.ImageUrl);
                return url;
            }
            return oldmodel.ImageUrl;


        }

        public async Task<string> AddImage(IFormFile fileData)
        {
            //string path = CreateDirectory("Gallery");
            string pathImage = "";
            try
            {

                using (MemoryStream obj = new MemoryStream())
                {
                    fileData.CopyTo(obj);
                    byte[] Arrays = obj.ToArray();
                    var imageName = Guid.NewGuid() + fileData.FileName;
                    var filePath = Path.Combine(hostEnvironment.WebRootPath, "Gallery", imageName);
                    await File.WriteAllBytesAsync(filePath, Arrays);
                    pathImage = imageName;

                }




                return pathImage;
            }
            catch (Exception E)
            {
                string DirectoryPath = Path.Combine(hostEnvironment.WebRootPath, "Gallery", pathImage);
                if (Directory.Exists(DirectoryPath))
                {
                    Directory.Delete(DirectoryPath);
                }
                var message = E.Message;
                return null;
            }

        }

        public bool RemoveImage(string url)
        {
            try
            {
                string path = Path.Combine(hostEnvironment.WebRootPath, "Gallery");
                File.Delete(Path.Combine(path, url));
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public string CreateDirectory(string directoryName)
        {
            string folderName = "";
            string pathDirectory = "";
            do
            {

                folderName = String.Concat(Guid.NewGuid().ToString());
                pathDirectory = Path.Combine(hostEnvironment.WebRootPath, "Files", directoryName, folderName);


            } while (Directory.Exists(pathDirectory));

            Directory.CreateDirectory(pathDirectory);

            return folderName;
        }

    }
}
