using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using GymTrackerModel;
using Microsoft.EntityFrameworkCore;

namespace GymTrackerBusiness
{
    public class CRUDManager
    {
        public User SelectedUser { get; set; }
        public Session SelectedSession { get; set; }
        public Set SelectedSet { get; set; }
        public Exercise SelectedExercise { get; set; }
        public MuscleGroup SelectedMuscleGroup { get; set; }

        static void Main(string[] args)
        {
            
        }


        public void SelectUser(object SelectedUserIn)
        {
            SelectedUser = (User)SelectedUserIn;
        }
        public void SelectSession(object SelectedSessionIn)
        {
            SelectedSession = (Session)SelectedSessionIn;
        }
        public void SelectSet(object SelectedSetIn)
        {
            SelectedSet = (Set)SelectedSetIn;
        }
        public void SelectExercise(object SelectedExerciseIn)
        {
            SelectedExercise = (Exercise)SelectedExerciseIn;
        }
        public void SelectMuscleGroup(object SelectedMuscleGroupIn)
        {
            SelectedMuscleGroup = (MuscleGroup)SelectedMuscleGroupIn;
        }


        public void AddandSelectUser(string name)
        {
            using (var db = new GymTrackerContext())
            {
                var newUser = new User()
                {
                    UserName = name
                };
                db.Users.Add(newUser);
                db.SaveChanges();
                SelectedUser = newUser;
            }
        }

        public List<User> ReadUser()
        {
            using (var db = new GymTrackerContext())
            {
                return db.Users.ToList();
            }
        }

        public void RemoveUser()
        {
            using (var db = new GymTrackerContext())
            {
                db.Users.Remove(SelectedUser);
                db.SaveChanges();
            }
        }


        public void AddandSelectExercise(string name)
        {
            using (var db = new GymTrackerContext())
            {
                var newExersice = new Exercise()
                {
                    ExerciseName = name,
                    MuscleGroupId = SelectedMuscleGroup.MuscleGroupId
                };
                db.Exercises.Add(newExersice);
                SelectedExercise = newExersice;
                db.SaveChanges();
            }
        }
        public void RemoveExercise()
        {
            using (var db = new GymTrackerContext())
            {
                db.Exercises.Remove(SelectedExercise);
                db.SaveChanges();
            }                
        }


        public void SelectExerciseFromSession()
        {
            using (var db = new GymTrackerContext())
            {
                var SelExersice = db.Exercises.Where(c => c.ExerciseId == SelectedSession.ExerciseId).FirstOrDefault();
                SelectExercise(SelExersice);
            }
        }

        public List<Exercise> ReadAllExercise()
        {
            using (var db = new GymTrackerContext())
            {
                List<Exercise> allExercise = new List<Exercise>();
                allExercise = db.Exercises.ToList();
                return allExercise;
            }
        }

        public List<Exercise> ReadUserExercise()
        {
            using (var db = new GymTrackerContext())
            {
                var userSessionList = new List<Session>();
                var allSessionsList = db.Sessions.ToList();
                foreach (var item in allSessionsList)
                {
                    if (item.UserId == SelectedUser.UserId)
                    {
                        userSessionList.Add(item);
                    }
                }

                var userExerciseList = new List<Exercise>();
                var allExerciseList = db.Exercises.ToList();
                foreach (var exerciseItem in allExerciseList)                    
                {
                    foreach (var userSession in userSessionList)
                    {
                        if (exerciseItem.ExerciseId == userSession.ExerciseId)
                        {
                            userExerciseList.Add(exerciseItem);
                            break;
                        }
                    }
                }
                return userExerciseList;
            }
        }

        public List<MuscleGroup> ReadMuscleGroups()
        {
            using (var db = new GymTrackerContext())
            {
                return db.MuscleGroups.ToList();
            }
        }     

       
        public void AddandSelectSession(string date)
        {
            using (var db = new GymTrackerContext())
            {
                var newSession = new Session()
                {
                    UserId = SelectedUser.UserId,
                    ExerciseId = SelectedExercise.ExerciseId,
                    SessionDate = date
                };
                db.Sessions.Add(newSession);
                SelectedSession = newSession;
                db.SaveChanges();
            }
        }

        public void RemoveSession()
        {
            using (var db = new GymTrackerContext())
            {
                db.Sessions.Remove(SelectedSession);
                db.SaveChanges();
            }
        }

        public void AddSets(int numofreps, int weight)
        {
            using (var db = new GymTrackerContext())
            {
                int setnumbermanual = 1;
                var allSets = db.Sets.ToList();
                foreach (var item in allSets)
                {
                    if (item.SessionId == SelectedSession.SessionId)
                    {
                        setnumbermanual++;
                    }
                }  
                var newSet = new Set()
                {
                    SessionId = SelectedSession.SessionId,
                    SetNumber = setnumbermanual,
                    NumberofReps = numofreps,
                    Weight = weight
                };
                db.Sets.Add(newSet);
                db.SaveChanges();
                SelectedSet = newSet;
            }
        }
        public List<Set> ReadSessionSets()
        {
            using (var db = new GymTrackerContext())
            {
                List<Set> allSets = db.Sets.ToList();
                List<Set> sessionSets = new List<Set>();
                if (SelectedSession != null)
                {
                    foreach (var item in allSets)
                    {
                        if (item.SessionId == SelectedSession.SessionId)
                        {
                            sessionSets.Add(item);
                        }
                    }
                }
                return sessionSets;
            }
        }

