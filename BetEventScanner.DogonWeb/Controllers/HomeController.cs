using BetEventScanner.DogonWeb.Models;
using BetEventScanner.DogonWeb.Services;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using BetEventScanner.DataAccess;

namespace BetEventScanner.DogonWeb.Controllers
{
    public class HomeController : Controller
    {
        private ITennisService _tennisService;
        private ICalculateService _calculateService;
        public HomeController()
        {
            _tennisService = new TennisBetService(new UnitOfWork(), new CalculateService(new UnitOfWork()));
            _calculateService = new CalculateService(new UnitOfWork());
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Tennis()
        {
            ViewBag.Message = "Tennis page.";

            return View();
        }

        [HttpPost]
        public JsonResult CreateBet(BetInfoDto betInfo)
        {

            var result = _tennisService.ProcessBetLine(betInfo);

            return Json(new
            {
                isSuccess = true,
                data = result
            });
        }

        [HttpPost]
        public JsonResult CreateCalculatedBet(BetInfoDto betInfo)
        {
            var result = _tennisService.CreateCalculatedBet(betInfo);

            return Json(new { isSuccess = result });
        }

        public ActionResult List()
        {

            return View();
        }

        public ActionResult ReadBetList([DataSourceRequest]DataSourceRequest request)
        {
            var betsList = _tennisService.GetBetsList();
            return Json(betsList.ToDataSourceResult(request));
        }

        public ActionResult UpdateBet([DataSourceRequest] DataSourceRequest request, BetInfoListDto betInfoListDto)
        {
            if (betInfoListDto != null && ModelState.IsValid)
            {
                _tennisService.UpdateBet(betInfoListDto);
            }

            return Json(new[] { betInfoListDto }.ToDataSourceResult(request, ModelState));
        }
    }
}