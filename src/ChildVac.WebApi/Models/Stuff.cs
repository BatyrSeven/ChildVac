﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChildVac.WebApi.Models
{
    public class Stuff : User
    {
        public Hospital Hospital { get; set; }
    }
}
