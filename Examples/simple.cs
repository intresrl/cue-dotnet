using System;
using System.Collections.Generic;

public interface AnnotationElementDefinitionvalueFormatBase
{
    public record AsDateTimeDefinition(DateTimeDefinition value) : AnnotationElementDefinitionvalueFormatBase;
    public record AsInitialsDefinition(InitialsDefinition value) : AnnotationElementDefinitionvalueFormatBase;
    public record AsTextDefinition(TextDefinition value) : AnnotationElementDefinitionvalueFormatBase;
    public record AsFullNameDefinition(FullNameDefinition value) : AnnotationElementDefinitionvalueFormatBase;
    public record AsFirstNameDefinition(FirstNameDefinition value) : AnnotationElementDefinitionvalueFormatBase;
    public record AsLastNameDefinition(LastNameDefinition value) : AnnotationElementDefinitionvalueFormatBase;
    public record AsEmailDefinition(EmailDefinition value) : AnnotationElementDefinitionvalueFormatBase;
    public record Value(AnnotationElementDefinitionvalueFormatBase[] Branches)
    {
        public bool Valid => Branches.Length == 1;
    };
}

public interface AnnotationFieldDtoannotationConfigBase
{
    public record AsFullNameAnnotationConfigDto(FullNameAnnotationConfigDto value) : AnnotationFieldDtoannotationConfigBase;
    public record AsFirstNameAnnotationConfigDto(FirstNameAnnotationConfigDto value) : AnnotationFieldDtoannotationConfigBase;
    public record AsLastNameAnnotationConfigDto(LastNameAnnotationConfigDto value) : AnnotationFieldDtoannotationConfigBase;
    public record AsInitialsAnnotationConfigDto(InitialsAnnotationConfigDto value) : AnnotationFieldDtoannotationConfigBase;
    public record AsEmailAnnotationConfigDto(EmailAnnotationConfigDto value) : AnnotationFieldDtoannotationConfigBase;
    public record AsDateAnnotationConfigDto(DateAnnotationConfigDto value) : AnnotationFieldDtoannotationConfigBase;
    public record AsTextAnnotationConfigDto(TextAnnotationConfigDto value) : AnnotationFieldDtoannotationConfigBase;
    public record Value(AnnotationFieldDtoannotationConfigBase[] Branches)
    {
        public bool Valid => Branches.Length == 1;
    };
}

public interface BulkEnvelopeFieldTaskItemfieldBase
{
    public record AsBulkEnvelopeFieldTaskItemfield(BulkEnvelopeFieldTaskItemfield value) : BulkEnvelopeFieldTaskItemfieldBase;
    public record AsBulkEnvelopeFieldTaskItemfield(BulkEnvelopeFieldTaskItemfield value) : BulkEnvelopeFieldTaskItemfieldBase;
    public record AsCheckboxFieldDto(CheckboxFieldDto value) : BulkEnvelopeFieldTaskItemfieldBase;
    public record AsDropDownFieldDto(DropDownFieldDto value) : BulkEnvelopeFieldTaskItemfieldBase;
    public record AsBulkEnvelopeFieldTaskItemfield(BulkEnvelopeFieldTaskItemfield value) : BulkEnvelopeFieldTaskItemfieldBase;
    public record AsAttachmentFieldDto(AttachmentFieldDto value) : BulkEnvelopeFieldTaskItemfieldBase;
    public record AsAnnotationFieldDto(AnnotationFieldDto value) : BulkEnvelopeFieldTaskItemfieldBase;
    public record AsLinkFieldDto(LinkFieldDto value) : BulkEnvelopeFieldTaskItemfieldBase;
    public record AsFileReadConfirmationFieldDto(FileReadConfirmationFieldDto value) : BulkEnvelopeFieldTaskItemfieldBase;
    public record AsPageReadConfirmationFieldDto(PageReadConfirmationFieldDto value) : BulkEnvelopeFieldTaskItemfieldBase;
    public record AsAreaReadConfirmationFieldDto(AreaReadConfirmationFieldDto value) : BulkEnvelopeFieldTaskItemfieldBase;
    public record AsRadioButtonFieldDto(RadioButtonFieldDto value) : BulkEnvelopeFieldTaskItemfieldBase;
    public record AsApprovalFieldDto(ApprovalFieldDto value) : BulkEnvelopeFieldTaskItemfieldBase;
    public record AsBulkEnvelopeFieldTaskItemfield(BulkEnvelopeFieldTaskItemfield value) : BulkEnvelopeFieldTaskItemfieldBase;
    public record Value(BulkEnvelopeFieldTaskItemfieldBase[] Branches)
    {
        public bool Valid => Branches.Length == 1;
    };
}

public interface BulkEnvelopeFieldTaskItemfieldallowedSignatureTypesBase
{
    public record AsClickToSignSignature(ClickToSignSignature value) : BulkEnvelopeFieldTaskItemfieldallowedSignatureTypesBase;
    public record AsDrawToSignSignature(DrawToSignSignature value) : BulkEnvelopeFieldTaskItemfieldallowedSignatureTypesBase;
    public record AsTypeToSignSignature(TypeToSignSignature value) : BulkEnvelopeFieldTaskItemfieldallowedSignatureTypesBase;
    public record AsLocalCertificateSignature(LocalCertificateSignature value) : BulkEnvelopeFieldTaskItemfieldallowedSignatureTypesBase;
    public record AsDisposableCertificateSignature(DisposableCertificateSignature value) : BulkEnvelopeFieldTaskItemfieldallowedSignatureTypesBase;
    public record AsBiometricSignature(BiometricSignature value) : BulkEnvelopeFieldTaskItemfieldallowedSignatureTypesBase;
    public record AsRemoteCertificateSignature(RemoteCertificateSignature value) : BulkEnvelopeFieldTaskItemfieldallowedSignatureTypesBase;
    public record AsOneTimePasswordSignature(OneTimePasswordSignature value) : BulkEnvelopeFieldTaskItemfieldallowedSignatureTypesBase;
    public record AsPluginSignature(PluginSignature value) : BulkEnvelopeFieldTaskItemfieldallowedSignatureTypesBase;
    public record AsAutomaticSignature(AutomaticSignature value) : BulkEnvelopeFieldTaskItemfieldallowedSignatureTypesBase;
    public record Value(BulkEnvelopeFieldTaskItemfieldallowedSignatureTypesBase[] Branches)
    {
        public bool Valid => Branches.Length == 1;
    };
}

public interface BulkEnvelopeFieldTaskItemfieldtextInputConfigBase
{
    public record AsStringInputConfig(StringInputConfig value) : BulkEnvelopeFieldTaskItemfieldtextInputConfigBase;
    public record AsBulkEnvelopeFieldTaskItemfieldtextInputConfig(BulkEnvelopeFieldTaskItemfieldtextInputConfig value) : BulkEnvelopeFieldTaskItemfieldtextInputConfigBase;
    public record AsBulkEnvelopeFieldTaskItemfieldtextInputConfig(BulkEnvelopeFieldTaskItemfieldtextInputConfig value) : BulkEnvelopeFieldTaskItemfieldtextInputConfigBase;
    public record AsPhoneNumberInputConfig(PhoneNumberInputConfig value) : BulkEnvelopeFieldTaskItemfieldtextInputConfigBase;
    public record AsTextFieldDtotextInputConfig(TextFieldDtotextInputConfig value) : BulkEnvelopeFieldTaskItemfieldtextInputConfigBase;
    public record Value(BulkEnvelopeFieldTaskItemfieldtextInputConfigBase[] Branches)
    {
        public bool Valid => Branches.Length == 1;
    };
}

public interface EnvelopeStageItemDtorecipientsBase
{
    public record AsEnvelopeStageStandardRecipientSummaryDto(EnvelopeStageStandardRecipientSummaryDto value) : EnvelopeStageItemDtorecipientsBase;
    public record AsEnvelopeStageAutomaticRecipientResponse(EnvelopeStageAutomaticRecipientResponse value) : EnvelopeStageItemDtorecipientsBase;
    public record Value(EnvelopeStageItemDtorecipientsBase[] Branches)
    {
        public bool Valid => Branches.Length == 1;
    };
}

public interface SignatureFieldDtoallowedSignatureTypesBase
{
    public record AsClickToSignSignature(ClickToSignSignature value) : SignatureFieldDtoallowedSignatureTypesBase;
    public record AsDrawToSignSignature(DrawToSignSignature value) : SignatureFieldDtoallowedSignatureTypesBase;
    public record AsTypeToSignSignature(TypeToSignSignature value) : SignatureFieldDtoallowedSignatureTypesBase;
    public record AsLocalCertificateSignature(LocalCertificateSignature value) : SignatureFieldDtoallowedSignatureTypesBase;
    public record AsDisposableCertificateSignature(DisposableCertificateSignature value) : SignatureFieldDtoallowedSignatureTypesBase;
    public record AsBiometricSignature(BiometricSignature value) : SignatureFieldDtoallowedSignatureTypesBase;
    public record AsRemoteCertificateSignature(RemoteCertificateSignature value) : SignatureFieldDtoallowedSignatureTypesBase;
    public record AsOneTimePasswordSignature(OneTimePasswordSignature value) : SignatureFieldDtoallowedSignatureTypesBase;
    public record AsPluginSignature(PluginSignature value) : SignatureFieldDtoallowedSignatureTypesBase;
    public record AsAutomaticSignature(AutomaticSignature value) : SignatureFieldDtoallowedSignatureTypesBase;
    public record Value(SignatureFieldDtoallowedSignatureTypesBase[] Branches)
    {
        public bool Valid => Branches.Length == 1;
    };
}

public interface SignatureTaskUpdateRequestsignatureBase
{
    public record AsClickToSignSignature(ClickToSignSignature value) : SignatureTaskUpdateRequestsignatureBase;
    public record AsDrawToSignSignature(DrawToSignSignature value) : SignatureTaskUpdateRequestsignatureBase;
    public record AsSignatureTaskUpdateRequestsignature(SignatureTaskUpdateRequestsignature value) : SignatureTaskUpdateRequestsignatureBase;
    public record AsLocalCertificateSignature(LocalCertificateSignature value) : SignatureTaskUpdateRequestsignatureBase;
    public record AsDisposableCertificateSignature(DisposableCertificateSignature value) : SignatureTaskUpdateRequestsignatureBase;
    public record AsBiometricSignature(BiometricSignature value) : SignatureTaskUpdateRequestsignatureBase;
    public record AsRemoteCertificateSignature(RemoteCertificateSignature value) : SignatureTaskUpdateRequestsignatureBase;
    public record AsOneTimePasswordSignature(OneTimePasswordSignature value) : SignatureTaskUpdateRequestsignatureBase;
    public record AsPluginSignature(PluginSignature value) : SignatureTaskUpdateRequestsignatureBase;
    public record AsAutomaticSignature(AutomaticSignature value) : SignatureTaskUpdateRequestsignatureBase;
    public record Value(SignatureTaskUpdateRequestsignatureBase[] Branches)
    {
        public bool Valid => Branches.Length == 1;
    };
}

public interface TextFieldDtotextInputConfigBase
{
    public record AsStringInputConfig(StringInputConfig value) : TextFieldDtotextInputConfigBase;
    public record AsDateInputConfig(DateInputConfig value) : TextFieldDtotextInputConfigBase;
    public record AsNumberInputConfig(NumberInputConfig value) : TextFieldDtotextInputConfigBase;
    public record AsPhoneNumberInputConfig(PhoneNumberInputConfig value) : TextFieldDtotextInputConfigBase;
    public record AsTextFieldDtotextInputConfig(TextFieldDtotextInputConfig value) : TextFieldDtotextInputConfigBase;
    public record Value(TextFieldDtotextInputConfigBase[] Branches)
    {
        public bool Valid => Branches.Length == 1;
    };
}

public interface TextTaskUpdateRequesttextInputValueBase
{
    public record AsWorkUnitStringInputValue(WorkUnitStringInputValue value) : TextTaskUpdateRequesttextInputValueBase;
    public record AsWorkUnitNumberInputValue(WorkUnitNumberInputValue value) : TextTaskUpdateRequesttextInputValueBase;
    public record AsWorkUnitDateInputValue(WorkUnitDateInputValue value) : TextTaskUpdateRequesttextInputValueBase;
    public record Value(TextTaskUpdateRequesttextInputValueBase[] Branches)
    {
        public bool Valid => Branches.Length == 1;
    };
}

public interface UpdateBulkEnvelopeForIntegrationDtoexpirationBase
{
    public record AsAbsoluteIntegrationExpirationDto(AbsoluteIntegrationExpirationDto value) : UpdateBulkEnvelopeForIntegrationDtoexpirationBase;
    public record AsRelativeIntegrationExpirationDto(RelativeIntegrationExpirationDto value) : UpdateBulkEnvelopeForIntegrationDtoexpirationBase;
    public record Value(UpdateBulkEnvelopeForIntegrationDtoexpirationBase[] Branches)
    {
        public bool Valid => Branches.Length == 1;
    };
}

public interface WorkUnitFieldTaskResponsefieldBase
{
    public record AsWorkUnitSignatureFieldResponse(WorkUnitSignatureFieldResponse value) : WorkUnitFieldTaskResponsefieldBase;
    public record AsWorkUnitFieldTaskResponsefield(WorkUnitFieldTaskResponsefield value) : WorkUnitFieldTaskResponsefieldBase;
    public record AsCheckboxFieldDto(CheckboxFieldDto value) : WorkUnitFieldTaskResponsefieldBase;
    public record AsWorkUnitDropDownFieldResponse(WorkUnitDropDownFieldResponse value) : WorkUnitFieldTaskResponsefieldBase;
    public record AsWorkUnitFieldTaskResponsefield(WorkUnitFieldTaskResponsefield value) : WorkUnitFieldTaskResponsefieldBase;
    public record AsAttachmentFieldDto(AttachmentFieldDto value) : WorkUnitFieldTaskResponsefieldBase;
    public record AsWorkUnitLinkFieldResponse(WorkUnitLinkFieldResponse value) : WorkUnitFieldTaskResponsefieldBase;
    public record AsWorkUnitFileReadConfirmationFieldResponse(WorkUnitFileReadConfirmationFieldResponse value) : WorkUnitFieldTaskResponsefieldBase;
    public record AsWorkUnitPageReadConfirmationFieldResponse(WorkUnitPageReadConfirmationFieldResponse value) : WorkUnitFieldTaskResponsefieldBase;
    public record AsWorkUnitAreaReadConfirmationFieldResponse(WorkUnitAreaReadConfirmationFieldResponse value) : WorkUnitFieldTaskResponsefieldBase;
    public record AsRadioButtonFieldDto(RadioButtonFieldDto value) : WorkUnitFieldTaskResponsefieldBase;
    public record AsApprovalFieldDto(ApprovalFieldDto value) : WorkUnitFieldTaskResponsefieldBase;
    public record AsInvisibleSignatureFieldDto(InvisibleSignatureFieldDto value) : WorkUnitFieldTaskResponsefieldBase;
    public record Value(WorkUnitFieldTaskResponsefieldBase[] Branches)
    {
        public bool Valid => Branches.Length == 1;
    };
}

