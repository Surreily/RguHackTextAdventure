using System.Collections.Generic;
using System.Text;

namespace RguHackTextAdventure.Core.Items
{
    public abstract class ItemBase
    {
        public abstract string Name { get; }

        public abstract List<string> Aliases { get; }

        public abstract void Describe(StringBuilder builder);

        public virtual bool Use(StringBuilder builder) {
            builder.AppendLine("You can't use this.");
            return false;
        }
    }
}
