﻿using GymTrackerBusiness;
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
            PopulateUserPage();
            PopulateSessionsDetailsPage();
            
        }

        
        private void MainListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (PageName.Text == "User Page" & MainListBox.SelectedItem != null)
            {
                crudManager.SelectUser(MainListBox.SelectedItem);
                PopulateWorkoutPage();
            }
            else if (PageName.Text == "Workouts Page" & MainListBox.SelectedItem != null)
            {
                crudManager.SelectSession(MainListBox.SelectedItem);
                crudManager.SelectExerciseFromSession();                
                PopulateSessionsDetailsPage();
                MainButtonRemove.Content = "Remove Workout";
            }
            else if (PageName.Text == "Add Workout Page" & MainListBox.SelectedItem != null)
            {
                crudManager.SelectExercise(MainListBox.SelectedItem);
                crudManager.AddandSelectSession(DateSelected.Text);
                SetListBox.ItemsSource = crudManager.ReadSessionSets();
                PopulateSessionsDetailsPage();
                PopulateWorkoutPage();
            }
            else if (PageName.Text == "New Exercise Page" & MainListBox.SelectedItem != null)
            {
                crudManager.SelectMuscleGroup(MainListBox.SelectedItem);
                crudManager.AddandSelectExercise(InputString.Text);
                crudManager.AddandSelectSession (DateSelected.Text);
                PopulateSessionsDetailsPage();
                PopulateWorkoutPage();
            }                
        }

        private void PopulateUserPage()
        {
            MainListBox.ItemsSource = crudManager.ReadUser();
            UserSelected.Text = "";
            MainTextBox.Text = "Select User to get started:";
            InputLabel.Text = "Enter New User Name:";
            MainButtonAdd.Content = "Add New User";
            MainButtonRemove.Content = "";
            PageName.Text = "User Page";
            BackButton.Content = "Speical Surprise";
            ErrorMessage.Text = "";
            BackButton.Visibility = Visibility.Hidden;
            MainButtonRemove.Visibility = Visibility.Hidden;
            RemoveInput.Visibility = Visibility.Hidden;

        }

        private void PopulateWorkoutPage()
        {                       
            MainListBox.ItemsSource = crudManager.ReadUserSessions();
            MainTextBox.Text = "Select Session to View Previous Workout:";
            InputLabel.Text = "Enter date:";
            MainButtonAdd.Content = "Start Workout?";
            MainButtonRemove.Content = "Remove User";
            PageName.Text = "Workouts Page";
            BackButton.Content = "Back";
            UserSelected.Text = crudManager.SelectedUser.UserName.ToString();
            BackButton.Visibility = Visibility.Visible;
            MainButtonRemove.Visibility = Visibility.Visible;
            RemoveInput.Visibility = Visibility.Visible;
            RemoveInput.Text = "Type Remove Here";
        }
        private void PopulateNewWorkoutPage()
        {
            MainListBox.ItemsSource = crudManager.ReadUserExercise();
            MainTextBox.Text = "Select the Exercise you want to do:";
            InputLabel.Text = "Enter New Exercise Name:";
            MainButtonAdd.Content = "Add Exercise";            
            PageName.Text = "Add Workout Page";
            BackButton.Content = "Back";
            BackButton.Visibility = Visibility.Visible;
            MainButtonRemove.Visibility = Visibility.Visible;


        }

        private void PopulateNewExercisePage()
        {
            MainListBox.ItemsSource = crudManager.ReadMuscleGroups();
            MainTextBox.Text = "Select the Main Muscle Group for this Exercise:";
            InputLabel.Text = "Last chance to change Exercise Name";
            MainButtonAdd.Content = "";
            MainButtonRemove.Content = "";
            PageName.Text = "New Exercise Page";
            BackButton.Content = "Back";
            BackButton.Visibility = Visibility.Visible;

        }

        private void PopulateSessionsDetailsPage()
        {
            if (crudManager.SelectedSession != null)
            {
                BoxName.Text = "Set List:";
                DateSelected.Text = crudManager.SelectedSession.SessionDate.ToString();
                ExerciseSelected.Text = crudManager.SelectedExercise.ExerciseName;
                SetListBox.ItemsSource = crudManager.ReadSessionSets();
                RepLabel.Text = "Repetitions:";
                WeightLabel.Text = "Weight:";
                ErrorMessage.Text = "";
            }
            else
            {
                BoxName.Text = "Set List:";
                DateSelected.Text = "";
                ExerciseSelected.Text = "";
                RepLabel.Text = "Repetitions:";
                WeightLabel.Text = "Weight:";
                SetListBox.ItemsSource = null;
                RepInput.Text = "";
                WeightInput.Text = "";
                ErrorMessage.Text = "";
            }
        }               

    
        private void MainButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            if (PageName.Text == "User Page")
            {
                if (InputString.Text.Trim() != "")
                {
                    crudManager.AddandSelectUser((InputString.Text.Trim()));
                    PopulateWorkoutPage();
                    PopulateSessionsDetailsPage();
                    InputString.Text = "";
                }
                else
                {
                    ErrorMessage.Text = "Enter a new User Name";
                }
            }
            else if (PageName.Text == "Workouts Page")
            {
                if (crudManager.CheckDate(InputString.Text) == false)
                {
                    ErrorMessage.Text = "Requires Date - must be in date format: **/**/**** or **/**/**";
                }
                else
                {                
                    PopulateNewWorkoutPage();
                    crudManager.SelectedSession = null;
                    PopulateSessionsDetailsPage();
                    DateSelected.Text = InputString.Text;
                    InputString.Text = "";
                    ErrorMessage.Text = "";
                    MainButtonRemove.Content = "All User Exercises";
                    RemoveInput.Visibility = Visibility.Hidden;
                }                
            }
            else if (PageName.Text == "Add Workout Page")
            {
                if (InputString.Text != "")
                {
                    PopulateNewExercisePage();
                }
                else
                {
                    ErrorMessage.Text = "Add an Exercise Names";
                }
            }
        }

        private void MainButtonRemove_Click(object sender, RoutedEventArgs e)
        {
            if (PageName.Text == "Workouts Page")
            {
                if (crudManager.SelectedUser != null & RemoveInput.Text == "REMOVE")
                {
                    crudManager.RemoveUser();
                    crudManager.SelectedUser = null;
                    crudManager.SelectedSession = null;
                    crudManager.SelectedMuscleGroup = null;
                    crudManager.SelectedExercise = null;
                    crudManager.SelectedSet = null;
                    PopulateSessionsDetailsPage();
                    PopulateUserPage();
                }
                else
                {
                    ErrorMessage.Text = "Type \"REMOVE\"";
                }
            }
            else if (PageName.Text == "Workouts Page" & MainButtonRemove.Content == "Remove Workout")
            {
                if (crudManager.SelectedSession != null & RemoveInput.Text == "REMOVE")
                {
                    crudManager.RemoveSession();
                    crudManager.SelectedSession = null;
                    crudManager.SelectedMuscleGroup = null;
                    crudManager.SelectedExercise = null;
                    crudManager.SelectedSet = null;
                    PopulateSessionsDetailsPage();
                    PopulateWorkoutPage();
                }
                else
                {
                    ErrorMessage.Text = "Type \"REMOVE\"";
                }
            }
            else if (PageName.Text == "Add Workout Page" & MainButtonRemove.Content == "All User Exercises")
            {
                MainListBox.ItemsSource = crudManager.ReadAllExercise();
                MainButtonRemove.Content = "Remove Exercise";
                RemoveInput.Visibility = Visibility.Visible;
            }
            else if (PageName.Text == "Add Workout Page" & MainButtonRemove.Content == "Remove Exercise")
            {
                if (crudManager.SelectedUser != null & RemoveInput.Text == "REMOVE")
                {
                    crudManager.RemoveSession();
                    crudManager.SelectedSession = null;
                    crudManager.SelectedMuscleGroup = null;
                    crudManager.SelectedExercise = null;
                    crudManager.SelectedSet = null;
                    PopulateSessionsDetailsPage();
                    PopulateWorkoutPage();
                }
                else
                {
                    ErrorMessage.Text = "Type \"REMOVE\"";
                }
            }

        }

        private void SetListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (SetListBox.SelectedItem != null)
            {
                crudManager.SelectSet(SetListBox.SelectedItem);
                RepInput.Text = crudManager.SelectedSet.NumberofReps.ToString();
                WeightInput.Text = crudManager.SelectedSet.Weight.ToString();
            }
        }

        private void AddSet_Click(object sender, RoutedEventArgs e)
        {
            string inputreps = RepInput.Text.Trim();
            string inputweight = WeightInput.Text.Trim();
            if (inputreps != "" & inputweight != "" & crudManager.SelectedSession != null)
            {
                if (IsDigitsOnly(inputreps) == true & IsDigitsOnly(inputweight) == true)
                {
                    crudManager.AddSets(Int32.Parse(inputreps), Int32.Parse(inputweight));
                    RepInput.Text = "";
                    WeightInput.Text = "";
                    PopulateSessionsDetailsPage();
                }
                else
                {
                    ErrorMessage.Text = "Sets and Weight must be Numbers";
                }
            }
            else
            {
                ErrorMessage.Text = "missing data";
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

        private void UpdateSet_Click(object sender, RoutedEventArgs e)
        {
            string inputreps = RepInput.Text.Trim();
            string inputweight = WeightInput.Text.Trim();
            if (inputreps != "" & inputweight != "" & crudManager.SelectedSession != null)
            {
                if (IsDigitsOnly(inputreps) == true & IsDigitsOnly(inputweight) == true)
                {
                    crudManager.UpdateSets(Int32.Parse(inputreps), Int32.Parse(inputweight));
                    RepInput.Text = "";
                    WeightInput.Text = "";
                    PopulateSessionsDetailsPage();
                }
                else
                {
                    ErrorMessage.Text = "Sets and Weight must be Numbers";
                }
            }
            else
            {
                ErrorMessage.Text = "missing data";
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

        private void RemoveSet_Click(object sender, RoutedEventArgs e)
        {
            if (crudManager.SelectedSet != null)
            {
                crudManager.RemoveSet();
                RepInput.Text = "";
                WeightInput.Text = "";
                PopulateSessionsDetailsPage();
                crudManager.SelectedSet = null;

            }
            else
            {
                ErrorMessage.Text = "Nothing Selected";
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (PageName.Text == "User Page")
            {
                ;
            }
            else if (PageName.Text == "Workouts Page")
            {
                crudManager.SelectedUser = null;
                crudManager.SelectedSession = null;
                crudManager.SelectedSet = null;
                PopulateUserPage();
                PopulateSessionsDetailsPage();
            }
            else if (PageName.Text == "Add Workout Page")
            {
                PopulateWorkoutPage();
                PopulateSessionsDetailsPage();
            }
            else if (PageName.Text == "New Exercise Page")
            {
                PopulateNewWorkoutPage();
            }

        }

        //    private void PopulateSixWeekOverivew()
        //    {
        //        if (crudManager.SelectedUser != null)
        //        {

        //        }
        //    }
        //    private void PopulateUserExersiceNames()
        //    {
        //        if (crudManager.SelectedUser != null)
        //        {
        //            int userid = crudManager.SelectedUser.UserId;
        //            ExerciseNames.ItemsSource = crudManager.ReadExercise(userid);
        //        }
        //    }
        //    private void PopulateALLExersiceNames()
        //    {
        //        if (crudManager.SelectedUser != null)
        //        {
        //            ExerciseNames.ItemsSource = crudManager.ReadExercise();
        //        }
        //    }
        //    private void AllUsersExercises_Click(object sender, RoutedEventArgs e)
        //    {
        //        PopulateALLExersiceNames();
        //    }

        //    private void PopulateSessionList()
        //    {
        //        if (crudManager.SelectedUser != null)
        //        {
        //            int userid = crudManager.SelectedUser.UserId;
        //            SessionList.ItemsSource = crudManager.SWOverview(userid, 1);
        //        }
        //    }
        //    private void PopulateMuscleGroupList()
        //    {
        //        if (crudManager.SelectedUser != null)
        //        {
        //            MuscleGroupList.ItemsSource = crudManager.ReadMuscleGroups();
        //        }
        //    }
        //    private void SessionList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        //    {
        //        if (SessionList.SelectedItem != null)
        //        {
        //            crudManager.SelectedSet = null;
        //            crudManager.SelectSession(SessionList.SelectedItem);
        //            InputDate.Text = crudManager.SelectedSession.ToString();
        //            ExerciseSelected.Text = crudManager.SelectedExercise.ToString();
        //            PopulateSetList();
        //        }
        //    }

        //    private void PopulateSetList()
        //    {
        //        if (crudManager.SelectedUser != null)
        //        {
        //            SetList.ItemsSource = crudManager.ReadSet();
        //        }
        //    }

        //    private void SetList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        //    {
        //        if (SetList.SelectedItem != null)
        //        {
        //            crudManager.SelectSet(SetList.SelectedItem);
        //            PopulateExerciseDetails();
        //        }
        //    }

        //    private void PopulateExerciseDetails()
        //    {
        //        if (SetList.SelectedItem != null)
        //        {
        //            InputReps.Text = $"Reps: {crudManager.SelectedSet.NumberofReps.ToString()}";
        //            InputWeight.Text = $"Weight: {crudManager.SelectedSet.Weight.ToString()}";
        //        }
        //    }


        //    private void ExersiceNames_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        //    {
        //        if (ExerciseNames.SelectedItem != null)
        //        {
        //            crudManager.SelectExercise((ExerciseNames.SelectedItem));
        //            ExerciseSelected.Text = crudManager.SelectedExercise.ExerciseName;
        //        }

        //    }

        //    private void MuscleGroupList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        //    {
        //        if (MuscleGroupList.SelectedItem != null)
        //        {
        //            crudManager.SelectMuscleGroup((MuscleGroupList.SelectedItem));
        //            OutSelecetedMuscleGroup.Text = crudManager.SelectedMuscleGroup.MuscleGroupName;
        //        }
        //    }



        //    private void AddUser_Click(object sender, RoutedEventArgs e)
        //    {
        //        if (InputNewUser.Text.Trim() != "")
        //        {
        //            crudManager.AddUser((InputNewUser.Text.Trim()));
        //            PopulateUserNames();
        //            InputNewUser.Text = "";
        //        }            
        //        else
        //        {
        //            ErrorBox.Text = "missing data";
        //        }
        //    }

        //    private void AddSet_Click(object sender, RoutedEventArgs e)
        //    {
        //        string inputreps = InputReps.Text.Trim();
        //        string inputweight = InputWeight.Text.Trim();
        //        if (inputreps != "" & InputWeight.Text != "" & crudManager.SelectedSession != null)
        //        {
        //            if (IsDigitsOnly(inputreps) == true & IsDigitsOnly(inputweight) == true)
        //            {
        //                crudManager.AddSets(Int32.Parse(inputreps), Int32.Parse(inputweight));
        //                InputReps.Text = "";
        //                InputWeight.Text = "";
        //                PopulateSetList();
        //            }
        //            else
        //            {
        //                ErrorBox.Text = "Sets and Weight must be Numbers";
        //            }

        //        }                
        //        else
        //        {
        //            ErrorBox.Text = "missing data";
        //        }

        //        bool IsDigitsOnly(string str)
        //        {
        //            foreach (char c in str)
        //            {
        //                if (c < '0' || c > '9')
        //                {
        //                    return false;
        //                }                        
        //            }
        //            return true;
        //        }
        //    }



        //    private void AddSession_Click(object sender, RoutedEventArgs e)
        //    {
        //        if (InputDate.Text.Trim() != "" & crudManager.SelectedExercise != null & crudManager.SelectedUser!= null)
        //        {
        //            crudManager.AddSession(InputDate.Text.Trim());
        //            InputDate.Text = "";
        //            PopulateSessionList();
        //        }            
        //        else
        //        {
        //            ErrorBox.Text = "missing data";
        //        }
        //    }
        //    private void AddExercise_Click(object sender, RoutedEventArgs e)
        //    {
        //        if (InputExerciseName.Text.Trim() != "" & crudManager.SelectedMuscleGroup != null)
        //        {
        //            crudManager.AddExercise(InputExerciseName.Text.Trim());
        //            PopulateUserExersiceNames();
        //            InputExerciseName.Text = "";
        //            var selex = crudManager.ReadExercise();
        //            crudManager.SelectedExercise = selex[selex.Count() - 1];
        //        }
        //        else
        //        {
        //            ErrorBox.Text = "missing data";
        //        }
        //    }

        //    private void RemoveUser_Click(object sender, RoutedEventArgs e)
        //    {
        //        if (crudManager.SelectedUser != null)
        //        {
        //            crudManager.RemoveUser();
        //            PopulateUserNames();
        //            PopulateUserExersiceNames();
        //            PopulateSessionList();
        //            PopulateSetList();
        //            crudManager.SelectedUser = null;
        //            crudManager.SelectedSession = null;
        //            crudManager.SelectedMuscleGroup = null;
        //            crudManager.SelectedExercise = null;
        //            crudManager.SelectedSet = null;
        //        }            
        //        else
        //        {
        //            ErrorBox.Text = "Nothing Selected";
        //        }
        //    }
        //    private void RemoveSession_Click(object sender, RoutedEventArgs e)
        //    {
        //        if (crudManager.SelectedSession != null)
        //        {
        //            crudManager.RemoveSession();
        //            PopulateSessionList();
        //            PopulateSetList();
        //            crudManager.SelectedSession = null;
        //            crudManager.SelectedMuscleGroup = null;
        //            crudManager.SelectedExercise = null;
        //            crudManager.SelectedSet = null;
        //        }
        //        else
        //        {
        //            ErrorBox.Text = "Nothing Selected";
        //        }

        //    }
        //    private void RemoveSet_Click(object sender, RoutedEventArgs e)
        //    {
        //        if (crudManager.SelectedSet != null)
        //        {
        //            crudManager.RemoveSet();
        //            PopulateSetList();
        //            crudManager.SelectedSet = null;
        //        }
        //        else
        //        {
        //            ErrorBox.Text = "Nothing Selected";
        //        }
        //    }
        //    private void RemoveExercise_Click(object sender, RoutedEventArgs e)
        //    {
        //        if (crudManager.SelectedExercise != null)
        //        {
        //            bool remove = true;
        //            var allsesions = crudManager.ReadAllSessions();
        //            foreach (var item in allsesions)
        //            {
        //                if (item.ExerciseId == crudManager.SelectedExercise.ExerciseId)
        //                {
        //                    ErrorBox.Text = "Exercise in use";
        //                    remove = false;
        //                    break;
        //                }
        //            }
        //            if (remove == true)
        //            {
        //                crudManager.RemoveExercise();
        //                PopulateUserExersiceNames();
        //                PopulateSessionList();
        //                PopulateSetList();
        //                crudManager.SelectedExercise = null;
        //                ClearAll();
        //            }                
        //        }
        //        else
        //        {
        //            ErrorBox.Text = "Nothing Selected";
        //        }
        //    }

        //    private void ClearAll()
        //    {
        //        InputDate.Text = "";
        //        ExerciseSelected.Text = "";
        //        UserSelected.Text = "";
        //        //.Text = "";
        //        //.Text = "";
        //        //.Text = "";
        //        //.Text = "";
        //        //.Text = "";
        //        //.Text = "";
        //        //.Text = "";

        //    }

    }
}
