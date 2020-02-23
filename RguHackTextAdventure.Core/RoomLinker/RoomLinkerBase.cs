using RguHackTextAdventure.Core.Items;
using RguHackTextAdventure.Core.Rooms;
using System;
using System.Text;

namespace RguHackTextAdventure.Core.RoomLinker {
    public abstract class RoomLinkerBase {
        public RoomBase SourceRoom { get; set; }
        public RoomBase DestinationRoom { get; set; }

        public abstract bool IsOpen { get; }

        public abstract void Describe(StringBuilder builder);
        public abstract void DescribeClosed(StringBuilder builder);
        
        public virtual bool UseItem(StringBuilder builder, ItemBase item) {
            builder.Append("You try to use the " + item.Name + ", but nothing happens.");
            return false;
        }

        public RoomBase GetOppositeRoom(RoomBase room) {
            if (SourceRoom == room && DestinationRoom != room) {
                return DestinationRoom;
            }

            if (DestinationRoom == room && SourceRoom != room) {
                return SourceRoom;
            }

            throw new ArgumentException("Given room is not a part of this room linker");
        }
    }
}
