using Application.Data;
using Domain.Products;
using MediatR;

namespace Application.Products.Create;

internal class CreateProductCommandHandler : IRequestHandler<CreateProductCommand>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var product = new Product(
            new ProductId(Guid.NewGuid()),
            command.Name,
            new Money(command.Currency, command.Amount),
            Sku.Create(command.Sku)!); //TODO Check that create method does not return null value
        
        _productRepository.Add(product);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}