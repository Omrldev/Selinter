using AutoMapper;
using Contracts;
using MassTransit;
using MongoDB.Entities;
using SearchService.Dto;

namespace SearchService.Consumers
{
    public class SalesCreatedConsumer : IConsumer<SalesCreated>
    {
        private readonly IMapper _mapper;

        public SalesCreatedConsumer(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task Consume(ConsumeContext<SalesCreated> context)
        {
            Console.WriteLine("=> consuming sales created: " + context.Message.Id);

            var product = _mapper.Map<Product>(context.Message);

            await product.SaveAsync();
        }
    }
}
