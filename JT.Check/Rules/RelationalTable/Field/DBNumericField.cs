namespace JT.Check.Rules.RelationalTable.Field {
    public class DBNumericField<T> : DBField<T> where T : struct {
        public FieldNumberPrecision FieldNumberPrecision { get; set; }
        public FieldNumberScale FieldNumberScale { get; set; }
    }
    /// <summary>
    /// eg.    123.45 : scale = 2 , precision = 5
    /// </summary>
    public class FieldNumberScale : Rule { public int Scale { get; set; } }
    /// <summary>
    /// eg.    123.45 : scale = 2 , precision = 5
    /// </summary>
    public class FieldNumberPrecision : Rule { public int Precision { get; set; } }
}
