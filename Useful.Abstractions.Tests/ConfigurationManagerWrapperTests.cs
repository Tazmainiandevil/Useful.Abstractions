using FluentAssertions;
using System;
using Useful.Abstractions.Configuration;
using Xunit;

namespace Useful.Abstractions.Tests
{
    /// <summary>
    /// Integration tests for Configuration Manager Wrapper
    /// </summary>
    public class ConfigurationManagerWrapperTests
    {
        #region get setting

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void get_app_setting_with_null_or_empty_key_throws_exception(string key)
        {
            // Arrange
            var wrapper = new ConfigurationManagerWrapper();

            // Act
            Action ex = () => wrapper.GetSetting<bool>(key);

            // Assert
            ex.ShouldThrow<ArgumentException>().WithMessage($"Specified key ({key}) not found or empty.");
        }

        [Fact]
        public void get_app_setting_with_an_unknown_key_throws_exception()
        {
            // Arrange
            var wrapper = new ConfigurationManagerWrapper();

            // Act
            Action ex = () => wrapper.GetSetting<bool>("Unknown");

            // Assert
            ex.ShouldThrow<ArgumentException>().WithMessage("Specified key (Unknown) not found or empty.");
        }

        [Fact]
        public void get_appsetting_of_boolean_and_return_the_expected_boolean_value()
        {
            // Arrange
            var wrapper = new ConfigurationManagerWrapper();

            // Act
            // Assert
            wrapper.GetSetting<bool>("IsValue").Should().BeTrue();
        }

        [Fact]
        public void get_appsetting_of_int_and_return_the_expected_int_value()
        {
            // Arrange
            var wrapper = new ConfigurationManagerWrapper();

            // Act
            // Assert
            wrapper.GetSetting<int>("Timeout").Should().Be(10);
        }

        [Fact]
        public void get_appsetting_of_boolean_but_using_incorrect_type_throws_exception()
        {
            // Arrange
            var wrapper = new ConfigurationManagerWrapper();

            // Act
            Action ex = () => wrapper.GetSetting<int>("IsValue");

            // Assert
            ex.ShouldThrow<Exception>().WithMessage("true is not a valid value for Int32.");
        }

        [Fact]
        public void get_appsetting_with_unknown_custom_section_throws_exception()
        {
            // Arrange
            var wrapper = new ConfigurationManagerWrapper();

            // Act
            Action ex = () => wrapper.GetSetting<int>(string.Empty, "unknown");

            // Assert
            ex.ShouldThrow<ArgumentException>().WithMessage("Section unknown is not found in configuration");
        }

        [Fact]
        public void get_appsetting_with_custom_section_of_int_and_return_the_expected_int_value()
        {
            // Arrange
            var wrapper = new ConfigurationManagerWrapper();

            // Act
            // Assert
            wrapper.GetSetting<int>("IntValue", "customSection").Should().Be(10);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void get_app_setting_with_custom_and_with_null_or_empty_key_throws_exception(string key)
        {
            // Arrange
            var wrapper = new ConfigurationManagerWrapper();

            // Act
            Action ex = () => wrapper.GetSetting<bool>(key, "customSection");

            // Assert
            ex.ShouldThrow<ArgumentException>().WithMessage($"Specified key ({key}) not found or empty.");
        }

        #endregion get setting

        #region get setting or default

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void get_app_setting_or_default_with_null_or_empty_key_returns_default_value_for_the_type(string key)
        {
            // Arrange
            var wrapper = new ConfigurationManagerWrapper();

            // Act
            var value = wrapper.GetSettingOrDefault<bool>(key);

            // Assert
            value.Should().BeFalse();
        }

        [Fact]
        public void get_app_setting_or_default_with_an_unknown_key_returns_default_value_for_the_typ()
        {
            // Arrange
            var wrapper = new ConfigurationManagerWrapper();

            // Act
            var value = wrapper.GetSettingOrDefault<bool>("Unknown");

            // Assert
            value.Should().BeFalse();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void get_app_setting_or_default_with_custom_and_with_null_or_empty_key_returns_default_value_for_the_typ(string key)
        {
            // Arrange
            var wrapper = new ConfigurationManagerWrapper();

            // Act
            var value = wrapper.GetSettingOrDefault<bool>(key, "customSection");

            // Assert
            value.Should().BeFalse();
        }

        [Fact]
        public void get_app_setting_or_default_with_key_but_empty_value_returns_the_types_default_value()
        {
            // Arrange
            var wrapper = new ConfigurationManagerWrapper();

            // Act
            var value = wrapper.GetSettingOrDefault<int>("EmptyValue");

            // Assert
            value.Should().Be(default(int));
        }

        [Fact]
        public void get_app_setting_or_default_with_specified_default_returns_the_expected_value()
        {
            // Arrange
            var wrapper = new ConfigurationManagerWrapper();

            // Act
            var value = wrapper.GetSettingOrDefault("EmptyValue", 22);

            // Assert
            value.Should().Be(22);
        }

        [Fact]
        public void get_app_setting_or_default_with_specified_default_but_has_a_value_returns_the_value_not_the_default()
        {
            // Arrange
            var wrapper = new ConfigurationManagerWrapper();

            // Act
            var value = wrapper.GetSettingOrDefault("Size", 22);

            // Assert
            value.Should().Be(30);
        }

        #endregion get setting or default

        #region Has Setting

        [Fact]
        public void has_setting_with_a_known_key_returns_true()
        {
            // Arrange
            var wrapper = new ConfigurationManagerWrapper();

            // Act
            var result = wrapper.HasSetting("IsValue");

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void has_setting_with_an_uknown_key_returns_false()
        {
            // Arrange
            var wrapper = new ConfigurationManagerWrapper();

            // Act
            var result = wrapper.HasSetting("Unknown");

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void has_setting_in_a_custom_section_with_a_known_key_returns_true()
        {
            // Arrange
            var wrapper = new ConfigurationManagerWrapper();

            // Act
            var result = wrapper.HasSetting("IntValue", "customSection");

            // Assert
            result.Should().BeTrue();
        }

        [Fact]
        public void has_setting_in_a_custom_section_with_an_unknown_key_returns_false()
        {
            // Arrange
            var wrapper = new ConfigurationManagerWrapper();

            // Act
            var result = wrapper.HasSetting("SomeValue", "customSection");

            // Assert
            result.Should().BeFalse();
        }

        #endregion Has Setting

        #region Has Connection String

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("    ")]
        public void has_connection_string_with_null_or_empty_name_returns_false(string name)
        {
            // Arrange
            var wrapper = new ConfigurationManagerWrapper();

            // Act
            var result = wrapper.HasConnectionString(name);

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void has_connection_string_with_no_configuration_returns_false()
        {
            // Arrange
            var wrapper = new ConfigurationManagerWrapper();

            // Act
            var result = wrapper.HasConnectionString("unknown");

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void has_connection_string_with_unknown_connection_name_returns_false()
        {
            // Arrange
            var wrapper = new ConfigurationManagerWrapper();

            // Act
            var result = wrapper.HasConnectionString("someconnection");

            // Assert
            result.Should().BeFalse();
        }

        [Fact]
        public void has_connection_string_with_a_known_connection_name_returns_true()
        {
            // Arrange
            var wrapper = new ConfigurationManagerWrapper();

            // Act
            var result = wrapper.HasConnectionString("default");

            // Assert
            result.Should().BeTrue();
        }

        #endregion Has Connection String
    }
}