//using GymTrackerBusiness;
//using GymTrackerModel;
//using NUnit.Framework;
//using System.Linq;

//namespace GymTrackerTests
//{
//    class CRUDMuscleGroupTests
//    {
//        CRUDManager crudManager = new CRUDManager();
        
//        [Test]
//        public void ReadMuscleGroupsTest()
//        {
//            using (var db = new GymTrackerContext())
//            {
//                var numberofMuscleGroups = db.MuscleGroups.ToList().Count();
//                var resultFromMethod = crudManager.ReadMuscleGroups().Count();

//                Assert.AreEqual(numberofMuscleGroups, resultFromMethod);
//            }
//        }

//        [Test]
//        public void SelectMuscleGroupTest()
//        {
//            using (var db = new GymTrackerContext())
//            {
//                var selecteditem = db.MuscleGroups.Where(mg => mg.MuscleGroupId == 2).FirstOrDefault();
//                var expectedResult = selecteditem.MuscleGroupName;

//                crudManager.SelectMuscleGroup(selecteditem);

//                Assert.AreEqual(expectedResult, crudManager.SelectedMuscleGroup.MuscleGroupName);
//            }
//        }

        




//    }
//}
