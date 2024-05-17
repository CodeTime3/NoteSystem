namespace NoteSystemDao.Interfaces
{
    public interface ISelectItems<T>
    {
        T[] GetAllItems();

        T[] GetItems(string str);
    }
}