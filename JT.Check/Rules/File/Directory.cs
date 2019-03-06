namespace JT.Check.Rules.File {
    public class Directory : Rule {
        public DirectoryName DirectoryName { get; set; }
        public DirectoryFullName DirectoryFullName { get; set; }
        public DirectoryEmpty DirectoryEmpty { get; set; }
        public DirectoryDirectorys DirectoryDirectorys { get; set; }
        public DirectoryFiles DirectoryFiles { get; set; }
        public DirectoryFilesCount DirectoryFilesCount { get; set; }
        public DirectoryFilesCountWithExtension DirectoryFilesCountWithExtension { get; set; }
        public DirectoryDirectorysCount DirectoryDirectorysCount { get; set; }
    }
    public class DirectoryName : Rule { public string Name { get; set; }  }
    public class DirectoryFullName : Rule { public string FullName { get; set; } }
    public class DirectoryEmpty : Rule { public bool Empty { get; set; } }
    public class DirectoryDirectorys : Rule { public Directory[] Directorys { get; set; } }
    public class DirectoryFiles : Rule { public File[] Files { get; set; } }
    public class DirectoryFilesCount : Rule { public int FilesCount { get; set; } }
    public class DirectoryFilesCountWithExtension : Rule {
        public int FilesCount { get; set; }
        public string FilesExtension { get; set; }
    }
    public class DirectoryDirectorysCount : Rule { public int DirectorysCount { get; set; } }
    
}