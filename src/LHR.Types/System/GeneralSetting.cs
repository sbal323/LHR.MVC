using LHR.Types.Constants.Entities;
using LHR.Types.ORM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LHR.Types.System
{
    /// <summary>
    /// General application settings
    /// </summary>
    public class GeneralSetting
    {
        /// <summary>
        /// Setting Identifier for future reference
        /// </summary>
        [FieldName(GeneralSettingFieldNames.Id)]
        public Guid Id { get; set; }
        /// <summary>
        /// Setting Name
        /// </summary>
        [FieldName(GeneralSettingFieldNames.Name)]
        public string Name { get; set; }
        /// <summary>
        /// Setting description
        /// </summary>
        [FieldName(GeneralSettingFieldNames.Description)]
        public string Description { get; set; }
        /// <summary>
        /// Setting Value
        /// </summary>
        [FieldName(GeneralSettingFieldNames.Value)]
        public string Value { get; set; }
        /// <summary>
        /// Setting default value (may be used in reset to defaults feature)
        /// </summary>
        [FieldName(GeneralSettingFieldNames.DefaultValue)]
        public string DefaultValue { get; set; }
        /// <summary>
        /// Is setting for custom addon
        /// </summary>
        [FieldName(GeneralSettingFieldNames.Custom)]
        public bool Custom { get; set; }
        /// <summary>
        /// Addon name
        /// </summary>
        [FieldName(GeneralSettingFieldNames.AddonName)]
        public string AddonName { get; set; }
    }
}