public interface WorkUnitFieldTaskResponsefieldtextInputConfigBase
{
    public record AsStringInputConfig(StringInputConfig value) : WorkUnitFieldTaskResponsefieldtextInputConfigBase;
    public record AsDateInputConfig(DateInputConfig value) : WorkUnitFieldTaskResponsefieldtextInputConfigBase;
    public record AsWorkUnitNumberInputConfigResponseResponse(WorkUnitNumberInputConfigResponseResponse value) : WorkUnitFieldTaskResponsefieldtextInputConfigBase;
    public record AsWorkUnitPhoneNumberInputConfigResponseResponse(WorkUnitPhoneNumberInputConfigResponseResponse value) : WorkUnitFieldTaskResponsefieldtextInputConfigBase;
    public record AsTimeInputConfig(TimeInputConfig value) : WorkUnitFieldTaskResponsefieldtextInputConfigBase;
    public record Value(WorkUnitFieldTaskResponsefieldtextInputConfigBase[] Branches)
    {
        public bool Valid => Branches.Length == 1;
    };
}

public interface WorkUnitSignatureFieldResponseallowedSignatureTypesBase
{
    public record AsClickToSignSignature(ClickToSignSignature value) : WorkUnitSignatureFieldResponseallowedSignatureTypesBase;
    public record AsDrawToSignSignature(DrawToSignSignature value) : WorkUnitSignatureFieldResponseallowedSignatureTypesBase;
    public record AsTypeToSignSignature(TypeToSignSignature value) : WorkUnitSignatureFieldResponseallowedSignatureTypesBase;
    public record AsLocalCertificateSignature(LocalCertificateSignature value) : WorkUnitSignatureFieldResponseallowedSignatureTypesBase;
    public record AsDisposableCertificateSignature(DisposableCertificateSignature value) : WorkUnitSignatureFieldResponseallowedSignatureTypesBase;
    public record AsBiometricSignature(BiometricSignature value) : WorkUnitSignatureFieldResponseallowedSignatureTypesBase;
    public record AsRemoteCertificateSignature(RemoteCertificateSignature value) : WorkUnitSignatureFieldResponseallowedSignatureTypesBase;
    public record AsOneTimePasswordSignature(OneTimePasswordSignature value) : WorkUnitSignatureFieldResponseallowedSignatureTypesBase;
    public record AsPluginSignature(PluginSignature value) : WorkUnitSignatureFieldResponseallowedSignatureTypesBase;
    public record AsAutomaticSignature(AutomaticSignature value) : WorkUnitSignatureFieldResponseallowedSignatureTypesBase;
    public record Value(WorkUnitSignatureFieldResponseallowedSignatureTypesBase[] Branches)
    {
        public bool Valid => Branches.Length == 1;
    };
}

public interface WorkUnitTextFieldResponsetextInputConfigBase
{
    public record AsStringInputConfig(StringInputConfig value) : WorkUnitTextFieldResponsetextInputConfigBase;
    public record AsDateInputConfig(DateInputConfig value) : WorkUnitTextFieldResponsetextInputConfigBase;
    public record AsWorkUnitNumberInputConfigResponseResponse(WorkUnitNumberInputConfigResponseResponse value) : WorkUnitTextFieldResponsetextInputConfigBase;
    public record AsWorkUnitPhoneNumberInputConfigResponseResponse(WorkUnitPhoneNumberInputConfigResponseResponse value) : WorkUnitTextFieldResponsetextInputConfigBase;
    public record AsTimeInputConfig(TimeInputConfig value) : WorkUnitTextFieldResponsetextInputConfigBase;
    public record Value(WorkUnitTextFieldResponsetextInputConfigBase[] Branches)
    {
        public bool Valid => Branches.Length == 1;
    };
}

public class AbsoluteIntegrationExpirationDto
{
    public string? ExpiresAt { get; init; }
    public string Mode { get; init; }
}

public class AddDefaultUserGroupDto
{
    public string UserGroupId { get; init; }
    public string DefaultType { get; init; }
}

public class AddedEnvelopeFileResponse
{
    public string Id { get; init; }
}

public class AddUserGroupUserDto
{
    public List<string> AddedUsers { get; init; }
    public List<string> SkippedUsers { get; init; }
}

public class AdminMeDto
{
    public string Email { get; init; }
    public string GivenName { get; init; }
    public string Surname { get; init; }
    public bool IsInstanceAdmin { get; init; }
    public bool IsAdminUser { get; init; }
    public List<AdminMeDtousers> Users { get; init; }
}

public class AdminMeUserDto
{
    public string UserId { get; init; }
    public string OrganizationId { get; init; }
    public string OrganizationName { get; init; }
    public bool IsEnabled { get; init; }
}

public class Agreement
{
    public string Language { get; init; }
    public string Body { get; init; }
    public string? Title { get; init; }
}

public class AgreementSettingsRequest
{
    public bool Enabled { get; init; }
    public bool Overridable { get; init; }
    public List<Agreement> Agreements { get; init; }
}

public class AnnotationElementDefinition
{
    public FileElementsPosition Position { get; init; }
    public FileElementsSize Size { get; init; }
    public FileElementTextFormat TextFormat { get; init; }
    public AnnotationElementDefinitionvalueFormatBase ValueFormat { get; init; }
}

public class AnnotationElementDto
{
    public string ElementId { get; init; }
    public AnnotationElementDefinition ElementDefinition { get; init; }
    public string Source { get; init; }
    public string? RecipientId { get; init; }
    public string? ElementName { get; init; }
}

public class AnnotationField
{
    public string Id { get; init; }
    public long Page { get; init; }
    public decimal PositionX { get; init; }
    public decimal PositionY { get; init; }
    public AnnotationElementDefinitionvalueFormatBase ValueFormat { get; init; }
    public AnnotationFieldfont? Font { get; init; }
    public string? ElementName { get; init; }
    public string FieldType { get; init; }
    public decimal Width { get; init; }
    public decimal Height { get; init; }
    public bool Required { get; init; }
    public string Source { get; init; }
}

public class AnnotationFieldDto
{
    public string Id { get; init; }
    public long Page { get; init; }
    public decimal PositionX { get; init; }
    public decimal PositionY { get; init; }
    public decimal Width { get; init; }
    public decimal Height { get; init; }
    public AnnotationFieldDtoannotationConfigBase AnnotationConfig { get; init; }
    public AnnotationFieldDtofont? Font { get; init; }
    public string? ElementName { get; init; }
    public string FieldType { get; init; }
}

public class ApprovalField
{
    public string FieldType { get; init; }
    public string Id { get; init; }
    public long Page { get; init; }
    public decimal PositionX { get; init; }
    public decimal PositionY { get; init; }
    public decimal Width { get; init; }
    public decimal Height { get; init; }
    public bool Required { get; init; }
    public string Source { get; init; }
}

public class ApprovalFieldDto
{
    public string Id { get; init; }
    public long Page { get; init; }
    public decimal PositionX { get; init; }
    public decimal PositionY { get; init; }
    public decimal Width { get; init; }
    public decimal Height { get; init; }
    public bool Required { get; init; }
    public string FieldType { get; init; }
}

public class ApproveElementDto
{
    public string ElementId { get; init; }
    public AreaReadElementDefinition ElementDefinition { get; init; }
    public string Source { get; init; }
    public string? RecipientId { get; init; }
    public bool Required { get; init; }
    public string? DisplayName { get; init; }
    public long GuidingOrder { get; init; }
}

public class AreaReadConfirmationField
{
    public string? DisplayName { get; init; }
    public string FieldType { get; init; }
    public string Id { get; init; }
    public long Page { get; init; }
    public decimal PositionX { get; init; }
    public decimal PositionY { get; init; }
    public decimal Width { get; init; }
    public decimal Height { get; init; }
    public bool Required { get; init; }
    public string Source { get; init; }
}

public class AreaReadConfirmationFieldDto
{
    public string Id { get; init; }
    public long Page { get; init; }
    public decimal PositionX { get; init; }
    public decimal PositionY { get; init; }
    public decimal Width { get; init; }
    public decimal Height { get; init; }
    public bool Required { get; init; }
    public string? DisplayName { get; init; }
    public string FieldType { get; init; }
}

public class AreaReadConfirmationTaskUpdateRequest
{
    public string FieldType { get; init; }
}

public class AreaReadElementDefinition
{
    public FileElementsPosition Position { get; init; }
    public FileElementsSize Size { get; init; }
}

public class AssociateMyNamirialIdDto
{
    public string MyNamirialId { get; init; }
}

public class ATrustCertificateDto
{
    public string? PhoneNumber { get; init; }
}

public class AttachmentElementDto
{
    public string ElementId { get; init; }
    public bool Required { get; init; }
    public AreaReadElementDefinition ElementDefinition { get; init; }
    public string Source { get; init; }
    public string? RecipientId { get; init; }
    public string Label { get; init; }
    public long GuidingOrder { get; init; }
}

public class AttachmentField
{
    public string Id { get; init; }
    public string Label { get; init; }
    public string FieldType { get; init; }
    public long Page { get; init; }
    public decimal PositionX { get; init; }
    public decimal PositionY { get; init; }
    public decimal Width { get; init; }
    public decimal Height { get; init; }
    public bool Required { get; init; }
    public string Source { get; init; }
}

public class AttachmentFieldDto
{
    public string Id { get; init; }
    public long Page { get; init; }
    public decimal PositionX { get; init; }
    public decimal PositionY { get; init; }
    public decimal Width { get; init; }
    public decimal Height { get; init; }
    public bool Required { get; init; }
    public string FieldType { get; init; }
}

public class AttachmentTaskUpdateRequest
{
    public string FileName { get; init; }
    public string Content { get; init; }
    public string FieldType { get; init; }
}

public class AuditTrailModeResponse
{
    public string AuditTrailMode { get; init; }
}

public class AutomaticESealingPermissions
{
    public bool CreateUpdateDelete { get; init; }
}

public class AutomaticSealingProfileDetailResponse
{
    public string Id { get; init; }
    public string Name { get; init; }
    public string Username { get; init; }
    public string Password { get; init; }
}

public class AutomaticSealingProfileRequest
{
    public string Name { get; init; }
    public string Username { get; init; }
    public string Password { get; init; }
}

public class AutomaticSealingProfileResponse
{
    public string Id { get; init; }
    public string Name { get; init; }
}

public class AutomaticSignature
{
    public string? LayoutId { get; init; }
    public string SignatureType { get; init; }
}

public class BackgroundImageDto
{
    public string MimeType { get; init; }
    public string DataBase64 { get; init; }
}

public class BaseField
{
    public string Id { get; init; }
    public long Page { get; init; }
    public decimal PositionX { get; init; }
    public decimal PositionY { get; init; }
    public decimal Width { get; init; }
    public decimal Height { get; init; }
    public bool Required { get; init; }
    public string Source { get; init; }
}

public class BatchAssignUserGroupUserRoleDto
{
    public List<string> UserIds { get; init; }
    public string BusinessRoleId { get; init; }
}

public class BatchDeleteUserGroupUserRoleDto
{
    public List<string> UserIds { get; init; }
}

public class BiometricSignature
{
    public string SignatureType { get; init; }
}

public class BulkEnvelopeDetailDto
{
    public string Id { get; init; }
    public string Name { get; init; }
    public List<BulkEnvelopeDetailDtostages> Stages { get; init; }
    public List<BulkEnvelopeDetailDtodocuments>? Documents { get; init; }
}

public class BulkEnvelopeDetailDtostages
{
    public string Id { get; init; }
    public long MandatoryRecipientsNumber { get; init; }
    public string? Name { get; init; }
    public string StageMode { get; init; }
    public List<BulkEnvelopeDetailDtostagesrecipients>? Recipients { get; init; }
}

public class BulkEnvelopeFieldTaskItem
{
    public BulkEnvelopeFieldTaskItemfieldBase Field { get; init; }
    public long SortOrder { get; init; }
    public string? RecipientId { get; init; }
    public string Source { get; init; }
    public string? StageId { get; init; }
}

public class BulkEnvelopeFieldTaskItemfield
{
    public string Id { get; init; }
    public long Page { get; init; }
    public decimal PositionX { get; init; }
    public decimal PositionY { get; init; }
    public decimal Width { get; init; }
    public decimal Height { get; init; }
    public List<BulkEnvelopeFieldTaskItemfieldallowedSignatureTypesBase> AllowedSignatureTypes { get; init; }
    public bool? QualifiedTimeStamp { get; init; }
    public bool Required { get; init; }
    public string FieldType { get; init; }
}

public class BulkEnvelopeFieldTaskItemfield
{
    public string Id { get; init; }
    public long Page { get; init; }
    public decimal PositionX { get; init; }
    public decimal PositionY { get; init; }
    public decimal Width { get; init; }
    public decimal Height { get; init; }
    public FontStyle Font { get; init; }
    public BulkEnvelopeFieldTaskItemfieldtextInputConfigBase TextInputConfig { get; init; }
    public bool Required { get; init; }
    public bool ReadOnly { get; init; }
    public string FieldType { get; init; }
}

public class BulkEnvelopeFieldTaskItemfieldtextInputConfig
{
    public string? Value { get; init; }
    public string? Format { get; init; }
    public string? MinValue { get; init; }
    public string? MaxValue { get; init; }
    public string TextInputType { get; init; }
}

public class BulkEnvelopeFieldTaskItemfieldtextInputConfig
{
    public decimal? Value { get; init; }
    public BulkEnvelopeFieldTaskItemfieldtextInputConfigsymbol? Symbol { get; init; }
    public string ThousandsSeparator { get; init; }
    public string DecimalSeparator { get; init; }
    public long? DecimalPlaces { get; init; }
    public decimal? MinValue { get; init; }
    public decimal? MaxValue { get; init; }
    public string TextInputType { get; init; }
}

public class BulkEnvelopeFieldTaskItemfield
{
    public List<BulkEnvelopeFieldTaskItemfieldallowedSignatureTypesBase>? AllowedSignatureTypes { get; init; }
    public string Id { get; init; }
    public bool? QualifiedTimeStamp { get; init; }
    public string FieldType { get; init; }
}

public class BulkEnvelopeFieldTaskItemfield
{
    public string Id { get; init; }
    public long Page { get; init; }
    public decimal PositionX { get; init; }
    public decimal PositionY { get; init; }
    public decimal Width { get; init; }
    public decimal Height { get; init; }
    public FontStyle Font { get; init; }
    public List<BulkEnvelopeFieldTaskItemfieldoptions>? Options { get; init; }
    public bool Multiselect { get; init; }
    public bool Required { get; init; }
    public bool ReadOnly { get; init; }
    public string FieldType { get; init; }
}

public class BulkEnvelopeFieldTaskItemRequest
{
    public BulkEnvelopeFieldTaskItemfieldBase Field { get; init; }
    public long SortOrder { get; init; }
    public string? RecipientId { get; init; }
    public string? StageId { get; init; }
}

public class BulkEnvelopeFileTasksResponse
{
    public List<BulkEnvelopeFieldTaskItem> Tasks { get; init; }
}

public class BulkEnvelopeListDto
{
    public List<BulkEnvelopeListDtobulkEnvelopes> BulkEnvelopes { get; init; }
    public Pagination Pagination { get; init; }
}

public class BulkEnvelopePartialDto
{
    public string Id { get; init; }
    public string Name { get; init; }
    public string Status { get; init; }
    public string CreatedAt { get; init; }
    public string UpdatedAt { get; init; }
}

public class BulkRecipientDefinition
{
    public string GivenName { get; init; }
    public string Surname { get; init; }
    public string Email { get; init; }
    public string? PhoneNumber { get; init; }
}

