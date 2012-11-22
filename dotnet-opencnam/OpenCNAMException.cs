#region File Info

// Solution: dotnet-opencnam
// Project: dotnet-opencnam
// 
// Filename: OpenCNAMException.cs
// Created: 11/18/2012 [10:25 PM]
// Modified: 11/18/2012 [10:25 PM]

#endregion

using System;
using System.IO;
using System.Net;

namespace DotNet.OpenCNAM
{
  public class OpenCNAMException : Exception
  {
    public HttpStatusCode StatusCode { get; private set; }
    public string ServerResponse { get; private set; }

    internal OpenCNAMException(WebException ex)
    {
      using(var response = (HttpWebResponse)ex.Response)
      {
        StatusCode = response.StatusCode;

        using ( var stream = response.GetResponseStream() )
        {
          if (stream == null)
          {
            ServerResponse = ex.Message;
            return;
          }

          using(var reader = new StreamReader(stream))
          {
            ServerResponse = reader.ReadToEnd();

            reader.Close();
          }

          stream.Close();
        }

        response.Close();
      }
    }
  }
}