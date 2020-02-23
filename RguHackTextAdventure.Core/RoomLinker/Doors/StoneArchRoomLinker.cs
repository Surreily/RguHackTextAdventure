using System;
using System.Collections.Generic;
using System.Text;

namespace RguHackTextAdventure.Core.RoomLinker.Doors {
    public class StoneArchRoomLinker : RoomLinkerBase {
        public override List<string> Aliases {
            get { return new List<string> { "arch", "archway" }; }
        }

        public override bool IsOpen {
            get { return true; }
        }

        public override void Describe(StringBuilder builder) {
            builder.Append("A stone arch leading to another room.");
        }

        public override void DescribeClosed(StringBuilder builder) {
            throw new NotSupportedException();
        }
    }
}
