using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Linq;
using Useful.Abstractions.Interfaces;

namespace Useful.Abstractions.Configuration
{
    /// <summary>
    /// Wrapper class for the Configuration Manager to aid in injection and testability
    /// </summary>
    public class ConfigurationManagerWrapper : IConfigurationManager
    {
        #region Default Configuration Manager Properties

        /// <summary>
        /// Gets the System.Configuration.AppSettingsSection data for the current application's default configuration
        /// </summary>
        public NameValueCollection AppSettings => ConfigurationManager.AppSettings;

        /// <summary>
        ///  Gets the System.Configuration.ConnectionStringsSection data for the current application's default configuration.
        /// </summary>
        public ConnectionStringSettingsCollection ConnectionStrings => ConfigurationManager.ConnectionStrings;

        #endregion Default Configuration Manager Properties

        #region Default Configuration Manager Methods

        /// <summary>
        //  Retrieves a specified configuration section for the current application's default configuration.
        /// </summary>
        /// <param name="sectionName">The configuration section path and name.</param>
        /// <returns>The specified System.Configuration.ConfigurationSection object, or null if the section does not exist.</returns>
        public object GetSection(string sectionName)
        {
            return ConfigurationManager.GetSection(sectionName);
        }

        /// <summary>
        /// Opens the configuration file for the current application as a System.Configuration.Configuration object.
        /// </summary>
        /// <param name="userLevel">The System.Configuration.ConfigurationUserLevel for which you are opening the configuration</param>
        /// <returns>A System.Configuration.Configuration object</returns>
        public IConfiguration OpenExeConfiguration(ConfigurationUserLevel userLevel)
        {
            return new ConfigurationWrapper(ConfigurationManager.OpenExeConfiguration(userLevel));
        }

        /// <summary>
        /// Opens the specified client configuration file as a System.Configuration.Configuration object.
        /// </summary>
        /// <param name="exePath">The path of the executable (exe) file</param>
        /// <returns>A System.Configuration.Configuration object</returns>
        public IConfiguration OpenExeConfiguration(string exePath)
        {
            return new ConfigurationWrapper(ConfigurationManager.OpenExeConfiguration(exePath));
        }

        /// <summary>
        /// Opens the machine configuration file on the current computer as a System.Configuration.Configuration object
        /// </summary>
        /// <returns>A System.Configuration.Configuration object</returns>
        public IConfiguration OpenMachineConfiguration()
        {
            return new ConfigurationWrapper(ConfigurationManager.OpenMachineConfiguration());
        }

        /// <summary>
        /// Opens the specified client configuration file as a System.Configuration.Configuration object that uses the specified file mapping and user level
        /// </summary>
        /// <param name="fileMap">An System.Configuration.ExeConfigurationFileMap object that references configuration file to use instead of the application default configuration file</param>
        /// <param name="userLevel">The System.Configuration.ConfigurationUserLevel object for which you are opening the configuration</param>
        /// <returns>The configuration object</returns>
        public IConfiguration OpenMappedExeConfiguration(ExeConfigurationFileMap fileMap, ConfigurationUserLevel userLevel)
        {
            return new ConfigurationWrapper(ConfigurationManager.OpenMappedExeConfiguration(fileMap, userLevel));
        }

        /// <summary>
        /// Opens the specified client configuration file as a System.Configuration.Configuration object that uses the specified file mapping, user level, and preload option
        /// </summary>
        /// <param name="fileMap">An System.Configuration.ExeConfigurationFileMap object that references the configuration file to use instead of the default application configuration file</param>
        /// <param name="userLevel">The System.Configuration.ConfigurationUserLevel object for which you are opening the configuration</param>
        /// <param name="preLoad">true to preload all section groups and sections; otherwise, false</param>
        /// <returns></returns>
        public IConfiguration OpenMappedExeConfiguration(ExeConfigurationFileMap fileMap, ConfigurationUserLevel userLevel, bool preLoad)
        {
            return new ConfigurationWrapper(ConfigurationManager.OpenMappedExeConfiguration(fileMap, userLevel, preLoad));
        }

        /// <summary>
        /// Opens the machine configuration file as a System.Configuration.Configuration object that uses the specified file mapping
        /// </summary>
        /// <param name="fileMap">An System.Configuration.ExeConfigurationFileMap object that references configuration file to use instead of the application default configuration file</param>
        /// <returns>A System.Configuration.Configuration object</returns>
        public IConfiguration OpenMappedMachineConfiguration(ConfigurationFileMap fileMap)
        {
            return new ConfigurationWrapper(ConfigurationManager.OpenMappedMachineConfiguration(fileMap));
        }

        /// <summary>
        /// Refreshes the named section so the next time that it is retrieved it will be re-read from disk
        /// </summary>
        /// <param name="sectionName">The configuration section name or the configuration path and section name of the section to refresh</param>
        public void RefreshSection(string sectionName)
        {
            ConfigurationManager.RefreshSection(sectionName);
        }

