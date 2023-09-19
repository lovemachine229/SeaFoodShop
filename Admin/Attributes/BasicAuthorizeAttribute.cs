using Common;
using Services.Enum;
using Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Admin.Attributes
{
    public class BasicAuthorizeAttribute : AuthorizeAttribute
    {
        public ModuleEnum? Module { get; set; }
        public PermissionEnum? Permission { get; set; }

        public BasicAuthorizeAttribute()
        {

        }

        public BasicAuthorizeAttribute(ModuleEnum module, PermissionEnum permission)
        {
            this.Module = module;
            this.Permission = permission;
        }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            bool authorize = false;

            var employee = httpContext.Session[Shared.Session_Admin] as Employee;
            if (employee != null)
            {
                if(Module == null || Permission == null)
                {
                    authorize = true;
                }
                else
                {
                    var permission = employee.Permissions.FirstOrDefault(_ => _.Module == Module.ToString() && (bool)_.GetType().GetProperty(Permission.ToString()).GetValue(_));
                    if (permission != null)
                    {
                        authorize = true;
                    }
                }
            }

            return authorize;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (filterContext.HttpContext.Session[Shared.Session_Admin] == null)
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Account", action = "Login" }));
            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Account", action = "UnAuthorized" }));
            }
        }
    }
}