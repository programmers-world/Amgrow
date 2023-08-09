using aryamoneygrow.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Stockmantras.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Stockmantras.Controllers
{
    public class UserController : Controller
    {
        private readonly IConfiguration _config;
        private readonly AppDBContext _dbcontext;
        public UserController(IConfiguration config, AppDBContext dbcontext)
        {
            _config = config;
            _dbcontext = dbcontext;
        }
        [Authorize(Roles ="Admin, User, Coin")]
        [HttpGet]
        public IActionResult Index()
        {
           
             var myportfolio = _dbcontext.tbl_Portfolio
            .Where(portfolio => portfolio.USER_NAME == User.Identity.Name)
            .Join(
            _dbcontext.tbl_Equities,
            portfolio => portfolio.SCRIPT_NAME,
            equities => equities.S_NAME,
            (portfolio, equities) => new MyPortfolio
                {
                Portfolio = portfolio,
                EquitiesLTP = equities.LTP
                }
            )
            .AsEnumerable() // Switch to client-side processing
            .GroupBy(item => item.Portfolio.SCRIPT_NAME)
            .Select(group => group.First())
            .ToList();
            if (myportfolio.Count == 0)
            {
                ViewBag.NoStocks = true;
            }
            return View(myportfolio);

        }
        [HttpGet]
        public async Task<JsonResult> GetSuggestionStocks(string query)
        {
            List<string> suggestions = await _dbcontext.tbl_Equities
                .Where(e => e.S_NAME.StartsWith(query))
                .Select(e => e.S_NAME)
                .ToListAsync();

            return new JsonResult(suggestions);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult ContactedBy()
        {
            var contacts = _dbcontext.tblContact.ToList();
            return View(contacts);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult DeleteQuery(int id)
        {
            var contact = _dbcontext.tblContact.Find(id);
            if (contact != null)
            {
                _dbcontext.tblContact.Remove(contact);
                _dbcontext.SaveChanges();
                TempData["QueryDeleted"] = "Record No. " + id + " deleted Successfully !";
            }
            return RedirectToAction("ContactedBy");
        }
        [Authorize(Roles = "Coin")]
        [HttpPost]
        public IActionResult DeleteCoin(int id)
        {
            var contact = _dbcontext.tbl_Coins.Find(id);
            if (contact != null)
            {
                _dbcontext.tbl_Coins.Remove(contact);
                _dbcontext.SaveChanges();
                TempData["CoinDeleted"] = "Coin No. " + id + " deleted Successfully !";
            }
            return RedirectToAction("ViewCoins");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult DeleteMF(int id)
        {
            var contact = _dbcontext.tbl_Mutual_Funds.Find(id);
            if (contact != null)
            {
                _dbcontext.tbl_Mutual_Funds.Remove(contact);
                _dbcontext.SaveChanges();
                TempData["MFDeleted"] = "Mutual Fund No. " + id + " deleted Successfully !";
            }
            return RedirectToAction("Viewfund");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult DeleteInsu(int id)
        {
            var contact = _dbcontext.tbl_Insurance.Find(id);
            if (contact != null)
            {
                _dbcontext.tbl_Insurance.Remove(contact);
                _dbcontext.SaveChanges();
                TempData["InsuDeleted"] = "Insurance ID No. " + id + " deleted Successfully !";
            }
            return RedirectToAction("ViewInsurance");
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult CreateEvent(string title, string content, DateTime imgurl)
        {
            var mymodel = new Events();
            mymodel.E_COY = title;
            mymodel.E_BRIEF = content;
            mymodel.E_DATE = imgurl;
            mymodel.E_STATUS = "D";
            try
            {
                _dbcontext.tblEvents.Add(mymodel);
                _dbcontext.SaveChanges();
                return RedirectToAction("Index", "User");
            }
            catch
            {
                return View(mymodel);
            }
        }

        [HttpGet]
        public IActionResult CreateEvent()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult CreateNews(string title, string content, string imgurl)
        {
            var mymodel = new News();
            mymodel.N_HEADING = title;
            mymodel.N_BRIEF = content;
            mymodel.N_IMG_URL = imgurl;
            mymodel.status = "D";
            try
            {
                _dbcontext.tblNews.Add(mymodel);
                _dbcontext.SaveChanges();
                return RedirectToAction("Index", "User");
            }
            catch
            {
                return View(mymodel);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult CreateNews()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult AddFund()
        {

            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult ViewFund()
        {
            var allfunds = _dbcontext.tbl_Mutual_Funds.ToList();
            return View(allfunds);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AddFund(MutualFund myfund)
        {
            try
            {
                _dbcontext.tbl_Mutual_Funds.Add(myfund);
                _dbcontext.SaveChanges();
                TempData["FundSaved"] = "Mutual Fund Saved Successfully";
                return RedirectToAction("AddFund", "User");

            }
            catch
            {
                return View(myfund);
            }

        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AddInsurance(Insurance myIns)
        {
            try
            {
                _dbcontext.tbl_Insurance.Add(myIns);
                _dbcontext.SaveChanges();
                TempData["InsuSaved"] = "Insurance Plan Saved Successfully";
                return RedirectToAction("AddInsurance", "User");

            }
            catch
            {
                return View(myIns);
            }

        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult AddInsurance()
        {

            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult ViewInsurance()
        {
            var allinsurances = _dbcontext.tbl_Insurance.ToList();
            return View(allinsurances);
        }




        [HttpGet]
        public IActionResult addStock()
        {
            /* string dbcon = _config.GetConnectionString("DefaultConnection");
             var temp_eq = from thing in _dbcontext.tbl_Equities.Where(m => m.STATUS != null).OrderBy(m => m.S_NAME)
                           select new EquityPage
                           {
                               S_CODE = thing.S_CODE,
                               S_NAME = thing.S_NAME
                           };
             ViewBag.allequities = new SelectList(temp_eq, "S_CODE", "S_NAME");*/
            return View();
        }
        [HttpPost]
        public IActionResult addStock(Transaction tr)
        {
            /* string dbcon = _config.GetConnectionString("DefaultConnection");
             var temp_eq = from thing in _dbcontext.tbl_Equities.Where(m => m.STATUS != null).OrderBy(m => m.S_NAME)
                           select new EquityPage
                           {
                               S_CODE = thing.S_CODE,
                               S_NAME = thing.S_NAME
                           };
             ViewBag.allequities = new SelectList(temp_eq, "S_CODE", "S_NAME");*/
            return View();
        }


        [HttpGet]
        public IActionResult AddCoin()
        {
            return View();
        }

        //[with multipart image uploading]
        [Authorize(Roles = "Coin")]
        [HttpPost]
        public IActionResult AddCoin(Coin myCoin)
        {
            if (ModelState.IsValid)
            {
                // Save the picture to /Coin folder
                string uniqueFileName = null;
                if (myCoin.COIN_PIC_URL != null && myCoin.COIN_PIC_URL.Length > 0) // Check if a file was uploaded
                {
                    string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Coins");
                    uniqueFileName = Path.GetRandomFileName() + "_" + myCoin.COIN_IMG_URL.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        myCoin.COIN_IMG_URL.CopyTo(fileStream);
                    }

                    // Store the image URL in the ViewModel
                    myCoin.COIN_PIC_URL = "/Coins/" + uniqueFileName;
                }

                // Save the coin details to the database
                var newCoin = new Coin
                {
                    COIN_NAME = myCoin.COIN_NAME,
                    COIN_DESC = myCoin.COIN_DESC,
                    COIN_MINT = myCoin.COIN_MINT,
                    COIN_PRICE = myCoin.COIN_PRICE,
                    COIN_IMG_URL = myCoin.COIN_IMG_URL, // Use the stored image URL from the ViewModel
                    COIN_BRIEF = myCoin.COIN_BRIEF
                };    // Save the coin details to the database
                // Assuming you have a DbContext named "YourDbContext" to work with the database
                _dbcontext.tbl_Coins.Add(myCoin);
                _dbcontext.SaveChanges();
                TempData["CoinSaved"] = "Details of Coin saved successfully.";

                return RedirectToAction("AddCoin", "User"); // Redirect to Home page or any other page
            }

            return View(myCoin); // Show the form again with validation errors
        }
        //authorised on id mukeshggm@gmail.com pwd 
        [Authorize(Roles = "Coin")]
        [HttpGet]
        public IActionResult ViewCoins()
        {
            var allCoins = _dbcontext.tbl_Coins.ToList();
            return View(allCoins);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult SubmitMarketOutlook()
        {


            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult SubmitMarketOutlook(MarketOutlook userInput)
        {
            _dbcontext.tbl_Marketoutlook.Add(userInput);
            _dbcontext.SaveChanges();

            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult ImportStockPrice()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult ImportAllStockPrice()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> ImportStockPrice(IFormFile csvFile)
        {
            if (csvFile == null || csvFile.Length <= 0)
            {
                // Handle file not selected error
                return RedirectToAction("Index");
            }

            using (var reader = new StreamReader(csvFile.OpenReadStream()))
            {
                while (!reader.EndOfStream)
                {
                    var line = await reader.ReadLineAsync();
                    var values = line.Split(',');

                    var symbol = values[0];

                    var existingEntity = await _dbcontext.tbl_Nifty_50.SingleOrDefaultAsync(e => e.SYMBOL == symbol);

                    if (existingEntity != null)
                    {
                        existingEntity.PREV_CLOSE = double.Parse(values[1]);
                        existingEntity.LTP = double.Parse(values[2]);
                        // Only update PREV_CLOSE and LTP columns
                    }
                }
            }
            await _dbcontext.SaveChangesAsync();

            return Redirect("Index");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> ImportAllStockPrice(IFormFile csvFile)
        {
            if (csvFile == null || csvFile.Length <= 0)
            {
                // Handle file not selected error
                return RedirectToAction("Index");
            }

            using (var reader = new StreamReader(csvFile.OpenReadStream()))
            {
                while (!reader.EndOfStream)
                {
                    var line = await reader.ReadLineAsync();
                    var values = line.Split(',');

                    var symbol = values[0];

                    var existingEntity = await _dbcontext.tbl_Equities.SingleOrDefaultAsync(e => e.SYMBOL == symbol);

                    if (existingEntity != null)
                    {
                        existingEntity.PREV_CLOSE = double.Parse(values[1]);
                        existingEntity.LTP = double.Parse(values[2]);
                        // Only update PREV_CLOSE and LTP columns
                    }
                }
            }
            await _dbcontext.SaveChangesAsync();

            return Redirect("Index");
        }

        //for auto complete
        public IActionResult GetEquitySuggestions(string term)
        {
            var suggestions = _dbcontext.tbl_Equities.ToList();

            return Json(suggestions);
        }
        [Authorize (Roles = "User,Admin,Coin")]
        [HttpGet]
        public IActionResult BuyStocks()
        {
            return View();
        }
        [Authorize(Roles = "User,Admin,Coin")]
        [HttpPost]
        public IActionResult BuyStocks(Portfolio pData)
        {
            pData.USER_NAME = User.Identity.Name;
            pData.INVESTED_AMOUNT = Math.Round(pData.PRICE * pData.QUANTITY,2);
             _dbcontext.tbl_Portfolio.Add(pData);
            var result = _dbcontext.SaveChanges();
            TempData["StockBought"] = "Stock " + result + " Bought Successfully !";
            return RedirectToAction("Index");
            
            
        }

    }

}
