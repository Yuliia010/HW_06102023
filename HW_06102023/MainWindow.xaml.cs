using RickAndMorty_API_CORE.Data.API;
using RickAndMorty_API_CORE.Domain.Models;
using RickAndMorty_API_CORE.Domain.ProviderJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
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

namespace HW_06102023
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int page = 1;
        private int maxPages;
        private RAM_API_ENGINE engine { get; set; }
        private List<Character> characters { get; set; }
        List<Character> allCharacters;
        List<Character> sorted;
        public MainWindow()
        {
            InitializeComponent();
            engine = new RAM_API_ENGINE();
            UpdatePage();
           

        }


        private async void UpdatePage()
        {
            ItemsContainer.ItemsSource = null;
            var pageResult = engine.GetPage(this.page);
            characters =  JsonProvider.FromJsonToCharacterList(pageResult);
            ItemsContainer.ItemsSource = characters;

            lb_maxPage.Content = maxPages.ToString();
            lb_curPage.Content = page.ToString();

            if (page ==1)
            {
                btn_Next.IsEnabled = true;
                btn_Prev.IsEnabled = false;
            }
            else if(page == maxPages) 
            {
                btn_Next.IsEnabled = false;
                btn_Prev.IsEnabled = true;

            }
            else
            {
                btn_Prev.IsEnabled = true;
                btn_Next.IsEnabled = true;
            }
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var pageResult = engine.GetPage(1);
            characters = JsonProvider.FromJsonToCharacterList(pageResult);
            ItemsContainer.ItemsSource = characters;
            maxPages = JsonProvider.MaxPagesInJson(pageResult);
            lb_maxPage.Content = maxPages.ToString();

            GetAllElements();
            createSortMenu();
            //MessageBox.Show($"{maxPages}");
        }

        private void btn_Prev_Click(object sender, RoutedEventArgs e)
        {
            if(page != 1)
            {
                page--;
                UpdatePage();
            }
        }

        private void btn_Next_Click(object sender, RoutedEventArgs e)
        {
            page++;
            UpdatePage();
        }

        private async void GetAllElements()
        {
            allCharacters = new List<Character>();
            for (int i = 1; i <= maxPages; i++)
            {
                var pageResult = engine.GetPage(i);
                allCharacters.AddRange(JsonProvider.FromJsonToCharacterList(pageResult));
            }
        }

        public List<Character> FindCharacterList(string findStr)
        {
            List<Character> result = new List<Character>();


            foreach (Character character in allCharacters)
            {
                //
                if (character.Name.ToLower().Contains(findStr.ToLower()) || character.Status.ToLower().Contains(findStr.ToLower()) || character.Species.ToLower().Contains(findStr.ToLower()) || character.Location.ToLower().Contains(findStr.ToLower()))
                {
                    result.Add(character);
                }
            }

            return result;


        }


        private void tb_Find_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ItemsContainer.ItemsSource = null;
                if (tb_Find.Text == "")
                {
                    UpdatePage();
                   
                }
                else
                {
                    //List<Character> FindCharacters = FindCharacterList(tb_Find.Text);

                    var pageResult = engine.GetFind(tb_Find.Text);
                    sorted = JsonProvider.FromJsonToCharacterList(pageResult);
                    ItemsContainer.ItemsSource = sorted;
                    lb_curPage.Content = string.Empty;
                    lb_maxPage.Content = string.Empty;
                    btn_Next.IsEnabled = false;
                    btn_Prev.IsEnabled = false;
                }
            }
        }

        private void tb_Find_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tb_Find.Text == "")
            {
                UpdatePage();
            }
        }


        private void createSortMenu()
        {
            foreach (Character character in allCharacters)
            {
                if (!m_Status.Items.OfType<MenuItem>().Any(item => string.Equals(item.Header.ToString(), character.Status, StringComparison.OrdinalIgnoreCase)))
                {
                    MenuItem statusMenuItem = new MenuItem();
                    statusMenuItem.Header = character.Status;
                    statusMenuItem.Style = (Style)FindResource("MenuStyle");
                    statusMenuItem.Click += menu_Click;
                    m_Status.Items.Add(statusMenuItem);
                }
                if (!m_Location.Items.OfType<MenuItem>().Any(item => string.Equals(item.Header.ToString(), character.Location, StringComparison.OrdinalIgnoreCase)))
                {
                    MenuItem locationMenuItem = new MenuItem();
                    locationMenuItem.Header = character.Location;
                    locationMenuItem.Style = (Style)FindResource("MenuStyle");
                    locationMenuItem.Click += menu_Click;
                    m_Location.Items.Add(locationMenuItem);
                }
                if (!m_Species.Items.OfType<MenuItem>().Any(item => string.Equals(item.Header.ToString(), character.Species, StringComparison.OrdinalIgnoreCase)))
                {
                    MenuItem speciesMenuItem = new MenuItem();
                    speciesMenuItem.Header = character.Species;
                    speciesMenuItem.Style = (Style)FindResource("MenuStyle");
                    speciesMenuItem.Click += menu_Click;
                    m_Species.Items.Add(speciesMenuItem);
                }
                if (!m_Gender.Items.OfType<MenuItem>().Any(item => string.Equals(item.Header.ToString(), character.Gender, StringComparison.OrdinalIgnoreCase)))
                {
                    MenuItem genderMenuItem = new MenuItem();
                    genderMenuItem.Header = character.Gender;
                    genderMenuItem.Style = (Style)FindResource("MenuStyle");
                    genderMenuItem.Click += menu_Click;
                    m_Gender.Items.Add(genderMenuItem);
                }
            }


        }


        private void menu_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            
            MenuItem parent = menuItem.Parent as MenuItem;
            ItemsContainer.ItemsSource = null;
            menu_sort.Header = menuItem.Header;

            if (menuItem.Header.Equals("All"))
            {
                UpdatePage();
            }
            else
            {
                var pageResult = engine.GetSorted(menuItem.Header.ToString().ToLower(), parent.Header.ToString().ToLower());
                sorted = JsonProvider.FromJsonToCharacterList(pageResult);
                ItemsContainer.ItemsSource = sorted;
                lb_curPage.Content = string.Empty;
                lb_maxPage.Content = string.Empty;
                btn_Next.IsEnabled = false;
                btn_Prev.IsEnabled = false;
            }

        }
    }
}
