using Application.Products.Create;
using Application.Products.Delete;
using Application.Products.Get;
using Application.Products.Update;
using Domain.Products;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace eSop.Api.Endpoints;

public class Products
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("products", async (CreateProductCommand command, ISender sender) =>
        {
            await sender.Send(command);
            return Results.Ok();
        });

        app.MapDelete("products/{id:guid}", async (Guid id, ISender sender) =>
        {
            try
            {
                await sender.Send(new DeleteProductCommand(new ProductId(id)));
                return Results.NoContent();
            }
            catch (ProductNotFoundException ex)
            {
                return Results.NotFound(ex.Message);
            }
        });

        app.MapPut("products/{id:guid}", async (Guid id, [FromBody] UpdateProductRequest request, ISender sender) =>
        {
            try
            {
                var command = new UpdateProductCommand(
                    new ProductId(id),
                    request.Name,
                    request.Sku,
                    request.Currency,
                    request.Amount);

                await sender.Send(command);
                return Results.NoContent();
            }
            catch (ProductNotFoundException ex)
            {
                return Results.NotFound(ex.Message);
            }
        });

        app.MapGet("products/{id:guid}", async (Guid id, ISender sender) =>
        {
            try
            {
                return Results.Ok(await sender.Send(new GetProductQuery(new ProductId(id))));
            }
            catch (ProductNotFoundException ex)
            {
                return Results.NotFound(ex.Message);
            }
        });
    }
}