namespace NoteSystemTest
{
    public class StringQueriesBuilderParameterTest
    {
        private StringQueriesBuilderParameter stringBuilder = new StringQueriesBuilderParameter();

        [Fact]
        public void CreateItemStringBuilder_should_work()
        {
            UserDbItem user = new UserDbItem();
            var finalString = stringBuilder.CreateItemStringBuilder(propertyInfos(user));
            var expectedString = $@"(UserName, UserHash) values (@UserName, @UserHash)";
            
            Assert.Equal(expectedString, finalString);
        }

        [Fact]
        public void ReadItemStringBuilder_should_work()
        {
            UserDbItem user =new UserDbItem();
            var finalString = stringBuilder.ReadItemStringBuilder(propertyInfos(user), "hh");
            var expectedString = $@"where UserName like binary '%hh%'";

            Assert.Equal(expectedString, finalString);
        }

        [Fact]
        public void UpdateItemStringBuilder_should_work()
        {
            UserDbItem user = new UserDbItem();
            var finalString = stringBuilder.UpdateItemStringBuilder(propertyInfos(user));
            var expectedString = $@"set UserName = @UserName, UserHash = @UserHash where UserId = @UserId";

            Assert.Equal(expectedString, finalString);
        }

        [Fact]
        public void DeleteItemStringBuilder_should_work()
        {
            UserDbItem user = new UserDbItem();
            var finalString = stringBuilder.DeleteItemStringBuilder(propertyInfos(user));
            var expectedString = $@"where UserId = @UserId";

            Assert.Equal(expectedString, finalString);
        }

        private PropertyInfo[] propertyInfos<T>(T item)
        {
            Type type = item.GetType();
            PropertyInfo[] properties = type.GetProperties();

            return properties;
        }
    }
}