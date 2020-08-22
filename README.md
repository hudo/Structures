# Data structures

## Time structure

This data structure represents point in time of a day. Semantically it's different than **TimeSpan** because it's not a duration, 
but rather a point in time, with support of some additional operators. Like DateTime just without a Date component. 

## Usage examples


```csharp

var time = new Time(16,30,15); // 16h, 30m, 15s
Time time = 5; // 5h, 0m, 0s
Time time = 2.5; // 2h, 30m, 0s
Time time = "17:30:05"; // 17h, 30m, 5s

var firstTime = new Time(1); // 1:0:0
Time secondTime = 3; // 3:0:0

var isTrue = firstTime < secondTime;
var (h, m, s) = time; // deconstructor to tuple

```

This project references **System.Text.Json** and contains JSON converter to support serialization operations like:  

```csharp

string json = JsonSerializer.Serialize(new Time(5, 30, 5)); // "5:30:5"
Time time = JsonSerializer.Deserialize<Time>("\"5:20\"");

```

## Package

https://www.nuget.org/packages/Structures.Time