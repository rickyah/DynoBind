/// <summary>
/// Establish the type of binding that 
/// .NET must perform on an late binding call
/// </summary>
public enum EOperationType
{
    /// <summary>
    /// The operation calls a method
    /// </summary>
    METHOD,

    /// <summary>
    /// The operation sets the value for a property
    /// </summary>
    PROPERTY_GET,

    /// <summary>
    /// The operation retrieves a value for a property
    /// </summary>
    PROPERTY_SET,

    /// <summary>
    /// The operation gets the value for a field
    /// </summary>
    FIELD_GET,

    /// <summary>
    /// The operation sets the value for a field
    /// </summary>
    FIELD_SET
}