using System.Collections.Generic;
using System.Text;
using RguHackTextAdventure.Core.Items;
using RguHackTextAdventure.Core.Items.Keys;

namespace RguHackTextAdventure.Core.RoomLinker.Doors {
    public class SteelDoorRoomLinker : RoomLinkerBase {
        private bool _isOpen;

        public SteelDoorRoomLinker() {
            _isOpen = false;
        }

        public override List<string> Aliases {
            get { return new List<string> { "door", "lock", "keyhole" }; }
        }

        public override bool IsOpen {
            get { return _isOpen; }
        }

        public override void Describe(StringBuilder builder) {
            if (_isOpen) {
                builder.Append("An open doorway.");
            } else {
                builder.Append("A heavy steel door with a small keyhole.");
            }
        }

        public override void DescribeClosed(StringBuilder builder) {
            builder.Append("You cannot open this door, but there is a small keyhole...");
        }

        public override bool UseItem(StringBuilder builder, ItemBase item) {
            if (item is SmallKeyItem smallKeyItem) {
                builder.AppendLine("You insert the small key into the keyhole and turn.");
                builder.AppendLine("*Click* The door unlocks and swings open!");
                _isOpen = true;
                return true;
            }

            return base.UseItem(builder, item);
        }
    }
}
