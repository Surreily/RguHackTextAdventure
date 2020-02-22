﻿using RguHackTextAdventure.Core.Items;
using RguHackTextAdventure.Core.RoomLinker;
using RguHackTextAdventure.Core.RoomLinker.Doors;
using RguHackTextAdventure.Core.Rooms;
using RguHackTextAdventure.Core.Rooms.Dungeon;
using System;
using System.Collections.Generic;

namespace RguHackTextAdventure.Core.Generators {
    public class MapGenerator {
        private bool _hasBeenGenerated;
        private Random _random;

        private int _mapWidth;
        private int _mapHeight;

        private RoomBase[,] _rooms;
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
            _rooms = new RoomBase[_mapWidth, _mapHeight];

            // Create a start room.
            int startRoomX = _random.Next(1, _mapWidth - 2);
            int startRoomY = _random.Next(1, _mapHeight - 2);

            RoomBase startRoom = new DungeonRoom();

            _rooms[startRoomX, startRoomY] = startRoom;

            for (int i = 0; i < 4; i++) {
                AddRoom(startRoomX, startRoomY);
            }

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
            if (_rooms[newRoomX, newRoomY] != null) {
                return;
            }

            // Get the current room.
            RoomBase currentRoom = _rooms[currentRoomX, currentRoomY];

            // Create a room.
            RoomBase newRoom = new DungeonRoom();
            _rooms[newRoomX, newRoomY] = newRoom;

            // Create a room linker.
            RoomLinkerBase newRoomLinker = new StoneArchRoomLinker {
                SourceRoom = currentRoom,
                DestinationRoom = newRoom,
            };

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
            // TODO: Add items.

            // Potentially add more rooms.
            if (_random.Next(0, 12) == 0) {
                return;
            }

            int roomsToAdd = _random.Next(1, 3);

            for (int i = 0; i < roomsToAdd; i++) {
                AddRoom(newRoomX, newRoomY);
            }
        }
    }
}
