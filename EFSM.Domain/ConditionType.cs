namespace EFSM.Domain
{
    public enum ConditionType
    {
        /// <summary>
        /// Gets the value of an input
        /// </summary>
        Input = 0,

        /// <summary>
        /// Child conditions are compared with an Or
        /// </summary>
        Or = 1,

        /// <summary>
        /// Child conditions are compared with an And
        /// </summary>
        And = 2,

        /// <summary>
        /// Logical inversion of the single child
        /// </summary>
        Not = 3
    }
}