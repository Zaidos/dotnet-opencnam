DotNet.OpenCNAM
---
a really easy to use opencnam library written in .net

this library uses [opencnam](http://www.opencnam.com "opencnam") as a backend.

## installation

you can install from [NuGet](https://nuget.org "nuget"), the best .net package manager :)

```
PM> Install-Package DotNet.OpenCNAM
```

or you can be a badass and compile from source

## usage

for a simple lookup (note: these are restricted to 60 an hour):

```c#
// A lookup for 'GOOGLE INC'
var response = OpenCNAM.Lookup("+16502530000");
```

to perform an authenticated lookup:

```c#
OpenCNAM.AccountSID = "xxx";
OpenCNAM.AuthToken = "xxx";

var response = OpenCNAM.Lookup("+16502530000", true);
```


## limits

The [opencnam](http://www.opencnam.com "opencnam") API used as a backend limits you to no more than 60 requests per hour (using their free tier).
