using RguHackTextAdventure.Core.RoomLinker;
using System.Text;

namespace RguHackTextAdventure.Core.Rooms {
    public abstract class RoomBase {
        private bool _hasBeenDesrcibed;

        public RoomBase()
        {
            _hasBeenDesrcibed = false;
        }

        public RoomLinkerBase NorthRoomLinker { get; set; }
        public RoomLinkerBase EastRoomLinker { get; set; }
        public RoomLinkerBase SouthRoomLinker { get; set; }
        public RoomLinkerBase WestRoomLinker { get; set; }

        public void Describe(StringBuilder builder)
        {
            DescribeRoom(builder);
            DescribeRoomLinkers(builder);
        }

        public virtual void DescribeRoom(StringBuilder builder) {
            if (_hasBeenDesrcibed) {
                DescribeRoomShort(builder);
            } else {
                DescribeRoomLong(builder);
                _hasBeenDesrcibed = true;
            }
        }

        public abstract void DescribeRoomShort(StringBuilder builder);

        public abstract void DescribeRoomLong(StringBuilder builder);

        public virtual void DescribeRoomLinkers(StringBuilder builder) {
            if (NorthRoomLinker != null) {
                builder.Append("To the North: ");
                NorthRoomLinker.Describe(builder);
                builder.AppendLine();
            }

            if (EastRoomLinker != null) {
                builder.Append("To the East: ");
                EastRoomLinker.Describe(builder);
                builder.AppendLine();
            }

            if (SouthRoomLinker != null) {
                builder.Append("To the South: ");
                SouthRoomLinker.Describe(builder);
                builder.AppendLine();
            }

            if (WestRoomLinker != null) {
                builder.Append("To the West: ");
                WestRoomLinker.Describe(builder);
                builder.AppendLine();
            }
        }
    }
}
