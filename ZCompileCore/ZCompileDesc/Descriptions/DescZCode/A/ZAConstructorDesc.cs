﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZCompileDesc.Descriptions
{
    public abstract class ZAConstructorDesc : IDesc
    {
        public abstract string ToZCode();

        public override string ToString()
        {
            return this.ToZCode();
        }
    }
}
