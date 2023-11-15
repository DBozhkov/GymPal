using GymPalApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymPalApi.Seeders
{
    public class SupplementsSeeder
    {
        private readonly ApplicationDbContext context;

        public SupplementsSeeder(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task SeedAsync()
        {
            if (!this.context.Supplements.Any())
            {
                var list = new List<Supplement>()
                {
                    new Supplement
                    {
                      SupplementName = "GoPro HERO6 4K Action Camera - Black",
                      Price = 790.50,
                      ImageUrl = "https://bootstrap-ecommerce.com/bootstrap5-ecommerce/images/items/1.webp",
                    },
                    new Supplement
                    {
                      SupplementName = "Canon camera 20x zoom, Black color EOS 2000",
                      Price = 320.00,
                      ImageUrl = "https://bootstrap-ecommerce.com/bootstrap5-ecommerce/images/items/2.webp",
                    },
                    new Supplement
                    {
                      SupplementName = "Xiaomi Redmi 8 Original Global Version 4GB",
                      Price = 120.00,
                      ImageUrl = "https://bootstrap-ecommerce.com/bootstrap5-ecommerce/images/items/3.webp",
                    },
                    new Supplement
                    {
                      SupplementName = "Apple iPhone 12 Pro 6.1 RAM 6GB 512GB Unlocked",
                      Price = 450.00,
                      ImageUrl = "https://bootstrap-ecommerce.com/bootstrap5-ecommerce/images/items/4.webp",
                    },
                    new Supplement
                    {
                      SupplementName = "Apple Watch Series 1 Sport Case 38mm Black",
                      Price = 220.00,
                      ImageUrl = "https://bootstrap-ecommerce.com/bootstrap5-ecommerce/images/items/5.webp",
                    },
                    new Supplement
                    {
                      SupplementName = "T-shirts with multiple colors, for men and lady",
                      Price = 650.00,
                      ImageUrl = "https://bootstrap-ecommerce.com/bootstrap5-ecommerce/images/items/6.webp",
                    },
                    new Supplement
                    {
                      SupplementName = "Gaming Headset 32db Blackbuilt in mic",
                      Price = 340.00,
                      ImageUrl = "https://bootstrap-ecommerce.com/bootstrap5-ecommerce/images/items/7.webp",
                    },
                    new Supplement
                    {
                      SupplementName = "T-shirts with multiple colors, for men and lady",
                      Price = 250.00,
                      ImageUrl = "https://bootstrap-ecommerce.com/bootstrap5-ecommerce/images/items/8.webp",
                    },
                };

                await this.context.Supplements.AddRangeAsync(list);
                await this.context.SaveChangesAsync();
            }
        }
    }
}
