#region File Info

// Solution: dotnet-opencnam
// Project: dotnet-opencnam
// 
// Filename: OpenCNAM.cs
// Created: 11/18/2012 [7:35 PM]
// Modified: 11/18/2012 [10:17 PM]

#endregion

using System;
using System.IO;
using System.Net;
using System.Text;

namespace DotNet.OpenCNAM
{
  public static class OpenCNAM
  {
    private const string HttpUrlTemplate = @"https://{0}:{1}@api.opencnam.com/v2/phone/{2}?format={3}";
    private const string QueryUrlTemplate = @"https://api.opencnam.com/v2/phone/{0}?format={1}";
    private const string QueryFormat = "text";

    public static bool UseHTTPAuth { get; set; }
    public static string AccountSID { get; set; }
    public static string AuthToken { get; set; }

    public static string Lookup(string lookup, bool useAuth = false)
    {
      var request = WebRequest.Create(BuildUrl(lookup, useAuth));

      string lookupResponse;

      try
      {
        using ( var response = (HttpWebResponse)request.GetResponse() )
        {
          using ( var stream = response.GetResponseStream() )
          {
            if ( stream == null )
              throw new InvalidOperationException("Response was empty.");

            using ( var reader = new StreamReader(stream) )
            {
              lookupResponse = reader.ReadToEnd();
              reader.Close();
            }

            stream.Close();
          }

          response.Close();
        }
      }
      catch (WebException ex)
      {
        throw new OpenCNAMException(ex);
      }

      return lookupResponse;
    }

    private static string BuildUrl(string lookup, bool auth = false)
    {
      if (auth)
      {
        ValidateAuthenticatedRequest();

        if (UseHTTPAuth)
          return string.Format(HttpUrlTemplate, AccountSID, AuthToken, lookup, QueryFormat);
      }

      var sb = new StringBuilder(string.Format(QueryUrlTemplate, lookup, QueryFormat));

      if (auth)
      {
        sb.Append(string.Format("&account_sid={0}&auth_token={1}", AccountSID, AuthToken));
      }

      return sb.ToString();
    }

    private static void ValidateAuthenticatedRequest()
    {
      if (string.IsNullOrEmpty(AccountSID))
        throw new InvalidOperationException("Account SID can not be empty. Set the AccountSID with OpenCNAM.AccountSID");

      if (string.IsNullOrEmpty(AuthToken))
        throw new InvalidOperationException("Authentication token can not be empty. Set token with OpenCNAM.AuthToken");
    }
  }
}