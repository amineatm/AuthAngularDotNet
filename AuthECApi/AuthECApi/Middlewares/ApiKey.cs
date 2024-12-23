namespace AuthECApi.Middlewares
{
    public class ApiKey
    {
        public string Key { get; set; }
        public bool ForApp { get; set; }
        public bool ForBrowser { get; set; }
        public string Origin { get; set; }
        public bool IsOpen { get; set; }
        public string AppSignature { get; set; }
    }

}