        #endregion Default Configuration Manager Methods

        #region Additional Methods

        /// <summary>
        /// Does the configuration contain a given setting
        /// </summary>
        /// <param name="settingName">The setting to find</param>
        /// <returns>A boolean denoting if the setting exists</returns>
        public bool HasSetting(string settingName)
        {
            return AppSettings.AllKeys.Contains(settingName);
        }

        /// <summary>
        /// Does the configuration contain a given setting and section
        /// </summary>
        /// <param name="settingName">The setting to find</param>
        /// <param name="section">Custom section the setting is in</param>
        /// <returns>A boolean denoting if the setting exists</returns>
        public bool HasSetting(string settingName, string section)
        {
            var namedSection = GetSection(section) as NameValueCollection;
            return namedSection != null && namedSection.AllKeys.Contains(settingName);
        }

        /// <summary>
        /// Does the configuration contain a given connection string
        /// </summary>
        /// <param name="connectionName">The connection to find</param>
        /// <returns>A boolean denoting if the connection string exists</returns>
        public bool HasConnectionString(string connectionName)
        {
            if (string.IsNullOrWhiteSpace(connectionName))
            {
                return false;
            }

            var value = ConnectionStrings.Cast<ConnectionStringSettings>().Where(c => c.Name.Equals(connectionName));
            return value.Any();
        }

        /// <summary>
        /// Get a setting from the configuration
        /// </summary>
        /// <param name="name">The setting name</param>
        /// <returns>The setting value</returns>
        public T GetSetting<T>(string name)
        {
            var appSetting = AppSettings[name];
            return ExtractValue<T>(name, appSetting);
        }

        /// <summary>
        /// Get a setting from the configuration and section
        /// </summary>
        /// <param name="name">The setting name</param>
        /// <param name="section">The custom section the setting is in</param>
        /// <returns>The setting value</returns>
        public T GetSetting<T>(string name, string section)
        {
            var namedSection = ValidateSection(section);
            var appSetting = namedSection[name];
            return ExtractValue<T>(name, appSetting);
        }

        /// <summary>
        /// Get a setting from the configuration or the default value if not found
        /// </summary>
        /// <param name="name">The setting name</param>
        /// <param name="fallBack">The default value to fallback to</param>
        /// <returns>The setting value or default</returns>
        public T GetSettingOrDefault<T>(string name, T fallBack = default(T))
        {
            var appSetting = AppSettings[name];
            return ExtractValueOrDefault(appSetting, fallBack);
        }

        /// <summary>
        /// Get a setting from the configuration and section or the default value if not found
        /// </summary>
        /// <param name="name">The setting name</param>
        /// <param name="section">The custom section the setting is in</param>
        /// <param name="fallBack">The default value to fallback to</param>
        /// <returns>The setting value or default</returns>
        public T GetSettingOrDefault<T>(string name, string section, T fallBack = default(T))
        {
            var namedSection = ValidateSection(section);
            var appSetting = namedSection[name];
            return ExtractValueOrDefault(appSetting, fallBack);
        }

        /// <summary>
        /// Validate the section
        /// </summary>
        /// <param name="section">The section name</param>
        /// <returns>The collection or throws an exception if not found</returns>
        /// <exception cref="ArgumentException">No section in the configuration</exception>
        private NameValueCollection ValidateSection(string section)
        {
            var namedSection = GetSection(section) as NameValueCollection;

            if (namedSection == null)
            {
                throw new ArgumentException($"Section {section} is not found in configuration");
            }
            return namedSection;
        }

        /// <summary>
        /// Extract the setting value or return the default
        /// </summary>
        /// <param name="value">The value to convert</param>
        /// <param name="fallback">The default value</param>
        /// <returns>The converted value or default</returns>
        private static T ExtractValueOrDefault<T>(string value, T fallback)
        {
            return string.IsNullOrWhiteSpace(value) ? fallback : ConvertValue<T>(value);
        }

        /// <summary>
        /// Extract the value or throw an exception if not found
        /// </summary>
        /// <param name="name">The setting name</param>
        /// <param name="value">The value to convert</param>
        /// <returns>The converted value or exception</returns>
        private static T ExtractValue<T>(string name, string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException($"Specified key ({name}) not found or empty.");
            }

            return ConvertValue<T>(value);
        }

        /// <summary>
        /// Convert the value
        /// </summary>
        /// <param name="value">The value to convert</param>
        /// <returns>The converted value or exception</returns>
        private static T ConvertValue<T>(string value)
        {
            var converter = TypeDescriptor.GetConverter(typeof(T));
            return (T)converter.ConvertFromInvariantString(value);
        }

        #endregion Additional Methods
    }
}