using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BtcProApp.Models;
using System.Collections;
using System.Threading.Tasks;
using System.Threading;

namespace BtcProApp.Controllers
{
    //[Authorize]
    public class HomeController : Controller
    {
        private BtcProDB db = new BtcProDB();

        [AllowAnonymous]
        public ActionResult IndexMain()
        {
            return View();
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
        public ActionResult TeamMembers()
        {
            ViewBag.Name = User.Identity.Name;
            return View();
        }
        public ActionResult Genealogy()
        {
            ViewBag.Name = User.Identity.Name;
            return View();
        }
        public ActionResult TeamStructure()
        {
            return View();
        }
        public ActionResult Registration()
        {
            return View();
        }
        public ActionResult TeamStructureNew()
        {
            return View();
        }
        public ActionResult GenerationStructure()
        {
            return View();
        }
        public ActionResult Network()
        {
            return View();
        }
        public ActionResult MyPurchase()
        {
            return View();
        }
        public ActionResult MyRePurchase()
        {
            return View();
        }
        public ActionResult PackagesShop()
        {
            return View();
        }
        public ActionResult OrderHistory()
        {
            return View();
        }
        public ActionResult Invoices()
        {
            return View();
        }
        public ActionResult SalesHistory()
        {
            return View();
        }
        public ActionResult PayoutByAdmin()
        {
            return View();
        }
        public ActionResult Illustration()
        {
            return View();
        }
        public ActionResult CashWallet()
        {
            return View();
        }
        public ActionResult ReturnWallet()
        {
            return View();
        }
        public ActionResult JpnWallet()
        {
            return View();
        }

        public ActionResult JpnDevWallet()
        {
            return View();
        }
        public ActionResult JpnRefWallet()
        {
            return View();
        }
        public ActionResult JpnRoyaltyWallet()
        {
            return View();
        }
        public ActionResult JpnDiamondWallet()
        {
            return View();
        }
        public ActionResult JoinWallet()
        {
            return View();
        }
        public ActionResult JpnWithdrawalWallet()
        {
            return View();
        }
        public ActionResult JoiningWallet()
        {
            return View();
        }
        public ActionResult ReserveWallet()
        {
            return View();
        }
        public ActionResult FrozenWallet()
        {
            return View();
        }
        public ActionResult Earnings()
        {
            return View();
        }
        public ActionResult Withdrawals()
        {
            return View();
        }
        public ActionResult Transfers()
        {
            return View();
        }
        public ActionResult AdminTransfers()
        {
            return View();
        }
        public ActionResult UploadBalance()
        {
            return View();
        }
        public ActionResult WithdrawalRequests()
        {
            return View();
        }

        public ActionResult TransfersWithdrawal()
        {
            return View();
        }
        public ActionResult TransferRequests()
        {
            return View();
        }
        public ActionResult EditProfile()
        {
            return View();
        }
        public ActionResult UploadKYC()
        {
            return View();
        }
        public ActionResult AccountStatus()
        {
            return View();
        }
        public ActionResult ChangePassword()
        {
            return View();
        }
        public ActionResult ChangeTrnPassword()
        {
            return View();
        }

        public ActionResult ApproveBitcoinTransfers()
        {
            return View();
        }

        public ActionResult PaymentAccount()
        {
            return View();
        }

        public ActionResult Tickets()
        {
            return View();
        }
        public ActionResult Notifications()
        {
            return View();
        }
        public ActionResult ProcessPayout()
        {
            return View();
        }
        public ActionResult Logout()
        {
            return View();
        }

        #region Common Code

        [HttpGet]
        public JsonResult CurrentUserName()
        {
            string user = User.Identity.Name;
            var reg = db.Registrations.SingleOrDefault(r => r.UserName.ToUpper() == user.ToUpper());
            return Json(new { CurrentUser = user, UserId = reg.Id }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsMemberACNOpresent()
        {
            bool isExist1 = true;
            bool isExist2 = true;
            string bikasAcno = "";
            string banknAcno = "";
            string UserName = User.Identity.Name;
            Register mem = db.Registrations.SingleOrDefault(m => m.UserName.ToUpper().Trim() == UserName.ToUpper().Trim());
            if (mem.MyWalletAccount == "" || mem.MyWalletAccount == null)
            {
                isExist1 = false;
                bikasAcno = "";
            }
            else
            {
                isExist1 = true;
                bikasAcno = "Bikas Ac/No. " + mem.MyWalletAccount;
            }
            if (mem.MyBankName == "" || mem.MyBankName == null || mem.MyBankName.ToUpper() == "NULL" || mem.MyBankAccountNum=="" || mem.MyBankAccountNum==null || mem.MyBankAccountNum.ToUpper() == "NULL")
            {
                isExist2 = false;
                banknAcno = "";
            }
            else
            {
                isExist2 = true;
                banknAcno = mem.MyBankName + " A/c No. " + mem.MyBankAccountNum;
            }

            return Json(new { Found1 = isExist1, Found2 = isExist2, BikasAcNo=bikasAcno, BankAcNo=banknAcno }, JsonRequestBehavior.AllowGet);
        }

        //called when a member puts request for withdrawal
        public JsonResult WithdrawPostingMember(int WalletType, double Amount, string PayAccount)
        {
            Guid g = Guid.NewGuid();
            string GuidString = Convert.ToBase64String(g.ToByteArray());
            GuidString = GuidString.Replace("=", "");
            GuidString = GuidString.Replace("+", "");

            var currentusr = User.Identity.Name;
            if (currentusr != null || currentusr != "")
            {
                try
                {
                    var rec0 = db.Registrations.SingleOrDefault(r => r.UserName.ToUpper().Trim() == currentusr.ToUpper().Trim());
                    WithdrawalRequest req = new WithdrawalRequest();
                    req.RegistrationId = rec0.Id;
                    req.Date = DateTime.Now;
                    req.WalletId = WalletType;
                    req.Amount = Amount;
                    req.ServiceCharge = (Amount * 7) / 100;
                    req.PaidOutAmount = Amount - req.ServiceCharge;
                    req.Status = "Under Process";
                    req.BatchNo = GuidString;
                    req.PaidBitCoinAccount = PayAccount;
                    db.WithdrawalRequests.Add(req);
                    db.SaveChanges();

                    Ledger ldgr = new Ledger();
                    ldgr.RegistrationId = rec0.Id;
                    ldgr.WalletId = WalletType;
                    ldgr.Date = DateTime.Now;
                    ldgr.Withdraw = Amount;
                    ldgr.Deposit = 0;
                    ldgr.TransactionTypeId = 5;
                    ldgr.TransactionId = req.Id;
                    ldgr.SubLedgerId = 5;
                    ldgr.ToFromUser = 0;
                    ldgr.BatchNo = GuidString;
                    ldgr.Comment = "Request sent to Admin";

                    db.Ledgers.Add(ldgr);
                    db.SaveChanges();
                }
                catch (Exception e)
                {

                }

            }

            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CancelOrder(int Id, string remarks, string comment)
        {
            Boolean success = false;
            try
            {
                var withreqRec = db.WithdrawalRequests.SingleOrDefault(w => w.Id == Id);
                withreqRec.Status = remarks;
                withreqRec.Comment = comment;
                string btchno = withreqRec.BatchNo;

                var rec0 = db.Ledgers.SingleOrDefault(l => l.BatchNo == btchno);
                Ledger ldgr = new Ledger();
                ldgr.RegistrationId = rec0.RegistrationId;
                ldgr.WalletId = rec0.WalletId;
                ldgr.Date = DateTime.Now;
                ldgr.Withdraw = 0;
                ldgr.Deposit = rec0.Withdraw;
                ldgr.TransactionTypeId = rec0.TransactionTypeId;
                ldgr.TransactionId = rec0.TransactionId;
                ldgr.SubLedgerId = 5;
                ldgr.ToFromUser = 0;
                ldgr.BatchNo = rec0.BatchNo;
                if (remarks=="" && comment == "")
                {
                    //i.e. case of cancellation by member
                    ldgr.Comment = "Request cancelled by Member";
                }
                else
                {
                    ldgr.Comment = remarks + ". " + comment;
                }
                

                db.Ledgers.Add(ldgr);
                db.SaveChanges();

                success = true;
            }
            catch (Exception e)
            {

            }

            return Json(new { Success = success }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> SendMyWithdrawalRequestemail(string Username, double amount, string status)
        {
            if (Username == "")
            {
                Username = User.Identity.Name;
            }
            var reg = db.Registrations.SingleOrDefault(r => r.UserName == Username);
            bool result = false;
            ArrayList array = new ArrayList();

            string line0 = "<table style='width:100%'>";
            string line1 = "<tr><td colspan='2'>Dear <b>" + Username + "</b></td></tr>";
            string line2 = "<tr style='line-height:28px'><td colspan='2'>We have received your withdrawal request.</td></tr>";
            string line3 = "<tr style='line-height:40px'><td colspan='2'>The details are as follows:</td></tr>";
            string line4 = "<tr><td style='width:50px'>Amount:</td><td><b>BDT " + amount.ToString() + "</b></td></tr>";
            string line6 = "<tr><td style='width:50px'>Status:</td><td><b>Under process</b></td></tr>";
            string line7 = "<tr style='line-height:48px'><td colspan='2'></td><tr>";
            string line8 = "<tr><td colspan='2' style='line-height:48px'>If you have any queries, please do not hesitate to contact us.</td><tr>";
            string line9 = "<tr style='line-height:48px'><td colspan='2'></td><tr>";
            string line10 = "<tr><td colspan='2'>Best regards,</td><tr>";
            string line11 = "<tr><td colspan='2'>JPN Management.</td><tr>";
            string line12 = "<tr><td colspan='2'><a>info@jpnpl.com</a></td><tr>";
            string line13 = "<tr><td colspan='2'><a>www.jpnpl.com</a></td><tr>";
            string line14 = "</table>";
            string body = line0 + line1 + line2 + line3 + line4 + line6 + line7 + line8 + line9 + line10 + line11 + line12 + line13 + line14;

            result = await SendEMail(reg.EmailId, "", "Withdrawal Request mail", body, array);
            return Json(new { status = "Success" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult MemberWithdrawalHistory()
        {
            string username = User.Identity.Name;
            var rec = db.Registrations.SingleOrDefault(r => r.UserName.ToUpper().Trim() == username.ToUpper().Trim());
            try
            {
                var transfers = (from t in db.WithdrawalRequests
                                 from w in db.Wallets
                                 from r in db.Registrations
                                 where t.RegistrationId == rec.Id && t.WalletId == w.Id && t.RegistrationId == r.Id
                                 select new MemberWithdrawVM
                                 {
                                     Id = t.Id,
                                     Date = t.Date,
                                     WalletName = w.WalletName,
                                     Amount = t.Amount,
                                     Payable = t.PaidOutAmount,
                                     BitCoinAccount=t.PaidBitCoinAccount,
                                     AdministrativeChg = t.ServiceCharge,
                                     Approved_Date = t.Approved_Date,
                                     Status = t.Status,
                                     Comment = t.Comment
                                 }).ToList();

                for (int i = 0; i < transfers.Count(); i++)
                {
                    transfers[i].sDate = transfers[i].Date.ToLongDateString();
                    if (transfers[i].Approved_Date == null || transfers[i].Approved_Date == DateTime.Parse("01-01-0001"))
                    {
                        transfers[i].sApproved_Date = "";
                    }
                    else
                    {
                        transfers[i].sApproved_Date = ((DateTime)transfers[i].Approved_Date).ToLongDateString();
                    }
                }

                return Json(new { Transfers = transfers }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { Transfers = "" }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult LedgerPosting(string Username, string DrCr, int WalletType, double Amount)
        {
            var currentusr = User.Identity.Name;
            var rec0 = db.Registrations.SingleOrDefault(r => r.UserName.ToUpper().Trim() == currentusr.ToUpper().Trim());

            var rec = db.Registrations.SingleOrDefault(r => r.UserName.ToUpper().Trim() == Username.ToUpper().Trim());
            Ledger ldgr = new Ledger();
            ldgr.RegistrationId = rec.Id;
            ldgr.WalletId = WalletType;
            ldgr.Date = DateTime.Now;
            if (DrCr == "D") { ldgr.Deposit = Amount; }
            if (DrCr == "W") { ldgr.Withdraw = Amount; }
            ldgr.TransactionTypeId = 1;
            ldgr.TransactionId = 0;
            ldgr.SubLedgerId = 3;
            ldgr.ToFromUser = rec0.Id;

            db.Ledgers.Add(ldgr);
            db.SaveChanges();

            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult LedgerPostingMember(string Username, int WalletType, double Amount)
        {
            var currentusr = User.Identity.Name;
            if (currentusr != null || currentusr != "")
            {
                var rec0 = db.Registrations.SingleOrDefault(r => r.UserName.ToUpper().Trim() == currentusr.ToUpper().Trim());
                var rec = db.Registrations.SingleOrDefault(r => r.UserName.ToUpper().Trim() == Username.ToUpper().Trim());

                if (rec.UserName != rec0.UserName)
                {
                    Ledger ldgr = new Ledger();
                    ldgr.RegistrationId = rec0.Id;
                    ldgr.WalletId = WalletType;
                    ldgr.Date = DateTime.Now;
                    ldgr.Withdraw = Amount;
                    ldgr.Deposit = 0;
                    ldgr.TransactionTypeId = 6;
                    ldgr.TransactionId = 0;
                    ldgr.SubLedgerId = 1;
                    ldgr.ToFromUser = rec.Id;

                    db.Ledgers.Add(ldgr);
                    db.SaveChanges();

                    Ledger ldgrT = new Ledger();
                    ldgrT.RegistrationId = rec.Id;
                    ldgrT.WalletId = WalletType;
                    ldgrT.Date = DateTime.Now;
                    ldgrT.Withdraw = 0;
                    ldgrT.Deposit = Amount;
                    ldgrT.TransactionTypeId = 7;
                    ldgrT.TransactionId = 0;
                    ldgrT.SubLedgerId = 1;
                    ldgrT.ToFromUser = rec0.Id;

                    db.Ledgers.Add(ldgrT);
                    db.SaveChanges();
                }
            }
            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult TransferLedgerPosting(int ToWalletType, int WalletType, double Amount)
        {
            var Username = User.Identity.Name;
            if (Username != null || Username != "")
            {
                var rec0 = db.Registrations.SingleOrDefault(r => r.UserName.ToUpper().Trim() == Username.ToUpper().Trim());

                if (rec0 !=null)
                {
                    Ledger ldgr = new Ledger();
                    ldgr.RegistrationId = rec0.Id;
                    ldgr.WalletId = WalletType;
                    ldgr.Date = DateTime.Now;
                    ldgr.Withdraw = Amount;
                    ldgr.Deposit = 0;
                    ldgr.TransactionTypeId = 5;
                    ldgr.TransactionId = 0;
                    ldgr.SubLedgerId = WalletType;
                    ldgr.ToFromUser = rec0.Id;
                    if (ToWalletType == 5) { ldgr.Comment = "To Joining Wallet"; }
                    if (ToWalletType == 6) { ldgr.Comment = "To Withdrawal Wallet"; }
                    db.Ledgers.Add(ldgr);
                    db.SaveChanges();

                    Ledger ldgrT = new Ledger();
                    ldgrT.RegistrationId = rec0.Id;
                    ldgrT.WalletId = ToWalletType;
                    ldgrT.Date = DateTime.Now;
                    ldgrT.Withdraw = 0;
                    ldgrT.Deposit = Amount;
                    ldgrT.TransactionTypeId = 5;
                    ldgrT.TransactionId = 0;
                    ldgrT.SubLedgerId = WalletType;
                    ldgrT.ToFromUser = rec0.Id;
                    if (WalletType == 1) { ldgrT.Comment = "From Development Wallet"; }
                    if (WalletType == 2) { ldgrT.Comment = "From Referral Wallet"; }
                    if (WalletType == 3) { ldgrT.Comment = "From Royalty Club Wallet"; }
                    if (WalletType == 4) { ldgrT.Comment = "From Diamond Royalty Club Wallet"; }
                    db.Ledgers.Add(ldgrT);
                    db.SaveChanges();
                }
            }
            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetTeam(int? RegistrationId)   //Get Tree of a Id
        {
            long regId = 0;
            if (RegistrationId == null)
            {
                string username = User.Identity.Name;
                var rec = db.Registrations.SingleOrDefault(r => r.UserName.ToUpper().Trim() == username.ToUpper().Trim());
                regId = rec.Id;
            }
            List<Member> members = new List<Member>();

            var m = db.Members.Where(mm => mm.ReferrerRegistrationId == regId);
            members.AddRange(m);
            //long lastId = 0;

            //for (int i = 0; i < members.Count(); i++)
            //{
            //    lastId = members[i].RegistrationId;
            //    var n = db.Members.Where(mm => mm.UplineRegistrationId == lastId);
            //    members.AddRange(n);
            //}

            var MemList = members.Select(n => new { n.RegistrationId, n.Username, n.Defaultpackagecode, n.Country, Doj = n.Doj.ToShortDateString(), n.BinaryPosition, n.Achievement1, n.Achievement2 }).OrderByDescending(n => n.Doj);

            return Json(new { Members = MemList }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetTeamByNameFirstLevel(string Member)   //Get Tree of a name
        {
            var res = db.Members.Where(x => x.Username == Member).Single();
            long MemberId = res.Id;

            List<Member> members = new List<Member>();

            var m = db.Members.Where(mm => mm.UplineRegistrationId == MemberId);
            members.AddRange(m);

            var MemList = members.Select(n => new { n.Id, n.Username, n.ReferrerRegistrationId, n.Referrerusername, n.Level });

            return Json(new { Members = MemList }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetTeamByIdNextLevel(long Id)   //Get Tree of a name
        {
            long MemberId = Id;

            List<Member> members = new List<Member>();

            var m = db.Members.Where(mm => mm.UplineRegistrationId == MemberId);
            members.AddRange(m);

            var MemList = members.Select(n => new { n.Id, n.Username, n.ReferrerRegistrationId, n.Referrerusername, n.Level });

            return Json(new { Members = MemList }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetTeamByName(string Member)   //Get Tree of a name
        {
            if (Member == "")
            {
                Member = User.Identity.Name;
            }

            long regId = 10000000;
            List<Member> members = new List<Member>();

            var res = db.Members.SingleOrDefault(x => x.Username == Member);

            if (res != null)
            {
                #region found

                long MemberId = res.RegistrationId;
                int pkgcode = 0;
                long regid = 0;

                //Add the first member to array first
                List<Treemodel> MemberList = new List<Treemodel>();
                Treemodel Mem0 = new Treemodel();
                Mem0.id = res.RegistrationId;
                Mem0.UplineRegistrationId = res.RegistrationId;
                Mem0.parentId = null;
                Mem0.Name = res.Username;
                if (res.Achievement2 == 1)
                {
                    Mem0.pic = PackageImage(3);
                    Mem0.achievement = "Diamond Royalty Club";
                }
                else if (res.Achievement1 == 1)
                {
                    Mem0.pic = PackageImage(2);
                    Mem0.achievement = "Royalty Club";
                }
                else
                {
                    Mem0.pic = PackageImage(1);
                    Mem0.achievement = "";
                }
                //Mem0.pic = PackageImage((int)res.Defaultpackagecode);
                Mem0.isMember = true;
                Mem0.fullname = res.Firstname;
                Mem0.username = res.Username;
                Mem0.sponsorname = res.Referrerusername;
                Mem0.package = res.Emailid;
                Mem0.totalleft = (long)res.Leftmembers;
                Mem0.totalright = (long)res.Rightmembers;
                Mem0.businessleft = db.BinaryIncomes.Where(i => i.RegistrationId == res.RegistrationId).Select(bi => bi.LeftNewBusinessAmount).DefaultIfEmpty(0).Sum();
                Mem0.businessright = db.BinaryIncomes.Where(i => i.RegistrationId == res.RegistrationId).Select(bi => bi.RightNewBusinessAmount).DefaultIfEmpty(0).Sum(); ;
                MemberList.Add(Mem0);


                int param1 = 0;
                int param2 = 0;
                for (int lvl = 1; lvl <= 4; lvl++)
                {
                    if (lvl == 1) { param1 = 0; param2 = 0; }
                    if (lvl == 2) { param1 = 1; param2 = 2; }
                    if (lvl == 3) { param1 = 3; param2 = 6; }
                    if (lvl == 4) { param1 = 7; param2 = 14; }

                    #region Level 1,2,3,4
                    for (int i = param1; i <= param2; i++)
                    {
                        try
                        {
                            MemberId = MemberList[i].UplineRegistrationId;
                            var m = db.Members.Where(mm => mm.UplineRegistrationId == MemberId).OrderBy(mm => mm.BinaryPosition).ToList();
                            if (m.Count() == 0)
                            {
                                Treemodel Mem1 = new Treemodel();
                                regId = regId + 1;
                                Mem1.id = regId;
                                Mem1.UplineRegistrationId = regId;
                                Mem1.parentId = MemberList[i].id;
                                Mem1.Name = "L";
                                Mem1.isMember = false;
                                if (MemberList[i].isMember) { Mem1.pic = PackageImage(99); } else { Mem1.pic = ""; }
                                Mem1.position = "L";
                                MemberList.Add(Mem1);

                                Treemodel Mem2 = new Treemodel();
                                regId = regId + 1;
                                Mem2.id = regId;
                                Mem2.UplineRegistrationId = regId;
                                Mem2.parentId = MemberList[i].id;
                                Mem2.isMember = false;
                                Mem2.Name = "R";
                                if (MemberList[i].isMember) { Mem2.pic = PackageImage(99); } else { Mem2.pic = ""; }
                                Mem2.position = "R";
                                MemberList.Add(Mem2);
                            }

                            if (m.Count() == 1)
                            {
                                if (m[0].BinaryPosition == "L")
                                {
                                    Treemodel Mem1 = new Treemodel();
                                    regId = regId + 1;
                                    Mem1.id = regId;
                                    Mem1.UplineRegistrationId = m[0].RegistrationId;
                                    Mem1.parentId = MemberList[i].id;
                                    Mem1.Name = m[0].Username;
                                    Mem1.isMember = true;
                                    //Mem1.pic = PackageImage((int)m[0].Defaultpackagecode);
                                    if (m[0].Achievement2 == 1)
                                    {
                                        Mem1.pic = PackageImage(3);
                                        Mem1.achievement = "Diamond Royalty Club";
                                    }
                                    else if (m[0].Achievement1 == 1)
                                    {
                                        Mem1.pic = PackageImage(2);
                                        Mem1.achievement = "Royalty Club";

                                    }
                                    else
                                    {
                                        Mem1.pic = PackageImage(1);
                                        Mem1.achievement = "";

                                    }
                                    Mem1.position = "L";
                                    Mem1.fullname = m[0].Firstname;
                                    Mem1.username = m[0].Username;
                                    Mem1.sponsorname = m[0].Referrerusername;
                                    pkgcode = (int)m[0].Defaultpackagecode;
                                    Mem1.package = m[0].Emailid;
                                    Mem1.totalleft = (long)m[0].Leftmembers;
                                    Mem1.totalright = (long)m[0].Rightmembers;
                                    regid = m[0].RegistrationId;
                                    Mem1.businessleft = db.BinaryIncomes.Where(y => y.RegistrationId == regid).Select(bi => bi.LeftNewBusinessAmount).DefaultIfEmpty(0).Sum();
                                    Mem1.businessright = db.BinaryIncomes.Where(z => z.RegistrationId == regid).Select(bi => bi.RightNewBusinessAmount).DefaultIfEmpty(0).Sum();
                                    MemberList.Add(Mem1);

                                    Treemodel Mem2 = new Treemodel();
                                    regId = regId + 1;
                                    Mem2.id = regId;
                                    Mem2.UplineRegistrationId = regId;
                                    Mem2.parentId = MemberList[i].id;
                                    Mem2.Name = "R";
                                    Mem2.isMember = false;
                                    if (MemberList[i].isMember) { Mem2.pic = PackageImage(99); } else { Mem2.pic = ""; }
                                    Mem2.position = "R";
                                    MemberList.Add(Mem2);
                                }
                                if (m[0].BinaryPosition == "R")
                                {
                                    Treemodel Mem1 = new Treemodel();
                                    regId = regId + 1;
                                    Mem1.id = regId;
                                    Mem1.UplineRegistrationId = regId;
                                    Mem1.parentId = MemberList[i].id;
                                    Mem1.Name = "L";
                                    Mem1.isMember = false;
                                    if (MemberList[i].isMember) { Mem1.pic = PackageImage(99); } else { Mem1.pic = ""; }
                                    Mem1.position = "L";
                                    MemberList.Add(Mem1);

                                    Treemodel Mem2 = new Treemodel();
                                    regId = regId + 1;
                                    Mem2.id = regId;
                                    Mem2.UplineRegistrationId = m[0].RegistrationId;
                                    Mem2.parentId = MemberList[i].id;
                                    Mem2.Name = m[0].Username;
                                    Mem2.isMember = true;
                                    //Mem2.pic = PackageImage((int)m[0].Defaultpackagecode);
                                    if (m[0].Achievement2 == 1)
                                    {
                                        Mem2.pic = PackageImage(3);
                                        Mem2.achievement = "Diamond Royalty Club";
                                    }
                                    else if (m[0].Achievement1 == 1)
                                    {
                                        Mem2.pic = PackageImage(2);
                                        Mem2.achievement = "Royalty Club";

                                    }
                                    else
                                    {
                                        Mem2.pic = PackageImage(1);
                                        Mem2.achievement = "";

                                    }
                                    Mem2.position = "R";
                                    Mem2.fullname = m[0].Firstname;
                                    Mem2.username = m[0].Username;
                                    Mem2.sponsorname = m[0].Referrerusername;
                                    pkgcode = (int)m[0].Defaultpackagecode;
                                    Mem2.package = m[0].Emailid;
                                    Mem2.totalleft = (long)m[0].Leftmembers;
                                    Mem2.totalright = (long)m[0].Rightmembers;
                                    regid = m[0].RegistrationId;
                                    Mem2.businessleft = db.BinaryIncomes.Where(y => y.RegistrationId == regid).Select(bi => bi.LeftNewBusinessAmount).DefaultIfEmpty(0).Sum();
                                    Mem2.businessright = db.BinaryIncomes.Where(z => z.RegistrationId == regid).Select(bi => bi.RightNewBusinessAmount).DefaultIfEmpty(0).Sum();
                                    MemberList.Add(Mem2);



                                }
                            }
                            if (m.Count() == 2)
                            {
                                Treemodel Mem1 = new Treemodel();
                                regId = regId + 1;
                                Mem1.id = regId;
                                Mem1.UplineRegistrationId = m[0].RegistrationId;
                                Mem1.parentId = MemberList[i].id;
                                Mem1.Name = m[0].Username;
                                Mem1.isMember = true;
                                //Mem1.pic = PackageImage((int)m[0].Defaultpackagecode);
                                if (m[0].Achievement2 == 1)
                                {
                                    Mem1.pic = PackageImage(3);
                                    Mem1.achievement = "Diamond Royalty Club";
                                }
                                else if (m[0].Achievement1 == 1)
                                {
                                    Mem1.pic = PackageImage(2);
                                    Mem1.achievement = "Royalty Club";

                                }
                                else
                                {
                                    Mem1.pic = PackageImage(1);
                                    Mem1.achievement = "";

                                }
                                Mem1.position = "L";
                                Mem1.fullname = m[0].Firstname;
                                Mem1.username = m[0].Username;
                                Mem1.sponsorname = m[0].Referrerusername;
                                pkgcode = (int)m[0].Defaultpackagecode;
                                Mem1.package = m[0].Emailid;
                                Mem1.totalleft = (long)m[0].Leftmembers;
                                Mem1.totalright = (long)m[0].Rightmembers;
                                regid = m[0].RegistrationId;
                                Mem1.businessleft = db.BinaryIncomes.Where(y => y.RegistrationId == regid).Select(bi => bi.LeftNewBusinessAmount).DefaultIfEmpty(0).Sum();
                                Mem1.businessright = db.BinaryIncomes.Where(z => z.RegistrationId == regid).Select(bi => bi.RightNewBusinessAmount).DefaultIfEmpty(0).Sum();
                                MemberList.Add(Mem1);

                                Treemodel Mem2 = new Treemodel();
                                regId = regId + 1;
                                Mem2.id = regId;
                                Mem2.UplineRegistrationId = m[1].RegistrationId;
                                Mem2.parentId = MemberList[i].id;
                                Mem2.Name = m[1].Username;
                                Mem2.isMember = true;
                                //Mem2.pic = PackageImage((int)m[1].Defaultpackagecode);
                                if (m[1].Achievement2 == 1)
                                {
                                    Mem2.pic = PackageImage(3);
                                    Mem2.achievement = "Diamond Royalty Club";
                                }
                                else if (m[1].Achievement1 == 1)
                                {
                                    Mem2.pic = PackageImage(2);
                                    Mem2.achievement = "Royalty Club";

                                }
                                else
                                {
                                    Mem2.pic = PackageImage(1);
                                    Mem2.achievement = "";

                                }
                                Mem2.position = "R";
                                Mem2.fullname = m[1].Firstname;
                                Mem2.username = m[1].Username;
                                Mem2.sponsorname = m[1].Referrerusername;
                                pkgcode = (int)m[1].Defaultpackagecode;
                                Mem2.package = m[1].Emailid;
                                Mem2.totalleft = (long)m[1].Leftmembers;
                                Mem2.totalright = (long)m[1].Rightmembers;
                                regid = m[1].RegistrationId;
                                Mem2.businessleft = db.BinaryIncomes.Where(y => y.RegistrationId == regid).Select(bi => bi.LeftNewBusinessAmount).DefaultIfEmpty(0).Sum();
                                Mem2.businessright = db.BinaryIncomes.Where(z => z.RegistrationId == regid).Select(bi => bi.RightNewBusinessAmount).DefaultIfEmpty(0).Sum();
                                MemberList.Add(Mem2);

                            }
                        }
                        catch (Exception e)
                        {

                        }
                    }
                    #endregion
                }

                return Json(new { Members = MemberList }, JsonRequestBehavior.AllowGet);

                #endregion
            }
            else
            {
                #region not found

                List<Treemodel> MemList = new List<Treemodel>();
                Treemodel Mem0 = new Treemodel();
                Mem0.id = 0;
                Mem0.parentId = null;
                Mem0.Name = "";
                Mem0.pic = "";
                MemList.Add(Mem0);

                Treemodel Mem1 = new Treemodel();
                Mem1.id = 1;
                Mem1.parentId = 0;
                Mem1.Name = "";
                Mem1.pic = "";
                MemList.Add(Mem1);
                Treemodel Mem2 = new Treemodel();
                Mem2.id = 2;
                Mem2.parentId = 0;
                Mem2.Name = "";
                Mem2.pic = "";
                MemList.Add(Mem2);

                Treemodel Mem3 = new Treemodel();
                Mem3.id = 3;
                Mem3.parentId = 1;
                Mem3.Name = "";
                Mem3.pic = "";
                MemList.Add(Mem3);
                Treemodel Mem4 = new Treemodel();
                Mem4.id = 4;
                Mem4.parentId = 1;
                Mem4.Name = "";
                Mem4.pic = "";
                MemList.Add(Mem4);
                Treemodel Mem5 = new Treemodel();
                Mem5.id = 5;
                Mem5.parentId = 2;
                Mem5.Name = "";
                Mem5.pic = "";
                MemList.Add(Mem5);
                Treemodel Mem6 = new Treemodel();
                Mem6.id = 6;
                Mem6.parentId = 2;
                Mem6.Name = "";
                Mem6.pic = "";
                MemList.Add(Mem6);

                Treemodel Mem7 = new Treemodel();
                Mem7.id = 7;
                Mem7.parentId = 3;
                Mem7.Name = "";
                Mem7.pic = "";
                MemList.Add(Mem7);
                Treemodel Mem8 = new Treemodel();
                Mem8.id = 8;
                Mem8.parentId = 3;
                Mem8.Name = "";
                Mem8.pic = "";
                MemList.Add(Mem8);
                Treemodel Mem9 = new Treemodel();
                Mem9.id = 9;
                Mem9.parentId = 4;
                Mem9.Name = "";
                Mem9.pic = "";
                MemList.Add(Mem9);
                Treemodel Mem10 = new Treemodel();
                Mem10.id = 10;
                Mem10.parentId = 4;
                Mem10.Name = "";
                Mem10.pic = "";
                MemList.Add(Mem10);
                Treemodel Mem11 = new Treemodel();
                Mem11.id = 11;
                Mem11.parentId = 5;
                Mem11.Name = "";
                Mem11.pic = "";
                MemList.Add(Mem11);
                Treemodel Mem12 = new Treemodel();
                Mem12.id = 12;
                Mem12.parentId = 5;
                Mem12.Name = "";
                Mem12.pic = "";
                MemList.Add(Mem12);
                Treemodel Mem13 = new Treemodel();
                Mem13.id = 13;
                Mem13.parentId = 6;
                Mem13.Name = "";
                Mem13.pic = "";
                MemList.Add(Mem13);
                Treemodel Mem14 = new Treemodel();
                Mem14.id = 14;
                Mem14.parentId = 6;
                Mem14.Name = "";
                Mem14.pic = "";
                MemList.Add(Mem14);

                Treemodel Mem15 = new Treemodel();
                Mem15.id = 15;
                Mem15.parentId = 7;
                Mem15.Name = "";
                Mem15.pic = "";
                MemList.Add(Mem15);
                Treemodel Mem16 = new Treemodel();
                Mem16.id = 16;
                Mem16.parentId = 7;
                Mem16.Name = "";
                Mem16.pic = "";
                MemList.Add(Mem16);

                Treemodel Mem17 = new Treemodel();
                Mem17.id = 17;
                Mem17.parentId = 8;
                Mem17.Name = "";
                Mem17.pic = "";
                MemList.Add(Mem17);
                Treemodel Mem18 = new Treemodel();
                Mem18.id = 18;
                Mem18.parentId = 8;
                Mem18.Name = "";
                Mem18.pic = "";
                MemList.Add(Mem18);

                Treemodel Mem19 = new Treemodel();
                Mem19.id = 19;
                Mem19.parentId = 9;
                Mem19.Name = "";
                Mem19.pic = "";
                MemList.Add(Mem19);
                Treemodel Mem20 = new Treemodel();
                Mem20.id = 20;
                Mem20.parentId = 9;
                Mem20.Name = "";
                Mem20.pic = "";
                MemList.Add(Mem20);

                Treemodel Mem21 = new Treemodel();
                Mem21.id = 21;
                Mem21.parentId = 10;
                Mem21.Name = "";
                Mem21.pic = "";
                MemList.Add(Mem21);
                Treemodel Mem22 = new Treemodel();
                Mem22.id = 22;
                Mem22.parentId = 10;
                Mem22.Name = "";
                Mem22.pic = "";
                MemList.Add(Mem22);
                Treemodel Mem23 = new Treemodel();
                Mem23.id = 23;
                Mem23.parentId = 11;
                Mem23.Name = "";
                Mem23.pic = "";
                MemList.Add(Mem23);
                Treemodel Mem24 = new Treemodel();
                Mem24.id = 24;
                Mem24.parentId = 11;
                Mem24.Name = "";
                Mem24.pic = "";
                MemList.Add(Mem24);
                Treemodel Mem25 = new Treemodel();
                Mem25.id = 25;
                Mem25.parentId = 12;
                Mem25.Name = "";
                Mem25.pic = "";
                MemList.Add(Mem25);
                Treemodel Mem26 = new Treemodel();
                Mem26.id = 26;
                Mem26.parentId = 12;
                Mem26.Name = "";
                Mem26.pic = "";
                MemList.Add(Mem26);
                Treemodel Mem27 = new Treemodel();
                Mem27.id = 27;
                Mem27.parentId = 13;
                Mem27.Name = "";
                Mem27.pic = "";
                MemList.Add(Mem27);
                Treemodel Mem28 = new Treemodel();
                Mem28.id = 28;
                Mem28.parentId = 13;
                Mem28.Name = "";
                Mem28.pic = "";
                MemList.Add(Mem28);
                Treemodel Mem29 = new Treemodel();
                Mem29.id = 29;
                Mem29.parentId = 14;
                Mem29.Name = "";
                Mem29.pic = "";
                MemList.Add(Mem29);
                Treemodel Mem30 = new Treemodel();
                Mem30.id = 30;
                Mem30.parentId = 14;
                Mem30.Name = "";
                Mem30.pic = "";
                MemList.Add(Mem30);

                return Json(new { Members = MemList }, JsonRequestBehavior.AllowGet);

                #endregion
            }

        }

        [HttpGet]
        public JsonResult GetBinaryTeamByTree(string Member, int levels, int? Id)   //Get Tree of a name
        {
            int levelsToSshow = levels;


            //Temporary valiables    
            string name = "";           //username
            int nameLength = 0;         //length of username
            int stringStartpos = 0;     //username starting position within html string
            int stringEndpos = 0;       //username ending position after </a> following username
            string tmphtml = "";        //intermediate stage during html construction of a username
            string firstPart = "";      //before html insert operation, store the <prefix> part
            string lastPart = "";       //before html insert operation store the <suffix> part
            string defaultPackage = "";

            if (Member != "")
            {
                var mm = db.Members.SingleOrDefault(i => i.Username == Member);
                if (mm.Defaultpackagecode == 1) { defaultPackage = "pluto1.png"; }
                if (mm.Defaultpackagecode == 2) { defaultPackage = "Jupiter1.png"; }
                if (mm.Defaultpackagecode == 3) { defaultPackage = "Earth1.png"; }
                if (mm.Defaultpackagecode == 4) { defaultPackage = "Mercury1.png"; }
            }

            if (Member == "" && Id == null)
            {
                Member = User.Identity.Name;
                var mm = db.Members.SingleOrDefault(i => i.Username == Member);
                if (mm.Defaultpackagecode == 1) { defaultPackage = "pluto1.png"; }
                if (mm.Defaultpackagecode == 2) { defaultPackage = "Jupiter1.png"; }
                if (mm.Defaultpackagecode == 3) { defaultPackage = "Earth1.png"; }
                if (mm.Defaultpackagecode == 4) { defaultPackage = "Mercury1.png"; }
            }
            if (Member == "" && Id != null)
            {
                var mm = db.Members.SingleOrDefault(i => i.Id == Id);
                Member = mm.Username;
                if (mm.Defaultpackagecode == 1) { defaultPackage = "pluto1.png"; }
                if (mm.Defaultpackagecode == 2) { defaultPackage = "Jupiter1.png"; }
                if (mm.Defaultpackagecode == 3) { defaultPackage = "Earth1.png"; }
                if (mm.Defaultpackagecode == 4) { defaultPackage = "Mercury1.png"; }
            }

            string MemTree = "<ul><li><a uib-popover-html='<b>HTML</b>, <i>inline</i>'>" + "<img src='../Content/" + defaultPackage + "' class='imgclass'/><br/>" + Member + "</a></li></ul>"; //the actual html string

            var res = db.Members.Where(x => x.Username == Member).Single();
            long MemberId = res.RegistrationId;
            int? currentMemberLevel = res.Level;
            int maximumlevel = (int)currentMemberLevel + levelsToSshow - 1;

            List<Member> members = new List<Member>();

            var m = db.Members.Where(mm => mm.UplineRegistrationId == MemberId);
            members.AddRange(m);

            if (m.Count() > 0)
            {
                name = Member.Trim();
                nameLength = name.Length;
                stringStartpos = MemTree.IndexOf(name, 0);
                stringEndpos = stringStartpos + name.Length + 4;
                tmphtml = "<ul>";
                foreach (Member mem in m)
                {
                    if (mem.Defaultpackagecode == 1) { defaultPackage = "pluto1.png"; }
                    if (mem.Defaultpackagecode == 2) { defaultPackage = "Jupiter1.png"; }
                    if (mem.Defaultpackagecode == 3) { defaultPackage = "Earth1.png"; }
                    if (mem.Defaultpackagecode == 4) { defaultPackage = "Mercury1.png"; }
                    tmphtml = tmphtml + "<li><a onmouseover='myFunction()'>" + "<img src='../Content/" + defaultPackage + "' class='imgclass'/><br/>" + mem.Username + "</a></li>";
                }
                tmphtml = tmphtml + "</ul>";
                firstPart = MemTree.Substring(0, stringEndpos);
                lastPart = MemTree.Substring(stringEndpos);
                MemTree = firstPart + tmphtml + lastPart;
            }
            long lastId = 0;
            string lastIdUsername = "";

            for (int i = 0; i < members.Count(); i++)
            {
                if (members[i].Level < maximumlevel)
                {
                    lastId = members[i].RegistrationId;
                    lastIdUsername = members[i].Username;
                    var n = db.Members.Where(mm => mm.UplineRegistrationId == lastId);
                    members.AddRange(n);
                    if (n.Count() > 0)
                    {
                        name = lastIdUsername.Trim();
                        nameLength = name.Length;
                        stringStartpos = MemTree.IndexOf(name, 0);
                        stringEndpos = stringStartpos + name.Length + 4;
                        tmphtml = "<ul>";
                        foreach (Member mem in n)
                        {
                            if (mem.Defaultpackagecode == 1) { defaultPackage = "pluto1.png"; }
                            if (mem.Defaultpackagecode == 2) { defaultPackage = "Jupiter1.png"; }
                            if (mem.Defaultpackagecode == 3) { defaultPackage = "Earth1.png"; }
                            if (mem.Defaultpackagecode == 4) { defaultPackage = "Mercury1.png"; }
                            tmphtml = tmphtml + "<li><a ng-click='clicksearch(" + mem.Id + ")'>" + "<img src='../Content/" + defaultPackage + "' class='imgclass'/><br/>" + mem.Username + "</a></li>";
                        }
                        tmphtml = tmphtml + "</ul>";
                        firstPart = MemTree.Substring(0, stringEndpos);
                        lastPart = MemTree.Substring(stringEndpos);
                        MemTree = firstPart + tmphtml + lastPart;
                    }
                }
            }

            return Json(new { Members = MemTree }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetUnilevelTeamByTree(string Member, int levels, int? Id)   //Get Tree of a name
        {
            int levelsToSshow = levels;


            //Temporary valiables    
            string name = "";           //username
            int nameLength = 0;         //length of username
            int stringStartpos = 0;     //username starting position within html string
            int stringEndpos = 0;       //username ending position after </a> following username
            string tmphtml = "";        //intermediate stage during html construction of a username
            string firstPart = "";      //before html insert operation, store the <prefix> part
            string lastPart = "";       //before html insert operation store the <suffix> part
            string defaultPackage = "";

            if (Member != "")
            {
                var mm = db.Members.SingleOrDefault(i => i.Username == Member);
                if (mm.Defaultpackagecode == 1) { defaultPackage = "pluto1.png"; }
                if (mm.Defaultpackagecode == 2) { defaultPackage = "Jupiter1.png"; }
                if (mm.Defaultpackagecode == 3) { defaultPackage = "Earth1.png"; }
                if (mm.Defaultpackagecode == 4) { defaultPackage = "Mercury1.png"; }
            }

            if (Member == "" && Id == null)
            {
                Member = User.Identity.Name;
                var mm = db.Members.SingleOrDefault(i => i.Username == Member);
                if (mm.Defaultpackagecode == 1) { defaultPackage = "pluto1.png"; }
                if (mm.Defaultpackagecode == 2) { defaultPackage = "Jupiter1.png"; }
                if (mm.Defaultpackagecode == 3) { defaultPackage = "Earth1.png"; }
                if (mm.Defaultpackagecode == 4) { defaultPackage = "Mercury1.png"; }
            }
            if (Member == "" && Id != null)
            {
                var mm = db.Members.SingleOrDefault(i => i.Id == Id);
                Member = mm.Username;
                if (mm.Defaultpackagecode == 1) { defaultPackage = "pluto1.png"; }
                if (mm.Defaultpackagecode == 2) { defaultPackage = "Jupiter1.png"; }
                if (mm.Defaultpackagecode == 3) { defaultPackage = "Earth1.png"; }
                if (mm.Defaultpackagecode == 4) { defaultPackage = "Mercury1.png"; }
            }

            string MemTree = "<ul><li><a uib-popover-html='<b>HTML</b>, <i>inline</i>'>" + "<img src='../Content/" + defaultPackage + "' class='imgclass'/><br/>" + Member + "</a></li></ul>"; //the actual html string

            var res = db.Members.Where(x => x.Username == Member).Single();
            long MemberId = res.RegistrationId;
            int? currentMemberLevel = res.Level;
            int maximumlevel = (int)currentMemberLevel + levelsToSshow - 1;

            List<Member> members = new List<Member>();

            var m = db.Members.Where(mm => mm.ReferrerRegistrationId == MemberId);
            members.AddRange(m);

            if (m.Count() > 0)
            {
                name = Member.Trim();
                nameLength = name.Length;
                stringStartpos = MemTree.IndexOf(name, 0);
                stringEndpos = stringStartpos + name.Length + 4;
                tmphtml = "<ul>";
                foreach (Member mem in m)
                {
                    if (mem.Defaultpackagecode == 1) { defaultPackage = "pluto1.png"; }
                    if (mem.Defaultpackagecode == 2) { defaultPackage = "Jupiter1.png"; }
                    if (mem.Defaultpackagecode == 3) { defaultPackage = "Earth1.png"; }
                    if (mem.Defaultpackagecode == 4) { defaultPackage = "Mercury1.png"; }
                    tmphtml = tmphtml + "<li><a onmouseover='myFunction()'>" + "<img src='../Content/" + defaultPackage + "' class='imgclass'/><br/>" + mem.Username + "</a></li>";
                }
                tmphtml = tmphtml + "</ul>";
                firstPart = MemTree.Substring(0, stringEndpos);
                lastPart = MemTree.Substring(stringEndpos);
                MemTree = firstPart + tmphtml + lastPart;
            }
            long lastId = 0;
            string lastIdUsername = "";

            for (int i = 0; i < members.Count(); i++)
            {
                if (members[i].Level < maximumlevel)
                {
                    lastId = members[i].RegistrationId;
                    lastIdUsername = members[i].Username;
                    var n = db.Members.Where(mm => mm.ReferrerRegistrationId == lastId);
                    members.AddRange(n);
                    if (n.Count() > 0)
                    {
                        name = lastIdUsername.Trim();
                        nameLength = name.Length;
                        stringStartpos = MemTree.IndexOf(name, 0);
                        stringEndpos = stringStartpos + name.Length + 4;
                        tmphtml = "<ul>";
                        foreach (Member mem in n)
                        {
                            if (mem.Defaultpackagecode == 1) { defaultPackage = "pluto1.png"; }
                            if (mem.Defaultpackagecode == 2) { defaultPackage = "Jupiter1.png"; }
                            if (mem.Defaultpackagecode == 3) { defaultPackage = "Earth1.png"; }
                            if (mem.Defaultpackagecode == 4) { defaultPackage = "Mercury1.png"; }
                            tmphtml = tmphtml + "<li><a ng-click='clicksearch(" + mem.Id + ")'>" + "<img src='../Content/" + defaultPackage + "' class='imgclass'/><br/>" + mem.Username + "</a></li>";
                        }
                        tmphtml = tmphtml + "</ul>";
                        firstPart = MemTree.Substring(0, stringEndpos);
                        lastPart = MemTree.Substring(stringEndpos);
                        MemTree = firstPart + tmphtml + lastPart;
                    }
                }
            }

            return Json(new { Members = MemTree }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult IsUserNameExist(string UserName)
        {
            bool isExist = true;
            long Id = 0;
            Member mem = db.Members.SingleOrDefault(m => m.Username.ToUpper().Trim() == UserName.ToUpper().Trim());
            if (mem == null)
            {
                isExist = false;
            }
            else
            {
                Register reg = db.Registrations.SingleOrDefault(m => m.UserName.ToUpper().Trim() == UserName.ToUpper().Trim());
                Id = reg.Id;
            }
            return Json(new { Found = isExist, ReferrerId = Id }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsUserMember()
        {
            bool isExist = true;
            bool trnPwdExists = false;
            string UserName = User.Identity.Name;
            long Id = 0;
            Member mem = db.Members.SingleOrDefault(m => m.Username.ToUpper().Trim() == UserName.ToUpper().Trim());
            if (mem == null)
            {
                isExist = false;
            }
            else
            {
                isExist = true;
                string trnpwd = db.Registrations.Where(r => r.UserName == UserName).Select(r => r.TrxPassword).FirstOrDefault();
                trnPwdExists = (trnpwd == null ? false : true);
            }
            return Json(new { Found = isExist, TrnPasswordExists = trnPwdExists }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateTransactionPassword(string TxPassword)
        {
            string user = User.Identity.Name;
            var rec = db.Registrations.SingleOrDefault(r => r.UserName == user);
            rec.TrxPassword = TxPassword;
            db.SaveChanges();
            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult MatchTrnPassword(string TxPassword)
        {
            Boolean isMatched = false;
            string user = User.Identity.Name;
            var rec = db.Registrations.SingleOrDefault(r => r.UserName == user);
            isMatched = (rec.TrxPassword == TxPassword ? true : false);
            return Json(new { Success = isMatched }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetMyAccountNo()
        {
            string user = User.Identity.Name;
            var reg = db.Registrations.SingleOrDefault(r => r.UserName.ToUpper() == user.ToUpper());
            return Json(new { WalletAc = reg.MyWalletAccount, Bank=reg.MyBankName, BankAc=reg.MyBankAccountNum }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult MyAccountNo(string WalletId, string Bank, string BankAc)
        {
            string user = User.Identity.Name;
            var reg = db.Registrations.SingleOrDefault(r => r.UserName.ToUpper() == user.ToUpper());
            reg.MyWalletAccount = WalletId;
            reg.MyBankName = Bank;
            reg.MyBankAccountNum = BankAc;
            db.SaveChanges();
            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult IsUserNameExist1(string UserName)
        {
            bool isExist = true;
            var mem = db.Registrations.SingleOrDefault(m => m.UserName.ToUpper().Trim() == UserName.ToUpper().Trim());
            string name = "";
            string email = "";
            string countrycode = "";
            if (mem == null)
            {
                isExist = false;
            }
            else
            {
                name = mem.FullName;
                email = mem.EmailId;
                countrycode = mem.CountryCode;
            }
            return Json(new { Found = isExist, Name = name, Email = email, Countrycode = countrycode }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult IsEmailPresent(string EmailId)
        {
            bool isExist = true;
            var mem = db.Registrations.Where(m => m.EmailId.ToLower().Trim() == EmailId.ToLower().Trim());
            if (mem == null || mem.Count() == 0)
            {
                isExist = false;
            }
            return Json(new { Found = isExist }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult MyWalletBalance(int WalletId)
        {
            string username = User.Identity.Name;
            var rec = db.Registrations.SingleOrDefault(r => r.UserName.ToUpper().Trim() == username.ToUpper().Trim());
            double sumdeposit = db.Ledgers.Where(i => i.RegistrationId == rec.Id && i.WalletId == WalletId).Select(i => i.Deposit).DefaultIfEmpty(0).Sum();
            double sumwithdrawal = db.Ledgers.Where(i => i.RegistrationId == rec.Id && i.WalletId == WalletId).Select(i => i.Withdraw).DefaultIfEmpty(0).Sum(); ;
            double balance = sumdeposit - sumwithdrawal;
            return Json(new { Balance = balance }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult UserWalletBalance(string username)
        {
            if (username == "")
            {
                username = User.Identity.Name;
            }
            var rec = db.Registrations.SingleOrDefault(r => r.UserName.ToUpper().Trim() == username.ToUpper().Trim());

            double wallet1_sumdeposit = db.Ledgers.Where(i => i.RegistrationId == rec.Id && i.WalletId == 1).Select(bi => bi.Deposit).DefaultIfEmpty(0).Sum();
            double wallet1_sumwithdrawal = db.Ledgers.Where(i => i.RegistrationId == rec.Id && i.WalletId == 1).Select(bi => bi.Withdraw).DefaultIfEmpty(0).Sum();
            double wallet1_balance = wallet1_sumdeposit - wallet1_sumwithdrawal;

            double wallet2_sumdeposit = db.Ledgers.Where(i => i.RegistrationId == rec.Id && i.WalletId == 2).Select(bi => bi.Deposit).DefaultIfEmpty(0).Sum();
            double wallet2_sumwithdrawal = db.Ledgers.Where(i => i.RegistrationId == rec.Id && i.WalletId == 2).Select(bi => bi.Withdraw).DefaultIfEmpty(0).Sum();
            double wallet2_balance = wallet2_sumdeposit - wallet2_sumwithdrawal;

            double wallet3_sumdeposit = db.Ledgers.Where(i => i.RegistrationId == rec.Id && i.WalletId == 3).Select(bi => bi.Deposit).DefaultIfEmpty(0).Sum();
            double wallet3_sumwithdrawal = db.Ledgers.Where(i => i.RegistrationId == rec.Id && i.WalletId == 3).Select(bi => bi.Withdraw).DefaultIfEmpty(0).Sum();
            double wallet3_balance = wallet3_sumdeposit - wallet3_sumwithdrawal;

            double wallet4_sumdeposit = db.Ledgers.Where(i => i.RegistrationId == rec.Id && i.WalletId == 4).Select(bi => bi.Deposit).DefaultIfEmpty(0).Sum();
            double wallet4_sumwithdrawal = db.Ledgers.Where(i => i.RegistrationId == rec.Id && i.WalletId == 4).Select(bi => bi.Withdraw).DefaultIfEmpty(0).Sum();
            double wallet4_balance = wallet4_sumdeposit - wallet4_sumwithdrawal;

            double wallet5_sumdeposit = db.Ledgers.Where(i => i.RegistrationId == rec.Id && i.WalletId == 5).Select(bi => bi.Deposit).DefaultIfEmpty(0).Sum();
            double wallet5_sumwithdrawal = db.Ledgers.Where(i => i.RegistrationId == rec.Id && i.WalletId == 5).Select(bi => bi.Withdraw).DefaultIfEmpty(0).Sum();
            double wallet5_balance = wallet5_sumdeposit - wallet5_sumwithdrawal;

            double wallet6_sumdeposit = db.Ledgers.Where(i => i.RegistrationId == rec.Id && i.WalletId == 6).Select(bi => bi.Deposit).DefaultIfEmpty(0).Sum();
            double wallet6_sumwithdrawal = db.Ledgers.Where(i => i.RegistrationId == rec.Id && i.WalletId == 6).Select(bi => bi.Withdraw).DefaultIfEmpty(0).Sum();
            double wallet6_balance = wallet6_sumdeposit - wallet6_sumwithdrawal;

            return Json(new { Wallet1_Balance = wallet1_balance, Wallet2_Balance = wallet2_balance, Wallet3_Balance = wallet3_balance, Wallet4_Balance = wallet4_balance, Wallet5_Balance = wallet5_balance, Wallet6_Balance = wallet6_balance, }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<JsonResult> MyNewPurchase(int packageId, double investmentAmt)
        {
            string username = User.Identity.Name;
            var rec = db.Registrations.SingleOrDefault(r => r.UserName.ToUpper().Trim() == username.ToUpper().Trim());

            //1...add a withdrawal entry in ledger
            //2...add an entry in purchase
            //3...update registration and ledger
            //4...add to member register
            //5...update left right count of uplines
            //6...weekly fixed income calculation
            //7...weekly binary income calculation
            //8...weekly sponsor income calculation
            //9...weekly generation income calculation
            //10..send confirmation email


            Guid g = Guid.NewGuid();
            string GuidString = Convert.ToBase64String(g.ToByteArray());
            GuidString = GuidString.Replace("=", "");
            GuidString = GuidString.Replace("+", "");

            //1...
            Ledger ldgr = new Ledger();
            ldgr.RegistrationId = rec.Id;
            ldgr.WalletId = 5;
            ldgr.Date = DateTime.Now;
            ldgr.Deposit = 0;
            ldgr.Withdraw = investmentAmt;
            if (!rec.Joined) { ldgr.TransactionTypeId = 4; } else { ldgr.TransactionTypeId = 9; }
            ldgr.TransactionId = 0;
            ldgr.SubLedgerId = 1;
            ldgr.BatchNo = GuidString;

            db.Ledgers.Add(ldgr);
            //await db.SaveChangesAsync();
            db.SaveChanges();

            //2...
            Purchase pur = new Purchase();
            pur.Purchasedate = DateTime.Now;
            if (!rec.Joined) { pur.Packageid = packageId; } else { pur.Packageid = 1; packageId = 1; } //0==repurchase
            pur.RegistrationId = rec.Id;
            pur.Amount = 2000;
            pur.Payreferenceno = GuidString;
            pur.Isapproved = true;

            db.Purchases.Add(pur);
            //await db.SaveChangesAsync();
            db.SaveChanges();

            //3...
            ldgr.TransactionId = pur.Id;
            Boolean isJoined = rec.Joined; //before update store last state of isJoined
            rec.Joined = true;
            //await db.SaveChangesAsync();
            db.SaveChanges();

            //4...
            long uplineId = 0;
            if (!isJoined)
            {
                try
                {
                    Member mem = new Member();
                    mem.Doj = rec.CreatedDate;
                    mem.Activationdate = DateTime.Now;
                    mem.Firstname = rec.FullName;
                    mem.Username = rec.UserName;
                    mem.Emailid = rec.EmailId;
                    mem.Isactive = true;
                    mem.RegistrationId = rec.Id;
                    mem.ReferrerRegistrationId = rec.ReferrerId;
                    mem.Referrerusername = rec.ReferrerName;
                    mem.Uplineusername = GetExtremeLeftRight(rec.ReferrerName, rec.BinaryPosition);
                    var temprec = db.Registrations.SingleOrDefault(b => b.UserName.ToUpper().Trim() == mem.Uplineusername.ToUpper().Trim());
                    mem.UplineRegistrationId = temprec.Id;
                    uplineId = (long)mem.UplineRegistrationId;
                    mem.Level = GetnewLevel(uplineId);
                    mem.BinaryPosition = rec.BinaryPosition;
                    mem.Defaultpackagecode = packageId;
                    mem.Totalmembers = 0;
                    mem.Rightmembers = 0;
                    mem.Leftmembers = 0;
                    mem.Country = "BD";
                    db.Members.Add(mem);
                    //await db.SaveChangesAsync();
                    db.SaveChanges();
                }
                catch (Exception e)
                {

                }

                //5...
                string searchname = rec.UserName;
                string searchpos = rec.BinaryPosition;
                var res0 = db.Members.Where(x1 => x1.Username == searchname).Single();
                long MemberId = res0.RegistrationId;
                Boolean keepgoing = true;

                do
                {
                    var res1 = db.Members.SingleOrDefault(m => m.RegistrationId == uplineId);
                    if (res1 != null)
                    {
                        if (searchpos == "L") { res1.Leftmembers = (res1.Leftmembers == null) ? 1 : res1.Leftmembers + 1; }
                        if (searchpos == "R") { res1.Rightmembers = (res1.Rightmembers == null) ? 1 : res1.Rightmembers + 1; }
                        res1.Totalmembers = (res1.Totalmembers == null) ? 1 : res1.Totalmembers + 1;
                        searchpos = res1.BinaryPosition;
                        db.SaveChanges();
                        uplineId = (long)res1.UplineRegistrationId;
                    }
                    else
                    {
                        keepgoing = false;
                    }

                } while (keepgoing);
            }

            //6...

            //7...
            string searchname1 = rec.UserName;
            string searchpos1 = rec.BinaryPosition;
            var res = db.Members.Where(x2 => x2.Username == searchname1).Single();
            long MemberId1 = res.RegistrationId;
            Boolean keepgoing1 = true;
            long uplineId1 = (long)res.UplineRegistrationId;
            do
            {
                var res1 = db.Members.SingleOrDefault(m => m.RegistrationId == uplineId1);
                if (res1 != null)
                {
                    await BinaryIncomeCalculation(uplineId1, res1.Doj, investmentAmt, searchpos1, isJoined, rec.Id, packageId, GuidString);
                    uplineId1 = (long)res1.UplineRegistrationId;
                    searchpos1 = res1.BinaryPosition;
                }
                else
                {
                    keepgoing1 = false;
                }

            } while (keepgoing1);

            //8...
            var res2 = db.Members.SingleOrDefault(m => m.RegistrationId == res.ReferrerRegistrationId);
            await SponsorIncomeCalculation(res.ReferrerRegistrationId, res2.Doj, investmentAmt, res2.BinaryPosition, isJoined, rec.Id, packageId, GuidString);

            ////9..


            //10..
            //SendCandidateEmail(rec.EmailId);


            return Json(new { Success = "TRUE" }, JsonRequestBehavior.AllowGet);
        }

        //called from registration form of new member
        public async Task<JsonResult> AutoPurchase(string username, long UplineId, int packageId, double investmentAmt, string mode)
        {
            var rec = db.Registrations.SingleOrDefault(r => r.UserName.ToUpper().Trim() == username.ToUpper().Trim());
            if (rec == null)
            {
                Thread.Sleep(2000);
                rec = db.Registrations.SingleOrDefault(r => r.UserName.ToUpper().Trim() == username.ToUpper().Trim());
            }
            Boolean isJoined = false;

            //1...add a withdrawal entry in ledger
            //2...add an entry in purchase
            //3...update registration and ledger
            //4...add to member register
            //5...update left right count of uplines
            //6...update achievement
            //7...weekly binary income calculation
            //8...weekly sponsor income calculation
            //9...weekly generation income calculation
            //10..send confirmation email

            Guid g = Guid.NewGuid();
            string GuidString = Convert.ToBase64String(g.ToByteArray());
            GuidString = GuidString.Replace("=", "");
            GuidString = GuidString.Replace("+", "");
            Ledger ldgr = new Ledger();
            try
            {
                //1...

                ldgr.RegistrationId = rec.Id;
                ldgr.WalletId = 5;              //to change
                ldgr.Date = DateTime.Now;
                ldgr.Deposit = 0;
                ldgr.Withdraw = 2000;           //to change
                if (!rec.Joined) { ldgr.TransactionTypeId = 4; } else { ldgr.TransactionTypeId = 9; }
                ldgr.TransactionId = 0;
                ldgr.SubLedgerId = 1;
                ldgr.ToFromUser = 1;
                ldgr.BatchNo = GuidString;
                db.Ledgers.Add(ldgr);
                db.SaveChanges();
            }
            catch (Exception e)
            {

            }
            Purchase pur = new Purchase();
            try
            {
                //2...

                pur.Purchasedate = DateTime.Now;
                if (!rec.Joined) { pur.Packageid = packageId; } else { pur.Packageid = 1; packageId = 1; } //0==repurchase
                pur.RegistrationId = rec.Id;
                pur.Amount = investmentAmt;
                pur.Payreferenceno = GuidString;
                pur.Isapproved = true;

                db.Purchases.Add(pur);
                //await db.SaveChangesAsync();
                db.SaveChanges();
            }
            catch (Exception e)
            {

            }
            try
            {
                //3...
                ldgr.TransactionId = pur.Id;
                isJoined = rec.Joined; //before update store last state of isJoined
                rec.Joined = true;
                //await db.SaveChangesAsync();
                db.SaveChanges();

            }
            catch (Exception e)
            {

            }
            //4...
            long uplineId = 0;
            Member mem = new Member();
            if (!isJoined)
            {
                try
                {
                    mem.Doj = rec.CreatedDate;
                    mem.Activationdate = DateTime.Now;
                    mem.Firstname = rec.FullName;
                    mem.Username = rec.UserName;
                    mem.Emailid = rec.EmailId;
                    mem.Isactive = true;
                    mem.RegistrationId = rec.Id;
                    mem.ReferrerRegistrationId = rec.ReferrerId;
                    mem.Referrerusername = rec.ReferrerName;

                    if (mode == "Form")
                    {
                        mem.Uplineusername = GetExtremeLeftRight(rec.ReferrerName, rec.BinaryPosition);
                        var temprec = db.Registrations.SingleOrDefault(b => b.UserName.ToUpper().Trim() == mem.Uplineusername.ToUpper().Trim());
                        mem.UplineRegistrationId = temprec.Id;
                    }
                    if (mode == "Tree")
                    {
                        var temprec = db.Registrations.SingleOrDefault(b => b.Id == UplineId);
                        mem.UplineRegistrationId = temprec.Id;
                        mem.Uplineusername = temprec.UserName;
                    }

                    uplineId = (long)mem.UplineRegistrationId;
                    mem.Level = GetnewLevel(uplineId);
                    mem.BinaryPosition = rec.BinaryPosition;
                    mem.Defaultpackagecode = packageId;
                    mem.Totalmembers = 0;
                    mem.Rightmembers = 0;
                    mem.Leftmembers = 0;
                    mem.Country = "BD";
                    db.Members.Add(mem);
                    //await db.SaveChangesAsync();
                    db.SaveChanges();
                }
                catch (Exception e)
                {

                }

                //5...
                string searchname = rec.UserName;
                string searchpos = rec.BinaryPosition;
                var res0 = db.Members.Where(x1 => x1.Username == searchname).Single();
                long MemberId = res0.RegistrationId;
                Boolean keepgoing = true;

                do
                {
                    var res1 = db.Members.SingleOrDefault(m => m.RegistrationId == uplineId);
                    if (res1 != null)
                    {
                        if (searchpos == "L") { res1.Leftmembers = (res1.Leftmembers == null) ? 1 : res1.Leftmembers + 1; }
                        if (searchpos == "R") { res1.Rightmembers = (res1.Rightmembers == null) ? 1 : res1.Rightmembers + 1; }
                        res1.Totalmembers = (res1.Totalmembers == null) ? 1 : res1.Totalmembers + 1;
                        searchpos = res1.BinaryPosition;
                        db.SaveChanges();
                        uplineId = (long)res1.UplineRegistrationId;
                    }
                    else
                    {
                        keepgoing = false;
                    }

                } while (keepgoing);
            }

            //6...

            IsAchievements(mem.Uplineusername);

            //7...
            string searchname1 = rec.UserName;
            string searchpos1 = rec.BinaryPosition;
            var res = db.Members.Where(x2 => x2.Username == searchname1).Single();
            long MemberId1 = res.RegistrationId;
            Boolean keepgoing1 = true;
            long uplineId1 = (long)res.UplineRegistrationId;
            do
            {
                var res1 = db.Members.SingleOrDefault(m => m.RegistrationId == uplineId1);
                if (res1 != null)
                {
                    await BinaryIncomeCalculation(uplineId1, res1.Doj, investmentAmt, searchpos1, isJoined, rec.Id, packageId, GuidString);
                    uplineId1 = (long)res1.UplineRegistrationId;
                    searchpos1 = res1.BinaryPosition;
                }
                else
                {
                    keepgoing1 = false;
                }

            } while (keepgoing1);

            //8...
            var res2 = db.Members.SingleOrDefault(m => m.RegistrationId == res.ReferrerRegistrationId);
            await SponsorIncomeCalculation(res.ReferrerRegistrationId, res2.Doj, investmentAmt, res2.BinaryPosition, isJoined, rec.Id, packageId, GuidString);

            //9..


            return Json(new { Success = "TRUE" }, JsonRequestBehavior.AllowGet);
        }

        public void IsAchievements(string parentmember)
        {
            var mylevel = db.Members.Where(m => m.Uplineusername.ToUpper() == parentmember.ToUpper()).ToList();
            if (mylevel.Count() == 2)
            {
                if (mylevel[0].Emailid == mylevel[1].Emailid)
                {
                    //my siblings found, now check papa, are we a family?
                    var papa = db.Members.SingleOrDefault(m => m.Username == parentmember);
                    if (papa.Emailid == mylevel[0].Emailid && papa.Emailid == mylevel[1].Emailid && mylevel[0].ReferrerRegistrationId == papa.RegistrationId && mylevel[1].ReferrerRegistrationId == papa.RegistrationId)
                    {
                        //papa is eligible for Royalty club but one further verification of grandpa
                        string grandpaid = papa.Uplineusername;
                        var grandpa = db.Members.SingleOrDefault(m => m.Username == grandpaid);
                        if (grandpa.Emailid == papa.Emailid && grandpa.Achievement1 == 1)
                        {
                            //do not qualify as papa is already a part of a royalty club formation of grandpa
                        }
                        else
                        {
                            //Hurray!!! papa is a royalty club member, update achiever's field of papa
                            papa.Achievement1 = 1;
                            papa.Achievement1Date = DateTime.Now;
                            db.SaveChanges();

                            //check papa's sponsor whether eligible for diamond royalty club membership
                            string papa_Sponsor = papa.Referrerusername;
                            var papasponsor = db.Members.SingleOrDefault(m => m.Username == papa_Sponsor);
                            //sponsor is also a royalty club achiever, and sponsor and papa are not same person
                            if (papasponsor.Achievement1 == 1 && papasponsor.Emailid != papa.Emailid)
                            {
                                //update the count, leftside if Papa is on left side of papasponsor other wise rightside, if Papa is on rightside of papasponsor
                                string leftORright = ExtremeLeftRightTOP(papa.Username, papasponsor.Username);
                                if (leftORright == "LEFT") { papasponsor.Achievers1CountTeamLeftside = papasponsor.Achievers1CountTeamLeftside + 1; }
                                if (leftORright == "RIGHT") { papasponsor.Achievers1CountTeamRightside = papasponsor.Achievers1CountTeamRightside + 1; }
                                db.SaveChanges();

                                if (papasponsor.Achievers1CountTeamLeftside >= 1 && papasponsor.Achievers1CountTeamRightside >= 1 && papasponsor.Achievement2 == 0)
                                {
                                    int teamAchieversCount = 2;
                                    papasponsor.Achievers1CountTeam = teamAchieversCount;
                                    if (teamAchieversCount == 2) //sponsor has sponsored two Royal Club members, one on the left and one on the right
                                    {
                                        //Hurray!!! Papa's sponsor is a Diamond Royalty club member
                                        papasponsor.Achievement2 = 1;
                                        papasponsor.Achievement2Date = DateTime.Now;
                                    }
                                    db.SaveChanges();
                                }
                            }
                        }
                    }
                }
            }
        }

        public string ExtremeLeftRightTOP(string papa, string papasponsor)
        {
            string leftORright = "";

            string searchname = papa;
            string searchpos = "";
            var res = db.Members.Where(x => x.Username.ToUpper().Trim() == searchname.ToUpper().Trim()).Single();
            string Membername = res.Uplineusername;
            searchpos = res.BinaryPosition;
            Boolean keepgoing = true;
            do
            {
                var res1 = db.Members.SingleOrDefault(m => m.Username == Membername);
                Membername = res1.Uplineusername;
                searchpos = res1.BinaryPosition;
                if (Membername.ToUpper() == papasponsor.ToUpper())
                {
                    keepgoing = false;
                }

            } while (keepgoing);

            if (searchpos == "L") { leftORright = "LEFT"; }
            if (searchpos == "R") { leftORright = "RIGHT"; }

            return leftORright;
        }
        [HttpGet]
        public JsonResult MyAddress()
        {
            string username = User.Identity.Name;
            var rec = db.Members.Select(r => new { Username = r.Username, Fullname = r.Firstname, AddressLine1 = r.Addressline1, AddressLine2 = r.Addressline2, City = r.City, State = r.State, Country = r.Country, EmailId = r.Emailid }).SingleOrDefault(r => r.Username.ToUpper().Trim() == username.ToUpper().Trim());
            return Json(new { Address = rec }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult MyPurchases()
        {
            string username = User.Identity.Name;
            var rec = db.Registrations.SingleOrDefault(r => r.UserName.ToUpper().Trim() == username.ToUpper().Trim());

            var purchases = (from p in db.Purchases
                             from s in db.Packages
                             where p.Packageid == s.Id && p.RegistrationId == rec.Id && p.Packageid > 0
                             orderby p.Purchasedate
                             select new
                             {
                                 PurchaseDate = p.Purchasedate.Day + "-" + p.Purchasedate.Month + "-" + p.Purchasedate.Year,
                                 ReferenceNo = p.Payreferenceno,
                                 Status = "Paid",
                                 Package = s.Name + " package",
                                 Quantity = 1,
                                 Amount = p.Amount,
                                 Paymode = "",
                                 MinPay = s.Minamount,
                                 MaxPay = s.Maxamount
                             }).ToList();
            return Json(new { Purchases = purchases }, JsonRequestBehavior.AllowGet);
        }

        public string GetExtremeLeftRight(string referrerName, string position)
        {
            string searchname = referrerName;
            string searchpos = position;
            var res = db.Members.Where(x => x.Username.ToUpper().Trim() == referrerName.ToUpper().Trim()).Single();
            long MemberId = res.RegistrationId;
            Boolean keepgoing = true;
            do
            {
                var res1 = db.Members.SingleOrDefault(m => m.UplineRegistrationId == (long?)MemberId && m.BinaryPosition == position);
                if (res1 != null)
                {
                    MemberId = res1.RegistrationId;
                    searchname = res1.Username;
                }
                else
                {
                    keepgoing = false;
                }

            } while (keepgoing);

            return searchname;
        }

        public int GetnewLevel(long uplineId)
        {
            var res = db.Members.Where(x => x.RegistrationId == uplineId).Single();
            int MemberLevel = (int)res.Level + 1;

            return MemberLevel;
        }

        public async Task<bool> BinaryIncomeCalculation(long RegistrationId, DateTime JoiningDate, double Amount, string Position, bool newJoining, long PurchaseRegistrationId, int packageId, string GuidString)
        {
            BinaryIncome newBusiness = new BinaryIncome();
            newBusiness.RegistrationId = RegistrationId;
            WeekModel wk = new WeekModel();
            wk = GetCurrentWeek(JoiningDate, DateTime.Now);
            newBusiness.WeekNo = wk.WeekNo;
            WeekModel wkc = new WeekModel();
            wkc = GetCurrentWeekBoundary(DateTime.Now);
            newBusiness.WeekStartDate = wkc.WeekStartDate;
            newBusiness.WeekEndDate = wkc.WeekEndDate;
            if (newJoining)
            {
                if (Position == "L") { newBusiness.LeftNewJoining = 1; newBusiness.RightNewJoining = 0; }
                if (Position == "R") { newBusiness.LeftNewJoining = 0; newBusiness.RightNewJoining = 1; }
            }
            else
            {
                newBusiness.LeftNewJoining = 0;
                newBusiness.RightNewJoining = 0;
            }
            if (Position == "L") { newBusiness.LeftNewBusinessCount = 1; newBusiness.RightNewBusinessCount = 0; }
            if (Position == "R") { newBusiness.LeftNewBusinessCount = 0; newBusiness.RightNewBusinessCount = 1; }

            if (Position == "L") { newBusiness.LeftNewBusinessAmount = Amount; newBusiness.RightNewBusinessAmount = 0; }
            if (Position == "R") { newBusiness.LeftNewBusinessAmount = 0; newBusiness.RightNewBusinessAmount = Amount; }

            newBusiness.TransactionDate = DateTime.Now;

            newBusiness.PurchaserRegistrationId = PurchaseRegistrationId;

            newBusiness.PackageId = packageId;

            double BinaryIncome = ((newBusiness.LeftNewBusinessAmount + newBusiness.RightNewBusinessAmount) * 10) / 100;


            newBusiness.BatchNo = GuidString;

            db.BinaryIncomes.Add(newBusiness);
            await db.SaveChangesAsync();

            return true;
        }

        public async Task<bool> SponsorIncomeCalculation(long RegistrationId, DateTime JoiningDate, double Amount, string Position, bool newJoining, long PurchaseRegistrationId, int packageId, string GuidString)
        {
            SponsorIncome newBusiness = new SponsorIncome();
            newBusiness.RegistrationId = RegistrationId;
            if (newJoining)
            {
                if (Position == "L") { newBusiness.LeftNewJoining = 1; newBusiness.RightNewJoining = 0; }
                if (Position == "R") { newBusiness.LeftNewJoining = 0; newBusiness.RightNewJoining = 1; }
            }
            else
            {
                newBusiness.LeftNewJoining = 0;
                newBusiness.RightNewJoining = 0;
            }
            if (Position == "L") { newBusiness.LeftNewBusinessCount = 1; newBusiness.RightNewBusinessCount = 0; }
            if (Position == "R") { newBusiness.LeftNewBusinessCount = 0; newBusiness.RightNewBusinessCount = 1; }

            if (Position == "L") { newBusiness.LeftNewBusinessAmount = Amount; newBusiness.RightNewBusinessAmount = 0; }
            if (Position == "R") { newBusiness.LeftNewBusinessAmount = 0; newBusiness.RightNewBusinessAmount = Amount; }

            newBusiness.TransactionDate = DateTime.Now;

            newBusiness.PurchaserRegistrationId = PurchaseRegistrationId;

            newBusiness.PackageId = packageId;

            newBusiness.IncomeAmount = (Amount * 7) / 100;
            newBusiness.WeekStartDate = DateTime.Now;
            newBusiness.WeekEndDate = DateTime.Now;
            newBusiness.BatchNo = GuidString;

            db.SponsorIncomes.Add(newBusiness);
            try
            {
                await db.SaveChangesAsync();

            }
            catch (Exception e)
            {

            }

            return true;
        }

        public WeekModel GetCurrentWeek(DateTime JoiningDate, DateTime Today)
        {
            int weekno = 0;
            DateTime jd = JoiningDate.AddDays(-1);
            Boolean keepdoing = true;

            do
            {
                if (Today >= jd && Today <= jd.AddDays(7))
                {
                    keepdoing = false;
                }
                jd = jd.AddDays(7);
                weekno = weekno + 1;
            } while (keepdoing);

            WeekModel wkmodel = new WeekModel();
            wkmodel.WeekNo = weekno;
            wkmodel.WeekStartDate = jd.AddDays(-6);
            wkmodel.WeekEndDate = jd;
            return wkmodel;
        }

        public WeekModel GetBoundaryWeek(DateTime JoiningDate, int WeekNo)
        {
            int weekno = 0;
            DateTime jd = JoiningDate.AddDays(-1);

            do
            {
                jd = jd.AddDays(7);
                weekno = weekno + 1;
            } while (weekno < WeekNo);

            WeekModel wkmodel = new WeekModel();
            wkmodel.WeekNo = weekno;
            wkmodel.WeekStartDate = jd.AddDays(-6);
            wkmodel.WeekEndDate = jd;

            return wkmodel;
        }

        public JsonResult MemberTransferHistory()
        {
            string username = User.Identity.Name;
            var rec = db.Registrations.SingleOrDefault(r => r.UserName.ToUpper().Trim() == username.ToUpper().Trim());
            var transfers = (from t in db.Ledgers
                             from r in db.Registrations
                             where t.ToFromUser == r.Id && t.RegistrationId == rec.Id && t.WalletId == 5
                             orderby t.Id ascending
                             select new MemberTransferVM
                             {
                                 Id = t.Id,
                                 DateD = t.Date,
                                 Deposit = t.Deposit,
                                 Withdraw = t.Withdraw,
                                 Transfer = r.UserName,
                                 Balance = 0,
                                 Comment=t.Comment
                             }).ToList();

            for (int i = 0; i < transfers.Count(); i++)
            {
                transfers[i].Date = transfers[i].DateD.ToLongDateString();
            }

            return Json(new { Transfers = transfers }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult MemberSelfTransferHistory()
        {
            string username = User.Identity.Name;
            var rec = db.Registrations.SingleOrDefault(r => r.UserName.ToUpper().Trim() == username.ToUpper().Trim());
            var transfers = (from t in db.Ledgers
                             from w in db.Wallets
                             where t.WalletId==w.Id && t.ToFromUser == rec.Id && t.RegistrationId == rec.Id && (t.WalletId == 5 || t.WalletId == 6)
                             orderby t.Id ascending
                             select new MemberTransferVM
                             {
                                 Id = t.Id,
                                 DateD = t.Date,
                                 Walletname= w.WalletName,
                                 Deposit = t.Deposit,
                                 Withdraw = t.Withdraw,
                                 Balance = 0,
                                 Comment=t.Comment
                             }).ToList();

            for (int i = 0; i < transfers.Count(); i++)
            {
                transfers[i].Date = transfers[i].DateD.ToLongDateString();
            }

            return Json(new { Transfers = transfers }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetMyCurrentSponsorIncome()
        {
            string username = User.Identity.Name;
            var rec = db.Members.SingleOrDefault(r => r.Username.ToUpper().Trim() == username.ToUpper().Trim());
            var weekly = GetCurrentWeekBoundary(DateTime.Now);

            var SpIncome = (from si in db.SponsorIncomes
                            from pkg in db.Packages
                            from mem in db.Members
                            where si.PackageId == pkg.Id && si.RegistrationId == rec.RegistrationId
                            //&& si.WeekStartDate >= weekly.WeekStartDate && si.WeekEndDate <= weekly.WeekEndDate
                            && mem.RegistrationId == si.PurchaserRegistrationId
                            orderby si.Id
                            select new BinaryIncomeLedgerVM
                            {
                                Id = si.Id,
                                WeekNo = si.WeekNo,
                                WeekStartDate = si.WeekStartDate,
                                WeekEndDate = si.WeekEndDate,
                                Date = si.TransactionDate,
                                Purchaser = mem.Username,
                                Package = pkg.Name,
                                LeftSideAmount = si.LeftNewBusinessAmount,
                                RightSideAmount = si.RightNewBusinessAmount,
                                WalletAmount = si.IncomeAmount
                            }).ToList();

            double currleftamt = 0;
            double currrightamt = 0;
            for (int i = 0; i < SpIncome.Count(); i++)
            {
                SpIncome[i].sWeekStartDate = SpIncome[i].WeekStartDate.ToLongDateString();
                SpIncome[i].sWeekEndDate = SpIncome[i].WeekEndDate.ToLongDateString();
                SpIncome[i].sDate = SpIncome[i].Date.ToLongDateString();
                currleftamt = currleftamt + SpIncome[i].LeftSideAmount;
                currrightamt = currrightamt + SpIncome[i].RightSideAmount;
            }


            return Json(new { SpIincomeArray = SpIncome }, JsonRequestBehavior.AllowGet);
        }

        public WeekModel GetCurrentWeekBoundary(DateTime date)
        {
            WeekModel wk = new WeekModel();
            wk.WeekNo = 0;
            int dayOfToday = (int)date.DayOfWeek;
            if (dayOfToday == 0) { dayOfToday = 7; }
            wk.WeekStartDate = date.AddDays(-1 * (dayOfToday - 1));
            wk.WeekEndDate = date.AddDays(7 - dayOfToday);
            return wk;
        }

        public JsonResult GetCurrentWeekBoundaryTest(DateTime date)
        {
            WeekModel wk = new WeekModel();
            wk.WeekNo = 0;
            int dayOfToday = (int)date.DayOfWeek;
            if (dayOfToday == 0) { dayOfToday = 7; }
            wk.WeekStartDate = date.AddDays(-1 * (dayOfToday - 1));
            wk.WeekEndDate = date.AddDays(7 - dayOfToday);
            return Json(new { WeekBoundary = wk }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Dashboarddata()
        {
            DashboardModel dashdata = new DashboardModel();

            string username = User.Identity.Name;
            var rec = db.Members.SingleOrDefault(r => r.Username.ToUpper().Trim() == username.ToUpper().Trim());

            dashdata.TotalTeamMembers = (long)rec.Totalmembers;

            double totalbusinessL = db.BinaryIncomes.Where(s => s.RegistrationId == rec.RegistrationId).Select(n => n.LeftNewBusinessAmount).DefaultIfEmpty(0).Sum();
            double totalbusinessR = db.BinaryIncomes.Where(s => s.RegistrationId == rec.RegistrationId).Select(n => n.RightNewBusinessAmount).DefaultIfEmpty(0).Sum();
            double totalbusinessSum = totalbusinessL + totalbusinessR;
            double totalselfbusiness = db.Purchases.Where(p => p.RegistrationId == rec.RegistrationId).Select(n => n.Amount).DefaultIfEmpty(0).Sum();
            dashdata.TotalTeamBusiness = totalbusinessSum + totalselfbusiness;

            dashdata.InitialInvestment = db.Purchases.Where(p => p.RegistrationId == rec.RegistrationId && p.Packageid != 0).Select(n => n.Amount).DefaultIfEmpty(0).Sum();

            dashdata.TotalRepurchase = db.Purchases.Where(p => p.RegistrationId == rec.RegistrationId && p.Packageid == 0).Select(n => n.Amount).DefaultIfEmpty(0).Sum();

            int plutocount = db.BinaryIncomes.Where(b => b.RegistrationId == rec.RegistrationId && b.PackageId == 1).Select(n => n.PackageId).DefaultIfEmpty(0).Count();
            int jupitercount = db.BinaryIncomes.Where(b => b.RegistrationId == rec.RegistrationId && b.PackageId == 2).Select(n => n.PackageId).DefaultIfEmpty(0).Count();
            int earthcount = db.BinaryIncomes.Where(b => b.RegistrationId == rec.RegistrationId && b.PackageId == 3).Select(n => n.PackageId).DefaultIfEmpty(0).Count();
            int mercurycount = db.BinaryIncomes.Where(b => b.RegistrationId == rec.RegistrationId && b.PackageId == 4).Select(n => n.PackageId).DefaultIfEmpty(0).Count();
            int totalcount = plutocount + jupitercount + earthcount + mercurycount;

            dashdata.PlutoPurchasePc = ((plutocount * 100) / totalcount);
            dashdata.JupiterPurchasePc = ((jupitercount * 100) / totalcount);
            dashdata.EarthPurchasePc = ((earthcount * 100) / totalcount);
            dashdata.MercuryPurchasePc = ((mercurycount * 100) / totalcount);
            dashdata.TotalPkgPurchaseCount = totalcount;

            dashdata.MyLeftCount = (long)rec.Leftmembers;
            dashdata.MyRightCount = (long)rec.Rightmembers;

            double amt1 = (double)(db.BinaryIncomes.Where(b => b.RegistrationId == rec.RegistrationId)).Select(b => b.CashWallet).DefaultIfEmpty(0).Sum();
            double amt2 = (double)(db.SponsorIncomes.Where(b => b.RegistrationId == rec.RegistrationId)).Select(b => b.CashWallet).DefaultIfEmpty(0).Sum();
            double amt3 = (double)(db.GenerationIncomes.Where(b => b.RegistrationId == rec.RegistrationId)).Select(b => b.CashWallet).DefaultIfEmpty(0).Sum();
            dashdata.CashWalletBalance = amt1 + amt2 + amt3;
            dashdata.CashWalletBalance = 0;

            double amt4 = (double)(db.BinaryIncomes.Where(b => b.RegistrationId == rec.RegistrationId)).Select(b => b.ReserveWallet).DefaultIfEmpty(0).Sum();
            double amt5 = (double)(db.SponsorIncomes.Where(b => b.RegistrationId == rec.RegistrationId)).Select(b => b.ReserveWallet).DefaultIfEmpty(0).Sum();
            double amt6 = (double)(db.GenerationIncomes.Where(b => b.RegistrationId == rec.RegistrationId)).Select(b => b.ReserveWallet).DefaultIfEmpty(0).Sum();
            dashdata.ReserveWalletBalance = amt4 + amt5 + amt6;
            dashdata.ReserveWalletBalance = 0;

            dashdata.ReturnWalletBalance = (double)db.WeeklyIncomes.Where(w => w.RegistrationId == rec.RegistrationId && w.WeekEndDate <= DateTime.Now).Select(b => b.FixedIncomeWallet).DefaultIfEmpty(0).Sum();
            dashdata.FrozenWalletBalance = 0;

            return Json(new { DashboardDataModel = dashdata }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult MyCashIncomeWallet()
        {
            string username = User.Identity.Name;
            var rec = db.Members.SingleOrDefault(r => r.Username.ToUpper().Trim() == username.ToUpper().Trim());
            WeekModel wm = GetCurrentWeekBoundary(DateTime.Now);

            var FIarray0 = (from wk in db.BinaryIncomes
                            from pkg in db.Packages
                            from reg in db.Registrations
                            where wk.PackageId == pkg.Id &&
                            wk.PurchaserRegistrationId == reg.Id &&
                            wk.RegistrationId == rec.RegistrationId
                            && wk.TransactionDate >= wm.WeekStartDate && wk.TransactionDate <= wm.WeekEndDate
                            orderby wk.TransactionDate descending

                            select new BinaryIncomeLedgerVM
                            {
                                WeekStartDate = wk.WeekStartDate,
                                WeekEndDate = wk.WeekEndDate,
                                Date = wk.TransactionDate,
                                Purchaser = reg.UserName,
                                Package = pkg.Name,
                                Total = wk.LeftNewBusinessAmount + wk.RightNewBusinessAmount,
                                WalletAmount = (double)wk.CashWallet,
                                PaymentStatus = "",
                                PaymentDate = wk.PaidDate
                            }).ToList();

            for (int i = 0; i < FIarray0.Count(); i++)
            {
                FIarray0[i].sWeekStartDate = FIarray0[i].WeekStartDate.ToLongDateString();
                FIarray0[i].sWeekEndDate = FIarray0[i].WeekEndDate.ToLongDateString();
                FIarray0[i].sDate = FIarray0[i].Date.ToLongDateString();
                if (FIarray0[i].PaymentDate == null) { FIarray0[i].sPaymentDate = ""; }
                if (FIarray0[i].PaymentDate != null)
                {
                    DateTime ddt = (DateTime)FIarray0[i].PaymentDate;
                    FIarray0[i].sPaymentDate = ddt.ToLongDateString();
                }
                if (FIarray0[i].PaymentDate == null) { FIarray0[i].PaymentStatus = ""; }
                if (FIarray0[i].PaymentDate != null) { FIarray0[i].PaymentStatus = "Paid"; }
            }

            //-------------------------------------------------------------------------------------------

            var FIarray1 = (from wk in db.SponsorIncomes
                            from pkg in db.Packages
                            from reg in db.Registrations
                            where wk.PackageId == pkg.Id &&
                            wk.PurchaserRegistrationId == reg.Id &&
                            wk.RegistrationId == rec.RegistrationId
                            && wk.TransactionDate >= wm.WeekStartDate && wk.TransactionDate <= wm.WeekEndDate
                            orderby wk.TransactionDate descending

                            select new BinaryIncomeLedgerVM
                            {
                                WeekStartDate = wk.WeekStartDate,
                                WeekEndDate = wk.WeekEndDate,
                                Date = wk.TransactionDate,
                                Purchaser = reg.UserName,
                                Package = pkg.Name,
                                Total = wk.LeftNewBusinessAmount + wk.RightNewBusinessAmount,
                                WalletAmount = (double)wk.CashWallet,
                                PaymentStatus = "",
                                PaymentDate = wk.PaidDate
                            }).ToList();

            for (int i = 0; i < FIarray1.Count(); i++)
            {
                FIarray1[i].sWeekStartDate = FIarray1[i].WeekStartDate.ToLongDateString();
                FIarray1[i].sWeekEndDate = FIarray1[i].WeekEndDate.ToLongDateString();
                FIarray1[i].sDate = FIarray1[i].Date.ToLongDateString();
                if (FIarray1[i].PaymentDate == null) { FIarray1[i].sPaymentDate = ""; }
                if (FIarray1[i].PaymentDate != null)
                {
                    DateTime ddt = (DateTime)FIarray1[i].PaymentDate;
                    FIarray1[i].sPaymentDate = ddt.ToLongDateString();
                }
                if (FIarray1[i].PaymentDate == null) { FIarray1[i].PaymentStatus = ""; }
                if (FIarray1[i].PaymentDate != null) { FIarray1[i].PaymentStatus = "Paid"; }
            }

            //----------------------------------------------------------------------------------------

            var FIarray2 = (from wk in db.GenerationIncomes
                            from pkg in db.Packages
                            from reg in db.Registrations
                            where wk.PackageId == pkg.Id &&
                            wk.PurchaserRegistrationId == reg.Id &&
                            wk.RegistrationId == rec.RegistrationId
                            && wk.TransactionDate >= wm.WeekStartDate && wk.TransactionDate <= wm.WeekEndDate
                            orderby wk.TransactionDate descending

                            select new BinaryIncomeLedgerVM
                            {
                                WeekStartDate = wk.WeekStartDate,
                                WeekEndDate = wk.WeekEndDate,
                                Date = wk.TransactionDate,
                                Purchaser = reg.UserName,
                                Package = pkg.Name,
                                Total = wk.LeftNewBusinessAmount + wk.RightNewBusinessAmount,
                                WalletAmount = (double)wk.CashWallet,
                                PaymentStatus = "",
                                PaymentDate = wk.PaidDate
                            }).ToList();

            for (int i = 0; i < FIarray2.Count(); i++)
            {
                FIarray2[i].sWeekStartDate = FIarray2[i].WeekStartDate.ToLongDateString();
                FIarray2[i].sWeekEndDate = FIarray2[i].WeekEndDate.ToLongDateString();
                FIarray2[i].sDate = FIarray2[i].Date.ToLongDateString();
                if (FIarray2[i].PaymentDate == null) { FIarray2[i].sPaymentDate = ""; }
                if (FIarray2[i].PaymentDate != null)
                {
                    DateTime ddt = (DateTime)FIarray2[i].PaymentDate;
                    FIarray2[i].sPaymentDate = ddt.ToLongDateString();
                }
                if (FIarray2[i].PaymentDate == null) { FIarray2[i].PaymentStatus = ""; }
                if (FIarray2[i].PaymentDate != null) { FIarray2[i].PaymentStatus = "Paid"; }
            }

            return Json(new { BinaryIncomeArray = FIarray0, SponsorIncomeArray = FIarray1, GenerationIncomeArray = FIarray2 }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult MyReserveIncomeWallet()
        {
            string username = User.Identity.Name;
            var rec = db.Members.SingleOrDefault(r => r.Username.ToUpper().Trim() == username.ToUpper().Trim());
            WeekModel wm = GetCurrentWeekBoundary(DateTime.Now);

            var FIarray0 = (from wk in db.BinaryIncomes
                            from pkg in db.Packages
                            from reg in db.Registrations
                            where wk.PackageId == pkg.Id &&
                            wk.PurchaserRegistrationId == reg.Id &&
                            wk.RegistrationId == rec.RegistrationId
                            && wk.TransactionDate >= wm.WeekStartDate && wk.TransactionDate <= wm.WeekEndDate
                            orderby wk.TransactionDate descending

                            select new BinaryIncomeLedgerVM
                            {
                                WeekStartDate = wk.WeekStartDate,
                                WeekEndDate = wk.WeekEndDate,
                                Date = wk.TransactionDate,
                                Purchaser = reg.UserName,
                                Package = pkg.Name,
                                Total = wk.LeftNewBusinessAmount + wk.RightNewBusinessAmount,
                                WalletAmount = (double)wk.ReserveWallet,
                                PaymentStatus = "",
                                PaymentDate = wk.PaidDate
                            }).ToList();

            for (int i = 0; i < FIarray0.Count(); i++)
            {
                FIarray0[i].sWeekStartDate = FIarray0[i].WeekStartDate.ToLongDateString();
                FIarray0[i].sWeekEndDate = FIarray0[i].WeekEndDate.ToLongDateString();
                FIarray0[i].sDate = FIarray0[i].Date.ToLongDateString();
                if (FIarray0[i].PaymentDate == null) { FIarray0[i].sPaymentDate = ""; }
                if (FIarray0[i].PaymentDate != null)
                {
                    DateTime ddt = (DateTime)FIarray0[i].PaymentDate;
                    FIarray0[i].sPaymentDate = ddt.ToLongDateString();
                }
                if (FIarray0[i].PaymentDate == null) { FIarray0[i].PaymentStatus = ""; }
                if (FIarray0[i].PaymentDate != null) { FIarray0[i].PaymentStatus = "Paid"; }
            }

            //-------------------------------------------------------------------------------------------

            var FIarray1 = (from wk in db.SponsorIncomes
                            from pkg in db.Packages
                            from reg in db.Registrations
                            where wk.PackageId == pkg.Id &&
                            wk.PurchaserRegistrationId == reg.Id &&
                            wk.RegistrationId == rec.RegistrationId
                            && wk.TransactionDate >= wm.WeekStartDate && wk.TransactionDate <= wm.WeekEndDate
                            orderby wk.TransactionDate descending

                            select new BinaryIncomeLedgerVM
                            {
                                WeekStartDate = wk.WeekStartDate,
                                WeekEndDate = wk.WeekEndDate,
                                Date = wk.TransactionDate,
                                Purchaser = reg.UserName,
                                Package = pkg.Name,
                                Total = wk.LeftNewBusinessAmount + wk.RightNewBusinessAmount,
                                WalletAmount = (double)wk.ReserveWallet,
                                PaymentStatus = "",
                                PaymentDate = wk.PaidDate
                            }).ToList();

            for (int i = 0; i < FIarray1.Count(); i++)
            {
                FIarray1[i].sWeekStartDate = FIarray1[i].WeekStartDate.ToLongDateString();
                FIarray1[i].sWeekEndDate = FIarray1[i].WeekEndDate.ToLongDateString();
                FIarray1[i].sDate = FIarray1[i].Date.ToLongDateString();
                if (FIarray1[i].PaymentDate == null) { FIarray1[i].sPaymentDate = ""; }
                if (FIarray1[i].PaymentDate != null)
                {
                    DateTime ddt = (DateTime)FIarray1[i].PaymentDate;
                    FIarray1[i].sPaymentDate = ddt.ToLongDateString();
                }
                if (FIarray1[i].PaymentDate == null) { FIarray1[i].PaymentStatus = ""; }
                if (FIarray1[i].PaymentDate != null) { FIarray1[i].PaymentStatus = "Paid"; }
            }

            //----------------------------------------------------------------------------------------

            var FIarray2 = (from wk in db.GenerationIncomes
                            from pkg in db.Packages
                            from reg in db.Registrations
                            where wk.PackageId == pkg.Id &&
                            wk.PurchaserRegistrationId == reg.Id &&
                            wk.RegistrationId == rec.RegistrationId
                            && wk.TransactionDate >= wm.WeekStartDate && wk.TransactionDate <= wm.WeekEndDate
                            orderby wk.TransactionDate descending

                            select new BinaryIncomeLedgerVM
                            {
                                WeekStartDate = wk.WeekStartDate,
                                WeekEndDate = wk.WeekEndDate,
                                Date = wk.TransactionDate,
                                Purchaser = reg.UserName,
                                Package = pkg.Name,
                                Total = wk.LeftNewBusinessAmount + wk.RightNewBusinessAmount,
                                WalletAmount = (double)wk.ReserveWallet,
                                PaymentStatus = "",
                                PaymentDate = wk.PaidDate
                            }).ToList();

            for (int i = 0; i < FIarray2.Count(); i++)
            {
                FIarray2[i].sWeekStartDate = FIarray2[i].WeekStartDate.ToLongDateString();
                FIarray2[i].sWeekEndDate = FIarray2[i].WeekEndDate.ToLongDateString();
                FIarray2[i].sDate = FIarray2[i].Date.ToLongDateString();
                if (FIarray2[i].PaymentDate == null) { FIarray2[i].sPaymentDate = ""; }
                if (FIarray2[i].PaymentDate != null)
                {
                    DateTime ddt = (DateTime)FIarray2[i].PaymentDate;
                    FIarray2[i].sPaymentDate = ddt.ToLongDateString();
                }
                if (FIarray2[i].PaymentDate == null) { FIarray2[i].PaymentStatus = ""; }
                if (FIarray2[i].PaymentDate != null) { FIarray2[i].PaymentStatus = "Paid"; }
            }

            return Json(new { BinaryIncomeArray = FIarray0, SponsorIncomeArray = FIarray1, GenerationIncomeArray = FIarray2 }, JsonRequestBehavior.AllowGet);
        }

        public string PackageImage(int packageId)
        {
            string path = "";
            if (packageId == 1) { path = "http://www.jpnpl.com/JpnplLibrary/Assets/avatar1.jpg"; }
            if (packageId == 2) { path = "http://www.jpnpl.com/JpnplLibrary/Assets/royalty_avatar.png"; }
            if (packageId == 3) { path = "http://www.jpnpl.com/JpnplLibrary/Assets/diamond.png"; }
            if (packageId == 4) { path = "http://www.jpnpl.com/JpnplLibrary/Assets/avatar1.jpg"; }
            if (packageId == 99) { path = "http://www.jpnpl.com/JpnplLibrary/Assets/plus-sign.png"; }
            return path;
        }

        public JsonResult MemberDetail()
        {
            string username = User.Identity.Name;
            var mem = db.Members.SingleOrDefault(m => m.Username == username);
            var sDate = mem.Doj.ToLongDateString();
            return Json(new { Member = mem, Date = sDate }, JsonRequestBehavior.AllowGet);
        }

        private async Task<bool> SendEMail(string mail_to, string mail_cc, string subj, string desc, ArrayList arr)
        {
            string mail_from = "noreply@jpnpl.com";
            //string mail_cc = "";
            System.Collections.Hashtable ht = new System.Collections.Hashtable();
            ht.Add("FROM", mail_from);
            ht.Add("TO", mail_to);
            ht.Add("CC", mail_cc);
            //ht.Add("BCC", mail_cc);
            ht.Add("SUBJECT", subj);
            ht.Add("BODY", desc);
            ht.Add("ATTACHMENT", arr);

            try
            {
                Email mail = new Email(ht);
                mail.SendEMail();
                return true;
            }
            catch
            {
                return false;
            }
        }

        //string mailfrom, string mail_to, string mail_cc, string subj, string desc
        [HttpPost]
        public async Task<bool> SendContactUsEMail(string fullname, string phone, string country, string mailfrom, string mail_to, string mail_cc, string subj, string desc)
        {
            string bodytext = "<table><tr><td>Full name</td><td>" + fullname + "</td></tr><tr><td>Phone No.</td><td>" + phone + "</td></tr><tr><td>Email</td><td>" + mailfrom + "</td></tr><tr><td>Country</td><td>" + country + "</td></tr><tr><td>Message</td><td>" + desc + "</td></tr></table>";
            string mail_from = mailfrom;
            mail_to = "info@jpnpl.com";
            ArrayList arr = null;
            //string mail_cc = "";
            System.Collections.Hashtable ht = new System.Collections.Hashtable();
            ht.Add("FROM", mail_from);
            ht.Add("TO", mail_to);
            ht.Add("CC", mail_cc);
            //ht.Add("BCC", mail_cc);
            ht.Add("SUBJECT", subj);
            ht.Add("BODY", bodytext);
            ht.Add("ATTACHMENT", arr);

            try
            {
                Email mail = new Email(ht);
                mail.SendEMail();
                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpPost]
        public async Task<JsonResult> SendMyRegistrationEmail(string Username)
        {
            var reg = db.Registrations.SingleOrDefault(r => r.UserName == Username);

            bool result = false;
            ArrayList array = new ArrayList();
            string body = "";
            body = @"<table style='width:100%'>
                            <tr>
                             <td colspan='2'>
                               <p style='text-align:left;font-size:14px;font-color:black;font-weight:600'> Greetings from JPN Group!!!</p></td>
                           </tr> 
                           <tr>
                              <td colspan='2'>
                                <p style='text-align:justified;font-size:12px;'> Thanks for your interest in www.jpnpl.com. We have received your new application for 1 share. We wish you a prosperous association with us. The following are your credentials to get you started !!! </p>
                              </td>
                           </tr>
                           <tr>
                             <td style='text-align:right;font-weight:600;width:20%'>
                                Date of Joining:
                             </td>
                             <td>" + reg.CreatedDate.ToLongDateString() + "</td>" +
                           "</tr>" +
                           "<tr>" +
                             "<td style='text-align:right;font-weight:600'>Username:" +
                              "</td>" +
                                "<td>" + reg.UserName + "</td></tr><tr><td style='text-align:right;font-weight:600'>Password:</td><td> " + reg.Password + " </td> " +
                                "</tr>" +
                                 "<tr><td style='text-align:right;font-weight:600'>Transaction Password:</td><td> " + reg.TrxPassword + " </td> " +
                                "</tr><tr><td colspan='2' style='padding-top:15px'>All the best !!!</td></tr><tr><td colspan='2'>JPN Team</td></tr>" +
                                "</table>";
            result = await SendEMail(reg.EmailId, "", "Registration Confirmation", body, array);
            return Json(new { status = "Success" }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult TransactionPasswordExists()
        {
            var user = User.Identity.Name;
            var res = db.Members.Where(i => i.Username == user).Select(i => i.Transactionpassword).ToList();
            Boolean isExists = false;
            if (res[0] != "") { isExists = true; }
            return Json(new { isEXISTS = isExists }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult TransactionPasswordMatch(string OldPassword)
        {
            var user = User.Identity.Name;
            var res = db.Members.Where(i => i.Username == user).Select(i => i.Transactionpassword).ToList();
            Boolean isMatching = false;
            if (res[0] == OldPassword) { isMatching = true; }
            return Json(new { isOK = isMatching }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult PasswordMatch(string OldPassword)
        {
            var user = User.Identity.Name;
            var res = db.Registrations.Where(i => i.UserName == user).Select(i => i.Password).ToList();
            Boolean isMatching = false;
            if (res[0] == OldPassword) { isMatching = true; }
            return Json(new { isOK = isMatching }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ResetPasswordInRegistration(string OldPassword)
        {
            var user = User.Identity.Name;
            var res = db.Registrations.Where(i => i.UserName == user).Single();
            res.Password = OldPassword;
            db.SaveChanges();
            Boolean isMatching = true;
            return Json(new { isOK = isMatching }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RegistrationDetail()
        {
            string username = User.Identity.Name;
            var mem = db.Registrations.SingleOrDefault(m => m.UserName == username);

            return Json(new { Member = mem }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AdminTransferLedger(string Username)
        {
            var admin = db.Registrations.SingleOrDefault(m => m.UserName.ToUpper() == "SUPERADMIN");
            if (Username != "")
            {
                var mem = db.Registrations.SingleOrDefault(m => m.UserName.ToUpper() == Username);

                var transfers = (from t in db.Ledgers
                                 from r in db.Registrations
                                 from w in db.Wallets
                                 where t.ToFromUser == admin.Id && t.RegistrationId == mem.Id && t.RegistrationId == r.Id
                                 && t.WalletId == w.Id
                                 orderby t.Id descending
                                 select new MemberTransferVM
                                 {
                                     Id = t.Id,
                                     DateD = t.Date,
                                     Deposit = t.Deposit,
                                     Withdraw = t.Withdraw,
                                     Transfer = r.UserName,
                                     Walletname = w.WalletName,
                                     Balance = 0,
                                 }).ToList();

                for (int i = 0; i < transfers.Count(); i++)
                {
                    transfers[i].Date = transfers[i].DateD.ToLongDateString();
                }
                //var ledger = db.Ledgers.Where(m => m.RegistrationId == mem.Id && m.ToFromUser == admin.Id).OrderByDescending(m => m.Date);
                return Json(new { Ledger = transfers }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var transfers = (from t in db.Ledgers
                                 from r in db.Registrations
                                 from w in db.Wallets
                                 where t.ToFromUser == admin.Id && t.WalletId == w.Id && t.RegistrationId == r.Id
                                 orderby t.Id descending
                                 select new MemberTransferVM
                                 {
                                     Id = t.Id,
                                     DateD = t.Date,
                                     Deposit = t.Deposit,
                                     Withdraw = t.Withdraw,
                                     Transfer = r.UserName,
                                     Walletname = w.WalletName,
                                     Balance = 0,
                                 }).ToList();

                for (int i = 0; i < transfers.Count(); i++)
                {
                    transfers[i].Date = transfers[i].DateD.ToLongDateString();
                }

                //var ledger = db.Ledgers.Where(m => m.ToFromUser == admin.Id).OrderBy(m => m.Date);
                return Json(new { Ledger = transfers }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult UpdateMember(long id, Member member)
        {
            try
            {
                Member mem = db.Members.SingleOrDefault(m => m.Id == id);
                mem.Firstname = member.Firstname;
                mem.Addressline1 = member.Addressline1;
                mem.Addressline2 = member.Addressline2;
                mem.City = member.City;
                mem.Postcode = member.Postcode;
                mem.State = member.State;
                mem.Country = member.Country;
                mem.Mobileno = member.Mobileno;
                mem.Secretquestion = member.Secretquestion;
                mem.Secretpassword = member.Secretpassword;
                mem.Transactionpassword = member.Transactionpassword;

                db.SaveChanges();

                return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { Success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetCountry(string countryCode)
        {
            string Countryname = db.Countries.Where(c => c.CountryId == countryCode).Select(c => c.CountryName).Single();
            if (Countryname == "" || Countryname == null)
            {
                Countryname = "hidden";
            }

            return Json(new { CountryName = Countryname }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetMyCurrentBinaryIncome()
        {
            string username = User.Identity.Name;
            var rec = db.Members.SingleOrDefault(r => r.Username.ToUpper().Trim() == username.ToUpper().Trim());

            var BIncome = (from bi in db.BinaryIncomes
                           from pkg in db.Packages
                           from mem in db.Members
                           where bi.PackageId == pkg.Id && bi.RegistrationId == rec.RegistrationId
                           //&& bi.WeekStartDate >= weekly.WeekStartDate && bi.WeekEndDate <= weekly.WeekEndDate
                           && mem.RegistrationId == bi.PurchaserRegistrationId && bi.ProcessId == null
                           orderby bi.Id
                           select new BinaryIncomeLedgerVM
                           {
                               Id = bi.Id,
                               Date = bi.TransactionDate,
                               Purchaser = mem.Username,
                               Package = pkg.Name,
                               LeftSideAmount = bi.LeftNewBusinessCount,
                               RightSideAmount = bi.RightNewBusinessCount
                           }).ToList();

            double currleftamt = 0;
            double currrightamt = 0;
            for (int i = 0; i < BIncome.Count(); i++)
            {
                BIncome[i].sDate = BIncome[i].Date.ToLongDateString();
                currleftamt = currleftamt + BIncome[i].LeftSideAmount;
                currrightamt = currrightamt + BIncome[i].RightSideAmount;
            }

            double leftamt = 0;
            double rightamt = 0;
            try
            {
                double? leftamt0 = db.BinaryIncomes.Where(bi => bi.RegistrationId == rec.RegistrationId && bi.TransactionDate == DateTime.Today).Select(bi => bi.LeftNewBusinessCount).DefaultIfEmpty(0).Sum();
                double? rightamt0 = db.BinaryIncomes.Where(bi => bi.RegistrationId == rec.RegistrationId && bi.TransactionDate == DateTime.Today).Select(bi => bi.RightNewBusinessCount).DefaultIfEmpty(0).Sum();
                if (leftamt0 != null) { leftamt = (double)leftamt0; }
                if (rightamt0 != null) { rightamt = (double)leftamt0; }
            }
            catch (Exception ex)
            {

            }


            double opLeftAmt = 0;
            double opRightAmt = 0;
            var opline = db.BinaryOpenings.SingleOrDefault(bi => bi.RegistrationId == rec.RegistrationId && bi.IsCurrent == true);
            if (opline != null)
            {
                opLeftAmt = opline.LeftSideCd;
                opRightAmt = opline.RightSideCd;
            }

            double grandTotalLeft = opLeftAmt + currleftamt;
            double grandTotalRight = opRightAmt + currrightamt;

            double currentbusinessToConsider = 0;
            if (grandTotalLeft >= grandTotalRight) { currentbusinessToConsider = grandTotalRight; }
            if (grandTotalLeft < grandTotalRight) { currentbusinessToConsider = grandTotalLeft; }

            double income = 0;

            if (rec.Defaultpackagecode == 1)
            {
                if (currentbusinessToConsider > 0 && currentbusinessToConsider < 3)
                {
                    income = 0;
                }
                else if (currentbusinessToConsider >= 3 && currentbusinessToConsider < 7)
                {
                    income = 360;
                }
                else if (currentbusinessToConsider >= 7 && currentbusinessToConsider < 15)
                {
                    income = 840;
                }
                else if (currentbusinessToConsider >= 15 && currentbusinessToConsider < 30)
                {
                    income = 1800;
                }
                else if (currentbusinessToConsider >= 30 && currentbusinessToConsider < 70)
                {
                    income = 3600;
                }
                else if (currentbusinessToConsider >= 70 && currentbusinessToConsider < 150)
                {
                    income = 8400;
                }
                else if (currentbusinessToConsider >= 150 && currentbusinessToConsider < 300)
                {
                    income = 18000;
                }
                else if (currentbusinessToConsider >= 300)
                {
                    income = 36000;
                }
                else
                {
                    income = 0;
                }
            }

            double cdLeftAmt = 0;
            double cdRightAmt = 0;
            if (income == 0)
            {
                //carry forward left and right as is when less than 3 pairs
                cdLeftAmt = grandTotalLeft;
                cdRightAmt = grandTotalRight;
            }
            else
            {
                if (grandTotalLeft > grandTotalRight) { cdLeftAmt = grandTotalLeft - grandTotalRight; cdRightAmt = 0; }
                if (grandTotalLeft < grandTotalRight) { cdLeftAmt = 0; cdRightAmt = grandTotalRight - grandTotalLeft; }
                if (grandTotalLeft == grandTotalRight) { cdLeftAmt = 0; cdRightAmt = 0; }
            }

            BinaryIncomeLedgerVMTotals totalArr = new BinaryIncomeLedgerVMTotals();
            totalArr.opLeftAmount = opLeftAmt;
            totalArr.opRightAmount = opRightAmt;
            totalArr.CurrentLeftAmount = currleftamt;
            totalArr.CurrentRightAmount = currrightamt;
            totalArr.TotalLeftAmount = grandTotalLeft;
            totalArr.TotalRightAmount = grandTotalRight;
            totalArr.ConsiderationAmount = currentbusinessToConsider;
            totalArr.IncomeAmount = income;
            totalArr.cdLeftAmount = cdLeftAmt;
            totalArr.cdRightAmount = cdRightAmt;

            return Json(new { BIincomeArray = BIncome, TotalsArray = totalArr }, JsonRequestBehavior.AllowGet);
        }

        //Binary Income Only procedure
        private double GetBinaryIncome(string username)
        {
            var rec = db.Members.SingleOrDefault(r => r.Username.ToUpper().Trim() == username.ToUpper().Trim());
            //var weekly = GetCurrentWeekBoundary(DateTime.Now);

            var BIncome = (from bi in db.BinaryIncomes
                           from pkg in db.Packages
                           from mem in db.Members
                           where bi.PackageId == pkg.Id && bi.RegistrationId == rec.RegistrationId
                           //&& bi.WeekStartDate >= weekly.WeekStartDate && bi.WeekEndDate <= weekly.WeekEndDate
                           && mem.RegistrationId == bi.PurchaserRegistrationId && bi.ProcessId == null
                           orderby bi.Id
                           select new BinaryIncomeLedgerVM
                           {
                               Id = bi.Id,
                               Date = bi.TransactionDate,
                               Purchaser = mem.Username,
                               Package = pkg.Name,
                               LeftSideAmount = bi.LeftNewBusinessCount,
                               RightSideAmount = bi.RightNewBusinessCount
                           }).ToList();

            double currleftamt = 0;
            double currrightamt = 0;
            for (int i = 0; i < BIncome.Count(); i++)
            {
                BIncome[i].sDate = BIncome[i].Date.ToLongDateString();
                currleftamt = currleftamt + BIncome[i].LeftSideAmount;
                currrightamt = currrightamt + BIncome[i].RightSideAmount;
            }

            double leftamt = 0;
            double rightamt = 0;
            try
            {
                double? leftamt0 = db.BinaryIncomes.Where(bi => bi.RegistrationId == rec.RegistrationId && bi.TransactionDate == DateTime.Today).Select(bi => bi.LeftNewBusinessCount).DefaultIfEmpty(0).Sum();
                double? rightamt0 = db.BinaryIncomes.Where(bi => bi.RegistrationId == rec.RegistrationId && bi.TransactionDate == DateTime.Today).Select(bi => bi.RightNewBusinessCount).DefaultIfEmpty(0).Sum();
                if (leftamt0 != null) { leftamt = (double)leftamt0; }
                if (rightamt0 != null) { rightamt = (double)leftamt0; }
            }
            catch (Exception ex)
            {

            }


            double opLeftAmt = 0;
            double opRightAmt = 0;
            var opline = db.BinaryOpenings.SingleOrDefault(bi => bi.RegistrationId == rec.RegistrationId && bi.IsCurrent == true);
            if (opline != null)
            {
                opLeftAmt = opline.LeftSideCd;
                opRightAmt = opline.RightSideCd;
            }

            double grandTotalLeft = opLeftAmt + currleftamt;
            double grandTotalRight = opRightAmt + currrightamt;

            double currentbusinessToConsider = 0;
            if (grandTotalLeft >= grandTotalRight) { currentbusinessToConsider = grandTotalRight; }
            if (grandTotalLeft < grandTotalRight) { currentbusinessToConsider = grandTotalLeft; }

            double income = 0;

            if (rec.Defaultpackagecode == 1)
            {
                if (currentbusinessToConsider > 0 && currentbusinessToConsider < 3)
                {
                    income = 0;
                }
                else if (currentbusinessToConsider >= 3 && currentbusinessToConsider < 7)
                {
                    income = 360;
                }
                else if (currentbusinessToConsider >= 7 && currentbusinessToConsider < 15)
                {
                    income = 840;
                }
                else if (currentbusinessToConsider >= 15 && currentbusinessToConsider < 30)
                {
                    income = 1800;
                }
                else if (currentbusinessToConsider >= 30 && currentbusinessToConsider < 70)
                {
                    income = 3600;
                }
                else if (currentbusinessToConsider >= 70 && currentbusinessToConsider < 150)
                {
                    income = 8400;
                }
                else if (currentbusinessToConsider >= 150 && currentbusinessToConsider < 300)
                {
                    income = 18000;
                }
                else if (currentbusinessToConsider >= 300)
                {
                    income = 36000;
                }
                else
                {
                    income = 0;
                }
            }

            return income;
        }

        //Binary Income Report
        public BinaryIncomeLedgerVMTotals GetCurrentBinaryIncome(string username)
        {
            var rec = db.Members.SingleOrDefault(r => r.Username.ToUpper().Trim() == username.ToUpper().Trim());
            //var weekly = GetCurrentWeekBoundary(DateTime.Now);

            var BIncome = (from bi in db.BinaryIncomes
                           from pkg in db.Packages
                           from mem in db.Members
                           where bi.PackageId == pkg.Id && bi.RegistrationId == rec.RegistrationId
                           //&& bi.WeekStartDate >= weekly.WeekStartDate && bi.WeekEndDate <= weekly.WeekEndDate
                           && mem.RegistrationId == bi.PurchaserRegistrationId && bi.ProcessId == null
                           orderby bi.Id
                           select new BinaryIncomeLedgerVM
                           {
                               Id = bi.Id,
                               Date = bi.TransactionDate,
                               Purchaser = mem.Username,
                               Package = pkg.Name,
                               LeftSideAmount = bi.LeftNewBusinessCount,
                               RightSideAmount = bi.RightNewBusinessCount
                           }).ToList();

            double currleftamt = 0;
            double currrightamt = 0;
            for (int i = 0; i < BIncome.Count(); i++)
            {
                BIncome[i].sDate = BIncome[i].Date.ToLongDateString();
                currleftamt = currleftamt + BIncome[i].LeftSideAmount;
                currrightamt = currrightamt + BIncome[i].RightSideAmount;
            }

            double leftamt = 0;
            double rightamt = 0;
            try
            {
                double? leftamt0 = db.BinaryIncomes.Where(bi => bi.RegistrationId == rec.RegistrationId && bi.TransactionDate == DateTime.Today).Select(bi => bi.LeftNewBusinessCount).DefaultIfEmpty(0).Sum();
                double? rightamt0 = db.BinaryIncomes.Where(bi => bi.RegistrationId == rec.RegistrationId && bi.TransactionDate == DateTime.Today).Select(bi => bi.RightNewBusinessCount).DefaultIfEmpty(0).Sum();
                if (leftamt0 != null) { leftamt = (double)leftamt0; }
                if (rightamt0 != null) { rightamt = (double)leftamt0; }
            }
            catch (Exception ex)
            {

            }


            double opLeftAmt = 0;
            double opRightAmt = 0;
            var opline = db.BinaryOpenings.SingleOrDefault(bi => bi.RegistrationId == rec.RegistrationId && bi.IsCurrent == true);
            if (opline != null)
            {
                opLeftAmt = opline.LeftSideCd;
                opRightAmt = opline.RightSideCd;
            }

            double grandTotalLeft = opLeftAmt + currleftamt;
            double grandTotalRight = opRightAmt + currrightamt;

            double currentbusinessToConsider = 0;
            if (grandTotalLeft >= grandTotalRight) { currentbusinessToConsider = grandTotalRight; }
            if (grandTotalLeft < grandTotalRight) { currentbusinessToConsider = grandTotalLeft; }

            double income = 0;

            if (rec.Defaultpackagecode == 1)
            {
                if (currentbusinessToConsider > 0 && currentbusinessToConsider < 3)
                {
                    income = 0;
                }
                else if (currentbusinessToConsider >= 3 && currentbusinessToConsider < 7)
                {
                    income = 360;
                }
                else if (currentbusinessToConsider >= 7 && currentbusinessToConsider < 15)
                {
                    income = 840;
                }
                else if (currentbusinessToConsider >= 15 && currentbusinessToConsider < 30)
                {
                    income = 1800;
                }
                else if (currentbusinessToConsider >= 30 && currentbusinessToConsider < 70)
                {
                    income = 3600;
                }
                else if (currentbusinessToConsider >= 70 && currentbusinessToConsider < 150)
                {
                    income = 8400;
                }
                else if (currentbusinessToConsider >= 150 && currentbusinessToConsider < 300)
                {
                    income = 18000;
                }
                else if (currentbusinessToConsider >= 300)
                {
                    income = 36000;
                }
                else
                {
                    income = 0;
                }
            }

            double cdLeftAmt = 0;
            double cdRightAmt = 0;

            if (income == 0)
            {
                //carry forward left and right as is when less than 3 pairs
                cdLeftAmt = grandTotalLeft;
                cdRightAmt = grandTotalRight;
            }
            else
            {
                if (grandTotalLeft > grandTotalRight) { cdLeftAmt = grandTotalLeft - grandTotalRight; cdRightAmt = 0; }
                if (grandTotalLeft < grandTotalRight) { cdLeftAmt = 0; cdRightAmt = grandTotalRight - grandTotalLeft; }
                if (grandTotalLeft == grandTotalRight) { cdLeftAmt = 0; cdRightAmt = 0; }
            }


            BinaryIncomeLedgerVMTotals totalArr = new BinaryIncomeLedgerVMTotals();
            totalArr.opLeftAmount = opLeftAmt;
            totalArr.opRightAmount = opRightAmt;
            totalArr.CurrentLeftAmount = currleftamt;
            totalArr.CurrentRightAmount = currrightamt;
            totalArr.TotalLeftAmount = grandTotalLeft;
            totalArr.TotalRightAmount = grandTotalRight;
            totalArr.ConsiderationAmount = currentbusinessToConsider;
            totalArr.IncomeAmount = income;
            totalArr.cdLeftAmount = cdLeftAmt;
            totalArr.cdRightAmount = cdRightAmt;

            return totalArr;
        }
        //Show pre payout information
        public JsonResult PreCalculatePayout()
        {
            //fetch the last process datetime
            List<PayoutProcess> lastinfo = db.PayoutProcesses.ToList();
            DateTime dt = DateTime.Now;
            string sdate = "";
            string stime = "";
            if (lastinfo.Count > 0)
            {
                dt = lastinfo[lastinfo.Count() - 1].Date;
                sdate = dt.ToShortDateString();
                stime = dt.ToShortTimeString();
            }
            string lastProcessDateTime = sdate + " at " + stime;

            //fetch count of new joinings to process
            int newJoinings = db.Members.Where(m => m.ProcessNo == null).Select(m => m.Username).DefaultIfEmpty().Count()-1;

            //fetch count of total members
            int totalJoinings = db.Members.Count();

            //fetch royal club achievers so far
            int royaltyClubMembers = db.Members.Where(m => m.Achievement1 == 1).Select(m => m.Username).DefaultIfEmpty().Count();

            //fetch diamond royalty club achievers so far
            int diamondRoyaltyClubMembers = db.Members.Where(m => m.Achievement2 == 1).Select(m => m.Username).DefaultIfEmpty().Count();

            //total fund for Royalty Club
            double royaltyClubFund = (100 * newJoinings);

            //total fund for Diamond Royalty Club
            double diamondRoyaltyClubFund = (100 * newJoinings);

            //share per Royalty Club member
            double RoyaltyShare = (royaltyClubFund > 0 ? royaltyClubFund / royaltyClubMembers : 0);

            //share per Diamond Club member
            double DiamondRoyaltyShare = (diamondRoyaltyClubFund > 0 ? diamondRoyaltyClubFund / diamondRoyaltyClubMembers : 0);

            PreprocessVM payoutinfo = new PreprocessVM();
            payoutinfo.newJoinings = newJoinings;
            payoutinfo.totalJoinings = totalJoinings;
            payoutinfo.royaltyClubMembers = royaltyClubMembers;
            payoutinfo.diamondRoyaltyClubMembers = diamondRoyaltyClubMembers;
            payoutinfo.royaltyClubFund = royaltyClubFund;
            payoutinfo.diamondRoyaltyClubFund = diamondRoyaltyClubFund;
            payoutinfo.RoyaltyShare = RoyaltyShare;
            payoutinfo.DiamondRoyaltyShare = DiamondRoyaltyShare;
            payoutinfo.lastProcessDateTime = lastProcessDateTime;

            return Json(new { PayoutInfo = payoutinfo }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        //Payout Calculation Process
        public JsonResult CalculatePayout()
        {
            #region preprocess

            //enter an entry in Payout table
            PayoutProcess rec = new PayoutProcess();
            rec.Date = DateTime.Now;
            db.PayoutProcesses.Add(rec);
            db.SaveChanges();
            rec.ProcessNo = "PRC-" + rec.Id.ToString();
            db.SaveChanges();
            string ProcessNo = rec.ProcessNo;

            //fetch count of new joinings to process
            int newJoinings = db.Members.Where(m => m.ProcessNo == null).Select(m => m.Username).DefaultIfEmpty().Count();

            //fetch royal club achievers so far
            int royaltyClubMembers = db.Members.Where(m => m.Achievement1 == 1).Select(m => m.Username).DefaultIfEmpty().Count();

            //fetch diamond royalty club achievers so far
            int diamondRoyaltyClubMembers = db.Members.Where(m => m.Achievement2 == 1).Select(m => m.Username).DefaultIfEmpty().Count();

            //total fund for Royalty Club
            double royaltyClubFund = (100 * newJoinings);

            //total fund for Diamond Royalty Club
            double diamondRoyaltyClubFund = (100 * newJoinings);

            //share per Royalty Club member
            double RoyaltyShare = (royaltyClubFund > 0 ? royaltyClubFund / royaltyClubMembers : 0);

            //share per Diamond Club member
            double DiamondRoyaltyShare = (diamondRoyaltyClubFund > 0 ? diamondRoyaltyClubFund / diamondRoyaltyClubMembers : 0);

            #endregion

            //fetch the list of members
            var memlist = db.Members.Where(m => m.ProcessNo == null).ToList();

            foreach (Member mem in memlist)
            {
                #region calculate incomes

                //1. Calculate Sponsor Income
                double sponsorIncome = db.SponsorIncomes.Where(s => s.RegistrationId == mem.RegistrationId && s.ProcessId == null).Select(m => m.IncomeAmount).DefaultIfEmpty().Sum();

                //2. Calculate Binary Income
                BinaryIncomeLedgerVMTotals BI = GetCurrentBinaryIncome(mem.Username);
                double binaryincome = BI.IncomeAmount;
                //update previous binary opening
                var openingrec = db.BinaryOpenings.Where(o => o.RegistrationId == mem.RegistrationId && o.IsCurrent == true);
                if (openingrec != null)
                {
                    foreach (BinaryOpening bo in openingrec)
                    {
                        bo.IsCurrent = false;
                    }
                    db.SaveChanges();
                }
                BinaryOpening closingrec = new BinaryOpening();
                closingrec.RegistrationId = mem.RegistrationId;
                closingrec.ProcessId = ProcessNo;
                closingrec.LeftSideCd = BI.cdLeftAmount;
                closingrec.RightSideCd = BI.cdRightAmount;
                closingrec.IsCurrent = true;
                db.BinaryOpenings.Add(closingrec);

                //3. Calculate Royalty Club Achiever Income
                double royaltyIncome = 0;
                if (mem.Achievement1 == 1)
                {
                    royaltyIncome = RoyaltyShare;
                }
                else
                {
                    royaltyIncome = 0;
                }

                //4. Calculate Diamond Club Achiever Income
                double diamondroyaltyIncome = 0;
                if (mem.Achievement2 == 1)
                {
                    diamondroyaltyIncome = DiamondRoyaltyShare;
                }
                else
                {
                    diamondroyaltyIncome = 0;
                }

                #endregion

                #region ledger postings

                //5. Posting in ledger in different wallets
                List<Ledger> Postings = new List<Ledger>();

                if (sponsorIncome > 0)   //referral wallet
                {
                    Ledger SPpostcash = new Ledger();
                    SPpostcash.RegistrationId = mem.RegistrationId;
                    SPpostcash.WalletId = 2;
                    SPpostcash.Date = DateTime.Now;
                    SPpostcash.Deposit = (double)sponsorIncome;
                    SPpostcash.Withdraw = 0;
                    SPpostcash.TransactionTypeId = 3;
                    SPpostcash.TransactionId = 0;
                    SPpostcash.SubLedgerId = 2;
                    SPpostcash.ToFromUser = 1;
                    SPpostcash.BatchNo = "";
                    SPpostcash.ProcessId = ProcessNo;
                    SPpostcash.Leftside_cd = 0;
                    SPpostcash.Rightside_cd = 0;
                    Postings.Add(SPpostcash);
                }
                if (binaryincome > 0)  //development wallet
                {
                    Ledger BIpostcash = new Ledger();
                    BIpostcash.RegistrationId = mem.RegistrationId;
                    BIpostcash.WalletId = 1;
                    BIpostcash.Date = DateTime.Now;
                    BIpostcash.Deposit = (double)binaryincome;
                    BIpostcash.Withdraw = 0;
                    BIpostcash.TransactionTypeId = 3;
                    BIpostcash.TransactionId = 0;
                    BIpostcash.SubLedgerId = 1;
                    BIpostcash.ToFromUser = 1;
                    BIpostcash.BatchNo = "";
                    BIpostcash.ProcessId = ProcessNo;
                    BIpostcash.Leftside_cd = 0;
                    BIpostcash.Rightside_cd = 0;
                    Postings.Add(BIpostcash);
                }
                if (royaltyIncome > 0)  //royalty wallet
                {
                    Ledger RIpostcash = new Ledger();
                    RIpostcash.RegistrationId = mem.RegistrationId;
                    RIpostcash.WalletId = 3;
                    RIpostcash.Date = DateTime.Now;
                    RIpostcash.Deposit = (double)royaltyIncome;
                    RIpostcash.Withdraw = 0;
                    RIpostcash.TransactionTypeId = 3;
                    RIpostcash.TransactionId = 0;
                    RIpostcash.SubLedgerId = 3;
                    RIpostcash.ToFromUser = 1;
                    RIpostcash.BatchNo = "";
                    RIpostcash.ProcessId = ProcessNo;
                    RIpostcash.Leftside_cd = 0;
                    RIpostcash.Rightside_cd = 0;
                    Postings.Add(RIpostcash);
                }
                if (diamondroyaltyIncome > 0)  //diamond royalty wallet
                {
                    Ledger DIpostcash = new Ledger();
                    DIpostcash.RegistrationId = mem.RegistrationId;
                    DIpostcash.WalletId = 4;
                    DIpostcash.Date = DateTime.Now;
                    DIpostcash.Deposit = (double)diamondroyaltyIncome;
                    DIpostcash.Withdraw = 0;
                    DIpostcash.TransactionTypeId = 3;
                    DIpostcash.TransactionId = 0;
                    DIpostcash.SubLedgerId = 4;
                    DIpostcash.ToFromUser = 1;
                    DIpostcash.BatchNo = "";
                    DIpostcash.ProcessId = ProcessNo;
                    DIpostcash.Leftside_cd = 0;
                    DIpostcash.Rightside_cd = 0;
                    Postings.Add(DIpostcash);
                }

                if (Postings.Count > 0)
                {
                    db.Ledgers.AddRange(Postings);
                    db.SaveChanges();
                }

                #endregion

                #region update process flag: member
                mem.ProcessNo = ProcessNo;
                db.SaveChanges();
                #endregion
            }

            #region post process
            //6. Update processed records with Process-ID
            var binaryrecs = db.BinaryIncomes.Where(b => b.ProcessId == null).ToList();
            for (int z = 0; z < binaryrecs.Count(); z++)
            {
                binaryrecs[z].ProcessId = ProcessNo;
            }
            var generationrecs = db.GenerationIncomes.Where(b => b.ProcessId == null).ToList();
            for (int z = 0; z < generationrecs.Count(); z++)
            {
                generationrecs[z].ProcessId = ProcessNo;
            }
            db.SaveChanges();
            #endregion

            return Json(new {Success=true },JsonRequestBehavior.AllowGet);
        }
        public JsonResult MyDevIncomeWallet()
        {
            string username = User.Identity.Name;
            var rec = db.Members.SingleOrDefault(r => r.Username.ToUpper().Trim() == username.ToUpper().Trim());
            WeekModel wm = GetCurrentWeekBoundary(DateTime.Now);

            var devwallet = (from l in db.Ledgers
                             from t in db.TransactionTypes
                             from s in db.SubLedgers
                             where l.TransactionTypeId == t.Id &&
                             l.SubLedgerId == s.Id &&
                             l.RegistrationId == rec.RegistrationId &&
                             l.WalletId == 1
                             orderby l.Id
                             select new LedgerVM
                             {
                                 Id = l.Id,
                                 Date = l.Date,
                                 Deposit = l.Deposit,
                                 Withdraw = l.Withdraw,
                                 Balance = 0,
                                 TransactionType = t.TransactionName,
                                 Ledger = s.SubLedgerName,
                                 BatchNo = l.BatchNo,
                                 Remarks = l.Comment
                             }).ToList();

            for (int x = 0; x < devwallet.Count(); x++)
            {
                devwallet[x].sDate = devwallet[x].Date.ToLongDateString();

            }
            return Json(new { DevWallet = devwallet }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult MyRefIncomeWallet()
        {
            string username = User.Identity.Name;
            var rec = db.Members.SingleOrDefault(r => r.Username.ToUpper().Trim() == username.ToUpper().Trim());
            WeekModel wm = GetCurrentWeekBoundary(DateTime.Now);

            var refwallet = (from l in db.Ledgers
                             from t in db.TransactionTypes
                             from s in db.SubLedgers
                             where l.TransactionTypeId == t.Id &&
                             l.SubLedgerId == s.Id &&
                             l.RegistrationId == rec.RegistrationId &&
                             l.WalletId == 2
                             orderby l.Id
                             select new LedgerVM
                             {
                                 Id = l.Id,
                                 Date = l.Date,
                                 Deposit = l.Deposit,
                                 Withdraw = l.Withdraw,
                                 Balance = 0,
                                 TransactionType = t.TransactionName,
                                 Ledger = s.SubLedgerName,
                                 BatchNo = l.BatchNo,
                                 Remarks = l.Comment
                             }).ToList();

            for (int x = 0; x < refwallet.Count(); x++)
            {
                refwallet[x].sDate = refwallet[x].Date.ToLongDateString();

            }
            return Json(new { RefWallet = refwallet }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult MyRoyalIncomeWallet()
        {
            string username = User.Identity.Name;
            var rec = db.Members.SingleOrDefault(r => r.Username.ToUpper().Trim() == username.ToUpper().Trim());
            WeekModel wm = GetCurrentWeekBoundary(DateTime.Now);

            var royalwallet = (from l in db.Ledgers
                               from t in db.TransactionTypes
                               from s in db.SubLedgers
                               where l.TransactionTypeId == t.Id &&
                               l.SubLedgerId == s.Id &&
                               l.RegistrationId == rec.RegistrationId &&
                               l.WalletId == 3
                               orderby l.Id
                               select new LedgerVM
                               {
                                   Id = l.Id,
                                   Date = l.Date,
                                   Deposit = l.Deposit,
                                   Withdraw = l.Withdraw,
                                   Balance = 0,
                                   TransactionType = t.TransactionName,
                                   Ledger = s.SubLedgerName,
                                   BatchNo = l.BatchNo,
                                   Remarks = l.Comment
                               }).ToList();

            for (int x = 0; x < royalwallet.Count(); x++)
            {
                royalwallet[x].sDate = royalwallet[x].Date.ToLongDateString();

            }
            return Json(new { RoyalWallet = royalwallet }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult MyDiamondIncomeWallet()
        {
            string username = User.Identity.Name;
            var rec = db.Members.SingleOrDefault(r => r.Username.ToUpper().Trim() == username.ToUpper().Trim());
            WeekModel wm = GetCurrentWeekBoundary(DateTime.Now);

            var diamondwallet = (from l in db.Ledgers
                                 from t in db.TransactionTypes
                                 from s in db.SubLedgers
                                 where l.TransactionTypeId == t.Id &&
                                 l.SubLedgerId == s.Id &&
                                 l.RegistrationId == rec.RegistrationId &&
                                 l.WalletId == 4
                                 orderby l.Id
                                 select new LedgerVM
                                 {
                                     Id = l.Id,
                                     Date = l.Date,
                                     Deposit = l.Deposit,
                                     Withdraw = l.Withdraw,
                                     Balance = 0,
                                     TransactionType = t.TransactionName,
                                     Ledger = s.SubLedgerName,
                                     BatchNo = l.BatchNo,
                                     Remarks = l.Comment
                                 }).ToList();

            for (int x = 0; x < diamondwallet.Count(); x++)
            {
                diamondwallet[x].sDate = diamondwallet[x].Date.ToLongDateString();

            }
            return Json(new { DiamondWallet = diamondwallet }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult MyJoiningAccountWallet()
        {
            string username = User.Identity.Name;
            var rec = db.Members.SingleOrDefault(r => r.Username.ToUpper().Trim() == username.ToUpper().Trim());
            WeekModel wm = GetCurrentWeekBoundary(DateTime.Now);

            var joiningwallet = (from l in db.Ledgers
                                 from t in db.TransactionTypes
                                 from s in db.SubLedgers
                                 where l.TransactionTypeId == t.Id &&
                                 l.SubLedgerId == s.Id &&
                                 l.RegistrationId == rec.RegistrationId &&
                                 l.WalletId == 5
                                 orderby l.Id
                                 select new LedgerVM
                                 {
                                     Id = l.Id,
                                     Date = l.Date,
                                     Deposit = l.Deposit,
                                     Withdraw = l.Withdraw,
                                     Balance = 0,
                                     TransactionType = t.TransactionName,
                                     Ledger = s.SubLedgerName,
                                     BatchNo = l.BatchNo,
                                     Remarks = l.Comment
                                 }).ToList();

            for (int x = 0; x < joiningwallet.Count(); x++)
            {
                joiningwallet[x].sDate = joiningwallet[x].Date.ToLongDateString();

            }
            return Json(new { JoiningWallet = joiningwallet }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult MyWithdrawalAccountWallet()
        {
            string username = User.Identity.Name;
            var rec = db.Members.SingleOrDefault(r => r.Username.ToUpper().Trim() == username.ToUpper().Trim());
            WeekModel wm = GetCurrentWeekBoundary(DateTime.Now);

            var withdrawalwallet = (from l in db.Ledgers
                                 from t in db.TransactionTypes
                                 from s in db.SubLedgers
                                 where l.TransactionTypeId == t.Id &&
                                 l.SubLedgerId == s.Id &&
                                 l.RegistrationId == rec.RegistrationId &&
                                 l.WalletId == 6
                                 orderby l.Id
                                 select new LedgerVM
                                 {
                                     Id = l.Id,
                                     Date = l.Date,
                                     Deposit = l.Deposit,
                                     Withdraw = l.Withdraw,
                                     Balance = 0,
                                     TransactionType = t.TransactionName,
                                     Ledger = s.SubLedgerName,
                                     BatchNo = l.BatchNo,
                                     Remarks = l.Comment
                                 }).ToList();

            for (int x = 0; x < withdrawalwallet.Count(); x++)
            {
                withdrawalwallet[x].sDate = withdrawalwallet[x].Date.ToLongDateString();

            }
            return Json(new { WithdrawalWallet = withdrawalwallet }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdatePaymentRequest(int id, string comment)
        {
            WithdrawalRequest recToUpdate = db.WithdrawalRequests.SingleOrDefault(w => w.Id == id);
            recToUpdate.Approved_Date = DateTime.Now;
            recToUpdate.Status = "Paid";
            recToUpdate.sDate = recToUpdate.Date.ToLongTimeString();
            recToUpdate.ReferenceNo = (100000 + recToUpdate.Id).ToString();
            recToUpdate.Comment = comment;

            Ledger rectoupdate = db.Ledgers.SingleOrDefault(l => l.BatchNo == recToUpdate.BatchNo);
            rectoupdate.Comment = "Approved by Admin. "+comment;
            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {

            }
            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdatePaymentRequestComment(int id, string comment)
        {
            WithdrawalRequest recToUpdate = db.WithdrawalRequests.SingleOrDefault(w => w.Id == id);
            recToUpdate.Comment = comment;
            try
            {
                db.SaveChanges();
            }
            catch (Exception e)
            {

            }
            return Json(new { Success = true }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public async Task<JsonResult> SendMyWithdrawalConfirmationemail(long RegistrationId, double amount, string status)
        {

            var reg = db.Registrations.SingleOrDefault(r => r.Id == RegistrationId);
            string Username = reg.UserName;
            bool result = false;
            ArrayList array = new ArrayList();

            string line0 = "<table style='width:100%'>";
            string line1 = "<tr><td colspan='2'>Dear <b>" + Username + "</b></td></tr>";
            string line2 = "<tr style='line-height:28px'><td colspan='2'>Your withdrawal request has been approved!.</td></tr>";
            string line3 = "<tr style='line-height:40px'><td colspan='2'>The details are as follows:</td></tr>";
            string line4 = "<tr><td style='width:50px'>Amount:</td><td><b>BDT " + amount.ToString() + "</b></td></tr>";
            string line6 = "<tr><td style='width:50px'>Status:</td><td><b>Approved</b></td></tr>";
            string line7 = "<tr><td style='width:50px'>Notes:</td><td><b>" + status + "</b></td></tr>";
            string line8 = "<tr style='line-height:48px'><td colspan='2'></td><tr>";
            string line9 = "<tr><td colspan='2' style='line-height:48px'>If you have any queries, please do not hesitate to contact us.</td><tr>";
            string line10 = "<tr style='line-height:48px'><td colspan='2'></td><tr>";
            string line11 = "<tr><td colspan='2'>Best regards,</td><tr>";
            string line12 = "<tr><td colspan='2'>JPN Team.</td><tr>";
            string line13 = "<tr><td colspan='2'><a>info@jpnpl.com</a></td><tr>";
            string line14 = "<tr><td colspan='2'><a>www.jpnpl.com</a></td><tr>";
            string line15 = "</table>";
            string body = line0 + line1 + line2 + line3 + line4 + line6 + line7 + line8 + line9 + line10 + line11 + line12 + line13 + line14 + line15;

            result = await SendEMail(reg.EmailId, "", "Withdrawal payment approval mail", body, array);
            return Json(new { status = "Success" }, JsonRequestBehavior.AllowGet);
        }

        public async Task<JsonResult> SendMyWithdrawalCancellationemail(long RegistrationId, double amount, string status)
        {

            var reg = db.Registrations.SingleOrDefault(r => r.Id == RegistrationId);
            string Username = reg.UserName;
            bool result = false;
            ArrayList array = new ArrayList();

            string line0 = "<table style='width:100%'>";
            string line1 = "<tr><td colspan='2'>Dear <b>" + Username + "</b></td></tr>";
            string line2 = "<tr style='line-height:28px'><td colspan='2'>Your withdrawal request has been declined.</td></tr>";
            string line3 = "<tr style='line-height:40px'><td colspan='2'>The details are as follows:</td></tr>";
            string line4 = "<tr><td style='width:50px'>Amount:</td><td><b>BDT " + amount.ToString() + "</b></td></tr>";
            string line6 = "<tr><td style='width:50px'>Status:</td><td><b>Declined</b></td></tr>";
            string line7 = "<tr><td style='width:50px'>Notes:</td><td><b>" + status + "</b></td></tr>";
            string line8 = "<tr style='line-height:48px'><td colspan='2'></td><tr>";
            string line9 = "<tr><td colspan='2' style='line-height:48px'>If you have any queries, please do not hesitate to contact us.</td><tr>";
            string line10 = "<tr style='line-height:48px'><td colspan='2'></td><tr>";
            string line11 = "<tr><td colspan='2'>Best regards,</td><tr>";
            string line12 = "<tr><td colspan='2'>JPN Team</td><tr>";
            string line13 = "<tr><td colspan='2'><a>info@jpnpl.com</a></td><tr>";
            string line14 = "<tr><td colspan='2'><a>www.jpnpl.com</a></td><tr>";
            string line15 = "</table>";
            string body = line0 + line1 + line2 + line3 + line4+ line6 + line7 + line8 + line9 + line10 + line11 + line12 + line13 + line14 + line15;

            result = await SendEMail(reg.EmailId, "", "Withdrawal payment status mail", body, array);
            return Json(new { status = "Success" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult CheckIdentity(string username, string emailId)
        {
            bool status = false;
            string message = "";
            var reg = db.Registrations.SingleOrDefault(r => r.UserName.ToUpper() == username.ToUpper());
            if (reg != null)
            {
                if (reg.EmailId == emailId)
                {
                    //send email
                    string line0 = "<table style='width:100%'>";
                    string line1 = "<tr><td colspan='2'>Dear <b>" + username + "</b></td></tr>";
                    string line2 = "<tr style='line-height:28px'><td colspan='2'>You have requested to restore password of your account.</td></tr>";
                    string line3 = "<tr style='line-height:40px'><td colspan='2'>The details are as follows:</td></tr>";
                    string line4 = "<tr><td style='width:50px'>Username:</td><td><b>" + username + "</b></td></tr>";
                    string line5 = "<tr><td style='width:50px'>Password:</td><td><b>" + reg.Password + "</b></td></tr>";
                    string line8 = "<tr style='line-height:48px'><td colspan='2'></td><tr>";
                    string line9 = "<tr><td colspan='2' style='line-height:48px'>You can change this password in your personal area</td><tr>";
                    string line10 = "<tr style='line-height:48px'><td colspan='2'></td><tr>";
                    string line11 = "<tr><td colspan='2'>Best regards,</td><tr>";
                    string line12 = "<tr><td colspan='2'>JPN Team</td><tr>";
                    string line13 = "<tr><td colspan='2'><a>support@jpnpl.com</a></td><tr>";
                    string line14 = "<tr><td colspan='2'><a>www.jpnpl.com</a></td><tr>";
                    string line15 = "</table>";
                    string body = line0 + line1 + line2 + line3 + line4 + line5 + line8 + line9 + line10 + line11 + line12 + line13 + line14 + line15;


                    string mail_from = "noreply@jpnpl.com";
                    string mail_to = reg.EmailId;
                    ArrayList arr = null;
                    string subj = "Forget Password mail";
                    string mail_cc = "info@jpnpl.com";
                    System.Collections.Hashtable ht = new System.Collections.Hashtable();
                    ht.Add("FROM", mail_from);
                    ht.Add("TO", mail_to);
                    ht.Add("CC", mail_cc);
                    //ht.Add("BCC", mail_cc);
                    ht.Add("SUBJECT", subj);
                    ht.Add("BODY", body);
                    ht.Add("ATTACHMENT", arr);

                    try
                    {
                        Email mail = new Email(ht);
                        mail.SendEMail();

                    }
                    catch (Exception e)
                    {

                    }
                    status = true;
                    message = "An email has been sent to your registered email Id.";
                    return Json(new { Status = status, Message = message }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    status = false;
                    message = "ERROR!!! Username and registered email id did not match.";
                    return Json(new { Status = status, Message = message }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                status = false;
                message = "ERROR!!! Username not found.";
                return Json(new { Status = status, Message = message }, JsonRequestBehavior.AllowGet);
            }

            //return Json(JsonRequestBehavior.AllowGet);
        }
        #endregion
    }


}

