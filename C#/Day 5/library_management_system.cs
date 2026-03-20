using System;
using LI= LibrarySystem.Items;
using LU= LibrarySystem.Users;
namespace LibrarySystem
{
    
    namespace Items
    {
        abstract class LibraryItem()
        {
            public string Title{get; set;}
            public string Author{get ; set;}
            public int ItemID{get; set;}
            public int DaysLate{get; set;}

            public abstract void display();
            public abstract void late_fee();
        }

        interface IReservable
        {
            void reserve();
            void msg(string str);
        }
        interface INotifiable
        {
            void reserve();
            void msg(string str);
        }

        class Book:LibraryItem,IReservable,INotifiable
        {
            public override void display()
            {
                Console.WriteLine("Author: "+Author);
                Console.WriteLine("Title: "+Title);
                Console.WriteLine("ItemID: "+ItemID);
            }
            public override void late_fee()
            {
                Console.WriteLine("Late Fees: "+(DaysLate*1)+"Rs");
            }
            public string str;
            public void reserve()
            {
                Console.WriteLine("Reservation success");
            }
            public void msg(string str)
            {
                this.str=str;
                Console.WriteLine("Notification message sent");
            }
        }
        class Magazine:LibraryItem
        {
            public override void display()
            {
                Console.WriteLine("Author:"+Author);
                Console.WriteLine("Title:"+Title);
                Console.WriteLine("ItemID"+ItemID);
            }
            public override void late_fee()
            {
                Console.WriteLine("Late Fees:"+(DaysLate*0.5)+"Rs");
            }

        }
    }

    namespace Users
    {
        enum UserRole
        {
            Admin,Librarian,Member  
        }
        enum ItemStatus
        {
            Available,Borrowed,Reserved,Lost
        }
        class Member
        {
            public string name{get; set;}
            UserRole role=UserRole.Member;
            ItemStatus status = ItemStatus.Borrowed;
            public void display()
            {
                Console.WriteLine("User Role: "+role);
                Console.WriteLine("Item Status: "+status);
                
            }
        }
        partial class LibraryAnalytics
        {
            public static int TotalBorrowedItems{set; get;}
        }
        partial class LibraryAnalytics
        {
            public static void Display()
            {
                Console.WriteLine("Total no of borrowed item: "+TotalBorrowedItems);
            }
        }
        
    }
}
