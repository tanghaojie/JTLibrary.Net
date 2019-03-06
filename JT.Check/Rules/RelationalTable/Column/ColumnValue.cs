namespace JT.Check.Rules.RelationalTable.Column {
    public class ColumnValue<T> : Rule {
        public ColumnValueGreaterThan<T> ColumnValueGreaterThan { get; set; }
        public ColumnValueGreaterThanOrEqual<T> ColumnValueGreaterThanOrEqual { get; set; }
        public ColumnValueLessThan<T> ColumnValueLessThan { get; set; }
        public ColumnValueLessThanOrEqual<T> ColumnValueLessThanOrEqual { get; set; }
        public ColumnValueContain<T> ColumnValueContain { get; set; }
        public ColumnValueNotContain<T> ColumnValueNotContain { get; set; }
    }
    public class ColumnValueGreaterThan<T> : Rule { public T GreaterThan { get; set; } }
    public class ColumnValueGreaterThanOrEqual<T> : Rule { public T GreaterThanOrEqual { get; set; } }
    public class ColumnValueLessThan<T> : Rule { public T LessThan { get; set; } }
    public class ColumnValueLessThanOrEqual<T> : Rule { public T LessThanOrEqual { get; set; } }
    public class ColumnValueContain<T> : Rule { public T[] Contains { get; set; } }
    public class ColumnValueNotContain<T> : Rule { public T[] NotContains { get; set; } }
}
