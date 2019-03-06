using System;
using System.Collections.Generic;
using System.Text;

namespace JT.Check.Data {
    public partial class FileData : IData {
        public FileData(string fullFilename) {
            Name = null;
            FullFilename = fullFilename;
        }
        public FileData(string fullFilename, string name) {
            Name = name;
            FullFilename = fullFilename;
        }
    }
    public partial class FileData {
        public string Name { get; }
        public string FullFilename { get; }
    }
}
