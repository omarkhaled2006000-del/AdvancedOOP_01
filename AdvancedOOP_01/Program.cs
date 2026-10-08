// Q1)
// A generic class is a class that uses a type parameter.
// It allows the same class to work with different data types.
// Generics provide code reuse and type safety.


// Q2)
// A generic class Container<T> can be written as:

class Container<T>
{
    private T value;

    public void Add(T value)
    {
        this.value = value;
    }

    public T Get()
    {
        return value;
    }
}


// Q3)
// Multiple type parameters allow a generic class to work with
// more than one data type.

class Pair<TKey, TValue>
{
    public TKey Key { get; set; }
    public TValue Value { get; set; }
}


// Q4)
// A generic method is a method that uses a type parameter
// and can work with different data types.

static void Swap<T>(ref T a, ref T b)
{
    T temp = a;
    a = b;
    b = temp;
}


// Q5)
// IComparable<T> allows the values to be compared.

static T FindMax<T>(T a, T b) where T : IComparable<T>
{
    if (a.CompareTo(b) > 0)
        return a;

    return b;
}


// Q6)
// A generic interface uses a type parameter so it can work
// with different data types.

interface IRepository<T>
{
    void Add(T item);
    T Get(int id);
}


// Q7)
// The struct constraint means T must be a value type.

class ValueContainer<T> where T : struct
{
    public T Value { get; set; }
}


// Q8)
// The class constraint means T must be a reference type.

class ReferenceContainer<T> where T : class
{
    public T Value { get; set; }
}


// Q9)
// The new() constraint requires T to have a public
// parameterless constructor.

class Factory<T> where T : new()
{
    public T Create()
    {
        return new T();
    }
}


// Q10)
// An interface constraint means T must implement
// the specified interface.

interface IPrintable
{
    void Print();
}

class Printer<T> where T : IPrintable
{
    public void PrintItem(T item)
    {
        item.Print();
    }
}


// Q11)
// A base class constraint means T must inherit from
// the specified base class.

class Animal
{
    public void Eat()
    {
        Console.WriteLine("Eating...");
    }
}

class AnimalContainer<T> where T : Animal
{
    public void MakeAnimalEat(T animal)
    {
        animal.Eat();
    }
}


// Q12)
// Multiple constraints can be applied after the where keyword.

class Manager<T> where T : class, IPrintable, new()
{
    public T Create()
    {
        return new T();
    }
}


// Q13)
// default returns the default value of the specified type.
//
// For example:
// int      -> 0
// bool     -> false
// reference type -> null

static T GetDefault<T>()
{
    return default(T);
}


// Q14)

class SafeList<T>
{
    private List<T> items = new List<T>();

    public void Add(T item)
    {
        items.Add(item);
    }

    public T Get(int index)
    {
        if (index < 0 || index >= items.Count)
            return default(T);

        return items[index];
    }
}


// Q15)
// Covariance allows a generic type containing a derived type
// to be assigned to a generic type containing its base type.
// The out keyword is used for covariance.

interface IProducer<out T>
{
    T Get();
}


// Q16)
// Contravariance allows a generic type containing a base type
// to be assigned to a generic type containing its derived type.
// The in keyword is used for contravariance.

interface IConsumer<in T>
{
    void Consume(T item);
}


// Q17)
// Covariance:
// - Uses "out".
// - Used mainly for output.
// - Derived type -> Base type.
//
// Contravariance:
// - Uses "in".
// - Used mainly for input.
// - Base type -> Derived type.


// Q18)
// Each constructed generic type has its own separate copy
// of static members.

class Counter<T>
{
    public static int Count = 0;

    public Counter()
    {
        Count++;
    }
}


// Q19)
// A class can inherit from a generic class by specifying
// the type argument.

class Storage<T>
{
    public T Value { get; set; }
}

class IntStorage : Storage<int>
{
}


// Q20)

class Cache<TKey, TValue>
{
    private class CacheItem
    {
        public TValue Value { get; set; }
        public DateTime Expiration { get; set; }
    }

    private Dictionary<TKey, CacheItem> items =
        new Dictionary<TKey, CacheItem>();

    public void Add(TKey key, TValue value, TimeSpan expiration)
    {
        items[key] = new CacheItem
        {
            Value = value,
            Expiration = DateTime.Now.Add(expiration)
        };
    }

    public TValue Get(TKey key)
    {
        if (!items.ContainsKey(key))
            return default(TValue);

        CacheItem item = items[key];

        if (DateTime.Now >= item.Expiration)
        {
            items.Remove(key);
            return default(TValue);
        }

        return item.Value;
    }

    public void Remove(TKey key)
    {
        items.Remove(key);
    }

    public bool Contains(TKey key)
    {
        if (!items.ContainsKey(key))
            return false;

        if (DateTime.Now >= items[key].Expiration)
        {
            items.Remove(key);
            return false;
        }

        return true;
    }
}
