using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Business_Logic_Layer.Models;
using WEB_Auction.Models;
using Business_Logic_Layer.Abstract;

namespace WEB_Auction.Controllers
{
    public class LotController : Controller
    {
        private readonly ILotService _lotService;
        private readonly IAuctionService _auctionService;
        private readonly IUserService _userService;
        public LotController(ILotService lotService, IAuctionService auctionService, IUserService userService)
        {
            _lotService = lotService;
            _auctionService = auctionService;
            _userService = userService;
        }
        // public  Task<ActionResult<IEnumerable<LotDto>>> GetAll()
        //    => Ok( _lotService.GetAllAsync());
        [HttpGet("{id}")]
        public async Task<ActionResult<LotDto>> GetById(int id)
            => Ok(await _lotService.GetByIdAsync(id));
        [HttpGet]
        public ActionResult Index(int id) 
        {
            var fullList = _lotService.GetByAuctionId(id);
            var actualList = new List<LotDto>();
            foreach (var l in fullList) 
            {
                if (l.Expiring > DateTime.Now) 
                {
                    actualList.Add(l);
                }
            }
            LotModel model = new LotModel(actualList);
            return View(model);
        }
        [HttpGet]
        public ActionResult Add(int id)
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(LotModel model)
        {
            var auctionId = Convert.ToInt32(RouteData.Values["id"]);
            var currentUser =  _userService.GetByEmail(User.Identity.Name);
            var currentUserId = currentUser.Result.Id;
            var auctionUser = _auctionService.GetByIdAsync(auctionId);
            var auctionUserId = auctionUser.Result.UserId;
            if (currentUserId == auctionUserId) 
            {
                _lotService.AddAsync(new LotDto { Name = model.Name, Price = model.Bid, Description = model.Description
                    , BidUserId=currentUserId, Expiring = model.Expiring, AuctionId=auctionId });
            }
            return View(model);
        }
        [HttpGet]
        public ActionResult MakeBid(int id) 
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> MakeBid(BidModel model)
        {
            if (ModelState.IsValid)
            {
                var lotId = Convert.ToInt32(RouteData.Values["id"]);
                var auctionId = _lotService.GetByIdAsync(lotId).Result.AuctionId;
                var auctionCreator = _auctionService.GetByIdAsync(auctionId).Result.UserId;
                var userName = _userService.GetByIdAsync(auctionCreator).Result.Email;
                if (userName != User.Identity.Name)
                {
                    var user = await _userService.GetByEmail(User.Identity.Name);
                    var id = user.Id;
                    _lotService.UpdateBidAsync(lotId, id, model.BidValue);
                    return RedirectToAction("Index", "Auction");
                }
            }
            ViewBag.Error = "Организатор аукциона не может делать ставку";
            return View(model);
        }
        [HttpGet]
        public ActionResult ShowFinishedLots()
        {
            var user =  _userService.GetByEmail(User.Identity.Name).Result.Id;
            var auctions = _auctionService.GetAllByCreator(user);
            List<LotDto> lots = new List<LotDto>();
            foreach (var a in auctions)
            {
                var list = _lotService.GetByAuctionId(a.Id);
                foreach (var l in list) 
                {
                    if (l.Expiring < DateTime.Now)
                    {
                        string email = _userService.GetByIdAsync(l.BidUserId).Result.Email;
                        l.BidUserEmail = email;
                        lots.Add(l);
                    }
                }
            }
            LotModel model = new LotModel(lots);
            return View(model);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id) 
        {
            await _lotService.DeleteAsync(id);
            return Ok();
        }
        //[HttpGet]
        //public IActionResult Index()
        //{
        //    var list = _lotService.GetAllAsync().ToList();
        //    LotModel model = new LotModel(list);
        //    return View(model);
        //}
    }
}
