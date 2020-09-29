//using GymTrackerBusiness;
//using GymTrackerModel;
//using NUnit.Framework;
//using System.Linq;

//namespace GymTrackerTests
//{
//    public class CRUDSetTests
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
//                crudManager.AddandSelectSession("12-12-12");
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


//        public void ReadSessionSetsTest()
//        {
//            using (var db = new GymTrackerContext())
//            {
//                var AllSets = db.Sets.ToList();
//                var numberOfUserSets = 0;
//                foreach (var item in AllSets)
//                {
//                    if (item.SessionId == crudManager.SelectedSession.SessionId)
//                    {
//                        numberOfUserSets++;
//                    }
//                }

//                var resultFromMethod = crudManager.ReadSessionSets().Count();

//                Assert.AreEqual(numberOfUserSets, resultFromMethod);
//                Assert.IsTrue(crudManager.SelectedSession != null);

//            }
//        }


//        [Test]
//        public void AddandSelectSetTest()
//        {
//            using (var db = new GymTrackerContext())
//            {
//                var allSetsbefore = db.Sets.ToList();
//                var beforeNumberofSessionSets = 0;

//                foreach (var item in allSetsbefore)
//                {
//                    if (item.SessionId == crudManager.SelectedSession.SessionId)
//                    {
//                        beforeNumberofSessionSets++;
//                    }
//                }

//                crudManager.AddandSelectSets(10, 10);
//                var allSessionsAfter = db.Sets.ToList();
//                var afterNumberofSessionSets = 0;

//                foreach (var item in allSessionsAfter)
//                {
//                    if (item.SessionId == crudManager.SelectedSession.SessionId)
//                    {
//                        afterNumberofSessionSets++;
//                    }
//                }

//                Assert.AreEqual(beforeNumberofSessionSets + 1, afterNumberofSessionSets);
//                Assert.IsTrue(crudManager.SelectedSet != null);

//            }
//        }

//        [Test]
//        public void RemoveSetTest()
//        {
//            using (var db = new GymTrackerContext())
//            {
//                crudManager.AddandSelectSets(10, 10);
//                var numberOfSetsBefore = db.Sets.ToList().Count();
//                crudManager.RemoveSet();
//                var numberOfSetsAfter = db.Sets.ToList().Count();

//                Assert.AreEqual(numberOfSetsBefore - 1, numberOfSetsAfter);
//            }
//        }

//        [Test]
//        public void UpdateSetTest()
//        {
//            using (var db = new GymTrackerContext())
//            {
//                crudManager.AddandSelectSets(10, 10);
//                var testSetId = crudManager.SelectedSet.SetId;
//                var beforeSetReps = db.Sets.Where(s => s.SetId == testSetId).FirstOrDefault().NumberofReps;
//                var beforeSetWeight = db.Sets.Where(s => s.SetId == testSetId).FirstOrDefault().Weight;
//                crudManager.UpdateSets(20, 5);

//                //var allSets = db.Sets.ToList();
//                //var afterSetReps = 0;
//                //var afterSetWeight = 0;
//                //foreach (var item in allSets)
//                //{
//                //    if (item.SetId == testSetId)
//                //    {
//                //        afterSetReps = item.NumberofReps;
//                //        afterSetWeight = item.Weight;
//                //    }
//                //}

//                var afterSetReps = db.Sets.Where(s => s.SetId == testSetId).FirstOrDefault().NumberofReps;
//                var afterSetWeight = db.Sets.Where(s => s.SetId == testSetId).FirstOrDefault().Weight;

//                Assert.AreEqual(beforeSetReps , afterSetReps);
//                Assert.AreEqual(beforeSetWeight , afterSetWeight);
//            }
//        }



//    }
//}


