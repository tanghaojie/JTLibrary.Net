namespace JT.Check.Rules.RelationalTable.Field {
    public class DBField<T> : Field<T> where T : struct {
        public FieldType<T> FieldType { get; set; }
        public FieldLength FieldLength { get; set; }
        public FieldNotNull FieldNotNull { get; set; }
    }
    public class FieldType<T> : Rule where T : struct { public T Type { get; set; } }
    public class FieldLength : Rule { public int Length { get; set; } }
    public class FieldNotNull : Rule { public bool NotNull { get; set; } }
}
