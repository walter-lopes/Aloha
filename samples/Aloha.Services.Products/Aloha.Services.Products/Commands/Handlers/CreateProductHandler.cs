
using Aloha.CQRS.Commands;
using Aloha.Persistence.MongoDB;
using Aloha.Services.Products.Domain;
using System;
using System.Threading.Tasks;

namespace Aloha.Services.Products.Commands.Handlers
{
    public class CreateProductHandler : ICommandHandler<CreateProduct>
    {
        private readonly IMongoRepository<Product, Guid> _mongoRepository;

        public CreateProductHandler(IMongoRepository<Product, Guid> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }

        public async Task HandleAsync(CreateProduct command)
        {
            await _mongoRepository.AddAsync(new Product(command.Name));
        }
    }
}
