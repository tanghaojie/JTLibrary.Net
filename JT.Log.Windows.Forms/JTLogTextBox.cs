using System;
using System.Windows.Forms;
namespace JT.Log.Windows.Forms {
    public partial class JTLogTextBox : IDisposable {
        public JTLogTextBox(TextBoxBase textBoxBase) {
            if (textBoxBase == null) { return; }
            this.textBoxBase = textBoxBase;
            var repositories = log4net.LogManager.GetAllRepositories();
            if (repositories == null || repositories.Length <= 0) { return; }
            jtTextBoxAppender = null;
            foreach (var repository in repositories) {
                var appenders = repository.GetAppenders();
                if (appenders == null || appenders.Length <= 0) { continue; }
                var find = false;
                foreach (var appender in appenders) {
                    if (appender is JTTextBoxAppender a) {
                        jtTextBoxAppender = a;
                        find = true;
                        break;
                    }
                }
                if (find) { break; }
            }
            if (jtTextBoxAppender == null) {
                jtTextBoxAppender = new JTTextBoxAppender { Layout = new log4net.Layout.PatternLayout("%date [%-5level]: - %message%newline") };
                log4net.Config.BasicConfigurator.Configure(jtTextBoxAppender);
            }
            jtTextBoxAppender.Add(textBoxBase);
        }

        public void Dispose() {
            if (jtTextBoxAppender == null || textBoxBase == null) { return; }
            jtTextBoxAppender.Remove(textBoxBase);
            textBoxBase = null;
        }
    }
    public partial class JTLogTextBox {
        private JTTextBoxAppender jtTextBoxAppender;
        private TextBoxBase textBoxBase;
    }
    public partial class JTLogTextBox {
        public class NoRepositoriesExistException : Exception {
            public NoRepositoriesExistException() : base() { }
            public NoRepositoriesExistException(string message) : base(message) { }
        }
        public class NoJTTextBoxAppenderExistException : Exception {
            public NoJTTextBoxAppenderExistException() : base() { }
        }
    }
}
