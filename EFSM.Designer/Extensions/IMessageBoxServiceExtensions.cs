using Cas.Common.WPF.Interfaces;
using System;
using System.Windows;

namespace EFSM.Designer.Extensions
{
    public static class IMessageBoxServiceExtensions
    {
        /// <summary>
        /// Displays an exception.
        /// </summary>
        /// <param name="messageBoxService"></param>
        /// <param name="ex"></param>
        /// <param name="message"></param>
        public static void Show(this IMessageBoxService messageBoxService, Exception ex, string message = null)
        {
            if (messageBoxService == null) throw new ArgumentNullException(nameof(messageBoxService));
            if (ex == null) throw new ArgumentNullException(nameof(ex));

            var caption = message ?? ex.GetType().Name;

            messageBoxService.Show(ex.Message, caption, MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
