using System.Text;

namespace RguHackTextAdventure.Core.RoomLinker.Doors {
    public class WoodenDoorRoomLinker : RoomLinkerBase {
        public override bool IsOpen {
            get { return false; }
        }

        public override void Describe(StringBuilder builder) {
            builder.Append("An old wooden door.");
        }

        public override void DescribeClosed(StringBuilder builder) {
            builder.Append("The door is closed.");
        }
    }
}
