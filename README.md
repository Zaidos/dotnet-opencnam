dotnet-opencnam
---
a really easy to use opencnam library written in .net

this library uses [opencnam](http://www.opencnam.com "opencnam") as a backend.

## usage

```c#
// A lookup for 'GOOGLE INC'
var response = OpenCNAM.Lookup("+16502530000");
```

## limits

The [opencnam](http://www.opencnam.com "opencnam") API we use as a backend
limits you to no more than 60 requests per hour (using their free tier).