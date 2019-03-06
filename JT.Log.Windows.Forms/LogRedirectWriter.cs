using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
namespace JT.Log.Windows.Forms {
    public class LogRedirectWriter : TextWriter {
        TextWriter oldOut;
        TextBoxBase textBox;
        // 远程调用
        delegate void WriteFunc(string value);
        WriteFunc write;
        WriteFunc writeLine;

        bool isReset = false;

        public LogRedirectWriter(TextBoxBase textBox) {
            this.textBox = textBox;
            oldOut = Console.Out;
            Console.SetOut(this);

            write = Write;
            writeLine = WriteLine;
        }


        // 使用UTF-16避免不必要的编码转换
        public override Encoding Encoding {
            get { return Encoding.Unicode; }
        }


        // 最低限度需要重写的方法
        public override void Write(string value) {
            if (textBox.InvokeRequired)
                textBox.BeginInvoke(write, value);
            else
                textBox.AppendText(value);
        }


        // 为提高效率直接处理一行的输出
        public override void WriteLine(string value) {
            if (textBox.InvokeRequired)
                textBox.BeginInvoke(writeLine, value);
            else {
                textBox.AppendText(value);
                textBox.AppendText(this.NewLine);
            }
        }
        public override void Close() {
            if (!isReset) {
                Console.SetOut(oldOut);
                isReset = true;
            }

            base.Close();
        }
        protected override void Dispose(bool disposing) {
            if (!isReset) {
                Console.SetOut(oldOut);
                isReset = true;
            }
            base.Dispose(disposing);
        }
    }
}
