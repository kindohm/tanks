using System.Windows;
using System.Windows.Controls;
using Tanks.Web.GameObjects;
using System.Linq;
namespace Tanks.Web.UI
{
    public partial class WeaponDisplay : UserControl
    {
        Vehicle vehicle;

        public WeaponDisplay(Vehicle vehicle)
        {
            this.vehicle = vehicle;
            InitializeComponent();
            this.Loaded += new System.Windows.RoutedEventHandler(WeaponDisplay_Loaded);
        }

        void WeaponDisplay_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            this.vehicle.WeaponChanged += new System.EventHandler(vehicle_WeaponChanged);
            this.weaponList.Items.Clear();
            this.weaponList.ItemsSource = this.vehicle.Weapons;
            this.vehicle_WeaponChanged(null, null);
        }

        void vehicle_WeaponChanged(object sender, System.EventArgs e)
        {
            //var weapons =
            //    from weapon in this.weaponList.Items
            //    where weapon == this.vehicle.CurrentWeapon
            //    select weapon;
            this.weaponList.SelectedItem = this.vehicle.CurrentWeapon;
           

            

        }
    }
}
