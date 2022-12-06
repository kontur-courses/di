namespace TagCloudContainer.Readers
{
    public class TxtReader:IFileReader
    {
        public string Read(string path)
        {
            return File.ReadAllText(path);
        }
    }
}
