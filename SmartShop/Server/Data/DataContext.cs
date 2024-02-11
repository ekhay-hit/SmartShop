namespace SmartShop.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(

               new Category
               {
                   Id = 1,
                   Name = "Books",
                   Url = "Books"
               },
               new Category { 
                   Id = 2,      
                   Name= "Movies",
                   Url = "Movies"
               },

               new Category { 
                   Id = 3,
                   Name="Video Games",
                   Url ="Video-games"
               }
                      

               );



            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Title = "Pilgrim's Progress",
                    Description = "The English novel begins behind bars, in extremis. Its first author, John Bunyan, was a Puritan dissenter whose writing starts with sermons and ends with fiction. His famous allegory, the story of Christian, opens with a sentence of luminous simplicity that has the haunting compulsion of the hook in a great melody. \"As I walk'd through the wilderness of this world, I lighted on a certain place, where was a Denn; And I laid me down in that place to sleep: And as I slept I dreamed a Dream.",
                    ImageUrl = "https://i.guim.co.uk/img/static/sys-images/Observer/Columnist/Columnists/2013/9/17/1379432188768/John-Bunyan-010.jpg?width=620&dpr=1&s=none",
                    Price = 4.66m,
                    CategoryId= 1
                },
        new Product
        {
            Id = 2,
            Title = "The second Book",
            Description = "Also the test for the second book is here",
            ImageUrl = "https://th.bing.com/th/id/R.41432b68c640604fef2ca79e7415779f?rik=xisO17TB5K1yUg&riu=http%3a%2f%2fwww.fh-augsburg.de%2f%7eharsch%2fanglica%2fChronology%2f20thC%2fKipling%2fkip_juti.jpg&ehk=BhTwvGJSZUMT1wEPyG0ZPYntyh7%2fIYpFIH4E5LLRWi0%3d&risl=&pid=ImgRaw&r=0",
            Price = 8.66m,
            CategoryId= 1
        },
        new Product
        {
            Id = 3,
            Title = "The third Book",
            Description = "Third book will have the test here ",
            ImageUrl = "https://th.bing.com/th/id/OIP._ZquNBZkvqxJZTt7srYcwAHaJY?pid=ImgDet&rs=1",
            Price = 6.66m,
            CategoryId= 1

        }
                );

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
    }
}
