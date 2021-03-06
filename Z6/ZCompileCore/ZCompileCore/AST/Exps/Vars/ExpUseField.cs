﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ZCompileCore.AST;
using ZCompileCore.Contexts;
using ZCompileCore.Lex;
using ZCompileCore.Tools;
using ZCompileDesc.Descriptions;

namespace ZCompileCore.AST.Exps
{
    /// <summary>
    /// 程序中定义的本类Field
    /// </summary>
    public class ExpUseField : ExpVarBase
    {
        protected ZLFieldInfo ZField;

        public ExpUseField(ContextExp expContext, LexToken token)
            : base(expContext)
        {
            VarToken = token;
        }

        public override Exp Analy()
        {
            if (this.IsAnalyed) return this;
            VarName = VarToken.Text;
            ZField = SearchZMember(VarName);
            RetType = ZField.ZFieldType;
            IsAnalyed = true;
            return this;
        }

        private ZLFieldInfo SearchZMember(string zname)
        {
            ContextImportUse importUseContext = this.FileContext.ImportUseContext;
            return importUseContext.SearchUseZField(zname);
        }

        #region Emit

        public void EmitLoadFielda()
        {
            bool isstatic = this.ZField.IsStatic;
            EmitHelper.EmitThis(IL, isstatic);
            EmitHelper.LoadFielda(IL, ZField.SharpField);
        }

        public override void Emit()
        {
            EmitGetField();
        }

        private void EmitGetField()
        {
            EmitHelper.LoadField(IL, ZField.SharpField);
            base.EmitConv();
        }

        public override void EmitSet(Exp valueExp)
        {
            EmitSetField(valueExp);
        }

        private void EmitSetField(Exp valueExp)
        {
            EmitHelper.EmitThis(IL, true);
            EmitValueExp(valueExp);
            EmitHelper.StormField(IL, ZField.SharpField);
            base.EmitConv();
        }
        #endregion

        #region 覆盖

        public override bool CanWrite
        {
            get
            {
                return ZField.CanWrite;
            }
        }

        #endregion
    }
}
