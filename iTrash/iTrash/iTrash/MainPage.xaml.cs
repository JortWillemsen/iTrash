using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace iTrash
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        bool? hasConnection;
        ContainerAgent containerAgent = new ContainerAgent();
        public MainPage()
        {

            InitializeComponent();

            BindingContext = this;
            hostipentry.Text = Application.Current.Properties["HostIP"] as string;

            UpdateListView();

        }

        async Task UpdateListView()
        {
            while (true)
            {
                hasConnection = Application.Current.Properties["HasConnection"] as bool?;
                cLV.ItemsSource = Application.Current.Properties["ContainerList"] as List<Container>;
                if (hasConnection == true)
                {
                    connectedText.Text = "Connected to service";
                    connectedText.TextColor = Color.Green;
                }
                else
                {
                    connectedText.Text = "ERROR connecting to service";
                    connectedText.TextColor = Color.Red;
                }

                await Task.Delay(200);
            }
        }

        void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Container selectedItem = e.SelectedItem as Container;
        }

        void OnListViewItemTapped(object sender, ItemTappedEventArgs e)
        {
            Container tappedItem = e.Item as Container;
            
            Navigation.PushModalAsync(new Page1(e.Item as Container));
        }

        void Button_Clicked(object sender, EventArgs e)
        {
            cLV.ItemsSource = containerAgent.RefreshNow();
        }

        private void hostipentry_TextChanged(object sender, TextChangedEventArgs e)
        {
            Application.Current.Properties["HostIP"] = hostipentry.Text;
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            List<Container> specific_list = new List<Container>();

            specific_list = containerAgent.SearchContainer("?name=" + searchBar.Text);

            if(specific_list.Count > 0)
            {
                Navigation.PushModalAsync(new Page1(specific_list[0]));
            }
            else
            {
                DisplayAlert("ERROR", "Kan geen container vinden met die naam.", "OK");
            }

            
        }
    }
}