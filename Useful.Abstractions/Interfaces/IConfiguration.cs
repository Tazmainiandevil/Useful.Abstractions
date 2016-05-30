using System;
using System.Configuration;
using System.Runtime.Versioning;

namespace Useful.Abstractions.Interfaces
{
    /// <summary>
    /// Interface for System.Configuration
    /// </summary>
    public interface IConfiguration
    {
        #region Default Configuration Properties

        /// <summary>
        /// Gets the System.Configuration.AppSettingsSection object configuration section that applies to this System.Configuration.Configuration object
        /// </summary>
        AppSettingsSection AppSettings { get; }

        /// <summary>
        /// Specifies a function delegate that is used to transform assembly strings in configuration files
        /// </summary>
        Func<string, string> AssemblyStringTransformer { get; set; }

        /// <summary>
        /// Gets a System.Configuration.ConnectionStringsSection configuration-section object that applies to this System.Configuration.Configuration object.
        /// </summary>
        ConnectionStringsSection ConnectionStrings { get; }

        /// <summary>
        /// Gets the System.Configuration.ContextInformation object for the System.Configuration.Configuration object.
        /// </summary>
        ContextInformation EvaluationContext { get; }

        /// <summary>
        /// Gets the physical path to the configuration file represented by this System.Configuration.Configuration object
        /// </summary>
        string FilePath { get; }

        /// <summary>
        /// Gets a value that indicates whether a file exists for the resource represented by this System.Configuration.Configuration object
        /// </summary>
        bool HasFile { get; }

        /// <summary>
        /// Gets the locations defined within this System.Configuration.Configuration object
        /// </summary>
        ConfigurationLocationCollection Locations { get; }

        /// <summary>
        /// Gets or sets a value indicating whether the configuration file has an XML namespace
        /// </summary>
        bool NamespaceDeclared { get; set; }

        /// <summary>
        /// Gets the root System.Configuration.ConfigurationSectionGroup for this System.Configuration.Configuration object
        /// </summary>
        ConfigurationSectionGroup RootSectionGroup { get; }

        /// <summary>
        /// Gets a collection of the section groups defined by this configuration
        /// </summary>
        ConfigurationSectionGroupCollection SectionGroups { get; }

        /// <summary>
        /// Gets a collection of the sections defined by this System.Configuration.Configuration object
        /// </summary>
        ConfigurationSectionCollection Sections { get; }

        /// <summary>
        /// Specifies the targeted version of the .NET Framework when a version earlier than the current one is targeted
        /// </summary>
        FrameworkName TargetFramework { get; set; }

        /// <summary>
        /// Specifies a function delegate that is used to transform type strings in configuration files
        /// </summary>
        Func<string, string> TypeStringTransformer { get; set; }

        #endregion Default Configuration Properties

        #region Default Configuration Methods

        /// <summary>
        /// Returns the specified System.Configuration.ConfigurationSection object
        /// </summary>
        /// <param name="sectionName">The path to the section to be returned</param>
        /// <returns>The specified System.Configuration.ConfigurationSection object</returns>
        ConfigurationSection GetSection(string sectionName);

        /// <summary>
        /// Gets the specified System.Configuration.ConfigurationSectionGroup object
        /// </summary>
        /// <param name="sectionGroupName">The path name of the System.Configuration.ConfigurationSectionGroup to return</param>
        /// <returns>The System.Configuration.ConfigurationSectionGroup specified</returns>
        ConfigurationSectionGroup GetSectionGroup(string sectionGroupName);

        /// <summary>
        /// Writes the configuration settings contained within this System.Configuration.Configuration object to the current XML configuration file
        /// </summary>
        void Save();

        /// <summary>
        /// Writes the configuration settings contained within this System.Configuration.Configuration object to the current XML configuration file
        /// </summary>
        /// <param name="saveMode">A System.Configuration.ConfigurationSaveMode value that determines which property values to save</param>
        void Save(ConfigurationSaveMode saveMode);

        /// <summary>
        /// Writes the configuration settings contained within this System.Configuration.Configuration object to the current XML configuration file
        /// </summary>
        /// <param name="saveMode">A System.Configuration.ConfigurationSaveMode value that determines which property values to save</param>
        /// <param name="forceSaveAll">true to save even if the configuration was not modified; otherwise, false</param>
        void Save(ConfigurationSaveMode saveMode, bool forceSaveAll);

        /// <summary>
        /// Writes the configuration settings contained within this System.Configuration.Configuration object to the specified XML configuration file
        /// </summary>
        /// <param name="filename">The path and file name to save the configuration file to</param>
        void SaveAs(string filename);

        /// <summary>
        /// Writes the configuration settings contained within this System.Configuration.Configuration object to the specified XML configuration file
        /// </summary>
        /// <param name="filename">The path and file name to save the configuration file to</param>
        /// <param name="saveMode">A System.Configuration.ConfigurationSaveMode value that determines which property values to save</param>
        void SaveAs(string filename, ConfigurationSaveMode saveMode);

        /// <summary>
        /// Writes the configuration settings contained within this System.Configuration.Configuration object to the specified XML configuration file
        /// </summary>
        /// <param name="filename">The path and file name to save the configuration file to</param>
        /// <param name="saveMode">A System.Configuration.ConfigurationSaveMode value that determines which property values to save</param>
        /// <param name="forceSaveAll">true to save even if the configuration was not modified; otherwise, false</param>
        void SaveAs(string filename, ConfigurationSaveMode saveMode, bool forceSaveAll);

        #endregion Default Configuration Methods
    }
}