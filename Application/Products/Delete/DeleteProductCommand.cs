using Domain.Products;
using MediatR;

namespace Application.Products.Delete;

public record DeleteProductCommand(ProductId Id) : IRequest;
