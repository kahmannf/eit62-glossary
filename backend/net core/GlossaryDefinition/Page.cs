﻿
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlossaryDefinition
{
    public class Page<T>
    {
        public T[] Items { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
