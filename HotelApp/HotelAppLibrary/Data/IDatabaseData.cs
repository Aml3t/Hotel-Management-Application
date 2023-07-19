using HotelAppLibrary.Models;
using System.Collections.Generic;
using System;

namespace HotelAppLibrary.Data
{
    public interface IDatabaseData
    {
        List<RoomTypeModel> GetAvailableRoomTypes(DateTime startDate, DateTime endDate);
        void BookGuest(string FirstName, string LastName, DateTime startDate, DateTime endDate, int roomTypeId);
        List<BookingFullModel> SearchBookings(string lastName);
        void CheckInGuest(int bookingId);
        RoomTypeModel GetRoomTypeById(int id);
    }
}