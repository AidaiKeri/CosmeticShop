using E_Shop_Cosmetic.Data.Interfaces;
using E_Shop_Cosmetic.Data.Models;
using E_Shop_Cosmetic.Data.Other;
using E_Shop_Cosmetic.Data.Specifications;
using E_Shop_Cosmetic.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shop_Cosmetic.Controllers
{
    public class OrdersController : Controller
    {
        private readonly ICookieService _cartService;
        private readonly IOrderRepository _orderRepository;

        public OrdersController(ICookieService cartService, IOrderRepository orderRepository)
        {
            _cartService = cartService;
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Search(SearchOrderParams searchParams)
        {
            var searchSpecification = new OrderSpecification();
            var isPrimaryKeyUsed = false;
            if (searchParams.OrderId is not null)
            {
                searchSpecification.WhereId(searchParams.OrderId.Value);
                isPrimaryKeyUsed = true;
            }

            if (!isPrimaryKeyUsed)
            {
                if (searchParams.Name is not null)
                {
                    searchSpecification.WhereName(searchParams.Name);
                }
                if (searchParams.LastName is not null)
                {
                    searchSpecification.WhereLastName(searchParams.LastName);
                }
                if (searchParams.Email is not null)
                {
                    searchSpecification.WhereEmail(searchParams.Email);
                }

                if (searchParams.PhoneNumber is not null)
                {
                    searchSpecification.WherePhone(searchParams.PhoneNumber);
                }
            }

            if (searchParams.IsSortByDateRequired)
            {
                searchSpecification.SortByDate();
            }

            searchSpecification.WhereActive(searchParams.IsOrderActive).WithoutTracking();
            var viewModel = new SearchOrderViewModel()
            {
                Orders = await _orderRepository.GetAll(searchSpecification),
                SearchParams = searchParams

            };

            ViewBag.Title = "Поиск по товарам";

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> PlaceOrder()
        {
            ViewBag.Title = "Оформление заказа";

            if (await _cartService.IsAnyProductInCartAsync())
            {
                return View();
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder(OrderViewModel orderViewModel)
        {
            var ordersDetails = await _cartService.GetOrderDetailsAsync();

            if (ordersDetails.Any())
            {
                var totalPrice = Math.Round(ordersDetails.Sum(detail => detail.TotalPrice), 2);

                var newOrder = new Order()
                {
                    Address = orderViewModel.Address,
                    IsOrderActive = true,
                    Information = orderViewModel.Information ?? "None",
                    Name = orderViewModel.Name,
                    LastName = orderViewModel.LastName,
                    PhoneNumber = orderViewModel.PhoneNumber,
                    TotalPrice = totalPrice,
                    OrderDetails = ordersDetails,
                    OrderDate = DateTime.Now,
                    Email = orderViewModel.Email

                };

                await _orderRepository.Add(newOrder);
                await _cartService.ClearCartAsync();

                return RedirectToAction("OrderSuccessful", newOrder);
            }

            else
            {
                return NoContent();
            }
        }


        public IActionResult OrderSuccessful(Order order)
        {
            return View(order);
        }

        [Authorize(Roles = IdentityRoleConstants.Admin)]
        [HttpGet]
        public async Task<IActionResult> UpdateOrder(int id)
        {
            ViewBag.Title = "Изменение заказа";
            var order = await _orderRepository.GetById(id);
            if (order is not null)
            {
                return View(order);
            }
            return NoContent();
        }

        [Authorize(Roles = IdentityRoleConstants.Admin)]
        [HttpPost]
        public async Task<IActionResult> UpdateOrder(int id, Order order)
        {
            if (id != order.Id)
            {
                return BadRequest();
            }

            await _orderRepository.Update(order);

            return RedirectToAction("ViewOrders", "Orders");
        }

        [Authorize(Roles = IdentityRoleConstants.Admin)]
        [HttpGet]
        public async Task<IActionResult> Order(int id)
        {
            ViewBag.Title = "Заказ";
            var order = await _orderRepository.GetOrderByIdWithDetailsOrDefault(id);

            if (order is not null)
            {
                return View(new ViewOrderViewModel { Order = order });
            }

            return NoContent();
        }

        [Authorize(Roles = IdentityRoleConstants.Admin)]
        [HttpGet]
        public async Task<IActionResult> ViewOrders()
        {
            ViewBag.Title = "Вывод заказов";

            var orders = await _orderRepository.GetAll(
                new OrderSpecification()
                    .IncludeDetails()
                    .SortByTotalPrice()
                    .WithoutTracking()
                );

            var viewModel = new SearchOrderViewModel()
            {
                Orders = orders,
                SearchParams = new SearchOrderParams(),
            };

            return View(viewModel);
        }
    }
}
