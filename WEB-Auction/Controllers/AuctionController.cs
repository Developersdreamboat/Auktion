using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Business_Logic_Layer.Abstract;
using Business_Logic_Layer.Models;
using WEB_Auction.Models;
using System.Threading.Tasks;

namespace WEB_Auction.Controllers
{
    public class AuctionController : Controller
    {
        private readonly IAuctionService _auctionService;
        private readonly IUserService _userService;
        private string userName;
        public AuctionController(IAuctionService auctionService, IUserService userService)
        {
            _auctionService = auctionService;
            _userService = userService;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(AuctionCreateModel model)
        {
            userName = User.Identity.Name;
            var user = _userService.GetByEmail(userName);
            var id = user.Result.Id;
            _auctionService.AddAsync(new AuctionDto { Name=model.Name, Description=model.Description, CreatedAt=DateTime.Now, UserId = id});
            return View(model);
        }
        public IActionResult Index() 
        {
            var list = _auctionService.GetAllAsync();
            AuctionModel model = new AuctionModel(list.Result);
            return View(model);
        }
    }
}
