//using GymTrackerBusiness;
//using GymTrackerModel;
//using NUnit.Framework;
//using System.Linq;

//namespace GymTrackerTests
//{
//    public class CRUDSessionTests
//    {
//        CRUDManager crudManager = new CRUDManager();

//        [SetUp]

//        public void Setup()
//        {
//            using (var db = new GymTrackerContext())
//            {
//                var selectedCustomer =
//                from c in db.Users
//                where c.UserName == "Test-Vinay"
//                select c;
//                foreach (var c in selectedCustomer)
//                {
//                    db.Users.Remove(c);
//                }
//                db.SaveChanges();

//                crudManager.AddandSelectUser("Test-Vinay");
//                var selEx = db.Exercises.Where(e => e.ExerciseId == 1).FirstOrDefault();
//                crudManager.SelectExercise(selEx);
//                crudManager.AddandSelectSession("12-12-12"); //autoremoved when user removed
//            }
//        }

//        [TearDown]

//        public void TearDown()
//        {
//            using (var db = new GymTrackerContext())
//            {
//                var selectedCustomer =
//                from c in db.Users
//                where c.UserName == "Test-Vinay"
//                select c;

//                foreach (var c in selectedCustomer)
//                {
//                    db.Users.Remove(c);
//                }

//                db.SaveChanges();
//            }
//        }



//        [Test]
//        public void ReadUserSessionsTest()
//        {
//            using (var db = new GymTrackerContext())
//            {
//                var allSessions = db.Users.ToList();
//                var numberOfUserSessions = 0;
//                foreach (var item in allSessions)
//                {
//                    if (item.UserId == crudManager.SelectedUser.UserId)
//                    {
//                        numberOfUserSessions++;
//                    }
//                }

//                var resultFromMethod = crudManager.ReadUserSessions().Count();

//                Assert.IsTrue(crudManager.SelectedSession != null);
//                Assert.AreEqual(numberOfUserSessions, resultFromMethod);              

//            }
//        }


//        [Test]
//        public void AddandSelectSessionTest()
//        {
//            using (var db = new GymTrackerContext())
//            {
//                var allSessionsbefore = db.Sessions.ToList();
//                var beforeNumberOfUsers = 0;

//                foreach (var item in allSessionsbefore)
//                {
//                    if (item.UserId == crudManager.SelectedUser.UserId)
//                    {
//                        beforeNumberOfUsers++;
//                    }
//                }

//                crudManager.AddandSelectSession("07/09/2020");

//                var allSessionsAfter = db.Sessions.ToList();
//                var afterNumberOfUsers = 0;
//                foreach (var item in allSessionsAfter)
//                {
//                    if (item.UserId == crudManager.SelectedUser.UserId)
//                    {
//                        afterNumberOfUsers++;
//                    }
//                }

//                var resultFromMethod = crudManager.ReadUserSessions().Count();

//                Assert.AreEqual(beforeNumberOfUsers + 1, afterNumberOfUsers);
//            }
//        }

//        [Test]
//        public void RemoveSessionTest()
//        {
//            using (var db = new GymTrackerContext())
//            {
//                var numberOfUsersBefore = db.Users.ToList().Count();
//                crudManager.RemoveUser();
//                var numberOfUsersAfter = db.Users.ToList().Count();

//                Assert.AreEqual(numberOfUsersBefore - 1, numberOfUsersAfter);
//            }
//        }

//        [Test]
//        public void SelectExerciseFromSessionTest()
//        {
//            using (var db = new GymTrackerContext())
//            {
//                crudManager.SelectExerciseFromSession();
//                Assert.IsTrue(crudManager.SelectedExercise != null);
//            }
//        }


//    }
//}
