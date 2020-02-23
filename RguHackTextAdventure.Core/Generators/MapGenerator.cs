using RguHackTextAdventure.Core.Items;
using RguHackTextAdventure.Core.Items.Consumables;
using RguHackTextAdventure.Core.Items.Keys;
using RguHackTextAdventure.Core.RoomLinker;
using RguHackTextAdventure.Core.RoomLinker.Doors;
using RguHackTextAdventure.Core.Rooms;
using RguHackTextAdventure.Core.Rooms.Dungeon;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RguHackTextAdventure.Core.Generators {
    public class MapGenerator {
        private bool _hasBeenGenerated;
        private Random _random;

        private int _mapWidth;
        private int _mapHeight;

        private RoomBase[,] _roomsGrid;
        private List<RoomBase> _rooms;
        private List<ItemBase> _availableItems;

        public MapGenerator(int mapWidth, int mapHeight, Random random) {
            _hasBeenGenerated = false;

            _mapWidth = mapWidth;
            _mapHeight = mapHeight;
            _random = random;
        }

        public RoomBase Generate() {
            // Ensure this is the first time we are creating the map.
            if (_hasBeenGenerated) {
                throw new InvalidOperationException("Map has already been generated");
            }

            // Ensure width and height are valid.
            if (_mapWidth < 3) {
                throw new ArgumentException("mapWidth must be 3 or greater");
            }

            if (_mapHeight < 3) {
                throw new ArgumentException("mapHeight must be 3 or greater");
            }

            // Prepare the grid of rooms.
            _roomsGrid = new RoomBase[_mapWidth, _mapHeight];
            _rooms = new List<RoomBase>();
            _availableItems = new List<ItemBase>();

            // Create a start room.
            int startRoomX = _random.Next(1, _mapWidth - 2);
            int startRoomY = _random.Next(1, _mapHeight - 2);

            RoomBase startRoom = new DungeonRoom();

            _roomsGrid[startRoomX, startRoomY] = startRoom;

            for (int i = 0; i < 4; i++) {
                AddRoom(startRoomX, startRoomY);
            }

            // Add the LEGO set (win condition) to a random room.
            _rooms[_random.Next(0, _rooms.Count - 1)].Items.Add(new LegoSet());

            return startRoom;
        }

        private void AddRoom(int currentRoomX, int currentRoomY) {
            // Pick a random direction.
            int direction = _random.Next(0, 3);

            int newRoomX = currentRoomX;
            int newRoomY = currentRoomY;

            switch (direction) {
                case 0:
                    newRoomY--;
                    break;
                case 1:
                    newRoomX++;
                    break;
                case 2:
                    newRoomY++;
                    break;
                case 3:
                    newRoomX--;
                    break;
            }

            // Ensure we don't escape the game world (outside of the array).
            if (newRoomX < 0 || newRoomX >= _mapWidth || newRoomY < 0 || newRoomY >= _mapHeight) {
                return;
            }

            // Only create a room there if there is space.
            if (_roomsGrid[newRoomX, newRoomY] != null) {
                return;
            }

            // Get the current room.
            RoomBase currentRoom = _roomsGrid[currentRoomX, currentRoomY];

            // Create a room.
            RoomBase newRoom = new DungeonRoom();
            _roomsGrid[newRoomX, newRoomY] = newRoom;
            _rooms.Add(newRoom);

            // Create a room linker.
            RoomLinkerBase newRoomLinker;

            if (_random.Next(0, 3) == 0 && _availableItems.Any(i => i is SmallKeyItem)) {
                newRoomLinker = new SteelDoorRoomLinker() {
                    SourceRoom = currentRoom,
                    DestinationRoom = newRoom,
                };
            } else {
                newRoomLinker = new StoneArchRoomLinker {
                    SourceRoom = currentRoom,
                    DestinationRoom = newRoom,
                };
            }

            switch (direction) {
                case 0:
                    currentRoom.NorthRoomLinker = newRoomLinker;
                    newRoom.SouthRoomLinker = newRoomLinker;
                    break;
                case 1:
                    currentRoom.EastRoomLinker = newRoomLinker;
                    newRoom.WestRoomLinker = newRoomLinker;
                    break;
                case 2:
                    currentRoom.SouthRoomLinker = newRoomLinker;
                    newRoom.NorthRoomLinker = newRoomLinker;
                    break;
                case 3:
                    currentRoom.WestRoomLinker = newRoomLinker;
                    newRoom.EastRoomLinker = newRoomLinker;
                    break;
            }

            // Add items to the room.
            AddItemsToRoom(newRoom);

            // Potentially add more rooms.
            if (_random.Next(0, 12) == 0) {
                return;
            }

            int roomsToAdd = _random.Next(1, 8);

            for (int i = 0; i < roomsToAdd; i++) {
                AddRoom(newRoomX, newRoomY);
            }
        }

        private void AddItemsToRoom(RoomBase room) {
            // Keys.
            if (_random.Next(0, 3) == 0) {
                int keyLevel = 0;

                for (int i = 0; i < 2; i++) {
                    if (_random.Next(0, 5) == 0) {
                        keyLevel++;
                    }
                }

                ItemBase item = new SmallKeyItem(keyLevel);
                room.Items.Add(item);
                _availableItems.Add(item);
            }

            // MEGABLOKS sets.
            if (_random.Next(0, 8) == 0) {
                ItemBase item = new MegaBloksSet();
                room.Items.Add(item);
                _availableItems.Add(item);
            }
        }
    }
}
