namespace Server.Test;
using Server.API;
using System.Text.Json;
public class CheckConverter
{
    [Fact]
    public void MakeSureWeHaveObject()
    {
        string stringMustBeThis = @"{""OriginalURL"":""http://localhost/api/helloworld"",""SmallerURL"":""Oo6IIrMDPxfGLajY""}";
        MyUrlObject TestUrlObject = new MyUrlObject("http://localhost/api/helloworld",1);
        string json = JsonSerializer.Serialize(TestUrlObject);
        Assert.Equal(json,stringMustBeThis);
    }
}