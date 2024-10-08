﻿namespace NoteSystemDao.Orm
{
    public class GeneralCrud
    {
        private readonly MySqlConnection _connection;

        public GeneralCrud(MySqlConnection connection)
        {
            _connection = connection;
        }
        
        public int CreateItem<T>(T item)
        {
            Type type = item.GetType();
            NameTableAttribute tableOrm = (NameTableAttribute)Attribute.GetCustomAttribute(type, typeof(NameTableAttribute));
            PropertyInfo[] properties = type.GetProperties();
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

            columnList = columnList.TrimEnd(',', ' ');
            parameterList = parameterList.TrimEnd(',', ' ');
            parameterList += ")";

            string cmdTxt = $@"insert into {tableOrm.Name} ({columnList}) values {parameterList}";

            using (var cmd = new MySqlCommand(cmdTxt, _connection))
            {
				if (_connection.State == ConnectionState.Closed)
				{
					_connection.Open();
				}

				foreach (var property in properties)
                {
                    cmd.Parameters.AddWithValue($@"@{property.Name}", property.GetValue(item));
                }

                return cmd.ExecuteNonQuery();
            }
        }

        public T ReadItem<T>(T item, object id)
        {
            Type type = typeof(T);
            NameTableAttribute tableOrm = (NameTableAttribute)Attribute.GetCustomAttribute(type, typeof(NameTableAttribute));
            PropertyInfo[] properties = type.GetProperties();

            string selectString = "";

            foreach (var property in properties)
            {
                if (property.GetCustomAttribute<SearchAttribute>() != null)
                {
                    selectString += property.Name;
                }
            }
            
            string cmdTxt = $@"select * from {tableOrm.Name} where {selectString} like binary '%{id}%'";

            using (var cmd = new MySqlCommand(cmdTxt, _connection))
			{
                if (_connection.State == ConnectionState.Closed)
                {   
                    _connection.Open();
                }               

                foreach (var property in properties)
                {
                    if (property.GetCustomAttribute<SearchAttribute>() == null)
                    {
                        cmd.Parameters.AddWithValue($@"@{property.Name}", property.GetValue(item));
                    }
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
            Type type = typeof (T);

			NameTableAttribute tableOrm = (NameTableAttribute)Attribute.GetCustomAttribute(type, typeof(NameTableAttribute));
			PropertyInfo[] properties = type.GetProperties();

            string selectString = "";

            foreach (var property in properties)
            {
                if (property.GetCustomAttribute<SearchAttribute>() != null)
                {
                    selectString += property.Name;
                }
            }

            string cmdTxt = $@"select * from {tableOrm.Name} where {selectString} = {id}";

            using (var cmd = new MySqlCommand(cmdTxt, _connection))
            {
				if (_connection.State == ConnectionState.Closed)
				{
					_connection.Open();
				}

                foreach (var property in properties)
                {
                    if (property.GetCustomAttribute<SearchAttribute>() == null)
                    {
                        cmd.Parameters.AddWithValue($@"@{property.Name}", property.GetValue(item));
                    }
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
            Type type = typeof(T);

            NameTableAttribute tableOrm = (NameTableAttribute)Attribute.GetCustomAttribute(type, typeof(NameTableAttribute));
            PropertyInfo[] properties = type.GetProperties();

            string cmdTxt = $@"select * from {tableOrm.Name}";

            using (var cmd = new MySqlCommand(cmdTxt, _connection))
            {
				if (_connection.State == ConnectionState.Closed)
				{
					_connection.Open();
				}

				foreach (var property in properties)
                {
                    if (property.GetCustomAttribute<IgnoreColumnAttribute>() == null)
                    {
                        cmd.Parameters.AddWithValue($@"@{property.Name}", property.GetValue(item));
                    }
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
            Type type = item.GetType();
            NameTableAttribute tableOrm = (NameTableAttribute)Attribute.GetCustomAttribute(type, typeof(NameTableAttribute));
            PropertyInfo[] properties = type.GetProperties();

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

            setFinalString = setFinalString.TrimEnd(',', ' ');

            string cmdTxt = $@"update {tableOrm.Name} set {setFinalString} where {columnId} = @{columnId}";

            using (var cmd = new MySqlCommand(cmdTxt, _connection))
            {
				if (_connection.State == ConnectionState.Closed)
				{
					_connection.Open();
				}

				foreach (var property in properties)
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
            Type type = item.GetType();
            NameTableAttribute tableOrm = (NameTableAttribute)Attribute.GetCustomAttribute(type, typeof(NameTableAttribute));
            PropertyInfo[] properties = type.GetProperties();

            string columnId = "";

            foreach (var property in properties)
            {
                if (property.GetCustomAttribute<IgnoreColumnAttribute>() != null)
                {
                    columnId += property.Name;
                }
            }

            var cmdTxt = $@"delete from {tableOrm.Name} where {columnId} = @{columnId}";

            using (var cmd = new MySqlCommand(cmdTxt, _connection))
            {
				if (_connection.State == ConnectionState.Closed)
				{
					_connection.Open();
				}

				foreach (var property in properties)
                {
                    if (property.GetCustomAttribute<IgnoreColumnAttribute>() != null)
                    {
                        cmd.Parameters.AddWithValue($@"@{columnId}", id);
                    }
                }

                return cmd.ExecuteNonQuery();
            }
        }

        public int DeleteItems<T>(T item, int id)
        {
            Type type = item.GetType();
            NameTableAttribute tableOrm = (NameTableAttribute)Attribute.GetCustomAttribute(type, typeof(NameTableAttribute));
            PropertyInfo[] properties = type.GetProperties();

            string columnId = "";

            foreach (var property in properties)
            {
                if (property.GetCustomAttribute<SearchAttribute>() != null)
                {
                    columnId += property.Name;
                }
            }

            var cmdTxt = $@"delete from {tableOrm.Name} where {columnId} = @{columnId}";

            using (var cmd = new MySqlCommand(cmdTxt, _connection))
            {
				if (_connection.State == ConnectionState.Closed)
				{
					_connection.Open();
				}

				foreach (var property in properties)
                {
                    if (property.GetCustomAttribute<SearchAttribute>() != null)
                    {
                        cmd.Parameters.AddWithValue($@"@{columnId}", id);
                    }
                }

                return cmd.ExecuteNonQuery();
            }
        }
    }
}