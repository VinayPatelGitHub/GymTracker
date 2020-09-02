using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GymTrackerA2
{
    class Program
    {
        static void Main(string[] args)
        {
            //initial data
            //AddUser("Vinay");
            //AddUser("Joel");
            //AddUser("Josh");

            //AddMuscleGroup("Chest");
            //AddMuscleGroup("Shoudlers");
            //AddMuscleGroup("Back");
            //AddMuscleGroup("Arms");
            //AddMuscleGroup("Legs");

            //AddExersice("Bench Press", 1);
            //AddExersice("Leg Press", 5);
            //AddExersice("Bicep Curl", 4);


            //this is where it gets tricky because i have to consdier how the wpf will work
            //add session then add sets to that sesssion
            //prehaps veryone clicks on an savedata object is currented for everything.

            //AddSession(UserId, ExersiceId, date);
            //AddSession(1, 1, "dateformat");
            //AddSession(1, 2, "dateformat");
            //AddSession(1, 1, "dateformat + 2days");
            //AddSession(1, 3, "dateformat + 2days");

            //AddSession(2, 3, "dateformat + 4days");

            //Adding sets:
            //AddSets(SessionId, SetNo., reps, weight);
            //AddSets(1, 1, 8, 40);
            //AddSets(1, 2, 8, 35);
            //AddSets(1, 3, 8, 30);
            //AddSets(1, 4, 8, 20);
            //AddSets(3, 1, 8, 45);
            //AddSets(3, 2, 8, 45);
            //AddSets(3, 3, 8, 35);
            //AddSets(3, 4, 8, 30);

            //AddSets(5, 1, 8, 20);
            //AddSets(5, 2, 8, 20);
            //AddSets(5, 3, 6, 20);
            //AddSets(5, 4, 6, 20);


            //Read users
            //List<string> allUser = new List<string>();
            //allUser = ReadUser();
            //foreach (var item in allUser)
            //{
            //    Console.WriteLine(item);
            //}


            //Read all for 6week overview 
            //need: session date and muscle group
            //where: user
            // = SWOverview(userId);
            //List<string> SWDates = new List<string>();
            //List<int> SWMuscleGroups = new List<int>();
            //(SWDates, SWMuscleGroups) = SWOverview(1);
            //foreach (var item in SWDates)
            //{
            //    Console.WriteLine(item);
            //}
            //foreach (var item in SWMuscleGroups)
            //{
            //    Console.WriteLine(item);
            //}

            //Read all for exercise overview 
            //need: date, set, reps, weight
            //where: user, exercise
            // = EXOverview(userId, exerciseId);
            //List<string> EXDates = new List<string>();
            //List<int> EXSet = new List<int>();
            //List<int> EXRep = new List<int>();
            //List<int> EXWeight = new List<int>();

            //(EXDates, EXSet, EXRep, EXWeight) = EXOverview(1,1);

            //for (var i = 0; i < EXDates.Count(); i++)
            //{
            //    Console.WriteLine($"{EXDates[i]}, {EXSet[i]}, {EXRep[i]}, {EXWeight[i]}");
            //}




            //will need an option to see all exercise and add new ones
            //read exercises
            //List<string> allExercises = new List<string>();
            //allExercises = ReadExercise();
            //
            //AddExersice("Bench Press", 1);
            //so need to read all musclegroups and offer as a list of options and then concevert to int
            //List<string> allMuscleGroups = new List<string>();
            //allMuscleGroups = ReadMuscleGroups();




            //Removing
            //using (var db = new GymTrackerContext())
            //{
            //    var thing = db.Exercises.Where(c => c.ExerciseId == 1).FirstOrDefault();  
            //    db.Exercises.Remove(thing);
            //    db.SaveChanges();
            //}

            //Update
            //using (var db = new GymTrackerContext())
            //{
            //    var thing = db.Exercises.Where(c => c.ExerciseId == 1).FirstOrDefault();
            //    thing.MuscleGroupId = 1;
            //    db.SaveChanges();
            //}

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

        static void AddMuscleGroup(string name)
        {
            using (var db = new GymTrackerContext())
            {
                var newMuscleGroup = new MuscleGroup()
                {
                    MuscleGroupName = name
                };
                db.MuscleGroups.Add(newMuscleGroup);
                db.SaveChanges();
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

        static List<string> ReadExercise()
        {
            using (var db = new GymTrackerContext())
            {
                List<string> allExercises = new List<string>();
                foreach (var user in db.Exercises)
                {
                    allExercises.Add(user.ExerciseName);
                }
                return allExercises;
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

