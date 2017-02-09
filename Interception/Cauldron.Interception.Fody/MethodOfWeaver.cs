﻿using Mono.Cecil;
using Mono.Cecil.Cil;
using System;
using System.Linq;

namespace Cauldron.Interception.Fody
{
    public class MethodOfWeaver : ModuleWeaverBase
    {
        public MethodOfWeaver(ModuleWeaver weaver) : base(weaver)
        {
        }

        public override void Implement()
        {
            if (!this.ModuleDefinition.AllReferencedAssemblies().Any(x => x.FullName == "Cauldron.Core"))
            {
                this.LogInfo("Skipping implementation of Cauldron.Core.Reflection.GetMethodBase. Cauldron.Core is not referenced in the project");
                return;
            }

            this.LogInfo("Implementing Cauldron.Core.Reflection.GetMethodBase");

            var methodOf = "Cauldron.Core.Reflection".ToTypeDefinition().GetMethodReference("GetMethodBase", 0);
            var allMethodsWithMethodOfCalls = this.GetMethodsWhere(x => x.OpCode == OpCodes.Call && (x.Operand as MethodReference).FullName == methodOf.FullName);
            var getMethodFromHandleRef = typeof(System.Reflection.MethodBase).Import().GetMethodReference("GetMethodFromHandle", 2).Import();

            foreach (var method in allMethodsWithMethodOfCalls)
            {
                var processor = method.Method.Body.GetILProcessor();

                foreach (var getMethodBaseCall in method.Instruction)
                {
                    processor.InsertBefore(getMethodBaseCall, processor.Create(OpCodes.Ldtoken, method.Method));
                    processor.InsertBefore(getMethodBaseCall, processor.Create(OpCodes.Ldtoken, method.Method.DeclaringType));
                    processor.InsertBefore(getMethodBaseCall, processor.Create(OpCodes.Call, getMethodFromHandleRef));
                    processor.Remove(getMethodBaseCall);
                }
            }
        }

        protected override void ImplementLockableOnEnter(MethodWeaverInfo methodWeaverInfo, VariableDefinition attributeVariable, MethodReference interceptorOnEnter, VariableDefinition parametersArrayVariable, FieldDefinition semaphoreSlim)
        {
            throw new NotImplementedException();
        }

        protected override void ImplementOnEnter(MethodWeaverInfo methodWeaverInfo, VariableDefinition attributeVariable, MethodReference interceptorOnEnter, VariableDefinition parametersArrayVariable)
        {
            throw new NotImplementedException();
        }
    }
}