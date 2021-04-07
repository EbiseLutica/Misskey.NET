using System;

namespace MisskeyDotNet.Example
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    sealed class DemoAttribute : Attribute
    {
        public string Name { get; }

        public DemoAttribute(string name)
        {
            Name = name;
        }
    }
}
