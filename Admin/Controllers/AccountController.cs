using Common;
using Services.Repository;
using Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Admin.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel obj)
        {
            if (!ModelState.IsValid)
                return View(obj);
            using (var uow = new UnitOfWork(Shared.connString))
            {
                obj.Password = Common.ExtendMethod.MD5EnCryptor(obj.Password + Shared.MD5_KEY);
                var employee = uow.EmployeeRepository.Login(obj);
                if(employee != null)
                {
                    employee.Role = uow.RoleRepository.ViewDetail(employee.RoleId);
                    employee.Permissions = uow.PermissionRepository.GetPermissions(employee.RoleId);
                    Session.Add(Shared.Session_Admin, employee);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Tài khoản hoặc mật khẩu không đúng.");
                    return View();
                }
            }
        }

        public ActionResult Logout()
        {
            Session.Remove(Shared.Session_Admin);
            return RedirectToAction("Login", "Account");
        }

        public ActionResult UnAuthorized()
        {
            return View();
        }
    }
}