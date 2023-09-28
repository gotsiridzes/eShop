using Application.Data;
using Domain.Products;
using MediatR;

namespace Application.Products.Delete;

internal class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand>
{
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteProductCommandHandler(IProductRepository productRepository, IUnitOfWork unitOfWork)
    {
        _productRepository = productRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        var product = await _productRepository.GetByIdAsync(command.Id);

        if (product == null)
            throw new ProductNotFoundException(command.Id);
        
        _productRepository.Remove(product);
        
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}