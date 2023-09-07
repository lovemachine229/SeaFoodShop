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
    public class CategoryController : AuthorizeController
    {
        [BasicAuthorize(ModuleEnum.Category, PermissionEnum.Read)]
        public ActionResult Index()
        {
            return View();
        }

        [BasicAuthorize(ModuleEnum.Category, PermissionEnum.Create)]
        public ActionResult Create()
        {
            return View();
        }

        [BasicAuthorize(ModuleEnum.Category, PermissionEnum.Create)]
        [HttpPost]
        public ActionResult Create(Category obj)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Result = "Có lỗi xảy ra, vui lòng thử lại";
                return View(obj);
            }

            using (var uow = new UnitOfWork(Shared.connString))
            {
                obj.CreatedDate = DateTime.Now;
                var res = uow.CategoryRepository.Create(obj);
                if (res > 0)
                {
                    uow.Commit();
                    TempData["Result"] = "Cập nhật thành công.";
                    return RedirectToAction("Index", "Category");
                }
                else
                {
                    ViewBag.Result = "Có lỗi xảy ra, vui lòng thử lại";
                    return View(obj);
                }

            }
        }

        public JsonResult ListAll(int exclude = 0)
        {
            using (var uow = new UnitOfWork(Shared.connString))
            {
                var res = uow.CategoryRepository.ListAll(exclude);
                return Json(new
                {
                    data = res
                },JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ListAll_Paging(int pageIndex,int pageSize)
        {
            using (var uow = new UnitOfWork(Shared.connString))
            {
                int totalRow = 0;
                var res = uow.CategoryRepository.ListAllPaging(pageIndex, pageSize, ref totalRow);
                var html = RenderHelper.PartialView(this, "_categoryList", res);
                return Json(new
                {
                    status = true,
                    totalRow = totalRow,
                    data = html
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [BasicAuthorize(ModuleEnum.Category, PermissionEnum.Read)]
        public JsonResult ViewDetail(int id)
        {
            using (var uow = new UnitOfWork(Shared.connString))
            {
                return Json(new {
                    data = uow
                });
            }
        }

        [BasicAuthorize(ModuleEnum.Category, PermissionEnum.Update)]
        public ViewResult Edit(int id)
        {
            using (var uow = new UnitOfWork(Shared.connString))
            {
                var res = uow.CategoryRepository.ViewDetail(id);
                return View(res);
            }
        }

        [BasicAuthorize(ModuleEnum.Category, PermissionEnum.Update)]
        [HttpPost]
        public ActionResult Edit(Category obj)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.Result = "Có lỗi xảy ra, vui lòng thử lại";
                return View(obj);
            }

            using (var uow = new UnitOfWork(Shared.connString))
            {
                var res = uow.CategoryRepository.Update(obj);
                if(res > 0)
                {
                    uow.Commit();
                    TempData["Result"] = "Cập nhật thành công.";
                    return RedirectToAction("Index", "Category");
                }
                else
                {
                    ViewBag.Result = "Có lỗi xảy ra, vui lòng thử lại";
                    return View(obj);
                }

            }
        }

        [BasicAuthorize(ModuleEnum.Category, PermissionEnum.Delete)]
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
                    uow.CategoryRepository.Delete(id);
                    uow.Commit();
                    return Json(new
                    {
                        status = true
                    });
                }
            }
        }

        [BasicAuthorize(ModuleEnum.Category, PermissionEnum.Update)]
        [HttpPost]
        public JsonResult ChangeStatus(int id)
        {
            using (var uow = new UnitOfWork(Shared.connString))
            {
                var res = uow.CategoryRepository.ChangeStatus(id);
                if (res)
                    uow.Commit();
                return Json(new { status = res });
            }
        }
    }
}