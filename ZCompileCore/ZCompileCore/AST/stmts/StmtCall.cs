﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using ZCompileCore.Contexts;

namespace ZCompileCore.AST
{
    public  class StmtCall:Stmt
    {
        public Exp CallExp { get; set; }
        ExpEach _eachExp;

        public override void Analy()
        {
            //if (this.ToString().StartsWith("清除出界子弹"))
            //{
            //    Console.WriteLine("清除出界子弹");
            //}
            var tempExp1 = CallExp;
            var CallExp2 = ParseExp(tempExp1);
            var CallExp3 = CallExp2.Analy();
            if (_eachExp != null)
            {
                _eachExp.BodyExp = CallExp3;
                CallExp = _eachExp;
            }
            else
            {
                CallExp = CallExp3;
            }
        }
   
        public void SetEachExp(ExpEach eachExp)
        {
            _eachExp = eachExp;
        }

        public override void Emit()
        {
            //if (this.ToString().StartsWith("子弹群添加Z"))
            //{
            //    Console.WriteLine("子弹群添加Z");
            //}
            CallExp.Emit();
            if (CallExp.RetType.SharpType != typeof(void))
            {
                IL.Emit(OpCodes.Pop);
            }
        }

        public override string ToString()
        {
            return CallExp.ToString() + ";";
        }
    }
}