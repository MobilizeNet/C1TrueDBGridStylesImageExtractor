using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace C1TrueDBGridStylesImageExtractorUI
{
    public class ControlWriter : TextWriter
    {
        private Control textbox;
        public ControlWriter(Control textbox)
        {
            this.textbox = textbox;
        }

        public override void Write(char value)
        {
            UpdateText(value);
        }

        public override void Write(string value)
        {
            UpdateText(value);
        }

        public override Encoding Encoding
        {
            get { return Encoding.ASCII; }
        }

        private delegate void UpdateTextDelegate(object value);

        private void UpdateText(object value)
        {
            if (this.textbox.InvokeRequired)
            {
                // This is a worker thread so delegate the task.
                this.textbox.Invoke(new UpdateTextDelegate(this.UpdateText), value);
            }
            else
            {
                // This is the UI thread so perform the task.
                if (textbox.GetType() == typeof(RichTextBox))
                {
                    ((RichTextBox)this.textbox).AppendText(value.ToString());
                }
                else
                {
                    this.textbox.Text += value.ToString();
                }

            }
        }
    }
}
