namespace JT.Check.Rules.RelationalTable.Field {
    public class Field<T> : Rule where T : struct {
        public FieldName FieldName { get; set; }
    }
    public class FieldName : Rule { public string Name { get; set; } }
}
