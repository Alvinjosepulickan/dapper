﻿using Dapper.Contrib.Extensions;

namespace DapperDemo.Models {
    [Table("Companies")]
    public class Company {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        [Write(false)]
        public List<Employee> Employees { get; set; }
    }
}
