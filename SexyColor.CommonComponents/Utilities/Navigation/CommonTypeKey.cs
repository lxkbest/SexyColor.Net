namespace SexyColor.CommonComponents
{
    public class CommonTypeKey
    {
        private static volatile CommonTypeKey _instance = null;
        private static readonly object lockObject = new object();

        public static CommonTypeKey Instance()
        {
            if (_instance == null)
            {
                lock (lockObject)
                {
                    if (_instance == null)
                    {
                        _instance = new CommonTypeKey();
                    }
                }
            }
            return _instance;
        }

        private CommonTypeKey()
        { }

        public string Role()
        {
            return "10086";
        }
    }
}
