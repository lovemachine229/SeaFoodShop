using Admin.Attributes;
using Admin.Helper;
using Common;
using Services.Enum;
using Services.Repository;
using Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Admin.Controllers
{
    public class OrderController : AuthorizeController
    {
        [BasicAuthorize(ModuleEnum.Order, PermissionEnum.Read)]
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ListAll_Paging(int pageIndex, int pageSize, int orderId = 0, string email = "")
        {
            using (var uow = new UnitOfWork(Shared.connString))
            {
                int totalRow = 0;
                var res = uow.OrderRepository.ListAll_Paging(pageIndex, pageSize, ref totalRow, orderId, email);
                var html = RenderHelper.PartialView(this, "_orderList", res);
                return Json(new
                {
                    status = true,
                    totalRow = totalRow,
                    data = html
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [BasicAuthorize(ModuleEnum.Order, PermissionEnum.Update)]
        [HttpPost]
        public JsonResult ChangeStatus(int orderId, int statusId)
        {
            using (var uow = new UnitOfWork(Shared.connString))
            {
                var res = uow.OrderRepository.ChangeStatus(orderId, statusId);
                if (res)
                {
                    if(statusId == 5)
                    {
                        var listItem = uow.OrderRepository.ViewListOrderItem(orderId);
                        foreach (var item in listItem)
                        {
                            uow.ProductRepository.UpdateQuantity(item.ProductId, item.Quantity);
                        }
                    }
                    uow.Commit();
                }
                return Json(new
                {
                    status = res
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [BasicAuthorize(ModuleEnum.Order, PermissionEnum.Read)]
        public PartialViewResult ViewOrder(SearchOrderViewModel obj)
        {
            using (var uow = new UnitOfWork(Shared.connString))
            {
                var res = uow.OrderRepository.CheckOrder(obj.OrderId, obj.Email);

                var model = new OrderViewModel();
                model.AddressDelivery = uow.OrderRepository.ViewAddressDelivery(obj.OrderId);
                model.Order = uow.OrderRepository.ViewOrder(obj.OrderId);
                model.ListOrderItem = uow.OrderRepository.ViewListOrderItem(obj.OrderId).ToList();

                return PartialView("_orderDetail", model);
            }
        }

        [BasicAuthorize(ModuleEnum.Order, PermissionEnum.Statistic)]
        public ActionResult Order_Statistics()
        {
            return View();
        }

        [BasicAuthorize(ModuleEnum.Order, PermissionEnum.Statistic)]
        [HttpPost]
        public JsonResult Order_Statistics_ByDay(string fromDate, string toDate)
        {
            using (var uow = new UnitOfWork(Shared.connString))
            {
                var fDate = fromDate.StringToDateTime();
                var tDate = toDate.StringToDateTime();
                var res = uow.OrderRepository.statisticsByDay(fDate, tDate);
                var allDate = new List<ChartsDataViewModel>();

                var totalDays = (tDate - fDate).TotalDays;
                for (DateTime i = fDate; i <= tDate; i = i.AddDays(1))
                {
                    var item = new ChartsDataViewModel();
                    item.value = "0";
                    foreach (var jtem in res)
                    {
                        if (jtem.date.Date == i.Date.Date)
                            item.value = jtem.value;
                    }
                    item.date = i;

                    allDate.Add(item);
                }

                return Json(new
                {
                    label = "Thống kê đơn hàng",
                    data = allDate
                },JsonRequestBehavior.AllowGet);

            }
        }
    }
}