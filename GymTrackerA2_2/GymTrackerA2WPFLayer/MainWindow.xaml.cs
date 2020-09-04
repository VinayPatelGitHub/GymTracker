using GymTrackerBusiness;
using GymTrackerModel;
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
                crudManager.SelectUser(UserNames.SelectedItem);
                PopulateSixWeekOverivew();
                PopulateUserExersiceNames();
                PopulateSessionList();
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
                SixWeekOverview.Text = dates.ToString();

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

        private void PopulateSessionList()
        {
            if (crudManager.SelectedUser != null)
            {
                int userid = crudManager.SelectedUser.UserId;
                SessionList.ItemsSource = crudManager.SWOverview(userid, 1);
            }
        }

        private void SessionList_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (SessionList.SelectedItem != null)
            {
                crudManager.SelectSession(SessionList.SelectedItem);
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
                Exercise.Text = "Empty1";
                Sets.Text = "Empty2";
                Reps.Text = $"Reps: {crudManager.SelectedSet.NumberofReps.ToString()}";
                Weight.Text = $"Weight: {crudManager.SelectedSet.Weight.ToString()}";
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

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            crudManager.AddUser((InputNewUser.Text));
            PopulateUserNames();
            InputNewUser.Text = "";
        }

        private void AddSet_Click(object sender, RoutedEventArgs e)
        {
            crudManager.AddSets(Int32.Parse(InputReps.Text), Int32.Parse(InputWeight.Text));
            InputReps.Text = "";
            InputWeight.Text = "";
            PopulateSetList();
        }

        
        private void AllUsersExercises_Click(object sender, RoutedEventArgs e)
        {
            PopulateALLExersiceNames();
        }

        private void AddSession_Click(object sender, RoutedEventArgs e)
        {
            crudManager.AddSession(InputDate.Text);
            InputDate.Text = "";
            PopulateSessionList();
        }
        private void RemoveUser_Click(object sender, RoutedEventArgs e)
        {
            crudManager.RemoveUser();
            PopulateUserNames();
            PopulateUserExersiceNames();
            PopulateSessionList();
            PopulateSetList();
            crudManager.SelectedUser = null;
            crudManager.SelectedExercise = null;
            crudManager.SelectedSession = null;
            crudManager.SelectedSet = null;
        }
        private void RemoveSession_Click(object sender, RoutedEventArgs e)
        {
            crudManager.RemoveSession();
            PopulateSessionList();
            PopulateSetList();
            crudManager.SelectedSession = null;
            crudManager.SelectedSet = null;
        }

        private void RemoveSet_Click(object sender, RoutedEventArgs e)
        {
            crudManager.RemoveSet();
            PopulateSetList();
            crudManager.SelectedSet = null;
        }

    }
}
