using System;
using System.Collections.Generic;

public class AbsoluteIntegrationExpirationDto
{
    public string Mode { get; init; }
}

public class AccessCode
{
}

public class Actor
{
    public string Email { get; init; }
}

public class Actor
{
    public string Email { get; init; }
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

public class AddedTemplateFileResponse
{
    public string Id { get; init; }
}

public class AddUserGroupUserDto
{
    public List<string> AddedUsers { get; init; }
    public List<string> SkippedUsers { get; init; }
}

public class AddUsersToUserGroupDto
{
}

public class AdminMeDto
{
    public string Email { get; init; }
    public string GivenName { get; init; }
    public string Surname { get; init; }
    public bool IsInstanceAdmin { get; init; }
    public bool IsAdminUser { get; init; }
    public List<Anonymous1> Users { get; init; }
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
}

public class AgreementRequest
{
    public string Language { get; init; }
    public string Body { get; init; }
}

public class AgreementResponse
{
    public string Language { get; init; }
    public string Body { get; init; }
}

public class AgreementSettingsRequest
{
    public bool Enabled { get; init; }
    public bool Overridable { get; init; }
    public List<Anonymous2> Agreements { get; init; }
}

public class AgreementSettingsResponse
{
    public bool Enabled { get; init; }
    public bool Overridable { get; init; }
    public List<Anonymous3> Agreements { get; init; }
}

public class AllowedSignatureTypes
{
}

public class AllowedSignatureTypes
{
}

public class AllowedSignatureTypesDto
{
}

public class AnnotationElementDefinition
{
    public Position Position { get; init; }
    public Size Size { get; init; }
    public TextFormat TextFormat { get; init; }
    public object ValueFormat { get; init; }
}

public class AnnotationElementDto
{
    public string ElementId { get; init; }
    public ElementDefinition ElementDefinition { get; init; }
    public string Source { get; init; }
}

public class AnnotationField
{
    public string Id { get; init; }
    public long Page { get; init; }
    public double PositionX { get; init; }
    public double PositionY { get; init; }
    public object ValueFormat { get; init; }
    public string FieldType { get; init; }
    public double Width { get; init; }
    public double Height { get; init; }
    public bool Required { get; init; }
    public string Source { get; init; }
}

public class AnnotationFieldDto
{
    public string Id { get; init; }
    public long Page { get; init; }
    public double PositionX { get; init; }
    public double PositionY { get; init; }
    public double Width { get; init; }
    public double Height { get; init; }
    public object AnnotationConfig { get; init; }
    public string FieldType { get; init; }
}

public class Anonymous1
{
    public string UserId { get; init; }
    public string OrganizationId { get; init; }
    public string OrganizationName { get; init; }
    public bool IsEnabled { get; init; }
}

public class Anonymous10
{
    public string Id { get; init; }
    public string GivenName { get; init; }
    public string Surname { get; init; }
    public string Email { get; init; }
    public string CultureIsoCode { get; init; }
}

public class Anonymous100
{
    public long Row { get; init; }
    public string Field { get; init; }
    public string Message { get; init; }
}

public class Anonymous101
{
    public string Id { get; init; }
    public string Name { get; init; }
    public string UserId { get; init; }
}

public class Anonymous102
{
    public object Field { get; init; }
    public long SortOrder { get; init; }
    public string Source { get; init; }
    public bool Completed { get; init; }
}

public class Anonymous103
{
    public long DocumentNumber { get; init; }
    public string Name { get; init; }
    public List<Anonymous104> Tasks { get; init; }
}

public class Anonymous104
{
    public object Field { get; init; }
    public long SortOrder { get; init; }
    public string Source { get; init; }
    public bool Completed { get; init; }
}

public class Anonymous11
{
    public long Id { get; init; }
    public string IsoCode { get; init; }
    public string EnglishName { get; init; }
}

public class Anonymous12
{
    public string Name { get; init; }
    public string Code { get; init; }
}

public class Anonymous13
{
    public string Name { get; init; }
    public string DataType { get; init; }
    public bool Required { get; init; }
    public long SortOrder { get; init; }
}

public class Anonymous14
{
    public string Entity { get; init; }
    public string Action { get; init; }
}

public class Anonymous15
{
    public string Name { get; init; }
}

public class Anonymous16
{
    public long Id { get; init; }
    public string Name { get; init; }
    public string Sample { get; init; }
}

public class Anonymous17
{
    public string Code { get; init; }
    public string Example { get; init; }
}

public class Anonymous18
{
    public string Id { get; init; }
    public string Name { get; init; }
}

public class Anonymous19
{
    public string Id { get; init; }
    public string Name { get; init; }
}

public class Anonymous2
{
    public string Language { get; init; }
    public string Body { get; init; }
}

public class Anonymous20
{
    public string Id { get; init; }
    public string Name { get; init; }
}

public class Anonymous21
{
    public string DeviceId { get; init; }
    public string OtpDeviceType { get; init; }
    public string OtpDeviceTypeId { get; init; }
    public string IdentificationInformation { get; init; }
}

public class Anonymous22
{
    public string Id { get; init; }
    public string ErrorId { get; init; }
}

public class Anonymous23
{
    public string Id { get; init; }
    public string GivenName { get; init; }
    public string Surname { get; init; }
    public string Email { get; init; }
    public bool RequiresDelegationCompletion { get; init; }
}

public class Anonymous24
{
    public string Id { get; init; }
    public string Name { get; init; }
    public string Type { get; init; }
}

public class Anonymous25
{
    public string Id { get; init; }
    public string Type { get; init; }
    public string OccurredAt { get; init; }
    public Actor Actor { get; init; }
    public Data Data { get; init; }
}

public class Anonymous26
{
    public string FieldDefinitionId { get; init; }
}

public class Anonymous27
{
    public object Field { get; init; }
    public long SortOrder { get; init; }
    public string Source { get; init; }
}

public class Anonymous28
{
    public string Id { get; init; }
    public string Name { get; init; }
    public long SortOrder { get; init; }
}

public class Anonymous29
{
    public string Id { get; init; }
    public string Name { get; init; }
    public bool ExpiringSoon { get; init; }
    public SenderUser SenderUser { get; init; }
    public string UpdatedAt { get; init; }
    public string Status { get; init; }
    public List<string> Actions { get; init; }
    public string CreatedAt { get; init; }
}

public class Anonymous3
{
    public string Language { get; init; }
    public string Body { get; init; }
}

public class Anonymous30
{
    public string Id { get; init; }
    public long SortOrder { get; init; }
    public long RequiredRecipientCompletions { get; init; }
    public string Type { get; init; }
    public List<object> Recipients { get; init; }
}

public class Anonymous31
{
    public string ElementId { get; init; }
    public ElementDefinition ElementDefinition { get; init; }
    public string Source { get; init; }
    public bool Required { get; init; }
    public string Value { get; init; }
    public long GuidingOrder { get; init; }
}

public class Anonymous32
{
    public string ElementId { get; init; }
    public ElementDefinition ElementDefinition { get; init; }
    public string Source { get; init; }
    public bool Required { get; init; }
    public bool IsChecked { get; init; }
    public long GuidingOrder { get; init; }
}

public class Anonymous33
{
    public string ElementId { get; init; }
    public AllowedSignatureTypes AllowedSignatureTypes { get; init; }
    public ElementDefinition ElementDefinition { get; init; }
    public string Source { get; init; }
    public bool Required { get; init; }
    public long GuidingOrder { get; init; }
    public bool IsApprove { get; init; }
}

public class Anonymous34
{
    public ElementDefinition ElementDefinition { get; init; }
    public string Source { get; init; }
    public string ElementId { get; init; }
    public bool Required { get; init; }
    public long GuidingOrder { get; init; }
}

public class Anonymous35
{
    public ElementDefinition ElementDefinition { get; init; }
    public string ElementId { get; init; }
    public List<Anonymous36> Items { get; init; }
    public bool IsRequired { get; init; }
    public bool IsEditable { get; init; }
    public bool IsMultiselect { get; init; }
    public bool IsChecked { get; init; }
    public string Source { get; init; }
    public long GuidingOrder { get; init; }
}

public class Anonymous36
{
    public string Key { get; init; }
    public string Value { get; init; }
    public bool IsSelected { get; init; }
}

public class Anonymous37
{
    public string ElementId { get; init; }
    public bool Required { get; init; }
    public long GuidingOrder { get; init; }
}

public class Anonymous38
{
    public string ElementId { get; init; }
    public long PageNumber { get; init; }
    public bool Required { get; init; }
    public long GuidingOrder { get; init; }
}

public class Anonymous39
{
    public string ElementId { get; init; }
    public bool Required { get; init; }
    public ElementDefinition ElementDefinition { get; init; }
    public string Source { get; init; }
    public long GuidingOrder { get; init; }
}

public class Anonymous4
{
    public string Id { get; init; }
    public long MandatoryRecipientsNumber { get; init; }
    public string StageMode { get; init; }
}

public class Anonymous40
{
    public ElementDefinition ElementDefinition { get; init; }
    public string Source { get; init; }
    public string ElementId { get; init; }
    public string Value { get; init; }
    public long GuidingOrder { get; init; }
}

public class Anonymous41
{
    public string ElementId { get; init; }
    public bool Required { get; init; }
    public ElementDefinition ElementDefinition { get; init; }
    public string Source { get; init; }
    public string Label { get; init; }
    public long GuidingOrder { get; init; }
}

public class Anonymous42
{
    public string ElementId { get; init; }
    public ElementDefinition ElementDefinition { get; init; }
    public string Source { get; init; }
}

public class Anonymous43
{
    public string ElementId { get; init; }
    public ElementDefinition ElementDefinition { get; init; }
    public string Source { get; init; }
    public string GroupName { get; init; }
    public bool IsChecked { get; init; }
    public bool IsSelectInUnison { get; init; }
    public bool Required { get; init; }
    public string Value { get; init; }
    public long GuidingOrder { get; init; }
}

public class Anonymous44
{
    public string ElementId { get; init; }
    public ElementDefinition ElementDefinition { get; init; }
    public string Source { get; init; }
    public bool Required { get; init; }
    public long GuidingOrder { get; init; }
}

public class Anonymous45
{
    public string ElementId { get; init; }
    public string Source { get; init; }
    public bool Required { get; init; }
    public long GuidingOrder { get; init; }
}

public class Anonymous46
{
    public string Id { get; init; }
    public string Name { get; init; }
    public bool Canceled { get; init; }
}

public class Anonymous47
{
    public string Id { get; init; }
    public string GivenName { get; init; }
    public string Surname { get; init; }
    public string Email { get; init; }
    public RegionalSettings RegionalSettings { get; init; }
    public bool Enabled { get; init; }
}

public class Anonymous48
{
    public string Id { get; init; }
    public string GivenName { get; init; }
    public string Surname { get; init; }
    public string Email { get; init; }
    public RegionalSettings RegionalSettings { get; init; }
    public bool Enabled { get; init; }
}

public class Anonymous49
{
    public string Id { get; init; }
    public long SortOrder { get; init; }
}

public class Anonymous5
{
    public object Field { get; init; }
    public long SortOrder { get; init; }
    public string Source { get; init; }
}

public class Anonymous50
{
    public string Id { get; init; }
    public string Name { get; init; }
    public long SortOrder { get; init; }
}

public class Anonymous51
{
    public string Id { get; init; }
    public long SortOrder { get; init; }
}

public class Anonymous52
{
    public string Id { get; init; }
    public string Name { get; init; }
    public long SortOrder { get; init; }
}

public class Anonymous53
{
    public string Id { get; init; }
    public long SortOrder { get; init; }
}

public class Anonymous54
{
    public string Id { get; init; }
    public string Name { get; init; }
    public long SortOrder { get; init; }
}

public class Anonymous55
{
    public string Code { get; init; }
    public string Name { get; init; }
}

public class Anonymous56
{
    public string Code { get; init; }
    public string Name { get; init; }
}

public class Anonymous57
{
    public string Id { get; init; }
    public string Code { get; init; }
    public string Name { get; init; }
    public bool IsActive { get; init; }
}

public class Anonymous58
{
    public string Code { get; init; }
    public bool IsActive { get; init; }
}

public class Anonymous59
{
    public string Key { get; init; }
    public string Value { get; init; }
    public bool IsSelected { get; init; }
}

public class Anonymous6
{
    public string Id { get; init; }
    public string Name { get; init; }
    public string Status { get; init; }
    public string CreatedAt { get; init; }
    public string UpdatedAt { get; init; }
}

public class Anonymous60
{
    public Recipient Recipient { get; init; }
    public string Link { get; init; }
}

public class Anonymous61
{
}

public class Anonymous62
{
    public long Id { get; init; }
    public string ExternalId { get; init; }
    public string Name { get; init; }
    public string ClientId { get; init; }
    public string AuthorizationUri { get; init; }
    public string TokenUri { get; init; }
    public long AuthenticationType { get; init; }
}

public class Anonymous63
{
    public long Id { get; init; }
    public bool Enabled { get; init; }
    public string Name { get; init; }
}

public class Anonymous64
{
    public string PluginId { get; init; }
    public string Name { get; init; }
    public bool AllowUserSigning { get; init; }
    public bool AllowBatchUserSigning { get; init; }
    public bool AllowAutomaticSigning { get; init; }
    public string Category { get; init; }
}

public class Anonymous65
{
    public string Id { get; init; }
    public string Name { get; init; }
    public bool IsSystemRole { get; init; }
}

public class Anonymous66
{
    public string GivenName { get; init; }
    public string Surname { get; init; }
    public string Email { get; init; }
}

public class Anonymous67
{
    public string Id { get; init; }
    public string Name { get; init; }
    public string CreatedAt { get; init; }
    public string ExpiresAt { get; init; }
}

public class Anonymous68
{
    public string Id { get; init; }
    public string Name { get; init; }
    public bool IsActive { get; init; }
}

public class Anonymous69
{
    public string Name { get; init; }
    public bool IsEnabled { get; init; }
}

public class Anonymous7
{
    public long Row { get; init; }
    public string Field { get; init; }
    public string Message { get; init; }
}

public class Anonymous70
{
    public string Entity { get; init; }
    public string Action { get; init; }
}

public class Anonymous71
{
    public string SubjectName { get; init; }
    public string Thumbprint { get; init; }
    public string ExpirationDate { get; init; }
    public string Issuer { get; init; }
}

public class Anonymous72
{
    public string LanguageCode { get; init; }
    public string Text { get; init; }
}

public class Anonymous73
{
    public string ClientId { get; init; }
    public string Email { get; init; }
    public string UserId { get; init; }
}

public class Anonymous74
{
    public string Id { get; init; }
    public bool DisplayFirstname { get; init; }
    public bool DisplayLastname { get; init; }
    public bool DisplayCustomText { get; init; }
    public bool DisplayDateTime { get; init; }
    public bool DisplayEmail { get; init; }
    public bool DisplayReason { get; init; }
    public string Position { get; init; }
}

public class Anonymous75
{
    public string Type { get; init; }
    public string Country { get; init; }
}

public class Anonymous76
{
    public object Field { get; init; }
    public long SortOrder { get; init; }
    public string Source { get; init; }
}

public class Anonymous77
{
    public string Id { get; init; }
    public string Name { get; init; }
    public long SortOrder { get; init; }
}

public class Anonymous78
{
    public string Id { get; init; }
    public string CreatorUserId { get; init; }
    public string Name { get; init; }
    public List<string> Actions { get; init; }
    public string CreatedAt { get; init; }
    public string UpdatedAt { get; init; }
}

public class Anonymous79
{
    public string Id { get; init; }
    public long SortOrder { get; init; }
    public long RequiredRecipientCompletions { get; init; }
    public string Type { get; init; }
    public List<object> Recipients { get; init; }
}

public class Anonymous8
{
    public string Id { get; init; }
    public string OrganizationId { get; init; }
    public string Name { get; init; }
    public long AssignmentCount { get; init; }
    public string CreatedAt { get; init; }
    public string UpdatedAt { get; init; }
}

public class Anonymous80
{
    public string TimeZone { get; init; }
    public string Code { get; init; }
    public string UtcOffset { get; init; }
}

public class Anonymous81
{
    public string Code { get; init; }
    public string Name { get; init; }
}

public class Anonymous82
{
    public object Field { get; init; }
    public long SortOrder { get; init; }
}

public class Anonymous83
{
    public object Field { get; init; }
    public long SortOrder { get; init; }
}

public class Anonymous84
{
    public object Field { get; init; }
    public long SortOrder { get; init; }
}

public class Anonymous85
{
    public string Id { get; init; }
    public long SortOrder { get; init; }
}

public class Anonymous86
{
    public object Field { get; init; }
    public long SortOrder { get; init; }
}

public class Anonymous87
{
    public long Id { get; init; }
    public bool Enabled { get; init; }
}

public class Anonymous88
{
    public string Entity { get; init; }
    public string Action { get; init; }
}

public class Anonymous89
{
    public string Id { get; init; }
    public long SortOrder { get; init; }
}

public class Anonymous9
{
    public long Row { get; init; }
    public string Field { get; init; }
    public string Message { get; init; }
}

public class Anonymous90
{
    public string Id { get; init; }
    public bool DisplayFirstname { get; init; }
    public bool DisplayLastname { get; init; }
    public bool DisplayCustomText { get; init; }
    public bool DisplayDateTime { get; init; }
    public bool DisplayEmail { get; init; }
    public bool DisplayReason { get; init; }
    public string Position { get; init; }
}

public class Anonymous91
{
    public object Field { get; init; }
    public long SortOrder { get; init; }
}

public class Anonymous92
{
    public object Field { get; init; }
    public long SortOrder { get; init; }
}

public class Anonymous93
{
    public string PluginId { get; init; }
    public string Name { get; init; }
    public bool AllowUserSigning { get; init; }
    public bool AllowBatchUserSigning { get; init; }
    public bool AllowAutomaticSigning { get; init; }
    public string Category { get; init; }
}

public class Anonymous94
{
    public string Id { get; init; }
    public string UserGroupId { get; init; }
    public string Name { get; init; }
}

public class Anonymous95
{
    public long Row { get; init; }
    public string Field { get; init; }
    public string Message { get; init; }
}

public class Anonymous96
{
    public string Id { get; init; }
    public string UserGroupId { get; init; }
}

public class Anonymous97
{
    public string UserGroupId { get; init; }
    public string Name { get; init; }
}

public class Anonymous98
{
    public string Id { get; init; }
    public string Email { get; init; }
    public string GivenName { get; init; }
    public string Surname { get; init; }
    public Permissions Permissions { get; init; }
}

public class Anonymous99
{
    public string Id { get; init; }
    public string OrganizationId { get; init; }
    public string Name { get; init; }
}

public class ApprovalField
{
    public string FieldType { get; init; }
    public string Id { get; init; }
    public long Page { get; init; }
    public double PositionX { get; init; }
    public double PositionY { get; init; }
    public double Width { get; init; }
    public double Height { get; init; }
    public bool Required { get; init; }
    public string Source { get; init; }
}

public class ApprovalFieldDto
{
    public string Id { get; init; }
    public long Page { get; init; }
    public double PositionX { get; init; }
    public double PositionY { get; init; }
    public double Width { get; init; }
    public double Height { get; init; }
    public bool Required { get; init; }
    public string FieldType { get; init; }
}

public class ApproveElementDto
{
    public string ElementId { get; init; }
    public ElementDefinition ElementDefinition { get; init; }
    public string Source { get; init; }
    public bool Required { get; init; }
    public long GuidingOrder { get; init; }
}

public class AreaReadConfirmationDto
{
    public string ElementId { get; init; }
    public bool Required { get; init; }
    public ElementDefinition ElementDefinition { get; init; }
    public string Source { get; init; }
    public long GuidingOrder { get; init; }
}

public class AreaReadConfirmationField
{
    public string FieldType { get; init; }
    public string Id { get; init; }
    public long Page { get; init; }
    public double PositionX { get; init; }
    public double PositionY { get; init; }
    public double Width { get; init; }
    public double Height { get; init; }
    public bool Required { get; init; }
    public string Source { get; init; }
}

public class AreaReadConfirmationFieldDto
{
    public string Id { get; init; }
    public long Page { get; init; }
    public double PositionX { get; init; }
    public double PositionY { get; init; }
    public double Width { get; init; }
    public double Height { get; init; }
    public bool Required { get; init; }
    public string FieldType { get; init; }
}

public class AreaReadConfirmationTaskUpdateRequest
{
    public string FieldType { get; init; }
}

public class AreaReadElementDefinition
{
    public Position Position { get; init; }
    public Size Size { get; init; }
}

public class AssociateMyNamirialIdDto
{
    public string MyNamirialId { get; init; }
}

public class ATrustCertificateDto
{
}

public class ATrustCertificateSignatureTypeDto
{
}

public class AttachmentElementDefinition
{
    public Position Position { get; init; }
    public Size Size { get; init; }
}

public class AttachmentElementDto
{
    public string ElementId { get; init; }
    public bool Required { get; init; }
    public ElementDefinition ElementDefinition { get; init; }
    public string Source { get; init; }
    public string Label { get; init; }
    public long GuidingOrder { get; init; }
}

public class AttachmentField
{
    public string Id { get; init; }
    public string Label { get; init; }
    public string FieldType { get; init; }
    public long Page { get; init; }
    public double PositionX { get; init; }
    public double PositionY { get; init; }
    public double Width { get; init; }
    public double Height { get; init; }
    public bool Required { get; init; }
    public string Source { get; init; }
}

public class AttachmentFieldDto
{
    public string Id { get; init; }
    public long Page { get; init; }
    public double PositionX { get; init; }
    public double PositionY { get; init; }
    public double Width { get; init; }
    public double Height { get; init; }
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

public class AutomaticESealing
{
    public bool CreateUpdateDelete { get; init; }
}

public class AutomaticESealing
{
    public bool CreateUpdateDelete { get; init; }
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
    public string SignatureType { get; init; }
}

public class AutomaticSignatureDataDto
{
}

public class AutomaticSignatureTypeDto
{
}

public class BackgroundImageDto
{
    public string MimeType { get; init; }
    public string DataBase64 { get; init; }
}

public class BankIdSettingsDto
{
}

public class BaseField
{
    public string Id { get; init; }
    public long Page { get; init; }
    public double PositionX { get; init; }
    public double PositionY { get; init; }
    public double Width { get; init; }
    public double Height { get; init; }
    public bool Required { get; init; }
    public string Source { get; init; }
}

public class BaseFieldDto
{
    public string Id { get; init; }
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

public class BiometricSignatureTypeDto
{
}

public class BulkEnvelopeDetailDto
{
    public string Id { get; init; }
    public string Name { get; init; }
    public List<Anonymous4> Stages { get; init; }
}

public class BulkEnvelopeFieldTaskItem
{
    public object Field { get; init; }
    public long SortOrder { get; init; }
    public string Source { get; init; }
}

public class BulkEnvelopeFieldTaskItemRequest
{
    public object Field { get; init; }
    public long SortOrder { get; init; }
}

public class BulkEnvelopeFileTasksResponse
{
    public List<Anonymous5> Tasks { get; init; }
}

public class BulkEnvelopeListDto
{
    public List<Anonymous6> BulkEnvelopes { get; init; }
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
}

public class BulkRecipientDto
{
    public string Id { get; init; }
    public string RecipientType { get; init; }
    public string NotificationChannel { get; init; }
    public long Order { get; init; }
}

public class BulkRecipientValidationErrorResponse
{
    public List<Anonymous7> Errors { get; init; }
}

public class BulkStageDto
{
    public string Id { get; init; }
    public long MandatoryRecipientsNumber { get; init; }
    public string StageMode { get; init; }
}

public class BusinessRoleCreateDto
{
    public string Name { get; init; }
}

public class BusinessRoleDto
{
    public string Id { get; init; }
    public string OrganizationId { get; init; }
    public string Name { get; init; }
    public long AssignmentCount { get; init; }
    public string CreatedAt { get; init; }
    public string UpdatedAt { get; init; }
}

public class BusinessRolesListDto
{
    public List<Anonymous8> Items { get; init; }
    public Pagination Pagination { get; init; }
}

public class BusinessRoleUpdateDto
{
    public string Name { get; init; }
}

public class CallbackConfigurationDto
{
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
    public Position Position { get; init; }
    public Size Size { get; init; }
    public string ExportValue { get; init; }
    public bool ReadOnly { get; init; }
}

public class CheckBoxElementDto
{
    public string ElementId { get; init; }
    public ElementDefinition ElementDefinition { get; init; }
    public string Source { get; init; }
    public bool Required { get; init; }
    public bool IsChecked { get; init; }
    public long GuidingOrder { get; init; }
}

public class CheckboxField
{
    public string Id { get; init; }
    public long Page { get; init; }
    public double PositionX { get; init; }
    public double PositionY { get; init; }
    public bool ReadOnly { get; init; }
    public bool Checked { get; init; }
    public string Value { get; init; }
    public string FieldType { get; init; }
    public double Width { get; init; }
    public double Height { get; init; }
    public bool Required { get; init; }
    public string Source { get; init; }
}

public class CheckboxFieldDto
{
    public string Id { get; init; }
    public long Page { get; init; }
    public double PositionX { get; init; }
    public double PositionY { get; init; }
    public double Width { get; init; }
    public double Height { get; init; }
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
    public string SignatureType { get; init; }
}

public class ClickToSignSignature
{
    public string SignatureType { get; init; }
}

public class ClickToSignSignatureTypeDto
{
}

public class ClickToSignStampImprintDto
{
    public bool DisplayName { get; init; }
    public bool DisplaySignatureDate { get; init; }
    public bool DisplayExtraInformation { get; init; }
    public bool DisplayEmail { get; init; }
    public bool DisplayIp { get; init; }
}

public class ClonedEnvelopeDto
{
    public string Id { get; init; }
}

public class Completed
{
    public long EnvelopeCount { get; init; }
}

public class ContactDto
{
    public string Id { get; init; }
    public string GivenName { get; init; }
    public string Surname { get; init; }
    public string Email { get; init; }
    public string CultureIsoCode { get; init; }
}

public class ContactImportResultDto
{
    public long Imported { get; init; }
}

public class ContactImportValidationErrorResponse
{
    public List<Anonymous9> Errors { get; init; }
}

public class ContactListDto
{
    public List<Anonymous10> Contacts { get; init; }
    public Pagination Pagination { get; init; }
}

public class ContactRequest
{
    public string GivenName { get; init; }
    public string Surname { get; init; }
    public string Email { get; init; }
    public string CultureIsoCode { get; init; }
}

public class Contacts
{
    public bool Read { get; init; }
    public bool CreateUpdateDelete { get; init; }
    public bool Customize { get; init; }
}

public class Contacts
{
    public bool Read { get; init; }
    public bool CreateUpdateDelete { get; init; }
    public bool Customize { get; init; }
}

public class Contacts
{
    public bool Read { get; init; }
    public bool CreateUpdateDelete { get; init; }
    public bool Customize { get; init; }
}

public class Contacts
{
    public bool Read { get; init; }
    public bool CreateUpdateDelete { get; init; }
    public bool Customize { get; init; }
}

public class CountriesDto
{
    public List<Anonymous11> Options { get; init; }
}

public class CountriesLookupResponse
{
    public List<Anonymous12> Countries { get; init; }
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

public class CreateAccessCodeDto
{
}

public class CreateATrustCertificateDto
{
}

public class CreateAuthenticationConfigurationDto
{
}

public class CreateAutomaticSignatureDataDto
{
}

public class CreateBulkEnvelopeStageRequest
{
    public string Type { get; init; }
    public string Mode { get; init; }
}

public class CreatedDocumentClassDto
{
    public string Id { get; init; }
}

public class CreatedEnvelopeDto
{
    public string Id { get; init; }
}

public class CreatedEnvelopeFromTemplateDto
{
    public string CreatedEnvelopeId { get; init; }
}

public class CreateDisposableCertificateDto
{
}

public class CreateDocumentClassRequest
{
    public string Name { get; init; }
    public string Description { get; init; }
    public List<Anonymous13> Metadata { get; init; }
}

public class CreatedOrganizationDto
{
    public string Id { get; init; }
}

public class CreatedPersonalAccessTokenResponse
{
    public string Id { get; init; }
    public string Name { get; init; }
    public string Token { get; init; }
    public string CreatedAt { get; init; }
    public string ExpiresAt { get; init; }
}

public class CreatedPolicyResponse
{
    public string Id { get; init; }
}

public class CreatedRecipientResponse
{
    public string Id { get; init; }
}

public class CreatedStageResponse
{
    public string Id { get; init; }
}

public class CreatedTemplateDto
{
    public string Id { get; init; }
}

public class CreatedTemplateStageRecipientDto
{
    public string Id { get; init; }
}

public class CreatedUserDto
{
    public string Id { get; init; }
}

public class CreateEnvelopeStageAutomaticRecipientRequest
{
    public string Type { get; init; }
}

public class CreateEnvelopeStageRecipientRequest
{
}

public class CreateEnvelopeStageRequest
{
    public string Type { get; init; }
}

public class CreateEnvelopeStageStandardRecipientRequest
{
    public string Type { get; init; }
}

public class CreateGenericSigningPluginsSenderDataDto
{
}

public class CreateOAuthAuthenticationDto
{
    public string ExternalId { get; init; }
}

public class CreateOAuthFieldDefinitionRequest
{
    public string Path { get; init; }
    public string Mode { get; init; }
    public string Target { get; init; }
}

public class CreateOAuthJwtConfigRequest
{
    public string JwksUri { get; init; }
    public string Issuer { get; init; }
    public bool EnforceNonce { get; init; }
    public bool ValidateAudience { get; init; }
    public bool ValidateIssuer { get; init; }
    public bool ValidateLifetime { get; init; }
}

public class CreateOAuthResourceUriRequest
{
    public string Uri { get; init; }
    public string AccessTokenParamName { get; init; }
}

public class CreateOAuthSignerProviderDetailsRequest
{
    public OAuthSignerProvider OAuthSignerProvider { get; init; }
}

public class CreateOAuthSignerProviderRequest
{
    public string Name { get; init; }
    public string ClientId { get; init; }
    public string ClientSecret { get; init; }
    public string AuthorizationUri { get; init; }
    public string TokenUri { get; init; }
}

public class CreateOrganizationDto
{
    public string Name { get; init; }
    public string IsoCulture { get; init; }
    public License License { get; init; }
    public string OnePlatformBusinessRelationIdentifier { get; init; }
    public List<string> FeatureFlagsNames { get; init; }
}

public class CreateOrganizationUserRequestDto
{
    public string GivenName { get; init; }
    public string Surname { get; init; }
    public string Email { get; init; }
    public RegionalSettings RegionalSettings { get; init; }
}

public class CreateOrganizationUserResponse
{
    public string Id { get; init; }
}

public class CreateOtpSignatureDataDto
{
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
}

public class CreateRemoteCertificateDto
{
}

public class CreateRoleRequest
{
    public string Name { get; init; }
    public List<Anonymous14> Permissions { get; init; }
}

public class CreateSenderGenericSigningPluginDto
{
}

public class CreateSenderGenericSigningPluginSettingsDto
{
}

public class CreateServiceAccountRequest
{
    public string ClientId { get; init; }
    public string Email { get; init; }
    public RegionalSettings RegionalSettings { get; init; }
}

public class CreateServiceAccountResponse
{
    public string ClientId { get; init; }
    public string ClientSecret { get; init; }
    public string UserId { get; init; }
}

public class CreateSignatureDataConfigurationDto
{
}

public class CreateSmsOneTimePasswordDto
{
}

public class CreateStageResponse
{
    public string Id { get; init; }
}

public class CreateSubstituteDelegationDto
{
    public string DelegateeUserEmail { get; init; }
    public bool UtilizeAlsoOnCCRecipients { get; init; }
}

public class CreateSwedishBankIdDto
{
}

public class CreateSwissComOnDemandDto
{
}

public class CreateTemplateStageAutomaticRecipientRequest
{
    public string Type { get; init; }
}

public class CreateTemplateStageRecipientRequest
{
}

public class CreateTemplateStageRequest
{
    public string Type { get; init; }
}

public class CreateTemplateStageStandardRecipientRequest
{
    public bool IsDelegationEnabled { get; init; }
    public string Type { get; init; }
}

public class CreateUserDto
{
    public string GivenName { get; init; }
    public string Surname { get; init; }
    public string Email { get; init; }
    public string IsoLanguage { get; init; }
    public bool Enabled { get; init; }
    public List<Anonymous15> RoleNames { get; init; }
}

public class Data
{
}

public class Data
{
}

public class Data
{
}

public class DateAnnotationConfigDto
{
    public string Format { get; init; }
    public string AnnotationType { get; init; }
}

public class DateInputConfig
{
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

public class DateTimeFormatsDto
{
    public List<Anonymous16> Options { get; init; }
}

public class DateTimeFormatsLookupResponse
{
    public List<Anonymous17> DateTimeFormats { get; init; }
}

public class DateTimeOptionDto
{
    public long Id { get; init; }
    public string Name { get; init; }
    public string Sample { get; init; }
}

public class DefaultLayout
{
    public bool DisplayFirstname { get; init; }
    public bool DisplayLastname { get; init; }
    public bool DisplayCustomText { get; init; }
    public bool DisplayDateTime { get; init; }
    public bool DisplayEmail { get; init; }
    public bool DisplayReason { get; init; }
    public string Position { get; init; }
}

public class DefaultLayout
{
    public bool DisplayFirstname { get; init; }
    public bool DisplayLastname { get; init; }
    public bool DisplayDateTime { get; init; }
    public bool DisplayEmail { get; init; }
    public bool DisplayCustomText { get; init; }
    public bool DisplayReason { get; init; }
    public string Position { get; init; }
}

public class DefaultSignatureType
{
    public string SignatureType { get; init; }
}

public class DefaultUserGroupsDto
{
    public List<Anonymous18> EnvelopesShare { get; init; }
    public List<Anonymous19> TemplatesShare { get; init; }
}

public class DelegationInfo
{
    public bool Enabled { get; init; }
}

public class DelegationInfo
{
    public bool Enabled { get; init; }
}

public class DisposableCertificateDto
{
}

public class DisposableCertificateSettingsDto
{
    public bool HasPassword { get; init; }
    public bool ShowDisclaimerBeforeCertificateRequest { get; init; }
    public bool SendDisposableDisclaimerDocumentNotifications { get; init; }
}

public class DisposableCertificateSignature
{
    public string SignatureType { get; init; }
}

public class DisposableCertificateSignatureTypeDto
{
}

public class DisposableCertificateStampImprintDto
{
    public bool DisplayName { get; init; }
    public bool DisplaySignatureDate { get; init; }
    public bool DisplayExtraInformation { get; init; }
    public bool DisplayIp { get; init; }
}

public class Document
{
    public string Id { get; init; }
    public string Name { get; init; }
    public long SortOrder { get; init; }
}

public class DocumentClassDto
{
    public string Id { get; init; }
    public string Name { get; init; }
}

public class DocumentClassesResponse
{
    public List<Anonymous20> DocumentClasses { get; init; }
    public Pagination Pagination { get; init; }
}

public class DocumentClassListItemDto
{
    public string Id { get; init; }
    public string Name { get; init; }
}

public class DocumentClassLookupResponse
{
    public string Id { get; init; }
    public string Name { get; init; }
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
    public long GuidingOrder { get; init; }
}

public class DocumentsUploadRequest
{
    public List<string> Files { get; init; }
}

public class Draft
{
    public long EnvelopeCount { get; init; }
}

public class DrawToSignSignature
{
    public string SignatureType { get; init; }
}

public class DrawToSignSignatureTypeDto
{
}

public class DrawToSignStampImprintDto
{
    public bool DisplayName { get; init; }
    public bool DisplaySignatureDate { get; init; }
    public bool DisplayExtraInformation { get; init; }
    public bool DisplayEmail { get; init; }
    public bool DisplayIp { get; init; }
}

public class DropDownElementDefinition
{
    public Position Position { get; init; }
    public Size Size { get; init; }
    public bool ReadOnly { get; init; }
    public TextFormat TextFormat { get; init; }
}

public class DropDownElementDto
{
    public ElementDefinition ElementDefinition { get; init; }
    public string Source { get; init; }
    public string ElementId { get; init; }
    public bool Required { get; init; }
    public long GuidingOrder { get; init; }
}

public class DropDownField
{
    public string Id { get; init; }
    public long Page { get; init; }
    public double PositionX { get; init; }
    public double PositionY { get; init; }
    public bool ReadOnly { get; init; }
    public string FieldType { get; init; }
    public double Width { get; init; }
    public double Height { get; init; }
    public bool Required { get; init; }
    public string Source { get; init; }
}

public class DropDownFieldDto
{
    public string Id { get; init; }
    public long Page { get; init; }
    public double PositionX { get; init; }
    public double PositionY { get; init; }
    public double Width { get; init; }
    public double Height { get; init; }
    public Font Font { get; init; }
    public bool Required { get; init; }
    public bool ReadOnly { get; init; }
    public string FieldType { get; init; }
}

public class DropDownItemEntry
{
    public string Value { get; init; }
    public string Label { get; init; }
}

public class DropDownTaskUpdateRequest
{
    public string Value { get; init; }
    public string FieldType { get; init; }
}

public class ElementDefinition
{
    public Position Position { get; init; }
    public Size Size { get; init; }
    public TextFormat TextFormat { get; init; }
    public object ValueFormat { get; init; }
}

public class ElementDefinition
{
    public Position Position { get; init; }
    public Size Size { get; init; }
}

public class ElementDefinition
{
    public Position Position { get; init; }
    public Size Size { get; init; }
}

public class ElementDefinition
{
    public Position Position { get; init; }
    public Size Size { get; init; }
}

public class ElementDefinition
{
    public Position Position { get; init; }
    public Size Size { get; init; }
    public string ExportValue { get; init; }
    public bool ReadOnly { get; init; }
}

public class ElementDefinition
{
    public Position Position { get; init; }
    public Size Size { get; init; }
    public bool ReadOnly { get; init; }
    public TextFormat TextFormat { get; init; }
}

public class ElementDefinition
{
    public Position Position { get; init; }
    public Size Size { get; init; }
    public TextFormat TextFormat { get; init; }
    public bool ReadOnly { get; init; }
    public bool IsMultiline { get; init; }
    public bool IsPassword { get; init; }
    public long MaxLength { get; init; }
}

public class ElementDefinition
{
    public Position Position { get; init; }
    public Size Size { get; init; }
    public string ExportValue { get; init; }
    public bool ReadOnly { get; init; }
}

public class ElementDefinition
{
    public Position Position { get; init; }
    public Size Size { get; init; }
}

public class ElementDefinition
{
    public Position Position { get; init; }
    public Size Size { get; init; }
    public bool ReadOnly { get; init; }
    public TextFormat TextFormat { get; init; }
}

public class ElementDefinition
{
    public Position Position { get; init; }
    public Size Size { get; init; }
    public TextFormat TextFormat { get; init; }
    public bool ReadOnly { get; init; }
}

public class ElementDefinition
{
    public Position Position { get; init; }
    public Size Size { get; init; }
}

public class ElementDefinition
{
    public Position Position { get; init; }
    public Size Size { get; init; }
}

public class ElementDefinition
{
    public Position Position { get; init; }
    public Size Size { get; init; }
}

public class ElementDefinition
{
    public Position Position { get; init; }
    public Size Size { get; init; }
    public TextFormat TextFormat { get; init; }
    public object ValueFormat { get; init; }
}

public class ElementDefinition
{
    public Position Position { get; init; }
    public Size Size { get; init; }
    public bool ReadOnly { get; init; }
}

public class ElementDefinition
{
    public Position Position { get; init; }
    public Size Size { get; init; }
}

public class ElementDefinition
{
    public Position Position { get; init; }
    public Size Size { get; init; }
}

public class ElementDefinition
{
    public Position Position { get; init; }
    public Size Size { get; init; }
    public TextFormat TextFormat { get; init; }
    public bool ReadOnly { get; init; }
}

public class ElementDefinition
{
    public Position Position { get; init; }
    public Size Size { get; init; }
    public bool ReadOnly { get; init; }
}

public class ElementDefinition
{
    public Position Position { get; init; }
    public Size Size { get; init; }
}

public class ElementDefinition
{
    public Position Position { get; init; }
    public Size Size { get; init; }
    public TextFormat TextFormat { get; init; }
    public bool ReadOnly { get; init; }
    public bool IsMultiline { get; init; }
    public bool IsPassword { get; init; }
    public long MaxLength { get; init; }
}

public class EmailAnnotationConfigDto
{
    public string AnnotationType { get; init; }
}

public class EmailDefinition
{
    public string ValueFormat { get; init; }
}

public class EnabledOrganizationDto
{
    public string Id { get; init; }
}

public class EnableOrganizationDto
{
    public string OnePlatformBusinessRelationIdentifier { get; init; }
}

public class EnvelopeActionResponse
{
    public string EnvelopeId { get; init; }
    public long StatusCode { get; init; }
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
    public List<Anonymous21> Devices { get; init; }
}

public class EnvelopeBulkSignDto
{
    public List<string> EnvelopeIds { get; init; }
}

public class EnvelopeBulkSignResultDto
{
    public List<string> SignedEnvelopes { get; init; }
    public List<Anonymous22> FailedEnvelopes { get; init; }
}

public class EnvelopeBulkSignTransactionDto
{
    public string TransactionId { get; init; }
    public string PayloadFileId { get; init; }
}

public class EnvelopeCancelRequestDto
{
}

public class EnvelopeDetailDto
{
    public string Id { get; init; }
    public string Name { get; init; }
    public string Status { get; init; }
    public bool ExpiringSoon { get; init; }
    public bool SendCopyToAllRecipients { get; init; }
    public List<string> Actions { get; init; }
    public string UpdatedAt { get; init; }
    public bool PreventFieldsEditingWhenFinished { get; init; }
}

public class EnvelopeDetailRecipientDto
{
    public string Id { get; init; }
    public string GivenName { get; init; }
    public string Surname { get; init; }
    public string Email { get; init; }
    public bool RequiresDelegationCompletion { get; init; }
}

public class EnvelopeDetailStageDto
{
    public string Id { get; init; }
    public long SortOrder { get; init; }
    public long RequiredRecipientCompletions { get; init; }
    public List<Anonymous23> Recipients { get; init; }
}

public class EnvelopeDownloadDto
{
    public string Id { get; init; }
    public string Name { get; init; }
    public string Type { get; init; }
}

public class EnvelopeDownloadsResponse
{
    public List<Anonymous24> Downloads { get; init; }
}

public class EnvelopeDto
{
    public string Id { get; init; }
    public bool SendCopyToAllRecipients { get; init; }
    public bool LateIdent { get; init; }
    public bool UseInvisibleSignatureWithTimestampForAllDocumentsAndRecipients { get; init; }
    public bool ShowOrganizationAgreements { get; init; }
    public ReminderConfiguration ReminderConfiguration { get; init; }
    public ExpirationConfiguration ExpirationConfiguration { get; init; }
    public List<string> UserGroupSharingIds { get; init; }
    public string Status { get; init; }
    public string CreatedAt { get; init; }
    public string UpdatedAt { get; init; }
    public bool PreventFieldsEditingWhenFinished { get; init; }
    public bool SignatureReasonAllowChange { get; init; }
    public string SignatureFormat { get; init; }
    public bool FileRestrictedVisibility { get; init; }
}

public class EnvelopeEventDto
{
    public string Id { get; init; }
    public string Type { get; init; }
    public string OccurredAt { get; init; }
    public Actor Actor { get; init; }
    public Data Data { get; init; }
}

public class EnvelopeEventsDto
{
    public List<Anonymous25> Events { get; init; }
}

public class EnvelopeFieldTaskItem
{
    public object Field { get; init; }
    public long SortOrder { get; init; }
    public string Source { get; init; }
}

public class EnvelopeFileDetailDocumentClassDto
{
    public string DocumentClassId { get; init; }
}

public class EnvelopeFileDetailDocumentClassRequest
{
    public string DocumentClassId { get; init; }
    public List<Anonymous26> MetadataValues { get; init; }
}

public class EnvelopeFileMetadataValueDto
{
    public string FieldDefinitionId { get; init; }
    public string Type { get; init; }
}

public class EnvelopeFilesResponse
{
    public List<Anonymous28> Files { get; init; }
}

public class EnvelopeFileTasksResponse
{
    public List<Anonymous27> Tasks { get; init; }
}

public class EnvelopeInsights
{
    public WaitingForYou WaitingForYou { get; init; }
    public WaitingForOthers WaitingForOthers { get; init; }
    public Draft Draft { get; init; }
    public Completed Completed { get; init; }
    public Rejected Rejected { get; init; }
    public Expired Expired { get; init; }
}

public class EnvelopeListDto
{
    public List<Anonymous29> Envelopes { get; init; }
    public Pagination Pagination { get; init; }
}

public class EnvelopePartialDto
{
    public string Id { get; init; }
    public string Name { get; init; }
    public bool ExpiringSoon { get; init; }
    public SenderUser SenderUser { get; init; }
    public string UpdatedAt { get; init; }
    public string Status { get; init; }
    public List<string> Actions { get; init; }
    public string CreatedAt { get; init; }
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

public class EnvelopeRejectDto
{
}

public class EnvelopeResumeDto
{
}

public class Envelopes
{
    public bool Read { get; init; }
    public bool CreateUpdateDelete { get; init; }
}

public class Envelopes
{
    public bool Read { get; init; }
    public bool CreateUpdateDelete { get; init; }
}

public class Envelopes
{
    public bool Share { get; init; }
    public bool Manage { get; init; }
}

public class Envelopes
{
    public bool Share { get; init; }
    public bool Manage { get; init; }
}

public class Envelopes
{
    public bool Share { get; init; }
    public bool Manage { get; init; }
}

public class Envelopes
{
    public bool Share { get; init; }
    public bool Manage { get; init; }
}

public class EnvelopeSenderDto
{
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
    public string Type { get; init; }
}

public class EnvelopeStageAutomaticRecipientSummaryDto
{
    public string Id { get; init; }
    public string Type { get; init; }
}

public class EnvelopeStageItemDto
{
    public string Id { get; init; }
    public long SortOrder { get; init; }
    public long RequiredRecipientCompletions { get; init; }
    public string Type { get; init; }
    public List<object> Recipients { get; init; }
}

public class EnvelopeStageListDto
{
    public List<Anonymous30> Stages { get; init; }
}

public class EnvelopeStageRecipientResponse
{
    public string Id { get; init; }
}

public class EnvelopeStageRecipientSummaryDto
{
    public string Id { get; init; }
}

public class EnvelopeStageStandardRecipientResponse
{
    public string Id { get; init; }
    public string Type { get; init; }
}

public class EnvelopeStageStandardRecipientSummaryDto
{
    public string GivenName { get; init; }
    public string Id { get; init; }
    public string Surname { get; init; }
    public string Email { get; init; }
    public string Type { get; init; }
}

public class EnvelopeViewerLinkDto
{
    public string ViewerLink { get; init; }
}

public class ErrorResult
{
    public string ErrorId { get; init; }
    public string Description { get; init; }
}

public class Errors
{
}

public class ESealingRemoteSignatureProfileDto
{
    public string Id { get; init; }
}

public class ExpirationConfiguration
{
}

public class ExpirationConfiguration
{
}

public class ExpirationConfiguration
{
}

public class ExpirationConfiguration
{
}

public class ExpirationConfiguration
{
}

public class ExpirationConfiguration
{
}

public class ExpirationConfiguration
{
}

public class ExpirationConfigurationDto
{
}

public class Expired
{
    public long EnvelopeCount { get; init; }
}

public class FailedEnvelope
{
    public string Id { get; init; }
    public string ErrorId { get; init; }
}

public class FieldTask
{
    public object Field { get; init; }
    public long SortOrder { get; init; }
}

public class FieldTaskItemRequest
{
    public object Field { get; init; }
    public long SortOrder { get; init; }
}

public class FileDetailResponse
{
}

public class FileElementDateValidationConfiguration
{
    public Range Range { get; init; }
}

public class FileElementFieldValidationRange
{
}

public class FileElementNumberValidationConfiguration
{
    public string SymbolPosition { get; init; }
    public Range Range { get; init; }
    public string ThousandsSeparator { get; init; }
    public string DecimalSeparator { get; init; }
}

public class FileElementPhoneValidationConfiguration
{
    public string Type { get; init; }
}

public class FileElementsDto
{
    public List<Anonymous31> TextBoxElements { get; init; }
    public List<Anonymous32> CheckBoxElements { get; init; }
    public List<Anonymous33> SignatureElements { get; init; }
    public List<Anonymous34> DropDownElements { get; init; }
    public List<Anonymous35> ListElements { get; init; }
    public List<Anonymous37> DocumentReadConfirmations { get; init; }
    public List<Anonymous38> PageReadConfirmations { get; init; }
    public List<Anonymous39> AreaReadConfirmations { get; init; }
    public List<Anonymous40> LinkElements { get; init; }
    public List<Anonymous41> AttachmentElements { get; init; }
    public List<Anonymous42> AnnotationElements { get; init; }
    public List<Anonymous43> RadioButtonElements { get; init; }
    public List<Anonymous44> ApproveElements { get; init; }
    public List<Anonymous45> InvisibleSignatureElements { get; init; }
}

public class FileElementsFieldValidation
{
    public string Type { get; init; }
}

public class FileElementsPosition
{
    public long PageNumber { get; init; }
    public double X { get; init; }
    public double Y { get; init; }
}

public class FileElementsSize
{
    public double Width { get; init; }
    public double Height { get; init; }
}

public class FileElementTextFormat
{
    public string TextColor { get; init; }
    public double FontSizeInPt { get; init; }
    public string FontName { get; init; }
    public bool Bold { get; init; }
    public bool Italic { get; init; }
    public string TextAlign { get; init; }
}

public class FileElementTimeValidationConfiguration
{
    public Range Range { get; init; }
}

public class FileOrderItem
{
    public string Id { get; init; }
    public long SortOrder { get; init; }
}

public class FileReadConfirmationField
{
    public string FieldType { get; init; }
    public string Id { get; init; }
    public long Page { get; init; }
    public double PositionX { get; init; }
    public double PositionY { get; init; }
    public double Width { get; init; }
    public double Height { get; init; }
    public bool Required { get; init; }
    public string Source { get; init; }
}

public class FileReadConfirmationFieldDto
{
    public string Id { get; init; }
    public bool Required { get; init; }
    public string FieldType { get; init; }
}

public class FileReadConfirmationTaskUpdateRequest
{
    public string FieldType { get; init; }
}

public class FileTaskItem
{
    public object Field { get; init; }
    public long SortOrder { get; init; }
}

public class FirstNameAnnotationConfigDto
{
    public string AnnotationType { get; init; }
}

public class FirstNameDefinition
{
    public string ValueFormat { get; init; }
}

public class Font
{
    public string TextColor { get; init; }
    public double FontSizeInPt { get; init; }
    public string FontName { get; init; }
    public bool Bold { get; init; }
    public bool Italic { get; init; }
    public long TextAlign { get; init; }
}

public class Font
{
    public string TextColor { get; init; }
    public double FontSizeInPt { get; init; }
    public string FontName { get; init; }
    public bool Bold { get; init; }
    public bool Italic { get; init; }
    public long TextAlign { get; init; }
}

public class Font
{
    public string TextColor { get; init; }
    public double FontSizeInPt { get; init; }
    public string FontName { get; init; }
    public bool Bold { get; init; }
    public bool Italic { get; init; }
    public long TextAlign { get; init; }
}

public class Font
{
    public string Color { get; init; }
    public double Size { get; init; }
    public string Name { get; init; }
    public bool Bold { get; init; }
    public bool Italic { get; init; }
    public string Align { get; init; }
}

public class Font
{
    public string Color { get; init; }
    public double Size { get; init; }
    public string Name { get; init; }
    public bool Bold { get; init; }
    public bool Italic { get; init; }
    public string Align { get; init; }
}

public class Font
{
    public string Color { get; init; }
    public double Size { get; init; }
    public string Name { get; init; }
    public bool Bold { get; init; }
    public bool Italic { get; init; }
    public string Align { get; init; }
}

public class FontStyle
{
    public string TextColor { get; init; }
    public double FontSizeInPt { get; init; }
    public string FontName { get; init; }
    public bool Bold { get; init; }
    public bool Italic { get; init; }
    public long TextAlign { get; init; }
}

public class ForcedAuthenticationRulesRequest
{
    public string AuthenticationMode { get; init; }
    public bool ForceInputSmsAuthentication { get; init; }
    public bool AllowBiometricWithoutAuthentication { get; init; }
    public bool AllowComplexSignaturesWithoutAuthentication { get; init; }
}

public class ForcedAuthenticationRulesResponse
{
    public string AuthenticationMode { get; init; }
    public bool ForceInputSmsAuthentication { get; init; }
    public bool AllowBiometricWithoutAuthentication { get; init; }
    public bool AllowComplexSignaturesWithoutAuthentication { get; init; }
}

public class FullNameAnnotationConfigDto
{
    public string AnnotationType { get; init; }
}

public class FullNameDefinition
{
    public string ValueFormat { get; init; }
}

public class GeneralPolicies
{
    public bool AllowSaveDocument { get; init; }
    public bool AllowSaveAuditTrail { get; init; }
    public bool AllowPrintDocument { get; init; }
    public bool AllowAdhocPdfAttachments { get; init; }
    public bool AllowRejectWorkstep { get; init; }
    public bool AllowUndoLastAction { get; init; }
}

public class GeneralSettingsDto
{
    public string Name { get; init; }
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
    public string Category { get; init; }
}

public class GenericSigningPluginSenderSettingsDto
{
    public string PluginId { get; init; }
    public string Name { get; init; }
    public bool AllowUserSigning { get; init; }
    public bool AllowBatchUserSigning { get; init; }
    public bool AllowAutomaticSigning { get; init; }
    public string Category { get; init; }
    public string PluginFriendlyName { get; init; }
}

public class GenericSigningPluginSettingLabelDto
{
    public string LanguageCode { get; init; }
    public string Text { get; init; }
}

public class GenericSigningPluginsSenderDataDto
{
}

public class GetOrganizationsListResponse
{
    public List<Anonymous46> Organizations { get; init; }
    public Pagination Pagination { get; init; }
}

public class GetUsersListResponse
{
    public List<Anonymous47> Users { get; init; }
    public Pagination Pagination { get; init; }
}

public class GetUsersResponse
{
    public List<Anonymous48> Users { get; init; }
    public Pagination Pagination { get; init; }
}

public class HttpValidationProblemDetails
{
    public Errors Errors { get; init; }
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
    public string CreatedAt { get; init; }
    public string UpdatedAt { get; init; }
    public ExpirationConfiguration ExpirationConfiguration { get; init; }
    public string ExpirationMode { get; init; }
    public ReminderConfiguration ReminderConfiguration { get; init; }
    public bool QualifiedTimeStamp { get; init; }
    public string SignatureFormat { get; init; }
    public List<Anonymous49> Stages { get; init; }
    public List<Anonymous50> Files { get; init; }
    public string Status { get; init; }
    public bool FileRestrictedVisibility { get; init; }
}

public class IntegrationEnvelopeDto
{
    public string Id { get; init; }
    public string CreatedAt { get; init; }
    public string UpdatedAt { get; init; }
    public ExpirationConfiguration ExpirationConfiguration { get; init; }
    public string ExpirationMode { get; init; }
    public ReminderConfiguration ReminderConfiguration { get; init; }
    public bool QualifiedTimeStamp { get; init; }
    public string SignatureFormat { get; init; }
    public List<Anonymous51> Stages { get; init; }
    public List<Anonymous52> Files { get; init; }
    public string Status { get; init; }
    public bool FileRestrictedVisibility { get; init; }
}

public class IntegrationFileDto
{
    public string Id { get; init; }
    public string Name { get; init; }
    public long SortOrder { get; init; }
}

public class IntegrationStageDto
{
    public string Id { get; init; }
    public long SortOrder { get; init; }
}

public class IntegrationTemplateDto
{
    public string Id { get; init; }
    public string CreatedAt { get; init; }
    public string UpdatedAt { get; init; }
    public ExpirationConfiguration ExpirationConfiguration { get; init; }
    public string ExpirationMode { get; init; }
    public ReminderConfiguration ReminderConfiguration { get; init; }
    public bool QualifiedTimeStamp { get; init; }
    public string SignatureFormat { get; init; }
    public List<Anonymous53> Stages { get; init; }
    public List<Anonymous54> Files { get; init; }
}

public class InvisibleSignatureElementDto
{
    public string ElementId { get; init; }
    public string Source { get; init; }
    public bool Required { get; init; }
    public long GuidingOrder { get; init; }
}

public class InvisibleSignatureField
{
    public string Id { get; init; }
    public long Page { get; init; }
    public double PositionX { get; init; }
    public double PositionY { get; init; }
    public string FieldType { get; init; }
    public double Width { get; init; }
    public double Height { get; init; }
    public bool Required { get; init; }
    public string Source { get; init; }
}

public class InvisibleSignatureFieldDto
{
    public string Id { get; init; }
    public string FieldType { get; init; }
}

public class LanguageListItemDto
{
    public string Code { get; init; }
    public string Name { get; init; }
}

public class LanguagesDto
{
    public List<Anonymous55> Options { get; init; }
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
    public List<Anonymous56> Languages { get; init; }
}

public class LanguagesSettingsResponse
{
    public List<Anonymous57> Languages { get; init; }
}

public class LanguagesSettingsUpdateRequest
{
    public List<Anonymous58> Languages { get; init; }
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

public class License
{
    public string Type { get; init; }
    public string ExpirationDate { get; init; }
    public long UserLimit { get; init; }
    public long DocumentLimit { get; init; }
}

public class LicenseDto
{
    public string Type { get; init; }
    public string ExpirationDate { get; init; }
    public long UserLimit { get; init; }
    public long DocumentLimit { get; init; }
}

public class LinkElementDefinition
{
    public Position Position { get; init; }
    public Size Size { get; init; }
}

public class LinkElementDto
{
    public ElementDefinition ElementDefinition { get; init; }
    public string Source { get; init; }
    public string ElementId { get; init; }
    public string Value { get; init; }
    public long GuidingOrder { get; init; }
}

public class LinkField
{
    public string Id { get; init; }
    public long Page { get; init; }
    public double PositionX { get; init; }
    public double PositionY { get; init; }
    public string Url { get; init; }
    public string FieldType { get; init; }
    public double Width { get; init; }
    public double Height { get; init; }
    public bool Required { get; init; }
    public string Source { get; init; }
}

public class LinkFieldDto
{
    public string Id { get; init; }
    public long Page { get; init; }
    public double PositionX { get; init; }
    public double PositionY { get; init; }
    public double Width { get; init; }
    public double Height { get; init; }
    public string Reference { get; init; }
    public string FieldType { get; init; }
}

public class ListBoxField
{
    public string Id { get; init; }
    public long Page { get; init; }
    public double PositionX { get; init; }
    public double PositionY { get; init; }
    public bool ReadOnly { get; init; }
    public bool Multiselect { get; init; }
    public string FieldType { get; init; }
    public double Width { get; init; }
    public double Height { get; init; }
    public bool Required { get; init; }
    public string Source { get; init; }
}

public class ListBoxFieldDto
{
    public string Id { get; init; }
    public long Page { get; init; }
    public double PositionX { get; init; }
    public double PositionY { get; init; }
    public double Width { get; init; }
    public double Height { get; init; }
    public Font Font { get; init; }
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

public class ListElementDefinition
{
    public Position Position { get; init; }
    public Size Size { get; init; }
    public TextFormat TextFormat { get; init; }
    public bool ReadOnly { get; init; }
}

public class ListElementDto
{
    public ElementDefinition ElementDefinition { get; init; }
    public string ElementId { get; init; }
    public List<Anonymous59> Items { get; init; }
    public bool IsRequired { get; init; }
    public bool IsEditable { get; init; }
    public bool IsMultiselect { get; init; }
    public bool IsChecked { get; init; }
    public string Source { get; init; }
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
    public string SignatureType { get; init; }
}

public class LocalCertificateSignatureTypeDto
{
}

public class LocalCertificateStampImprintDto
{
    public bool DisplayName { get; init; }
    public bool DisplaySignatureDate { get; init; }
    public bool DisplayExtraInformation { get; init; }
    public bool DisplayIp { get; init; }
}

public class MetadataValueDto
{
    public string FieldDefinitionId { get; init; }
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
    public string Position { get; init; }
}

public class NamedSignatureAppearanceLayoutRequest
{
    public string Id { get; init; }
    public bool DisplayFirstname { get; init; }
    public bool DisplayLastname { get; init; }
    public bool DisplayCustomText { get; init; }
    public bool DisplayDateTime { get; init; }
    public bool DisplayEmail { get; init; }
    public bool DisplayReason { get; init; }
    public string Position { get; init; }
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
    public Recipient Recipient { get; init; }
    public string Link { get; init; }
}

public class NextRecipientLinksResponse
{
    public List<Anonymous60> NextRecipientLinks { get; init; }
}

public class NotificationChannelMessagesDto
{
    public List<Anonymous61> Messages { get; init; }
}

public class NotificationChannels
{
    public bool Email { get; init; }
    public bool Sms { get; init; }
    public bool WhatsApp { get; init; }
}

public class NotificationChannelsDto
{
    public bool Email { get; init; }
    public bool Sms { get; init; }
    public bool WhatsApp { get; init; }
}

public class NotificationMessageDto
{
}

public class NotificationPreferencesRequest
{
    public bool NotifyRecipientOnActionNeeded { get; init; }
}

public class NotificationPreferencesResponse
{
    public bool NotifyRecipientOnActionNeeded { get; init; }
}

public class NotificationSettings
{
    public string EmailSenderDisplayType { get; init; }
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

public class NotificationSettingsDto
{
    public string EmailSenderDisplayType { get; init; }
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
    public string ThousandsSeparator { get; init; }
    public string DecimalSeparator { get; init; }
    public string TextInputType { get; init; }
}

public class NumberSymbol
{
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
}

public class OAuthFieldReferenceDto
{
    public string FieldTarget { get; init; }
}

public class OAuthGenericSigningPluginReferenceDto
{
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
}

public class OAuthResourceUriDto
{
    public long Id { get; init; }
    public string Uri { get; init; }
    public string AccessTokenParamName { get; init; }
}

public class OAuthSignerProvider
{
    public string Name { get; init; }
    public string ClientId { get; init; }
    public string ClientSecret { get; init; }
    public string AuthorizationUri { get; init; }
    public string TokenUri { get; init; }
}

public class OAuthSignerProvider
{
    public long Id { get; init; }
    public string ExternalId { get; init; }
    public string Name { get; init; }
    public string ClientId { get; init; }
    public string AuthorizationUri { get; init; }
    public string TokenUri { get; init; }
    public long AuthenticationType { get; init; }
}

public class OAuthSignerProvider
{
    public long Id { get; init; }
    public string ExternalId { get; init; }
    public string Name { get; init; }
    public string ClientId { get; init; }
    public string AuthorizationUri { get; init; }
    public string TokenUri { get; init; }
    public long AuthenticationType { get; init; }
}

public class OAuthSignerProvider
{
    public string ExternalId { get; init; }
    public string Name { get; init; }
    public string ClientId { get; init; }
    public string AuthorizationUri { get; init; }
    public string TokenUri { get; init; }
    public long AuthenticationType { get; init; }
}

public class OAuthSignerProviderDetailsResponse
{
    public OAuthSignerProvider OAuthSignerProvider { get; init; }
}

public class OAuthSignerProviderDto
{
    public long Id { get; init; }
    public string ExternalId { get; init; }
    public string Name { get; init; }
    public string ClientId { get; init; }
    public string AuthorizationUri { get; init; }
    public string TokenUri { get; init; }
    public long AuthenticationType { get; init; }
}

public class OAuthSignerProviderFieldModeResponse
{
    public string Name { get; init; }
    public long Value { get; init; }
}

public class OAuthSignerProviderFieldTargetResponse
{
    public string Name { get; init; }
    public long Value { get; init; }
}

public class OAuthSignerProvidersResponse
{
    public List<Anonymous62> OAuthSignerProviders { get; init; }
    public Pagination Pagination { get; init; }
}

public class OneTimePasswordSignature
{
    public string SignatureType { get; init; }
}

public class OneTimePasswordSignatureTypeDto
{
}

public class OneTimePasswordStampImprintDto
{
    public bool DisplayName { get; init; }
    public bool DisplaySignatureDate { get; init; }
    public bool DisplayExtraInformation { get; init; }
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

public class OptionDto
{
    public string Key { get; init; }
    public string Value { get; init; }
    public bool IsSelected { get; init; }
}

public class OrganizationCustomTimeStampServerSettings
{
}

public class OrganizationDefaultSignatureTypeDto
{
    public string SignatureType { get; init; }
}

public class OrganizationDelegationSettingsDto
{
    public string DelegationPolicy { get; init; }
}

public class OrganizationDetailDto
{
    public string Id { get; init; }
    public string Name { get; init; }
    public string CreationDateUtc { get; init; }
    public bool Canceled { get; init; }
    public string LicenseType { get; init; }
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
    public List<Anonymous63> FeatureFlags { get; init; }
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

public class OrganizationLanguageLookupDto
{
    public string Code { get; init; }
    public string Name { get; init; }
}

public class OrganizationPAdESConfiguration
{
}

public class OrganizationRecipientAuthenticationTypesDto
{
}

public class OrganizationRecipientOAuthProviderDto
{
    public string Identifier { get; init; }
    public string Name { get; init; }
    public bool HasEIdAssertion { get; init; }
    public bool HasLateIdentSigTypes { get; init; }
    public bool ProvidesIdentification { get; init; }
    public long UpdateFieldComparisonValue { get; init; }
}

public class OrganizationRecipientSettingsDto
{
    public bool SendFinishedDocumentsToAllRecipients { get; init; }
    public bool ShowNotEnoughSignaturesWarning { get; init; }
    public bool DelegationAvailable { get; init; }
}

public class OrganizationSettings
{
    public bool Read { get; init; }
    public bool Update { get; init; }
}

public class OrganizationSettings
{
    public bool Read { get; init; }
    public bool Update { get; init; }
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
    public List<Anonymous64> AllowedGenericSigningPlugins { get; init; }
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
    public RegionalSettings RegionalSettings { get; init; }
    public bool Enabled { get; init; }
}

public class OrganizationUserRegionalSettingsDto
{
    public string TimeZone { get; init; }
    public string Language { get; init; }
    public string Country { get; init; }
    public string DateTimeFormat { get; init; }
}

public class OrganizationUserSummaryDto
{
    public string Id { get; init; }
    public string GivenName { get; init; }
    public string Surname { get; init; }
    public string Email { get; init; }
    public RegionalSettings RegionalSettings { get; init; }
    public bool Enabled { get; init; }
}

public class OtpSignatureDataDto
{
}

public class PAdESSignatureConfig
{
    public bool Enabled { get; init; }
    public string Level { get; init; }
}

public class PageReadConfirmationDto
{
    public string ElementId { get; init; }
    public long PageNumber { get; init; }
    public bool Required { get; init; }
    public long GuidingOrder { get; init; }
}

public class PageReadConfirmationField
{
    public string FieldType { get; init; }
    public string Id { get; init; }
    public long Page { get; init; }
    public double PositionX { get; init; }
    public double PositionY { get; init; }
    public double Width { get; init; }
    public double Height { get; init; }
    public bool Required { get; init; }
    public string Source { get; init; }
}

public class PageReadConfirmationFieldDto
{
    public string Id { get; init; }
    public long Page { get; init; }
    public bool Required { get; init; }
    public string FieldType { get; init; }
}

public class PageReadConfirmationTaskUpdateRequest
{
    public string FieldType { get; init; }
}

public class PaginatedRoles
{
    public List<Anonymous65> Roles { get; init; }
    public Pagination Pagination { get; init; }
}

public class Pagination
{
    public long Page { get; init; }
    public long PageSize { get; init; }
    public long TotalCount { get; init; }
}

public class Pagination
{
    public long Page { get; init; }
    public long PageSize { get; init; }
    public long TotalCount { get; init; }
}

public class Pagination
{
    public long Page { get; init; }
    public long PageSize { get; init; }
    public long TotalCount { get; init; }
}

public class Pagination
{
    public long Page { get; init; }
    public long PageSize { get; init; }
    public long TotalCount { get; init; }
}

public class Pagination
{
    public long Page { get; init; }
    public long PageSize { get; init; }
    public long TotalCount { get; init; }
}

public class Pagination
{
    public long Page { get; init; }
    public long PageSize { get; init; }
    public long TotalCount { get; init; }
}

public class Pagination
{
    public long Page { get; init; }
    public long PageSize { get; init; }
    public long TotalCount { get; init; }
}

public class Pagination
{
    public long Page { get; init; }
    public long PageSize { get; init; }
    public long TotalCount { get; init; }
}

public class Pagination
{
    public long Page { get; init; }
    public long PageSize { get; init; }
    public long TotalCount { get; init; }
}

public class Pagination
{
    public long Page { get; init; }
    public long PageSize { get; init; }
    public long TotalCount { get; init; }
}

public class Pagination
{
    public long Page { get; init; }
    public long PageSize { get; init; }
    public long TotalCount { get; init; }
}

public class Pagination
{
    public long Page { get; init; }
    public long PageSize { get; init; }
    public long TotalCount { get; init; }
}

public class Pagination
{
    public long Page { get; init; }
    public long PageSize { get; init; }
    public long TotalCount { get; init; }
}

public class Pagination
{
    public long Page { get; init; }
    public long PageSize { get; init; }
    public long TotalCount { get; init; }
}

public class Pagination
{
    public long Page { get; init; }
    public long PageSize { get; init; }
    public long TotalCount { get; init; }
}

public class Pagination
{
    public long Page { get; init; }
    public long PageSize { get; init; }
    public long TotalCount { get; init; }
}

public class PaginationDto
{
    public long Page { get; init; }
    public long PageSize { get; init; }
    public long TotalCount { get; init; }
}

public class PaginationResponse
{
    public long Page { get; init; }
    public long PageSize { get; init; }
    public long TotalCount { get; init; }
}

public class ParseBulkRecipientsResponse
{
    public List<Anonymous66> BulkRecipients { get; init; }
}

public class PdfDocumentSettingsDto
{
    public bool AllowSigningOfLockedPdfDocuments { get; init; }
}

public class PermissionDto
{
    public string Entity { get; init; }
    public string Action { get; init; }
}

public class Permissions
{
    public Users Users { get; init; }
    public Envelopes Envelopes { get; init; }
    public Templates Templates { get; init; }
    public Contacts Contacts { get; init; }
}

public class Permissions
{
    public Users Users { get; init; }
    public Envelopes Envelopes { get; init; }
    public Templates Templates { get; init; }
    public Contacts Contacts { get; init; }
}

public class Permissions
{
    public Users Users { get; init; }
    public Envelopes Envelopes { get; init; }
    public Templates Templates { get; init; }
    public Contacts Contacts { get; init; }
}

public class PermissionsDto
{
    public Envelopes Envelopes { get; init; }
    public Templates Templates { get; init; }
    public UserGroups UserGroups { get; init; }
    public OrganizationSettings OrganizationSettings { get; init; }
    public Users Users { get; init; }
    public Roles Roles { get; init; }
    public AutomaticESealing AutomaticESealing { get; init; }
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
    public List<Anonymous67> PersonalAccessTokens { get; init; }
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
    public string SignatureType { get; init; }
}

public class PluginStampImprintDto
{
    public bool DisplayName { get; init; }
    public bool DisplaySignatureDate { get; init; }
    public bool DisplayExtraInformation { get; init; }
    public bool DisplayEmail { get; init; }
    public bool DisplayIp { get; init; }
}

public class PoliciesResponse
{
    public List<Anonymous68> Policies { get; init; }
    public Pagination Pagination { get; init; }
}

public class PolicyActionDto
{
    public long SortOrder { get; init; }
    public string Type { get; init; }
    public Stage Stage { get; init; }
    public RecipientSource RecipientSource { get; init; }
}

public class PolicyConditionDto
{
    public string Id { get; init; }
    public string MetadataId { get; init; }
    public string Operator { get; init; }
    public string Value { get; init; }
    public long SortOrder { get; init; }
}

public class PolicyConditionRequest
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
}

public class PolicyListItemResponse
{
    public string Id { get; init; }
    public string Name { get; init; }
    public bool IsActive { get; init; }
}

public class PolicyRecipientDto
{
    public string GivenName { get; init; }
    public string Surname { get; init; }
    public string Email { get; init; }
}

public class PolicyRecipientSourceDto
{
    public string Type { get; init; }
}

public class Position
{
    public long PageNumber { get; init; }
    public double X { get; init; }
    public double Y { get; init; }
}

public class Position
{
    public long PageNumber { get; init; }
    public double X { get; init; }
    public double Y { get; init; }
}

public class Position
{
    public long PageNumber { get; init; }
    public double X { get; init; }
    public double Y { get; init; }
}

public class Position
{
    public long PageNumber { get; init; }
    public double X { get; init; }
    public double Y { get; init; }
}

public class Position
{
    public long PageNumber { get; init; }
    public double X { get; init; }
    public double Y { get; init; }
}

public class Position
{
    public long PageNumber { get; init; }
    public double X { get; init; }
    public double Y { get; init; }
}

public class Position
{
    public long PageNumber { get; init; }
    public double X { get; init; }
    public double Y { get; init; }
}

public class Position
{
    public long PageNumber { get; init; }
    public double X { get; init; }
    public double Y { get; init; }
}

public class Position
{
    public long PageNumber { get; init; }
    public double X { get; init; }
    public double Y { get; init; }
}

public class Position
{
    public long PageNumber { get; init; }
    public double X { get; init; }
    public double Y { get; init; }
}

public class Position
{
    public long PageNumber { get; init; }
    public double X { get; init; }
    public double Y { get; init; }
}

public class Position
{
    public long PageNumber { get; init; }
    public double X { get; init; }
    public double Y { get; init; }
}

public class Position
{
    public long PageNumber { get; init; }
    public double X { get; init; }
    public double Y { get; init; }
}

public class Position
{
    public long PageNumber { get; init; }
    public double X { get; init; }
    public double Y { get; init; }
}

public class Position
{
    public long PageNumber { get; init; }
    public double X { get; init; }
    public double Y { get; init; }
}

public class Position
{
    public long PageNumber { get; init; }
    public double X { get; init; }
    public double Y { get; init; }
}

public class Position
{
    public long PageNumber { get; init; }
    public double X { get; init; }
    public double Y { get; init; }
}

public class Position
{
    public long PageNumber { get; init; }
    public double X { get; init; }
    public double Y { get; init; }
}

public class Position
{
    public long PageNumber { get; init; }
    public double X { get; init; }
    public double Y { get; init; }
}

public class Position
{
    public long PageNumber { get; init; }
    public double X { get; init; }
    public double Y { get; init; }
}

public class Position
{
    public long PageNumber { get; init; }
    public double X { get; init; }
    public double Y { get; init; }
}

public class Position
{
    public long PageNumber { get; init; }
    public double X { get; init; }
    public double Y { get; init; }
}

public class Position
{
    public long PageNumber { get; init; }
    public double X { get; init; }
    public double Y { get; init; }
}

public class Position
{
    public long PageNumber { get; init; }
    public double X { get; init; }
    public double Y { get; init; }
}

public class Position
{
    public long PageNumber { get; init; }
    public double X { get; init; }
    public double Y { get; init; }
}

public class Position
{
    public long PageNumber { get; init; }
    public double X { get; init; }
    public double Y { get; init; }
}

public class Position
{
    public long PageNumber { get; init; }
    public double X { get; init; }
    public double Y { get; init; }
}

public class Position
{
    public long PageNumber { get; init; }
    public double X { get; init; }
    public double Y { get; init; }
}

public class Position
{
    public long PageNumber { get; init; }
    public double X { get; init; }
    public double Y { get; init; }
}

public class Position
{
    public long PageNumber { get; init; }
    public double X { get; init; }
    public double Y { get; init; }
}

public class Position
{
    public long PageNumber { get; init; }
    public double X { get; init; }
    public double Y { get; init; }
}

public class Position
{
    public long PageNumber { get; init; }
    public double X { get; init; }
    public double Y { get; init; }
}

public class ProblemDetails
{
}

public class PutFileDetailRequest
{
}

public class RadioButtonElementDefinition
{
    public Position Position { get; init; }
    public Size Size { get; init; }
    public bool ReadOnly { get; init; }
}

public class RadioButtonElementDto
{
    public string ElementId { get; init; }
    public ElementDefinition ElementDefinition { get; init; }
    public string Source { get; init; }
    public string GroupName { get; init; }
    public bool IsChecked { get; init; }
    public bool IsSelectInUnison { get; init; }
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
    public double PositionX { get; init; }
    public double PositionY { get; init; }
    public bool ReadOnly { get; init; }
    public bool Checked { get; init; }
    public string Value { get; init; }
    public string FieldType { get; init; }
    public double Width { get; init; }
    public double Height { get; init; }
    public bool Required { get; init; }
    public string Source { get; init; }
}

public class RadioButtonFieldDto
{
    public string Id { get; init; }
    public long Page { get; init; }
    public double PositionX { get; init; }
    public double PositionY { get; init; }
    public double Width { get; init; }
    public double Height { get; init; }
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

public class Range
{
}

public class Range
{
}

public class Range
{
}

public class Recipient
{
    public string Id { get; init; }
    public string GivenName { get; init; }
    public string Surname { get; init; }
    public string Type { get; init; }
}

public class Recipient
{
    public string Id { get; init; }
    public string GivenName { get; init; }
    public string Surname { get; init; }
    public string Type { get; init; }
}

public class RecipientAuthenticationDto
{
}

public class RecipientAuthenticationSettingItemResponse
{
    public string Name { get; init; }
    public bool IsEnabled { get; init; }
}

public class RecipientAuthenticationSettingsResponse
{
    public List<Anonymous69> Settings { get; init; }
}

public class RecipientAuthenticationTypes
{
}

public class RecipientDto
{
    public string Id { get; init; }
    public bool IsP7mSigner { get; init; }
    public long Order { get; init; }
    public bool IsDelegationEnabled { get; init; }
}

public class RecipientGeneralPoliciesOverridesDto
{
    public bool AllowSaveDocument { get; init; }
    public bool AllowSaveAuditTrail { get; init; }
    public bool AllowPrintDocument { get; init; }
    public bool AllowAdhocPdfAttachments { get; init; }
    public bool AllowRejectWorkstep { get; init; }
    public bool AllowUndoLastAction { get; init; }
}

public class RecipientMetadataEntry
{
    public string Name { get; init; }
    public string Value { get; init; }
}

public class RecipientSignatureDataDto
{
}

public class RecipientSource
{
    public string Type { get; init; }
}

public class RegionalSettings
{
    public string TimeZone { get; init; }
    public string DateTimeFormat { get; init; }
    public string Language { get; init; }
    public string Country { get; init; }
}

public class RegionalSettings
{
    public string WorldTimeZone { get; init; }
    public long DateTimeFormatId { get; init; }
    public string UiLanguage { get; init; }
    public long CountryId { get; init; }
}

public class RegionalSettings
{
    public string TimeZone { get; init; }
    public string Language { get; init; }
    public string Country { get; init; }
    public string DateTimeFormat { get; init; }
}

public class RegionalSettings
{
    public string TimeZone { get; init; }
    public string Language { get; init; }
    public string Country { get; init; }
    public string DateTimeFormat { get; init; }
}

public class RegionalSettings
{
    public string TimeZone { get; init; }
    public string Language { get; init; }
    public string Country { get; init; }
    public string DateTimeFormat { get; init; }
}

public class RegionalSettings
{
    public string TimeZone { get; init; }
    public string Language { get; init; }
    public string Country { get; init; }
    public string DateTimeFormat { get; init; }
}

public class RegionalSettingsDto
{
    public string Id { get; init; }
    public string WorldTimeZone { get; init; }
    public long DateTimeFormatId { get; init; }
    public string UiLanguage { get; init; }
    public long CountryId { get; init; }
}

public class Rejected
{
    public long EnvelopeCount { get; init; }
}

public class RelativeIntegrationExpirationDto
{
    public string Mode { get; init; }
}

public class Reminder
{
    public bool Enabled { get; init; }
    public long FirstReminderInDays { get; init; }
    public long ResendIntervalInDays { get; init; }
    public long BeforeExpirationInDays { get; init; }
}

public class Reminder
{
    public bool Enabled { get; init; }
    public long FirstReminderInDays { get; init; }
    public long ResendIntervalInDays { get; init; }
    public long BeforeExpirationInDays { get; init; }
}

public class Reminder
{
    public bool Enabled { get; init; }
    public long FirstReminderInDays { get; init; }
    public long ResendIntervalInDays { get; init; }
    public long BeforeExpirationInDays { get; init; }
}

public class ReminderConfiguration
{
    public bool Enabled { get; init; }
    public long FirstReminderInDays { get; init; }
    public long ReminderResendIntervalInDays { get; init; }
    public long BeforeExpirationInDays { get; init; }
}

public class ReminderConfiguration
{
    public bool Enabled { get; init; }
    public long FirstReminderInDays { get; init; }
    public long ReminderResendIntervalInDays { get; init; }
    public long BeforeExpirationInDays { get; init; }
}

public class ReminderConfiguration
{
    public bool Enabled { get; init; }
    public long FirstReminderInDays { get; init; }
    public long ReminderResendIntervalInDays { get; init; }
    public long BeforeExpirationInDays { get; init; }
}

public class ReminderConfiguration
{
    public bool Enabled { get; init; }
    public long FirstReminderInDays { get; init; }
    public long ReminderResendIntervalInDays { get; init; }
    public long BeforeExpirationInDays { get; init; }
}

public class ReminderConfiguration
{
}

public class ReminderConfiguration
{
}

public class ReminderConfiguration
{
}

public class ReminderConfigurationDto
{
    public bool Enabled { get; init; }
    public long FirstReminderInDays { get; init; }
    public long ReminderResendIntervalInDays { get; init; }
    public long BeforeExpirationInDays { get; init; }
}

public class RemoteCertificateDto
{
}

public class RemoteCertificateEnvelopeBulkSignDto
{
    public List<string> EnvelopeIds { get; init; }
    public string SignatureType { get; init; }
    public string CertificateUserId { get; init; }
    public string DevicePassword { get; init; }
    public string Otp { get; init; }
}

public class RemoteCertificateSignature
{
    public string SignatureType { get; init; }
}

public class RemoteCertificateSignatureTypeDto
{
}

public class RemoteCertificateStampImprintDto
{
    public bool DisplayName { get; init; }
    public bool DisplaySignatureDate { get; init; }
    public bool DisplayExtraInformation { get; init; }
    public bool DisplayIp { get; init; }
}

public class ReplacedEnvelopeFileResponse
{
    public string Id { get; init; }
    public long OrderIndex { get; init; }
}

public class ReplacedTemplateFileResponse
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
    public Data Data { get; init; }
    public List<string> EnvelopeIds { get; init; }
}

public class RoleDetailsDto
{
    public string Id { get; init; }
    public string Name { get; init; }
    public List<Anonymous70> Permissions { get; init; }
    public string CreatedAt { get; init; }
    public bool IsSystemRole { get; init; }
}

public class RoleDto
{
    public string Id { get; init; }
    public string Name { get; init; }
    public bool IsSystemRole { get; init; }
}

public class Roles
{
    public bool Read { get; init; }
    public bool CreateUpdateDelete { get; init; }
    public bool Assign { get; init; }
}

public class Roles
{
    public bool Read { get; init; }
    public bool CreateUpdateDelete { get; init; }
    public bool Assign { get; init; }
}

public class RolesSettings
{
    public bool Read { get; init; }
    public bool CreateUpdateDelete { get; init; }
    public bool Assign { get; init; }
}

public class Root
{
    public ATrustCertificateDto ATrustCertificateDto { get; init; }
    public ATrustCertificateSignatureTypeDto ATrustCertificateSignatureTypeDto { get; init; }
    public AbsoluteIntegrationExpirationDto AbsoluteIntegrationExpirationDto { get; init; }
    public AccessCode AccessCode { get; init; }
    public string Action { get; init; }
    public AddDefaultUserGroupDto AddDefaultUserGroupDto { get; init; }
    public AddUserGroupUserDto AddUserGroupUserDto { get; init; }
    public AddUsersToUserGroupDto AddUsersToUserGroupDto { get; init; }
    public AddedEnvelopeFileResponse AddedEnvelopeFileResponse { get; init; }
    public AddedTemplateFileResponse AddedTemplateFileResponse { get; init; }
    public AdminMeDto AdminMeDto { get; init; }
    public AdminMeUserDto AdminMeUserDto { get; init; }
    public Agreement Agreement { get; init; }
    public AgreementRequest AgreementRequest { get; init; }
    public AgreementResponse AgreementResponse { get; init; }
    public AgreementSettingsRequest AgreementSettingsRequest { get; init; }
    public AgreementSettingsResponse AgreementSettingsResponse { get; init; }
    public AllowedSignatureTypesDto AllowedSignatureTypesDto { get; init; }
    public AnnotationElementDefinition AnnotationElementDefinition { get; init; }
    public AnnotationElementDto AnnotationElementDto { get; init; }
    public AnnotationField AnnotationField { get; init; }
    public AnnotationFieldDto AnnotationFieldDto { get; init; }
    public string AnnotationType { get; init; }
    public string AnnotationValueFormat { get; init; }
    public ApprovalField ApprovalField { get; init; }
    public ApprovalFieldDto ApprovalFieldDto { get; init; }
    public ApproveElementDto ApproveElementDto { get; init; }
    public AreaReadConfirmationDto AreaReadConfirmationDto { get; init; }
    public AreaReadConfirmationField AreaReadConfirmationField { get; init; }
    public AreaReadConfirmationFieldDto AreaReadConfirmationFieldDto { get; init; }
    public AreaReadConfirmationTaskUpdateRequest AreaReadConfirmationTaskUpdateRequest { get; init; }
    public AreaReadElementDefinition AreaReadElementDefinition { get; init; }
    public AssociateMyNamirialIdDto AssociateMyNamirialIdDto { get; init; }
    public AttachmentElementDefinition AttachmentElementDefinition { get; init; }
    public AttachmentElementDto AttachmentElementDto { get; init; }
    public AttachmentField AttachmentField { get; init; }
    public AttachmentFieldDto AttachmentFieldDto { get; init; }
    public AttachmentTaskUpdateRequest AttachmentTaskUpdateRequest { get; init; }
    public AuditTrailModeResponse AuditTrailModeResponse { get; init; }
    public AutomaticESealingPermissions AutomaticESealingPermissions { get; init; }
    public AutomaticSealingProfileDetailResponse AutomaticSealingProfileDetailResponse { get; init; }
    public AutomaticSealingProfileRequest AutomaticSealingProfileRequest { get; init; }
    public AutomaticSealingProfileResponse AutomaticSealingProfileResponse { get; init; }
    public AutomaticSignature AutomaticSignature { get; init; }
    public AutomaticSignatureDataDto AutomaticSignatureDataDto { get; init; }
    public AutomaticSignatureTypeDto AutomaticSignatureTypeDto { get; init; }
    public BackgroundImageDto BackgroundImageDto { get; init; }
    public BankIdSettingsDto BankIdSettingsDto { get; init; }
    public BaseField BaseField { get; init; }
    public BaseFieldDto BaseFieldDto { get; init; }
    public BatchAssignUserGroupUserRoleDto BatchAssignUserGroupUserRoleDto { get; init; }
    public BatchDeleteUserGroupUserRoleDto BatchDeleteUserGroupUserRoleDto { get; init; }
    public string BatchMode { get; init; }
    public BiometricSignature BiometricSignature { get; init; }
    public string BiometricSignaturePositioning { get; init; }
    public BiometricSignatureTypeDto BiometricSignatureTypeDto { get; init; }
    public BulkEnvelopeDetailDto BulkEnvelopeDetailDto { get; init; }
    public BulkEnvelopeFieldTaskItem BulkEnvelopeFieldTaskItem { get; init; }
    public BulkEnvelopeFieldTaskItemRequest BulkEnvelopeFieldTaskItemRequest { get; init; }
    public BulkEnvelopeFileTasksResponse BulkEnvelopeFileTasksResponse { get; init; }
    public BulkEnvelopeListDto BulkEnvelopeListDto { get; init; }
    public BulkEnvelopePartialDto BulkEnvelopePartialDto { get; init; }
    public BulkRecipientDefinition BulkRecipientDefinition { get; init; }
    public BulkRecipientDto BulkRecipientDto { get; init; }
    public BulkRecipientValidationErrorResponse BulkRecipientValidationErrorResponse { get; init; }
    public BulkStageDto BulkStageDto { get; init; }
    public BusinessRoleCreateDto BusinessRoleCreateDto { get; init; }
    public BusinessRoleDto BusinessRoleDto { get; init; }
    public BusinessRoleUpdateDto BusinessRoleUpdateDto { get; init; }
    public BusinessRolesListDto BusinessRolesListDto { get; init; }
    public string BusinessRolesSortingKey { get; init; }
    public CallbackConfigurationDto CallbackConfigurationDto { get; init; }
    public CertificateDetailsResponse CertificateDetailsResponse { get; init; }
    public CheckBoxElementDefinition CheckBoxElementDefinition { get; init; }
    public CheckBoxElementDto CheckBoxElementDto { get; init; }
    public CheckboxField CheckboxField { get; init; }
    public CheckboxFieldDto CheckboxFieldDto { get; init; }
    public CheckboxTaskUpdateRequest CheckboxTaskUpdateRequest { get; init; }
    public ClickToSignEnvelopeBulkSignDto ClickToSignEnvelopeBulkSignDto { get; init; }
    public ClickToSignSignature ClickToSignSignature { get; init; }
    public ClickToSignSignatureTypeDto ClickToSignSignatureTypeDto { get; init; }
    public ClickToSignStampImprintDto ClickToSignStampImprintDto { get; init; }
    public ClonedEnvelopeDto ClonedEnvelopeDto { get; init; }
    public ContactDto ContactDto { get; init; }
    public ContactImportResultDto ContactImportResultDto { get; init; }
    public ContactImportValidationErrorResponse ContactImportValidationErrorResponse { get; init; }
    public ContactListDto ContactListDto { get; init; }
    public ContactRequest ContactRequest { get; init; }
    public string ContactsSortingKey { get; init; }
    public CountriesDto CountriesDto { get; init; }
    public CountriesLookupResponse CountriesLookupResponse { get; init; }
    public CountryDto CountryDto { get; init; }
    public CountryListItemDto CountryListItemDto { get; init; }
    public CreateATrustCertificateDto CreateATrustCertificateDto { get; init; }
    public CreateAccessCodeDto CreateAccessCodeDto { get; init; }
    public CreateAuthenticationConfigurationDto CreateAuthenticationConfigurationDto { get; init; }
    public CreateAutomaticSignatureDataDto CreateAutomaticSignatureDataDto { get; init; }
    public CreateBulkEnvelopeStageRequest CreateBulkEnvelopeStageRequest { get; init; }
    public CreateDisposableCertificateDto CreateDisposableCertificateDto { get; init; }
    public CreateDocumentClassRequest CreateDocumentClassRequest { get; init; }
    public CreateEnvelopeStageAutomaticRecipientRequest CreateEnvelopeStageAutomaticRecipientRequest { get; init; }
    public CreateEnvelopeStageRecipientRequest CreateEnvelopeStageRecipientRequest { get; init; }
    public CreateEnvelopeStageRequest CreateEnvelopeStageRequest { get; init; }
    public CreateEnvelopeStageStandardRecipientRequest CreateEnvelopeStageStandardRecipientRequest { get; init; }
    public CreateGenericSigningPluginsSenderDataDto CreateGenericSigningPluginsSenderDataDto { get; init; }
    public CreateOAuthAuthenticationDto CreateOAuthAuthenticationDto { get; init; }
    public CreateOAuthFieldDefinitionRequest CreateOAuthFieldDefinitionRequest { get; init; }
    public CreateOAuthJwtConfigRequest CreateOAuthJwtConfigRequest { get; init; }
    public CreateOAuthResourceUriRequest CreateOAuthResourceUriRequest { get; init; }
    public CreateOAuthSignerProviderDetailsRequest CreateOAuthSignerProviderDetailsRequest { get; init; }
    public CreateOAuthSignerProviderRequest CreateOAuthSignerProviderRequest { get; init; }
    public CreateOrganizationDto CreateOrganizationDto { get; init; }
    public CreateOrganizationUserRequestDto CreateOrganizationUserRequestDto { get; init; }
    public CreateOrganizationUserResponse CreateOrganizationUserResponse { get; init; }
    public CreateOtpSignatureDataDto CreateOtpSignatureDataDto { get; init; }
    public CreatePersonalAccessTokenRequest CreatePersonalAccessTokenRequest { get; init; }
    public CreatePolicyRequest CreatePolicyRequest { get; init; }
    public CreateRemoteCertificateDto CreateRemoteCertificateDto { get; init; }
    public CreateRoleRequest CreateRoleRequest { get; init; }
    public CreateSenderGenericSigningPluginDto CreateSenderGenericSigningPluginDto { get; init; }
    public CreateSenderGenericSigningPluginSettingsDto CreateSenderGenericSigningPluginSettingsDto { get; init; }
    public CreateServiceAccountRequest CreateServiceAccountRequest { get; init; }
    public CreateServiceAccountResponse CreateServiceAccountResponse { get; init; }
    public CreateSignatureDataConfigurationDto CreateSignatureDataConfigurationDto { get; init; }
    public CreateSmsOneTimePasswordDto CreateSmsOneTimePasswordDto { get; init; }
    public CreateStageResponse CreateStageResponse { get; init; }
    public CreateSubstituteDelegationDto CreateSubstituteDelegationDto { get; init; }
    public CreateSwedishBankIdDto CreateSwedishBankIdDto { get; init; }
    public CreateSwissComOnDemandDto CreateSwissComOnDemandDto { get; init; }
    public CreateTemplateStageAutomaticRecipientRequest CreateTemplateStageAutomaticRecipientRequest { get; init; }
    public CreateTemplateStageRecipientRequest CreateTemplateStageRecipientRequest { get; init; }
    public CreateTemplateStageRequest CreateTemplateStageRequest { get; init; }
    public CreateTemplateStageStandardRecipientRequest CreateTemplateStageStandardRecipientRequest { get; init; }
    public CreateUserDto CreateUserDto { get; init; }
    public CreatedDocumentClassDto CreatedDocumentClassDto { get; init; }
    public CreatedEnvelopeDto CreatedEnvelopeDto { get; init; }
    public CreatedEnvelopeFromTemplateDto CreatedEnvelopeFromTemplateDto { get; init; }
    public CreatedOrganizationDto CreatedOrganizationDto { get; init; }
    public CreatedPersonalAccessTokenResponse CreatedPersonalAccessTokenResponse { get; init; }
    public CreatedPolicyResponse CreatedPolicyResponse { get; init; }
    public CreatedRecipientResponse CreatedRecipientResponse { get; init; }
    public CreatedStageResponse CreatedStageResponse { get; init; }
    public CreatedTemplateDto CreatedTemplateDto { get; init; }
    public CreatedTemplateStageRecipientDto CreatedTemplateStageRecipientDto { get; init; }
    public CreatedUserDto CreatedUserDto { get; init; }
    public string DataFieldType { get; init; }
    public DateAnnotationConfigDto DateAnnotationConfigDto { get; init; }
    public string DateFormatSwaggerEnumProvider { get; init; }
    public DateInputConfig DateInputConfig { get; init; }
    public DateTimeDefinition DateTimeDefinition { get; init; }
    public DateTimeFormatDto DateTimeFormatDto { get; init; }
    public string DateTimeFormatSwaggerEnumProvider { get; init; }
    public DateTimeFormatsDto DateTimeFormatsDto { get; init; }
    public DateTimeFormatsLookupResponse DateTimeFormatsLookupResponse { get; init; }
    public DateTimeOptionDto DateTimeOptionDto { get; init; }
    public string DbEnvelopeStatus { get; init; }
    public string DbRecipientType { get; init; }
    public string DbWorkstepResult { get; init; }
    public string DecimalSeparatorType { get; init; }
    public DefaultUserGroupsDto DefaultUserGroupsDto { get; init; }
    public DelegationInfo DelegationInfo { get; init; }
    public string DelegationPolicy { get; init; }
    public DisposableCertificateDto DisposableCertificateDto { get; init; }
    public DisposableCertificateSettingsDto DisposableCertificateSettingsDto { get; init; }
    public DisposableCertificateSignature DisposableCertificateSignature { get; init; }
    public DisposableCertificateSignatureTypeDto DisposableCertificateSignatureTypeDto { get; init; }
    public DisposableCertificateStampImprintDto DisposableCertificateStampImprintDto { get; init; }
    public string DisposableType { get; init; }
    public Document Document { get; init; }
    public DocumentClassDto DocumentClassDto { get; init; }
    public DocumentClassListItemDto DocumentClassListItemDto { get; init; }
    public DocumentClassLookupResponse DocumentClassLookupResponse { get; init; }
    public DocumentClassMetadataDto DocumentClassMetadataDto { get; init; }
    public DocumentClassMetadataFieldDto DocumentClassMetadataFieldDto { get; init; }
    public DocumentClassesResponse DocumentClassesResponse { get; init; }
    public string DocumentClassesSortingKey { get; init; }
    public DocumentReadConfirmationDto DocumentReadConfirmationDto { get; init; }
    public DocumentsUploadRequest DocumentsUploadRequest { get; init; }
    public DrawToSignSignature DrawToSignSignature { get; init; }
    public DrawToSignSignatureTypeDto DrawToSignSignatureTypeDto { get; init; }
    public DrawToSignStampImprintDto DrawToSignStampImprintDto { get; init; }
    public DropDownElementDefinition DropDownElementDefinition { get; init; }
    public DropDownElementDto DropDownElementDto { get; init; }
    public DropDownField DropDownField { get; init; }
    public DropDownFieldDto DropDownFieldDto { get; init; }
    public DropDownItemEntry DropDownItemEntry { get; init; }
    public DropDownTaskUpdateRequest DropDownTaskUpdateRequest { get; init; }
    public ESealingRemoteSignatureProfileDto ESealingRemoteSignatureProfileDto { get; init; }
    public string ElementSource { get; init; }
    public EmailAnnotationConfigDto EmailAnnotationConfigDto { get; init; }
    public EmailDefinition EmailDefinition { get; init; }
    public string EmailSenderDisplayType { get; init; }
    public EnableOrganizationDto EnableOrganizationDto { get; init; }
    public EnabledOrganizationDto EnabledOrganizationDto { get; init; }
    public string Entity { get; init; }
    public string EnvelopeAction { get; init; }
    public EnvelopeActionResponse EnvelopeActionResponse { get; init; }
    public EnvelopeActorDto EnvelopeActorDto { get; init; }
    public EnvelopeBacklogDto EnvelopeBacklogDto { get; init; }
    public EnvelopeBulkSignDeviceDto EnvelopeBulkSignDeviceDto { get; init; }
    public EnvelopeBulkSignDevicesResponseDto EnvelopeBulkSignDevicesResponseDto { get; init; }
    public EnvelopeBulkSignDto EnvelopeBulkSignDto { get; init; }
    public EnvelopeBulkSignResultDto EnvelopeBulkSignResultDto { get; init; }
    public string EnvelopeBulkSignSignatureType { get; init; }
    public EnvelopeBulkSignTransactionDto EnvelopeBulkSignTransactionDto { get; init; }
    public EnvelopeCancelRequestDto EnvelopeCancelRequestDto { get; init; }
    public EnvelopeDetailDto EnvelopeDetailDto { get; init; }
    public EnvelopeDetailRecipientDto EnvelopeDetailRecipientDto { get; init; }
    public EnvelopeDetailStageDto EnvelopeDetailStageDto { get; init; }
    public string EnvelopeDetailStatus { get; init; }
    public EnvelopeDownloadDto EnvelopeDownloadDto { get; init; }
    public EnvelopeDownloadsResponse EnvelopeDownloadsResponse { get; init; }
    public EnvelopeDto EnvelopeDto { get; init; }
    public EnvelopeEventDto EnvelopeEventDto { get; init; }
    public string EnvelopeEventType { get; init; }
    public EnvelopeEventsDto EnvelopeEventsDto { get; init; }
    public EnvelopeFieldTaskItem EnvelopeFieldTaskItem { get; init; }
    public EnvelopeFileDetailDocumentClassDto EnvelopeFileDetailDocumentClassDto { get; init; }
    public EnvelopeFileDetailDocumentClassRequest EnvelopeFileDetailDocumentClassRequest { get; init; }
    public EnvelopeFileMetadataValueDto EnvelopeFileMetadataValueDto { get; init; }
    public EnvelopeFileTasksResponse EnvelopeFileTasksResponse { get; init; }
    public EnvelopeFilesResponse EnvelopeFilesResponse { get; init; }
    public EnvelopeInsights EnvelopeInsights { get; init; }
    public EnvelopeListDto EnvelopeListDto { get; init; }
    public string EnvelopeLogGeneration { get; init; }
    public EnvelopePartialDto EnvelopePartialDto { get; init; }
    public EnvelopePermissions EnvelopePermissions { get; init; }
    public EnvelopePoliciesVerifyDto EnvelopePoliciesVerifyDto { get; init; }
    public EnvelopeRejectDto EnvelopeRejectDto { get; init; }
    public EnvelopeResumeDto EnvelopeResumeDto { get; init; }
    public EnvelopeSenderDto EnvelopeSenderDto { get; init; }
    public EnvelopeSignatureTypeDto EnvelopeSignatureTypeDto { get; init; }
    public EnvelopeSignatureTypesRequestDto EnvelopeSignatureTypesRequestDto { get; init; }
    public string EnvelopeSortingKey { get; init; }
    public EnvelopeStageAutomaticRecipientResponse EnvelopeStageAutomaticRecipientResponse { get; init; }
    public EnvelopeStageAutomaticRecipientSummaryDto EnvelopeStageAutomaticRecipientSummaryDto { get; init; }
    public EnvelopeStageItemDto EnvelopeStageItemDto { get; init; }
    public EnvelopeStageListDto EnvelopeStageListDto { get; init; }
    public EnvelopeStageRecipientResponse EnvelopeStageRecipientResponse { get; init; }
    public EnvelopeStageRecipientSummaryDto EnvelopeStageRecipientSummaryDto { get; init; }
    public EnvelopeStageStandardRecipientResponse EnvelopeStageStandardRecipientResponse { get; init; }
    public EnvelopeStageStandardRecipientSummaryDto EnvelopeStageStandardRecipientSummaryDto { get; init; }
    public string EnvelopeStageType { get; init; }
    public string EnvelopeType { get; init; }
    public EnvelopeViewerLinkDto EnvelopeViewerLinkDto { get; init; }
    public string ErrorCode { get; init; }
    public ErrorResult ErrorResult { get; init; }
    public ExpirationConfigurationDto ExpirationConfigurationDto { get; init; }
    public string ExpirationMode { get; init; }
    public string ExternalSignatureImageMode { get; init; }
    public FailedEnvelope FailedEnvelope { get; init; }
    public FieldTask FieldTask { get; init; }
    public FieldTaskItemRequest FieldTaskItemRequest { get; init; }
    public string FieldTaskSignatureType { get; init; }
    public string FieldType { get; init; }
    public string FieldValidationType { get; init; }
    public FileDetailResponse FileDetailResponse { get; init; }
    public FileElementDateValidationConfiguration FileElementDateValidationConfiguration { get; init; }
    public FileElementFieldValidationRange FileElementFieldValidationRange { get; init; }
    public FileElementNumberValidationConfiguration FileElementNumberValidationConfiguration { get; init; }
    public FileElementPhoneValidationConfiguration FileElementPhoneValidationConfiguration { get; init; }
    public FileElementTextFormat FileElementTextFormat { get; init; }
    public FileElementTimeValidationConfiguration FileElementTimeValidationConfiguration { get; init; }
    public FileElementsDto FileElementsDto { get; init; }
    public FileElementsFieldValidation FileElementsFieldValidation { get; init; }
    public FileElementsPosition FileElementsPosition { get; init; }
    public FileElementsSize FileElementsSize { get; init; }
    public FileOrderItem FileOrderItem { get; init; }
    public FileReadConfirmationField FileReadConfirmationField { get; init; }
    public FileReadConfirmationFieldDto FileReadConfirmationFieldDto { get; init; }
    public FileReadConfirmationTaskUpdateRequest FileReadConfirmationTaskUpdateRequest { get; init; }
    public FileTaskItem FileTaskItem { get; init; }
    public FirstNameAnnotationConfigDto FirstNameAnnotationConfigDto { get; init; }
    public FirstNameDefinition FirstNameDefinition { get; init; }
    public FontStyle FontStyle { get; init; }
    public string ForceAuthenticationModeApi { get; init; }
    public ForcedAuthenticationRulesRequest ForcedAuthenticationRulesRequest { get; init; }
    public ForcedAuthenticationRulesResponse ForcedAuthenticationRulesResponse { get; init; }
    public string FormFieldSource { get; init; }
    public FullNameAnnotationConfigDto FullNameAnnotationConfigDto { get; init; }
    public FullNameDefinition FullNameDefinition { get; init; }
    public GeneralSettingsDto GeneralSettingsDto { get; init; }
    public GenericSigningPluginDto GenericSigningPluginDto { get; init; }
    public GenericSigningPluginSenderSettingsDto GenericSigningPluginSenderSettingsDto { get; init; }
    public GenericSigningPluginSettingLabelDto GenericSigningPluginSettingLabelDto { get; init; }
    public GenericSigningPluginsSenderDataDto GenericSigningPluginsSenderDataDto { get; init; }
    public GetOrganizationsListResponse GetOrganizationsListResponse { get; init; }
    public GetUsersListResponse GetUsersListResponse { get; init; }
    public GetUsersResponse GetUsersResponse { get; init; }
    public string GuidingOrderMode { get; init; }
    public HttpValidationProblemDetails HttpValidationProblemDetails { get; init; }
    public string ImagePosition { get; init; }
    public InitialsAnnotationConfigDto InitialsAnnotationConfigDto { get; init; }
    public InitialsDefinition InitialsDefinition { get; init; }
    public IntegrationBulkEnvelopeDto IntegrationBulkEnvelopeDto { get; init; }
    public IntegrationEnvelopeDto IntegrationEnvelopeDto { get; init; }
    public object IntegrationExpirationConfigurationDto { get; init; }
    public IntegrationFileDto IntegrationFileDto { get; init; }
    public IntegrationStageDto IntegrationStageDto { get; init; }
    public IntegrationTemplateDto IntegrationTemplateDto { get; init; }
    public InvisibleSignatureElementDto InvisibleSignatureElementDto { get; init; }
    public InvisibleSignatureField InvisibleSignatureField { get; init; }
    public InvisibleSignatureFieldDto InvisibleSignatureFieldDto { get; init; }
    public LanguageListItemDto LanguageListItemDto { get; init; }
    public LanguageSettingDto LanguageSettingDto { get; init; }
    public LanguageStateRequest LanguageStateRequest { get; init; }
    public LanguagesDto LanguagesDto { get; init; }
    public LanguagesLookupResponse LanguagesLookupResponse { get; init; }
    public LanguagesSettingsResponse LanguagesSettingsResponse { get; init; }
    public LanguagesSettingsUpdateRequest LanguagesSettingsUpdateRequest { get; init; }
    public LastNameAnnotationConfigDto LastNameAnnotationConfigDto { get; init; }
    public LastNameDefinition LastNameDefinition { get; init; }
    public string LastRecipientAction { get; init; }
    public LicenseDto LicenseDto { get; init; }
    public string LicenseType { get; init; }
    public LinkElementDefinition LinkElementDefinition { get; init; }
    public LinkElementDto LinkElementDto { get; init; }
    public LinkField LinkField { get; init; }
    public LinkFieldDto LinkFieldDto { get; init; }
    public ListBoxField ListBoxField { get; init; }
    public ListBoxFieldDto ListBoxFieldDto { get; init; }
    public ListBoxTaskUpdateRequest ListBoxTaskUpdateRequest { get; init; }
    public ListElementDefinition ListElementDefinition { get; init; }
    public ListElementDto ListElementDto { get; init; }
    public ListItemEntry ListItemEntry { get; init; }
    public string LocalCertificateHashAlgorithm { get; init; }
    public LocalCertificateSignature LocalCertificateSignature { get; init; }
    public LocalCertificateSignatureTypeDto LocalCertificateSignatureTypeDto { get; init; }
    public LocalCertificateStampImprintDto LocalCertificateStampImprintDto { get; init; }
    public string MetadataDataType { get; init; }
    public MetadataValueDto MetadataValueDto { get; init; }
    public NamedSignatureAppearanceLayoutDto NamedSignatureAppearanceLayoutDto { get; init; }
    public NamedSignatureAppearanceLayoutRequest NamedSignatureAppearanceLayoutRequest { get; init; }
    public NextRecipientDto NextRecipientDto { get; init; }
    public NextRecipientLinkDto NextRecipientLinkDto { get; init; }
    public NextRecipientLinksResponse NextRecipientLinksResponse { get; init; }
    public string NextRecipientType { get; init; }
    public string NotificationChannel { get; init; }
    public NotificationChannelMessagesDto NotificationChannelMessagesDto { get; init; }
    public NotificationChannelsDto NotificationChannelsDto { get; init; }
    public NotificationMessageDto NotificationMessageDto { get; init; }
    public NotificationPreferencesRequest NotificationPreferencesRequest { get; init; }
    public NotificationPreferencesResponse NotificationPreferencesResponse { get; init; }
    public NotificationSettingsDto NotificationSettingsDto { get; init; }
    public NumberInputConfig NumberInputConfig { get; init; }
    public NumberSymbol NumberSymbol { get; init; }
    public OAuthAuthentication OAuthAuthentication { get; init; }
    public OAuthFieldDefinitionDto OAuthFieldDefinitionDto { get; init; }
    public OAuthFieldReferenceDto OAuthFieldReferenceDto { get; init; }
    public string OAuthFieldTarget { get; init; }
    public OAuthGenericSigningPluginReferenceDto OAuthGenericSigningPluginReferenceDto { get; init; }
    public OAuthJwtConfigDto OAuthJwtConfigDto { get; init; }
    public OAuthResourceUriDto OAuthResourceUriDto { get; init; }
    public OAuthSignerProvider OAuthSignerProvider { get; init; }
    public OAuthSignerProviderDetailsResponse OAuthSignerProviderDetailsResponse { get; init; }
    public OAuthSignerProviderDto OAuthSignerProviderDto { get; init; }
    public string OAuthSignerProviderFieldMode { get; init; }
    public OAuthSignerProviderFieldModeResponse OAuthSignerProviderFieldModeResponse { get; init; }
    public string OAuthSignerProviderFieldTarget { get; init; }
    public OAuthSignerProviderFieldTargetResponse OAuthSignerProviderFieldTargetResponse { get; init; }
    public OAuthSignerProvidersResponse OAuthSignerProvidersResponse { get; init; }
    public string OAuthSignerProvidersSortingKey { get; init; }
    public OneTimePasswordSignature OneTimePasswordSignature { get; init; }
    public OneTimePasswordSignatureTypeDto OneTimePasswordSignatureTypeDto { get; init; }
    public OneTimePasswordStampImprintDto OneTimePasswordStampImprintDto { get; init; }
    public Option Option { get; init; }
    public OptionDto OptionDto { get; init; }
    public OrganizationCustomTimeStampServerSettings OrganizationCustomTimeStampServerSettings { get; init; }
    public OrganizationDefaultSignatureTypeDto OrganizationDefaultSignatureTypeDto { get; init; }
    public OrganizationDelegationSettingsDto OrganizationDelegationSettingsDto { get; init; }
    public OrganizationDetailDto OrganizationDetailDto { get; init; }
    public OrganizationFeatureFlagResponse OrganizationFeatureFlagResponse { get; init; }
    public OrganizationFeatureFlagsResponse OrganizationFeatureFlagsResponse { get; init; }
    public OrganizationGeneralPoliciesDto OrganizationGeneralPoliciesDto { get; init; }
    public OrganizationItemDto OrganizationItemDto { get; init; }
    public OrganizationLanguageLookupDto OrganizationLanguageLookupDto { get; init; }
    public OrganizationPAdESConfiguration OrganizationPAdESConfiguration { get; init; }
    public OrganizationRecipientAuthenticationTypesDto OrganizationRecipientAuthenticationTypesDto { get; init; }
    public OrganizationRecipientOAuthProviderDto OrganizationRecipientOAuthProviderDto { get; init; }
    public OrganizationRecipientSettingsDto OrganizationRecipientSettingsDto { get; init; }
    public OrganizationSettingsPermissions OrganizationSettingsPermissions { get; init; }
    public OrganizationSignatureTypesDto OrganizationSignatureTypesDto { get; init; }
    public OrganizationSummaryDto OrganizationSummaryDto { get; init; }
    public OrganizationUserDto OrganizationUserDto { get; init; }
    public OrganizationUserRegionalSettingsDto OrganizationUserRegionalSettingsDto { get; init; }
    public OrganizationUserSummaryDto OrganizationUserSummaryDto { get; init; }
    public string OtpDeliveryChannel { get; init; }
    public OtpSignatureDataDto OtpSignatureDataDto { get; init; }
    public string PAdESLevel { get; init; }
    public PAdESSignatureConfig PAdESSignatureConfig { get; init; }
    public PageReadConfirmationDto PageReadConfirmationDto { get; init; }
    public PageReadConfirmationField PageReadConfirmationField { get; init; }
    public PageReadConfirmationFieldDto PageReadConfirmationFieldDto { get; init; }
    public PageReadConfirmationTaskUpdateRequest PageReadConfirmationTaskUpdateRequest { get; init; }
    public PaginatedRoles PaginatedRoles { get; init; }
    public Pagination Pagination { get; init; }
    public PaginationDto PaginationDto { get; init; }
    public PaginationResponse PaginationResponse { get; init; }
    public ParseBulkRecipientsResponse ParseBulkRecipientsResponse { get; init; }
    public PdfDocumentSettingsDto PdfDocumentSettingsDto { get; init; }
    public PermissionDto PermissionDto { get; init; }
    public PermissionsDto PermissionsDto { get; init; }
    public PersonalAccessTokenListItemResponse PersonalAccessTokenListItemResponse { get; init; }
    public PersonalAccessTokenListResponse PersonalAccessTokenListResponse { get; init; }
    public PhoneNumberInputConfig PhoneNumberInputConfig { get; init; }
    public string PhoneType { get; init; }
    public PluginSignature PluginSignature { get; init; }
    public PluginStampImprintDto PluginStampImprintDto { get; init; }
    public PoliciesResponse PoliciesResponse { get; init; }
    public string PoliciesSortingKey { get; init; }
    public PolicyActionDto PolicyActionDto { get; init; }
    public string PolicyActionType { get; init; }
    public PolicyConditionDto PolicyConditionDto { get; init; }
    public string PolicyConditionOperator { get; init; }
    public PolicyConditionRequest PolicyConditionRequest { get; init; }
    public PolicyDto PolicyDto { get; init; }
    public PolicyListItemResponse PolicyListItemResponse { get; init; }
    public PolicyRecipientDto PolicyRecipientDto { get; init; }
    public PolicyRecipientSourceDto PolicyRecipientSourceDto { get; init; }
    public string PolicyRecipientSourceType { get; init; }
    public string PredefinedSenderDataField { get; init; }
    public ProblemDetails ProblemDetails { get; init; }
    public PutFileDetailRequest PutFileDetailRequest { get; init; }
    public RadioButtonElementDefinition RadioButtonElementDefinition { get; init; }
    public RadioButtonElementDto RadioButtonElementDto { get; init; }
    public RadioButtonField RadioButtonField { get; init; }
    public RadioButtonFieldDto RadioButtonFieldDto { get; init; }
    public RadioButtonTaskUpdateRequest RadioButtonTaskUpdateRequest { get; init; }
    public RecipientAuthenticationDto RecipientAuthenticationDto { get; init; }
    public RecipientAuthenticationSettingItemResponse RecipientAuthenticationSettingItemResponse { get; init; }
    public RecipientAuthenticationSettingsResponse RecipientAuthenticationSettingsResponse { get; init; }
    public string RecipientAuthenticationTypes { get; init; }
    public string RecipientDiscriminator { get; init; }
    public RecipientDto RecipientDto { get; init; }
    public RecipientGeneralPoliciesOverridesDto RecipientGeneralPoliciesOverridesDto { get; init; }
    public RecipientMetadataEntry RecipientMetadataEntry { get; init; }
    public RecipientSignatureDataDto RecipientSignatureDataDto { get; init; }
    public string RecipientStatus { get; init; }
    public string RecipientType { get; init; }
    public RegionalSettingsDto RegionalSettingsDto { get; init; }
    public RelativeIntegrationExpirationDto RelativeIntegrationExpirationDto { get; init; }
    public ReminderConfigurationDto ReminderConfigurationDto { get; init; }
    public RemoteCertificateDto RemoteCertificateDto { get; init; }
    public RemoteCertificateEnvelopeBulkSignDto RemoteCertificateEnvelopeBulkSignDto { get; init; }
    public RemoteCertificateSignature RemoteCertificateSignature { get; init; }
    public RemoteCertificateSignatureTypeDto RemoteCertificateSignatureTypeDto { get; init; }
    public RemoteCertificateStampImprintDto RemoteCertificateStampImprintDto { get; init; }
    public ReplacedEnvelopeFileResponse ReplacedEnvelopeFileResponse { get; init; }
    public ReplacedTemplateFileResponse ReplacedTemplateFileResponse { get; init; }
    public RequestBulkSignDevicesDto RequestBulkSignDevicesDto { get; init; }
    public ResumeBatchRequest ResumeBatchRequest { get; init; }
    public RoleDetailsDto RoleDetailsDto { get; init; }
    public RoleDto RoleDto { get; init; }
    public RolesSettings RolesSettings { get; init; }
    public string RolesSortingKey { get; init; }
    public RotateServiceAccountSecretResponse RotateServiceAccountSecretResponse { get; init; }
    public RowError RowError { get; init; }
    public SealingCertificateResponse SealingCertificateResponse { get; init; }
    public SenderAutomaticProfileDto SenderAutomaticProfileDto { get; init; }
    public SenderDataFieldSettingDto SenderDataFieldSettingDto { get; init; }
    public SenderGenericSigningPluginDto SenderGenericSigningPluginDto { get; init; }
    public SenderGenericSigningPluginSettingsDto SenderGenericSigningPluginSettingsDto { get; init; }
    public SentBulkEnvelopeResponse SentBulkEnvelopeResponse { get; init; }
    public SentEnvelopeDto SentEnvelopeDto { get; init; }
    public ServiceAccountListItemResponse ServiceAccountListItemResponse { get; init; }
    public ServiceAccountListResponse ServiceAccountListResponse { get; init; }
    public SettingsDto SettingsDto { get; init; }
    public SharingOptionsResponse SharingOptionsResponse { get; init; }
    public SignDeskOpenResultDto SignDeskOpenResultDto { get; init; }
    public SignatureAppearanceLayoutDto SignatureAppearanceLayoutDto { get; init; }
    public SignatureAppearanceLayoutRequest SignatureAppearanceLayoutRequest { get; init; }
    public string SignatureCategory { get; init; }
    public SignatureElementDefinition SignatureElementDefinition { get; init; }
    public SignatureElementDto SignatureElementDto { get; init; }
    public SignatureField SignatureField { get; init; }
    public SignatureFieldDto SignatureFieldDto { get; init; }
    public string SignatureFormat { get; init; }
    public SignatureImage SignatureImage { get; init; }
    public string SignatureOptions { get; init; }
    public SignaturePluginSignatureTypeDto SignaturePluginSignatureTypeDto { get; init; }
    public SignatureTaskConfiguration SignatureTaskConfiguration { get; init; }
    public SignatureTaskUpdateRequest SignatureTaskUpdateRequest { get; init; }
    public string SignatureType { get; init; }
    public SignerAgreements SignerAgreements { get; init; }
    public SingleInsight SingleInsight { get; init; }
    public SmsOneTimePassword SmsOneTimePassword { get; init; }
    public StageConfigurationDto StageConfigurationDto { get; init; }
    public StageDto StageDto { get; init; }
    public string StageMode { get; init; }
    public StageSortOrderItem StageSortOrderItem { get; init; }
    public string StageType { get; init; }
    public StampImprintConfigurationDto StampImprintConfigurationDto { get; init; }
    public StartBulkSignTransactionDto StartBulkSignTransactionDto { get; init; }
    public string StatusKey { get; init; }
    public StringInputConfig StringInputConfig { get; init; }
    public SubstituteDelegationDto SubstituteDelegationDto { get; init; }
    public SupportedElectronicIdentitiesResponse SupportedElectronicIdentitiesResponse { get; init; }
    public SupportedElectronicIdentityResponse SupportedElectronicIdentityResponse { get; init; }
    public SupportedFileFormatResponse SupportedFileFormatResponse { get; init; }
    public SwedishBankIdDto SwedishBankIdDto { get; init; }
    public SwedishBankIdSignatureTypeDto SwedishBankIdSignatureTypeDto { get; init; }
    public SwedishBankIdStampImprintDto SwedishBankIdStampImprintDto { get; init; }
    public SwissComOnDemandDto SwissComOnDemandDto { get; init; }
    public SwissComOnDemandSignatureTypeDto SwissComOnDemandSignatureTypeDto { get; init; }
    public SwissComOnDemandStampImprintDto SwissComOnDemandStampImprintDto { get; init; }
    public string SymbolLocationType { get; init; }
    public string TemplateAction { get; init; }
    public TemplateDto TemplateDto { get; init; }
    public TemplateFieldTask TemplateFieldTask { get; init; }
    public TemplateFieldTaskItem TemplateFieldTaskItem { get; init; }
    public TemplateFileTasksResponse TemplateFileTasksResponse { get; init; }
    public TemplateFilesResponse TemplateFilesResponse { get; init; }
    public TemplateListDto TemplateListDto { get; init; }
    public TemplatePermissions TemplatePermissions { get; init; }
    public TemplateStageAutomaticRecipientResponse TemplateStageAutomaticRecipientResponse { get; init; }
    public TemplateStageAutomaticRecipientSummaryDto TemplateStageAutomaticRecipientSummaryDto { get; init; }
    public TemplateStageItemDto TemplateStageItemDto { get; init; }
    public TemplateStageListDto TemplateStageListDto { get; init; }
    public TemplateStageRecipientResponse TemplateStageRecipientResponse { get; init; }
    public TemplateStageRecipientSummaryDto TemplateStageRecipientSummaryDto { get; init; }
    public TemplateStageStandardRecipientResponse TemplateStageStandardRecipientResponse { get; init; }
    public TemplateStageStandardRecipientSummaryDto TemplateStageStandardRecipientSummaryDto { get; init; }
    public TemplateThumbnailDto TemplateThumbnailDto { get; init; }
    public string TextAlignment { get; init; }
    public TextAnnotationConfigDto TextAnnotationConfigDto { get; init; }
    public TextBoxElementDefinition TextBoxElementDefinition { get; init; }
    public TextBoxElementDto TextBoxElementDto { get; init; }
    public TextDefinition TextDefinition { get; init; }
    public TextFieldDto TextFieldDto { get; init; }
    public object TextInputConfig { get; init; }
    public TextInputField TextInputField { get; init; }
    public string TextInputType { get; init; }
    public TextTaskUpdateRequest TextTaskUpdateRequest { get; init; }
    public string ThousandsSeparatorType { get; init; }
    public string TimeFormatSwaggerEnumProvider { get; init; }
    public TimeInputConfig TimeInputConfig { get; init; }
    public TimeZoneDto TimeZoneDto { get; init; }
    public TimeZoneListItemDto TimeZoneListItemDto { get; init; }
    public TimeZonesDto TimeZonesDto { get; init; }
    public TimeZonesLookupResponse TimeZonesLookupResponse { get; init; }
    public string TimestampHashAlgorithm { get; init; }
    public TimestampSettingsDto TimestampSettingsDto { get; init; }
    public TypeToSignSignature TypeToSignSignature { get; init; }
    public TypeToSignSignatureTypeDto TypeToSignSignatureTypeDto { get; init; }
    public TypeToSignStampImprintDto TypeToSignStampImprintDto { get; init; }
    public UiLanguageDto UiLanguageDto { get; init; }
    public UpdateATrustCertificateDto UpdateATrustCertificateDto { get; init; }
    public UpdateAccessCodeDto UpdateAccessCodeDto { get; init; }
    public UpdateAuditTrailModeRequest UpdateAuditTrailModeRequest { get; init; }
    public UpdateAuthenticationConfigurationDto UpdateAuthenticationConfigurationDto { get; init; }
    public UpdateAutomaticSignatureDataDto UpdateAutomaticSignatureDataDto { get; init; }
    public UpdateBankIdSettingsDto UpdateBankIdSettingsDto { get; init; }
    public UpdateBasicSettingsDto UpdateBasicSettingsDto { get; init; }
    public UpdateBulkEnvelopeDto UpdateBulkEnvelopeDto { get; init; }
    public UpdateBulkEnvelopeFileTasksRequest UpdateBulkEnvelopeFileTasksRequest { get; init; }
    public UpdateBulkEnvelopeForIntegrationDto UpdateBulkEnvelopeForIntegrationDto { get; init; }
    public UpdateBulkFileTasksRequest UpdateBulkFileTasksRequest { get; init; }
    public UpdateDisposableCertificateDto UpdateDisposableCertificateDto { get; init; }
    public UpdateDisposableCertificateSettingsDto UpdateDisposableCertificateSettingsDto { get; init; }
    public UpdateDocumentClassRequest UpdateDocumentClassRequest { get; init; }
    public UpdateEnvelopeDto UpdateEnvelopeDto { get; init; }
    public UpdateEnvelopeFileTasksRequest UpdateEnvelopeFileTasksRequest { get; init; }
    public UpdateEnvelopeForIntegrationDto UpdateEnvelopeForIntegrationDto { get; init; }
    public UpdateEnvelopeRecipientDto UpdateEnvelopeRecipientDto { get; init; }
    public UpdateEnvelopeStageAutomaticRecipientRequest UpdateEnvelopeStageAutomaticRecipientRequest { get; init; }
    public UpdateEnvelopeStageRecipientRequest UpdateEnvelopeStageRecipientRequest { get; init; }
    public UpdateEnvelopeStageRequest UpdateEnvelopeStageRequest { get; init; }
    public UpdateEnvelopeStageStandardRecipientRequest UpdateEnvelopeStageStandardRecipientRequest { get; init; }
    public UpdateExpirationConfigurationDto UpdateExpirationConfigurationDto { get; init; }
    public UpdateFileOrderRequest UpdateFileOrderRequest { get; init; }
    public UpdateFileTasksRequest UpdateFileTasksRequest { get; init; }
    public UpdateForIntegrationReminderDto UpdateForIntegrationReminderDto { get; init; }
    public UpdateGeneralPoliciesOverridesDto UpdateGeneralPoliciesOverridesDto { get; init; }
    public UpdateGenericSigningPluginsSenderDataDto UpdateGenericSigningPluginsSenderDataDto { get; init; }
    public UpdateOAuthAuthenticationDto UpdateOAuthAuthenticationDto { get; init; }
    public UpdateOAuthFieldDefinitionRequest UpdateOAuthFieldDefinitionRequest { get; init; }
    public UpdateOAuthJwtConfigRequest UpdateOAuthJwtConfigRequest { get; init; }
    public UpdateOAuthResourceUriRequest UpdateOAuthResourceUriRequest { get; init; }
    public UpdateOAuthSignerProviderDetailsRequest UpdateOAuthSignerProviderDetailsRequest { get; init; }
    public UpdateOAuthSignerProviderRequest UpdateOAuthSignerProviderRequest { get; init; }
    public UpdateOrganizationDefaultSignatureTypeRequest UpdateOrganizationDefaultSignatureTypeRequest { get; init; }
    public UpdateOrganizationDelegationSettingsRequest UpdateOrganizationDelegationSettingsRequest { get; init; }
    public UpdateOrganizationFeatureFlag UpdateOrganizationFeatureFlag { get; init; }
    public UpdateOrganizationFeatureFlagsRequest UpdateOrganizationFeatureFlagsRequest { get; init; }
    public UpdateOrganizationRecipientSettingsRequest UpdateOrganizationRecipientSettingsRequest { get; init; }
    public UpdateOrganizationUserDto UpdateOrganizationUserDto { get; init; }
    public UpdateOrganizationUserRolesDto UpdateOrganizationUserRolesDto { get; init; }
    public UpdateOtpSignatureDataDto UpdateOtpSignatureDataDto { get; init; }
    public UpdatePdfDocumentSettingsDto UpdatePdfDocumentSettingsDto { get; init; }
    public UpdatePolicyRequest UpdatePolicyRequest { get; init; }
    public UpdateRecipientAuthenticationSettingItemRequest UpdateRecipientAuthenticationSettingItemRequest { get; init; }
    public UpdateRecipientAuthenticationSettingsRequest UpdateRecipientAuthenticationSettingsRequest { get; init; }
    public UpdateRegionalSettingsDto UpdateRegionalSettingsDto { get; init; }
    public UpdateReminderConfigurationDto UpdateReminderConfigurationDto { get; init; }
    public UpdateRemoteCertificateDto UpdateRemoteCertificateDto { get; init; }
    public UpdateRoleRequest UpdateRoleRequest { get; init; }
    public UpdateSenderGenericSigningPluginDto UpdateSenderGenericSigningPluginDto { get; init; }
    public UpdateSenderGenericSigningPluginSettingsDto UpdateSenderGenericSigningPluginSettingsDto { get; init; }
    public UpdateSharingOptionsRequest UpdateSharingOptionsRequest { get; init; }
    public UpdateSignatureDataConfigurationDto UpdateSignatureDataConfigurationDto { get; init; }
    public UpdateSmsOneTimePasswordDto UpdateSmsOneTimePasswordDto { get; init; }
    public UpdateStageDto UpdateStageDto { get; init; }
    public UpdateStageSortOrderRequest UpdateStageSortOrderRequest { get; init; }
    public UpdateStampImprintConfigurationRequest UpdateStampImprintConfigurationRequest { get; init; }
    public UpdateSubstituteDelegationDto UpdateSubstituteDelegationDto { get; init; }
    public UpdateSwedishBankIdDto UpdateSwedishBankIdDto { get; init; }
    public UpdateSwissComOnDemandDto UpdateSwissComOnDemandDto { get; init; }
    public UpdateTemplateDto UpdateTemplateDto { get; init; }
    public UpdateTemplateFieldTasksRequest UpdateTemplateFieldTasksRequest { get; init; }
    public UpdateTemplateFileTasksRequest UpdateTemplateFileTasksRequest { get; init; }
    public UpdateTemplateForIntegrationDto UpdateTemplateForIntegrationDto { get; init; }
    public UpdateTemplateRecipientDto UpdateTemplateRecipientDto { get; init; }
    public UpdateTemplateStageAutomaticRecipientRequest UpdateTemplateStageAutomaticRecipientRequest { get; init; }
    public UpdateTemplateStageRecipientRequest UpdateTemplateStageRecipientRequest { get; init; }
    public UpdateTemplateStageRequest UpdateTemplateStageRequest { get; init; }
    public UpdateTemplateStageStandardRecipientRequest UpdateTemplateStageStandardRecipientRequest { get; init; }
    public UpdatedBasicSettingsDto UpdatedBasicSettingsDto { get; init; }
    public UserAndOrganizationDto UserAndOrganizationDto { get; init; }
    public UserApplicationContextDto UserApplicationContextDto { get; init; }
    public UserDefaultUserGroup UserDefaultUserGroup { get; init; }
    public string UserDefaultUserGroupDefaultType { get; init; }
    public UserGroupContactCreateDto UserGroupContactCreateDto { get; init; }
    public UserGroupContactDto UserGroupContactDto { get; init; }
    public UserGroupContactFieldDto UserGroupContactFieldDto { get; init; }
    public UserGroupContactFieldListDto UserGroupContactFieldListDto { get; init; }
    public UserGroupContactImportResultDto UserGroupContactImportResultDto { get; init; }
    public UserGroupContactImportValidationErrorResponse UserGroupContactImportValidationErrorResponse { get; init; }
    public UserGroupContactUpdateDto UserGroupContactUpdateDto { get; init; }
    public UserGroupContactsListDto UserGroupContactsListDto { get; init; }
    public UserGroupContactsPermissionDto UserGroupContactsPermissionDto { get; init; }
    public string UserGroupContactsSortingKey { get; init; }
    public UserGroupCreateDto UserGroupCreateDto { get; init; }
    public UserGroupCustomFieldUpdateData UserGroupCustomFieldUpdateData { get; init; }
    public UserGroupCustomFieldUpdateRequest UserGroupCustomFieldUpdateRequest { get; init; }
    public UserGroupDto UserGroupDto { get; init; }
    public UserGroupEnvelopesPermissionDto UserGroupEnvelopesPermissionDto { get; init; }
    public UserGroupPermissionDataDto UserGroupPermissionDataDto { get; init; }
    public UserGroupPermissionDto UserGroupPermissionDto { get; init; }
    public UserGroupPermissionsSetDto UserGroupPermissionsSetDto { get; init; }
    public UserGroupTemplatesPermissionDto UserGroupTemplatesPermissionDto { get; init; }
    public UserGroupUpdateDto UserGroupUpdateDto { get; init; }
    public UserGroupUserBusinessRoleRequest UserGroupUserBusinessRoleRequest { get; init; }
    public UserGroupUserDto UserGroupUserDto { get; init; }
    public UserGroupUserListDto UserGroupUserListDto { get; init; }
    public UserGroupUsersPermissionDto UserGroupUsersPermissionDto { get; init; }
    public string UserGroupUsersSortingKey { get; init; }
    public UserGroupsListDto UserGroupsListDto { get; init; }
    public UserGroupsPermissions UserGroupsPermissions { get; init; }
    public string UserGroupsSortingKey { get; init; }
    public UserImportResultDto UserImportResultDto { get; init; }
    public UserImportValidationErrorResponse UserImportValidationErrorResponse { get; init; }
    public UserOrganizationsDto UserOrganizationsDto { get; init; }
    public UserRegionalSettingsDto UserRegionalSettingsDto { get; init; }
    public UserRegionalSettingsRequestDto UserRegionalSettingsRequestDto { get; init; }
    public UserRoleRequest UserRoleRequest { get; init; }
    public UserRolesDto UserRolesDto { get; init; }
    public UsersSettings UsersSettings { get; init; }
    public string UsersSortingKey { get; init; }
    public ValidateOrganizationDto ValidateOrganizationDto { get; init; }
    public VersionInfo VersionInfo { get; init; }
    public WebhookAuthenticationRequest WebhookAuthenticationRequest { get; init; }
    public WebhookSubscriptionDto WebhookSubscriptionDto { get; init; }
    public WebhookSubscriptionRequest WebhookSubscriptionRequest { get; init; }
    public WorkUnitApprovalFieldResponse WorkUnitApprovalFieldResponse { get; init; }
    public WorkUnitAreaReadConfirmationFieldResponse WorkUnitAreaReadConfirmationFieldResponse { get; init; }
    public WorkUnitAttachmentFieldResponse WorkUnitAttachmentFieldResponse { get; init; }
    public WorkUnitAuthenticateRequest WorkUnitAuthenticateRequest { get; init; }
    public string WorkUnitAuthenticationProviderType { get; init; }
    public WorkUnitAuthenticationRequiredResponse WorkUnitAuthenticationRequiredResponse { get; init; }
    public WorkUnitAutomaticSignature WorkUnitAutomaticSignature { get; init; }
    public WorkUnitAutomaticSignatureResponse WorkUnitAutomaticSignatureResponse { get; init; }
    public WorkUnitBiometricSignature WorkUnitBiometricSignature { get; init; }
    public WorkUnitBiometricSignatureResponse WorkUnitBiometricSignatureResponse { get; init; }
    public WorkUnitCheckboxFieldResponse WorkUnitCheckboxFieldResponse { get; init; }
    public WorkUnitClickToSignSignature WorkUnitClickToSignSignature { get; init; }
    public WorkUnitClickToSignSignatureRequest WorkUnitClickToSignSignatureRequest { get; init; }
    public WorkUnitClickToSignSignatureResponse WorkUnitClickToSignSignatureResponse { get; init; }
    public WorkUnitDateInputConfigResponseResponse WorkUnitDateInputConfigResponseResponse { get; init; }
    public WorkUnitDateInputValue WorkUnitDateInputValue { get; init; }
    public string WorkUnitDecimalSeparatorTypeResponse { get; init; }
    public WorkUnitDisposableCertificateSignature WorkUnitDisposableCertificateSignature { get; init; }
    public WorkUnitDisposableCertificateSignatureResponseResponse WorkUnitDisposableCertificateSignatureResponseResponse { get; init; }
    public WorkUnitDrawToSignSignature WorkUnitDrawToSignSignature { get; init; }
    public WorkUnitDrawToSignSignatureRequest WorkUnitDrawToSignSignatureRequest { get; init; }
    public WorkUnitDrawToSignSignatureResponse WorkUnitDrawToSignSignatureResponse { get; init; }
    public WorkUnitDropDownFieldResponse WorkUnitDropDownFieldResponse { get; init; }
    public string WorkUnitElementSourceResponse { get; init; }
    public WorkUnitFieldResponse WorkUnitFieldResponse { get; init; }
    public WorkUnitFieldTaskResponse WorkUnitFieldTaskResponse { get; init; }
    public string WorkUnitFieldTaskSignatureType { get; init; }
    public string WorkUnitFieldTaskSignatureTypeRequest { get; init; }
    public string WorkUnitFieldTaskSignatureTypeResponse { get; init; }
    public string WorkUnitFieldType { get; init; }
    public string WorkUnitFieldTypeResponse { get; init; }
    public WorkUnitFileReadConfirmationFieldResponse WorkUnitFileReadConfirmationFieldResponse { get; init; }
    public WorkUnitFileResponse WorkUnitFileResponse { get; init; }
    public WorkUnitFontStyleResponse WorkUnitFontStyleResponse { get; init; }
    public WorkUnitInvisibleSignatureFieldResponse WorkUnitInvisibleSignatureFieldResponse { get; init; }
    public WorkUnitLinkFieldResponse WorkUnitLinkFieldResponse { get; init; }
    public WorkUnitListBoxFieldResponse WorkUnitListBoxFieldResponse { get; init; }
    public WorkUnitLocalCertificateSignature WorkUnitLocalCertificateSignature { get; init; }
    public WorkUnitLocalCertificateSignatureResponseResponse WorkUnitLocalCertificateSignatureResponseResponse { get; init; }
    public WorkUnitNumberInputConfigResponseResponse WorkUnitNumberInputConfigResponseResponse { get; init; }
    public WorkUnitNumberInputValue WorkUnitNumberInputValue { get; init; }
    public WorkUnitNumberSymbol WorkUnitNumberSymbol { get; init; }
    public WorkUnitOneTimePasswordSignature WorkUnitOneTimePasswordSignature { get; init; }
    public WorkUnitOneTimePasswordSignatureResponse WorkUnitOneTimePasswordSignatureResponse { get; init; }
    public WorkUnitOptionResponse WorkUnitOptionResponse { get; init; }
    public WorkUnitPageReadConfirmationFieldResponse WorkUnitPageReadConfirmationFieldResponse { get; init; }
    public WorkUnitPhoneNumberInputConfigResponseResponse WorkUnitPhoneNumberInputConfigResponseResponse { get; init; }
    public WorkUnitPluginSignature WorkUnitPluginSignature { get; init; }
    public WorkUnitPluginSignatureResponseResponse WorkUnitPluginSignatureResponseResponse { get; init; }
    public WorkUnitRadioButtonFieldResponse WorkUnitRadioButtonFieldResponse { get; init; }
    public WorkUnitRemoteCertificateSignature WorkUnitRemoteCertificateSignature { get; init; }
    public WorkUnitRemoteCertificateSignatureResponseResponse WorkUnitRemoteCertificateSignatureResponseResponse { get; init; }
    public WorkUnitResponse WorkUnitResponse { get; init; }
    public WorkUnitSignatureFieldResponse WorkUnitSignatureFieldResponse { get; init; }
    public WorkUnitSignaturePosition WorkUnitSignaturePosition { get; init; }
    public WorkUnitSignaturePositionRequest WorkUnitSignaturePositionRequest { get; init; }
    public WorkUnitStringInputConfigResponseResponse WorkUnitStringInputConfigResponseResponse { get; init; }
    public WorkUnitStringInputValue WorkUnitStringInputValue { get; init; }
    public string WorkUnitSymbolLocationTypeResponse { get; init; }
    public string WorkUnitTextAlignResponse { get; init; }
    public WorkUnitTextFieldResponse WorkUnitTextFieldResponse { get; init; }
    public object WorkUnitTextInputConfigResponse { get; init; }
    public string WorkUnitTextInputType { get; init; }
    public string WorkUnitTextInputTypeResponse { get; init; }
    public string WorkUnitThousandsSeparatorTypeResponse { get; init; }
    public WorkUnitTimeInputConfigResponse WorkUnitTimeInputConfigResponse { get; init; }
    public WorkUnitTypeToSignSignature WorkUnitTypeToSignSignature { get; init; }
    public WorkUnitTypeToSignSignatureRequest WorkUnitTypeToSignSignatureRequest { get; init; }
    public WorkUnitTypeToSignSignatureResponse WorkUnitTypeToSignSignatureResponse { get; init; }
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

public class SealingCertificate
{
    public string SubjectName { get; init; }
    public string Thumbprint { get; init; }
    public string ExpirationDate { get; init; }
    public string Issuer { get; init; }
}

public class SealingCertificateResponse
{
    public long Id { get; init; }
    public string ExternalId { get; init; }
    public bool IsActive { get; init; }
    public SealingCertificate SealingCertificate { get; init; }
    public List<Anonymous71> CertificateChain { get; init; }
}

public class SenderAutomaticProfileDto
{
    public string ProfileId { get; init; }
}

public class SenderDataFieldSettingDto
{
    public bool Required { get; init; }
    public List<Anonymous72> TranslatedLabels { get; init; }
    public string Type { get; init; }
}

public class SenderGenericSigningPluginDto
{
}

public class SenderGenericSigningPluginSettingsDto
{
}

public class SenderUser
{
}

public class SenderUser
{
}

public class SentBulkEnvelopeResponse
{
    public string Id { get; init; }
}

public class SentEnvelopeDto
{
    public string Id { get; init; }
}

public class ServiceAccountListItemResponse
{
    public string ClientId { get; init; }
    public string Email { get; init; }
    public string UserId { get; init; }
}

public class ServiceAccountListResponse
{
    public List<Anonymous73> Items { get; init; }
}

public class SettingsDto
{
    public long MaxEnvelopeValidityInDays { get; init; }
    public long MinEnvelopeValidityInSeconds { get; init; }
    public long FilterExpiringSoonDays { get; init; }
    public NotificationSettings NotificationSettings { get; init; }
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
    public string Position { get; init; }
}

public class SignatureAppearanceLayoutRequest
{
    public bool DisplayFirstname { get; init; }
    public bool DisplayLastname { get; init; }
    public bool DisplayDateTime { get; init; }
    public bool DisplayEmail { get; init; }
    public bool DisplayCustomText { get; init; }
    public bool DisplayReason { get; init; }
    public string Position { get; init; }
}

public class SignatureElementDefinition
{
    public Position Position { get; init; }
    public Size Size { get; init; }
}

public class SignatureElementDto
{
    public string ElementId { get; init; }
    public AllowedSignatureTypes AllowedSignatureTypes { get; init; }
    public ElementDefinition ElementDefinition { get; init; }
    public string Source { get; init; }
    public bool Required { get; init; }
    public long GuidingOrder { get; init; }
    public bool IsApprove { get; init; }
}

public class SignatureField
{
    public string Id { get; init; }
    public long Page { get; init; }
    public double PositionX { get; init; }
    public double PositionY { get; init; }
    public string FieldType { get; init; }
    public double Width { get; init; }
    public double Height { get; init; }
    public bool Required { get; init; }
    public string Source { get; init; }
}

public class SignatureFieldDto
{
    public string Id { get; init; }
    public long Page { get; init; }
    public double PositionX { get; init; }
    public double PositionY { get; init; }
    public double Width { get; init; }
    public double Height { get; init; }
    public List<object> AllowedSignatureTypes { get; init; }
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
}

public class SignatureTaskConfiguration
{
}

public class SignatureTaskUpdateRequest
{
    public object Signature { get; init; }
    public string FieldType { get; init; }
}

public class SignatureTypes
{
    public List<string> AllowedSignatureTypes { get; init; }
    public List<string> AllowedDefaultSignatureTypes { get; init; }
    public List<Anonymous93> AllowedGenericSigningPlugins { get; init; }
}

public class SignDeskOpenResultDto
{
}

public class SignerAgreements
{
    public bool IsEnvelopeOverrideEnabled { get; init; }
}

public class SignerAgreements
{
    public bool IsEnvelopeOverrideEnabled { get; init; }
}

public class SingleInsight
{
    public long EnvelopeCount { get; init; }
}

public class Size
{
    public double Width { get; init; }
    public double Height { get; init; }
}

public class Size
{
    public double Width { get; init; }
    public double Height { get; init; }
}

public class Size
{
    public double Width { get; init; }
    public double Height { get; init; }
}

public class Size
{
    public double Width { get; init; }
    public double Height { get; init; }
}

public class Size
{
    public double Width { get; init; }
    public double Height { get; init; }
}

public class Size
{
    public double Width { get; init; }
    public double Height { get; init; }
}

public class Size
{
    public double Width { get; init; }
    public double Height { get; init; }
}

public class Size
{
    public double Width { get; init; }
    public double Height { get; init; }
}

public class Size
{
    public double Width { get; init; }
    public double Height { get; init; }
}

public class Size
{
    public double Width { get; init; }
    public double Height { get; init; }
}

public class Size
{
    public double Width { get; init; }
    public double Height { get; init; }
}

public class Size
{
    public double Width { get; init; }
    public double Height { get; init; }
}

public class Size
{
    public double Width { get; init; }
    public double Height { get; init; }
}

public class Size
{
    public double Width { get; init; }
    public double Height { get; init; }
}

public class Size
{
    public double Width { get; init; }
    public double Height { get; init; }
}

public class Size
{
    public double Width { get; init; }
    public double Height { get; init; }
}

public class Size
{
    public double Width { get; init; }
    public double Height { get; init; }
}

public class Size
{
    public double Width { get; init; }
    public double Height { get; init; }
}

public class Size
{
    public double Width { get; init; }
    public double Height { get; init; }
}

public class Size
{
    public double Width { get; init; }
    public double Height { get; init; }
}

public class Size
{
    public double Width { get; init; }
    public double Height { get; init; }
}

public class Size
{
    public double Width { get; init; }
    public double Height { get; init; }
}

public class Size
{
    public double Width { get; init; }
    public double Height { get; init; }
}

public class Size
{
    public double Width { get; init; }
    public double Height { get; init; }
}

public class Size
{
    public double Width { get; init; }
    public double Height { get; init; }
}

public class Size
{
    public double Width { get; init; }
    public double Height { get; init; }
}

public class Size
{
    public double Width { get; init; }
    public double Height { get; init; }
}

public class Size
{
    public double Width { get; init; }
    public double Height { get; init; }
}

public class Size
{
    public double Width { get; init; }
    public double Height { get; init; }
}

public class Size
{
    public double Width { get; init; }
    public double Height { get; init; }
}

public class Size
{
    public double Width { get; init; }
    public double Height { get; init; }
}

public class Size
{
    public double Width { get; init; }
    public double Height { get; init; }
}

public class SmsOneTimePassword
{
}

public class Stage
{
    public string Name { get; init; }
    public string Type { get; init; }
    public long RequiredRecipientCompletions { get; init; }
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
}

public class StageSortOrderItem
{
    public string Id { get; init; }
    public long SortOrder { get; init; }
}

public class StampImprintConfigurationDto
{
    public DefaultLayout DefaultLayout { get; init; }
    public List<Anonymous74> CustomSignatures { get; init; }
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
}

public class SupportedElectronicIdentitiesResponse
{
    public List<Anonymous75> ElectronicIdentities { get; init; }
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

public class SwedishBankIdDto
{
}

public class SwedishBankIdSignatureTypeDto
{
}

public class SwedishBankIdStampImprintDto
{
    public bool DisplayName { get; init; }
    public bool DisplaySignatureDate { get; init; }
    public bool DisplayExtraInformation { get; init; }
    public bool DisplayTransactionId { get; init; }
}

public class SwissComOnDemandDto
{
}

public class SwissComOnDemandSignatureTypeDto
{
}

public class SwissComOnDemandStampImprintDto
{
    public bool DisplayName { get; init; }
    public bool DisplaySignatureDate { get; init; }
    public bool DisplayExtraInformation { get; init; }
    public bool DisplayIp { get; init; }
}

public class TemplateDto
{
    public string Id { get; init; }
    public string CreatorUserId { get; init; }
    public string Name { get; init; }
    public List<string> Actions { get; init; }
    public string CreatedAt { get; init; }
    public string UpdatedAt { get; init; }
}

public class TemplateFieldTask
{
    public object Field { get; init; }
    public long SortOrder { get; init; }
}

public class TemplateFieldTaskItem
{
    public object Field { get; init; }
    public long SortOrder { get; init; }
    public string Source { get; init; }
}

public class TemplateFilesResponse
{
    public List<Anonymous77> Files { get; init; }
}

public class TemplateFileTasksResponse
{
    public List<Anonymous76> Tasks { get; init; }
}

public class TemplateListDto
{
    public List<Anonymous78> Templates { get; init; }
    public Pagination Pagination { get; init; }
}

public class TemplatePermissions
{
    public bool Read { get; init; }
    public bool CreateUpdateDelete { get; init; }
}

public class Templates
{
    public bool Read { get; init; }
    public bool CreateUpdateDelete { get; init; }
}

public class Templates
{
    public bool Read { get; init; }
    public bool CreateUpdateDelete { get; init; }
}

public class Templates
{
    public bool Share { get; init; }
    public bool Manage { get; init; }
}

public class Templates
{
    public bool Share { get; init; }
    public bool Manage { get; init; }
}

public class Templates
{
    public bool Share { get; init; }
    public bool Manage { get; init; }
}

public class Templates
{
    public bool Share { get; init; }
    public bool Manage { get; init; }
}

public class TemplateStageAutomaticRecipientResponse
{
    public string Id { get; init; }
    public string Type { get; init; }
}

public class TemplateStageAutomaticRecipientSummaryDto
{
    public string Id { get; init; }
    public string Type { get; init; }
}

public class TemplateStageItemDto
{
    public string Id { get; init; }
    public long SortOrder { get; init; }
    public long RequiredRecipientCompletions { get; init; }
    public string Type { get; init; }
    public List<object> Recipients { get; init; }
}

public class TemplateStageListDto
{
    public List<Anonymous79> Stages { get; init; }
}

public class TemplateStageRecipientResponse
{
    public string Id { get; init; }
}

public class TemplateStageRecipientSummaryDto
{
    public string Id { get; init; }
}

public class TemplateStageStandardRecipientResponse
{
    public string Id { get; init; }
    public bool IsDelegationEnabled { get; init; }
    public string Type { get; init; }
}

public class TemplateStageStandardRecipientSummaryDto
{
    public string GivenName { get; init; }
    public string Id { get; init; }
    public string Surname { get; init; }
    public string Email { get; init; }
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
    public string AnnotationType { get; init; }
}

public class TextBoxElementDefinition
{
    public Position Position { get; init; }
    public Size Size { get; init; }
    public TextFormat TextFormat { get; init; }
    public bool ReadOnly { get; init; }
    public bool IsMultiline { get; init; }
    public bool IsPassword { get; init; }
    public long MaxLength { get; init; }
}

public class TextBoxElementDto
{
    public string ElementId { get; init; }
    public ElementDefinition ElementDefinition { get; init; }
    public string Source { get; init; }
    public bool Required { get; init; }
    public string Value { get; init; }
    public long GuidingOrder { get; init; }
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
    public double PositionX { get; init; }
    public double PositionY { get; init; }
    public double Width { get; init; }
    public double Height { get; init; }
    public Font Font { get; init; }
    public object TextInputConfig { get; init; }
    public bool Required { get; init; }
    public bool ReadOnly { get; init; }
    public string FieldType { get; init; }
}

public class TextFormat
{
    public string TextColor { get; init; }
    public double FontSizeInPt { get; init; }
    public string FontName { get; init; }
    public bool Bold { get; init; }
    public bool Italic { get; init; }
    public string TextAlign { get; init; }
}

public class TextFormat
{
    public string TextColor { get; init; }
    public double FontSizeInPt { get; init; }
    public string FontName { get; init; }
    public bool Bold { get; init; }
    public bool Italic { get; init; }
    public string TextAlign { get; init; }
}

public class TextFormat
{
    public string TextColor { get; init; }
    public double FontSizeInPt { get; init; }
    public string FontName { get; init; }
    public bool Bold { get; init; }
    public bool Italic { get; init; }
    public string TextAlign { get; init; }
}

public class TextFormat
{
    public string TextColor { get; init; }
    public double FontSizeInPt { get; init; }
    public string FontName { get; init; }
    public bool Bold { get; init; }
    public bool Italic { get; init; }
    public string TextAlign { get; init; }
}

public class TextFormat
{
    public string TextColor { get; init; }
    public double FontSizeInPt { get; init; }
    public string FontName { get; init; }
    public bool Bold { get; init; }
    public bool Italic { get; init; }
    public string TextAlign { get; init; }
}

public class TextFormat
{
    public string TextColor { get; init; }
    public double FontSizeInPt { get; init; }
    public string FontName { get; init; }
    public bool Bold { get; init; }
    public bool Italic { get; init; }
    public string TextAlign { get; init; }
}

public class TextFormat
{
    public string TextColor { get; init; }
    public double FontSizeInPt { get; init; }
    public string FontName { get; init; }
    public bool Bold { get; init; }
    public bool Italic { get; init; }
    public string TextAlign { get; init; }
}

public class TextFormat
{
    public string TextColor { get; init; }
    public double FontSizeInPt { get; init; }
    public string FontName { get; init; }
    public bool Bold { get; init; }
    public bool Italic { get; init; }
    public string TextAlign { get; init; }
}

public class TextFormat
{
    public string TextColor { get; init; }
    public double FontSizeInPt { get; init; }
    public string FontName { get; init; }
    public bool Bold { get; init; }
    public bool Italic { get; init; }
    public string TextAlign { get; init; }
}

public class TextFormat
{
    public string TextColor { get; init; }
    public double FontSizeInPt { get; init; }
    public string FontName { get; init; }
    public bool Bold { get; init; }
    public bool Italic { get; init; }
    public string TextAlign { get; init; }
}

public class TextFormat
{
    public string TextColor { get; init; }
    public double FontSizeInPt { get; init; }
    public string FontName { get; init; }
    public bool Bold { get; init; }
    public bool Italic { get; init; }
    public string TextAlign { get; init; }
}

public class TextFormat
{
    public string TextColor { get; init; }
    public double FontSizeInPt { get; init; }
    public string FontName { get; init; }
    public bool Bold { get; init; }
    public bool Italic { get; init; }
    public string TextAlign { get; init; }
}

public class TextInputField
{
    public string Id { get; init; }
    public long Page { get; init; }
    public double PositionX { get; init; }
    public double PositionY { get; init; }
    public bool ReadOnly { get; init; }
    public string Text { get; init; }
    public bool Password { get; init; }
    public bool Multiline { get; init; }
    public long MaxLength { get; init; }
    public string FieldType { get; init; }
    public double Width { get; init; }
    public double Height { get; init; }
    public bool Required { get; init; }
    public string Source { get; init; }
}

public class TextTaskUpdateRequest
{
    public object TextInputValue { get; init; }
    public string FieldType { get; init; }
}

public class TimeInputConfig
{
    public string TextInputType { get; init; }
}

public class TimestampSettingsDto
{
    public string Url { get; init; }
    public string Username { get; init; }
    public string Password { get; init; }
    public string HashAlgorithm { get; init; }
}

public class TimeZoneDto
{
    public string Code { get; init; }
    public string Name { get; init; }
}

public class TimeZoneListItemDto
{
    public string TimeZone { get; init; }
    public string Code { get; init; }
    public string UtcOffset { get; init; }
}

public class TimeZonesDto
{
    public List<Anonymous80> Options { get; init; }
}

public class TimeZonesLookupResponse
{
    public List<Anonymous81> TimeZones { get; init; }
}

public class TypeToSignSignature
{
    public string SignatureType { get; init; }
}

public class TypeToSignSignatureTypeDto
{
}

public class TypeToSignStampImprintDto
{
    public bool DisplayName { get; init; }
    public bool DisplaySignatureDate { get; init; }
    public bool DisplayExtraInformation { get; init; }
    public bool DisplayEmail { get; init; }
    public bool DisplayIp { get; init; }
}

public class UiLanguageDto
{
    public string Code { get; init; }
    public string Name { get; init; }
}

public class UpdateAccessCodeDto
{
    public string Code { get; init; }
}

public class UpdateATrustCertificateDto
{
}

public class UpdateAuditTrailModeRequest
{
    public string AuditTrailMode { get; init; }
}

public class UpdateAuthenticationConfigurationDto
{
}

public class UpdateAutomaticSignatureDataDto
{
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
    public ExpirationConfiguration ExpirationConfiguration { get; init; }
    public ReminderConfiguration ReminderConfiguration { get; init; }
    public string EnvelopeType { get; init; }
}

public class UpdateBulkEnvelopeFileTasksRequest
{
    public List<Anonymous82> Tasks { get; init; }
}

public class UpdateBulkEnvelopeForIntegrationDto
{
    public string Name { get; init; }
    public Reminder Reminder { get; init; }
    public object Expiration { get; init; }
}

public class UpdateBulkFileTasksRequest
{
    public List<Anonymous83> FieldTasks { get; init; }
}

public class UpdatedBasicSettingsDto
{
    public string Id { get; init; }
    public string GivenName { get; init; }
    public string Surname { get; init; }
}

public class UpdateDisposableCertificateDto
{
}

public class UpdateDisposableCertificateSettingsDto
{
    public string LraId { get; init; }
    public string User { get; init; }
    public string DisposableType { get; init; }
    public bool ShowDisclaimerBeforeCertificateRequest { get; init; }
    public bool SendDisposableDisclaimerDocumentNotifications { get; init; }
}

public class UpdateDocumentClassRequest
{
    public string Name { get; init; }
}

public class UpdateEnvelopeDto
{
    public ExpirationConfiguration ExpirationConfiguration { get; init; }
    public ReminderConfiguration ReminderConfiguration { get; init; }
    public string EnvelopeType { get; init; }
}

public class UpdateEnvelopeFileTasksRequest
{
    public List<Anonymous84> Tasks { get; init; }
}

public class UpdateEnvelopeForIntegrationDto
{
    public string Name { get; init; }
    public Reminder Reminder { get; init; }
    public object Expiration { get; init; }
}

public class UpdateEnvelopeRecipientDto
{
    public string Id { get; init; }
    public bool IsDelegationEnabled { get; init; }
}

public class UpdateEnvelopeStageAutomaticRecipientRequest
{
    public string Type { get; init; }
}

public class UpdateEnvelopeStageRecipientRequest
{
}

public class UpdateEnvelopeStageRequest
{
}

public class UpdateEnvelopeStageStandardRecipientRequest
{
    public string Type { get; init; }
}

public class UpdateExpirationConfigurationDto
{
}

public class UpdateFileOrderRequest
{
    public List<Anonymous85> Files { get; init; }
}

public class UpdateFileTasksRequest
{
    public List<Anonymous86> FieldTasks { get; init; }
}

public class UpdateForIntegrationReminderDto
{
    public bool Enabled { get; init; }
    public long FirstReminderInDays { get; init; }
    public long ResendIntervalInDays { get; init; }
    public long BeforeExpirationInDays { get; init; }
}

public class UpdateGeneralPoliciesOverridesDto
{
    public bool AllowSaveDocument { get; init; }
    public bool AllowSaveAuditTrail { get; init; }
    public bool AllowPrintDocument { get; init; }
    public bool AllowAdhocPdfAttachments { get; init; }
    public bool AllowRejectWorkstep { get; init; }
    public bool AllowUndoLastAction { get; init; }
}

public class UpdateGenericSigningPluginsSenderDataDto
{
}

public class UpdateOAuthAuthenticationDto
{
    public string ExternalId { get; init; }
}

public class UpdateOAuthFieldDefinitionRequest
{
    public string Path { get; init; }
    public string Mode { get; init; }
    public string Target { get; init; }
}

public class UpdateOAuthJwtConfigRequest
{
    public long OAuthProviderId { get; init; }
    public string JwksUri { get; init; }
    public string Issuer { get; init; }
    public bool EnforceNonce { get; init; }
    public bool ValidateAudience { get; init; }
    public bool ValidateIssuer { get; init; }
    public bool ValidateLifetime { get; init; }
}

public class UpdateOAuthResourceUriRequest
{
    public string Uri { get; init; }
    public string AccessTokenParamName { get; init; }
}

public class UpdateOAuthSignerProviderDetailsRequest
{
    public OAuthSignerProvider OAuthSignerProvider { get; init; }
}

public class UpdateOAuthSignerProviderRequest
{
    public string ExternalId { get; init; }
    public string Name { get; init; }
    public string ClientId { get; init; }
    public string AuthorizationUri { get; init; }
    public string TokenUri { get; init; }
    public long AuthenticationType { get; init; }
}

public class UpdateOrganizationDefaultSignatureTypeRequest
{
    public string SignatureType { get; init; }
}

public class UpdateOrganizationDelegationSettingsRequest
{
    public string DelegationPolicy { get; init; }
}

public class UpdateOrganizationFeatureFlag
{
    public long Id { get; init; }
    public bool Enabled { get; init; }
}

public class UpdateOrganizationFeatureFlagsRequest
{
    public List<Anonymous87> FeatureFlags { get; init; }
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
    public UserRegionalSettings UserRegionalSettings { get; init; }
}

public class UpdateOrganizationUserRolesDto
{
    public List<string> Roles { get; init; }
}

public class UpdateOtpSignatureDataDto
{
}

public class UpdatePdfDocumentSettingsDto
{
    public bool AllowSigningOfLockedPdfDocuments { get; init; }
}

public class UpdatePolicyRequest
{
    public string Name { get; init; }
    public bool IsActive { get; init; }
    public long SortOrder { get; init; }
}

public class UpdateRecipientAuthenticationSettingItemRequest
{
    public string Name { get; init; }
    public bool IsEnabled { get; init; }
}

public class UpdateRecipientAuthenticationSettingsRequest
{
}

public class UpdateRegionalSettingsDto
{
    public string WorldTimeZone { get; init; }
    public long DateTimeFormatId { get; init; }
    public string UiLanguage { get; init; }
    public long CountryId { get; init; }
}

public class UpdateReminderConfigurationDto
{
}

public class UpdateRemoteCertificateDto
{
}

public class UpdateRoleRequest
{
    public string Name { get; init; }
    public List<Anonymous88> Permissions { get; init; }
}

public class UpdateSenderGenericSigningPluginDto
{
}

public class UpdateSenderGenericSigningPluginSettingsDto
{
}

public class UpdateSharingOptionsRequest
{
    public List<string> UserGroupIds { get; init; }
}

public class UpdateSignatureDataConfigurationDto
{
}

public class UpdateSmsOneTimePasswordDto
{
}

public class UpdateStageDto
{
    public string Id { get; init; }
    public long MandatoryRecipientsNumber { get; init; }
}

public class UpdateStageSortOrderRequest
{
    public List<Anonymous89> Stages { get; init; }
}

public class UpdateStampImprintConfigurationRequest
{
    public DefaultLayout DefaultLayout { get; init; }
    public List<Anonymous90> CustomSignatures { get; init; }
}

public class UpdateSubstituteDelegationDto
{
    public string DelegateeUserEmail { get; init; }
    public bool UtilizeAlsoOnCCRecipients { get; init; }
}

public class UpdateSwedishBankIdDto
{
}

public class UpdateSwissComOnDemandDto
{
}

public class UpdateTemplateDto
{
    public ExpirationConfiguration ExpirationConfiguration { get; init; }
    public ReminderConfiguration ReminderConfiguration { get; init; }
    public string EnvelopeType { get; init; }
}

public class UpdateTemplateFieldTasksRequest
{
    public List<Anonymous91> FieldTasks { get; init; }
}

public class UpdateTemplateFileTasksRequest
{
    public List<Anonymous92> Tasks { get; init; }
}

public class UpdateTemplateForIntegrationDto
{
    public string Name { get; init; }
    public Reminder Reminder { get; init; }
    public object Expiration { get; init; }
}

public class UpdateTemplateRecipientDto
{
    public string Id { get; init; }
    public bool IsDelegationEnabled { get; init; }
}

public class UpdateTemplateStageAutomaticRecipientRequest
{
    public string Type { get; init; }
}

public class UpdateTemplateStageRecipientRequest
{
}

public class UpdateTemplateStageRequest
{
}

public class UpdateTemplateStageStandardRecipientRequest
{
    public string Type { get; init; }
}

public class UserAndOrganizationDto
{
    public string Id { get; init; }
    public string GivenName { get; init; }
    public string Surname { get; init; }
    public string Email { get; init; }
    public string OrganizationId { get; init; }
    public string OrganizationName { get; init; }
}

public class UserApplicationContextDto
{
    public SignatureTypes SignatureTypes { get; init; }
    public DefaultSignatureType DefaultSignatureType { get; init; }
    public List<string> SignatureOptions { get; init; }
    public List<string> RecipientTypes { get; init; }
    public RecipientAuthenticationTypes RecipientAuthenticationTypes { get; init; }
    public SignerAgreements SignerAgreements { get; init; }
    public GeneralPolicies GeneralPolicies { get; init; }
    public NotificationChannels NotificationChannels { get; init; }
    public UserPermissions UserPermissions { get; init; }
    public UserGroupPermissions UserGroupPermissions { get; init; }
    public DelegationInfo DelegationInfo { get; init; }
    public bool OAuthAvailable { get; init; }
    public bool AutomaticRemoteSignatureAvailable { get; init; }
    public bool DocumentClassesEnabled { get; init; }
    public bool EnvelopeEventServiceEnabled { get; init; }
    public List<string> FontFamilies { get; init; }
    public bool BulkEnvelopeEnabled { get; init; }
}

public class UserDefaultUserGroup
{
    public string Id { get; init; }
    public string Name { get; init; }
}

public class UserGroupContactCreateDto
{
}

public class UserGroupContactDto
{
    public string Id { get; init; }
    public string UserGroupId { get; init; }
}

public class UserGroupContactFieldDto
{
    public string Id { get; init; }
    public string UserGroupId { get; init; }
    public string Name { get; init; }
}

public class UserGroupContactFieldListDto
{
    public List<Anonymous94> UserGroupContactFields { get; init; }
}

public class UserGroupContactImportResultDto
{
    public long Imported { get; init; }
}

public class UserGroupContactImportValidationErrorResponse
{
    public List<Anonymous95> Errors { get; init; }
}

public class UserGroupContactsListDto
{
    public List<Anonymous96> UserGroupContacts { get; init; }
    public Pagination Pagination { get; init; }
}

public class UserGroupContactsPermissionDto
{
    public bool Read { get; init; }
    public bool CreateUpdateDelete { get; init; }
    public bool Customize { get; init; }
}

public class UserGroupContactUpdateDto
{
}

public class UserGroupCreateDto
{
    public string Name { get; init; }
}

public class UserGroupCustomFieldUpdateData
{
    public string UserGroupId { get; init; }
    public string Name { get; init; }
}

public class UserGroupCustomFieldUpdateRequest
{
    public List<Anonymous97> UpdatedCustomFields { get; init; }
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
    public Permissions Permissions { get; init; }
}

public class UserGroupPermissionDto
{
    public Users Users { get; init; }
    public Envelopes Envelopes { get; init; }
    public Templates Templates { get; init; }
    public Contacts Contacts { get; init; }
}

public class UserGroupPermissions
{
    public UserGroups UserGroups { get; init; }
}

public class UserGroupPermissionsSetDto
{
    public UserGroups UserGroups { get; init; }
}

public class UserGroups
{
    public bool Read { get; init; }
    public bool CreateUpdateDelete { get; init; }
}

public class UserGroups
{
    public bool Read { get; init; }
    public bool CreateUpdateDelete { get; init; }
}

public class UserGroups
{
}

public class UserGroups
{
}

public class UserGroupsListDto
{
    public List<Anonymous99> UserGroups { get; init; }
    public Pagination Pagination { get; init; }
}

public class UserGroupsPermissions
{
    public bool Read { get; init; }
    public bool CreateUpdateDelete { get; init; }
}

public class UserGroupTemplatesPermissionDto
{
    public bool Share { get; init; }
    public bool Manage { get; init; }
}

public class UserGroupUpdateDto
{
    public string Name { get; init; }
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
    public Permissions Permissions { get; init; }
}

public class UserGroupUserListDto
{
    public string UserGroupId { get; init; }
    public List<Anonymous98> UserGroupUsers { get; init; }
    public Pagination Pagination { get; init; }
}

public class UserGroupUsersPermissionDto
{
    public bool Read { get; init; }
    public bool CreateUpdateDelete { get; init; }
}

public class UserImportResultDto
{
    public long Imported { get; init; }
}

public class UserImportValidationErrorResponse
{
    public List<Anonymous100> Errors { get; init; }
}

public class UserOrganizationsDto
{
    public List<Anonymous101> Organizations { get; init; }
    public string DefaultOrganizationId { get; init; }
}

public class UserPermissions
{
    public Envelopes Envelopes { get; init; }
    public Templates Templates { get; init; }
    public UserGroups UserGroups { get; init; }
    public OrganizationSettings OrganizationSettings { get; init; }
    public Users Users { get; init; }
    public Roles Roles { get; init; }
    public AutomaticESealing AutomaticESealing { get; init; }
}

public class UserRegionalSettings
{
    public string TimeZone { get; init; }
    public string DateTimeFormat { get; init; }
    public string Language { get; init; }
    public string Country { get; init; }
}

public class UserRegionalSettingsDto
{
    public string WorldTimeZone { get; init; }
    public long DateTimeFormatId { get; init; }
    public string UiLanguage { get; init; }
    public long CountryId { get; init; }
}

public class UserRegionalSettingsRequestDto
{
    public string TimeZone { get; init; }
    public string DateTimeFormat { get; init; }
    public string Language { get; init; }
    public string Country { get; init; }
}

public class UserRoleRequest
{
    public string Name { get; init; }
}

public class UserRolesDto
{
    public List<string> Roles { get; init; }
}

public class Users
{
    public bool Read { get; init; }
    public bool CreateUpdateDelete { get; init; }
}

public class Users
{
    public bool Read { get; init; }
    public bool CreateUpdateDelete { get; init; }
}

public class Users
{
    public bool Read { get; init; }
    public bool CreateUpdateDelete { get; init; }
}

public class Users
{
    public bool Read { get; init; }
    public bool CreateUpdateDelete { get; init; }
}

public class Users
{
    public bool Read { get; init; }
    public bool CreateUpdateDelete { get; init; }
}

public class Users
{
    public bool Read { get; init; }
    public bool CreateUpdateDelete { get; init; }
}

public class UsersSettings
{
    public bool Read { get; init; }
    public bool CreateUpdateDelete { get; init; }
}

public class ValidateOrganizationDto
{
    public string Name { get; init; }
    public string IsoCulture { get; init; }
    public List<string> Features { get; init; }
}

public class VersionInfo
{
    public string ImageTag { get; init; }
    public string Version { get; init; }
}

public class WaitingForOthers
{
    public long EnvelopeCount { get; init; }
}

public class WaitingForYou
{
    public long EnvelopeCount { get; init; }
}

public class WebhookAuthenticationRequest
{
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
}

public class WorkUnitApprovalFieldResponse
{
    public string Id { get; init; }
    public long Page { get; init; }
    public double PositionX { get; init; }
    public double PositionY { get; init; }
    public double Width { get; init; }
    public double Height { get; init; }
    public bool Required { get; init; }
    public string FieldType { get; init; }
}

public class WorkUnitAreaReadConfirmationFieldResponse
{
    public string Id { get; init; }
    public long Page { get; init; }
    public double PositionX { get; init; }
    public double PositionY { get; init; }
    public double Width { get; init; }
    public double Height { get; init; }
    public bool Required { get; init; }
    public bool ReadOnly { get; init; }
    public string FieldType { get; init; }
}

public class WorkUnitAttachmentFieldResponse
{
    public string Id { get; init; }
    public long Page { get; init; }
    public double PositionX { get; init; }
    public double PositionY { get; init; }
    public double Width { get; init; }
    public double Height { get; init; }
    public bool Required { get; init; }
    public string FieldType { get; init; }
}

public class WorkUnitAuthenticateRequest
{
    public string Code { get; init; }
}

public class WorkUnitAuthenticationRequiredResponse
{
}

public class WorkUnitAutomaticSignature
{
    public string SignatureType { get; init; }
}

public class WorkUnitAutomaticSignatureResponse
{
    public string SignatureType { get; init; }
}

public class WorkUnitBiometricSignature
{
    public string SignatureType { get; init; }
}

public class WorkUnitBiometricSignatureResponse
{
    public string SignatureType { get; init; }
}

public class WorkUnitCheckboxFieldResponse
{
    public string Id { get; init; }
    public long Page { get; init; }
    public double PositionX { get; init; }
    public double PositionY { get; init; }
    public double Width { get; init; }
    public double Height { get; init; }
    public bool Checked { get; init; }
    public string Value { get; init; }
    public bool Required { get; init; }
    public bool ReadOnly { get; init; }
    public string FieldType { get; init; }
}

public class WorkUnitClickToSignSignature
{
    public string SignatureType { get; init; }
}

public class WorkUnitClickToSignSignatureRequest
{
    public string SignatureType { get; init; }
}

public class WorkUnitClickToSignSignatureResponse
{
    public string SignatureType { get; init; }
}

public class WorkUnitDateInputConfigResponseResponse
{
    public string TextInputType { get; init; }
}

public class WorkUnitDateInputValue
{
    public string Value { get; init; }
    public string TextInputType { get; init; }
}

public class WorkUnitDisposableCertificateSignature
{
    public string SignatureType { get; init; }
}

public class WorkUnitDisposableCertificateSignatureResponseResponse
{
    public string SignatureType { get; init; }
}

public class WorkUnitDrawToSignSignature
{
    public string SignatureType { get; init; }
}

public class WorkUnitDrawToSignSignatureRequest
{
    public string SignatureType { get; init; }
}

public class WorkUnitDrawToSignSignatureResponse
{
    public string SignatureType { get; init; }
}

public class WorkUnitDropDownFieldResponse
{
    public string Id { get; init; }
    public long Page { get; init; }
    public double PositionX { get; init; }
    public double PositionY { get; init; }
    public double Width { get; init; }
    public double Height { get; init; }
    public Font Font { get; init; }
    public bool Required { get; init; }
    public bool ReadOnly { get; init; }
    public bool IsEditable { get; init; }
    public string FieldType { get; init; }
}

public class WorkUnitFieldResponse
{
    public string Id { get; init; }
}

public class WorkUnitFieldTaskResponse
{
    public object Field { get; init; }
    public long SortOrder { get; init; }
    public string Source { get; init; }
    public bool Completed { get; init; }
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
    public List<Anonymous102> Tasks { get; init; }
}

public class WorkUnitFontStyleResponse
{
    public string Color { get; init; }
    public double Size { get; init; }
    public string Name { get; init; }
    public bool Bold { get; init; }
    public bool Italic { get; init; }
    public string Align { get; init; }
}

public class WorkUnitInvisibleSignatureFieldResponse
{
    public string Id { get; init; }
    public string FieldType { get; init; }
}

public class WorkUnitLinkFieldResponse
{
    public string Id { get; init; }
    public long Page { get; init; }
    public double PositionX { get; init; }
    public double PositionY { get; init; }
    public double Width { get; init; }
    public double Height { get; init; }
    public string FieldType { get; init; }
}

public class WorkUnitListBoxFieldResponse
{
    public string Id { get; init; }
    public long Page { get; init; }
    public double PositionX { get; init; }
    public double PositionY { get; init; }
    public double Width { get; init; }
    public double Height { get; init; }
    public Font Font { get; init; }
    public bool MultiSelect { get; init; }
    public bool Required { get; init; }
    public bool ReadOnly { get; init; }
    public string FieldType { get; init; }
}

public class WorkUnitLocalCertificateSignature
{
    public string SignatureType { get; init; }
}

public class WorkUnitLocalCertificateSignatureResponseResponse
{
    public string SignatureType { get; init; }
}

public class WorkUnitNumberInputConfigResponseResponse
{
    public string TextInputType { get; init; }
}

public class WorkUnitNumberInputValue
{
    public double Value { get; init; }
    public string TextInputType { get; init; }
}

public class WorkUnitNumberSymbol
{
    public string Position { get; init; }
}

public class WorkUnitOneTimePasswordSignature
{
    public string SignatureType { get; init; }
}

public class WorkUnitOneTimePasswordSignatureResponse
{
    public string SignatureType { get; init; }
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
    public double PositionX { get; init; }
    public double PositionY { get; init; }
    public double Width { get; init; }
    public double Height { get; init; }
    public bool Required { get; init; }
    public bool ReadOnly { get; init; }
    public string FieldType { get; init; }
}

public class WorkUnitPhoneNumberInputConfigResponseResponse
{
    public string Value { get; init; }
    public string TextInputType { get; init; }
}

public class WorkUnitPluginSignature
{
    public string PluginId { get; init; }
    public string SignatureType { get; init; }
}

public class WorkUnitPluginSignatureResponseResponse
{
    public string PluginId { get; init; }
    public string SignatureType { get; init; }
}

public class WorkUnitRadioButtonFieldResponse
{
    public string Id { get; init; }
    public long Page { get; init; }
    public double PositionX { get; init; }
    public double PositionY { get; init; }
    public double Width { get; init; }
    public double Height { get; init; }
    public string GroupName { get; init; }
    public bool ReadOnly { get; init; }
    public bool Checked { get; init; }
    public string Value { get; init; }
    public bool Required { get; init; }
    public string FieldType { get; init; }
}

public class WorkUnitRemoteCertificateSignature
{
    public string SignatureType { get; init; }
}

public class WorkUnitRemoteCertificateSignatureResponseResponse
{
    public string SignatureType { get; init; }
}

public class WorkUnitResponse
{
    public string Id { get; init; }
    public List<Anonymous103> Files { get; init; }
    public bool IsSequenceEnforced { get; init; }
    public bool IsFinished { get; init; }
}

public class WorkUnitSignatureFieldResponse
{
    public string Id { get; init; }
    public long Page { get; init; }
    public double PositionX { get; init; }
    public double PositionY { get; init; }
    public double Width { get; init; }
    public double Height { get; init; }
    public List<object> AllowedSignatureTypes { get; init; }
    public bool Required { get; init; }
    public bool ReadOnly { get; init; }
    public string FieldType { get; init; }
}

public class WorkUnitSignaturePosition
{
    public double X { get; init; }
    public double Y { get; init; }
    public double Width { get; init; }
    public double Height { get; init; }
}

public class WorkUnitSignaturePositionRequest
{
    public double X { get; init; }
    public double Y { get; init; }
    public double Width { get; init; }
    public double Height { get; init; }
}

public class WorkUnitStringInputConfigResponseResponse
{
    public string Value { get; init; }
    public bool Password { get; init; }
    public bool Multiline { get; init; }
    public long MaxLength { get; init; }
    public string TextInputType { get; init; }
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
    public double PositionX { get; init; }
    public double PositionY { get; init; }
    public double Width { get; init; }
    public double Height { get; init; }
    public Font Font { get; init; }
    public object TextInputConfig { get; init; }
    public bool Required { get; init; }
    public bool ReadOnly { get; init; }
    public string FieldType { get; init; }
}

public class WorkUnitTimeInputConfigResponse
{
    public string TextInputType { get; init; }
}

public class WorkUnitTypeToSignSignature
{
    public string SignatureType { get; init; }
}

public class WorkUnitTypeToSignSignatureRequest
{
    public string SignatureType { get; init; }
}

public class WorkUnitTypeToSignSignatureResponse
{
    public string SignatureType { get; init; }
}