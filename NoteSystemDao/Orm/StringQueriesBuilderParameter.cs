namespace NoteSystemDao.Orm
{
    public class StringQueriesBuilderParameter
    {
        public string CreateItemStringBuilder(PropertyInfo[] properties) 
        {   
            string columnList = "";
            string parameterList = "(";

            foreach (var property in properties)
            {
                if (property.GetCustomAttribute<IgnoreColumnAttribute>() == null)
                {
                    columnList += property.Name + ", ";
                    parameterList += $"@{property.Name}, ";
                }
            }

            columnList = StringEndFormater(columnList);
            parameterList = StringEndFormater(parameterList);
            parameterList += ")";

            return $@"({columnList}) values {parameterList}";
        }

        public string ReadItemStringBuilder(PropertyInfo[] properties, object id) 
        {   
            string selectString = "";

            foreach (var property in properties)
            {
                if (property.GetCustomAttribute<SearchAttribute>() != null)
                {
                    selectString += property.Name;
                }
            }

            return $@"where {selectString} like binary '%{id}%'";
        }

        public string UpdateItemStringBuilder(PropertyInfo[] properties) 
        {   
            string columnId = "";
            string columnList = "";
            string parameterList = "";
            string setFinalString = "";

            foreach (var property in properties)
            {
                if (property.GetCustomAttribute<IgnoreColumnAttribute>() != null)
                {
                    columnId += property.Name;
                }

                if (property.GetCustomAttribute<IgnoreColumnAttribute>() == null)
                {
                    columnList += property.Name + " = ";
                    parameterList += $"@{property.Name}" + ", ";
                    setFinalString += columnList + parameterList;
                    columnList = "";
                    parameterList = "";
                }
            }

            setFinalString = StringEndFormater(setFinalString);

            return $@"set {setFinalString} where {columnId} = @{columnId}";
        }

        public string DeleteItemStringBuilder(PropertyInfo[] properties) 
        {   
            string columnId = "";

            foreach (var property in properties)
            {
                if (property.GetCustomAttribute<IgnoreColumnAttribute>() != null)
                {
                    columnId += property.Name;
                }
            }

            return $@"where {columnId} = @{columnId}";
        }

        public string DeleteItemsStringBuilder(PropertyInfo[] properties) 
        {   
            string columnId = "";

            foreach (var property in properties)
            {
                if (property.GetCustomAttribute<SearchAttribute>() != null)
                {
                    columnId += property.Name;
                }
            }

            return $@"where {columnId} = @{columnId}";
        }

        private string StringEndFormater(string stringToFormater)
        {
            return stringToFormater.TrimEnd(',', ' ');
        }
    }
}