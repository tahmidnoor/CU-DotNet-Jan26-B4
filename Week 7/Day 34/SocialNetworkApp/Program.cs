using System;
using System.Collections.Generic;
using System.Linq;

namespace SocialNetworkApp
{
    class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Person(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }

    class User : Person
    {
        public List<User> friends = new List<User>();

        public User(int id, string name) : base(id, name) { }

        public void AddFriend(User friend)
        {
            if (!SocialNetwork.members.Contains(this) && !SocialNetwork.members.Contains(friend))
            {
                Console.WriteLine("Both users must be registered in the social network.");
                return;
            }

            if (!friends.Contains(friend))
            {
                friends.Add(friend);
                friend.friends.Add(this);
            }
        }
    }

    class SocialNetwork
    {
        public static List<User> members = new List<User>();
        private static int nextId = 101;

        public static User Register(string name)
        {
            User user = new User(nextId++, name);
            members.Add(user);
            return user;
        }

        public static void ShowNetwork()
        {
            foreach (var member in members)
            {
                Console.Write(member.Name + " -> ");

                List<string> list = new List<string>();
                foreach (var friend in member.friends)
                {
                    list.Add(friend.Name);
                }

                Console.WriteLine(string.Join(", ", list));
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            User Tahmid = SocialNetwork.Register("Tahmid");
            User Shreya = SocialNetwork.Register("Shreya");
            User Aditi = SocialNetwork.Register("Aditi");

            User Shreynash = new User(999, "Shreyansh");

            Tahmid.AddFriend(Shreya);
            Tahmid.AddFriend(Aditi);
            Tahmid.AddFriend(Shreynash);

            SocialNetwork.ShowNetwork();
        }
    }
}