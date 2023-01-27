namespace BulkyBook.DataAccess.Data
{
    public class SeedDb
    {
        private readonly ApplicationDbContext _context;
        private Random _random;

        public SeedDb(ApplicationDbContext context)
        {
            _context = context;
            _random = new Random();
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();

            if (!_context.Categories.Any())
            {
                AddProduct("Fiction", _random.Next(1, 12), new DateTime(2023, _random.Next(1, 12), _random.Next(1, 9)));
                AddProduct("Romance", _random.Next(1, 12), new DateTime(2023, _random.Next(1, 12), _random.Next(1, 9)));
                AddProduct("Action", _random.Next(1, 12), new DateTime(2023, _random.Next(1, 12), _random.Next(1, 9)));
                AddProduct("Fantasy", _random.Next(1, 12), new DateTime(2023, _random.Next(1, 12), _random.Next(1, 9)));
                await _context.SaveChangesAsync();
            }
        }

        private void AddProduct(string name, int displayOrder, DateTime createdDateTime)
        {
            _context.Categories.Add(new Models.Category
            {
                Name = name,
                DisplayOrder = displayOrder,
                CreatedDateTime = createdDateTime,
            });
        }
    }
}
