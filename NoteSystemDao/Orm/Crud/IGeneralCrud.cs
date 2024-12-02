namespace NoteSystemDao.Orm
{
    public interface IGeneralCrud
    {
        int CreateItem<T>(T item);
        T ReadItem<T>(T item, object id);
        T[] ReadItems<T> (T item, object id);
        T[] ReadAllItems<T>(T item);
        int UpdateItem<T>(T item, int id);
        int DeleteItem<T>(T item, int id);
        int DeleteItems<T>(T item, int id);
    }
}