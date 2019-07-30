using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace CustomOrder
{
    public static class ExecutionOrderHelper
    { 
        [MenuItem("Stencil/Configure Execution Order")]
        public static void ConfigureExecutionOrder()
        {
            var mono = Resources.FindObjectsOfTypeAll<MonoBehaviour>()
                .Where(it => it.GetType().GetCustomAttribute(typeof(ExecutionOrder)) != null);
            foreach (var b in mono)
            {
                var attr = b.GetType().GetCustomAttribute<ExecutionOrder>();
                var order = attr.Order;
                var script = MonoScript.FromMonoBehaviour(b);
                Debug.Log($"Setting {b} to order {order}");
                MonoImporter.SetExecutionOrder(script, order);
            }
        }
    }
}