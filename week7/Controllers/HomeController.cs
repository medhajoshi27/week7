using week7.Service;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using week7.Repository;
using week7.Repository.Entity;
using System.IO;

namespace week7.Controllers
{
        public class HomeController : Controller
        {
            private Employeecontext db = new Employeecontext();

            // GET: Home
            private readonly IEmployeeService employeeservice;

            public ActionResult Index()
            {
                return View(db.Employees.ToList());
            }
            public HomeController(IEmployeeService employeeService)
            {
                this.employeeservice = employeeService;
            }
            public ActionResult Create()
            {
            Employeecontext db = new Employeecontext();


            ViewBag.StateList = db.StateList;
            var model = new Emp2();
            return View(model);
        }
            public ActionResult Details(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Emp2 emp = db.Employees.Find(id);
                if (emp == null)
                {
                    return HttpNotFound();
                }
                return View(emp);
            }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Email,Phone,MaritalStatus,State,City,image")] Emp2 emp, HttpPostedFileBase imageFile)
        {

            if (ModelState.IsValid)
            {
                db.Employees.Add(emp);
                db.SaveChanges();
                if (imageFile != null && imageFile.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(imageFile.FileName);

                    emp.image = fileName;
                    //db.Employees.Add(emp);//Adding into the database
                    db.SaveChanges();
                    var path = Path.Combine(Server.MapPath("~/Content/img/"));
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                    imageFile.SaveAs(path + "/" + fileName);
                }
                return RedirectToAction("Index");
                //return RedirectToAction("Index");
            }
            ViewBag.StateList = db.StateList;
            ViewBag.CityList = db.CityList;
            return View(emp);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
            {

                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Emp2 employee = db.Employees.Find(id);
                if (employee == null)
                {
                    return HttpNotFound();
                }
                return View(employee);
        }


            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Edit([Bind(Include = "Id,Name,Email,Phone,MaritalStatus,State,City")] Emp2 employee)
            {
                if (ModelState.IsValid)
                {
                    db.Entry(employee).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(employee);
            }

            // GET: Products/Delete/5
            public ActionResult Delete(int? id)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Emp2 emp = db.Employees.Find(id);
                if (emp == null)
                {
                    return HttpNotFound();
                }
                return View(emp);
            }

            // POST: Products/Delete/5
            [HttpPost, ActionName("Delete")]
            [ValidateAntiForgeryToken]
            public ActionResult DeleteConfirmed(int id)
            {
                Emp2 emp = db.Employees.Find(id);
                db.Employees.Remove(emp);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        public ActionResult FillCity(int state)
        {
            Employeecontext db = new Employeecontext();

            var cities = db.CityList.Where(c => c.StateId == state);
            return Json(cities, JsonRequestBehavior.AllowGet);
        }
        private byte[] GetBytesFromFile(HttpPostedFileBase file)
        {
            using (Stream inputStream = file.InputStream)
            {
                MemoryStream memoryStream = inputStream as MemoryStream;
                if (memoryStream == null)
                {
                    memoryStream = new MemoryStream();
                    inputStream.CopyTo(memoryStream);
                }
                return memoryStream.ToArray();
            }
        }
    }
    }