using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class PersonCollection : IPersonCollection
{
    private Dictionary<string, Person> peopleByEmail;
    private Dictionary<string, SortedSet<Person>> peopleByEmailDomain;
    private Dictionary<string, SortedSet<Person>> peopleByNameAndTown;
    private OrderedDictionary<int, SortedSet<Person>> peopleByAge;
    private Dictionary<string, OrderedDictionary<int, SortedSet<Person>>> peopleByTownAndAge;

    public PersonCollection()
    {
        this.peopleByEmail = new Dictionary<string, Person>();
        this.peopleByEmailDomain = new Dictionary<string, SortedSet<Person>>();
        this.peopleByNameAndTown = new Dictionary<string, SortedSet<Person>>();
        this.peopleByAge = new OrderedDictionary<int, SortedSet<Person>>();
        this.peopleByTownAndAge = new Dictionary<string, OrderedDictionary<int, SortedSet<Person>>>();
    }

    public bool AddPerson(string email, string name, int age, string town)
    {
        var person = this.FindPerson(email);
        if (person != null)
        {
            return false;
        }

        person = new Person(email, name, age, town);
        this.peopleByEmail.Add(email, person);
        var domain = GetMailDomain(email);

        if (!this.peopleByEmailDomain.ContainsKey(domain))
        {
            this.peopleByEmailDomain[domain] = new SortedSet<Person>();
        }
        this.peopleByEmailDomain[domain].Add(person);

        string personNameTown = name + town;
        if (!this.peopleByNameAndTown.ContainsKey(personNameTown))
        {
            this.peopleByNameAndTown[personNameTown] = new SortedSet<Person>();
        }
        this.peopleByNameAndTown[personNameTown].Add(person);

        if (!this.peopleByAge.ContainsKey(age))
        {
            this.peopleByAge[age] = new SortedSet<Person>();
        }
        this.peopleByAge[age].Add(person);

        if (!this.peopleByTownAndAge.ContainsKey(town))
        {
            this.peopleByTownAndAge[town] = new OrderedDictionary<int, SortedSet<Person>>();
        }
        if (!this.peopleByTownAndAge[town].ContainsKey(age))
        {
            this.peopleByTownAndAge[town][age] = new SortedSet<Person>();
        }
        this.peopleByTownAndAge[town][age].Add(person);

        return true;
    }

    private string GetMailDomain(string email)
    {
        string domain = email.Substring(email.IndexOf("@") + 1);
        return domain;
    }

    public int Count
    {
        get
        {
            return this.peopleByEmail.Count;
        }
    }

    public Person FindPerson(string email)
    {
        if (!this.peopleByEmail.ContainsKey(email))
        {
            return null;
        }

        return this.peopleByEmail[email];
    }

    public bool DeletePerson(string email)
    {
        var personToDelete = this.FindPerson(email);
        if (personToDelete == null)
        {
            return false;
        }

        this.peopleByEmail.Remove(email);
        string domain = this.GetMailDomain(email);
        this.peopleByEmailDomain[domain].Remove(personToDelete);
        string personNameTown = personToDelete.Name + personToDelete.Town;
        this.peopleByNameAndTown[personNameTown].Remove(personToDelete);
        this.peopleByAge[personToDelete.Age].Remove(personToDelete);
        this.peopleByTownAndAge[personToDelete.Town][personToDelete.Age].Remove(personToDelete);

        return true;
    }

    public IEnumerable<Person> FindPersons(string emailDomain)
    {
        if (!this.peopleByEmailDomain.ContainsKey(emailDomain))
        {
            return Enumerable.Empty<Person>();
        }

        return this.peopleByEmailDomain[emailDomain];
    }

    public IEnumerable<Person> FindPersons(string name, string town)
    {
        string personNameTown = name + town;
        if (!this.peopleByNameAndTown.ContainsKey(personNameTown))
        {
            return Enumerable.Empty<Person>();
        }

        return this.peopleByNameAndTown[personNameTown];
    }

    public IEnumerable<Person> FindPersons(int startAge, int endAge)
    {
        return this.peopleByAge.Range(startAge, true, endAge, true).SelectMany(p => p.Value);
    }

    public IEnumerable<Person> FindPersons(
        int startAge, int endAge, string town)
    {
        if (!this.peopleByTownAndAge.ContainsKey(town))
        {
            return Enumerable.Empty<Person>();
        }

        return this.peopleByTownAndAge[town].Range(startAge, true, endAge, true).SelectMany(p => p.Value);
    }
}