        public void UpdateSets(int numofreps, int weight)
        {
            using (var db = new GymTrackerContext())
            {
                var SetWhy = db.Sets.Where(c => c.SetId == SelectedSet.SetId).FirstOrDefault();
                var allSessionSets = ReadSessionSets();
                int renumber = 1;
                foreach (var item in allSessionSets)
                {
                    if (item.SetId == SetWhy.SetId)
                    {                        
                        break;
                    }
                    renumber++;
                }
                SetWhy.SetNumber = renumber;
                SetWhy.NumberofReps = numofreps;
                SetWhy.Weight = weight;
                db.SaveChanges();
            }
        }

        public void RemoveSet()
        {
            using (var db = new GymTrackerContext())
            {
                db.Sets.Remove(SelectedSet);
                db.SaveChanges();
                var allinSession = ReadSessionSets();
                foreach (var item in allinSession)
                {
                    SelectedSet = item;
                    UpdateSets(item.NumberofReps, item.Weight);
                }
            }
        }

        public List<Session> ReadAllSessions()
        {
            using (var db = new GymTrackerContext())
            {
                return db.Sessions.ToList();
            }
        }
        public List<Session> ReadUserSessions()
        {
            using (var db = new GymTrackerContext())
            {
                var allsessions = db.Sessions.ToList();
                List<Session> userSessions = new List<Session>();
                foreach (var item in allsessions)
                {
                    if (item.UserId == SelectedUser.UserId)
                    {
                        userSessions.Add(item);
                    }
                }
                return userSessions;
            }
        }

        public bool CheckDate(string date)
        {
            bool result = true;
            string justNumbers = "A";
            if (date.Length == 10) 
            {
                justNumbers = $"{date[0]}{date[1]}{date[3]}{date[4]}{date[6]}{date[7]}{date[8]}{date[9]}";
            }
            else if (date.Length == 8)
            {
                justNumbers = $"{date[0]}{date[1]}{date[3]}{date[4]}{date[6]}{date[7]}";
            }

            foreach (var item in justNumbers)
            {
                if (Char.IsDigit(item) == false)
                {
                    return false;
                }
            }

            return result;
        }






        public List<Session> SWOverview(int userid, int fake)
        {
            using (var db = new GymTrackerContext())
            {
                List<Session> allSessions = new List<Session>();
                List<Session> userSessions = new List<Session>();
                allSessions = db.Sessions.ToList();
                foreach (var item in allSessions)
                {
                    if (item.UserId == userid)
                    {
                        userSessions.Add(item);
                    }
                }
                return userSessions;
            }
        }

        //////////////////////////////////////////////////////////////////////////////////////

        public (List<string>, List<int>, List<string>) SWOverview(int userid)
        {
            using (var db = new GymTrackerContext())
            {
                List<string> dates = new List<string>();
                List<int> totalExersices = new List<int>();
                List<string> ratios = new List<string>();

                var allSessions =
                    from s in db.Sessions
                    join e in db.Exercises on s.ExerciseId equals e.ExerciseId
                    where s.UserId == userid
                    select new { s.SessionDate, e.MuscleGroupId };
                //int countChest = 0;
                //int countShoulders = 0;
                //int countBack= 0;
                //int countArms = 0;
                //int countLegs = 0; //for future feature - hopefully not
                foreach (var item in allSessions)
                {
                    if (dates.Count() > 0) 
                    {
                        if (item.SessionDate == dates[dates.Count() - 1])
                        {
                            totalExersices[totalExersices.Count() - 1]++;
                            //ratios[ratios.Count() - 1] = ("1:1:0:0:0"); // for future feature
                        }
                    }

                    else
                    {
                        dates.Add(item.SessionDate);
                        totalExersices.Add(1);

                        /////na....
                        if (item.MuscleGroupId == 1)
                        {
                            ratios.Add("1:0:0:0:0"); // for future feature
                        }
                        else if (item.MuscleGroupId == 2)
                        {
                            {
                                ratios.Add("0:1:0:0:0"); // for future feature
                            }
                        }
                        else if (item.MuscleGroupId == 3)
                        {
                            {
                                ratios.Add("0:0:1:0:0"); // for future feature
                            }
                        }
                        else if (item.MuscleGroupId == 4)
                        {
                            {
                                ratios.Add("0:0:0:1:0"); // for future feature
                            }
                        }
                        else if (item.MuscleGroupId == 5)
                        {
                            {
                                ratios.Add("0:0:0:0:1"); // for future feature
                            }
                        }
                        ////na....
                    }  
                }

                return (dates, totalExersices, ratios);
            }
        }    


        public static(List<string>, List<int>, List<int>, List<int>) EXOverview(int userid, int exersiceid)
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

