using aryamoneygrow.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Stockmantras.Models;
using Stockmantras.Repository;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Stockmantras.Controllers
{
    public class HomeController : Controller
    {
        readonly Uri baseAddress = new Uri("https://chauhanmukesh.pythonanywhere.com/");
        private readonly ILogger<HomeController> _logger;
        private readonly IAccountRepository _accRepo;
        private readonly IConfiguration _config;
        private readonly AppDBContext _dbcontext;
        private readonly UserManager<ApplicationUser> _usrManager;
        
        
        private readonly HttpClient _httpClient;
        public HomeController(ILogger<HomeController> logger, IAccountRepository accrepo, IConfiguration config, AppDBContext dbcontext, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _accRepo = accrepo;
            _dbcontext = dbcontext;
            _config = config;
            _usrManager = userManager;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAddress;

        }
        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //EquityLTP eqdata = new EquityLTP();

             HttpResponseMessage response = await _httpClient.GetAsync(_httpClient.BaseAddress + "get_live_sensex");
             if (response.IsSuccessStatusCode)
             {
                 string data = await response.Content.ReadAsStringAsync();
                 if (data.Length > 40)
                 {
                    var sensexData = JsonConvert.DeserializeObject<LiveSensex>(data);
                    ViewBag.LiveSensex = sensexData;

                }
                else
                 {
                    ViewBag.LiveSensex = false;
                 }

             }
            var stockDataList = await _dbcontext.tbl_Nifty_50.ToListAsync();
            ViewBag.Nifty50Stocks_list = stockDataList;

            string dbcon = _config.GetConnectionString("DefaultConnection");
            DateTime currentDate = DateTime.Now.Date;

            var up_events = from thing in _dbcontext.tblEvents
                            where thing.E_DATE >= currentDate
                            orderby thing.E_DATE
                            select new Events
                            {
                                E_DATE = thing.E_DATE,
                                E_COY = thing.E_COY,
                                E_BRIEF = thing.E_BRIEF
                            };
            ViewBag.allEvents = await up_events.ToListAsync();

            var newList = await _dbcontext.tblNews.OrderByDescending(m => m.N_ID).Take(2).ToListAsync();
            ViewBag.NewsListAll = newList;
            ViewBag.IsHomePage = true;
            return View();
        }
        [HttpGet]
        [Route("signup")]
        public IActionResult SignUpUser()
        {
            return View();
        }
        [HttpGet]
        public IActionResult PrivacyPolicy()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AboutUs()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ContactUs()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ContactUs(Contact contactdtl)
        {
            _dbcontext.tblContact.Add(contactdtl);
            _dbcontext.SaveChanges();
            TempData["QueryMessage"] = "Thankyou for showing your interest. We shall Contact your very soon";
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("signup")]
        public async Task<IActionResult> SignUpUser(SignUpUser usermodel)
        {
            if (ModelState.IsValid)
            {

                var result = await _accRepo.CreateUserAsync(usermodel, "User");
                if (!result.Succeeded)
                {
                    foreach (var errorMessage in result.Errors)
                    {
                        ModelState.AddModelError("", errorMessage.Description);
                    }
                    return View(usermodel);
                }
                else { TempData["SignupOk"] = "Thankyou for registering. You can now Login with your Email and Password."; }
                ModelState.Clear();
            }
            return View();
        }
        [HttpGet]
        [Route("signin")]
        public IActionResult LoginUser()
        {
            return View();
        }
        [HttpPost]
        [Route("signin")]
        public async Task<IActionResult> LoginUser(LoginModel lmodel)
        {
            if (ModelState.IsValid)
            {
                var result = await _accRepo.PasswordSignInAsync(lmodel);
                if (result.Succeeded)
                {
                    return RedirectToAction("index", "User");

                }
                else
                {
                    //here work on showing error on login screen is pending
                    ModelState.AddModelError(string.Empty, "Invalid User or Password.");
                    TempData["InvalidLogin"] = "Invalid User or Password.";
                    return RedirectToAction("index", "Home");
                }
            }
            return View(lmodel);
        }
        public async Task<IActionResult> LogOut()
        {
            await _accRepo.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult PositionSize()
        {
            return View();
        }



        [HttpPost]
        public IActionResult PositionSize(Portfolio PS)
        {
            /* Console.WriteLine("hello");
             int x = PS.PORT_SIZE * PS.RISK_PER/100;
             int y = PS.ENTRY_PRICE - PS.STOP_LOSS;
             int buysize = x / y;
             PS.tot_shares = buysize.ToString();
             int total_invest = buysize * PS.ENTRY_PRICE;
             PS.total_inv = total_invest.ToString();
             int total_allocation = (total_invest / PS.PORT_SIZE) * 100;
             float r_ratio = (PS.TARGET_PRICE - PS.ENTRY_PRICE) / y;
             PS.r_reward = r_ratio.ToString();
             Console.WriteLine(buysize);
             Console.WriteLine(total_invest);
             Console.WriteLine(total_allocation);
             Console.WriteLine(r_ratio);*/

            return View(PS);
        }
        // for demate account page
        [HttpPost]
        // [Route("CreateNewDemAccount")]
        public IActionResult DemateAccount(DemateAccount user)
        {
            if (ModelState.IsValid)
            {

                // var result = await _accRepo.CreateUserAsync(usermodel);
                _dbcontext.tbl_Demate_Request.Add(user);
                _dbcontext.SaveChanges();

            }
            ModelState.Clear();
            TempData["SuccessMessageDemate"] = "Thankyou for showing your interest. We shall Contact your very soon";

            return RedirectToAction("Index", "Home");
            //return View();
        }
        // to get a view of demate account page
        [HttpGet]
        public IActionResult DemateAccount()
        {

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Equities()
        {
            var stockDataList = await _dbcontext.tbl_Nifty_50.ToListAsync();
            ViewBag.Nifty50Stocks_list = stockDataList;
            /*HttpResponseMessage response = await _httpClient.GetAsync(_httpClient.BaseAddress + "get_top_gainers");
            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var rawdata = JsonConvert.DeserializeObject<dynamic>(data);
                //var stockDataList = JsonConvert.DeserializeObject<Dictionary<string, TopGainers>>(data);
                ViewBag.Top_Gainers = rawdata["Top Gainers"];
                ViewBag.Top_Losers = rawdata["Top Losers"];
            }*/
            // to get market outlook data
            var userInputtest = _dbcontext.tbl_Marketoutlook.ToList().Last();
            ViewBag.MarketOutlook = userInputtest;

            return View();
        }
        [HttpGet]
        public IActionResult Insurances()
        {
            var allfunds = _dbcontext.tbl_Insurance.ToList();
            if (allfunds != null)
            {
                ViewBag.AllInsurances = allfunds;
                return View();
            }
            else
            {
                return View();
            }

        }
        [HttpGet]
        public IActionResult Coins()
        {
            var allcoins = _dbcontext.tbl_Coins.ToList();
            if (allcoins != null)
            {
                ViewBag.AllCoins = allcoins;
                return View();
            }
            else
            {
                return View();
            }

        }
        [HttpGet]
        public IActionResult MutualFund()
        {
            var allfunds = _dbcontext.tbl_Mutual_Funds.ToList();
            if (allfunds != null)
            {
                ViewBag.MutualFunds = allfunds;
                return View();
            }
            else
            {
                return View();
            }

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            ViewBag.PasswordChanged = false;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _usrManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }

            var changePasswordResult = await _usrManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);

            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(model);
            }
            ViewBag.PasswordChanged = true;
            // Redirect to a page indicating successful password change
            TempData["PasswordSuccess"] = "Your Password has been changed successfully. Please Login with new Credentials";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult SensexCard()
        {

            return View();

        }
        [HttpGet]
        public IActionResult MarketToday()
        {
            // Retrieve the user input from TempData and pass it to the view.
            var userInput = _dbcontext.tbl_Marketoutlook.ToList().Last();
            ViewBag.MarketOutlook = userInput;
            return View();
        }

    }
}


/*string dbcon = _config.GetConnectionString("DefaultConnection");
            var temp_eq = from thing in _dbcontext.tbl_Equities.Where(m=>m.STATUS!=null).OrderBy(m=>m.S_NAME)
                          select new Equity
                          {
                              S_CODE = thing.S_CODE,
                              S_NAME = thing.S_NAME
                          };
            ViewBag.allequities = new SelectList(temp_eq, "S_CODE", "S_NAME");
            The above code i used for adding security in my website.
             */