using System;
using System.Collections.Generic;
using System.Text;

namespace RguHackTextAdventure.Core.Items.Consumables {
    public class LegoSet : ItemBase {
        public override string Name {
            get { return "LEGO set"; }
        }

        public override List<string> Aliases {
            get { return new List<string> { "lego", "set" }; }
        }

        public override void Describe(StringBuilder builder) {
            builder.AppendLine("A LEGO set! Could you build something with this?");
        }

        public override bool Use(StringBuilder builder) {
            builder.AppendLine("You build a hole with the LEGO set and escape through it.");
            return true;
        }
    }
}
