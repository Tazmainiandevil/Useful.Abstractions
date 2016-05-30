using System.Collections.Specialized;
using System.Configuration;

namespace Useful.Abstractions.Interfaces
{
    /// <summary>
    /// Interface for System.Configuration.ConfigurationManager
    /// </summary>
    public interface IConfigurationManager
    {
        #region Default Configuration Manager Properties

        /// <summary>
        /// Gets the System.Configuration.AppSettingsSection data for the current application's default configuration
        /// </summary>
        NameValueCollection AppSettings { get; }

        /// <summary>
        ///  Gets the System.Configuration.ConnectionStringsSection data for the current application's default configuration.
        /// </summary>
        ConnectionStringSettingsCollection ConnectionStrings { get; }

        #endregion Default Configuration Manager Properties

        #region Default Configuration Manager Methods

        /// <summary>
        //  Retrieves a specified configuration section for the current application's default configuration.
        /// </summary>
        /// <param name="sectionName">The configuration section path and name.</param>
        /// <returns>The specified System.Configuration.ConfigurationSection object, or null if the section does not exist.</returns>
        object GetSection(string sectionName);

        /// <summary>
        /// Opens the configuration file for the current application as a System.Configuration.Configuration object.
        /// </summary>
        /// <param name="userLevel">The System.Configuration.ConfigurationUserLevel for which you are opening the configuration</param>
        /// <returns>A System.Configuration.Configuration object</returns>
        IConfiguration OpenExeConfiguration(ConfigurationUserLevel userLevel);

        /// <summary>
        /// Opens the specified client configuration file as a System.Configuration.Configuration object.
        /// </summary>
        /// <param name="exePath">The path of the executable (exe) file</param>
        /// <returns>A System.Configuration.Configuration object</returns>
        IConfiguration OpenExeConfiguration(string exePath);

        /// <summary>
        /// Opens the machine configuration file on the current computer as a System.Configuration.Configuration object
        /// </summary>
        /// <returns>A System.Configuration.Configuration object</returns>
        IConfiguration OpenMachineConfiguration();

        /// <summary>
        /// Opens the specified client configuration file as a System.Configuration.Configuration object that uses the specified file mapping and user level
        /// </summary>
        /// <param name="fileMap">An System.Configuration.ExeConfigurationFileMap object that references configuration file to use instead of the application default configuration file</param>
        /// <param name="userLevel">The System.Configuration.ConfigurationUserLevel object for which you are opening the configuration</param>
        /// <returns>The configuration object</returns>
        IConfiguration OpenMappedExeConfiguration(ExeConfigurationFileMap fileMap, ConfigurationUserLevel userLevel);

        /// <summary>
        /// Opens the specified client configuration file as a System.Configuration.Configuration object that uses the specified file mapping, user level, and preload option
        /// </summary>
        /// <param name="fileMap">An System.Configuration.ExeConfigurationFileMap object that references the configuration file to use instead of the default application configuration file</param>
        /// <param name="userLevel">The System.Configuration.ConfigurationUserLevel object for which you are opening the configuration</param>
        /// <param name="preLoad">true to preload all section groups and sections; otherwise, false</param>
        /// <returns></returns>
        IConfiguration OpenMappedExeConfiguration(ExeConfigurationFileMap fileMap, ConfigurationUserLevel userLevel, bool preLoad);

        /// <summary>
        /// Opens the machine configuration file as a System.Configuration.Configuration object that uses the specified file mapping
        /// </summary>
        /// <param name="fileMap">An System.Configuration.ExeConfigurationFileMap object that references configuration file to use instead of the application default configuration file</param>
        /// <returns>A System.Configuration.Configuration object</returns>
        IConfiguration OpenMappedMachineConfiguration(ConfigurationFileMap fileMap);

        /// <summary>
        /// Refreshes the named section so the next time that it is retrieved it will be re-read from disk
        /// </summary>
        /// <param name="sectionName">The configuration section name or the configuration path and section name of the section to refresh</param>
        void RefreshSection(string sectionName);

        #endregion Default Configuration Manager Methods

        #region Additional Methods

        /// <summary>
        /// Does the configuration contain a given setting
        /// </summary>
        /// <param name="settingName">The setting to find</param>
        /// <returns>A boolean denoting if the setting exists</returns>
        bool HasSetting(string settingName);

        /// <summary>
        /// Does the configuration contain a given setting and section
        /// </summary>
        /// <param name="settingName">The setting to find</param>
        /// <param name="section">Custom section the setting is in</param>
        /// <returns>A boolean denoting if the setting exists</returns>
        bool HasSetting(string settingName, string section);

        /// <summary>
        /// Does the configuration contain a given connection string
        /// </summary>
        /// <param name="connectionName">The connection to find</param>
        /// <returns>A boolean denoting if the connection string exists</returns>
        bool HasConnectionString(string connectionName);

        /// <summary>
        /// Get a setting from the configuration
        /// </summary>
        /// <param name="name">The setting name</param>
        /// <returns>The setting value</returns>
        T GetSetting<T>(string name);

        /// <summary>
        /// Get a setting from the configuration and section
        /// </summary>
        /// <param name="name">The setting name</param>
        /// <param name="section">The custom section the setting is in</param>
        /// <returns>The setting value</returns>
        T GetSetting<T>(string name, string section);

        /// <summary>
        /// Get a setting from the configuration or the default value if not found
        /// </summary>
        /// <param name="name">The setting name</param>
        /// <param name="fallBack">The default value to fallback to</param>
        /// <returns>The setting value or default</returns>
        T GetSettingOrDefault<T>(string name, T fallBack);

        /// <summary>
        /// Get a setting from the configuration and section or the default value if not found
        /// </summary>
        /// <param name="name">The setting name</param>
        /// <param name="section">The custom section the setting is in</param>
        /// <param name="fallBack">The default value to fallback to</param>
        /// <returns>The setting value or default</returns>
        T GetSettingOrDefault<T>(string name, string section, T fallBack);

        #endregion Additional Methods
    }
}