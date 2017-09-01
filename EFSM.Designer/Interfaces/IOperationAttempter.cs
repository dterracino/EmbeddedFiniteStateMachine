using System;
using System.Threading.Tasks;

namespace EFSM.Designer.Interfaces
{
    public interface IOperationAttempter
    {
        /// <summary>
        /// Attempt an operation.
        /// </summary>
        /// <param name="action"></param>
        /// <param name="message"></param>
        void Attempt(Action action, string message = "An operation failed.");

        /// <summary>
        /// Attempt an async operation.
        /// </summary>
        /// <param name="func"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        Task AttemptAsync(Func<Task> func, string message);
    }
}
