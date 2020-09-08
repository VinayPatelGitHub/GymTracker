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
            PopulateUserPage();
            PopulateSessionsDetailsPage();
        }
        private void OkayButton_Click(object sender, RoutedEventArgs e)
        {
            if (PageName.Text == "User Page" & crudManager.SelectedUser != null)
            {
                PopulateWorkoutPage();
                OkayButton.Visibility = Visibility.Hidden;
                MainButtonRemove.Visibility = Visibility.Hidden;
            }
            else if ((string)OkayButton.Content == "All User Exercises")
            {
                MainListBox.ItemsSource = crudManager.ReadAllExercise();
                OkayButton.Content = "Okay";
                OkayButton.Visibility = Visibility.Hidden;
                MainButtonRemove.Visibility = Visibility.Hidden;
            }
            else if (PageName.Text == "Add Workout Page" & MainListBox.SelectedItem != null)
            {
                crudManager.AddandSelectSession(DateSelected.Text);
                SetListBox.ItemsSource = crudManager.ReadSessionSets();
                PopulateSessionsDetailsPage();
                PopulateWorkoutPage();
                OkayButton.Visibility = Visibility.Hidden;
                MainButtonRemove.Visibility = Visibility.Hidden;
            }
            else if (PageName.Text == "New Exercise Page" & MainListBox.SelectedItem != null)
            {
                crudManager.SelectMuscleGroup(MainListBox.SelectedItem);
                crudManager.AddandSelectExercise(InputString.Text);
                crudManager.AddandSelectSession(DateSelected.Text);
                PopulateSessionsDetailsPage();
                PopulateWorkoutPage();
                MainButtonRemove.Visibility = Visibility.Hidden;
                OkayButton.Visibility = Visibility.Hidden;
                InputString.Text = "";
            }
        }

        private void MainListBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (PageName.Text == "User Page" & MainListBox.SelectedItem != null)
            {
                crudManager.SelectUser(MainListBox.SelectedItem);
                OkayButton.Visibility = Visibility.Visible;
                MainButtonRemove.Visibility = Visibility.Visible;
                MainButtonRemove.Content = "Remove User?";
            }
            else if (PageName.Text == "Workouts Page" & MainListBox.SelectedItem != null)
            {                
                crudManager.SelectSession(MainListBox.SelectedItem);
                crudManager.SelectExerciseFromSession();
                PopulateSessionsDetailsPage();
                MainButtonRemove.Visibility = Visibility.Visible;
                MainButtonRemove.Content = "Remove Workout?";
            }
            else if (PageName.Text == "Add Workout Page" & MainListBox.SelectedItem != null)
            {
                crudManager.SelectExercise(MainListBox.SelectedItem);                
                MainButtonRemove.Visibility = Visibility.Visible;
                MainButtonRemove.Content = "Remove Exercise?";
                OkayButton.Content = "Okay";
                OkayButton.Visibility = Visibility.Visible;
            }
            else if (PageName.Text == "New Exercise Page" & MainListBox.SelectedItem != null)
            {
                OkayButton.Visibility = Visibility.Visible;
            }
        }

        private void PopulateUserPage()
        {
            MainListBox.ItemsSource = crudManager.ReadUser();
            UserSelected.Text = "";
            MainTextBox.Text = "Select User to get started:";
            InputLabel.Text = "Enter New User Name:";
            MainButtonAdd.Visibility = Visibility.Visible;
            MainButtonAdd.Content = "Add New User";
            MainButtonRemove.Content = "";
            PageName.Text = "User Page";
            ErrorMessage.Text = "";
            BackButton.Visibility = Visibility.Hidden;
            OkayButton.Visibility = Visibility.Hidden;
            MainButtonRemove.Visibility = Visibility.Hidden;
        }

        private void PopulateWorkoutPage()
        {
            MainListBox.ItemsSource = crudManager.ReadUserSessions();
            MainTextBox.Text = "Select Session to View Previous Workout:";
            InputLabel.Text = "Enter date:";
            BackButton.Visibility = Visibility.Visible;
            MainButtonAdd.Visibility = Visibility.Visible;
            MainButtonAdd.Content = "Start Workout?";
            PageName.Text = "Workouts Page";
            UserSelected.Text = crudManager.SelectedUser.UserName.ToString();
        }
        private void PopulateAddWorkoutPage()
        {
            MainListBox.ItemsSource = crudManager.ReadUserExercise();
            MainTextBox.Text = "Select the Exercise you want to do:";
            InputLabel.Text = "Enter New Exercise Name:";
            InputString.Text = "";
            MainButtonAdd.Visibility = Visibility.Visible;
            MainButtonRemove.Visibility = Visibility.Hidden;
            OkayButton.Visibility = Visibility.Visible;
            OkayButton.Content = "All User Exercises";
            MainButtonAdd.Content = "Add Exercise";
            PageName.Text = "Add Workout Page";
        }

        private void PopulateNewExercisePage()
        {
            MainListBox.ItemsSource = crudManager.ReadMuscleGroups();
            MainTextBox.Text = "Select the Main Muscle Group for this Exercise:";
            InputLabel.Text = "Last chance to change Exercise Name";
            MainButtonAdd.Visibility = Visibility.Hidden;
            OkayButton.Visibility = Visibility.Hidden;
            MainButtonRemove.Visibility = Visibility.Hidden;
            PageName.Text = "New Exercise Page";
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
                    crudManager.SelectedSession = null;
                    PopulateSessionsDetailsPage();
                    DateSelected.Text = InputString.Text;
                    PopulateAddWorkoutPage();
                    InputString.Text = "";
                    ErrorMessage.Text = "";
                    MainButtonRemove.Content = "All User Exercises";
                }
            }
            else if (PageName.Text == "Add Workout Page")
            {
                if (InputString.Text != "")
                {
                    crudManager.SelectedExercise = null;
                    crudManager.SelectedSession = null;
                    PopulateNewExercisePage();
                    OkayButton.Content = "Okay";
                }
                else
                {
                    ErrorMessage.Text = "Add an Exercise Names";
                }
            }
        }

        private void MainButtonRemove_Click(object sender, RoutedEventArgs e)
        {
            if ((string)MainButtonRemove.Content == "Remove User?")
            {
                if (crudManager.SelectedUser != null)
                {
                    crudManager.RemoveUser();       
                    PopulateSessionsDetailsPage();
                    PopulateUserPage();
                }
            }
            else if ((string)MainButtonRemove.Content == "Remove Workout?")
            {
                if (crudManager.SelectedSession != null)
                {
                    crudManager.RemoveSession();
                    PopulateSessionsDetailsPage();
                    PopulateWorkoutPage();
                }
            }
            else if ((string)MainButtonRemove.Content == "Remove Exercise?")
            {
                if (crudManager.SelectedExercise != null)
                {
                    crudManager.RemoveExercise();    //could cause errors if used else where
                    PopulateAddWorkoutPage();
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
                if (crudManager.IsDigitsOnly(inputreps) == true & crudManager.IsDigitsOnly(inputweight) == true)
                {
                    crudManager.AddandSelectSets(Int32.Parse(inputreps), Int32.Parse(inputweight));
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

        }

        private void UpdateSet_Click(object sender, RoutedEventArgs e)
        {
            string inputreps = RepInput.Text.Trim();
            string inputweight = WeightInput.Text.Trim();
            if (inputreps != "" & inputweight != "" & crudManager.SelectedSession != null)
            {
                if (crudManager.IsDigitsOnly(inputreps) == true & crudManager.IsDigitsOnly(inputweight) == true)
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
        }

        private void RemoveSet_Click(object sender, RoutedEventArgs e)
        {
            if (crudManager.SelectedSet != null)
            {
                crudManager.RemoveSet();
                RepInput.Text = "";
                WeightInput.Text = "";
                PopulateSessionsDetailsPage();
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
            }
            else if (PageName.Text == "Workouts Page")
            {
                crudManager.SelectedUser = null;
                crudManager.SelectedSession = null;
                crudManager.SelectedSet = null;
                MainButtonRemove.Visibility = Visibility.Hidden;
                PopulateUserPage();
                PopulateSessionsDetailsPage();
            }
            else if (PageName.Text == "Add Workout Page")
            {
                OkayButton.Content = "Okay";
                OkayButton.Visibility = Visibility.Hidden;
                MainButtonRemove.Visibility = Visibility.Hidden;
                PopulateWorkoutPage();
                PopulateSessionsDetailsPage();
            }
            else if (PageName.Text == "New Exercise Page")
            {
                PopulateAddWorkoutPage();
                MainButtonRemove.Visibility = Visibility.Hidden;
            }

        }
    }
}
