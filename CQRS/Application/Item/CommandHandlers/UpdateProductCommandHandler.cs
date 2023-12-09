using CQRS.Application.Item.Commands;
using CQRS.Domain.Repository;
using MediatR;
using MongoDB.Bson;


namespace CQRS.Application.Item.CommandHandlers
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand>
    {
        private readonly IProductRepository _productRepository;
        public UpdateProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {

            var newId = new ObjectId(request.Id);

            await _productRepository.UpdateProductAsync(newId, request.Name, request.Description);
        }
    }
}
