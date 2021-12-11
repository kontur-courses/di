namespace App.Infrastructure.FileInteractions.Readers
{
    public interface IReaderFactory
    {
        ILinesReader CreateReader();
    }
}