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




        public int TotalRoom { get; set; }
        public int TotalFree { get; set; }
        public int TotalBook { get; set; }
        public int TotalBlock { get; set; }
        public int TotalCheckIn { get; set; }
        public int ExchangeRateID { get; set; }
        public int WaterPowerPriceID { get; set; }


        

    }
}