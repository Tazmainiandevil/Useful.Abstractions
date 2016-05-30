using System;
using System.Configuration;
using System.Runtime.Versioning;
using Useful.Abstractions.Interfaces;

namespace Useful.Abstractions.Configuration
{
    /// <summary>
    /// Wrapper class for the Configuration to aid in injection and testability
    /// </summary>
    public class ConfigurationWrapper : IConfiguration
    {
        /// <summary>
        /// The configuration object
        /// </summary>
        private readonly System.Configuration.Configuration _configuration;

        #region Default Configuration Properties

        /// <summary>
        /// Gets the System.Configuration.AppSettingsSection object configuration section that applies to this System.Configuration.Configuration object
        /// </summary>
        public AppSettingsSection AppSettings => _configuration.AppSettings;

        /// <summary>
        /// Specifies a function delegate that is used to transform assembly strings in configuration files
        /// </summary>
        public Func<string, string> AssemblyStringTransformer
        {
            get { return _configuration.AssemblyStringTransformer; }
            set { _configuration.AssemblyStringTransformer = value; }
        }

        /// <summary>
        /// Gets a System.Configuration.ConnectionStringsSection configuration-section object that applies to this System.Configuration.Configuration object.
        /// </summary>
        public ConnectionStringsSection ConnectionStrings => _configuration.ConnectionStrings;

        /// <summary>
        /// Gets the System.Configuration.ContextInformation object for the System.Configuration.Configuration object.
        /// </summary>
        public ContextInformation EvaluationContext => _configuration.EvaluationContext;

        /// <summary>
        /// Gets the physical path to the configuration file represented by this System.Configuration.Configuration object
        /// </summary>
        public string FilePath => _configuration.FilePath;

        /// <summary>
        /// Gets a value that indicates whether a file exists for the resource represented by this System.Configuration.Configuration object
        /// </summary>
        public bool HasFile => _configuration.HasFile;

        /// <summary>
        /// Gets the locations defined within this System.Configuration.Configuration object
        /// </summary>
        public ConfigurationLocationCollection Locations => _configuration.Locations;

        /// <summary>
        /// Gets or sets a value indicating whether the configuration file has an XML namespace
        /// </summary>
        public bool NamespaceDeclared
        {
            get { return _configuration.NamespaceDeclared; }
            set { _configuration.NamespaceDeclared = value; }
        }

        /// <summary>
        /// Gets the root System.Configuration.ConfigurationSectionGroup for this System.Configuration.Configuration object
        /// </summary>
        public ConfigurationSectionGroup RootSectionGroup => _configuration.RootSectionGroup;

        /// <summary>
        /// Gets a collection of the section groups defined by this configuration
        /// </summary>
        public ConfigurationSectionGroupCollection SectionGroups => _configuration.SectionGroups;

        /// <summary>
        /// Gets a collection of the sections defined by this System.Configuration.Configuration object
        /// </summary>
        public ConfigurationSectionCollection Sections => _configuration.Sections;

        /// <summary>
        /// Specifies the targeted version of the .NET Framework when a version earlier than the current one is targeted
        /// </summary>
        public FrameworkName TargetFramework { get { return _configuration.TargetFramework; } set { _configuration.TargetFramework = value; } }

        /// <summary>
        /// Specifies a function delegate that is used to transform type strings in configuration files
        /// </summary>
        public Func<string, string> TypeStringTransformer
        {
            get { return _configuration.TypeStringTransformer; }
            set { _configuration.TypeStringTransformer = value; }
        }

        #endregion Default Configuration Properties

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration">The system configuration</param>
        public ConfigurationWrapper(System.Configuration.Configuration configuration)
        {
            _configuration = configuration;
        }

        #region Default Configuration Methods

        /// <summary>
        /// Returns the specified System.Configuration.ConfigurationSection object
        /// </summary>
        /// <param name="sectionName">The path to the section to be returned</param>
        /// <returns>The specified System.Configuration.ConfigurationSection object</returns>
        public ConfigurationSection GetSection(string sectionName)
        {
            return _configuration.GetSection(sectionName);
        }

        /// <summary>
        /// Gets the specified System.Configuration.ConfigurationSectionGroup object
        /// </summary>
        /// <param name="sectionGroupName">The path name of the System.Configuration.ConfigurationSectionGroup to return</param>
        /// <returns>The System.Configuration.ConfigurationSectionGroup specified</returns>
        public ConfigurationSectionGroup GetSectionGroup(string sectionGroupName)
        {
            return _configuration.GetSectionGroup(sectionGroupName);
        }

        /// <summary>
        /// Writes the configuration settings contained within this System.Configuration.Configuration object to the current XML configuration file
        /// </summary>
        public void Save()
        {
            _configuration.Save();
        }

        /// <summary>
        /// Writes the configuration settings contained within this System.Configuration.Configuration object to the current XML configuration file
        /// </summary>
        /// <param name="saveMode">A System.Configuration.ConfigurationSaveMode value that determines which property values to save</param>
        public void Save(ConfigurationSaveMode saveMode)
        {
            _configuration.Save(saveMode);
        }

        /// <summary>
        /// Writes the configuration settings contained within this System.Configuration.Configuration object to the current XML configuration file
        /// </summary>
        /// <param name="saveMode">A System.Configuration.ConfigurationSaveMode value that determines which property values to save</param>
        /// <param name="forceSaveAll">true to save even if the configuration was not modified; otherwise, false</param>
        public void Save(ConfigurationSaveMode saveMode, bool forceSaveAll)
        {
            _configuration.Save(saveMode, forceSaveAll);
        }

        /// <summary>
        /// Writes the configuration settings contained within this System.Configuration.Configuration object to the specified XML configuration file
        /// </summary>
        /// <param name="filename">The path and file name to save the configuration file to</param>
        public void SaveAs(string filename)
        {
            _configuration.SaveAs(filename);
        }

        /// <summary>
        /// Writes the configuration settings contained within this System.Configuration.Configuration object to the specified XML configuration file
        /// </summary>
        /// <param name="filename">The path and file name to save the configuration file to</param>
        /// <param name="saveMode">A System.Configuration.ConfigurationSaveMode value that determines which property values to save</param>
        public void SaveAs(string filename, ConfigurationSaveMode saveMode)
        {
            _configuration.SaveAs(filename, saveMode);
        }

        /// <summary>
        /// Writes the configuration settings contained within this System.Configuration.Configuration object to the specified XML configuration file
        /// </summary>
        /// <param name="filename">The path and file name to save the configuration file to</param>
        /// <param name="saveMode">A System.Configuration.ConfigurationSaveMode value that determines which property values to save</param>
        /// <param name="forceSaveAll">true to save even if the configuration was not modified; otherwise, false</param>
        public void SaveAs(string filename, ConfigurationSaveMode saveMode, bool forceSaveAll)
        {
            _configuration.SaveAs(filename, saveMode, forceSaveAll);
        }

        #endregion Default Configuration Methods
    }
}