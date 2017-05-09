using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using TODO.Models;
using Microsoft.AspNet.Identity;

namespace TODO.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        protected Entities entities;
        ApplicationDbContext context;

        public HomeController()
        {
            entities = new Entities();
            context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                List<EntryViewModel> entryList = new List<EntryViewModel>();
                var entries = entities.Entry.Where(b => b.DoneDate == null && System.DateTime.Compare(System.DateTime.Now,b.Expire)<0).OrderBy(b => b.Expire).ToArray();
                string uid = User.Identity.GetUserId();
                if (User.IsInRole("Member"))
                {
                    
                    foreach(var entry in entries)
                    {
                        if (entry.UserId == uid)
                        {
                            EntryViewModel model = new EntryViewModel(entry.Id, entry.Title, entry.Desc, entry.Created, entry.Expire);
                            ApplicationUser user = context.Users.FirstOrDefault(x => x.Id == entry.OwnerId);
                            model.Owner = user.UserName;
                            entryList.Add(model);
                        }
                    }
                }
                else if(User.IsInRole("Admin"))
                {
                    foreach (var entry in entries)
                    {
                        if (entry.OwnerId == uid)
                        {
                            EntryViewModel model = new EntryViewModel(entry.Id, entry.Title, entry.Desc, entry.Created, entry.Expire);
                            ApplicationUser user = context.Users.FirstOrDefault(x => x.Id == entry.UserId);
                            model.Member = user.UserName;
                            entryList.Add(model);
                        }
                    }
                }
                ViewBag.Entries = entryList;
            }
            else
            {
                ViewBag.Entries = null;
            }

            return View();
        }

        [HttpPost]
        public ActionResult Index(Int32 id)
        {   
            if (User.IsInRole("Member"))
            {
                string uid = User.Identity.GetUserId();
                Entry entry = entities.Entry.FirstOrDefault(b => b.Id == id && b.UserId == uid);
                if (entry != null)
                {
                    entry.DoneDate = DateTime.Now;
                    entities.SaveChanges();
                }
            }
            else if(User.IsInRole("Admin"))
            {
                Entry entry = entities.Entry.FirstOrDefault(b => b.Id == id);
                if (entry != null)
                {
                    entry.DoneDate = DateTime.Now;
                    entities.SaveChanges();
                }
            }
            

            return RedirectToAction("Index");
        }

        public ActionResult DoneList()
        {
            if (Request.IsAuthenticated)
            {
                List<EntryViewModel> entryList = new List<EntryViewModel>();
                var entries = entities.Entry.Where(b => b.DoneDate != null).OrderByDescending(b => b.DoneDate).ToArray();
                string uid = User.Identity.GetUserId();
                if (User.IsInRole("Member"))
                {
                    foreach (var entry in entries)
                    {
                        if (entry.UserId == uid)
                        {
                            EntryViewModel model = new EntryViewModel(entry.Id, entry.Title, entry.Desc, entry.Created, entry.Expire, entry.DoneDate);
                            ApplicationUser user = context.Users.FirstOrDefault(x => x.Id == entry.OwnerId);
                            model.Owner = user.UserName;
                            entryList.Add(model);
                        }
                    }
                }
                else if (User.IsInRole("Admin"))
                {
                    foreach (var entry in entries)
                    {
                        if (entry.OwnerId == uid)
                        {
                            EntryViewModel model = new EntryViewModel(entry.Id, entry.Title, entry.Desc, entry.Created, entry.Expire, entry.DoneDate);
                            ApplicationUser user = context.Users.FirstOrDefault(x => x.Id == entry.UserId);
                            model.Member = user.UserName;
                            entryList.Add(model);
                        }
                    }
                }
                ViewBag.Entries = entryList;
            }
            else
            {
                ViewBag.Entries = null;
            }

            return View();
        }

        [HttpPost]
        public ActionResult DoneList(int id)
        {
            string uid = User.Identity.GetUserId();

            Entry ent = entities.Entry.FirstOrDefault(b => b.Id == id && b.OwnerId == uid);
            if (ent != null)
            {
                entities.Entry.Remove(ent);
            }

            entities.SaveChanges();
            return RedirectToAction("DoneList");
        }

        public ActionResult Fail()
        {
            if (Request.IsAuthenticated)
            {
                List<EntryViewModel> entryList = new List<EntryViewModel>();
                var entries = entities.Entry.Where(b => b.DoneDate == null && System.DateTime.Compare(System.DateTime.Now, b.Expire) > 0).OrderByDescending(b => b.Expire).ToArray();
                string uid = User.Identity.GetUserId();
                if (User.IsInRole("Member"))
                {
                    foreach (var entry in entries)
                    {
                        if (entry.UserId == uid)
                        {
                            EntryViewModel model = new EntryViewModel(entry.Id, entry.Title, entry.Desc, entry.Created, entry.Expire);
                            ApplicationUser user = context.Users.FirstOrDefault(x => x.Id == entry.OwnerId);
                            model.Owner = user.UserName;
                            entryList.Add(model);
                        }
                    }
                }
                else if (User.IsInRole("Admin"))
                {
                    foreach (var entry in entries)
                    {
                        if (entry.OwnerId == uid)
                        {
                            EntryViewModel model = new EntryViewModel(entry.Id, entry.Title, entry.Desc, entry.Created, entry.Expire);
                            ApplicationUser user = context.Users.FirstOrDefault(x => x.Id == entry.UserId);
                            model.Member = user.UserName;
                            entryList.Add(model);
                        }
                    }
                }
                ViewBag.Entries = entryList;
            }
            else
            {
                ViewBag.Entries = null;
            }

            return View();
        }

        [HttpPost]
        public ActionResult Fail(int id)
        {
            string uid = User.Identity.GetUserId();

            Entry ent = entities.Entry.FirstOrDefault(b => b.Id == id && b.OwnerId == uid);
            if (ent != null)
            {
                entities.Entry.Remove(ent);
            }

            entities.SaveChanges();
            return RedirectToAction("Fail");
        }

        public ActionResult ViewEntry()
        {
            ViewBag.Error = "Válasszon bejegyzést!";

            return View();
        }

        [HttpGet]
        public ActionResult ViewEntry(Int32 id=-1)
        {
            if (id ==-1)
            {
                ViewBag.Error = "Válasszon bejegyzést!";
            }
            else
            {
                string uid = User.Identity.GetUserId();
                if (User.IsInRole("Member"))
                {
                    Entry entry = entities.Entry.FirstOrDefault(b => b.Id == id && b.UserId == uid);
                    if (entry != null)
                    {
                        EntryViewModel model = new EntryViewModel(entry.Id, entry.Title, entry.Desc, entry.Created, entry.Expire, entry.DoneDate);
                        ApplicationUser user = context.Users.FirstOrDefault(x => x.Id == entry.OwnerId);
                        model.Owner = user.UserName;
                        
                        return View(model);
                    }

                }
                else if (User.IsInRole("Admin"))
                {
                    Entry entry = entities.Entry.FirstOrDefault(b => b.Id == id && b.OwnerId==uid);
                    if (entry != null)
                    {
                        EntryViewModel model = new EntryViewModel(entry.Id, entry.Title, entry.Desc, entry.Created, entry.Expire, entry.DoneDate);
                        ApplicationUser user = context.Users.FirstOrDefault(x => x.Id == entry.UserId);
                        model.Member = user.UserName;
                        
                        return View(model);
                    }
                }
            }
            return View();
        }

        public ActionResult NewEntry()
        {
            EntryViewModel Model = new EntryViewModel();
            if (User.IsInRole("Admin"))
            {
                List<SelectListItem> items = new List<SelectListItem>();
                var allusers = context.Users.ToList();
                foreach (ApplicationUser user in allusers)
                {
                    items.Add(new SelectListItem { Text = user.UserName, Value = user.UserName });
                }
                Model.Members = items;
                Model.Member = User.Identity.Name;
            }
            return View(Model);
        }

        [HttpPost]
        public ActionResult NewEntry(EntryViewModel entry, FormCollection form)
        {
            string uid = "";
            if (ModelState.IsValid)
            {
                if (User.IsInRole("Member"))
                {
                    uid = User.Identity.GetUserId();
                }
                if (User.IsInRole("Admin"))
                {
                    string member = form["SelectedMember"].ToString();
                    ApplicationUser user = context.Users.FirstOrDefault(u => u.UserName == member);
                    uid = user.Id;
                }
                Entry ent = new Entry { UserId = uid, OwnerId= User.Identity.GetUserId(),Title = entry.Title, Created = DateTime.Now, Desc = entry.Desc, Expire = entry.ExpireDate};
                entities.Entry.Add(ent);
                entities.SaveChanges();
                return RedirectToAction("Index");
            }
            return NewEntry();
        }

        public ActionResult SetEntry(Int32 id)
        {
            if (User.IsInRole("Member"))
            {
                string uid = User.Identity.GetUserId();
                Entry entry = entities.Entry.FirstOrDefault(b => b.Id == id && b.UserId == uid);
                if (entry != null)
                {
                    ViewBag.ID = entry.Id;
                    return View(new EntryViewModel(entry.Title, entry.Desc, entry.Expire));
                }
            }
            else if (User.IsInRole("Admin"))
            {
                Entry entry = entities.Entry.FirstOrDefault(b => b.Id == id);

                if (entry != null)
                {
                    EntryViewModel Model = new EntryViewModel(entry.Title, entry.Desc, entry.Expire);
                    List<SelectListItem> items = new List<SelectListItem>();
                    var allusers = context.Users.ToList();
                    foreach (ApplicationUser user in allusers)
                    {
                        if (entry.UserId == user.Id) Model.Member = user.UserName;
                        items.Add(new SelectListItem { Text = user.UserName, Value = user.UserName });
                    }
                        
                    Model.Members = items;
                    ViewBag.ID = entry.Id;
                    return View(Model);
                }
            }

            return View();
        }

        [HttpPost]
        public ActionResult SetEntry(EntryViewModel entry, int id, FormCollection form)
        {
            string uid = User.Identity.GetUserId();
            if (ModelState.IsValid)
            { 
                if (User.IsInRole("Member"))
                {
                    Entry ent = entities.Entry.FirstOrDefault(b => b.Id == id && b.UserId == uid);
                    ent.Title = entry.Title;
                    ent.Desc = entry.Desc;
                    ent.Expire = entry.ExpireDate;
                }
                if (User.IsInRole("Admin"))
                {
                    string member = form["SelectedMember"].ToString();
                    ApplicationUser user = context.Users.FirstOrDefault(u=>u.UserName== member);
                    Entry ent = entities.Entry.FirstOrDefault(b => b.Id == id);
                    ent.Title = entry.Title;
                    ent.Desc = entry.Desc;
                    ent.Expire = entry.ExpireDate;
                    ent.UserId = user.Id;
                }
                entities.SaveChanges();
                return RedirectToAction("Index");
            }
            return SetEntry(id);
        }

        public ActionResult Contact()
        {
            return View();
        }

    }
}