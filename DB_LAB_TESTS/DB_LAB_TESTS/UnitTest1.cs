namespace DB_LAB_TESTS { 
using Xunit;


    public class TestValidation
    {
        private Column _column;

        [Fact]
        public void TestIntColumnValidation1()
        {
            _column = new IntColumn("");
            Assert.True(_column.Validate("1"));
        }

        [Fact]
        public void TestIntColumnValidation2()
        {
            _column = new IntColumn("");
            Assert.False(_column.Validate("dasdf"));
        }

        [Fact]
        public void TestRealColumnValidation1()
        {
            _column = new RealColumn("");
            Assert.True(_column.Validate("22.42"));
        }

        [Fact]
        public void TestRealColumnValidation2()
        {
            _column = new RealColumn("");
            Assert.False(_column.Validate("5555a555"));
        }

        [Fact]
        public void TestCharColumnValidation1()
        {
            _column = new CharColumn("");
            Assert.True(_column.Validate("T"));
        }

        [Fact]
        public void TestCharColumnValidation2()
        {
            _column = new CharColumn("");
            Assert.False(_column.Validate("kgb"));
        }

        [Fact]
        public void TestStringColumnValidation()
        {
            _column = new StringColumn("");
            Assert.True(_column.Validate("sttt"));
        }

        [Fact]
        public void TestIntIntervalColumnValidation1()
        {
            _column = new IntIntervalColumn("");
            Assert.True(_column.Validate("6, 4, 6"));
        }

        [Fact]
        public void TestIntIntervalColumnValidation2()
        {
            _column = new IntIntervalColumn("");
            Assert.False(_column.Validate("2, 1"));
        }

        [Fact]
        public void TestIntIntervalColumnValidation3()
        {
            _column = new IntIntervalColumn("");
            Assert.False(_column.Validate("6.2, 5"));
        }
    }
}
