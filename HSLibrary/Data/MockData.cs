using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using HSLibrary.Models;
using HSLibrary.Models.Dinghy;
using HSLibrary.Models.Dinghy.Motorized;
using Microsoft.VisualBasic.FileIO;

namespace HSLibrary.Data
{
    public class MockData
    {
        #region Instance fields
        private static Dictionary<int, Member> _memberData =
            new Dictionary<int, Member>()
            {
                { 0, new Member("Mikkel","Barfod", "gg@gamermail.com", new DateOnly(1999, 12, 15) , "20202020") },
                { 1, new Member("Chad","Gaylord Smith", "LordOfTheGaymers@gamermail.com", new DateOnly(1961,10,25), "42069420") },
                { 2, new Member("Carina", "Carina", "CarinaDenFlotte.420@gmail.com", new DateOnly(1892,11,02), "60606969" )},
                { 3, new Member("Theodore", "Kaczynski", "UncleTed@Boston.com", new DateOnly (1942, 5, 22), "10062023")},
                { 4, new Member("Steve", "Minecraft", "GettingDiamonds@Mojang.com", new DateOnly (2014, 3, 25), "12341234")}
            };

        private static Dictionary<int, Team> _teamData =
            new Dictionary<int, Team>
            {
                { 0, new Team(_memberData[0], 8) },
                { 1, new Team(_memberData[0], 8) },
                { 2, new Team(_memberData[1], 10) }
            };

        private static Dictionary<int, Dinghy> _dinghyData =
            new Dictionary<int, Dinghy>()
            {
                {0, new Dinghy(DinghyModel.Feva, "blå fugl")},
                {1, new Dinghy(DinghyModel.Europajolle, "Statue af Europa")},
                {2, new Dinghy(DinghyModel.Tera, "Sejl")},
                {3, new Dinghy(DinghyModel.Lynæs, "Motor, oppustelig gummi båd")},
                {4, new Dinghy(DinghyModel.Lynæs, "Hele Lynæs")},
                {5, new Dinghy(DinghyModel.Optimistjolle , "Oppustelig gummi and")},
                {6, new Dinghy(DinghyModel.Snipejolle, "blå snipe (en fugl)")},
                {7, new MDinghy("Den har fandme lasere",FuelType.BioDiesel ,5000, 2, 420)}
            };

        private static Dictionary<int, Booking> _bookingData =
            new Dictionary<int, Booking>()
            {
                {0, new Booking(new DateTime(2024,12,12,13,30,0), new TimeSpan(1,30,0), _memberData[0], _dinghyData[0])},
                {1, new Booking(new DateTime(2024,12,12,15,00,0), new TimeSpan(2,0,0), _memberData[2], _dinghyData[3])},
                {2, new Booking(new DateTime(2024,12,20,13,30,0), new TimeSpan(0,15,0), _memberData[3], _dinghyData[1])},
                {3, new Booking(new DateTime(2024,12,24,0,0,0), new TimeSpan(3,30,0), _memberData[1], _dinghyData[4])},
                {4, new Booking(new DateTime(2024,12,23,0,0,0), new TimeSpan(3,30,0), _memberData[1], _dinghyData[4])}
            };

        private static Dictionary<int, Blog> _blogData =
        new Dictionary<int, Blog>()
        {
                {0, new Blog(_memberData[0], "Jans kartoffel", "Jan er en sur kartoffel") },
                {1, new Blog(_memberData[1], "Flask potat", "Vi sælger Potat på flask") },
                {2, new Blog(_memberData[2], "Mødre meeting" +
                    "", "Alle mødre bliver inviteret til at komme og rydde op efter deres børn") }
            };

        private static Dictionary<int, Event> _eventData =
            new Dictionary<int, Event>()
            {
                {0, new Event(new DateTime(2024, 12, 31, 20, 30, 0), "Nytårsfest", "Nytårsfest for Hillerød sejlklub", _memberData[3]) },
                {1, new Event(new DateTime(2025, 4, 18, 14, 30, 0), "Korsfæstelse", "Korsfæstelse for Hans Jegner", _memberData[1], 2) }
            };

        #endregion
        private static void Misc()
        {
            _memberData[0].IsActiveTrainer = true;
            _memberData[1].IsActiveTrainer = true;
            _memberData[2].IsActiveTrainer = true;
            _memberData[1].IsAdmin = true;
            _memberData[1].HasKey = true;
            _dinghyData[2].NeedRepair("Der er en Goblin gemt i jollen");
            _dinghyData[7].NeedRepair("Motoren bryder termodynamiske love");
            _dinghyData[7].RepairDinghy("Stop med at klag over FTL motoren", "FTL motoren, lavet af Jan, er et videnskabeligt vidunder og du kan ikke undgå en smule antimatter når den bruges.");
        }

        #region Properties
        public static Dictionary<int, Member> MemberData
        {
            get
            {
                Misc();
                return _memberData;
            }
        }


        public static Dictionary<int, Team> TeamData
        {
            get
            {
                Misc();
                return _teamData;
            }
        }

        public static Dictionary<int, Dinghy> DinghyData
        {
            get
            {
                Misc();
                return _dinghyData;
            }
        }

        public static Dictionary<int, Booking> BookingData
        {
            get
            {
                Misc();
                return _bookingData;
            }
        }

        public static Dictionary<int, Blog> BlogData
        {
            get
            {
                Misc();
                return _blogData;
            }
        }

        public static Dictionary<int, Event> EventData
        {
            get
            {
                Misc();
                return _eventData;
            }
        }
        #endregion

    }
}
