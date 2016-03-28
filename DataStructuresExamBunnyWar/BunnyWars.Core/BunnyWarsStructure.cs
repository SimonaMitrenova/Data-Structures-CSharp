namespace BunnyWars.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Wintellect.PowerCollections;

    public class BunnyWarsStructure : IBunnyWarsStructure
    {
        private Dictionary<string, Bunny> bunniesByName;
        private OrderedSet<int> rooms;
        private Dictionary<int, SortedSet<Bunny>> bunniesByTeam;
        private Dictionary<int, MultiDictionary<int, Bunny>> bunniesInRoom;
        private OrderedDictionary<string, Bunny> bunniesBySuffix;

        public BunnyWarsStructure()
        {
            this.bunniesByName = new Dictionary<string, Bunny>();
            this.rooms = new OrderedSet<int>();
            this.bunniesByTeam = new Dictionary<int, SortedSet<Bunny>>();
            this.bunniesInRoom = new Dictionary<int, MultiDictionary<int, Bunny>>();
            this.bunniesBySuffix = new OrderedDictionary<string, Bunny>(StringComparer.Ordinal);
        }

        public int BunnyCount
        {
            get
            {
                return this.bunniesByName.Count;
            }
        }

        public int RoomCount
        {
            get
            {
                return this.rooms.Count;
            }
        }

        public void AddRoom(int roomId)
        {
            if (this.rooms.Contains(roomId))
            {
                throw new ArgumentException("Room with given ID already exist.");
            }

            this.rooms.Add(roomId);
            this.bunniesInRoom[roomId] = new MultiDictionary<int, Bunny>(true);
        }

        public void AddBunny(string name, int team, int roomId)
        {
            if (!this.rooms.Contains(roomId))
            {
                throw new ArgumentException("Given room does not exist.");
            }
            if (this.bunniesByName.ContainsKey(name))
            {
                throw new ArgumentException("Bunny with such name already exist.");
            }
            if (team < 0 || team > 4)
            {
                throw new IndexOutOfRangeException("Incorrect team.");
            }

            var bunny = new Bunny(name, team, roomId);
            this.bunniesByName.Add(name, bunny);
            this.bunniesInRoom[roomId].Add(bunny.Team, bunny);

            if (!this.bunniesByTeam.ContainsKey(team))
            {
                this.bunniesByTeam[team] = new SortedSet<Bunny>();
            }
            this.bunniesByTeam[team].Add(bunny);

            string reversedName = this.ReverseName(name);
            this.bunniesBySuffix.Add(reversedName, bunny);
        }

        private string ReverseName(string name)
        {
            StringBuilder reversed = new StringBuilder();
            foreach (var character in name.Reverse())
            {
                reversed.Append(character);
            }
            return reversed.ToString();
        }

        public void Remove(int roomId)
        {
            if (!this.rooms.Contains(roomId))
            {
                throw new ArgumentException("Given room does not exist.");
            }

            this.rooms.Remove(roomId);
            var bynniesToRemove = this.bunniesInRoom[roomId].Values;
            this.bunniesInRoom.Remove(roomId);
            foreach (var bunny in bynniesToRemove)
            {
                this.bunniesByName.Remove(bunny.Name);
                this.bunniesByTeam.Remove(bunny.Team);
                this.bunniesBySuffix.Remove(this.ReverseName(bunny.Name));
            }
        }

        public void Next(string bunnyName)
        {
            if (!this.bunniesByName.ContainsKey(bunnyName))
            {
                throw new ArgumentException("Bunny with such name does not exist.");
            }
            if (this.RoomCount == 1)
            {
                return;
            }

            var bunny = this.bunniesByName[bunnyName];
            int currentRoom = bunny.RoomId;
            var roomsNextToCurrent = this.rooms.RangeFrom(currentRoom, false);
            int nextRoom;
            if (!roomsNextToCurrent.Any())
            {
                nextRoom = this.rooms.GetFirst();
            }
            else
            {
                nextRoom = roomsNextToCurrent.GetFirst();
            }
            bunny.RoomId = nextRoom;
            this.bunniesInRoom[currentRoom].Remove(bunny.Team, bunny);
            this.bunniesInRoom[nextRoom].Add(bunny.Team, bunny);
        }

        public void Previous(string bunnyName)
        {
            if (!this.bunniesByName.ContainsKey(bunnyName))
            {
                throw new ArgumentException("Bunny with such name does not exist.");
            }
            if (this.RoomCount == 1)
            {
                return;
            }
            var bunny = this.bunniesByName[bunnyName];
            int currentRoom = bunny.RoomId;
            var roomsPrevToCurrent = this.rooms.RangeTo(currentRoom, false);
            int prevRoom;
            if (!roomsPrevToCurrent.Any())
            {
                prevRoom = this.rooms.GetLast();
            }
            else
            {
                prevRoom = roomsPrevToCurrent.GetLast();
            }
            bunny.RoomId = prevRoom;
            this.bunniesInRoom[currentRoom].Remove(bunny.Team, bunny);
            this.bunniesInRoom[prevRoom].Add(bunny.Team, bunny);
        }

        public void Detonate(string bunnyName)
        {
            if (!this.bunniesByName.ContainsKey(bunnyName))
            {
                throw new ArgumentException("Bunny with such name does not exist.");
            }

            var taliban = this.bunniesByName[bunnyName];
            int currentRoom = taliban.RoomId;
            var bunniesToDetonate = this.bunniesInRoom[currentRoom].Where(b => b.Key != taliban.Team).SelectMany(b => b.Value);
            var bunniesToKill = new List<Bunny>();
            foreach (var bunny in bunniesToDetonate)
            {
                bunny.Health -= 30;
                if (bunny.Health <= 0)
                {
                    bunniesToKill.Add(bunny);
                    taliban.Score++;
                }
            }
            foreach (var bunny in bunniesToKill)
            {
                this.bunniesByName.Remove(bunny.Name);
                this.bunniesByTeam[bunny.Team].Remove(bunny);
                this.bunniesInRoom[currentRoom].Remove(bunny.Team, bunny);
                this.bunniesBySuffix.Remove(this.ReverseName(bunny.Name));
            }
        }

        public IEnumerable<Bunny> ListBunniesByTeam(int team)
        {
            if (team < 0 || team > 4)
            {
                throw new IndexOutOfRangeException("Incorrect team.");
            }
            if (!this.bunniesByTeam.ContainsKey(team))
            {
                return Enumerable.Empty<Bunny>();
            }

            return this.bunniesByTeam[team];
        }

        public IEnumerable<Bunny> ListBunniesBySuffix(string suffix)
        {
            string reversedSuffix = this.ReverseName(suffix);
            return this.bunniesBySuffix.Range(reversedSuffix, true, reversedSuffix + char.MaxValue, true).Values;
        }
    }
}
