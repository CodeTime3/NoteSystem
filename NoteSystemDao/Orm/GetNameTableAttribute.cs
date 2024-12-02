namespace NoteSystemDao.Orm
{
    public class GetNameTableAttribute
    {
        public string GetNameTable(Type type) 
        {
            NameTableAttribute tableOrm = (NameTableAttribute)Attribute.GetCustomAttribute(type, typeof(NameTableAttribute))!;

            if(tableOrm is null)
            {
                return type.Name;
            }

            if(tableOrm.Name.Equals(""))
            {
                return type.Name;
            }

            return tableOrm.Name;
        }
    }
}