public class BulkRecipientDto
{
    public string Id { get; init; }
    public string? GivenName { get; init; }
    public string? Surname { get; init; }
    public string? Email { get; init; }
    public string? PhoneNumber { get; init; }
    public string RecipientType { get; init; }
    public string NotificationChannel { get; init; }
    public long Order { get; init; }
}

public class BulkRecipientValidationErrorResponse
{
    public List<BulkRecipientValidationErrorResponseerrors> Errors { get; init; }
}

public class BulkStageDto
{
    public string Id { get; init; }
    public long MandatoryRecipientsNumber { get; init; }
    public string? Name { get; init; }
    public string StageMode { get; init; }
    public List<BulkRecipientDto>? Recipients { get; init; }
}

public class BusinessRoleCreateDto
{
    public string Name { get; init; }
    public string? Description { get; init; }
}

public class BusinessRoleDto
{
    public string Id { get; init; }
    public string OrganizationId { get; init; }
    public string Name { get; init; }
    public string? Description { get; init; }
    public long AssignmentCount { get; init; }
    public string CreatedAt { get; init; }
    public string UpdatedAt { get; init; }
}

public class BusinessRolesListDto
{
    public List<BusinessRoleDto> Items { get; init; }
    public Pagination Pagination { get; init; }
}

public class CertificateDetailsResponse
{
    public string SubjectName { get; init; }
    public string Thumbprint { get; init; }
    public string ExpirationDate { get; init; }
    public string Issuer { get; init; }
}

public class CheckBoxElementDefinition
{
    public FileElementsPosition Position { get; init; }
    public FileElementsSize Size { get; init; }
    public string ExportValue { get; init; }
    public bool ReadOnly { get; init; }
}

public class CheckBoxElementDto
{
    public string ElementId { get; init; }
    public CheckBoxElementDefinition ElementDefinition { get; init; }
    public string Source { get; init; }
    public bool Required { get; init; }
    public bool IsChecked { get; init; }
    public string? RecipientId { get; init; }
    public long GuidingOrder { get; init; }
}

public class CheckboxField
{
    public string Id { get; init; }
    public long Page { get; init; }
    public decimal PositionX { get; init; }
    public decimal PositionY { get; init; }
    public bool ReadOnly { get; init; }
    public bool Checked { get; init; }
    public string Value { get; init; }
    public string FieldType { get; init; }
    public decimal Width { get; init; }
    public decimal Height { get; init; }
    public bool Required { get; init; }
    public string Source { get; init; }
}

public class CheckboxFieldDto
{
    public string Id { get; init; }
    public long Page { get; init; }
    public decimal PositionX { get; init; }
    public decimal PositionY { get; init; }
    public decimal Width { get; init; }
    public decimal Height { get; init; }
    public bool Checked { get; init; }
    public string Value { get; init; }
    public bool Required { get; init; }
    public bool ReadOnly { get; init; }
    public string FieldType { get; init; }
}

public class CheckboxTaskUpdateRequest
{
    public bool IsChecked { get; init; }
    public string FieldType { get; init; }
}

public class ClickToSignEnvelopeBulkSignDto
{
    public List<string> EnvelopeIds { get; init; }
    public string? IpAddress { get; init; }
    public string SignatureType { get; init; }
}

public class ClickToSignSignature
{
    public string? LayoutId { get; init; }
    public string SignatureType { get; init; }
}

public class ClickToSignStampImprintDto
{
    public bool DisplayName { get; init; }
    public bool DisplaySignatureDate { get; init; }
    public bool DisplayExtraInformation { get; init; }
    public string? FontName { get; init; }
    public long? FontSizeInPt { get; init; }
    public bool DisplayEmail { get; init; }
    public bool DisplayIp { get; init; }
}

public class ContactDto
{
    public string Id { get; init; }
    public string GivenName { get; init; }
    public string Surname { get; init; }
    public string Email { get; init; }
    public string CultureIsoCode { get; init; }
    public string? PhoneNumber { get; init; }
}

public class ContactImportResultDto
{
    public long Imported { get; init; }
}

public class ContactListDto
{
    public List<ContactDto> Contacts { get; init; }
    public Pagination Pagination { get; init; }
}

public class ContactRequest
{
    public string GivenName { get; init; }
    public string Surname { get; init; }
    public string Email { get; init; }
    public string CultureIsoCode { get; init; }
    public string? PhoneNumber { get; init; }
}

public class CountriesDto
{
    public List<CountriesDtooptions> Options { get; init; }
    public long? SelectedId { get; init; }
    public string? SelectedIsoCode { get; init; }
}

public class CountriesLookupResponse
{
    public List<CountriesLookupResponsecountries> Countries { get; init; }
}

public class CountryDto
{
    public string Name { get; init; }
    public string Code { get; init; }
}

public class CountryListItemDto
{
    public long Id { get; init; }
    public string IsoCode { get; init; }
    public string EnglishName { get; init; }
}

public class CreateBulkEnvelopeStageRequest
{
    public string Type { get; init; }
    public string? Name { get; init; }
    public long? RequiredRecipientCompletions { get; init; }
    public string Mode { get; init; }
}

public class CreatedEnvelopeFromTemplateDto
{
    public string CreatedEnvelopeId { get; init; }
}

public class CreateDocumentClassRequest
{
    public string Name { get; init; }
    public string Description { get; init; }
    public List<CreateDocumentClassRequestmetadata> Metadata { get; init; }
}

public class CreatedPersonalAccessTokenResponse
{
    public string Id { get; init; }
    public string Name { get; init; }
    public string Token { get; init; }
    public string CreatedAt { get; init; }
    public string ExpiresAt { get; init; }
}

public class CreateEnvelopeStageAutomaticRecipientRequest
{
    public string? LanguageCode { get; init; }
    public string? SignatureProfile { get; init; }
    public string? SignatureReason { get; init; }
    public bool? SignatureReasonAllowChange { get; init; }
    public List<CreateEnvelopeStageAutomaticRecipientRequestmetadata>? Metadata { get; init; }
    public string Type { get; init; }
}

public class CreateEnvelopeStageRequest
{
    public string Type { get; init; }
    public string? Name { get; init; }
    public long? RequiredRecipientCompletions { get; init; }
}

public class CreateEnvelopeStageStandardRecipientRequest
{
    public string? GivenName { get; init; }
    public string? LanguageCode { get; init; }
    public string? SignatureReason { get; init; }
    public string? Surname { get; init; }
    public string? Email { get; init; }
    public string? PhoneNumber { get; init; }
    public string? NotificationChannel { get; init; }
    public ATrustCertificateDto? Authentication { get; init; }
    public ATrustCertificateDto? SignatureConfiguration { get; init; }
    public string? PersonalMessage { get; init; }
    public bool? SignatureReasonAllowChange { get; init; }
    public List<CreateEnvelopeStageStandardRecipientRequestmetadata>? Metadata { get; init; }
    public string Type { get; init; }
}

public class CreateOAuthAuthenticationDto
{
    public string? ProviderName { get; init; }
    public string ExternalId { get; init; }
}

public class CreateOAuthFieldDefinitionRequest
{
    public string Path { get; init; }
    public string Mode { get; init; }
    public string Target { get; init; }
    public string? CustomFieldName { get; init; }
    public string? GenericSigningPluginId { get; init; }
    public string? GenericSigningPluginFieldKey { get; init; }
}

public class CreateOAuthJwtConfigRequest
{
    public string JwksUri { get; init; }
    public string Issuer { get; init; }
    public bool EnforceNonce { get; init; }
    public bool ValidateAudience { get; init; }
    public bool ValidateIssuer { get; init; }
    public bool ValidateLifetime { get; init; }
    public List<CreateOAuthFieldDefinitionRequest>? OAuthFieldDefinitions { get; init; }
}

public class CreateOAuthResourceUriRequest
{
    public string Uri { get; init; }
    public string AccessTokenParamName { get; init; }
    public string? EIdServiceCombination { get; init; }
    public List<CreateOAuthFieldDefinitionRequest>? OAuthFieldDefinitions { get; init; }
}

public class CreateOAuthSignerProviderDetailsRequest
{
    public CreateOAuthSignerProviderRequest OAuthSignerProvider { get; init; }
    public CreateOAuthJwtConfigRequest? OAuthJwtConfig { get; init; }
    public List<CreateOAuthResourceUriRequest>? OAuthResourceUris { get; init; }
}

public class CreateOAuthSignerProviderRequest
{
    public string Name { get; init; }
    public string ClientId { get; init; }
    public string ClientSecret { get; init; }
    public string AuthorizationUri { get; init; }
    public string TokenUri { get; init; }
    public string? Scope { get; init; }
    public string? LogoutUri { get; init; }
}

public class CreateOrganizationDto
{
    public string Name { get; init; }
    public string IsoCulture { get; init; }
    public LicenseDto License { get; init; }
    public string OnePlatformBusinessRelationIdentifier { get; init; }
    public List<string> FeatureFlagsNames { get; init; }
}

public class CreateOrganizationUserRequestDto
{
    public string GivenName { get; init; }
    public string Surname { get; init; }
    public string Email { get; init; }
    public CreateOrganizationUserRequestDtoregionalSettings RegionalSettings { get; init; }
    public string? PhoneNumber { get; init; }
}

public class CreateOrganizationUserRequestDtoregionalSettings
{
    public string TimeZone { get; init; }
    public string DateTimeFormat { get; init; }
    public string Language { get; init; }
    public string Country { get; init; }
}

public class CreatePersonalAccessTokenRequest
{
    public string Name { get; init; }
    public string ExpiresAt { get; init; }
}

public class CreatePolicyRequest
{
    public string Name { get; init; }
    public bool IsActive { get; init; }
    public string? Description { get; init; }
    public string? DocumentClassId { get; init; }
    public List<CreatePolicyRequestconditions>? Conditions { get; init; }
}

public class CreateRoleRequest
{
    public string Name { get; init; }
    public List<CreateRoleRequestpermissions> Permissions { get; init; }
    public string? Description { get; init; }
}

public class CreateRoleRequestpermissions
{
    public string Entity { get; init; }
    public Action Action { get; init; }
}

public class CreateServiceAccountRequest
{
    public string ClientId { get; init; }
    public string Email { get; init; }
    public UpdateRegionalSettingsDto RegionalSettings { get; init; }
}

public class CreateServiceAccountResponse
{
    public string ClientId { get; init; }
    public string ClientSecret { get; init; }
    public string UserId { get; init; }
}

public class CreateSubstituteDelegationDto
{
    public string DelegateeUserEmail { get; init; }
    public bool UtilizeAlsoOnCCRecipients { get; init; }
    public string? Reason { get; init; }
    public string? StartDate { get; init; }
    public string? EndDate { get; init; }
}

public class CreateTemplateStageStandardRecipientRequest
{
    public string? GivenName { get; init; }
    public string? LanguageCode { get; init; }
    public string? SignatureReason { get; init; }
    public string? Surname { get; init; }
    public string? Email { get; init; }
    public string? PhoneNumber { get; init; }
    public string? NotificationChannel { get; init; }
    public ATrustCertificateDto? Authentication { get; init; }
    public ATrustCertificateDto? SignatureConfiguration { get; init; }
    public string? PersonalMessage { get; init; }
    public bool? SignatureReasonAllowChange { get; init; }
    public bool IsDelegationEnabled { get; init; }
    public List<CreateTemplateStageStandardRecipientRequestmetadata>? Metadata { get; init; }
    public string Type { get; init; }
}

public class CreateUserDto
{
    public string GivenName { get; init; }
    public string Surname { get; init; }
    public string Email { get; init; }
    public string IsoLanguage { get; init; }
    public bool Enabled { get; init; }
    public List<BusinessRoleCreateDto> RoleNames { get; init; }
}

public class DateAnnotationConfigDto
{
    public string Format { get; init; }
    public string AnnotationType { get; init; }
}

public class DateInputConfig
{
    public string? Value { get; init; }
    public DateFormatSwaggerEnumProvider? Format { get; init; }
    public string? MinValue { get; init; }
    public string? MaxValue { get; init; }
    public string TextInputType { get; init; }
}

public class DateTimeDefinition
{
    public string DateTimeFormat { get; init; }
    public string ValueFormat { get; init; }
}

public class DateTimeFormatDto
{
    public string Code { get; init; }
    public string Example { get; init; }
}

public class DateTimeFormatsLookupResponse
{
    public List<DateTimeFormatDto> DateTimeFormats { get; init; }
}

public class DateTimeOptionDto
{
    public long Id { get; init; }
    public string Name { get; init; }
    public string Sample { get; init; }
}

public class DefaultUserGroupsDto
{
    public List<AutomaticSealingProfileResponse> EnvelopesShare { get; init; }
    public List<AutomaticSealingProfileResponse> TemplatesShare { get; init; }
}

public class DelegationInfo
{
    public bool Enabled { get; init; }
    public string? DefaultDelegationPolicy { get; init; }
}

public class DisposableCertificateSettingsDto
{
    public string? LraId { get; init; }
    public string? User { get; init; }
    public bool HasPassword { get; init; }
    public string? DisposableType { get; init; }
    public bool ShowDisclaimerBeforeCertificateRequest { get; init; }
    public bool SendDisposableDisclaimerDocumentNotifications { get; init; }
}

public class DisposableCertificateSignature
{
    public string? LayoutId { get; init; }
    public string SignatureType { get; init; }
}

public class DisposableCertificateStampImprintDto
{
    public bool DisplayName { get; init; }
    public bool DisplaySignatureDate { get; init; }
    public bool DisplayExtraInformation { get; init; }
    public string? FontName { get; init; }
    public long? FontSizeInPt { get; init; }
    public bool DisplayIp { get; init; }
}

public class Document
{
    public string Id { get; init; }
    public string Name { get; init; }
    public long SortOrder { get; init; }
    public string? DocumentClassId { get; init; }
}

public class DocumentClassesResponse
{
    public List<AutomaticSealingProfileResponse> DocumentClasses { get; init; }
    public Pagination Pagination { get; init; }
}

public class DocumentClassMetadataDto
{
    public string Id { get; init; }
    public string Name { get; init; }
    public string DataType { get; init; }
    public bool Required { get; init; }
    public long SortOrder { get; init; }
}

public class DocumentClassMetadataFieldDto
{
    public string Name { get; init; }
    public string DataType { get; init; }
    public bool Required { get; init; }
    public long SortOrder { get; init; }
}

public class DocumentReadConfirmationDto
{
    public string ElementId { get; init; }
    public bool Required { get; init; }
    public string? RecipientId { get; init; }
    public long GuidingOrder { get; init; }
    public string? DisplayName { get; init; }
}

public class DocumentsUploadRequest
{
    public List<string> Files { get; init; }
}

public class DrawToSignSignature
{
    public string? LayoutId { get; init; }
    public string SignatureType { get; init; }
}

public class DropDownElementDefinition
{
    public FileElementsPosition Position { get; init; }
    public FileElementsSize Size { get; init; }
    public bool ReadOnly { get; init; }
    public FileElementTextFormat TextFormat { get; init; }
}

public class DropDownElementDto
{
    public DropDownElementDefinition ElementDefinition { get; init; }
    public string Source { get; init; }
    public string ElementId { get; init; }
    public string? RecipientId { get; init; }
    public bool Required { get; init; }
    public bool? Editable { get; init; }
    public string? Value { get; init; }
    public long GuidingOrder { get; init; }
    public List<DropDownElementDtoitems>? Items { get; init; }
}

