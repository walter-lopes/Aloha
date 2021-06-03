namespace Aloha
{
    public interface IAlohaSerializer
    {
        string Serialize<T>(T value);
        string Serialize(object value);
        T Deserialize<T>(string value);
        object Deserialize(string value);
    }
}
