using Contracts;
using MassTransit;
using MongoDB.Entities;
using SearchService.Dto;

namespace SearchService.Consumers
{
    public class SalesDeletedConsumer : IConsumer<SalesDeleted>
    {
        public async Task Consume(ConsumeContext<SalesDeleted> context)
        {
            Console.WriteLine("--> Consuming the sales-deleted");

            var result = await DB.DeleteAsync<Product>(context.Message.Id);

            if(!result.IsAcknowledged)
            {
                throw new MessageException(typeof(SalesDeleted), "*** Problem to delete ***");
            }
        }
    }
}
