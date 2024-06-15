namespace NoteSystemDao.Orm.MyAttributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class NameTableAttribute : Attribute
    {
        public string Name { get; set; }

        public NameTableAttribute(string name)
        {
            Name = name;
        }
    }
}
