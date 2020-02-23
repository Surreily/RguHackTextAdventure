﻿using RguHackTextAdventure.Core.Items;
using RguHackTextAdventure.Core.RoomLinker;
using RguHackTextAdventure.Core.Rooms;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RguHackTextAdventure.Core {
    public class Controller
    {
        private RoomBase _currentRoom;
        private int _health;
        private int _money;
        private List<ItemBase> _inventory;

        public Controller(RoomBase currentRoom, int health, int money) {
            _currentRoom = currentRoom;
            _health = health;
            _money = money;
            _inventory = new List<ItemBase>();
        }

        public void DescribeRoom(StringBuilder builder) {
            _currentRoom.Describe(builder);
        }

        public void ExecuteCommand(StringBuilder builder, string command) {
            // Command shortcuts
            if (command == "n") {
                GoNorth(builder);
                return;
            }

            if (command == "e") {
                GoEast(builder);
                return;
            }

            if (command == "s") {
                GoSouth(builder);
                return;
            }

            if (command == "w") {
                GoWest(builder);
                return;
            }

            if (command == "l") {
                Look(builder);
                return;
            }

            if (command == "i") {
                LookAtInventory(builder);
            }

            // Multi-part commands.
            string[] commandParts = command.Split(' ');

            if (commandParts[0] == "get" && commandParts.Length == 2) {
                GetItem(builder, commandParts[1]);
            }
        }

        private void GoNorth(StringBuilder builder) {
            GoToRoom(builder, _currentRoom.NorthRoomLinker);
        }

        private void GoEast(StringBuilder builder) {
            GoToRoom(builder, _currentRoom.EastRoomLinker);
        }

        private void GoSouth(StringBuilder builder) {
            GoToRoom(builder, _currentRoom.SouthRoomLinker);
        }

        private void GoWest(StringBuilder builder) {
            GoToRoom(builder, _currentRoom.WestRoomLinker);
        }

        private void GoToRoom(StringBuilder builder, RoomLinkerBase roomLinker) {
            if (roomLinker == null) {
                builder.AppendLine("There is nothing here.");
                return;
            }

            if (!roomLinker.IsOpen) {
                builder.AppendLine("It's locked.");
                return;
            }

            _currentRoom = roomLinker.GetOppositeRoom(_currentRoom);
            _currentRoom.Describe(builder);
        }

        private void Look(StringBuilder builder) {
            _currentRoom.Describe(builder);
        }

        private void LookAtInventory(StringBuilder builder) {
            if (_inventory.Count == 0) {
                builder.AppendLine("You don't have anything.");
                return;
            }

            builder.AppendLine("You have the following items:");

            foreach (ItemBase item in _inventory) {
                builder.AppendLine(" - " + item.Name);
            }
        }

        private void GetItem(StringBuilder builder, string itemName) {
            ItemBase item = _currentRoom.Items
                .FirstOrDefault(i => i.Aliases.Any(a => a == itemName));

            if (item == null) {
                builder.AppendLine("You don't see a " + itemName + ".");
                return;
            }

            _currentRoom.Items.Remove(item);
            _inventory.Add(item);

            builder.AppendLine("You got the " + itemName + ".");
        }
    }
}
