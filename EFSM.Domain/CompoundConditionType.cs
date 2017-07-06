namespace EFSM.Domain
{
    public enum CompoundConditionType
    {
        /// <summary>
        /// Child conditions are compared with an Or
        /// </summary>
        Or = 0,

        /// <summary>
        /// Child conditions are compared with an And
        /// </summary>
        And = 1
    }
}