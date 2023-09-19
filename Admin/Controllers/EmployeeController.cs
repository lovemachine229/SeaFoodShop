using Admin.Attributes;
using Admin.Helper;
using Common;
using Services.Enum;
using Services.Models;
using Services.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Admin.Controllers
{
    public class EmployeeController : AuthorizeController
    {
        [BasicAuthorize(ModuleEnum.Employee, PermissionEnum.Read)]
        public ActionResult Index()
        {
            return View();
        }

        [BasicAuthorize(ModuleEnum.Employee, PermissionEnum.Create)]
        public ActionResult Create()
        {
            return View();
        }

        [BasicAuthorize(ModuleEnum.Employee, PermissionEnum.Create)]
        [HttpPost]
        public ActionResult Create(Employee obj)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Result = "Có lỗi xảy ra, vui lòng thử lại";
                return View(obj);
            }

            using (var uow = new UnitOfWork(Shared.connString))
            {
                obj.CreatedDate = DateTime.Now;
                obj.Password = ExtendMethod.MD5EnCryptor(obj.Password + Shared.MD5_KEY); 
                var res = uow.EmployeeRepository.Create(obj);
                if (res > 0)
                {
                    uow.Commit();
                    TempData["Result"] = "Cập nhật thành công.";
                    return RedirectToAction("Index", "Employee");
                }
                else
                {
                    ViewBag.Result = "Có lỗi xảy ra, vui lòng thử lại";
                    return View(obj);
                }

            }
        }

        public JsonResult ListAll_Paging(int pageIndex,int pageSize)
        {
            using (var uow = new UnitOfWork(Shared.connString))
            {
                int totalRow = 0;
                var res = uow.EmployeeRepository.ListAllPaging(pageIndex, pageSize, ref totalRow);
                var html = RenderHelper.PartialView(this, "_EmployeeList", res);
                return Json(new
                {
                    status = true,
                    totalRow = totalRow,
                    data = html
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ViewDetail(int id)
        {
            using (var uow = new UnitOfWork(Shared.connString))
            {
                return Json(new {
                    data = uow
                });
            }
        }

        [BasicAuthorize(ModuleEnum.Employee, PermissionEnum.Update)]
        public ViewResult Edit(int id)
        {
            using (var uow = new UnitOfWork(Shared.connString))
            {
                var res = uow.EmployeeRepository.ViewDetail(id);
                return View(res);
            }
        }

        [BasicAuthorize(ModuleEnum.Employee, PermissionEnum.Update)]
        [HttpPost]
        public ActionResult Edit(Employee obj)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.Result = "Có lỗi xảy ra, vui lòng thử lại";
                return View(obj);
            }

            using (var uow = new UnitOfWork(Shared.connString))
            {
                if(!String.IsNullOrEmpty(obj.Password))
                {
                    obj.Password = ExtendMethod.MD5EnCryptor(obj.Password + Shared.MD5_KEY);
                }

                var res = uow.EmployeeRepository.Update(obj);
                if(res > 0)
                {
                    uow.Commit();
                    TempData["Result"] = "Cập nhật thành công.";
                    return RedirectToAction("Index", "Employee");
                }
                else
                {
                    ViewBag.Result = "Có lỗi xảy ra, vui lòng thử lại";
                    return View(obj);
                }

            }
        }

        [BasicAuthorize(ModuleEnum.Employee, PermissionEnum.Delete)]
        [HttpPost]
        public JsonResult Delete(int id)
        {
            using (var uow = new UnitOfWork(Shared.connString))
            {
                var numberOfProducts = uow.ProductRepository.CountByCate(id);
                if(numberOfProducts > 0)
                {
                    return Json(new
                    {
                        status = false
                    });
                }
                else
                {
                    uow.EmployeeRepository.Delete(id);
                    uow.Commit();
                    return Json(new
                    {
                        status = true
                    });
                }
            }
        }

        [BasicAuthorize(ModuleEnum.Employee, PermissionEnum.Update)]
        [HttpPost]
        public JsonResult ChangeStatus(int id)
        {
            using (var uow = new UnitOfWork(Shared.connString))
            {
                var res = uow.EmployeeRepository.ChangeStatus(id);
                if (res)
                    uow.Commit();
                return Json(new { status = res });
            }
        }
    }
}