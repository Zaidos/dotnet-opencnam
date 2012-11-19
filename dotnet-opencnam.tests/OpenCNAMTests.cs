#region File Info

// Solution: dotnet-opencnam
// Project: dotnet-opencnam.tests
// 
// Filename: OpenCNAMTests.cs
// Created: 11/18/2012 [7:49 PM]
// Modified: 11/18/2012 [8:45 PM]

#endregion

using System;
using System.Net;
using Xunit;

namespace dotnet_opencnam.tests
{
  public class OpenCNAMTests
  {
    private const string ValidLookup = "+16502530000";
    private const string InvalidLookup = "cnam";
     
    public OpenCNAMTests()
    {
      OpenCNAM.UseHTTPAuth = false;
    }

    [Fact]
    public void ReturnsAResponse()
    {
      var response = OpenCNAM.Lookup(ValidLookup);
      Assert.False(string.IsNullOrEmpty(response));
    }

    [Fact]
    public void ThrowsWebExceptionIfInvalidFormat()
    {
      Assert.Throws<WebException>(() => { OpenCNAM.Lookup(InvalidLookup); });
    }

    [Fact]
    public void BadRequestHasErrorMessage()
    {
      try
      {
        OpenCNAM.Lookup(InvalidLookup);
      }
      catch (WebException ex)
      {
        var response = ex.Response;

        using(var stream = response.GetResponseStream())
        {
          Assert.NotNull(stream);
        }
      }
    }

    [Fact]
    public void ThrowsInvalidOperationExceptionIfAccountSIDIsNoSet()
    {
      OpenCNAM.UseHTTPAuth = true;
      Assert.Throws<InvalidOperationException>(() => { OpenCNAM.Lookup(ValidLookup); });
    }
  }
}