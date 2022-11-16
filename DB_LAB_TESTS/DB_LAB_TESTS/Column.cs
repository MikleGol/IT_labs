using System.IO;

namespace DB_LAB_TESTS
{
    public abstract class Column {
        public string Name { get; set; }
        public string Type { get; } = "";

        public Column(string name) {
            Name = name;
        }

        public abstract bool Validate(string value);
    }

    public class IntColumn : Column {
        public new string Type { get; } = "INT";
        public IntColumn(string name) : base(name) { }

        public override bool Validate(string value) => int.TryParse(value, out _);
    }

    public class RealColumn : Column {
        public new string Type { get; }  = "REAL";
        public RealColumn(string name) : base(name) { }

        public override bool Validate(string value) => double.TryParse(value, out _);
    }

    public class CharColumn : Column {
        public new string Type { get; } = "CHAR";
        public CharColumn(string name) : base(name) { }

        public override bool Validate(string value) => char.TryParse(value, out _);
    }

    public class StringColumn : Column {
        public new string Type { get; } = "STRING";
        public StringColumn(string name) : base(name) { }

        public override bool Validate(string value) => true;
    }

    public class TextFileColumn : Column {
        public new string Type { get; }  = "TEXT FILE";
        public TextFileColumn(string name) : base(name) { }

        public override bool Validate(string value) => value.ToLower().EndsWith(".txt") && File.Exists(value);
    }

    public class IntIntervalColumn : Column {
        public new string Type { get; } = "INT INTERVAL";
        public IntIntervalColumn(string name) : base(name) { }

        public override bool Validate(string value) {
            string[] buf = value.Replace(" ", "").Split(',');

            return buf.Length == 2 && int.TryParse(buf[0], out int a) && int.TryParse(buf[1], out int b) && a < b;
        }
    }
}
