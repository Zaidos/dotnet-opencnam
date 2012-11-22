#region File Info

// Solution: dotnet-opencnam
// Project: DotNet.OpenCNAM.Tests
// 
// Filename: OpenCNAMTests.cs
// Created: 11/18/2012 [7:49 PM]
// Modified: 11/21/2012 [11:38 PM]

#endregion

using System;
using Xunit;

namespace DotNet.OpenCNAM.tests
{
  public class OpenCNAMTests
  {
    private const string ValidLookup = "+16502530000";
    private const string InvalidLookup = "cnam";

    public OpenCNAMTests()
    {
      OpenCNAM.UseHTTPAuth = false;
      OpenCNAM.AccountSID = string.Empty;
      OpenCNAM.AuthToken = string.Empty;
    }

    [Fact]
    public void ReturnsAResponseOnValidLookup()
    {
      var response = OpenCNAM.Lookup(ValidLookup);
      Assert.False(string.IsNullOrEmpty(response));
    }

    [Fact]
    public void OpenCNAMExceptionContainsDetails()
    {
      try
      {
        OpenCNAM.Lookup(InvalidLookup);
      }
      catch (OpenCNAMException ex)
      {
        Assert.False(string.IsNullOrEmpty(ex.ServerResponse));
        Assert.NotNull(ex.StatusCode);
      }
    }

    [Fact]
    public void ThrowsOpenCNAMExceptionIfNotOk()
    {
      Assert.Throws<OpenCNAMException>(() => { OpenCNAM.Lookup(InvalidLookup); });
    }

    [Fact]
    public void ThrowsInvOpExceptionIfCredentialsArentSet()
    {
      Assert.Throws<InvalidOperationException>(() => { OpenCNAM.Lookup(ValidLookup, true); });
    }
  }
}