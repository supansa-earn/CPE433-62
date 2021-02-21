using System;
using System.Collections.Generic;
using System.Text;

namespace DNWS
{
  class Client : IPlugin
  {
    protected static Dictionary<String, int> statDictionary = null;
    public Client()
    {
      if (statDictionary == null)
      {
        statDictionary = new Dictionary<String, int>();

      }
    }

    public void PreProcessing(HTTPRequest request)
    {
      if (statDictionary.ContainsKey(request.Url))
      {
        statDictionary[request.Url] = (int)statDictionary[request.Url] + 1;
      }
      else
      {
        statDictionary[request.Url] = 1;
      }
    }
    public HTTPResponse GetResponse(HTTPRequest request)
    {
      HTTPResponse response = null;
      StringBuilder sb = new StringBuilder();
      String collect=request.getPropertyByKey("remoteendpoint");
      String[] Ip = collect.Split(':');
      String Browser =request.getPropertyByKey("user-agent");
      String Language =request.getPropertyByKey("accept-language");
      String Encode =request.getPropertyByKey("accept-encoding");
      sb.Append("<html><body><h1>Client Information</h1>");
      sb.Append("Client IP: " + Ip[0]);
      sb.Append("<br>Client Port: " + Ip[1]);
      sb.Append("<br>Browser Information: " + Browser);
      sb.Append("<br>Accept Language: " + Language);
      sb.Append("<br>Accept Encoding: " + Encode);
      sb.Append("</body></html>");
      response = new HTTPResponse(200);
      response.body = Encoding.UTF8.GetBytes(sb.ToString());
      return response;
    }

    public HTTPResponse PostProcessing(HTTPResponse response)
    {
      throw new NotImplementedException();
    }
  }
}