public class DropDownField
{
    public string Id { get; init; }
    public long Page { get; init; }
    public decimal PositionX { get; init; }
    public decimal PositionY { get; init; }
    public bool ReadOnly { get; init; }
    public DropDownFieldfont? Font { get; init; }
    public List<DropDownFieldoptions>? Options { get; init; }
    public bool? Editable { get; init; }
    public string? SelectedValue { get; init; }
    public string FieldType { get; init; }
    public decimal Width { get; init; }
    public decimal Height { get; init; }
    public bool Required { get; init; }
    public string Source { get; init; }
}

public class DropDownFieldDto
{
    public string Id { get; init; }
    public long Page { get; init; }
    public decimal PositionX { get; init; }
    public decimal PositionY { get; init; }
    public decimal Width { get; init; }
    public decimal Height { get; init; }
    public FontStyle Font { get; init; }
    public List<DropDownFieldDtooptions>? Options { get; init; }
    public bool Required { get; init; }
    public bool ReadOnly { get; init; }
    public string FieldType { get; init; }
}

public class DropDownItemEntry
{
    public string Value { get; init; }
    public string Label { get; init; }
    public bool? IsSelected { get; init; }
}

public class DropDownTaskUpdateRequest
{
    public string Value { get; init; }
    public string FieldType { get; init; }
}

public class EmailAnnotationConfigDto
{
    public string AnnotationType { get; init; }
}

public class EmailDefinition
{
    public string ValueFormat { get; init; }
}

public class EnableOrganizationDto
{
    public string OnePlatformBusinessRelationIdentifier { get; init; }
}

public class EnvelopeActionResponse
{
    public string EnvelopeId { get; init; }
    public long StatusCode { get; init; }
    public string? Message { get; init; }
}

public class EnvelopeActorDto
{
    public string Email { get; init; }
}

public class EnvelopeBacklogDto
{
    public string Id { get; init; }
    public string Name { get; init; }
    public string SenderName { get; init; }
    public string SentDate { get; init; }
}

public class EnvelopeBulkSignDeviceDto
{
    public string DeviceId { get; init; }
    public string OtpDeviceType { get; init; }
    public string OtpDeviceTypeId { get; init; }
    public string IdentificationInformation { get; init; }
}

public class EnvelopeBulkSignDevicesResponseDto
{
    public List<EnvelopeBulkSignDeviceDto> Devices { get; init; }
}

public class EnvelopeBulkSignDto
{
    public List<string> EnvelopeIds { get; init; }
    public string? IpAddress { get; init; }
}

public class EnvelopeBulkSignResultDto
{
    public List<string> SignedEnvelopes { get; init; }
    public List<EnvelopeBulkSignResultDtofailedEnvelopes> FailedEnvelopes { get; init; }
}

public class EnvelopeBulkSignResultDtofailedEnvelopes
{
    public string Id { get; init; }
    public string ErrorId { get; init; }
}

public class EnvelopeBulkSignTransactionDto
{
    public string TransactionId { get; init; }
    public string PayloadFileId { get; init; }
    public string? ExpiresAt { get; init; }
}

public class EnvelopeDetailDto
{
    public string Id { get; init; }
    public string Name { get; init; }
    public string Status { get; init; }
    public bool ExpiringSoon { get; init; }
    public bool SendCopyToAllRecipients { get; init; }
    public List<EnvelopeAction> Actions { get; init; }
    public string UpdatedAt { get; init; }
    public string? SentAt { get; init; }
    public string? ExpirationDate { get; init; }
    public EnvelopeAction? DefaultAction { get; init; }
    public List<Document>? Documents { get; init; }
    public List<EnvelopeDetailDtostages>? Stages { get; init; }
    public bool PreventFieldsEditingWhenFinished { get; init; }
}

public class EnvelopeDetailRecipientDto
{
    public string Id { get; init; }
    public string GivenName { get; init; }
    public string Surname { get; init; }
    public string Email { get; init; }
    public string? Placeholder { get; init; }
    public long? Order { get; init; }
    public DbRecipientType? Type { get; init; }
    public string? Status { get; init; }
    public string? StatusReason { get; init; }
    public string? LastAction { get; init; }
    public string? LastActionDate { get; init; }
    public string? ViewerLink { get; init; }
    public string? StageId { get; init; }
    public string? SignatureProfile { get; init; }
    public bool RequiresDelegationCompletion { get; init; }
}

public class EnvelopeDetailStageDto
{
    public string Id { get; init; }
    public long SortOrder { get; init; }
    public long RequiredRecipientCompletions { get; init; }
    public List<EnvelopeDetailRecipientDto> Recipients { get; init; }
    public string? Name { get; init; }
}

public class EnvelopeDownloadDto
{
    public string Id { get; init; }
    public string Name { get; init; }
    public string Type { get; init; }
}

public class EnvelopeDownloadsResponse
{
    public List<EnvelopeDownloadDto> Downloads { get; init; }
}

public class EnvelopeDto
{
    public string Id { get; init; }
    public string? Name { get; init; }
    public string? DefaultSubject { get; init; }
    public string? DefaultBody { get; init; }
    public bool SendCopyToAllRecipients { get; init; }
    public bool LateIdent { get; init; }
    public bool UseInvisibleSignatureWithTimestampForAllDocumentsAndRecipients { get; init; }
    public bool ShowOrganizationAgreements { get; init; }
    public ReminderConfigurationDto ReminderConfiguration { get; init; }
    public ATrustCertificateDto ExpirationConfiguration { get; init; }
    public List<EnvelopeDtorecipients>? Recipients { get; init; }
    public List<EnvelopeDtostages>? Stages { get; init; }
    public List<Document>? Documents { get; init; }
    public List<Agreement>? Agreements { get; init; }
    public List<string> UserGroupSharingIds { get; init; }
    public ATrustCertificateDto? CallbackConfiguration { get; init; }
    public DbEnvelopeStatus Status { get; init; }
    public string CreatedAt { get; init; }
    public string UpdatedAt { get; init; }
    public bool PreventFieldsEditingWhenFinished { get; init; }
    public string? AfterSendRedirectUrl { get; init; }
    public string? SignatureReason { get; init; }
    public bool SignatureReasonAllowChange { get; init; }
    public string SignatureFormat { get; init; }
    public bool FileRestrictedVisibility { get; init; }
}

public class EnvelopeEventDto
{
    public string Id { get; init; }
    public string Type { get; init; }
    public string OccurredAt { get; init; }
    public EnvelopeActorDto Actor { get; init; }
    public ATrustCertificateDto Data { get; init; }
}

public class EnvelopeEventsDto
{
    public List<EnvelopeEventDto> Events { get; init; }
}

public class EnvelopeFileDetailDocumentClassDto
{
    public string DocumentClassId { get; init; }
    public string? Name { get; init; }
    public List<EnvelopeFileDetailDocumentClassDtometadataValues>? MetadataValues { get; init; }
}

public class EnvelopeFileDetailDocumentClassRequest
{
    public string DocumentClassId { get; init; }
    public List<EnvelopeFileDetailDocumentClassRequestmetadataValues> MetadataValues { get; init; }
}

public class EnvelopeFileMetadataValueDto
{
    public string FieldDefinitionId { get; init; }
    public string? Name { get; init; }
    public string? Value { get; init; }
    public string Type { get; init; }
}

public class EnvelopeInsights
{
    public SingleInsight WaitingForYou { get; init; }
    public SingleInsight WaitingForOthers { get; init; }
    public SingleInsight Draft { get; init; }
    public SingleInsight Completed { get; init; }
    public SingleInsight Rejected { get; init; }
    public SingleInsight Expired { get; init; }
}

public class EnvelopeListDto
{
    public List<EnvelopeListDtoenvelopes> Envelopes { get; init; }
    public Pagination Pagination { get; init; }
}

public class EnvelopePartialDto
{
    public string Id { get; init; }
    public string Name { get; init; }
    public bool ExpiringSoon { get; init; }
    public ATrustCertificateDto SenderUser { get; init; }
    public string UpdatedAt { get; init; }
    public EnvelopeDetailStatus Status { get; init; }
    public List<EnvelopeAction> Actions { get; init; }
    public string CreatedAt { get; init; }
    public string? SentAt { get; init; }
    public List<EnvelopeDetailRecipientDto>? Recipients { get; init; }
    public EnvelopeAction? DefaultAction { get; init; }
}

public class EnvelopePermissions
{
    public bool Read { get; init; }
    public bool CreateUpdateDelete { get; init; }
}

public class EnvelopePoliciesVerifyDto
{
    public bool Compliant { get; init; }
}

public class EnvelopeSignatureTypeDto
{
    public string Id { get; init; }
    public List<string> SignatureTypes { get; init; }
    public bool CanBeSignedInBulk { get; init; }
}

public class EnvelopeSignatureTypesRequestDto
{
    public List<string> Ids { get; init; }
}

public class EnvelopeStageAutomaticRecipientResponse
{
    public string Id { get; init; }
    public string? LanguageCode { get; init; }
    public string? SignatureProfile { get; init; }
    public string? SignatureReason { get; init; }
    public bool? SignatureReasonAllowChange { get; init; }
    public List<EnvelopeStageAutomaticRecipientResponsemetadata>? Metadata { get; init; }
    public string Type { get; init; }
}

public class EnvelopeStageItemDto
{
    public string Id { get; init; }
    public string? Name { get; init; }
    public long SortOrder { get; init; }
    public long RequiredRecipientCompletions { get; init; }
    public string Type { get; init; }
    public List<EnvelopeStageItemDtorecipientsBase> Recipients { get; init; }
}

public class EnvelopeStageListDto
{
    public List<EnvelopeStageItemDto> Stages { get; init; }
}

public class EnvelopeStageStandardRecipientResponse
{
    public string? GivenName { get; init; }
    public string Id { get; init; }
    public string? LanguageCode { get; init; }
    public string? Surname { get; init; }
    public string? Email { get; init; }
    public string? PhoneNumber { get; init; }
    public string? NotificationChannel { get; init; }
    public ATrustCertificateDto? Authentication { get; init; }
    public ATrustCertificateDto? SignatureConfiguration { get; init; }
    public string? PersonalMessage { get; init; }
    public string? SignatureReason { get; init; }
    public bool? SignatureReasonAllowChange { get; init; }
    public List<EnvelopeStageStandardRecipientResponsemetadata>? Metadata { get; init; }
    public EnvelopeStageStandardRecipientResponsegeneralPoliciesOverrides? GeneralPoliciesOverrides { get; init; }
    public string Type { get; init; }
}

public class EnvelopeStageStandardRecipientSummaryDto
{
    public string GivenName { get; init; }
    public string Id { get; init; }
    public string Surname { get; init; }
    public string Email { get; init; }
    public string? PhoneNumber { get; init; }
    public string? NotificationChannel { get; init; }
    public string Type { get; init; }
}

public class EnvelopeViewerLinkDto
{
    public string ViewerLink { get; init; }
}

public class ErrorResult
{
    public ErrorCode ErrorId { get; init; }
    public string Description { get; init; }
    public ATrustCertificateDto? Errors { get; init; }
    public string? Field { get; init; }
}

public class FailedEnvelope
{
    public string Id { get; init; }
    public ErrorCode ErrorId { get; init; }
}

public class FileElementDateValidationConfiguration
{
    public ATrustCertificateDto Range { get; init; }
    public string? DateFormat { get; init; }
}

public class FileElementNumberValidationConfiguration
{
    public string SymbolPosition { get; init; }
    public ATrustCertificateDto Range { get; init; }
    public string? Symbol { get; init; }
    public string ThousandsSeparator { get; init; }
    public DecimalSeparatorType DecimalSeparator { get; init; }
    public long? Decimals { get; init; }
}

public class FileElementPhoneValidationConfiguration
{
    public string Type { get; init; }
}

public class FileElementsDto
{
    public List<FileElementsDtotextBoxElements> TextBoxElements { get; init; }
    public List<CheckBoxElementDto> CheckBoxElements { get; init; }
    public List<FileElementsDtosignatureElements> SignatureElements { get; init; }
    public List<DropDownElementDto> DropDownElements { get; init; }
    public List<FileElementsDtolistElements> ListElements { get; init; }
    public List<DocumentReadConfirmationDto> DocumentReadConfirmations { get; init; }
    public List<FileElementsDtopageReadConfirmations> PageReadConfirmations { get; init; }
    public List<ApproveElementDto> AreaReadConfirmations { get; init; }
    public List<FileElementsDtolinkElements> LinkElements { get; init; }
    public List<AttachmentElementDto> AttachmentElements { get; init; }
    public List<AnnotationElementDto> AnnotationElements { get; init; }
    public List<FileElementsDtoradioButtonElements> RadioButtonElements { get; init; }
    public List<ApproveElementDto> ApproveElements { get; init; }
    public List<FileElementsDtoinvisibleSignatureElements> InvisibleSignatureElements { get; init; }
}

public class FileElementsDtoinvisibleSignatureElements
{
    public string ElementId { get; init; }
    public string Source { get; init; }
    public string? RecipientId { get; init; }
    public bool Required { get; init; }
    public List<FileElementsDtoinvisibleSignatureElementsallowedSignatureTypesBase>? AllowedSignatureTypes { get; init; }
    public bool? QualifiedTimeStamp { get; init; }
    public long GuidingOrder { get; init; }
}

public class FileElementsDtolinkElements
{
    public AreaReadElementDefinition ElementDefinition { get; init; }
    public string Source { get; init; }
    public string ElementId { get; init; }
    public string? RecipientId { get; init; }
    public string Value { get; init; }
    public long GuidingOrder { get; init; }
}

public class FileElementsDtolistElements
{
    public DropDownElementDefinition ElementDefinition { get; init; }
    public string ElementId { get; init; }
    public List<FileElementsDtolistElementsitems> Items { get; init; }
    public bool IsRequired { get; init; }
    public bool IsEditable { get; init; }
    public bool IsMultiselect { get; init; }
    public bool IsChecked { get; init; }
    public string Source { get; init; }
    public string? RecipientId { get; init; }
    public long GuidingOrder { get; init; }
}

public class FileElementsDtoradioButtonElements
{
    public string ElementId { get; init; }
    public FileElementsDtoradioButtonElementselementDefinition ElementDefinition { get; init; }
    public string Source { get; init; }
    public string GroupName { get; init; }
    public bool IsChecked { get; init; }
    public bool IsSelectInUnison { get; init; }
    public string? RecipientId { get; init; }
    public bool Required { get; init; }
    public string Value { get; init; }
    public long GuidingOrder { get; init; }
}

public class FileElementsDtoradioButtonElementselementDefinition
{
    public FileElementsPosition Position { get; init; }
    public FileElementsSize Size { get; init; }
    public bool ReadOnly { get; init; }
}

public class FileElementsDtosignatureElements
{
    public string ElementId { get; init; }
    public ATrustCertificateDto AllowedSignatureTypes { get; init; }
    public AreaReadElementDefinition ElementDefinition { get; init; }
    public string Source { get; init; }
    public string? RecipientId { get; init; }
    public bool Required { get; init; }
    public string? DisplayName { get; init; }
    public string? ElementDescription { get; init; }
    public bool? UseExternalTimestampServer { get; init; }
    public long GuidingOrder { get; init; }
    public ATrustCertificateDto? TaskConfiguration { get; init; }
    public bool IsApprove { get; init; }
}

