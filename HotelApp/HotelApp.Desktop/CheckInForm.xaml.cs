using HotelAppLibrary.Data;
using HotelAppLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace HotelApp.Desktop
{
    /// <summary>
    /// Interaction logic for CheckInPage.xaml
    /// </summary>
    public partial class CheckInForm : Window
    {
        private BookingFullModel _data = null;

        public CheckInForm()
        {
            InitializeComponent();
            
        }

        public void PopulateCheckInInfo(BookingFullModel data)
        {
            _data = data;
        }


        private void checkInId_Click(object sender, RoutedEventArgs e)
        {
            //bool checkIn = _db.CheckInGuest(checkInId);
        }
    }
}
