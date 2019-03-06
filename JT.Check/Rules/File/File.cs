namespace JT.Check.Rules.File {
    public class File : Rule {
        public FileName FileName { get; set; }
        public FileFullName FileFullName { get; set; }
        public FileNameWithoutExtension FileNameWithoutExtension { get; set; }
        public FileExtension FileExtension { get; set; }
    }
    public class FileName : Rule { public string Name { get; set; } }
    public class FileFullName : Rule { public string FullName { get; set; } }
    public class FileNameWithoutExtension : Rule { public string NameWithoutExtension { get; set; } }
    public class FileExtension : Rule { public string Extension { get; set; } }
}
