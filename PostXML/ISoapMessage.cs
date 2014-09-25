using System.Net;
public class ISoapMessage
{
    string _Uri = string.Empty;
    string _ContentXml = string.Empty;
    string _SoapAction = string.Empty;
    ICredentials _Credentials = null;
    public string Uri
    {
        get
        {
            return _Uri;
        }
    }
    public string ContentXml
    {
        get
        {
            return _ContentXml;
        }
    }
    public string SoapAction
    {
        get
        {
            return _SoapAction;
        }
    }
    public ICredentials Credentials
    {
        get
        {
            return _Credentials;
        }
    }
}