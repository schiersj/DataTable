using System;
using System.Linq.Expressions;
using DataTables.Models;

namespace DataTables.Contracts
{
    public interface IIdentifiable
    {
        int Id { get; set; }
    }

    public interface IDataService<TEntity> where TEntity : class, IIdentifiable
    {
        DataTableReturnModel<TEntity> GetPaginated<TProperty>(int skip, int take,
            Expression<Func<TEntity, TProperty>> sortBy = null,
            Expression<Func<TEntity, bool>> filter = null);
    }
}