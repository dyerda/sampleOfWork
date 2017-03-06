using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Darren.Dyer.lab2.Models;
using Darren.Dyer.lab2.Models.ViewModels;

namespace Darren.Dyer.lab2.Controllers
{
    public class RentalOrdersController : Controller
    {
        private BusterBlockContext db = new BusterBlockContext();

        // GET: RentalOrders
        public ActionResult Index()
        {
            var rentalOrders = db.RentalOrders.Include(r => r.Customer);
            return View(rentalOrders.ToList());
        }

        // GET: RentalOrders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RentalOrder rentalOrder = db.RentalOrders.Find(id);
            if (rentalOrder == null)
            {
                return HttpNotFound();
            }
            return View(rentalOrder);
        }

        // GET: RentalOrders/Create
        public ActionResult Create()
        {
            ViewBag.CustomerCustomerId = new SelectList(db.Customers, "CustomerId", "FirstName");
            return View();
        }

        // POST: RentalOrders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RentalOrderId,CustomerCustomerId,CheckoutDate,ReturnDate")] RentalOrder rentalOrder)
        {
            if (ModelState.IsValid)
            {
                db.RentalOrders.Add(rentalOrder);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerCustomerId = new SelectList(db.Customers, "CustomerId", "FirstName", rentalOrder.CustomerCustomerId);
            return View(rentalOrder);
        }

        // GET: RentalOrders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RentalOrder rentalOrder = db.RentalOrders.Find(id);
            if (rentalOrder == null)
            {
                return HttpNotFound();
            }
            ViewBag.CustomerCustomerId = new SelectList(db.Customers, "CustomerId", "FirstName", rentalOrder.CustomerCustomerId);
            return View(rentalOrder);
        }

        // POST: RentalOrders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RentalOrderId,CustomerCustomerId,CheckoutDate,ReturnDate")] RentalOrder rentalOrder)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rentalOrder).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerCustomerId = new SelectList(db.Customers, "CustomerId", "FirstName", rentalOrder.CustomerCustomerId);
            return View(rentalOrder);
        }

        // GET: RentalOrders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RentalOrder rentalOrder = db.RentalOrders.Find(id);
            if (rentalOrder == null)
            {
                return HttpNotFound();
            }
            return View(rentalOrder);
        }

        // POST: RentalOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RentalOrder rentalOrder = db.RentalOrders.Find(id);
            db.RentalOrders.Remove(rentalOrder);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult CreateAndAsscoaite(int ?id)
        {
            RentalOrder _rentalOrder = db.RentalOrders.Find(id);
            if (_rentalOrder == null)
            {
                return HttpNotFound();
            }
            RentalOrderViewModel _rentalOrderViewModel = new RentalOrderViewModel()
            {
                RentalOrderId = _rentalOrder.RentalOrderId,
                CustomerCustomerId = _rentalOrder.CustomerCustomerId,
                CheckoutDate = _rentalOrder.CheckoutDate
            };

            List<ItemViewModel> _itemViewModel = new List<ItemViewModel> ();
            foreach (Item i in db.Items)
            {
                _itemViewModel.Add(new ItemViewModel()
                {
                    ItemId = i.ItemId,
                    Name = i.Name,
                    Category = i.Category,
                    Selected = _rentalOrder.Items.Contains(i)
                  
                });
            }

            _rentalOrderViewModel.Items = _itemViewModel;
            return View(_rentalOrderViewModel);

        }

        [HttpPost]
        public ActionResult CreateAndAsscoaite(RentalOrderViewModel _rentalOrderViewModel)
        {
            RentalOrder _rentalOrder = db.RentalOrders.Find(_rentalOrderViewModel.RentalOrderId);
            _rentalOrder.Items.Clear();

            foreach (ItemViewModel _itemViewModel in _rentalOrderViewModel.Items)
            {
                if (_itemViewModel.Selected)
                {
                    Item _item = db.Items.Find(_itemViewModel.ItemId);
                    _rentalOrder.Items.Add(_item);
                }
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
