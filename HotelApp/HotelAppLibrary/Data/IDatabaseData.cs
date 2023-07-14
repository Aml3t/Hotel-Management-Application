﻿using HotelAppLibrary.Models;
using System.Collections.Generic;
using System;

namespace HotelAppLibrary.Data
{
    public interface IDatabaseData
    {
        void BookGuest(string FirstName, string LastName, DateTime startDate, DateTime endDate, int roomTypeId);
        void CheckInGuest(int bookingId);
        List<RoomTypeModel> GetAvailableRoomTypes(DateTime startDate, DateTime endDate);
        RoomTypeModel GetRoomTypeById(int id);
        List<BookingFullModel> SearchBookings(string lastName);
    }
}