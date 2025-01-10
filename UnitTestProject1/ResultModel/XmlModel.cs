using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1.ResultModel
{

    // 注意: 生成的代码可能至少需要 .NET Framework 4.5 或 .NET Core/Standard 2.0。
    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class FormView
    {

        private string nameField;

        private string metaObjectNameField;

        private ulong versionField;

        private string titleField;

        private string descriptionField;

        private byte sortField;

        private string idField;

        private string templateNameField;

        private string formCreateOperationTypeSettingField;

        private string formEditOperationTypeSettingField;

        private object linkageRuleIDField;

        private string isSetRelationObjetFileField;

        private FormViewTitleRegion titleRegionField;

        private FormViewFormPart[] formPartsField;

        /// <remarks/>
        public string Name
        {
            get
            {
                return this.nameField;
            }
            set
            {
                this.nameField = value;
            }
        }

        /// <remarks/>
        public string MetaObjectName
        {
            get
            {
                return this.metaObjectNameField;
            }
            set
            {
                this.metaObjectNameField = value;
            }
        }

        /// <remarks/>
        public ulong Version
        {
            get
            {
                return this.versionField;
            }
            set
            {
                this.versionField = value;
            }
        }

        /// <remarks/>
        public string Title
        {
            get
            {
                return this.titleField;
            }
            set
            {
                this.titleField = value;
            }
        }

        /// <remarks/>
        public string Description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        /// <remarks/>
        public byte Sort
        {
            get
            {
                return this.sortField;
            }
            set
            {
                this.sortField = value;
            }
        }

        /// <remarks/>
        public string ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        public string TemplateName
        {
            get
            {
                return this.templateNameField;
            }
            set
            {
                this.templateNameField = value;
            }
        }

        /// <remarks/>
        public string FormCreateOperationTypeSetting
        {
            get
            {
                return this.formCreateOperationTypeSettingField;
            }
            set
            {
                this.formCreateOperationTypeSettingField = value;
            }
        }

        /// <remarks/>
        public string FormEditOperationTypeSetting
        {
            get
            {
                return this.formEditOperationTypeSettingField;
            }
            set
            {
                this.formEditOperationTypeSettingField = value;
            }
        }

        /// <remarks/>
        public object LinkageRuleID
        {
            get
            {
                return this.linkageRuleIDField;
            }
            set
            {
                this.linkageRuleIDField = value;
            }
        }

        /// <remarks/>
        public string IsSetRelationObjetFile
        {
            get
            {
                return this.isSetRelationObjetFileField;
            }
            set
            {
                this.isSetRelationObjetFileField = value;
            }
        }

        /// <remarks/>
        public FormViewTitleRegion TitleRegion
        {
            get
            {
                return this.titleRegionField;
            }
            set
            {
                this.titleRegionField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("FormPart", IsNullable = false)]
        public FormViewFormPart[] FormParts
        {
            get
            {
                return this.formPartsField;
            }
            set
            {
                this.formPartsField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class FormViewTitleRegion
    {

        private string titleField;

        private string descriptionField;

        private string styleSpaceField;

        private string styleTitleField;

        private string styleTitleENField;

        private string templateNameField;

        /// <remarks/>
        public string Title
        {
            get
            {
                return this.titleField;
            }
            set
            {
                this.titleField = value;
            }
        }

        /// <remarks/>
        public string Description
        {
            get
            {
                return this.descriptionField;
            }
            set
            {
                this.descriptionField = value;
            }
        }

        /// <remarks/>
        public string StyleSpace
        {
            get
            {
                return this.styleSpaceField;
            }
            set
            {
                this.styleSpaceField = value;
            }
        }

        /// <remarks/>
        public string StyleTitle
        {
            get
            {
                return this.styleTitleField;
            }
            set
            {
                this.styleTitleField = value;
            }
        }

        /// <remarks/>
        public string StyleTitleEN
        {
            get
            {
                return this.styleTitleENField;
            }
            set
            {
                this.styleTitleENField = value;
            }
        }

        /// <remarks/>
        public string TemplateName
        {
            get
            {
                return this.templateNameField;
            }
            set
            {
                this.templateNameField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class FormViewFormPart
    {

        private string idField;

        private string templateNameField;

        private string promptMessageField;

        private string partNameField;

        private FormViewFormPartPartTitle partTitleField;

        private FormViewFormPartField[] fieldsField;

        /// <remarks/>
        public string ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        public string TemplateName
        {
            get
            {
                return this.templateNameField;
            }
            set
            {
                this.templateNameField = value;
            }
        }

        /// <remarks/>
        public string PromptMessage
        {
            get
            {
                return this.promptMessageField;
            }
            set
            {
                this.promptMessageField = value;
            }
        }

        /// <remarks/>
        public string PartName
        {
            get
            {
                return this.partNameField;
            }
            set
            {
                this.partNameField = value;
            }
        }

        /// <remarks/>
        public FormViewFormPartPartTitle PartTitle
        {
            get
            {
                return this.partTitleField;
            }
            set
            {
                this.partTitleField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlArrayItemAttribute("Field", IsNullable = false)]
        public FormViewFormPartField[] Fields
        {
            get
            {
                return this.fieldsField;
            }
            set
            {
                this.fieldsField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class FormViewFormPartPartTitle
    {

        private string labelField;

        private byte maxColumnCountField;

        private string templateNameField;

        private byte creatingField;

        private byte editingField;

        private byte showingField;

        /// <remarks/>
        public string Label
        {
            get
            {
                return this.labelField;
            }
            set
            {
                this.labelField = value;
            }
        }

        /// <remarks/>
        public byte MaxColumnCount
        {
            get
            {
                return this.maxColumnCountField;
            }
            set
            {
                this.maxColumnCountField = value;
            }
        }

        /// <remarks/>
        public string TemplateName
        {
            get
            {
                return this.templateNameField;
            }
            set
            {
                this.templateNameField = value;
            }
        }

        /// <remarks/>
        public byte Creating
        {
            get
            {
                return this.creatingField;
            }
            set
            {
                this.creatingField = value;
            }
        }

        /// <remarks/>
        public byte Editing
        {
            get
            {
                return this.editingField;
            }
            set
            {
                this.editingField = value;
            }
        }

        /// <remarks/>
        public byte Showing
        {
            get
            {
                return this.showingField;
            }
            set
            {
                this.showingField = value;
            }
        }
    }

    /// <remarks/>
    [System.SerializableAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class FormViewFormPartField
    {

        private string idField;

        private string isShowHeadPicField;

        private string showDescriptionField;

        private string formFieldTypeField;

        private string metaObjectNameField;

        private string formFieldRelationContentField;

        private ushort fixedHeightField;

        private bool fixedHeightFieldSpecified;

        /// <remarks/>
        public string ID
        {
            get
            {
                return this.idField;
            }
            set
            {
                this.idField = value;
            }
        }

        /// <remarks/>
        public string IsShowHeadPic
        {
            get
            {
                return this.isShowHeadPicField;
            }
            set
            {
                this.isShowHeadPicField = value;
            }
        }

        /// <remarks/>
        public string ShowDescription
        {
            get
            {
                return this.showDescriptionField;
            }
            set
            {
                this.showDescriptionField = value;
            }
        }

        /// <remarks/>
        public string FormFieldType
        {
            get
            {
                return this.formFieldTypeField;
            }
            set
            {
                this.formFieldTypeField = value;
            }
        }

        /// <remarks/>
        public string MetaObjectName
        {
            get
            {
                return this.metaObjectNameField;
            }
            set
            {
                this.metaObjectNameField = value;
            }
        }

        /// <remarks/>
        public string FormFieldRelationContent
        {
            get
            {
                return this.formFieldRelationContentField;
            }
            set
            {
                this.formFieldRelationContentField = value;
            }
        }

        /// <remarks/>
        public ushort FixedHeight
        {
            get
            {
                return this.fixedHeightField;
            }
            set
            {
                this.fixedHeightField = value;
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlIgnoreAttribute()]
        public bool FixedHeightSpecified
        {
            get
            {
                return this.fixedHeightFieldSpecified;
            }
            set
            {
                this.fixedHeightFieldSpecified = value;
            }
        }
    }


}
