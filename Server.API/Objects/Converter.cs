public class MyUrlObject{
    public MyUrlObject(string url){
        random = new Random();
        this._originalURL = url;
        this._smallerURL = makeRandomString();

    }
    public MyUrlObject(string url, int seed){
        random = new Random(seed);
        this._originalURL = url;
        this._smallerURL = makeRandomString();
    }
    private Random random;
    private string _originalURL;
    private string _smallerURL;
    public string OriginalURL {get{return _originalURL;}}
    public string SmallerURL {get{return this._smallerURL;}}
    private string makeRandomString(){
        const int length = 16;
        var result = "";
        var characters = @"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        for(int i = 0; i < length; i++){

            result += characters[random.Next()%characters.Length];
        }
        return result;
    }

}