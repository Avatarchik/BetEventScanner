using BetEventScanner.DataAccess;
using BetEventScanner.DogonWeb.Models;
using BetEventScanner.DogonWeb.Services;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BetEventScanner.DogonWeb.Controllers
{
    public class BetController : Controller
    {
        private ITennisService _tennisService;
        private ICalculateService _calculateService;

        public BetController()
        {
            _tennisService = new TennisBetService(new UnitOfWork(), new CalculateService(new UnitOfWork()));
            _calculateService = new CalculateService(new UnitOfWork());
        }
        public ActionResult Index()
        {
            ViewBag.Message = "Tennis page";
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> CreateBet(BetInfoDto betInfo)
        {

            var result = await _tennisService.ProcessBetLineAsync(betInfo);

            return Json(new
            {
                isSuccess = true,
                data = result
            });
        }

        [HttpPost]
        public async Task<JsonResult> CreateCalculatedBet(BetInfoDto betInfo)
        {
            var result = await _tennisService.CreateCalculatedBetAsync(betInfo);

            return Json(new { isSuccess = result });
        }

        public ActionResult List()
        {

            return View();
        }

        public async Task<ActionResult> ReadBetList([DataSourceRequest]DataSourceRequest request)
        {
            var betsList = await _tennisService.GetBetsListAsync();
            return Json(betsList.ToDataSourceResult(request));
        }

        public async Task<ActionResult> UpdateBet([DataSourceRequest] DataSourceRequest request, BetInfoListDto betInfoListDto)
        {
            if (betInfoListDto != null && ModelState.IsValid)
            {
                await _tennisService.UpdateBetAsync(betInfoListDto);
            }

            return Json(new[] { betInfoListDto }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public async Task<ActionResult> RemoveBet([DataSourceRequest] DataSourceRequest request, BetInfoListDto betInfoListDto)
        {
            if (betInfoListDto != null && ModelState.IsValid)
            {
                await _tennisService.RemoveBetAsync(betInfoListDto.Id);
            }

            return Json(new[] { betInfoListDto }.ToDataSourceResult(request, ModelState));
        }
    }
}