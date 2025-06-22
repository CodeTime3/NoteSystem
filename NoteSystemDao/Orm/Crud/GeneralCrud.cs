namespace NoteSystemDao.Orm
{
    public class GeneralCrud : IGeneralCrud
    {
        private MySqlConnection _connection;
        private GetNameTableAttribute _getNameTable;
        private StringQueriesBuilderParameter _queryBuilder;

        public GeneralCrud(MySqlConnection connection, GetNameTableAttribute getNameTable, StringQueriesBuilderParameter queryBuilder)
        {
            _connection = connection;
            _getNameTable = getNameTable;
            _queryBuilder = queryBuilder;
        }
        
        public int CreateItem<T>(T item)
        {   
            var nameTable = _getNameTable.GetNameTable(GetGenericType(item));
            var insertQueryParameter = _queryBuilder.CreateItemStringBuilder(GetPropertyInfos(GetGenericType(item)));

            string cmdTxt = $@"insert into {nameTable} {insertQueryParameter}";

            using (var cmd = new MySqlCommand(cmdTxt, _connection))
            {
				if (_connection.State == ConnectionState.Closed)
				{
					_connection.Open();
				}

				foreach (var property in GetPropertyInfos(GetGenericType(item)))
                {
                    cmd.Parameters.AddWithValue($@"@{property.Name}", property.GetValue(item));
                }

                return cmd.ExecuteNonQuery();
            }
        }

        public T ReadItem<T>(T item, object id)
        {   
            Type type = GetGenericType(item);
            var nameTable = _getNameTable.GetNameTable(type);
            var properties = GetPropertyInfos(GetGenericType(item));
            var selectQueryParameter = _queryBuilder.ReadItemStringBuilder(properties, id);
            
            string cmdTxt = $@"select * from {nameTable} {selectQueryParameter}";

            using (var cmd = new MySqlCommand(cmdTxt, _connection))
			{
                if (_connection.State == ConnectionState.Closed)
                {   
                    _connection.Open();
                }                       
                
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        item = (T)Activator.CreateInstance(type);

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            string columnName = reader.GetName(i);

                            PropertyInfo property = null;

                            for (int j = 0; j < properties.Length; j++)
                            {
                                if (properties[j].Name == columnName)
                                {
                                    property = properties[j];
                                    break;
                                }
                            }
                            object value = reader[i];

                            property.SetValue(item, Convert.ChangeType(value, property.PropertyType));
                        }
                    }
                }
            }

            return item;
        }
        
        public T[] ReadItems<T> (T item, object id)
        {
            List<T> items = new List<T>();
            Type type = GetGenericType(item);
			var nameTable = _getNameTable.GetNameTable(GetGenericType(item));
            var properties = GetPropertyInfos(GetGenericType(item));
            var selectQueryParameter = _queryBuilder.ReadItemStringBuilder(properties, id);

            string cmdTxt = $@"select * from {nameTable} {selectQueryParameter}";

            using (var cmd = new MySqlCommand(cmdTxt, _connection))
            {
				if (_connection.State == ConnectionState.Closed)
				{
					_connection.Open();
				}

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read()) 
                    {
                        T newItem = (T)Activator.CreateInstance(type);

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            string columnName = reader.GetName(i);

                            PropertyInfo property = null;

                            for (int j = 0; j < properties.Length; j++)
                            {
                                if (properties[j].Name == columnName)
                                {
                                    property = properties[j];
                                    break;
                                }
                            }
                            object value = reader[i];

                            property.SetValue(newItem, Convert.ChangeType(value, property.PropertyType));
                        }

                        items.Add(newItem);
                    }
                }          
			}

            return items.ToArray();
		}

        public T[] ReadAllItems<T>(T item)
        {
            List<T> items = new List<T>();
            Type type = GetGenericType(item);
			var nameTable = _getNameTable.GetNameTable(GetGenericType(item));
            var properties = GetPropertyInfos(GetGenericType(item));

            string cmdTxt = $@"select * from {nameTable}";

            using (var cmd = new MySqlCommand(cmdTxt, _connection))
            {
				if (_connection.State == ConnectionState.Closed)
				{
					_connection.Open();
				}

                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        T newItem = (T)Activator.CreateInstance(type);

                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            string columnName = reader.GetName(i);

                            PropertyInfo property = null;

                            for (int j = 0; j < properties.Length; j++)
                            {
                                if (properties[j].Name == columnName)
                                {
                                    property = properties[j];
                                    break;
                                }
                            }
                            object value = reader[i];

                            property.SetValue(newItem, Convert.ChangeType(value, property.PropertyType));
                        }

                        items.Add(newItem);
                    }
                }
            }

            return items.ToArray();
        }

        public int UpdateItem<T>(T item, int id)
        {
            var nameTable = _getNameTable.GetNameTable(GetGenericType(item));
            var updateQueryParameter = _queryBuilder.UpdateItemStringBuilder(GetPropertyInfos(GetGenericType(item)));

            string cmdTxt = $@"update {nameTable} {updateQueryParameter}";

            using (var cmd = new MySqlCommand(cmdTxt, _connection))
            {
				if (_connection.State == ConnectionState.Closed)
				{
					_connection.Open();
				}

				foreach (var property in GetPropertyInfos(GetGenericType(item)))
                {
                    if (property.GetCustomAttribute<IgnoreColumnAttribute>() != null)
                    {
                        cmd.Parameters.AddWithValue($@"@{property.Name}", id);
                    }

                    if (property.GetCustomAttribute<IgnoreColumnAttribute>() == null)
                    {
                        cmd.Parameters.AddWithValue($@"@{property.Name}", property.GetValue(item));
                    }
                }

                return cmd.ExecuteNonQuery();
            }
        }

        public int DeleteItem<T>(T item, int id)
        {
            var nameTable = _getNameTable.GetNameTable(GetGenericType(item));
            var deleteQueryParameter = _queryBuilder.DeleteItemStringBuilder(GetPropertyInfos(GetGenericType(item)));

            var cmdTxt = $@"delete from {nameTable} {deleteQueryParameter}";

            using (var cmd = new MySqlCommand(cmdTxt, _connection))
            {
				if (_connection.State == ConnectionState.Closed)
				{
					_connection.Open();
				}

				foreach (var property in GetPropertyInfos(GetGenericType(item)))
                {
                    if (property.GetCustomAttribute<IgnoreColumnAttribute>() != null)
                    {
                        cmd.Parameters.AddWithValue($@"@{property.Name}", id);
                    }
                }

                return cmd.ExecuteNonQuery();
            }
        }

        public int DeleteItems<T>(T item, int id)
        {
            var nameTable = _getNameTable.GetNameTable(GetGenericType(item));
            var deleteQueryParameter = _queryBuilder.DeleteItemsStringBuilder(GetPropertyInfos(GetGenericType(item)));

            var cmdTxt = $@"delete from {nameTable} {deleteQueryParameter}";           

            using (var cmd = new MySqlCommand(cmdTxt, _connection))
            {
				if (_connection.State == ConnectionState.Closed)
				{
					_connection.Open();
				}

				foreach (var property in GetPropertyInfos(GetGenericType(item)))
                {
                    if (property.GetCustomAttribute<SearchAttribute>() != null)
                    {
                        cmd.Parameters.AddWithValue($@"@{property.Name}", id);
                    }
                }

                return cmd.ExecuteNonQuery();
            }
        }
        
        private Type GetGenericType<T>(T item)
        {
            Type type = item.GetType();
            
            return type;
        }

        private PropertyInfo[] GetPropertyInfos(Type type)
        {
            PropertyInfo[] properties = type.GetProperties();

            return properties;
        }
    }
}