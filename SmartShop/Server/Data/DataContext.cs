namespace SmartShop.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Title = "The First Book",
                    Description = "The is a test for the first book",
                    ImageUrl = "https://th.bing.com/th/id/R.6d1e757f6ae90bd14e92c28f53507b68?rik=wU%2f%2bO0hTLOuLLA&riu=http%3a%2f%2fsteve-lovelace.com%2fwordpress%2fwp-content%2fuploads%2f2012%2f06%2fwikipedia-book-cover.png&ehk=gsHeCrgqoz9gCkutL0MeqjMwsMKHcVR3gZ1rwT4W7l4%3d&risl=&pid=ImgRaw&r=0",
                    Price = 4.66m
                },
        new Product
        {
            Id = 2,
            Title = "The second Book",
            Description = "Also the test for the second book is here",
            ImageUrl = "https://th.bing.com/th/id/R.41432b68c640604fef2ca79e7415779f?rik=xisO17TB5K1yUg&riu=http%3a%2f%2fwww.fh-augsburg.de%2f%7eharsch%2fanglica%2fChronology%2f20thC%2fKipling%2fkip_juti.jpg&ehk=BhTwvGJSZUMT1wEPyG0ZPYntyh7%2fIYpFIH4E5LLRWi0%3d&risl=&pid=ImgRaw&r=0",
            Price = 8.66m
        },
        new Product
        {
            Id = 3,
            Title = "The third Book",
            Description = "Third book will have the test here ",
            ImageUrl = "https://th.bing.com/th/id/OIP._ZquNBZkvqxJZTt7srYcwAHaJY?pid=ImgDet&rs=1",
            Price = 6.66m
        }
                );

        }

        public DbSet<Product> Products { get; set; }
    }
}