public class FileElementsDtotextBoxElements
{
    public string ElementId { get; init; }
    public FileElementsDtotextBoxElementselementDefinition ElementDefinition { get; init; }
    public string Source { get; init; }
    public string? RecipientId { get; init; }
    public bool Required { get; init; }
    public string Value { get; init; }
    public long GuidingOrder { get; init; }
    public FileElementsDtotextBoxElementsvalidation? Validation { get; init; }
}

public class FileElementsDtotextBoxElementselementDefinition
{
    public FileElementsPosition Position { get; init; }
    public FileElementsSize Size { get; init; }
    public FileElementTextFormat TextFormat { get; init; }
    public bool ReadOnly { get; init; }
    public bool IsMultiline { get; init; }
    public bool IsPassword { get; init; }
    public long MaxLength { get; init; }
}

public class FileElementsFieldValidation
{
    public FieldValidationType Type { get; init; }
    public FileElementDateValidationConfiguration? DateValidationConfiguration { get; init; }
    public FileElementNumberValidationConfiguration? NumberValidationConfiguration { get; init; }
    public FileElementPhoneValidationConfiguration? PhoneValidationConfiguration { get; init; }
    public FileElementDateValidationConfiguration? TimeValidationConfiguration { get; init; }
}

public class FileElementsPosition
{
    public long PageNumber { get; init; }
    public decimal X { get; init; }
    public decimal Y { get; init; }
}

public class FileElementsSize
{
    public decimal Width { get; init; }
    public decimal Height { get; init; }
}

public class FileElementTextFormat
{
    public string TextColor { get; init; }
    public decimal FontSizeInPt { get; init; }
    public string FontName { get; init; }
    public bool Bold { get; init; }
    public bool Italic { get; init; }
    public string TextAlign { get; init; }
}

public class FileOrderItem
{
    public string Id { get; init; }
    public long SortOrder { get; init; }
}

public class FileReadConfirmationField
{
    public string? DisplayName { get; init; }
    public string FieldType { get; init; }
    public string Id { get; init; }
    public long Page { get; init; }
    public decimal PositionX { get; init; }
    public decimal PositionY { get; init; }
    public decimal Width { get; init; }
    public decimal Height { get; init; }
    public bool Required { get; init; }
    public string Source { get; init; }
}

public class FileReadConfirmationFieldDto
{
    public string Id { get; init; }
    public bool Required { get; init; }
    public string? DisplayName { get; init; }
    public string FieldType { get; init; }
}

public class FileReadConfirmationTaskUpdateRequest
{
    public string FieldType { get; init; }
}

public class FirstNameAnnotationConfigDto
{
    public string AnnotationType { get; init; }
}

public class FirstNameDefinition
{
    public string ValueFormat { get; init; }
}

public class FontStyle
{
    public string TextColor { get; init; }
    public decimal FontSizeInPt { get; init; }
    public string FontName { get; init; }
    public bool Bold { get; init; }
    public bool Italic { get; init; }
    public long TextAlign { get; init; }
}

public class ForcedAuthenticationRulesRequest
{
    public ForceAuthenticationModeApi AuthenticationMode { get; init; }
    public bool ForceInputSmsAuthentication { get; init; }
    public bool AllowBiometricWithoutAuthentication { get; init; }
    public bool AllowComplexSignaturesWithoutAuthentication { get; init; }
    public string? AuthenticationProviderId { get; init; }
}

public class FullNameAnnotationConfigDto
{
    public string AnnotationType { get; init; }
}

public class FullNameDefinition
{
    public string ValueFormat { get; init; }
}

public class GeneralSettingsDto
{
    public string Name { get; init; }
    public string? ContactUrl { get; init; }
    public string? SupportUrl { get; init; }
    public bool AllowSendCC { get; init; }
    public bool PreventEmailFromBeingSent { get; init; }
    public bool CustomStampImprintEnabled { get; init; }
}

public class GenericSigningPluginDto
{
    public string PluginId { get; init; }
    public string Name { get; init; }
    public bool AllowUserSigning { get; init; }
    public bool AllowBatchUserSigning { get; init; }
    public bool AllowAutomaticSigning { get; init; }
    public List<GenericSigningPluginDtosignatureFriendlyNames>? SignatureFriendlyNames { get; init; }
    public string Category { get; init; }
}

public class GenericSigningPluginSenderSettingsDto
{
    public string PluginId { get; init; }
    public string Name { get; init; }
    public bool AllowUserSigning { get; init; }
    public bool AllowBatchUserSigning { get; init; }
    public bool AllowAutomaticSigning { get; init; }
    public List<GenericSigningPluginSenderSettingsDtosignatureFriendlyNames>? SignatureFriendlyNames { get; init; }
    public string Category { get; init; }
    public string PluginFriendlyName { get; init; }
    public string? SignatureFriendlyName { get; init; }
    public List<GenericSigningPluginSenderSettingsDtosenderDataFields>? SenderDataFields { get; init; }
    public List<string>? PredefinedSenderDataFields { get; init; }
    public List<GenericSigningPluginSenderSettingsDtoprofiles>? Profiles { get; init; }
}

public class GenericSigningPluginSettingLabelDto
{
    public string LanguageCode { get; init; }
    public string Text { get; init; }
}

public class GetOrganizationsListResponse
{
    public List<GetOrganizationsListResponseorganizations> Organizations { get; init; }
    public Pagination Pagination { get; init; }
}

public class GetUsersListResponse
{
    public List<GetUsersListResponseusers> Users { get; init; }
    public Pagination Pagination { get; init; }
}

public class HttpValidationProblemDetails
{
    public string? Type { get; init; }
    public string? Title { get; init; }
    public long? Status { get; init; }
    public string? Detail { get; init; }
    public string? Instance { get; init; }
    public ATrustCertificateDto Errors { get; init; }
}

public class InitialsAnnotationConfigDto
{
    public string AnnotationType { get; init; }
}

public class InitialsDefinition
{
    public bool UseMiddleNameInInitials { get; init; }
    public string ValueFormat { get; init; }
}

public class IntegrationBulkEnvelopeDto
{
    public string Id { get; init; }
    public string? Name { get; init; }
    public string CreatedAt { get; init; }
    public string UpdatedAt { get; init; }
    public ATrustCertificateDto ExpirationConfiguration { get; init; }
    public ExpirationMode ExpirationMode { get; init; }
    public ReminderConfigurationDto ReminderConfiguration { get; init; }
    public bool QualifiedTimeStamp { get; init; }
    public string? DefaultSubject { get; init; }
    public string? DefaultBody { get; init; }
    public string? SignatureReason { get; init; }
    public string SignatureFormat { get; init; }
    public List<FileOrderItem> Stages { get; init; }
    public List<Document> Files { get; init; }
    public List<Agreement>? Agreements { get; init; }
    public string Status { get; init; }
    public string? StatusChangeReason { get; init; }
    public string? SentAt { get; init; }
    public bool FileRestrictedVisibility { get; init; }
}

public class IntegrationTemplateDto
{
    public string Id { get; init; }
    public string? Name { get; init; }
    public string CreatedAt { get; init; }
    public string UpdatedAt { get; init; }
    public ATrustCertificateDto ExpirationConfiguration { get; init; }
    public ExpirationMode ExpirationMode { get; init; }
    public ReminderConfigurationDto ReminderConfiguration { get; init; }
    public bool QualifiedTimeStamp { get; init; }
    public string? DefaultSubject { get; init; }
    public string? DefaultBody { get; init; }
    public string? SignatureReason { get; init; }
    public string SignatureFormat { get; init; }
    public List<FileOrderItem> Stages { get; init; }
    public List<Document> Files { get; init; }
    public List<Agreement>? Agreements { get; init; }
}

public class InvisibleSignatureElementDto
{
    public string ElementId { get; init; }
    public FormFieldSource Source { get; init; }
    public string? RecipientId { get; init; }
    public bool Required { get; init; }
    public List<InvisibleSignatureElementDtoallowedSignatureTypesBase>? AllowedSignatureTypes { get; init; }
    public bool? QualifiedTimeStamp { get; init; }
    public long GuidingOrder { get; init; }
}

public class InvisibleSignatureField
{
    public List<InvisibleSignatureFieldallowedSignatureTypesBase>? AllowedSignatureTypes { get; init; }
    public string Id { get; init; }
    public long Page { get; init; }
    public decimal PositionX { get; init; }
    public decimal PositionY { get; init; }
    public bool? QualifiedTimeStamp { get; init; }
    public string FieldType { get; init; }
    public decimal Width { get; init; }
    public decimal Height { get; init; }
    public bool Required { get; init; }
    public FormFieldSource Source { get; init; }
}

public class InvisibleSignatureFieldDto
{
    public List<InvisibleSignatureFieldDtoallowedSignatureTypesBase>? AllowedSignatureTypes { get; init; }
    public string Id { get; init; }
    public bool? QualifiedTimeStamp { get; init; }
    public string FieldType { get; init; }
}

public class LanguageSettingDto
{
    public string Id { get; init; }
    public string Code { get; init; }
    public string Name { get; init; }
    public bool IsActive { get; init; }
}

public class LanguagesLookupResponse
{
    public List<CountryDto> Languages { get; init; }
}

public class LanguageStateRequest
{
    public string Code { get; init; }
    public bool IsActive { get; init; }
}

public class LastNameAnnotationConfigDto
{
    public string AnnotationType { get; init; }
}

public class LastNameDefinition
{
    public string ValueFormat { get; init; }
}

public class LicenseDto
{
    public string Type { get; init; }
    public string ExpirationDate { get; init; }
    public long UserLimit { get; init; }
    public long DocumentLimit { get; init; }
}

public class LinkElementDto
{
    public AreaReadElementDefinition ElementDefinition { get; init; }
    public FormFieldSource Source { get; init; }
    public string ElementId { get; init; }
    public string? RecipientId { get; init; }
    public string Value { get; init; }
    public long GuidingOrder { get; init; }
}

public class LinkField
{
    public string Id { get; init; }
    public long Page { get; init; }
    public decimal PositionX { get; init; }
    public decimal PositionY { get; init; }
    public string Url { get; init; }
    public string FieldType { get; init; }
    public decimal Width { get; init; }
    public decimal Height { get; init; }
    public bool Required { get; init; }
    public FormFieldSource Source { get; init; }
}

public class LinkFieldDto
{
    public string Id { get; init; }
    public long Page { get; init; }
    public decimal PositionX { get; init; }
    public decimal PositionY { get; init; }
    public decimal Width { get; init; }
    public decimal Height { get; init; }
    public string Reference { get; init; }
    public string FieldType { get; init; }
}

public class ListBoxField
{
    public string Id { get; init; }
    public long Page { get; init; }
    public decimal PositionX { get; init; }
    public decimal PositionY { get; init; }
    public bool ReadOnly { get; init; }
    public FontStyle? Font { get; init; }
    public List<ListBoxFieldoptions>? Options { get; init; }
    public bool Multiselect { get; init; }
    public string FieldType { get; init; }
    public decimal Width { get; init; }
    public decimal Height { get; init; }
    public bool Required { get; init; }
    public FormFieldSource Source { get; init; }
}

public class ListBoxFieldDto
{
    public string Id { get; init; }
    public long Page { get; init; }
    public decimal PositionX { get; init; }
    public decimal PositionY { get; init; }
    public decimal Width { get; init; }
    public decimal Height { get; init; }
    public FontStyle Font { get; init; }
    public List<ListBoxFieldDtooptions>? Options { get; init; }
    public bool Multiselect { get; init; }
    public bool Required { get; init; }
    public bool ReadOnly { get; init; }
    public string FieldType { get; init; }
}

public class ListBoxTaskUpdateRequest
{
    public List<string> SelectedItemIds { get; init; }
    public string FieldType { get; init; }
}

public class ListElementDto
{
    public DropDownElementDefinition ElementDefinition { get; init; }
    public string ElementId { get; init; }
    public List<ListElementDtoitems> Items { get; init; }
    public bool IsRequired { get; init; }
    public bool IsEditable { get; init; }
    public bool IsMultiselect { get; init; }
    public bool IsChecked { get; init; }
    public FormFieldSource Source { get; init; }
    public string? RecipientId { get; init; }
    public long GuidingOrder { get; init; }
}

public class ListItemEntry
{
    public string Key { get; init; }
    public string Value { get; init; }
    public bool IsSelected { get; init; }
}

public class LocalCertificateSignature
{
    public string? LayoutId { get; init; }
    public string SignatureType { get; init; }
}

public class MetadataValueDto
{
    public string FieldDefinitionId { get; init; }
    public string? Value { get; init; }
}

public class NamedSignatureAppearanceLayoutDto
{
    public string Id { get; init; }
    public bool DisplayFirstname { get; init; }
    public bool DisplayLastname { get; init; }
    public bool DisplayCustomText { get; init; }
    public bool DisplayDateTime { get; init; }
    public bool DisplayEmail { get; init; }
    public bool DisplayReason { get; init; }
    public BackgroundImageDto? BackgroundImage { get; init; }
    public ImagePosition Position { get; init; }
}

public class NextRecipientDto
{
    public string Id { get; init; }
    public string GivenName { get; init; }
    public string Surname { get; init; }
    public string Type { get; init; }
}

public class NextRecipientLinkDto
{
    public NextRecipientDto Recipient { get; init; }
    public string Link { get; init; }
}

public class NextRecipientLinksResponse
{
    public List<NextRecipientLinkDto> NextRecipientLinks { get; init; }
}

public class NotificationChannelMessagesDto
{
    public List<ATrustCertificateDto> Messages { get; init; }
}

public class NotificationChannelsDto
{
    public bool Email { get; init; }
    public bool Sms { get; init; }
    public bool WhatsApp { get; init; }
}

public class NotificationPreferencesRequest
{
    public bool NotifyRecipientOnActionNeeded { get; init; }
}

public class NotificationSettingsDto
{
    public EmailSenderDisplayType EmailSenderDisplayType { get; init; }
    public bool EnvelopeLimitReachedNotificationEnabled { get; init; }
    public long EnvelopesInPercentFromLimitNotification { get; init; }
    public long EnvelopesLimitReachedPercentStep { get; init; }
    public bool OrganizationCallbackEnabled { get; init; }
    public bool LicenseExpireNotificationEnabled { get; init; }
    public long LicenseExpireNotificationBeforeDays { get; init; }
    public long LicenseExpireNotificationRecurrentDays { get; init; }
    public string OrganizationCallbackUrl { get; init; }
    public long ReminderSendLimitInMinutes { get; init; }
}

public class NumberInputConfig
{
    public decimal? Value { get; init; }
    public NumberInputConfigsymbol? Symbol { get; init; }
    public string ThousandsSeparator { get; init; }
    public DecimalSeparatorType DecimalSeparator { get; init; }
    public long? DecimalPlaces { get; init; }
    public decimal? MinValue { get; init; }
    public decimal? MaxValue { get; init; }
    public string TextInputType { get; init; }
}

public class NumberSymbol
{
    public string? Value { get; init; }
    public string Position { get; init; }
}

public class OAuthAuthentication
{
    public string ProviderName { get; init; }
    public string ExternalId { get; init; }
}

public class OAuthFieldDefinitionDto
{
    public long Id { get; init; }
    public string Path { get; init; }
    public string Mode { get; init; }
    public string Target { get; init; }
    public long? OAuthResourceUriId { get; init; }
    public long? OAuthJwtConfigId { get; init; }
    public long? OAuthProviderId { get; init; }
    public string? CustomFieldName { get; init; }
    public string? GenericSigningPluginId { get; init; }
    public string? GenericSigningPluginFieldKey { get; init; }
}

