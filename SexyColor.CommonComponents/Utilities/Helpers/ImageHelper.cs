namespace SexyColor.CommonComponents
{
    public class ImageHelper
    {
        public static bool IsImage(string extension)
        {
            extension = extension.ToLower();
            if (extension == ".gif" || extension == ".jpg" || extension == ".png" || extension == ".jpeg" || extension == ".bmp")
            {
                return true;
            }
            else return false;
        }
    }
}
