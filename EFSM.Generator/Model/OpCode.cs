namespace EFSM.Generator.Model
{
    /// <summary>
    /// Logical operators
    /// </summary>
    public enum OpCode : byte
    {
        /// <summary>
        /// Pushes the value of an input onto the stack.
        /// </summary>
        Push = 0,

        /// <summary>
        /// Or's the top two items on the stack, leaving the result on the stack.
        /// </summary>
        Or = 1,

        /// <summary>
        /// And's the top two items on the stack, leaving the result on the stack.
        /// </summary>
        And = 2,

        /// <summary>
        /// Peforms a logic inverse of the item on the stack
        /// </summary>
        Not = 3
    }
}