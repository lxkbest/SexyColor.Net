namespace SexyColor.CommonComponents
{
    public class ImageSizeTypeKey
    {
        private static volatile ImageSizeTypeKey _instance = null;
        private static readonly object lockObject = new object();

        public static ImageSizeTypeKey Instance()
        {
            if (_instance == null)
            {
                lock (lockObject)
                {
                    if (_instance == null)
                    {
                        _instance = new ImageSizeTypeKey();
                    }
                }
            }
            return _instance;
        }

        private ImageSizeTypeKey()
        { }

        /// <summary>
        /// 原始尺寸
        /// </summary>
        public string Original()
        {
            return "Original";
        }

        public string Small()
        {
            return "Small";
        }
    }
}
