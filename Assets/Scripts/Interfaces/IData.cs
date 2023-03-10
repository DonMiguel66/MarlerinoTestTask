public interface IData<T>
{
    void SaveToJson(T data, string path = null);
    T LoadFromJson(string path = null);
}