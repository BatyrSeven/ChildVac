﻿using System.Collections.Generic;

namespace ChildVac.WebApi.Domain.Entities
{
    public class Parent : User
    {
        public List<Child> Children { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}
