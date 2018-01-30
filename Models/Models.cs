using System.Collections.Generic;
using DataTables.Contracts;

namespace DataTables.Models
{
    public class DataTableAjaxPostModel
    {
        // properties are not capital due to json mapping
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public List<Column> columns { get; set; }
        public Search search { get; set; }
        public List<Order> order { get; set; }
    }
    
    public class Column
    {
        public string data { get; set; }
        public string name { get; set; }
        public bool searchable { get; set; }
        public bool orderable { get; set; }
        public Search search { get; set; }
    }
    
    public class Search
    {
        public string value { get; set; }
        public string regex { get; set; }
    }
    
    public class Order
    {
        public int column { get; set; }
        public string dir { get; set; }
    }

    public class DataTableReturnModel<TEntity>
    {
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public IEnumerable<TEntity> data { get; set; }
    }

    public class Entity : IIdentifiable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Occupation { get; set; }
    }
}