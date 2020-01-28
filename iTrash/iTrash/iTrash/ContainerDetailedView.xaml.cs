using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace iTrash
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {

        public Page1(Container container)
        {


            InitializeComponent();

            Navigation.PopAsync();

            if (container.name != null && container.capaciy != null)
            {
                Double usedCapacityReal;
                usedCapacityReal = container.used_capacity * Convert.ToDouble(container.capaciy);
                usedCapacityReal = Math.Round(usedCapacityReal, 1);
                capacityBar.Progress = container.used_capacity;
                capacityText.Text = "Capaciteit: " + container.capaciy + "L";
                containerNameText.Text = container.name;
                containerAdresText.Text = container.location;
                OwnerText.Text = "Eigenaar: " + container.owner;
                useText.Text = "Capaciteit in gebruik: " + usedCapacityReal.ToString() + "L";
            }
        }

        private void buttonBack_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new MainPage());
            
        }
    }
}