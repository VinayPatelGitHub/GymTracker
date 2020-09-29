using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GymTrackerModel
{
    public class GymTrackerContext : DbContext
    {
        public DbSet<Set> Sets { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Exercise> Exercises { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<MuscleGroup> MuscleGroups { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB; Initial Catalog=GymTrackerA2;");
    }


    public partial class Set
    {
        public int SetId { get; set; }
        public int SetNumber { get; set; }
        public int NumberofReps { get; set; }
        public int Weight { get; set; }
        public int SessionId { get; set; }

        public Session Session { get; set; }
    }
    public partial class Session
    {
        public int SessionId { get; set; }
        public string SessionDate { get; set; } //to datetime
        public int ExerciseId { get; set; }
        public int UserId { get; set; }

        public User User { get; set; }
        public Exercise Exercise { get; set; }
        public List<Set> Sets { get; } = new List<Set>();
    }

    public partial class Exercise
    {
        public int ExerciseId { get; set; }
        public string ExerciseName { get; set; }
        public int MuscleGroupId { get; set; }

        public MuscleGroup MuscleGroup { get; set; }
        public List<Session> Sessions { get; } = new List<Session>();
    }


    public partial class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }

        public List<Session> Sessions { get; } = new List<Session>();   

    }

    public partial class MuscleGroup
    {
        public int MuscleGroupId { get; set; }
        public string MuscleGroupName { get; set; }

        public List<Exercise> Exercises { get; } = new List<Exercise>();
    }
}
