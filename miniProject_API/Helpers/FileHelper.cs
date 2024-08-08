namespace miniProject_API.Helpers
{
    public class FileHelper
    {
        public static void Delete(string path)
        {
            if (System.IO.File.Exists(path)) System.IO.File.Delete(path);
        }
    }
}
