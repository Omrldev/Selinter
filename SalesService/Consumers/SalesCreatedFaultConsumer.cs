using Contracts;
using MassTransit;

namespace SalesService.Consumers
{
    public class SalesCreatedFaultConsumer : IConsumer<Fault<SalesCreated>>
    {
        public async Task Consume(ConsumeContext<Fault<SalesCreated>> context)
        {
            Console.WriteLine("--> Consuming the fault exception created");

            var exception = context.Message.Exceptions.First();

            if (exception.ExceptionType == "System.ArgumentException")
            {
                context.Message.Message.Brand = "validBrand";

                await context.Publish(context.Message.Message);
            }
            else
            {
                Console.WriteLine("Not argument exception was found");
            }
        }
    }
}
