namespace NoteSystemDao.Interfaces
{
    public interface ICreateUpdateDelete<T>
    {
        int AddNewItem(T item);

        int UpdateItem(T item);

        int DeleteItem(int id);
    }
}