using MediatR;

namespace MyMicroservice.Application.Features.Products.Queries.GetProductById;

public class GetProductByIdQuery : IRequest<ProductDto?>
{
    public Guid Id { get; set; }

    public GetProductByIdQuery(Guid id)
    {
        Id = id;
    }
}