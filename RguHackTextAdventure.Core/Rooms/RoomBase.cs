using RguHackTextAdventure.Core.Items;
using RguHackTextAdventure.Core.RoomLinker;
using System.Collections.Generic;
using System.Text;

namespace RguHackTextAdventure.Core.Rooms {
    public abstract class RoomBase {
        private readonly List<ItemBase> _items;

        private bool _hasBeenDesrcibed;

        public RoomBase()
        {
            _hasBeenDesrcibed = false;
            _items = new List<ItemBase>();

        }

        public RoomLinkerBase NorthRoomLinker { get; set; }
        public RoomLinkerBase EastRoomLinker { get; set; }
        public RoomLinkerBase SouthRoomLinker { get; set; }
        public RoomLinkerBase WestRoomLinker { get; set; }

        public List<ItemBase> Items {
            get { return _items; }
        }

        public void Describe(StringBuilder builder)
        {
            DescribeRoom(builder);
            DescribeRoomLinkers(builder);
            DescribeItems(builder);
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

        public virtual void DescribeItems(StringBuilder builder) {
            if (Items.Count == 0) {
                builder.AppendLine("There are no items in this room.");
                return;
            }

            builder.AppendLine("You can see the following in this room: ");

            foreach (ItemBase item in Items) {
                builder.Append(" - ");
                builder.Append(item.Name);
                builder.AppendLine();
            }
        }
    }
}
