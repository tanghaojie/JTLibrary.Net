using log4net.Core;
using System.Collections.Generic;
using System.Windows.Forms;
namespace JT.Log.Windows.Forms {
    public partial class JTTextBoxAppender : log4net.Appender.AppenderSkeleton {
        protected override void Append(LoggingEvent loggingEvent) {
            Write(RenderLoggingEvent(loggingEvent));
        }
    }
    public partial class JTTextBoxAppender {
        public void Add(TextBoxBase textBoxBase) {
            TextBoxs.Add(textBoxBase);
        }
        public void Remove(TextBoxBase textBoxBase) {
            TextBoxs.Remove(textBoxBase);
        }
        public void Clear() {
            TextBoxs.Clear();
        }
    }
    public partial class JTTextBoxAppender {
        private IList<TextBoxBase> TextBoxs { get; } = new List<TextBoxBase>();
        private void Write(string text) {
            if (TextBoxs == null) { return; }
            foreach (var textBox in TextBoxs) {
                AppendText(textBox, text);
            }
        }
    }
    public partial class JTTextBoxAppender {
        private delegate void AppendTextDelegate(TextBoxBase textBoxBase, string text);
        private void AppendText(TextBoxBase textBox, string text) {
            if (textBox == null) { return; }
            if (textBox.InvokeRequired) {
                textBox.BeginInvoke(new AppendTextDelegate(AppendText), new object[] { textBox, text });
            } else {
                textBox.AppendText(text);
            }
        }
    }
}
