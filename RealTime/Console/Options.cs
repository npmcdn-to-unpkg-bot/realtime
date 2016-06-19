namespace Allors
{
    /// <summary>
    /// The options.
    /// </summary>
    public enum Options
    {
        /// <summary>
        /// Saves the current population to population.xml
        /// </summary>
        Save, 

        /// <summary>
        /// Loads a the population from population.xml
        /// </summary>
        Load, 

        /// <summary>
        /// Creates a new population
        /// </summary>
        Populate, 

        /// <summary>
        /// Upgrades the current population
        /// </summary>
        Upgrade,

        /// <summary>
        /// Custom code snippets for Koen.
        /// </summary>
        Custom,

        /// <summary>
        /// Custom code snippets for Patrick.
        /// </summary>
        CustomPatrick,

        /// <summary>
        /// Investigate data (uses Investigations.cs)
        /// </summary>
        Investigate,

        /// <summary>
        /// Performance checks.
        /// </summary>
        Performance,

        /// <summary>
        /// Exist the application
        /// </summary>
        Exit
    }
}