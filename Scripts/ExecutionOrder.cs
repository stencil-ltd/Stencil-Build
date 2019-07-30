using System;

namespace CustomOrder
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ExecutionOrder : Attribute
    {
        public readonly int Order;

        public ExecutionOrder(int order)
        {
            Order = order;
        }
    }
}