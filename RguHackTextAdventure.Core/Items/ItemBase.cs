using System.Text;

namespace RguHackTextAdventure.Core.Items
{
    public abstract class ItemBase
    {
        public abstract string Name { get; }

        public abstract void Describe(StringBuilder builder);
    }
}
