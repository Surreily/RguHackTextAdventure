using System;
using System.Collections.Generic;
using System.Text;

namespace RguHackTextAdventure.Core.Items.Keys {
    public class SmallKeyItem : ItemBase {
        private int _level;

        public SmallKeyItem(int level) {
            if (level < 0 || level > 2) {
                throw new ArgumentException("Key level must be from 0 to 2");
            }

            _level = level;
        }

        public override string Name {
            get { return "Small " + GetKeyMaterial() + " key"; }
        }

        public override List<string> Aliases {
            get { return new List<string>() { "key" }; }
        }

        public override void Describe(StringBuilder builder) {
            builder.Append("A small " + GetKeyMaterial() + " key. It might fit a small lock...");
        }

        private string GetKeyMaterial() {
            switch (_level) {
                case 0:
                    return "rusty bronze";
                case 1:
                    return "silver";
                case 2:
                    return "golden";
                default:
                    throw new InvalidOperationException("Key level is invalid");
            }
        }
    }
}
