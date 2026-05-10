using Lab9.Green;

namespace Lab10.Green
{
    public interface ISerializer
    {
        T Deserialize<T>() where T : Green;
        void Serialize<T>(T obj) where T : Green;
    }
}
