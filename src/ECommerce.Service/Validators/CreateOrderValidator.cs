using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Dto;
using ECommerce.Entity;
using ECommerce.UnitOfWork;
using FluentValidation;

namespace ECommerce.Service
{
    public class CreateOrderValidator : AbstractValidator<OrderDto>
    {
        private readonly IUnitOfWork _uow;
        private readonly Product _product;
        private readonly int _quantity;

        public CreateOrderValidator(IUnitOfWork uow, Product product, int quantity)
        {
            _uow = uow;
            _product = product;
            _quantity = quantity;

            RuleFor(c => c.ProductCode).NotNull().WithMessage("Product Code is required.");
            RuleFor(c => c.ProductCode).Must(code => ExistProduct()).WithMessage("Product not found!");
            RuleFor(c => c.ProductCode).Must(code => CheckStock()).WithMessage("Product stock is insufficient!");
            RuleFor(c => c.Quantity).NotNull().WithMessage("Quantity must be greater than 0.");
        }

        private bool ExistProduct()
        {
            return _product != null;
        }

        private bool CheckStock()
        {
            if (_product == null) return false;

            var totalStock = _product.Stock;
            var currentSalesCount = _uow.OrderRepository.Find(x => x.ProductId == _product.Id).Sum(x => x.Quantity);

            return currentSalesCount + _quantity <= totalStock;
        }
    }
}