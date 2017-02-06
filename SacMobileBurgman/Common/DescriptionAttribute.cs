using System;

namespace SacMobileBurgman.Common
{
    internal class DescriptionAttribute : Attribute
    {
        public DescriptionAttribute(string description)
        {
            this.Description = description;
        }

        public string Description { get; internal set; }
    }
}