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

namespace Muftahov_glazki
{
    /// <summary>
    /// Логика взаимодействия для AgentPage.xaml
    /// </summary>
    public partial class AgentPage : Page
    {
        int CountRecords;
        int CountPage;
        int CurrentPage;
        List<Agent> CurrentPageList = new List<Agent>();
        List<Agent> TableList;
        public AgentPage()
        {
            InitializeComponent();
            var currentServices = muftahov_eyesEntities.GetContext().Agent.ToList();
            AgentListView.ItemsSource = currentServices;
            ComboType.SelectedIndex = 0;
            ComboType1.SelectedIndex = 0;
            UpdateServices();
        }
        private void UpdateServices()
        {

            var currentServices = muftahov_eyesEntities.GetContext().Agent.ToList();

            if (ComboType.SelectedIndex == 0)
            {
                currentServices = currentServices.Where(p => (p.AgentTypeID == p.AgentTypeID)).ToList();
            }
            if (ComboType.SelectedIndex == 1)
            {
                currentServices = currentServices.Where(p => (p.AgentTypeID == 1)).ToList();
            }
            if (ComboType.SelectedIndex == 2)
            {
                currentServices = currentServices.Where(p => (p.AgentTypeID == 2)).ToList();
            }
            if (ComboType.SelectedIndex == 3)
            {
                currentServices = currentServices.Where(p => (p.AgentTypeID == 3)).ToList();
            }
            if (ComboType.SelectedIndex == 4)
            {
                currentServices = currentServices.Where(p => (p.AgentTypeID == 4)).ToList();
            }
            if (ComboType.SelectedIndex == 5)
            {
                currentServices = currentServices.Where(p => (p.AgentTypeID == 5)).ToList();
            }
            if (ComboType.SelectedIndex == 6)
            {
                currentServices = currentServices.Where(p => (p.AgentTypeID == 6)).ToList();
            }

            currentServices = currentServices.Where(z => z.Title.ToLower().Contains(TBoxSearch.Text.ToLower()) || z.Phone.Replace("-", "").Replace(" ", "").Replace(")", "").Replace("(", "").ToLower().Contains(TBoxSearch.Text.Replace(" ", "").Replace(")", "").Replace("(", "").Replace("-", "").ToLower()) || z.Email.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();

            if (ComboType1.SelectedIndex == 0)
            {
                currentServices = currentServices.Where(p => (p.Title == p.Title)).ToList();
            }
            if (ComboType1.SelectedIndex == 1)
            {
                currentServices = currentServices.OrderBy(p => p.Title).ToList();
            }
            if (ComboType1.SelectedIndex == 2)
            {
                currentServices = currentServices.OrderByDescending(p => p.Title).ToList();
            }
            if (ComboType1.SelectedIndex == 3)
            {

            }
            if (ComboType1.SelectedIndex == 4)
            {

            }
            if (ComboType1.SelectedIndex == 5)
            {
                currentServices = currentServices.OrderBy(p => p.Priority).ToList();
            }
            if (ComboType1.SelectedIndex == 6)
            {
                currentServices = currentServices.OrderByDescending(p => p.Priority).ToList();
            }
            AgentListView.ItemsSource = currentServices;
            TableList = currentServices;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPage());
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                muftahov_eyesEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                AgentListView.ItemsSource = muftahov_eyesEntities.GetContext().Agent.ToList();
            }
            UpdateServices();
        }
        private void ChangePage(int direction, int? selectedPage)
        {
            CurrentPageList.Clear();
            CountRecords = TableList.Count;
            if (CountRecords % 10 > 0)
            {
                CountPage = CountRecords / 10 + 1;
            }
            else { CountPage = CountRecords / 10; }
            Boolean Ifupdate = true;
            int min;
            if (selectedPage.HasValue)
            {
                if (selectedPage >= 0 && selectedPage <= CountPage)
                {
                    CurrentPage = (int)selectedPage;
                    min = CurrentPage * 10 + 10 < CountRecords ? CurrentPage * 10 + 10 : CountRecords;
                    for (int i = CurrentPage * 10; i < min; i++)
                    {
                        CurrentPageList.Add(TableList[i]);
                    }
                }
            }
            else
            {
                switch (direction)
                {
                    case 1:
                        if (CurrentPage > 0)
                        {
                            CurrentPage--;
                            min = CurrentPage * 10 + 10 < CountRecords ? CurrentPage * 10 + 10 : CountRecords;
                            for (int i = CurrentPage * 10; i < min; i++)
                            {
                                CurrentPageList.Add(TableList[i]);
                            }
                        }
                        else
                        {
                            Ifupdate = false;
                        }
                        break;
                    case 2:
                        if (CurrentPage < CountPage - 1)
                        {
                            CurrentPage++;
                            min = CurrentPage * 10 + 10 < CountRecords ? CurrentPage * 10 + 10 : CountRecords;
                            for (int i = CurrentPage * 10; i < min; i++)
                            {
                                CurrentPageList.Add(TableList[i]);
                            }
                        }
                        else
                        {
                            Ifupdate = false;
                        }
                        break;
                }
            }
            if (Ifupdate)
            {
                PageListBox.Items.Clear();
                for (int i = 1; i <= CountPage; i++)
                {
                    PageListBox.Items.Add(i);
                }
                PageListBox.SelectedIndex = CurrentPage;
                AgentListView.ItemsSource = CurrentPageList;
                AgentListView.Items.Refresh();
            }
        }

        private void LeftDirButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PageListBox_MouseUp(object sender, MouseButtonEventArgs e)
        {
            UpdateServices();
        }

        private void RightDirButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateServices();
        }

        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateServices();
        }

        private void ComboType1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateServices();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
