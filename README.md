# Local Data Base
## Installation
PM> Install-Package LDB.Linq -Version 1.0.0
## How to use
1. Add LDB.Linq to your project

2. Create data model class and inherit from LTable
```csharp
class Test : LTable
{
    public int Code { get; set; }
    public string Name { get; set; }
}
```

3. Create database context and inherit from LContext
```csharp
class MyContext : LContext
{
    public DbSet<Test> Tests { get; set; }
}
```

4. Use it ;)
```csharp
using (var db = new MyContext())
{
    var maxValue = db.Tests.Max(a => (int?)a.Code) ?? 0;

    db.Tests.Add(new Test
    {
        Code = ++maxValue,
        Name = "Name " + maxValue
    });
}
```