public class OAuthFieldReferenceDto
{
    public string? Id { get; init; }
    public string FieldTarget { get; init; }
    public string? CustomFieldName { get; init; }
    public ATrustCertificateDto? GenericSigningPluginReference { get; init; }
}

public class OAuthJwtConfigDto
{
    public long OAuthProviderId { get; init; }
    public string JwksUri { get; init; }
    public string Issuer { get; init; }
    public bool EnforceNonce { get; init; }
    public bool ValidateAudience { get; init; }
    public bool ValidateIssuer { get; init; }
    public bool ValidateLifetime { get; init; }
    public List<OAuthFieldDefinitionDto>? OAuthFieldDefinitions { get; init; }
}

public class OAuthResourceUriDto
{
    public long Id { get; init; }
    public string Uri { get; init; }
    public string AccessTokenParamName { get; init; }
    public string? EIdServiceCombination { get; init; }
    public List<OAuthFieldDefinitionDto>? OAuthFieldDefinitions { get; init; }
}

public class OAuthSignerProvider
{
    public long Id { get; init; }
    public string ExternalId { get; init; }
    public string Name { get; init; }
    public string ClientId { get; init; }
    public string? ClientSecret { get; init; }
    public string? Scope { get; init; }
    public string AuthorizationUri { get; init; }
    public string TokenUri { get; init; }
    public string? LogoutUri { get; init; }
    public long AuthenticationType { get; init; }
    public bool? IsActive { get; init; }
    public string? RedirectUrl { get; init; }
}

public class OAuthSignerProviderDetailsResponse
{
    public OAuthSignerProvider OAuthSignerProvider { get; init; }
    public OAuthJwtConfigDto? OAuthJwtConfig { get; init; }
    public List<OAuthResourceUriDto>? OAuthResourceUris { get; init; }
}

public class OAuthSignerProviderFieldModeResponse
{
    public string Name { get; init; }
    public long Value { get; init; }
}

public class OAuthSignerProvidersResponse
{
    public List<OAuthSignerProvider> OAuthSignerProviders { get; init; }
    public Pagination Pagination { get; init; }
}

public class OneTimePasswordSignature
{
    public string? LayoutId { get; init; }
    public string SignatureType { get; init; }
}

public class OneTimePasswordStampImprintDto
{
    public bool DisplayName { get; init; }
    public bool DisplaySignatureDate { get; init; }
    public bool DisplayExtraInformation { get; init; }
    public string? FontName { get; init; }
    public long? FontSizeInPt { get; init; }
    public bool DisplayTransactionId { get; init; }
    public bool DisplayTransactionToken { get; init; }
    public bool DisplayPhoneNumber { get; init; }
    public bool DisplayIp { get; init; }
    public bool DisplayEmail { get; init; }
}

public class Option
{
    public string Value { get; init; }
    public string Label { get; init; }
    public bool IsSelected { get; init; }
}

public class OrganizationDefaultSignatureTypeDto
{
    public string SignatureType { get; init; }
}

public class OrganizationDelegationSettingsDto
{
    public DelegationPolicy DelegationPolicy { get; init; }
}

public class OrganizationDetailDto
{
    public string Id { get; init; }
    public string Name { get; init; }
    public string CreationDateUtc { get; init; }
    public bool Canceled { get; init; }
    public LicenseType LicenseType { get; init; }
    public string LicenseExpirationDate { get; init; }
    public long UserLimit { get; init; }
}

public class OrganizationFeatureFlagResponse
{
    public long Id { get; init; }
    public bool Enabled { get; init; }
    public string Name { get; init; }
}

public class OrganizationFeatureFlagsResponse
{
    public List<OrganizationFeatureFlagResponse> FeatureFlags { get; init; }
}

public class OrganizationGeneralPoliciesDto
{
    public bool AllowSaveDocument { get; init; }
    public bool AllowSaveAuditTrail { get; init; }
    public bool AllowPrintDocument { get; init; }
    public bool AllowAdhocPdfAttachments { get; init; }
    public bool AllowRejectWorkstep { get; init; }
    public bool AllowUndoLastAction { get; init; }
}

public class OrganizationItemDto
{
    public string Id { get; init; }
    public string Name { get; init; }
    public string UserId { get; init; }
}

public class OrganizationRecipientOAuthProviderDto
{
    public string Identifier { get; init; }
    public string Name { get; init; }
    public bool HasEIdAssertion { get; init; }
    public bool HasLateIdentSigTypes { get; init; }
    public bool ProvidesIdentification { get; init; }
    public long UpdateFieldComparisonValue { get; init; }
    public List<OAuthFieldReferenceDto>? UpdateFields { get; init; }
    public List<OAuthFieldReferenceDto>? ValidateFields { get; init; }
}

public class OrganizationRecipientSettingsDto
{
    public bool SendFinishedDocumentsToAllRecipients { get; init; }
    public bool ShowNotEnoughSignaturesWarning { get; init; }
    public bool DelegationAvailable { get; init; }
}

public class OrganizationSettingsPermissions
{
    public bool Read { get; init; }
    public bool Update { get; init; }
}

public class OrganizationSignatureTypesDto
{
    public List<string> AllowedSignatureTypes { get; init; }
    public List<string> AllowedDefaultSignatureTypes { get; init; }
    public List<GenericSigningPluginDto> AllowedGenericSigningPlugins { get; init; }
}

public class OrganizationSummaryDto
{
    public string Id { get; init; }
    public string Name { get; init; }
    public bool Canceled { get; init; }
}

public class OrganizationUserDto
{
    public string Id { get; init; }
    public string GivenName { get; init; }
    public string Surname { get; init; }
    public string Email { get; init; }
    public OrganizationUserRegionalSettingsDto RegionalSettings { get; init; }
    public string? PhoneNumber { get; init; }
    public bool Enabled { get; init; }
}

public class OrganizationUserRegionalSettingsDto
{
    public string TimeZone { get; init; }
    public string Language { get; init; }
    public string Country { get; init; }
    public DateTimeFormatSwaggerEnumProvider DateTimeFormat { get; init; }
}

public class PAdESSignatureConfig
{
    public bool Enabled { get; init; }
    public PAdESLevel Level { get; init; }
}

public class PageReadConfirmationDto
{
    public string ElementId { get; init; }
    public long PageNumber { get; init; }
    public bool Required { get; init; }
    public string? RecipientId { get; init; }
    public long GuidingOrder { get; init; }
    public string? DisplayName { get; init; }
}

public class PageReadConfirmationField
{
    public string? DisplayName { get; init; }
    public string FieldType { get; init; }
    public string Id { get; init; }
    public long Page { get; init; }
    public decimal PositionX { get; init; }
    public decimal PositionY { get; init; }
    public decimal Width { get; init; }
    public decimal Height { get; init; }
    public bool Required { get; init; }
    public FormFieldSource Source { get; init; }
}

public class PageReadConfirmationFieldDto
{
    public string Id { get; init; }
    public long Page { get; init; }
    public bool Required { get; init; }
    public string? DisplayName { get; init; }
    public string FieldType { get; init; }
}

public class PageReadConfirmationTaskUpdateRequest
{
    public string FieldType { get; init; }
}

public class PaginatedRoles
{
    public List<PaginatedRolesroles> Roles { get; init; }
    public Pagination Pagination { get; init; }
}

public class Pagination
{
    public long Page { get; init; }
    public long PageSize { get; init; }
    public long TotalCount { get; init; }
}

public class ParseBulkRecipientsResponse
{
    public List<BulkRecipientDefinition> BulkRecipients { get; init; }
}

public class PdfDocumentSettingsDto
{
    public ATrustCertificateDto? PAdESConfiguration { get; init; }
    public bool AllowSigningOfLockedPdfDocuments { get; init; }
    public ATrustCertificateDto? CustomTimeStampSettings { get; init; }
}

public class PermissionDto
{
    public Entity Entity { get; init; }
    public Action Action { get; init; }
}

public class PermissionsDto
{
    public EnvelopePermissions Envelopes { get; init; }
    public EnvelopePermissions Templates { get; init; }
    public EnvelopePermissions UserGroups { get; init; }
    public OrganizationSettingsPermissions OrganizationSettings { get; init; }
    public EnvelopePermissions Users { get; init; }
    public RolesSettings Roles { get; init; }
    public AutomaticESealingPermissions AutomaticESealing { get; init; }
}

public class PersonalAccessTokenListItemResponse
{
    public string Id { get; init; }
    public string Name { get; init; }
    public string CreatedAt { get; init; }
    public string ExpiresAt { get; init; }
}

public class PersonalAccessTokenListResponse
{
    public List<PersonalAccessTokenListItemResponse> PersonalAccessTokens { get; init; }
}

public class PhoneNumberInputConfig
{
    public string Value { get; init; }
    public string Format { get; init; }
    public string TextInputType { get; init; }
}

public class PluginSignature
{
    public string PluginId { get; init; }
    public string? LayoutId { get; init; }
    public string SignatureType { get; init; }
}

public class PoliciesResponse
{
    public List<PoliciesResponsepolicies> Policies { get; init; }
    public Pagination Pagination { get; init; }
}

public class PolicyActionDto
{
    public string? Id { get; init; }
    public long SortOrder { get; init; }
    public string Type { get; init; }
    public StageConfigurationDto Stage { get; init; }
    public PolicyRecipientSourceDto RecipientSource { get; init; }
}

public class PolicyConditionDto
{
    public string Id { get; init; }
    public string MetadataId { get; init; }
    public string Operator { get; init; }
    public string Value { get; init; }
    public long SortOrder { get; init; }
}

public class PolicyDto
{
    public string Id { get; init; }
    public string Name { get; init; }
    public bool IsActive { get; init; }
    public long SortOrder { get; init; }
    public string? Description { get; init; }
    public string? DocumentClassId { get; init; }
    public List<PolicyConditionDto>? Conditions { get; init; }
    public List<PolicyActionDto>? Actions { get; init; }
}

public class PolicyListItemResponse
{
    public string Id { get; init; }
    public string Name { get; init; }
    public bool IsActive { get; init; }
    public string? Description { get; init; }
}

public class PolicyRecipientSourceDto
{
    public string Type { get; init; }
    public List<BulkRecipientDefinition>? Recipients { get; init; }
    public string? UserGroupId { get; init; }
    public string? BusinessRoleId { get; init; }
}

public class RadioButtonElementDefinition
{
    public FileElementsPosition Position { get; init; }
    public FileElementsSize Size { get; init; }
    public bool ReadOnly { get; init; }
}

public class RadioButtonElementDto
{
    public string ElementId { get; init; }
    public RadioButtonElementDefinition ElementDefinition { get; init; }
    public FormFieldSource Source { get; init; }
    public string GroupName { get; init; }
    public bool IsChecked { get; init; }
    public bool IsSelectInUnison { get; init; }
    public string? RecipientId { get; init; }
    public bool Required { get; init; }
    public string Value { get; init; }
    public long GuidingOrder { get; init; }
}

public class RadioButtonField
{
    public string GroupName { get; init; }
    public string Id { get; init; }
    public bool IsSelectInUnison { get; init; }
    public long Page { get; init; }
    public decimal PositionX { get; init; }
    public decimal PositionY { get; init; }
    public bool ReadOnly { get; init; }
    public bool Checked { get; init; }
    public string Value { get; init; }
    public string FieldType { get; init; }
    public decimal Width { get; init; }
    public decimal Height { get; init; }
    public bool Required { get; init; }
    public FormFieldSource Source { get; init; }
}

public class RadioButtonFieldDto
{
    public string Id { get; init; }
    public long Page { get; init; }
    public decimal PositionX { get; init; }
    public decimal PositionY { get; init; }
    public decimal Width { get; init; }
    public decimal Height { get; init; }
    public string GroupName { get; init; }
    public bool ReadOnly { get; init; }
    public bool Checked { get; init; }
    public string Value { get; init; }
    public bool Required { get; init; }
    public string FieldType { get; init; }
}

public class RadioButtonTaskUpdateRequest
{
    public string SelectedFieldId { get; init; }
    public string FieldType { get; init; }
}

public class RecipientAuthenticationSettingItemResponse
{
    public string Name { get; init; }
    public bool IsEnabled { get; init; }
}

public class RecipientAuthenticationSettingsResponse
{
    public List<RecipientAuthenticationSettingItemResponse> Settings { get; init; }
}

public class RecipientDto
{
    public string Id { get; init; }
    public string? GivenName { get; init; }
    public string? Surname { get; init; }
    public string? Email { get; init; }
    public string? PhoneNumber { get; init; }
    public string? Placeholder { get; init; }
    public DbRecipientType? Type { get; init; }
    public bool IsP7mSigner { get; init; }
    public NotificationChannel? NotificationChannel { get; init; }
    public long Order { get; init; }
    public string? LanguageCode { get; init; }
    public ATrustCertificateDto? AuthenticationConfiguration { get; init; }
    public ATrustCertificateDto? SignatureDataConfiguration { get; init; }
    public string? StageId { get; init; }
    public string? PersonalMessage { get; init; }
    public GuidingOrderMode? GuidingOrderMode { get; init; }
    public bool IsDelegationEnabled { get; init; }
    public OrganizationGeneralPoliciesDto? GeneralPoliciesOverrides { get; init; }
    public string? SignatureReason { get; init; }
    public bool? SignatureReasonAllowChange { get; init; }
    public string? SignatureProfile { get; init; }
    public List<RecipientDtometadata>? Metadata { get; init; }
    public DbWorkstepResult? WorkstepResult { get; init; }
}

public class RecipientMetadataEntry
{
    public string Name { get; init; }
    public string Value { get; init; }
}

public class RegionalSettingsDto
{
    public string Id { get; init; }
    public string WorldTimeZone { get; init; }
    public long DateTimeFormatId { get; init; }
    public string UiLanguage { get; init; }
    public long CountryId { get; init; }
}

public class RelativeIntegrationExpirationDto
{
    public long? AfterSendInSeconds { get; init; }
    public string Mode { get; init; }
}

public class ReminderConfigurationDto
{
    public bool Enabled { get; init; }
    public long FirstReminderInDays { get; init; }
    public long ReminderResendIntervalInDays { get; init; }
    public long BeforeExpirationInDays { get; init; }
}

public class RemoteCertificateEnvelopeBulkSignDto
{
    public List<string> EnvelopeIds { get; init; }
    public string? IpAddress { get; init; }
    public string SignatureType { get; init; }
    public string CertificateUserId { get; init; }
    public string DevicePassword { get; init; }
    public string Otp { get; init; }
    public string? OtpDeviceType { get; init; }
    public string? OtpDeviceTypeId { get; init; }
    public string? TransactionId { get; init; }
    public string? PayloadFileId { get; init; }
}

public class RemoteCertificateSignature
{
    public string? LayoutId { get; init; }
    public string SignatureType { get; init; }
}

public class ReplacedEnvelopeFileResponse
{
    public string Id { get; init; }
    public long OrderIndex { get; init; }
}

public class RequestBulkSignDevicesDto
{
    public string UserId { get; init; }
    public List<string> EnvelopeIds { get; init; }
}

public class ResumeBatchRequest
{
    public ATrustCertificateDto Data { get; init; }
    public List<string> EnvelopeIds { get; init; }
}

