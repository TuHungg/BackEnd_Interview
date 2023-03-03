using Microsoft.EntityFrameworkCore;

namespace BackEnd_Interview.Model
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> option) : base(option)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<UserMoviePreferences> FavoriteMovies { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<UserMoviePreferences>()
                .HasKey(fm => new { fm.UserId, fm.MovieId });

            modelBuilder.Entity<UserMoviePreferences>()
                .HasOne(fm => fm.User)
                .WithMany(u => u.FavoriteMovies)
                .HasForeignKey(fm => fm.UserId);

            modelBuilder.Entity<UserMoviePreferences>()
                .HasOne(fm => fm.Movie)
                .WithMany(m => m.FavoriteMovies)
                .HasForeignKey(fm => fm.MovieId);
        }
    }
}
