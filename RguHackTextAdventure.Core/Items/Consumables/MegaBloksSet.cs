using System;
using System.Collections.Generic;
using System.Text;

namespace RguHackTextAdventure.Core.Items.Consumables {
    class MegaBloksSet : ItemBase {
        public override string Name {
            get { return "MEGABLOKS Set"; }
        }

        public override List<string> Aliases {
            get { return new List<string> { "megabloks" }; }
        }

        public override void Describe(StringBuilder builder) {
            builder.Append("It's MEGABLOKS set! I wonder what you could build with this...");
        }

        public override bool Use(StringBuilder builder) {
            builder.AppendLine("I am disappointed in you. You lose.");
            builder.AppendLine("~ GAME OVER ~");
            return true;
        }
    }
}
