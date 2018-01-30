using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataTables.Models;
using DataTables.Services;

namespace DataTables.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetAll(DataTableAjaxPostModel model)
        {
            var entities = GetEntities(model);

            return Json(new {
                model.draw,
                entities.recordsTotal,
                entities.recordsFiltered,
                data = entities.data
            });
        }

        private DataTableReturnModel<Entity> GetEntities(DataTableAjaxPostModel model)
        {
            var skip = model.start;
            var take = model.length;
            var searchBy = model.search?.value;

            return new MockDataService().GetPaginated(skip, take
            , sortBy: e => e.Name.Trim()
            , filter: FilterEntities(searchBy));
        }
        
        private static Expression<Func<Entity, bool>> FilterEntities(string searchBy)
        {
            if (string.IsNullOrWhiteSpace(searchBy)) return null;

            searchBy = searchBy.ToUpperInvariant();

            return query => query.Name.ToUpperInvariant().Contains(searchBy) ||
                            query.City.ToUpperInvariant().Contains(searchBy) ||
                            query.State.ToUpperInvariant().Contains(searchBy) ||
                            query.Occupation.ToUpperInvariant().Contains(searchBy);
        }
    }
}