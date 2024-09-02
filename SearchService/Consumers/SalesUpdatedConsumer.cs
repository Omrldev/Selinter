using AutoMapper;
using Contracts;
using MassTransit;
using MongoDB.Entities;
using SearchService.Dto;

namespace SearchService.Consumers
{
    public class SalesUpdatedConsumer : IConsumer<SalesUpdated>
    {
        private readonly IMapper _mapper;
        public SalesUpdatedConsumer(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task Consume(ConsumeContext<SalesUpdated> context)
        {
            Console.WriteLine("--> Consuming sales Updated" + context.Message.Id);

            var product = _mapper.Map<Product>(context.Message);

            var result = await DB.Update<Product>()
                .Match(x => x.ID == context.Message.Id)
                .ModifyOnly(x => new
                {
                    x.Title,
                    x.Description,
                    x.Brand,
                    x.Quality,
                    x.Category,
                }, product)
                .ExecuteAsync();

            if(!result.IsAcknowledged)
            {
                throw new MessageException(typeof(SalesCreated), "Problem to update MongoDB");
            }
        }
    }
}
