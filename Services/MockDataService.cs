using DataTables.Models;
using DataTables.Contracts;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace DataTables.Services
{
    public class MockDataService : IDataService<Entity>
    {
        private IList<Entity> _entities;

        public MockDataService()
        {
            _entities = new List<Entity>();

            for (int i = 0; i < 100; i++)
            {
                var entity = new Entity
                {
                    Id = i,
                    Name = $"Name {i.ToString().PadLeft(2, '0')}",
                    City = $"City {i.ToString().PadLeft(2, '0')}",
                    State = $"State {i.ToString().PadLeft(2, '0')}",
                    Occupation = $"Occupation {i.ToString().PadLeft(2, '0')}"
                };

                _entities.Add(entity);
            }
        }

        public DataTableReturnModel<Entity> GetPaginated<TProperty>(int skip, int take,
            Expression<Func<Entity, TProperty>> sortBy = null,
            Expression<Func<Entity, bool>> filter = null)
        {
            var data = _entities.AsQueryable();
            var recordsTotal = data.Count();

            if (filter != null)
                data = data.Where(filter);

            var recordsFilterd = data.Count();

            data = sortBy != null ? data.OrderBy(sortBy) : data.OrderBy(e => e.Id);
            data = data.Skip(skip).Take(take);

            return new DataTableReturnModel<Entity> 
            {
                recordsTotal = recordsTotal,
                recordsFiltered = recordsFilterd,
                data = data.ToList()
            };
        }
    }
}