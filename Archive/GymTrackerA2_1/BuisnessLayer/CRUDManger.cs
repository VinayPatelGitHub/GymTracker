using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using GymTrackerA2;
using Microsoft.EntityFrameworkCore;

namespace GymTrackerA2
{
    class CRUDManger
    {
        static void Main(string[] args)
        {
            ////Add users
            //AddUser("Vinay");
            ////Read users
            //List<string> allUser = new List<string>();
            //allUser = ReadUser();

            ////Add Exersice (need list of musclegroups to select from)
            //List<string> allMuscleGroups = new List<string>();
            //allMuscleGroups = ReadMuscleGroups();
            //AddExersice("Bench Press", 1);
            ////Read user Exercises
            //List<string> userExercises = new List<string>();
            //userExercises = ReadExercise();
            ////Read all Exercises
            //List<string> allExercises = new List<string>();
            //allExercises = ReadExercise();

            ////Add Session
            //AddSession(UserId, ExersiceId, date);
            ////Continue Session?

            ////Add sets
            //AddSets(SessionId, SetNo., reps, weight);
            ////Editing sets?

            ////Read all for 6week overview 
            ////need: session date and muscle group
            ////where: user
            // = SWOverview(userId);
            //List<string> SWDates = new List<string>();
            //List<int> SWMuscleGroups = new List<int>();
            //(SWDates, SWMuscleGroups) = SWOverview(1);

            ////Read all for exercise overview 
            ////need: date, set, reps, weight
            ////where: user, exercise
            // = EXOverview(userId, exerciseId);
            //List<string> EXDates = new List<string>();
            //List<int> EXSet = new List<int>();
            //List<int> EXRep = new List<int>();
            //List<int> EXWeight = new List<int>();
            //(EXDates, EXSet, EXRep, EXWeight) = EXOverview(1,1); 

        }

        static void AddUser(string name)
        {
            using (var db = new GymTrackerContext())
            {
                var newUser = new User()
                {
                    UserName = name
                };
                db.Users.Add(newUser);
                db.SaveChanges();
            }
        }
        static List<string> ReadUser()
        {
            using (var db = new GymTrackerContext())
            {
                List<string> allUsers = new List<string>();
                foreach (var user in db.Users)
                {
                    allUsers.Add(user.UserName);
                }
                return allUsers;
            }
        }



        static void AddExersice(string name, int muslegroupid)
        {
            using (var db = new GymTrackerContext())
            {
                var newExersice = new Exercise()
                {
                    ExerciseName = name,
                    MuscleGroupId = muslegroupid
                };
                db.Exercises.Add(newExersice);
                db.SaveChanges();
            }
        }
        static List<string> ReadMuscleGroups()
        {
            using (var db = new GymTrackerContext())
            {
                List<string> allMusclegroups = new List<string>();
                foreach (var user in db.MuscleGroups)
                {
                    allMusclegroups.Add(user.MuscleGroupName);
                }
                return allMusclegroups;
            }
        }
        static List<string> ReadExercise()
        {
            using (var db = new GymTrackerContext())
            {
                List<string> allExercises = new List<string>();
                foreach (var Exercises in db.Exercises)
                {
                    allExercises.Add(Exercises.ExerciseName);
                }
                return allExercises;
            }
        }
        static List<string> ReadExercise(int userid)
        {
            using (var db = new GymTrackerContext())
            {
                List<string> allExercises = new List<string>();
                var userExercises =
                    from s in db.Sessions
                    join e in db.Exercises on s.ExerciseId equals e.ExerciseId
                    where s.UserId == userid
                    select new { e.ExerciseName };

                foreach (var Exercises in userExercises)
                {
                    allExercises.Add(Exercises.ExerciseName);
                }
                return allExercises;
            }
        }



        static void AddSession(int user, int exersice, string date)
        {
            using (var db = new GymTrackerContext())
            {
                var newSession = new Session()
                {
                    UserId = user,
                    ExerciseId = exersice,
                    SessionDate = date
                };
                db.Sessions.Add(newSession);
                db.SaveChanges();
            }
        }



        static void AddSets(int sessionid, int setnumber, int numofreps, int weight)
        {
            using (var db = new GymTrackerContext())
            {
                var newSet = new Set()
                {
                    SessionId = sessionid,
                    SetNumber = setnumber,
                    NumberofReps = numofreps,
                    Weight = weight

                };
                db.Sets.Add(newSet);
                db.SaveChanges();
            }
        }



        static (List<string>, List<int>) SWOverview(int userid)
        {
            using (var db = new GymTrackerContext())
            {
                List<string> Dates = new List<string>();
                List<int> MuscleGroup = new List<int>();

                var allSessions =
                    from s in db.Sessions
                    join e in db.Exercises on s.ExerciseId equals e.ExerciseId
                    where s.UserId == userid
                    select new { s.SessionDate, e.MuscleGroupId };

                foreach (var item in allSessions)
                {
                    Dates.Add(item.SessionDate);
                    MuscleGroup.Add(item.MuscleGroupId);
                }

                return (Dates, MuscleGroup);
            }
        }



        static (List<string>, List<int>, List<int>, List<int>) EXOverview(int userid, int exersiceid)
        {
            using (var db = new GymTrackerContext())
            {
                List<string> ExDates = new List<string>();
                List<int> ExSet = new List<int>();
                List<int> ExRep = new List<int>();
                List<int> ExWeight = new List<int>();

                var allSessions =
                    from s in db.Sessions
                    join set in db.Sets on s.SessionId equals set.SessionId
                    where s.UserId == userid && s.ExerciseId == exersiceid
                    select new { s.SessionDate, set.SetNumber, set.NumberofReps, set.Weight };

                foreach (var item in allSessions)
                {
                    ExDates.Add(item.SessionDate);
                    ExSet.Add(item.SetNumber);
                    ExRep.Add(item.NumberofReps);
                    ExWeight.Add(item.Weight);
                }

                return (ExDates, ExSet, ExRep, ExWeight);
            }
        }




    }
}

