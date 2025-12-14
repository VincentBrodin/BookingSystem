using BookingSystem.Core.Data;
using BookingSystem.Core.Models;
using BookingSystem.Core.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BookingSystem.Cli;

static class Program
{
    static async Task Main(string[] args)
    {
        var config = new ConfigurationBuilder().AddEnvironmentVariables().Build();
        var connectionString = config.GetConnectionString("DefaultConnection") ?? throw new Exception("Missing Connection String");

        var services = new ServiceCollection();
        services.AddDbContext<BookingDbContext>(options => options.UseSqlServer(connectionString));
        services.AddScoped<BookingService>();

        var provider = services.BuildServiceProvider();
        var bookingService = provider.GetService<BookingService>() ?? throw new Exception("Failed to get booking service");


        var running = true;
        do
        {
            Console.WriteLine("(1) Add room");
            Console.WriteLine("(2) Show rooms");
            Console.WriteLine("(3) Create booking");
            Console.WriteLine("(4) Show bookings");
            Console.WriteLine("(5) Exit");
            var input = GetIntInput("Select", 1, 5);
            switch (input)
            {
                case 1:
                    await AddRoom(bookingService);
                    break;
                case 2:
                    await ShowRooms(bookingService);
                    break;
                case 3:
                    await CreateBooking(bookingService);
                    break;
                case 4:
                    break;
                case 5:
                    running = false;
                    break;
                default:
                    Console.WriteLine("UNKOWN INPUT");
                    break;
            }
        } while (running);

    }

    static async Task AddRoom(BookingService service)
    {
        var name = GetStringInput("Room name");
        var room = await service.CreateRoom(name);
        Console.WriteLine($"Added: {room}");
        WaitForInput();
    }

    static async Task ShowRooms(BookingService service)
    {
        var rooms = await service.GetRooms();
        if (rooms.Count == 0)
        {
            Console.WriteLine("No rooms to show :(");
        }
        else
        {
            Console.WriteLine($"Found {rooms.Count} rooms:");
            foreach (var room in rooms)
            {
                Console.WriteLine($"| {room}");
                foreach (var booking in room.Bookings)
                {
                    Console.WriteLine($"| | {booking}");
                }
            }
        }
        WaitForInput();
    }


    static async Task CreateBooking(BookingService service)
    {
        var rooms = await service.GetRooms();
        for (var i = 0; i < rooms.Count; i++)
        {
            var room = rooms[i];
            Console.WriteLine($"({i + 1}) {room}:");
            foreach (var booking in room.Bookings)
            {
                Console.WriteLine($"|   {booking}");
            }
        }
        var index = GetIntInput("Choose room to book", 1, rooms.Count) - 1;
        var choosenRoom = rooms[index];
        while (true)
        {
            var now = DateTime.Now;
            var fromDate = GetDateInput("from");
            if (now > fromDate)
            {
                Console.WriteLine("You don't have a time machine :(");
            }
            var toDate = GetDateInput("to");
            if (now > toDate)
            {
                Console.WriteLine("You don't have a time machine :(");
            }
            if (choosenRoom.IsAvailable(fromDate, toDate))
            {
                var user = GetStringInput("user id");
                var booking = await service.CreateBooking(choosenRoom, user, fromDate, toDate);
                Console.WriteLine($"You have now booked: {booking}");
                break;
            }
            else
            {
                Console.WriteLine("Room is booked that time");
            }
        }
        WaitForInput();
    }

    static int GetIntInput(string query, int min, int max)
    {
        while (true)
        {
            Console.Write($"{query} ({min}-{max}): ");
            var input = Console.ReadLine() ?? string.Empty;

            if (int.TryParse(input, out var result) && min <= result && result <= max)
            {
                return result;
            }
        }

    }

    static string GetStringInput(string query)
    {
        while (true)
        {
            Console.Write($"{query}: ");
            var input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input))
            {
                return input;
            }

        }

    }

    static DateTime GetDateInput(string query)
    {
        while (true)
        {
            Console.Write($"{query}: ");
            var input = Console.ReadLine();
            if (!string.IsNullOrEmpty(input) && DateTime.TryParse(input, out var result))
            {
                return result;
            }

        }
    }

    static void WaitForInput()
    {
        Console.Write("Press enter to continue: ");
        Console.ReadLine();
    }
}
