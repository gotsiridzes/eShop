using MediatR;

namespace Application.Products.Update;
public record UpdateProductRequest(
    string Name,
    string Sku,
    string Currency,
    decimal Amount) : IRequest;
