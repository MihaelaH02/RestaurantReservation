var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<Projects.RestaurantReservation>("restaurantreservation");

builder.Build().Run();
