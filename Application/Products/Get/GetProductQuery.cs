using Domain.Products;
using MediatR;

namespace Application.Products.Get;

public record GetProductQuery(ProductId ProductId) : IRequest<ProductResponse>;