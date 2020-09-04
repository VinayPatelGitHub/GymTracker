using System;
using System.Collections.Generic;
using System.Text;

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
            return $"{SessionId}, {SessionDate}";
        }
    }

    public partial class Set
    {
        public override string ToString()
        {
            return $"Set Number: {SetNumber}";
        }
    }

}
