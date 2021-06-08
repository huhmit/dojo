using System.Windows.Forms;

namespace Dojo.Services
{
    public class MessageBoxService : IMessageBoxService
    {
        #region Public Methods

        /// <summary>
        /// Builds and shows a Windows forms.
        /// Designed for success messages.
        /// </summary>
        /// <param name="message">Main content of the MessageBox</param>
        /// <param name="title">Title of the MessageBox, defaults to "Success", can be overwritten</param>
        /// <param name="buttons">Buttons of the MessageBox, defaults to MessageBoxButtons.OK, can be overwritten</param>
        public void ShowSuccessDialog(string message, string title = "Success", MessageBoxButtons buttons = MessageBoxButtons.OK)
        {
            MessageBox.Show(message, title, buttons, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Builds and shows a Windows form.
        /// Designed for warning messages.
        /// </summary>
        /// <param name="message">Main content of the MessageBox</param>
        /// <param name="title">Title of the MessageBox, defaults to "Success", can be overwritten</param>
        /// <param name="buttons">Buttons of the MessageBox, defaults to MessageBoxButtons.OK, can be overwritten</param>
        /// <returns>DialogResult, can be Yes/No</returns>
        public DialogResult ShowWarningDialog(string message, string title = "Warning", MessageBoxButtons buttons = MessageBoxButtons.YesNo)
        {
            return MessageBox.Show(message, title, buttons, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Builds and shows a Windows form.
        /// Designed for error messages.
        /// </summary>
        /// <param name="message">Main content of the MessageBox</param>
        /// <param name="title">Title of the MessageBox, defaults to "Success", can be overwritten</param>
        /// <param name="buttons">Buttons of the MessageBox, defaults to MessageBoxButtons.OK, can be overwritten</param>
        public void ShowErrorDialog(string message, string title = "Error", MessageBoxButtons buttons = MessageBoxButtons.OK)
        {
            MessageBox.Show(message, title, buttons, MessageBoxIcon.Error);
        }

        #endregion Public Methods
    }

    public interface IMessageBoxService
    {
        void ShowSuccessDialog(string message, string title = "Success", MessageBoxButtons buttons = MessageBoxButtons.OK);

        DialogResult ShowWarningDialog(string message, string title = "Warning", MessageBoxButtons buttons = MessageBoxButtons.YesNo);

        void ShowErrorDialog(string message, string title = "Error", MessageBoxButtons buttons = MessageBoxButtons.OK);
    }
}