public class RoleDetailsDto
{
    public string Id { get; init; }
    public string Name { get; init; }
    public List<PermissionDto> Permissions { get; init; }
    public string CreatedAt { get; init; }
    public string? Description { get; init; }
    public bool IsSystemRole { get; init; }
}

public class RoleDto
{
    public string Id { get; init; }
    public string Name { get; init; }
    public bool IsSystemRole { get; init; }
}

public class RolesSettings
{
    public bool Read { get; init; }
    public bool CreateUpdateDelete { get; init; }
    public bool Assign { get; init; }
}

public class RotateServiceAccountSecretResponse
{
    public string ClientSecret { get; init; }
}

public class RowError
{
    public long Row { get; init; }
    public string Field { get; init; }
    public string Message { get; init; }
}

public class SealingCertificateResponse
{
    public long Id { get; init; }
    public string ExternalId { get; init; }
    public bool IsActive { get; init; }
    public CertificateDetailsResponse SealingCertificate { get; init; }
    public List<CertificateDetailsResponse> CertificateChain { get; init; }
}

public class SenderAutomaticProfileDto
{
    public string ProfileId { get; init; }
    public string? ProfileFriendlyName { get; init; }
}

public class SenderDataFieldSettingDto
{
    public bool Required { get; init; }
    public List<GenericSigningPluginSettingLabelDto> TranslatedLabels { get; init; }
    public string? DefaultValue { get; init; }
    public string? Key { get; init; }
    public DataFieldType Type { get; init; }
    public ATrustCertificateDto? Items { get; init; }
}

public class ServiceAccountListItemResponse
{
    public string ClientId { get; init; }
    public string Email { get; init; }
    public string UserId { get; init; }
}

public class ServiceAccountListResponse
{
    public List<ServiceAccountListItemResponse> Items { get; init; }
}

public class SettingsDto
{
    public long MaxEnvelopeValidityInDays { get; init; }
    public long MinEnvelopeValidityInSeconds { get; init; }
    public long FilterExpiringSoonDays { get; init; }
    public NotificationSettingsDto NotificationSettings { get; init; }
}

public class SharingOptionsResponse
{
    public List<string> UserGroupIds { get; init; }
}

public class SignatureAppearanceLayoutDto
{
    public bool DisplayFirstname { get; init; }
    public bool DisplayLastname { get; init; }
    public bool DisplayCustomText { get; init; }
    public bool DisplayDateTime { get; init; }
    public bool DisplayEmail { get; init; }
    public bool DisplayReason { get; init; }
    public BackgroundImageDto? BackgroundImage { get; init; }
    public ImagePosition Position { get; init; }
}

public class SignatureElementDto
{
    public string ElementId { get; init; }
    public ATrustCertificateDto AllowedSignatureTypes { get; init; }
    public AreaReadElementDefinition ElementDefinition { get; init; }
    public FormFieldSource Source { get; init; }
    public string? RecipientId { get; init; }
    public bool Required { get; init; }
    public string? DisplayName { get; init; }
    public string? ElementDescription { get; init; }
    public bool? UseExternalTimestampServer { get; init; }
    public long GuidingOrder { get; init; }
    public ATrustCertificateDto? TaskConfiguration { get; init; }
    public bool IsApprove { get; init; }
}

public class SignatureField
{
    public ATrustCertificateDto? AllowedSignatureTypes { get; init; }
    public string? DisplayName { get; init; }
    public string? ElementDescription { get; init; }
    public string Id { get; init; }
    public long Page { get; init; }
    public decimal PositionX { get; init; }
    public decimal PositionY { get; init; }
    public bool? UseExternalTimestampServer { get; init; }
    public ATrustCertificateDto? TaskConfiguration { get; init; }
    public string FieldType { get; init; }
    public decimal Width { get; init; }
    public decimal Height { get; init; }
    public bool Required { get; init; }
    public FormFieldSource Source { get; init; }
}

public class SignatureFieldDto
{
    public string Id { get; init; }
    public long Page { get; init; }
    public decimal PositionX { get; init; }
    public decimal PositionY { get; init; }
    public decimal Width { get; init; }
    public decimal Height { get; init; }
    public List<SignatureFieldDtoallowedSignatureTypesBase> AllowedSignatureTypes { get; init; }
    public bool? QualifiedTimeStamp { get; init; }
    public bool Required { get; init; }
    public string FieldType { get; init; }
}

public class SignatureImage
{
    public string Id { get; init; }
    public string Name { get; init; }
    public string DataUrlPrefix { get; init; }
    public string Data { get; init; }
}

public class SignaturePluginSignatureTypeDto
{
    public string PluginId { get; init; }
    public bool? Preferred { get; init; }
    public string? LayoutId { get; init; }
    public ClickToSignStampImprintDto? StampImprintConfiguration { get; init; }
}

public class SignatureTaskUpdateRequest
{
    public SignatureTaskUpdateRequestsignatureBase Signature { get; init; }
    public string FieldType { get; init; }
}

public class SignatureTaskUpdateRequestsignature
{
    public string? Text { get; init; }
    public string? TextFontFamily { get; init; }
    public string? TextFontColor { get; init; }
    public decimal? TextFontSizeFraction { get; init; }
    public SignatureTaskUpdateRequestsignatureposition? Position { get; init; }
    public string? LayoutId { get; init; }
    public string SignatureType { get; init; }
}

public class SignerAgreements
{
    public bool IsEnvelopeOverrideEnabled { get; init; }
}

public class SingleInsight
{
    public long EnvelopeCount { get; init; }
}

public class StageConfigurationDto
{
    public string Name { get; init; }
    public string Type { get; init; }
    public long RequiredRecipientCompletions { get; init; }
}

public class StageDto
{
    public string Id { get; init; }
    public long MandatoryRecipientsNumber { get; init; }
    public string? Name { get; init; }
}

public class StampImprintConfigurationDto
{
    public SignatureAppearanceLayoutDto DefaultLayout { get; init; }
    public List<NamedSignatureAppearanceLayoutDto> CustomSignatures { get; init; }
}

public class StartBulkSignTransactionDto
{
    public string UserId { get; init; }
    public string DeviceId { get; init; }
    public string OtpDeviceType { get; init; }
    public string OtpDeviceTypeId { get; init; }
    public List<string> EnvelopeIds { get; init; }
}

public class StringInputConfig
{
    public string Value { get; init; }
    public bool Password { get; init; }
    public bool Multiline { get; init; }
    public long MaxLength { get; init; }
    public string TextInputType { get; init; }
}

public class SubstituteDelegationDto
{
    public bool UtilizeAlsoOnCCRecipients { get; init; }
    public string DelegateeFirstName { get; init; }
    public string DelegateeLastName { get; init; }
    public string DelegateeEmail { get; init; }
    public string? Reason { get; init; }
    public string? StartDate { get; init; }
    public string? EndDate { get; init; }
    public string? DelegateeUserId { get; init; }
}

public class SupportedElectronicIdentitiesResponse
{
    public List<SupportedElectronicIdentitiesResponseelectronicIdentities> ElectronicIdentities { get; init; }
}

public class SupportedElectronicIdentityResponse
{
    public string Type { get; init; }
    public string Country { get; init; }
}

public class SupportedFileFormatResponse
{
    public string Extension { get; init; }
    public string MimeType { get; init; }
}

public class SwedishBankIdStampImprintDto
{
    public bool DisplayName { get; init; }
    public bool DisplaySignatureDate { get; init; }
    public bool DisplayExtraInformation { get; init; }
    public string? FontName { get; init; }
    public long? FontSizeInPt { get; init; }
    public bool DisplayTransactionId { get; init; }
}

public class TemplateDto
{
    public string Id { get; init; }
    public string CreatorUserId { get; init; }
    public string Name { get; init; }
    public List<TemplateAction> Actions { get; init; }
    public string CreatedAt { get; init; }
    public string UpdatedAt { get; init; }
    public TemplateAction? DefaultAction { get; init; }
}

public class TemplateListDto
{
    public List<TemplateDto> Templates { get; init; }
    public Pagination Pagination { get; init; }
}

public class TemplateStageStandardRecipientResponse
{
    public string? GivenName { get; init; }
    public string Id { get; init; }
    public string? LanguageCode { get; init; }
    public string? Surname { get; init; }
    public string? Email { get; init; }
    public string? PhoneNumber { get; init; }
    public NotificationChannel? NotificationChannel { get; init; }
    public ATrustCertificateDto? Authentication { get; init; }
    public ATrustCertificateDto? SignatureConfiguration { get; init; }
    public string? PersonalMessage { get; init; }
    public string? SignatureReason { get; init; }
    public bool? SignatureReasonAllowChange { get; init; }
    public bool IsDelegationEnabled { get; init; }
    public List<RecipientMetadataEntry>? Metadata { get; init; }
    public string Type { get; init; }
}

public class TemplateStageStandardRecipientSummaryDto
{
    public string GivenName { get; init; }
    public string Id { get; init; }
    public string Surname { get; init; }
    public string Email { get; init; }
    public string? PhoneNumber { get; init; }
    public string? NotificationChannel { get; init; }
    public bool IsDelegationEnabled { get; init; }
    public string Type { get; init; }
}

public class TemplateThumbnailDto
{
    public string TemplateId { get; init; }
    public string Name { get; init; }
    public string FileData { get; init; }
}

public class TextAnnotationConfigDto
{
    public string? Value { get; init; }
    public string AnnotationType { get; init; }
}

public class TextBoxElementDefinition
{
    public FileElementsPosition Position { get; init; }
    public FileElementsSize Size { get; init; }
    public FileElementTextFormat TextFormat { get; init; }
    public bool ReadOnly { get; init; }
    public bool IsMultiline { get; init; }
    public bool IsPassword { get; init; }
    public long MaxLength { get; init; }
}

public class TextBoxElementDto
{
    public string ElementId { get; init; }
    public TextBoxElementDefinition ElementDefinition { get; init; }
    public FormFieldSource Source { get; init; }
    public string? RecipientId { get; init; }
    public bool Required { get; init; }
    public string Value { get; init; }
    public long GuidingOrder { get; init; }
    public FileElementsFieldValidation? Validation { get; init; }
}

public class TextDefinition
{
    public string DefaultValue { get; init; }
    public string ValueFormat { get; init; }
}

public class TextFieldDto
{
    public string Id { get; init; }
    public long Page { get; init; }
    public decimal PositionX { get; init; }
    public decimal PositionY { get; init; }
    public decimal Width { get; init; }
    public decimal Height { get; init; }
    public FontStyle Font { get; init; }
    public TextFieldDtotextInputConfigBase TextInputConfig { get; init; }
    public bool Required { get; init; }
    public bool ReadOnly { get; init; }
    public string FieldType { get; init; }
}

public class TextFieldDtotextInputConfig
{
    public string? Value { get; init; }
    public string? Format { get; init; }
    public string? MinValue { get; init; }
    public string? MaxValue { get; init; }
    public string TextInputType { get; init; }
}

public class TextInputField
{
    public string Id { get; init; }
    public long Page { get; init; }
    public decimal PositionX { get; init; }
    public decimal PositionY { get; init; }
    public bool ReadOnly { get; init; }
    public FontStyle? Font { get; init; }
    public string Text { get; init; }
    public bool Password { get; init; }
    public bool Multiline { get; init; }
    public long MaxLength { get; init; }
    public FileElementsFieldValidation? Validation { get; init; }
    public string FieldType { get; init; }
    public decimal Width { get; init; }
    public decimal Height { get; init; }
    public bool Required { get; init; }
    public FormFieldSource Source { get; init; }
}

public class TextTaskUpdateRequest
{
    public TextTaskUpdateRequesttextInputValueBase TextInputValue { get; init; }
    public string FieldType { get; init; }
}

public class TimeInputConfig
{
    public string? Value { get; init; }
    public TimeFormatSwaggerEnumProvider? Format { get; init; }
    public string? MinValue { get; init; }
    public string? MaxValue { get; init; }
    public string TextInputType { get; init; }
}

public class TimestampSettingsDto
{
    public string Url { get; init; }
    public string Username { get; init; }
    public string Password { get; init; }
    public TimestampHashAlgorithm HashAlgorithm { get; init; }
}

public class TimeZoneListItemDto
{
    public string TimeZone { get; init; }
    public string Code { get; init; }
    public string UtcOffset { get; init; }
}

public class TimeZonesLookupResponse
{
    public List<CountryDto> TimeZones { get; init; }
}

public class TypeToSignSignature
{
    public string? LayoutId { get; init; }
    public string SignatureType { get; init; }
}

public class UpdateAccessCodeDto
{
    public string Code { get; init; }
}

public class UpdateBankIdSettingsDto
{
    public string AuthenticationCertificateThumbprint { get; init; }
}

public class UpdateBasicSettingsDto
{
    public string GivenName { get; init; }
    public string Surname { get; init; }
    public string PhoneNumber { get; init; }
}

public class UpdateBulkEnvelopeDto
{
    public ATrustCertificateDto ExpirationConfiguration { get; init; }
    public ATrustCertificateDto ReminderConfiguration { get; init; }
    public string? Name { get; init; }
    public List<UpdateBulkEnvelopeDtorecipients>? Recipients { get; init; }
    public List<BulkStageDto>? Stages { get; init; }
    public bool? SendCopyToAllRecipients { get; init; }
    public bool? LateIdent { get; init; }
    public bool? UseInvisibleSignatureWithTimestampForAllDocumentsAndRecipients { get; init; }
    public string? DefaultSubject { get; init; }
    public string? DefaultBody { get; init; }
    public List<string>? DocumentsIds { get; init; }
    public List<Agreement>? Agreements { get; init; }
    public List<string>? UserGroupSharingIds { get; init; }
    public EnvelopeType EnvelopeType { get; init; }
    public ATrustCertificateDto? CallbackConfiguration { get; init; }
    public bool? PreventFieldsEditingWhenFinished { get; init; }
    public string? AfterSendRedirectUrl { get; init; }
    public string? SignatureReason { get; init; }
    public bool? AllowChangeSignatureReason { get; init; }
    public SignatureFormat? SignatureFormat { get; init; }
    public bool? FileRestrictedVisibility { get; init; }
}

public class UpdateBulkEnvelopeForIntegrationDto
{
    public string Name { get; init; }
    public UpdateForIntegrationReminderDto Reminder { get; init; }
    public UpdateBulkEnvelopeForIntegrationDtoexpirationBase Expiration { get; init; }
    public bool? QualifiedTimeStamp { get; init; }
    public string? SignatureReason { get; init; }
    public SignatureFormat? SignatureFormat { get; init; }
    public List<NotificationChannelMessagesDto>? NotificationMessages { get; init; }
    public List<Agreement>? Agreements { get; init; }
    public bool? FileRestrictedVisibility { get; init; }
}

public class UpdateBulkFileTasksRequest
{
    public List<BulkEnvelopeFieldTaskItemRequest> FieldTasks { get; init; }
}

public class UpdatedBasicSettingsDto
{
    public string Id { get; init; }
    public string GivenName { get; init; }
    public string Surname { get; init; }
    public string? PhoneNumber { get; init; }
}

public class UpdateDisposableCertificateSettingsDto
{
    public string LraId { get; init; }
    public string User { get; init; }
    public string? Password { get; init; }
    public DisposableType DisposableType { get; init; }
    public bool ShowDisclaimerBeforeCertificateRequest { get; init; }
    public bool SendDisposableDisclaimerDocumentNotifications { get; init; }
}

