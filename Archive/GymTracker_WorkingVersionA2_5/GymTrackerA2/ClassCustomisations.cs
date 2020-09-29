using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using GymTrackerModel;
using Microsoft.EntityFrameworkCore;

namespace GymTrackerModel
{
    public partial class User
    {
        public override string ToString()
        {
            return $"{UserName}";
        }
    }

    public partial class Exercise
    {
        public override string ToString()
        {
            return $"{ExerciseName}";
        }
    }

    public partial class Session
    {
        public override string ToString()
        {
            using (var db = new GymTrackerContext())
            {
                var exerciseName = db.Exercises.Where(e => e.ExerciseId == ExerciseId).FirstOrDefault();
                return $"{exerciseName.ExerciseName}";
            }
        }

    }

    public partial class Set
    {
        public override string ToString()
        {
            return $"Set Number: {SetNumber}, Number of Repetitions: {NumberofReps}, Weight: {Weight}";
        }
    }

    public partial class MuscleGroup
    {
        public override string ToString()
        {
            return $"{MuscleGroupName}";
        }
    }

}
