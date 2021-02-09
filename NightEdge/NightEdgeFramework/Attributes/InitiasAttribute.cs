using System;
using System.Reflection;
using System.Collections.Generic;
using System.Text;

namespace NightEdgeFrameworks
{
    /// <summary>
    /// 用此特性标记需要初始化的单例对象。
    /// </summary>
    public class InitiasAttribute:Attribute
    {
        // 在初始化 NightEdgeCore 时调用这个方法
        // 小心Initialize方法重复迭代产生的栈溢出
        // 需要进一步按照功能划分程序集
        internal void Initialize()
        {
            var assembly = Assembly.GetExecutingAssembly();
            foreach (var type in assembly.GetTypes())
            {
                foreach (var mi in type.GetMethods())
                {
                    if (mi.Name == "Initialize")
                        mi.Invoke(null, null);
                }
               
            }
        }
    }
}
