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
## Context constructors
LContext has 3 type of constructor:
```csharp
// Default constructor
public MyContext() { }

// ConnectionString constructor
public MyContext(string connectionString)
 : base(connectionString) { }

// Parametrized constructor
public MyContext(DataTypeEnum dataType, string path, PositionTypeEnum positionType, bool isReadOnly)
 : base(dataType, path, positionType, isReadOnly) { }
```
Default constructor will:

1. create relative path application\Data\
2. use xml format for storage data
3. available for reading

```csharp
// Example connectionString constructor
var db = new MyContext("type:xml;position:relative;path:Data;isreadonly:false");

// Example connectionString constructor
var db = new MyContext(DataTypeEnum.XML, "Data", PositionTypeEnum.Relative, false);
```
Parameters:
1. dataType -  type of storage file, default XML. Available types: XML, CSV, BIN
2. path - a directory keep files, default "Data".
3. PositionTypeEnum - Relative, Absolute
4. isReadOnly - available for reading only if true.
