using Microsoft.EntityFrameworkCore;
using SalesService.Data.DbContexts;
using SalesService.Enitities;

namespace SalesService.Data
{
    public class DbInitializer
    {
        public static void InitDb(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            
            SeedData(scope.ServiceProvider.GetService<SalesDbContext>());
        }

        private static void SeedData(SalesDbContext context)
        {
            context.Database.Migrate();

            if(context.Sales.Any())
            {
                Console.WriteLine("*** Ya existen datos ***");
                return;
            }

            var sales = new List<Sale>()
            {
                // 1
                new Sale
                {
                    Id = Guid.Parse("afbee524-5972-4075-8800-7d1f9d7b0a0c"),
                    Status = Status.Live,
                    Seller = "jose",
                    Price = 10,
                    Product = new Product
                    {
                        Title = "training",
                        Description = "ford blanco",
                        Category = "Mujer",
                        Brand = "Nike",
                        Quality = "Nuevo con etiqueta",
                        Image = "https://cdn.pixabay.com/photo/2016/05/06/16/32/car-1376190_960_720.jpg"
                    }
                },
                 // 2
                new Sale
                {
                    Id = Guid.Parse("c8c3ec17-01bf-49db-82aa-1ef80b833a9f"),
                    Status = Status.Live,
                    Seller = "omar",
                    Price = 20,
                    Product = new Product
                    {
                        Title = "training",
                        Description = "bugatti negro",
                        Category = "Hombre",
                        Brand = "Nike",
                        Quality = "Nuevo sin etiqueta",
                        Image = "https://cdn.pixabay.com/photo/2012/05/29/00/43/car-49278_960_720.jpg"
                    }
                },
                // 3
                new Sale
                {
                    Id = Guid.Parse("bbab4d5a-8565-48b1-9450-5ac2a5c4a654"),
                    Status = Status.Live,
                    Seller = "ledesma",
                    Price = 30,
                    Product = new Product
                    {
                        Title = "futbol",
                        Description = "ford negro",
                        Category = "Niños",
                        Brand = "Adidas",
                        Quality = "Nuevo sin etiqueta",
                        Image = "https://cdn.pixabay.com/photo/2012/11/02/13/02/car-63930_960_720.jpg"
                    }
                },
                // 4
                new Sale
                {
                    Id = Guid.Parse("155225c1-4448-4066-9886-6786536e05ea"),
                    Status = Status.Live,
                    Seller = "aguilar",
                    Price = 40,
                    Product = new Product
                    {
                        Title = "cycling",
                        Description = "mercedes gris",
                        Category = "Mujer",
                        Brand = "under armour",
                        Quality = "Muy bueno",
                        Image = "https://cdn.pixabay.com/photo/2016/04/17/22/10/mercedes-benz-1335674_960_720.png"
                    }
                },
                 // 5
                new Sale
                {
                    Id = Guid.Parse("466e4744-4dc5-4987-aae0-b621acfc5e39"),
                    Status = Status.Live,
                    Seller = "iris",
                    Price = 50,
                    Product = new Product
                    {
                        Title = "gym",
                        Description = "bmw blanco",
                        Category = "Hombre",
                        Brand = "Under Armour",
                        Quality = "Bueno",
                        Image = "https://cdn.pixabay.com/photo/2017/08/31/05/47/bmw-2699538_960_720.jpg"
                    }
                },
                 // 6
                new Sale
                {
                    Id = Guid.Parse("dc1e4071-d19d-459b-b848-b5c3cd3d151f"),
                    Status = Status.Live,
                    Seller = "bermejo",
                    Price = 60,
                    Product = new Product
                    {
                        Title = "bike",
                        Description = "ferrari rojo",
                        Category = "Niños",
                        Brand = "Nike",
                        Quality = "Nuevo con etiqueta",
                        Image = "https://cdn.pixabay.com/photo/2017/11/09/01/49/ferrari-458-spider-2932191_960_720.jpg"
                    }
                },
                // 7
                new Sale
                {
                    Id = Guid.Parse("47111973-d176-4feb-848d-0ea22641c31a"),
                    Status = Status.Live,
                    Seller = "noelia",
                    Price = 70,
                    Product = new Product
                    {
                        Title = "golf",
                        Description = "ferrari rojo",
                        Category = "Mujer",
                        Brand = "Under Armour",
                        Quality = "Nuevo sin etiqueta",
                        Image = "https://cdn.pixabay.com/photo/2017/11/08/14/39/ferrari-f430-2930661_960_720.jpg"
                    }
                },
                // 8
                new Sale
                {
                    Id = Guid.Parse("6a5011a1-fe1f-47df-9a32-b5346b289391"),
                    Status = Status.Live,
                    Seller = "chris",
                    Price = 80,
                    Product = new Product
                    {
                        Title = "tennis",
                        Description = "audi blanco",
                        Category = "hombre",
                        Brand = "adidas",
                        Quality = "muy bueno",
                        Image = "https://cdn.pixabay.com/photo/2019/12/26/20/50/audi-r8-4721217_960_720.jpg"
                    }
                },
                // 9
                new Sale
                {
                    Id = Guid.Parse("40490065-dac7-46b6-acc4-df507e0d6570"),
                    Status = Status.Live,
                    Seller = "charito",
                    Price = 90,
                    Product = new Product
                    {
                        Title = "soccer",
                        Description = "audi negro",
                        Category = "niños",
                        Brand = "nike",
                        Quality = "bueno",
                        Image = "https://cdn.pixabay.com/photo/2016/09/01/15/06/audi-1636320_960_720.jpg"
                    }
                },
                // 10
                new Sale
                {
                    Id = Guid.Parse("3659ac24-29dd-407a-81f5-ecfe6f924b9b"),
                    Status = Status.Live,
                    Seller = "carlos",
                    Price = 100,
                    Product = new Product
                    {
                        Title = "baloncesto",
                        Description = "ford naranja",
                        Category = "mujer",
                        Brand = "under armour",
                        Quality = "bueno",
                        Image = "https://cdn.pixabay.com/photo/2016/09/01/15/06/audi-1636320_960_720.jpg"
                    }
                },
            };

            context.AddRange(sales);

            context.SaveChanges();
        }
    }
}
