using RguHackTextAdventure.Core;
using RguHackTextAdventure.Core.Generators;
using RguHackTextAdventure.Core.Rooms;
using System;
using System.Text;

namespace RguHackTextAdventure.Client {
    internal class Program {
        public static void Main(string[] args) {
            // Create a random generator.
            Random random = new Random();

            // Generate a map.
            MapGenerator generator = new MapGenerator(5, 5, random);
            RoomBase startRoom = generator.Generate();

            // Prepare the game controller.
            Controller controller = new Controller(startRoom, 100, 3);

            StringBuilder builder = new StringBuilder();
            controller.DescribeRoom(builder);
            Console.Write(builder.ToString());

            // Main game loop.
            while (true) {
                string command = Console.ReadLine();

                command = command.Trim().ToLowerInvariant();

                builder = new StringBuilder();
                controller.ExecuteCommand(builder, command);
                Console.Write(builder.ToString());
            }
        }
    }
}
