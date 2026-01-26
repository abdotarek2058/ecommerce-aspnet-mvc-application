using eTickets.Data.Enums;
using eTickets.Models;
using Microsoft.EntityFrameworkCore;

namespace eTickets.Data
{
    public class AppDbIntializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                context.Database.EnsureCreated();

                //context.Actors_Movies.RemoveRange(context.Actors_Movies);
                //context.Movies.RemoveRange(context.Movies);
                //context.SaveChanges();
                //context.Actors.RemoveRange(context.Actors);
                //context.Producers.RemoveRange(context.Producers);
                //context.Cinemas.RemoveRange(context.Cinemas);
                //context.SaveChanges();

                //context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Actors', RESEED, 0)");
                //context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Cinemas', RESEED, 0)");
                //context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Producers', RESEED, 0)");
                //context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Movies', RESEED, 0)");




                //Cinema
                if (!context.Cinemas.Any())
                {

                    context.Cinemas.AddRange(new List<Cinema>()
                    {
                        new Cinema()
                        {
                            Name = "Cinema 1",
                            Logo = "/Images/Cinemas/cinema-1.jpg",
                            Description = "This is the description of the first cinema"
                        },
                        new Cinema()
                        {
                            Name = "Cinema 2",
                            Logo = "/Images/Cinemas/cinema-2.jpg",
                            Description = "This is the description of the second cinema"
                        },
                        new Cinema()
                        {
                            Name = "Cinema 3",
                            Logo = "/Images/Cinemas/cinema-3.jpg",
                            Description = "This is the description of the third cinema"
                        },
                        new Cinema()
                        {
                            Name = "Cinema 4",
                            Logo = "/Images/Cinemas/cinema-4.jpg",
                            Description = "This is the description of the fourth cinema"
                        },
                        new Cinema()
                        {
                            Name = "Cinema 5",
                            Logo = "/Images/Cinemas/cinema-5.jpg",
                            Description = "This is the description of the fifth cinema"
                        }
                    });
                    context.SaveChanges();

                }

                //Actors
                if (!context.Actors.Any())
                {


                    context.Actors.AddRange(new List<Actor>()
                    {
                        new Actor()
                        {
                            FullName = "Actor 1",
                            Bio = "This is the bio of the first actor",
                            ProfilePictureURL = "/Images/Actors/actor-1.jpg"
                        },
                        new Actor()
                        {
                            FullName = "Actor 2",
                            Bio = "This is the bio of the second actor",
                            ProfilePictureURL = "/Images/Actors/actor-2.jpg"
                        },
                        new Actor()
                        {
                            FullName = "Actor 3",
                            Bio = "This is the bio of the third actor",
                            ProfilePictureURL = "/Images/Actors/actor-3.jpg"
                        },
                        new Actor()
                        {
                            FullName = "Actor 4",
                            Bio = "This is the bio of the fourth actor",
                            ProfilePictureURL = "/Images/Actors/actor-4.jpg"
                        },
                        new Actor()
                        {
                            FullName = "Actor 5",
                            Bio = "This is the bio of the fifth actor",
                            ProfilePictureURL = "/Images/Actors/actor-5.jpg"
                        }
                    });
                    context.SaveChanges();
                }

                //Producers
                if (!context.Producers.Any())
                {

                    context.Producers.AddRange(new List<Producer>()
                    {
                        new Producer()
                        {
                            FullName = "Producer 1",
                            Bio = "This is the bio of the first producer",
                            ProfilePictureURL = "/images/producers/producer-1.jpg"
                        },
                        new Producer()
                        {
                            FullName = "Producer 2",
                            Bio = "This is the bio of the second producer",
                            ProfilePictureURL = "/images/producers/producer-2.jpg"
                        },
                        new Producer()
                        {
                            FullName = "Producer 3",
                            Bio = "This is the bio of the third producer",
                            ProfilePictureURL = "/images/producers/producer-3.jpg"
                        },
                        new Producer()
                        {
                            FullName = "Producer 4",
                            Bio = "This is the bio of the fourth producer",
                            ProfilePictureURL = "/images/producers/ستيفن سبيلبرغ-4.jpg"
                        },
                        new Producer()
                        {
                            FullName = "Producer 5",
                            Bio = "This is the bio of the fifth producer",
                            ProfilePictureURL = "/images/producers/producer-5.jpg"
                        }
                    });
                    context.SaveChanges();
                }

