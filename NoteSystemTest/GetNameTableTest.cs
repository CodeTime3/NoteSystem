namespace NoteSystemTest
{
    public class GetNameTableTest
    {   
        [Fact]
        public void GetUserNameTable_should_work() 
        {   
            GetNameTableAttribute getNameTable = new GetNameTableAttribute();
            UserDbItem user = new UserDbItem();

            Assert.Equal("users", getNameTable.GetNameTable(GetGenericType(user)));
        }

        [Fact]
        public void GetClassTestNameTable_should_work() 
        {   
            GetNameTableAttribute getNameTable = new GetNameTableAttribute();
            ClassTest test = new ClassTest();

            Assert.Equal("ClassTest", getNameTable.GetNameTable(GetGenericType(test)));
        }

        [Fact]
        public void GetNameTable_with_empty_string()
        {
            GetNameTableAttribute getNameTable = new GetNameTableAttribute();
            EmptyClass emptyClass = new EmptyClass();

            Assert.Equal("EmptyClass", getNameTable.GetNameTable(GetGenericType(emptyClass)));
        }

        private Type GetGenericType<T>(T item)
        {
            Type type = item.GetType();
            
            return type;
        }
    }

    class ClassTest
    {

    }

    [NameTable("")]
    class EmptyClass
    {

    }
}