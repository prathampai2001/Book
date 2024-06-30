using BookNetCore.DataAccess.Data;
using BookNetCore.DataAccess.Repository;
using BookNetCore.DataAccess.Repository.IRepository;
using BookNetCore.Models;
using BookNetCore.Models.ViewModels;
using BookNetCore.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookNetCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles =SD.Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
       // private readonly IWebHostEnvironment _webHostEnvironment;
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            //_webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();
           
            return View(objCompanyList);
        }

        public IActionResult Upsert(int ? id, Company company)
        {
           // IEnumerable<SelectListItem> CategoryList =

            //ViewBag.CategoryList = CategoryList;
            // ViewData["CategoryList"] = CategoryList;

            //CompanyVM companyVM = new()
            //{
            //    CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
            //   {
            //       Text = u.Name,
            //       Value = u.Id.ToString()
            //   }),
            //Company = new Company()
            //};
            if (id == null || id == 0)
            {
                //Create
                return View(company);
            }
            else
            {
                //Update
                Company companyObj= _unitOfWork.Company.Get(u=>u.Id==id);
                return View(companyObj);
            }
          
        }

      

        [HttpPost]
        public IActionResult Upsert(Company CompanyObj)
        {
            if (ModelState.IsValid)
            {
               
                if (CompanyObj.Id == 0)
                {
                    _unitOfWork.Company.Add(CompanyObj);
                }
                else
                {
                    _unitOfWork.Company.Update(CompanyObj);
                }

               
                _unitOfWork.Save();
                TempData["success"] = "Company Created Successfully";
                return RedirectToAction("Index");
            }
            else
            {
                //companyVM.CategoryList = _unitOfWork.Category.GetAll().Select(u => new SelectListItem
                //{
                //    Text = u.Name,
                //    Value = u.Id.ToString()
                //});
                
                return View(CompanyObj);
            }
        }


        //public IActionResult Edit(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    Company? companyfromDb = _unitOfWork.Company.Get(u => u.Id == id);
        //    //Company? companyfromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
        //    //Company? companyfromDb2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();
        //    if (companyfromDb == null)
        //    {

        //        return NotFound();
        //    }
        //    return View(companyfromDb);
        //}

        //[HttpPost]
        //public IActionResult Edit(Company obj)
        //{
        //    //if (obj.Name == obj.DisplayOrder.ToString())
        //    //{
        //    //    ModelState.AddModelError("name", "The DisplayOrder cannot exactly Match the Name.");
        //    //}


        //    if (ModelState.IsValid)
        //    {
        //        _unitOfWork.Company.Update(obj);
        //        _unitOfWork.Save();
        //        TempData["success"] = "Company Updated Successfully";
        //        return RedirectToAction("Index");
        //    }
        //    return View();

        //}





        //public IActionResult Delete(int? id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    Company? companyfromDb = _unitOfWork.Company.Get(u => u.Id == id);

        //    if (companyfromDb == null)
        //    {

        //        return NotFound();
        //    }
        //    return View(companyfromDb);
        //}

        //[HttpPost, ActionName("Delete")]
        //public IActionResult DeletePOST(int? id)
        //{
        //    Company obj = _unitOfWork.Company.Get(u => u.Id == id);
        //    if (obj == null)
        //    {
        //        return NotFound();
        //    }
        //    _unitOfWork.Company.Remove(obj);
        //    _unitOfWork.Save(); ;
        //    TempData["success"] = "Company Deleted Successfully";
        //    return RedirectToAction("Index");



        //}

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();
            return Json(new { data = objCompanyList });
        
        }


        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var companyToBeDeleted=_unitOfWork.Company.Get(u=>u.Id == id);
            if(companyToBeDeleted == null)
            {
                return Json(new {success=false,message="Error while deleting"});
            }

            _unitOfWork.Company.Remove(companyToBeDeleted);
            _unitOfWork.Save(); 

    
            return Json(new { success=true,message="Delete Successful"});

        }



        #endregion



    }
}



