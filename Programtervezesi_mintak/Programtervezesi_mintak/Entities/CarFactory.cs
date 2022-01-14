﻿using Programtervezesi_mintak.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Programtervezesi_mintak.Entities
{
    class CarFactory : iToyFactory
    {
        public Toy CreateNew()
        {
            return new Car();
        }
    }
}
