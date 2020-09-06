using GymTrackerBusiness;
using GymTrackerModel;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GymTrackerGui
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private CRUDManager crudManager = new CRUDManager();

        public MainWindow()
        {
            InitializeComponent();
            PopulateUserNames();
        }

        private void PopulateUserNames()
        {
            UserNames.ItemsSource = crudManager.ReadUser();
        }

        private void UserNames_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (UserNames.SelectedItem != null)
            {
                crudManager.SelectedSession = null;
                crudManager.SelectedMuscleGroup = null;
                crudManager.SelectedExercise = null;
                crudManager.SelectedSet = null;
                crudManager.SelectUser(UserNames.SelectedItem);                
                PopulateSixWeekOverivew();
                PopulateUserExersiceNames();
                PopulateSessionList();
                PopulateMuscleGroupList();
                UserSelected.Text = crudManager.SelectedUser.UserName;
            }
        }
        private void PopulateSixWeekOverivew()
        {
            if (crudManager.SelectedUser != null)
            {
                int userid = crudManager.SelectedUser.UserId;
                // so i want a list of dates and muscle group ids
                List<string> dates = new List<string>();
                List<int> totalExersices = new List<int>();
                List<string> ratios = new List<string>();
                (dates, totalExersices, ratios) = crudManager.SWOverview(userid);
                SixWeekOverview.Text = "6 week overivew"; //dates.ToString();

            }
        }
        private void PopulateUserExersiceNames()
        {
            if (crudManager.SelectedUser != null)
            {
                int userid = crudManager.SelectedUser.UserId;
                ExerciseNames.ItemsSource = crudManager.ReadExercise(userid);
            }
        }
        private void PopulateALLExersiceNames()
        {
            if (crudManager.SelectedUser != null)
            {
                ExerciseNames.ItemsSource = crudManager.ReadExercise();
            }
        }
        private void AllUsersExercises_Click(object sender, RoutedEventArgs e)
        {
            PopulateALLExersiceNames();
        }

        private void PopulateSessionList()
        {
            if (crudManager.SelectedUser != null)
            {
                int userid = crudManager.SelectedUser.UserId;
                SessionList.ItemsSource = crudManager.SWOverview(userid, 1);
            }
        }
        private void PopulateMuscleGroupList()
        {
            if (crudManager.SelectedUser != null)
            {
                MuscleGroupList.ItemsSource = crudManager.ReadMuscleGroups();
            }
        }
        private void SessionList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (SessionList.SelectedItem != null)
            {
                crudManager.SelectedSet = null;
                crudManager.SelectSession(SessionList.SelectedItem);
                InputDate.Text = crudManager.SelectedSession.ToString();
                ExerciseSelected.Text = crudManager.SelectedExercise.ToString();
                PopulateSetList();
            }
        }

        private void PopulateSetList()
        {
            if (crudManager.SelectedUser != null)
            {
                SetList.ItemsSource = crudManager.ReadSet();
            }
        }

        private void SetList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (SetList.SelectedItem != null)
            {
                crudManager.SelectSet(SetList.SelectedItem);
                PopulateExerciseDetails();
            }
        }

        private void PopulateExerciseDetails()
        {
            if (SetList.SelectedItem != null)
            {
                InputReps.Text = $"Reps: {crudManager.SelectedSet.NumberofReps.ToString()}";
                InputWeight.Text = $"Weight: {crudManager.SelectedSet.Weight.ToString()}";
            }
        }


        private void ExersiceNames_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (ExerciseNames.SelectedItem != null)
            {
                crudManager.SelectExercise((ExerciseNames.SelectedItem));
                ExerciseSelected.Text = crudManager.SelectedExercise.ExerciseName;
            }

        }

        private void MuscleGroupList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (MuscleGroupList.SelectedItem != null)
            {
                crudManager.SelectMuscleGroup((MuscleGroupList.SelectedItem));
                OutSelecetedMuscleGroup.Text = crudManager.SelectedMuscleGroup.MuscleGroupName;
            }
        }



        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            if (InputNewUser.Text.Trim() != "")
            {
                crudManager.AddUser((InputNewUser.Text.Trim()));
                PopulateUserNames();
                InputNewUser.Text = "";
            }            
            else
            {
                ErrorBox.Text = "missing data";
            }
        }

        private void AddSet_Click(object sender, RoutedEventArgs e)
        {
            string inputreps = InputReps.Text.Trim();
            string inputweight = InputWeight.Text.Trim();
            if (inputreps != "" & InputWeight.Text != "" & crudManager.SelectedSession != null)
            {
                if (IsDigitsOnly(inputreps) == true & IsDigitsOnly(inputweight) == true)
                {
                    crudManager.AddSets(Int32.Parse(inputreps), Int32.Parse(inputweight));
                    InputReps.Text = "";
                    InputWeight.Text = "";
                    PopulateSetList();
                }
                else
                {
                    ErrorBox.Text = "Sets and Weight must be Numbers";
                }
                
            }                
            else
            {
                ErrorBox.Text = "missing data";
            }

            bool IsDigitsOnly(string str)
            {
                foreach (char c in str)
                {
                    if (c < '0' || c > '9')
                    {
                        return false;
                    }                        
                }
                return true;
            }
        }

        

        private void AddSession_Click(object sender, RoutedEventArgs e)
        {
            if (InputDate.Text.Trim() != "" & crudManager.SelectedExercise != null & crudManager.SelectedUser!= null)
            {
                crudManager.AddSession(InputDate.Text.Trim());
                InputDate.Text = "";
                PopulateSessionList();
            }            
            else
            {
                ErrorBox.Text = "missing data";
            }
        }
        private void AddExercise_Click(object sender, RoutedEventArgs e)
        {
            if (InputExerciseName.Text.Trim() != "" & crudManager.SelectedMuscleGroup != null)
            {
                crudManager.AddExercise(InputExerciseName.Text.Trim());
                PopulateUserExersiceNames();
                InputExerciseName.Text = "";
                var selex = crudManager.ReadExercise();
                crudManager.SelectedExercise = selex[selex.Count() - 1];
            }
            else
            {
                ErrorBox.Text = "missing data";
            }
        }

        private void RemoveUser_Click(object sender, RoutedEventArgs e)
        {
            if (crudManager.SelectedUser != null)
            {
                crudManager.RemoveUser();
                PopulateUserNames();
                PopulateUserExersiceNames();
                PopulateSessionList();
                PopulateSetList();
                crudManager.SelectedUser = null;
                crudManager.SelectedSession = null;
                crudManager.SelectedMuscleGroup = null;
                crudManager.SelectedExercise = null;
                crudManager.SelectedSet = null;
            }            
            else
            {
                ErrorBox.Text = "Nothing Selected";
            }
        }
        private void RemoveSession_Click(object sender, RoutedEventArgs e)
        {
            if (crudManager.SelectedSession != null)
            {
                crudManager.RemoveSession();
                PopulateSessionList();
                PopulateSetList();
                crudManager.SelectedSession = null;
                crudManager.SelectedMuscleGroup = null;
                crudManager.SelectedExercise = null;
                crudManager.SelectedSet = null;
            }
            else
            {
                ErrorBox.Text = "Nothing Selected";
            }

        }
        private void RemoveSet_Click(object sender, RoutedEventArgs e)
        {
            if (crudManager.SelectedSet != null)
            {
                crudManager.RemoveSet();
                PopulateSetList();
                crudManager.SelectedSet = null;
            }
            else
            {
                ErrorBox.Text = "Nothing Selected";
            }
        }
        private void RemoveExercise_Click(object sender, RoutedEventArgs e)
        {
            if (crudManager.SelectedExercise != null)
            {
                bool remove = true;
                var allsesions = crudManager.ReadAllSessions();
                foreach (var item in allsesions)
                {
                    if (item.ExerciseId == crudManager.SelectedExercise.ExerciseId)
                    {
                        ErrorBox.Text = "Exercise in use";
                        remove = false;
                        break;
                    }
                }
                if (remove == true)
                {
                    crudManager.RemoveExercise();
                    PopulateUserExersiceNames();
                    PopulateSessionList();
                    PopulateSetList();
                    crudManager.SelectedExercise = null;
                    ClearAll();
                }                
            }
            else
            {
                ErrorBox.Text = "Nothing Selected";
            }
        }

        private void ClearAll()
        {
            InputDate.Text = "";
            ExerciseSelected.Text = "";
            UserSelected.Text = "";
            //.Text = "";
            //.Text = "";
            //.Text = "";
            //.Text = "";
            //.Text = "";
            //.Text = "";
            //.Text = "";

        }

    }
}
