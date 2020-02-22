using System.Text;

namespace RguHackTextAdventure.Core.Rooms.Dungeon {
    public class DungeonRoom : RoomBase {
        public override void DescribeRoomLong(StringBuilder builder) {
            builder.AppendLine("You are in a small, dark room.");
        }

        public override void DescribeRoomShort(StringBuilder builder) {
            builder.AppendLine("You are in a small, dark room. The walls surrounding you are made of dark, gray" +
                "bricks.");
        }
    }
}
