using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace ClientApp.Core.Common;

public class PostItResponse
{
    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonProperty("success")]
    public bool Success { get; set; }

    [JsonProperty("message")]
    public string Message { get; set; }

    [JsonProperty("message_code")]
    public int MessageCode { get; set; }

    [JsonProperty("total")]
    public int Total { get; set; }

    [JsonProperty("data")]

    public List<PostItResponseData> Data { get; set; }

    public PostItResponse()
    {
        Status = string.Empty;
        Message = string.Empty;
        Data = new List<PostItResponseData>();
    }
    public PostItResponse(string status, bool success, string message, int messageCode, int total, string data)
    {
        Status = status;
        Success = success;
        Message = message;
        MessageCode = messageCode;
        Total = total;
        Data = new List<PostItResponseData>();
    }
}

public class PostItResponseData
{
    [JsonProperty("post_code")]
    public string PostCode { get; set; }

    [JsonProperty("address")]
    public string Address { get; set; }

    [JsonProperty("street")]
    public string Street { get; set; }

    [JsonProperty("number")]
    public string Number { get; set; }

    [JsonProperty("only_number")]
    public string OnlyNumber { get; set; }

    [JsonProperty("housing")]
    public string Housing { get; set; }

    [JsonProperty("city")]
    public string City { get; set; }

    [JsonProperty("eldership")]
    public string Eldership { get; set; }

    [JsonProperty("municipality")]
    public string Municipality { get; set; }

    [JsonProperty("post")]
    public string Post { get; set; }

    [JsonProperty("mailbox")]
    public string Mailbox { get; set; }

    public PostItResponseData()
    {
        PostCode = string.Empty;
        Address = string.Empty;
        Street = string.Empty;
        Number = string.Empty;
        OnlyNumber = string.Empty;
        Housing = string.Empty;
        City = string.Empty;
        Eldership = string.Empty;
        Municipality = string.Empty;
        Post = string.Empty;
        Mailbox = string.Empty;
    }
    public PostItResponseData(string postCode, string address, string street, string number, string onlyNumber, string housing, string city, string eldership, string municipality, string post, string mailbox)
    {
        PostCode = postCode;
        Address = address;
        Street = street;
        Number = number;
        OnlyNumber = onlyNumber;
        Housing = housing;
        City = city;
        Eldership = eldership;
        Municipality = municipality;
        Post = post;
        Mailbox = mailbox;
    }
}

