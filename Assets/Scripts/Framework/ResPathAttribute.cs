using System;

namespace Framework
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class ResPathAttribute : Attribute
    {
        public readonly string Path;

        public ResPathAttribute(string path)
        {
            Path = path;
        }
    }
}