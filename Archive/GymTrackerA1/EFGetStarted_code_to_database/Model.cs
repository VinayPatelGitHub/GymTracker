using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GymTracker
{
    public class GymTrackerContext : DbContext
    {
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=Blogging;");
    }

   

    public class Session
    {
        public int SessionId { get; set; }
        public string SessionDate { get; set; } //to datetime
        public string Set1 { get; set; }
        public string Weight1 { get; set; }        

        public Exercise Exercise { get; set; }
        public User User { get; set; }
    }

    public class Exercise
    {
        public int ExerciseId { get; set; }
        public string ExerciseName { get; set; }
        public string MuscleGroup { get; set; }

        public List<Session> Sessions { get; } = new List<Session>();
        public User User { get; set; }
    }


    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }

        public List<Session> Sessions { get; } = new List<Session>();
        public List<Exercise> Exercises { get; } = new List<Exercise>();

    }
}