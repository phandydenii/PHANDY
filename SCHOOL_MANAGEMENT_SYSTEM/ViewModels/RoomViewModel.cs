using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SCHOOL_MANAGEMENT_SYSTEM.Models;

namespace SCHOOL_MANAGEMENT_SYSTEM.ViewModels
{
    public class RoomViewModel
    {
        public IEnumerable<RoomType> RoomTypes { get; set; }
        public IEnumerable<Building> Buildings { get; set; }
        public IEnumerable<Floor> Floors { get; set; }
        public IEnumerable<Room> Rooms { get; set; }
        public IEnumerable<Room> ListRoomFree { get; set; }
        public IEnumerable<Room> ListRoomBook { get; set; }
        public IEnumerable<Item> Items { get; set; }

        public IEnumerable<Guest> GuestBook { get; set; }
        public IEnumerable<Guest> GuestList { get; set; }
        public IEnumerable<Item> ItemList { get; set; }
        public IEnumerable<Staff> StaffList { get; set; }
        public IEnumerable<ExchangeRate> ExchangeRate { get; set; }

        public IEnumerable<Room> Room0 { get; set; }
        public IEnumerable<Room> RoomF1 { get; set; }
        public IEnumerable<Room> RoomF2 { get; set; }
        public IEnumerable<Room> RoomF3 { get; set; }
        public IEnumerable<Room> RoomF4 { get; set; }
        public IEnumerable<Room> RoomF5 { get; set; }


        public IEnumerable<Floor> Floor { get; set; }
        public IEnumerable<Floor> TotalFloor { get; set; }
        public IEnumerable<Room> TotalRoomFloor { get; set; }

        


        public int TotalRoom { get; set; }
        public int TotalFree { get; set; }
        public int TotalBook { get; set; }
        public int TotalBlock { get; set; }
        public int TotalCheckIn { get; set; }
        public int ExchangeRateID { get; set; }
        public int WaterPowerPriceID { get; set; }
        //public int TotalFloor { get; set; }
    }
}