                //Movies
                if (!context.Movies.Any())
                {
                    context.Movies.RemoveRange(context.Movies);
                    context.SaveChanges();

                    context.Movies.AddRange(new List<Movie>()
                    {
                        new Movie()
                        {
                            Name = "Life",
                            Description = "This is the Life movie description",
                            Price = 39.50,
                            ImageURL = "/Images/Movies/movie-1.jpg",
                            StartDate = DateTime.Now.AddDays(-10),
                            EndDate = DateTime.Now.AddDays(10),
                            CinemaId = 3,
                            ProducerId = 3,
                            MovieCategory = MovieCategory.Documentary
                        },
                        new Movie()
                        {
                            Name = "The Shawshank Redemption",
                            Description = "This is the Shawshank Redemption description",
                            Price = 29.50,
                            ImageURL = "/Images/Movies/movie-2.jpg",
                            StartDate = DateTime.Now,
                            EndDate = DateTime.Now.AddDays(3),
                            CinemaId = 1,
                            ProducerId = 1,
                            MovieCategory = MovieCategory.Action
                        },
                        new Movie()
                        {
                            Name = "Ghost",
                            Description = "This is the Ghost movie description",
                            Price = 39.50,
                            ImageURL = "/Images/Movies/movie-3.jpg",
                            StartDate = DateTime.Now,
                            EndDate = DateTime.Now.AddDays(7),
                            CinemaId = 4,
                            ProducerId = 4,
                            MovieCategory = MovieCategory.Horror
                        },
                        new Movie()
                        {
                            Name = "Race",
                            Description = "This is the Race movie description",
                            Price = 39.50,
                            ImageURL = "/Images/Movies/movie-4.jpg",
                            StartDate = DateTime.Now.AddDays(-10),
                            EndDate = DateTime.Now.AddDays(-5),
                            CinemaId = 1,
                            ProducerId = 2,
                            MovieCategory = MovieCategory.Documentary
                        },
                        new Movie()
                        {
                            Name = "Scoob",
                            Description = "This is the Scoob movie description",
                            Price = 39.50,
                            ImageURL = "/Images/Movies/movie-5.jpg",
                            StartDate = DateTime.Now.AddDays(-10),
                            EndDate = DateTime.Now.AddDays(-2),
                            CinemaId = 1,
                            ProducerId = 3,
                            MovieCategory = MovieCategory.Cartoon
                        },
                        new Movie()
                        {
                            Name = "Cold Soles",
                            Description = "This is the Cold Soles movie description",
                            Price = 39.50,
                            ImageURL = "/Images/Movies/movie-6.jpg",
                            StartDate = DateTime.Now.AddDays(3),
                            EndDate = DateTime.Now.AddDays(20),
                            CinemaId = 1,
                            ProducerId = 5,
                            MovieCategory = MovieCategory.Drama
                        }
                    });
                    context.SaveChanges();
                }
                //Actors & Movies
                if (!context.Actors_Movies.Any())
                {

                    context.Actors_Movies.AddRange(new List<Actor_Movie>()
                    {
                        new Actor_Movie()
                        {
                            ActorId = 1,
                            MovieId = 1
                        },
                        new Actor_Movie()
                        {
                            ActorId = 3,
                            MovieId = 1
                        },

                         new Actor_Movie()
                        {
                            ActorId = 1,
                            MovieId = 2
                        },
                         new Actor_Movie()
                        {
                            ActorId = 4,
                            MovieId = 2
                        },

                         new Actor_Movie()
                        {
                            ActorId = 1,
                            MovieId = 3
                        },
                        new Actor_Movie()
                        {
                            ActorId = 2,
                            MovieId = 3
                        },
                        new Actor_Movie()
                        {
                            ActorId = 5,
                            MovieId = 3
                        },
                        new Actor_Movie()
                        {
                            ActorId = 2,
                            MovieId = 4
                        },
                        new Actor_Movie()
                        {
                            ActorId = 3,
                            MovieId = 4
                        },
                        new Actor_Movie()
                        {
                            ActorId = 4,
                            MovieId = 4
                        },


                        new Actor_Movie()
                        {
                            ActorId = 2,
                            MovieId = 5
                        },
                        new Actor_Movie()
                        {
                            ActorId = 3,
                            MovieId = 5
                        },
                        new Actor_Movie()
                        {
                            ActorId = 4,
                            MovieId = 5
                        },
                        new Actor_Movie()
                        {
                            ActorId = 5,
                            MovieId = 5
                        },


                        new Actor_Movie()
                        {
                            ActorId = 3,
                            MovieId = 6
                        },
                        new Actor_Movie()
                        {
                            ActorId = 4,
                            MovieId = 6
                        },
                        new Actor_Movie()
                        {
                            ActorId = 5,
                            MovieId = 6
                        },
                     });
                    context.SaveChanges();
                }
            }
        }
    }
}
