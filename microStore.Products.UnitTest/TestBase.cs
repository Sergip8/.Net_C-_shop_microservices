

using AutoMapper;
using microStore.Services.ProductApi;

namespace microStore.Products.UnitTest
{
    public class TestBase
    {
        protected static IMapper BuildMapper()
        {
            var config = new MapperConfiguration(options =>
            {
                options.AddProfile(new ProductMappingProfile());
            });

            return config.CreateMapper();
        }
        protected static Product CreateProductFixture()
        {
            Fixture fixture = new Fixture();
            fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var product = fixture.Create<Product>();
            product.Id = 3;

            return product;
        }

    }
}
