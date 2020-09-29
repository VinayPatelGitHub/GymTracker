//using GymTrackerBusiness;
//using GymTrackerModel;
//using NUnit.Framework;
//using NUnit.Framework.Constraints;
//using System.Collections.Generic;
//using System.Linq;

//namespace GymTrackerTests
//{
    //public class CRUDExerciseTests
    //{
    //    CRUDManager crudManager = new CRUDManager();

    //    [SetUp]

    //    public void Setup()
    //    {
    //        using (var db = new GymTrackerContext())
    //        {
    //            var selectedCustomer =
    //            from c in db.Users
    //            where c.UserName == "Test-Vinay"
    //            select c;

    //            foreach (var c in selectedCustomer)
    //            {
    //                db.Users.Remove(c);
    //            }

    //            db.SaveChanges();

    //            crudManager.AddandSelectUser("Test-Vinay");
    //            //var selEx = db.Exercises.Where(e => e.ExerciseId == 1).FirstOrDefault();
    //            //crudManager.SelectExercise(selEx);
    //            //crudManager.AddandSelectSession("12-12-12");

    //        }
    //    }

    //    [TearDown]

    //    public void TearDown()
    //    {
    //        using (var db = new GymTrackerContext())
    //        {
    //            var selectedCustomer =
    //            from c in db.Users
    //            where c.UserName == "Test-Vinay"
    //            select c;

    //            foreach (var c in selectedCustomer)
    //            {
    //                db.Users.Remove(c);
    //            }

    //            db.SaveChanges();

    //            //var exercise = db.Exercises.Where(e => e.ExerciseName == "Jumping");
    //            //crudManager.SelectExercise(exercise);
    //            //crudManager.RemoveExercise();
    //        }
    //    }


    //    [Test]
    //    public void ReadAllExercisesTest()
    //    {
    //        using (var db = new GymTrackerContext())
    //        {
    //            var numberOfExercises = db.Exercises.ToList().Count();
    //            var returnFromMethod = crudManager.ReadAllExercise().Count(); ;

    //            Assert.AreEqual(numberOfExercises, returnFromMethod);
    //        }
    //    }

    //    [Test]
    //    public void ReadUserExercisesTest()
    //    {
    //        using (var db = new GymTrackerContext())
    //        {
    //            List<Session> listofSessions = new List <Session>();
    //            var userSessionsListQuery = db.Sessions.Where(s => s.UserId == crudManager.SelectedUser.UserId);
    //            foreach (var item in userSessionsListQuery)
    //            {
    //                listofSessions.Add(item);
    //            }
    //            var listofExercises = db.Exercises.ToList() ;
    //            List<Exercise> userListofEx = new List<Exercise>();
    //            foreach (var exercise in listofExercises)
    //            {
    //                foreach (var session in listofSessions)
    //                {
    //                    if (exercise.ExerciseId == session.ExerciseId)
    //                    {
    //                        userListofEx.Add(exercise);
    //                    }
    //                }

    //            }
    //            var expectedNumber = userListofEx.ToList().Count();
    //            var returnFromMethod = crudManager.ReadUserExercise().Count(); ;

    //            Assert.AreEqual(expectedNumber, returnFromMethod);
    //        }
    //    }

    //    [Test]
    //    public void AddandSelectExerciseTest()
    //    {
    //        using (var db = new GymTrackerContext())
    //        {
    //            var selMusGro = db.MuscleGroups.Where(c => c.MuscleGroupId == 1).FirstOrDefault();
    //            crudManager.SelectMuscleGroup(selMusGro);
    //            var beforeNumberOfExercises = db.Exercises.ToList().Count();                
    //            crudManager.AddandSelectExercise("Jumping");
    //            var afterNumberOfExercises = db.Exercises.ToList().Count();

    //            Assert.AreEqual(crudManager.SelectedExercise.ExerciseName, "Jumping");
    //            Assert.AreEqual(beforeNumberOfExercises + 1, afterNumberOfExercises);
    //        }
    //    }


    //    [Test]
    //    public void RemoveExerciseTest()
    //    {
    //        using (var db = new GymTrackerContext())
    //        {
    //            var selMusGro = db.MuscleGroups.Where(c => c.MuscleGroupId == 1).FirstOrDefault();
    //            crudManager.SelectMuscleGroup(selMusGro);
    //            crudManager.AddandSelectExercise("Jumping");
    //            var beforeNumberOfExercises = db.Exercises.ToList().Count();
    //            crudManager.RemoveExercise();
    //            var afterNumberOfExercises = db.Exercises.ToList().Count();

    //            Assert.IsTrue(crudManager.SelectedExercise == null);
    //            Assert.AreEqual(beforeNumberOfExercises, afterNumberOfExercises+1);
    //        }
    //    }
//    }
//}