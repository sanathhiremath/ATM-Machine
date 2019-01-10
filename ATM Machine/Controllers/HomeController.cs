using ATM_Machine.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ATM_Machine.DataLayer;
using System.Data;

namespace ATM_Machine.Controllers
{
    public class HomeController : Controller
    {
        public static long cardNo;
        public static string firstName;
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

        public ActionResult Login(UserDetails user)
        {
            DataSet ds = Home.IsUserExists(user);

            if (ds.Tables[0].Rows.Count > 0)
            {
                ViewBag.cardNo = Convert.ToInt64(ds.Tables[0].Rows[0]["CardNo"]);
                cardNo = Convert.ToInt64(ds.Tables[0].Rows[0]["CardNo"]);
                ViewBag.firstName = Convert.ToString(ds.Tables[0].Rows[0]["FirstName"]);
                firstName= Convert.ToString(ds.Tables[0].Rows[0]["FirstName"]);
                return View("MainPage");
            }
            return View("Login");
        }

        public ActionResult SignUp()
        {
            return View("Register");
        }

        public ActionResult Register(UserDetails user)
        {
            Home.AddNewUser(user);
            return View("Register");
        }

        public ActionResult Deposit(Transaction tr)
        {
            if (tr.DepositAmt != 0)
            {
                tr.CardNo = cardNo;
                Home.AddAmt(tr);
                ViewBag.status = "Success";
            }
            return View("Deposit");
        }

        public ActionResult Withdrawal(Transaction tr)
        {
            if (tr.WithdrawalAmt != 0)
            {
                tr.CardNo = cardNo;
                string status = Home.WithdrawAmt(tr);
                if (status == "false")
                {
                    ViewBag.status = "Insufficient Balance";
                }
                else
                {
                    ViewBag.status = "Success";
                }
            }
            return View("Withdrawal");
        }
        public ActionResult Balance(Transaction tr)
        {
            if (tr != null)
            {
                tr.CardNo = cardNo;
                DataTable dt = Home.Balance(tr);
                ViewBag.balance = Convert.ToInt32(dt.Rows[0]["Balance"]);
            }

            return View("Balance");
        }

        public ActionResult MainPage(Transaction tr)
        {
            ViewBag.firstName = firstName;
            return View("MainPage");
        }
    }
}