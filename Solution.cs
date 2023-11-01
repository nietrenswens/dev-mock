
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;

class Solution
{

    public static void q1Solution(HotelContext db)
    {
        // Exercise 1: Give the booking detail of given guest booking details (for GuestID 10).  
        // The result should include booking date, room number, and number of nights. 	
        var result = from booking in db.bookings
                     where booking.GuestID == 10
                     select new { Date = booking.BookingDate, Room = booking.RoomNumber, Nights = booking.Nights };
        foreach (var booking in result)
        {
            System.Console.WriteLine($"{booking.Date}, {booking.Room}, {booking.Nights}");
        }
    }

    public static void q2Solution(HotelContext db, DateOnly date)
    {
        // Exercise: 2:  List down all the guest names, and room number, 
        // having booking on specific date (2022 - 01 - 31) 	
        var results = from b in db.bookings
                        join gu in db.guests on b.GuestID equals gu.Id
                        orderby gu.Id
                        where b.BookingDate == date
                        select new {Id = gu.Id ,Guest = gu.Name, Room = b.RoomNumber};
        foreach(var b in results)
        {
            System.Console.WriteLine($"{b.Id}, {b.Guest}, {b.Room}");
        }


    }

    public static void q3Solution(HotelContext db)
    {
        // Exercise 3: List down number of bookings per day where there are more than 1 bookings
        var results = from b in db.bookings
                        group b by b.BookingDate into grp
                        where grp.Count() > 1
                        orderby grp.Key
                        select new {Date = grp.Key, Count = grp.Count()};
        foreach(var b in results)
        {
            System.Console.WriteLine($"{b.Date}, {b.Count}");
        }
    }

    public static void q4Solution(HotelContext db, DateOnly date)
    {
        // Exercise 4. List the rooms that are free on '2022-01-13'.
        var roomnumbers = db.rooms.OrderBy(room => room.Number).Select(_ => _.Number).ToList();
        var reservedroomnumbers = db.bookings.Where(_ => _.BookingDate == date).Select(_ => _.RoomNumber).ToList();
        var freerooms = roomnumbers.Except(reservedroomnumbers);
        foreach(var roomnum in freerooms)
        {
            System.Console.WriteLine(roomnum);
        }
    }

    public static void q5Solution(HotelContext db)
    {
        // Exercise: 5:  List down top 5 valued customers, with their id and spending 
        // HINT: a valued customer is the on with max amount spent, 
        // amount = Nights * Price for each booking of a customer
        var result = from b in db.bookings
                    join r in db.rooms on b.RoomNumber equals r.Number
                    join rt in db.roomType on r.RoomTypeId equals rt.Id
                    group b by b.GuestID into grp
                    select new {id = grp.Key, Sum = grp.Sum(_ => _.Nights * _.room.roomType.Price)};

        var top5 = result.OrderByDescending(_ => _.Sum).Take(5).ToList();
        top5.ForEach(_ => System.Console.WriteLine($"{_.id}, {_.Sum}"));
    }

    public static void q6Solution(HotelContext db, DateOnly date)
    {
        // Exercise 6: 

    }

    //     ****  Ex7 in Model.cs  **** 

    public static void q8aSolution(HotelContext db)
    {
        // Exercise 8a: 

    }

    public static void q8bSolution(HotelContext db)
    {
        // Exercise 8b: 

    }

}