public class UpdateEnvelopeRecipientDto
{
    public string Id { get; init; }
    public string? GivenName { get; init; }
    public string? Surname { get; init; }
    public string? Email { get; init; }
    public string? PhoneNumber { get; init; }
    public string? Placeholder { get; init; }
    public DbRecipientType? Type { get; init; }
    public NotificationChannel? NotificationChannel { get; init; }
    public long? Order { get; init; }
    public string? LanguageCode { get; init; }
    public ATrustCertificateDto? AuthenticationConfiguration { get; init; }
    public ATrustCertificateDto? SignatureDataConfiguration { get; init; }
    public string? StageId { get; init; }
    public string? PersonalMessage { get; init; }
    public GuidingOrderMode? GuidingOrderMode { get; init; }
    public bool IsDelegationEnabled { get; init; }
    public OrganizationGeneralPoliciesDto? GeneralPoliciesOverrides { get; init; }
    public string? SignatureReason { get; init; }
    public bool? SignatureReasonAllowChange { get; init; }
    public List<RecipientMetadataEntry>? Metadata { get; init; }
    public string? SyncId { get; init; }
}

public class UpdateForIntegrationReminderDto
{
    public bool Enabled { get; init; }
    public long FirstReminderInDays { get; init; }
    public long ResendIntervalInDays { get; init; }
    public long BeforeExpirationInDays { get; init; }
}

public class UpdateOAuthSignerProviderDetailsRequest
{
    public UpdateOAuthSignerProviderRequest OAuthSignerProvider { get; init; }
    public OAuthJwtConfigDto? OAuthJwtConfig { get; init; }
    public List<CreateOAuthResourceUriRequest>? OAuthResourceUris { get; init; }
}

public class UpdateOAuthSignerProviderRequest
{
    public string ExternalId { get; init; }
    public string Name { get; init; }
    public string ClientId { get; init; }
    public string AuthorizationUri { get; init; }
    public string TokenUri { get; init; }
    public long AuthenticationType { get; init; }
    public string? ClientSecret { get; init; }
    public string? Scope { get; init; }
    public string? LogoutUri { get; init; }
}

public class UpdateOrganizationFeatureFlag
{
    public long Id { get; init; }
    public bool Enabled { get; init; }
}

public class UpdateOrganizationRecipientSettingsRequest
{
    public bool SendFinishedDocumentsToAllRecipients { get; init; }
    public bool ShowNotEnoughSignaturesWarning { get; init; }
}

public class UpdateOrganizationUserDto
{
    public string GivenName { get; init; }
    public string Surname { get; init; }
    public OrganizationUserRegionalSettingsDto UserRegionalSettings { get; init; }
    public string? PhoneNumber { get; init; }
}

public class UpdateOrganizationUserRolesDto
{
    public List<string> Roles { get; init; }
}

public class UpdatePolicyRequest
{
    public string Name { get; init; }
    public bool IsActive { get; init; }
    public long SortOrder { get; init; }
    public string? Description { get; init; }
    public string? DocumentClassId { get; init; }
    public List<PolicyConditionDto>? Conditions { get; init; }
    public List<PolicyActionDto>? Actions { get; init; }
}

public class UpdateRegionalSettingsDto
{
    public string WorldTimeZone { get; init; }
    public long DateTimeFormatId { get; init; }
    public string UiLanguage { get; init; }
    public long CountryId { get; init; }
}

public class UserAndOrganizationDto
{
    public string Id { get; init; }
    public string GivenName { get; init; }
    public string Surname { get; init; }
    public string Email { get; init; }
    public string OrganizationId { get; init; }
    public string OrganizationName { get; init; }
    public string? PhoneNumber { get; init; }
}

public class UserApplicationContextDto
{
    public OrganizationSignatureTypesDto SignatureTypes { get; init; }
    public OrganizationDefaultSignatureTypeDto DefaultSignatureType { get; init; }
    public List<SignatureOptions> SignatureOptions { get; init; }
    public List<DbRecipientType> RecipientTypes { get; init; }
    public ATrustCertificateDto RecipientAuthenticationTypes { get; init; }
    public SignerAgreements SignerAgreements { get; init; }
    public OrganizationGeneralPoliciesDto GeneralPolicies { get; init; }
    public NotificationChannelsDto NotificationChannels { get; init; }
    public PermissionsDto UserPermissions { get; init; }
    public UserGroupPermissionsSetDto UserGroupPermissions { get; init; }
    public DelegationInfo DelegationInfo { get; init; }
    public bool OAuthAvailable { get; init; }
    public bool AutomaticRemoteSignatureAvailable { get; init; }
    public bool DocumentClassesEnabled { get; init; }
    public bool EnvelopeEventServiceEnabled { get; init; }
    public List<string> FontFamilies { get; init; }
    public bool BulkEnvelopeEnabled { get; init; }
}

public class UserGroupContactDto
{
    public string Id { get; init; }
    public string UserGroupId { get; init; }
    public string? Details { get; init; }
    public string? GivenName { get; init; }
    public string? Surname { get; init; }
    public string? Email { get; init; }
    public string? PhoneNumber { get; init; }
    public string? CultureIsoCode { get; init; }
}

public class UserGroupContactFieldDto
{
    public string Id { get; init; }
    public string UserGroupId { get; init; }
    public string Name { get; init; }
}

public class UserGroupContactFieldListDto
{
    public List<UserGroupContactFieldDto> UserGroupContactFields { get; init; }
}

public class UserGroupContactsListDto
{
    public List<UserGroupContactDto> UserGroupContacts { get; init; }
    public Pagination Pagination { get; init; }
}

public class UserGroupContactsPermissionDto
{
    public bool Read { get; init; }
    public bool CreateUpdateDelete { get; init; }
    public bool Customize { get; init; }
}

public class UserGroupCustomFieldUpdateData
{
    public string UserGroupId { get; init; }
    public string Name { get; init; }
    public string? Id { get; init; }
}

public class UserGroupCustomFieldUpdateRequest
{
    public List<UserGroupCustomFieldUpdateData> UpdatedCustomFields { get; init; }
}

public class UserGroupDto
{
    public string Id { get; init; }
    public string OrganizationId { get; init; }
    public string Name { get; init; }
}

public class UserGroupEnvelopesPermissionDto
{
    public bool Share { get; init; }
    public bool Manage { get; init; }
}

public class UserGroupPermissionDataDto
{
    public string Name { get; init; }
    public UserGroupPermissionDto Permissions { get; init; }
}

public class UserGroupPermissionDto
{
    public EnvelopePermissions Users { get; init; }
    public UserGroupEnvelopesPermissionDto Envelopes { get; init; }
    public UserGroupEnvelopesPermissionDto Templates { get; init; }
    public UserGroupContactsPermissionDto Contacts { get; init; }
}

public class UserGroupPermissionsSetDto
{
    public ATrustCertificateDto UserGroups { get; init; }
}

public class UserGroupsListDto
{
    public List<UserGroupDto> UserGroups { get; init; }
    public Pagination Pagination { get; init; }
}

public class UserGroupUserBusinessRoleRequest
{
    public string BusinessRoleId { get; init; }
}

public class UserGroupUserDto
{
    public string Id { get; init; }
    public string Email { get; init; }
    public string GivenName { get; init; }
    public string Surname { get; init; }
    public UserGroupPermissionDto Permissions { get; init; }
    public string? BusinessRole { get; init; }
    public string? BusinessRoleId { get; init; }
}

public class UserGroupUserListDto
{
    public string UserGroupId { get; init; }
    public List<UserGroupUserDto> UserGroupUsers { get; init; }
    public Pagination Pagination { get; init; }
}

public class UserOrganizationsDto
{
    public List<OrganizationItemDto> Organizations { get; init; }
    public string DefaultOrganizationId { get; init; }
}

public class ValidateOrganizationDto
{
    public string Name { get; init; }
    public string IsoCulture { get; init; }
    public string? OnePlatformBusinessRelationIdentifier { get; init; }
    public List<string> Features { get; init; }
}

public class VersionInfo
{
    public string ImageTag { get; init; }
    public string Version { get; init; }
}

public class WebhookSubscriptionDto
{
    public string Id { get; init; }
    public string Url { get; init; }
    public bool HasHeaders { get; init; }
    public bool HasClientCertificate { get; init; }
    public string CreatedAt { get; init; }
}

public class WebhookSubscriptionRequest
{
    public string Url { get; init; }
    public ATrustCertificateDto? Authentication { get; init; }
}

public class WorkUnitAreaReadConfirmationFieldResponse
{
    public string Id { get; init; }
    public long Page { get; init; }
    public decimal PositionX { get; init; }
    public decimal PositionY { get; init; }
    public decimal Width { get; init; }
    public decimal Height { get; init; }
    public bool Required { get; init; }
    public bool ReadOnly { get; init; }
    public string FieldType { get; init; }
}

public class WorkUnitDateInputValue
{
    public string Value { get; init; }
    public string TextInputType { get; init; }
}

public class WorkUnitDropDownFieldResponse
{
    public string Id { get; init; }
    public long Page { get; init; }
    public decimal PositionX { get; init; }
    public decimal PositionY { get; init; }
    public decimal Width { get; init; }
    public decimal Height { get; init; }
    public WorkUnitFontStyleResponse Font { get; init; }
    public List<WorkUnitDropDownFieldResponseoptions>? Options { get; init; }
    public bool Required { get; init; }
    public bool ReadOnly { get; init; }
    public bool IsEditable { get; init; }
    public string FieldType { get; init; }
}

public class WorkUnitFieldTaskResponse
{
    public WorkUnitFieldTaskResponsefieldBase Field { get; init; }
    public long SortOrder { get; init; }
    public string? RecipientId { get; init; }
    public ElementSource Source { get; init; }
    public string? DisplayName { get; init; }
    public bool Completed { get; init; }
}

public class WorkUnitFieldTaskResponsefield
{
    public string Id { get; init; }
    public long Page { get; init; }
    public decimal PositionX { get; init; }
    public decimal PositionY { get; init; }
    public decimal Width { get; init; }
    public decimal Height { get; init; }
    public WorkUnitFontStyleResponse Font { get; init; }
    public WorkUnitFieldTaskResponsefieldtextInputConfigBase TextInputConfig { get; init; }
    public bool Required { get; init; }
    public bool ReadOnly { get; init; }
    public string FieldType { get; init; }
}

public class WorkUnitFieldTaskResponsefield
{
    public string Id { get; init; }
    public long Page { get; init; }
    public decimal PositionX { get; init; }
    public decimal PositionY { get; init; }
    public decimal Width { get; init; }
    public decimal Height { get; init; }
    public WorkUnitFontStyleResponse Font { get; init; }
    public List<WorkUnitFieldTaskResponsefieldoptions>? Options { get; init; }
    public bool MultiSelect { get; init; }
    public bool Required { get; init; }
    public bool ReadOnly { get; init; }
    public string FieldType { get; init; }
}

public class WorkUnitFileReadConfirmationFieldResponse
{
    public string Id { get; init; }
    public bool Required { get; init; }
    public bool Confirmed { get; init; }
    public bool ReadOnly { get; init; }
    public string FieldType { get; init; }
}

public class WorkUnitFileResponse
{
    public long DocumentNumber { get; init; }
    public string Name { get; init; }
    public List<WorkUnitFieldTaskResponse> Tasks { get; init; }
}

public class WorkUnitFontStyleResponse
{
    public string Color { get; init; }
    public decimal Size { get; init; }
    public string Name { get; init; }
    public bool Bold { get; init; }
    public bool Italic { get; init; }
    public TextAlignment Align { get; init; }
}

public class WorkUnitLinkFieldResponse
{
    public string Id { get; init; }
    public long Page { get; init; }
    public decimal PositionX { get; init; }
    public decimal PositionY { get; init; }
    public decimal Width { get; init; }
    public decimal Height { get; init; }
    public string? Reference { get; init; }
    public string FieldType { get; init; }
}

public class WorkUnitListBoxFieldResponse
{
    public string Id { get; init; }
    public long Page { get; init; }
    public decimal PositionX { get; init; }
    public decimal PositionY { get; init; }
    public decimal Width { get; init; }
    public decimal Height { get; init; }
    public WorkUnitFontStyleResponse Font { get; init; }
    public List<WorkUnitListBoxFieldResponseoptions>? Options { get; init; }
    public bool MultiSelect { get; init; }
    public bool Required { get; init; }
    public bool ReadOnly { get; init; }
    public string FieldType { get; init; }
}

public class WorkUnitNumberInputConfigResponseResponse
{
    public decimal? Value { get; init; }
    public NumberSymbol? Symbol { get; init; }
    public string? ThousandsSeparator { get; init; }
    public WorkUnitDecimalSeparatorTypeResponse? DecimalSeparator { get; init; }
    public long? DecimalPlaces { get; init; }
    public decimal? MinValue { get; init; }
    public decimal? MaxValue { get; init; }
    public string TextInputType { get; init; }
}

public class WorkUnitNumberInputValue
{
    public decimal Value { get; init; }
    public string TextInputType { get; init; }
}

public class WorkUnitOptionResponse
{
    public string Key { get; init; }
    public string Value { get; init; }
    public bool Selected { get; init; }
}

public class WorkUnitPageReadConfirmationFieldResponse
{
    public string Id { get; init; }
    public long Page { get; init; }
    public decimal PositionX { get; init; }
    public decimal PositionY { get; init; }
    public decimal Width { get; init; }
    public decimal Height { get; init; }
    public bool Required { get; init; }
    public bool ReadOnly { get; init; }
    public string FieldType { get; init; }
}

public class WorkUnitPhoneNumberInputConfigResponseResponse
{
    public string Value { get; init; }
    public string? Format { get; init; }
    public string TextInputType { get; init; }
}

public class WorkUnitResponse
{
    public string Id { get; init; }
    public List<WorkUnitFileResponse> Files { get; init; }
    public bool IsSequenceEnforced { get; init; }
    public bool IsFinished { get; init; }
}

public class WorkUnitSignatureFieldResponse
{
    public string Id { get; init; }
    public long Page { get; init; }
    public decimal PositionX { get; init; }
    public decimal PositionY { get; init; }
    public decimal Width { get; init; }
    public decimal Height { get; init; }
    public List<WorkUnitSignatureFieldResponseallowedSignatureTypesBase> AllowedSignatureTypes { get; init; }
    public bool? QualifiedTimeStamp { get; init; }
    public bool Required { get; init; }
    public bool ReadOnly { get; init; }
    public string FieldType { get; init; }
}

public class WorkUnitSignaturePosition
{
    public decimal X { get; init; }
    public decimal Y { get; init; }
    public decimal Width { get; init; }
    public decimal Height { get; init; }
}

public class WorkUnitStringInputValue
{
    public string Value { get; init; }
    public string TextInputType { get; init; }
}

public class WorkUnitTextFieldResponse
{
    public string Id { get; init; }
    public long Page { get; init; }
    public decimal PositionX { get; init; }
    public decimal PositionY { get; init; }
    public decimal Width { get; init; }
    public decimal Height { get; init; }
    public WorkUnitFontStyleResponse Font { get; init; }
    public WorkUnitTextFieldResponsetextInputConfigBase TextInputConfig { get; init; }
    public bool Required { get; init; }
    public bool ReadOnly { get; init; }
    public string FieldType { get; init; }
}