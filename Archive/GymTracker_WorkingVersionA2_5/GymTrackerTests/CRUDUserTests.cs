//using GymTrackerBusiness;
//using GymTrackerModel;
//using NUnit.Framework;
//using System.Linq;

//namespace GymTrackerTests
//{
//    public class CRUDUserTests
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
//        public void ReadUserTest()
//        {
//            using (var db = new GymTrackerContext())
//            {
//                var numberOfUsers = db.Users.ToList().Count();
//                var returnFromMethod = crudManager.ReadUser();
//                var resultFromMethod = returnFromMethod.Count();

//                Assert.AreEqual(numberOfUsers, resultFromMethod);
//            }
//        }

//        [Test]
//        public void AddandSelectUserTest()
//        {
//            using (var db = new GymTrackerContext())
//            {
//                var numberOfUsersBefore = db.Users.ToList().Count();
//                crudManager.AddandSelectUser("Test-Vinay");
//                var numberOfUsersAfter = db.Users.ToList().Count();

//                Assert.AreEqual(numberOfUsersBefore + 1, numberOfUsersAfter);
//                Assert.IsTrue(crudManager.SelectedUser.UserName == "Test-Vinay");
//            }
//        }

//        [Test]
//        public void RemoveUserTest()
//        {
//            using (var db = new GymTrackerContext())
//            {
//                crudManager.AddandSelectUser("Test-Vinay");
//                var numberOfUsersBefore = db.Users.ToList().Count();
//                crudManager.RemoveUser();
//                var numberOfUsersAfter = db.Users.ToList().Count();

//                Assert.AreEqual(numberOfUsersBefore, numberOfUsersAfter + 1);
//            }
//        }
//    }
//}