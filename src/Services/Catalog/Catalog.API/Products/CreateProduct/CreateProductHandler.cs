
using FluentValidation;
using JasperFx.Core;

namespace Catalog.API.Products.CreateProduct
{
    /// <summary>
    /// Create product Command definition
    /// </summary>
    /// <param name="Name"></param>
    /// <param name="Category"></param>
    /// <param name="Description"></param>
    /// <param name="ImageFile"></param>
    /// <param name="Price"></param>
    public record CreateProductCommand(string Name, List<string> Category, string Description, string ImageFile, decimal Price)
        : ICommand<CreateProductResult>;

    /// <summary>
    /// Create product Command response
    /// </summary>
    /// <param name="Id"></param>
    public record CreateProductResult(Guid Id);

    /// <summary>
    /// CreateProductCommandValidator
    /// </summary>
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Category).NotEmpty().WithMessage("Category is required");
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("ImageFile is required");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than 0");
        }
    }


    /// <summary>
    /// Create product CommandHandler
    /// </summary>
    /// <param name="session" type="IDocumentSession"></param>
    internal class CreateProductCommandHandler(IDocumentSession session)
        : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            // Business logic to crate a product
            //1. Create Product entity
            var product = new Product
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price

            };

            //2. Save to DB
            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);

            //3. return CreateProductResult
            return new CreateProductResult(product.Id);


        }
    }
}
