using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Summary description for SearchCriteria
/// </summary>
public class SearchCriteria
{

    private string _responsecode = "";
    private string _cardtype = "";
    private string _fromdate = "";
    private string _todate = "";
    private string _searchtext = "";
    private bool _checknumber = false;
    private string _strendswith = "";
    private string _strbeginswith = "";
    private bool _CheckNote = false;
    private bool _CheckReceiptDestination = false;
    private bool _CheckCompanyName = false;
    private bool _CheckAccountCode = false;
    private bool _CheckPostToCode = false;
    private bool _CheckReferenceCode = false;
    private string _AlertTypes = "";

    public string AlertTypes
    {
        get
        {
            return _AlertTypes;
        }
        set
        {
            _AlertTypes = value;
        }
    }
    public bool CheckReferenceCode
    {
        get
        {
            return _CheckReferenceCode;
        }
        set
        {
            _CheckReferenceCode = value;
        }
    }
    public bool CheckPostToCode
    {
        get
        {
            return _CheckPostToCode;
        }
        set
        {
            _CheckPostToCode = value;
        }
    }
    public bool CheckAccountCode
    {
        get
        {
            return _CheckAccountCode;
        }
        set
        {
            _CheckAccountCode = value;
        }
    }
    public bool CheckCompanyName
    {
        get
        {
            return _CheckCompanyName;
        }
        set
        {
            _CheckCompanyName = value;
        }
    }
    public bool CheckReceiptDestination
    {
        get
        {
            return _CheckReceiptDestination;
        }
        set
        {
            _CheckReceiptDestination = value;
        }
    }
    public bool CheckNote
    {
        get
        {
            return _CheckNote;
        }
        set
        {
            _CheckNote = value;
        }
    }
    public string StrBeginsWith
    {
        get
        {
            return _strbeginswith;
        }
        set
        {
            _strbeginswith = value;
        }
    }
    public string StrEndsWith
    {
        get
        {
            return _strendswith;
        }
        set
        {
            _strendswith = value;
        }
    }
    public bool CheckNumber
    {
        get
        {
            return _checknumber;
        }
        set
        {
            _checknumber = value;
        }
    }
    public string SearchText
    {
        get
        {
            return _searchtext;
        }
        set
        {
            _searchtext = value;
        }
    }
    public string ToDate
    {
        get
        {
            return _todate;
        }
        set
        {
            _todate = value;
        }
    }
    public string FromDate
    {
        get
        {
            return _fromdate;
        }
        set
        {
            _fromdate = value;
        }
    }
    public string CardType
    {
        get
        {
            return _cardtype;
        }
        set
        {
            _cardtype = value;
        }
    }
    public string ResponseCode
    {
        get
        {
            return _responsecode;
        }
        set
        {
            _responsecode = value;
        }
    }
}