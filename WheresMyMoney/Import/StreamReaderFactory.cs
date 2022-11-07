namespace WheresMyMoney.Import;

public interface IStreamReaderFactory
{
    StreamReader Open(string path);
}

public class StreamReaderFactory : IStreamReaderFactory
{
    public StreamReader Open(string path) => new(path);
}