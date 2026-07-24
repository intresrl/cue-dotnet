// Namirial Sign Nova API
//
// Namirial Sign Nova

import "time"

info: {
	title:       *"Namirial Sign Nova API" | string
	description: "Namirial Sign Nova"
	contact: name: "Namirial Sign Nova"
	version: *"v1" | string
}

#ATrustCertificateDto: close({
	phoneNumber?: null | string
})

#ATrustCertificateSignatureTypeDto: close({
	templateId?: null | string
	preferred?:  null | bool
	layoutId?:   null | string
})

#AbsoluteIntegrationExpirationDto: #IntegrationExpirationConfigurationDto & {
	...
} & close({
	expiresAt?: null | time.Time
	mode!:      "Absolute"
})

#AccessCode: close({
	code?: null | string
})

#Action: "Read" | "Write"

#AddDefaultUserGroupDto: close({
	userGroupId!: string
	defaultType!: #UserDefaultUserGroupDefaultType
})

#AddUserGroupUserDto: close({
	addedUsers!: [...string]
	skippedUsers!: [...string]
})

#AddUsersToUserGroupDto: close({
	userIds?: null | [...string]
})

#AddedEnvelopeFileResponse: close({
	id!: string
})

#AddedTemplateFileResponse: close({
	id!: string
})

#AdminMeDto: close({
	email!:           string
	givenName!:       string
	surname!:         string
	isInstanceAdmin!: bool
	isAdminUser!:     bool
	users!: [...#AdminMeUserDto]
})

#AdminMeUserDto: close({
	userId!:           string
	organizationId!:   string
	organizationName!: string
	isEnabled!:        bool
})

#Agreement: close({
	language!: string
	body!:     string
	title?:    null | string
})

#AgreementRequest: close({
	language!: string
	body!:     string
	title?:    null | string
})

#AgreementResponse: close({
	language!: string
	body!:     string
	title?:    null | string
})

#AgreementSettingsRequest: close({
	enabled!:     bool
	overridable!: bool
	agreements!: [...#AgreementRequest]
})

#AgreementSettingsResponse: close({
	enabled!:     bool
	overridable!: bool
	agreements!: [...#AgreementResponse]
})

#AllowedSignatureTypesDto: close({
	clickToSign?:           null | #ClickToSignSignatureTypeDto
	drawToSign?:            null | #DrawToSignSignatureTypeDto
	typeToSign?:            null | #TypeToSignSignatureTypeDto
	localCertificate?:      null | #LocalCertificateSignatureTypeDto
	disposableCertificate?: null | #DisposableCertificateSignatureTypeDto
	swissComOnDemand?:      null | #SwissComOnDemandSignatureTypeDto
	aTrustCertificate?:     null | #ATrustCertificateSignatureTypeDto
	biometric?:             null | #BiometricSignatureTypeDto
	remoteCertificate?:     null | #RemoteCertificateSignatureTypeDto
	oneTimePassword?:       null | #OneTimePasswordSignatureTypeDto
	swedishBankId?:         null | #SwedishBankIdSignatureTypeDto
	signaturePlugins?: null | [...#SignaturePluginSignatureTypeDto]
	automaticSignature?: null | #AutomaticSignatureTypeDto
})

#AnnotationElementDefinition: close({
	position!:   #FileElementsPosition
	size!:       #FileElementsSize
	textFormat!: #FileElementTextFormat
	valueFormat!: matchN(1, [#DateTimeDefinition, #InitialsDefinition, #TextDefinition, #FullNameDefinition, #FirstNameDefinition, #LastNameDefinition, #EmailDefinition])
})

#AnnotationElementDto: close({
	elementId!:         string
	elementDefinition!: #AnnotationElementDefinition
	source!:            #FormFieldSource
	recipientId?:       null | string
	elementName?:       null | string
})

#AnnotationField: #BaseField & {
	...
} & close({
	valueFormat!: matchN(1, [#DateTimeDefinition, #InitialsDefinition, #TextDefinition, #FullNameDefinition, #FirstNameDefinition, #LastNameDefinition, #EmailDefinition])
	font?:        null | #FontStyle
	elementName?: null | string
	fieldType!:   "Annotation"
})

#AnnotationFieldDto: #BaseFieldDto & {
	...
} & close({
	page!:      int32 & int
	positionX!: number
	positionY!: number
	width!:     number
	height!:    number
	annotationConfig!: matchN(1, [#FullNameAnnotationConfigDto, #FirstNameAnnotationConfigDto, #LastNameAnnotationConfigDto, #InitialsAnnotationConfigDto, #EmailAnnotationConfigDto, #DateAnnotationConfigDto, #TextAnnotationConfigDto])
	font?:        null | #FontStyle
	elementName?: null | string
	fieldType!:   "Annotation"
})

#AnnotationType: "FullName" | "FirstName" | "LastName" | "Initials" | "Email" | "Date" | "Text"

#AnnotationValueFormat: "FullName" | "FirstName" | "LastName" | "Initials" | "Email" | "Date" | "Text"

#ApprovalField: #BaseField & {
	...
} & close({
	fieldType!: "Approval"
})

#ApprovalFieldDto: #BaseFieldDto & {
	...
} & close({
	page!:      int32 & int
	positionX!: number
	positionY!: number
	width!:     number
	height!:    number
	required!:  bool
	fieldType!: "Approval"
})

#ApproveElementDto: close({
	elementId!:         string
	elementDefinition!: #SignatureElementDefinition
	source!:            #FormFieldSource
	recipientId?:       null | string
	required!:          bool
	displayName?:       null | string
	guidingOrder!:      int32 & int
})

#AreaReadConfirmationDto: close({
	elementId!:         string
	required!:          bool
	elementDefinition!: #AreaReadElementDefinition
	source!:            #FormFieldSource
	recipientId?:       null | string
	displayName?:       null | string
	guidingOrder!:      int32 & int
})

#AreaReadConfirmationField: #BaseField & {
	...
} & close({
	displayName?: null | string
	fieldType!:   "AreaReadConfirmation"
})

#AreaReadConfirmationFieldDto: #BaseFieldDto & {
	...
} & close({
	page!:        int32 & int
	positionX!:   number
	positionY!:   number
	width!:       number
	height!:      number
	required!:    bool
	displayName?: null | string
	fieldType!:   "AreaReadConfirmation"
})

#AreaReadConfirmationTaskUpdateRequest: close({
	fieldType!: "AreaReadConfirmation"
})

#AreaReadElementDefinition: close({
	position!: #FileElementsPosition
	size!:     #FileElementsSize
})

#AssociateMyNamirialIdDto: close({
	myNamirialId!: string
})

#AttachmentElementDefinition: close({
	position!: #FileElementsPosition
	size!:     #FileElementsSize
})

#AttachmentElementDto: close({
	elementId!:         string
	required!:          bool
	elementDefinition!: #AttachmentElementDefinition
	source!:            #FormFieldSource
	recipientId?:       null | string
	label!:             string
	guidingOrder!:      int32 & int
})

#AttachmentField: #BaseField & {
	...
} & close({
	label!:     string
	fieldType!: "Attachment"
})

#AttachmentFieldDto: #BaseFieldDto & {
	...
} & close({
	page!:      int32 & int
	positionX!: number
	positionY!: number
	width!:     number
	height!:    number
	required!:  bool
	fieldType!: "Attachment"
})

#AttachmentTaskUpdateRequest: close({
	fileName!:  string
	content!:   string
	fieldType!: "Attachment"
})

#AuditTrailModeResponse: close({
	auditTrailMode!: #EnvelopeLogGeneration
})

#AutomaticESealingPermissions: close({
	createUpdateDelete!: bool
})

#AutomaticSealingProfileDetailResponse: close({
	id!:       string
	name!:     string
	username!: string
	password!: string
})

#AutomaticSealingProfileRequest: close({
	name!:     string
	username!: string
	password!: string
})

#AutomaticSealingProfileResponse: close({
	id!:   string
	name!: string
})

#AutomaticSignature: close({
	layoutId?:      null | string
	signatureType!: "AutomaticSignature"
})

#AutomaticSignatureDataDto: close({
	profileId?: null | string
	pluginId?:  null | string
})

#AutomaticSignatureTypeDto: close({
	useExternalSignatureImage?: null | #ExternalSignatureImageMode
	preferred?:                 null | bool
	layoutId?:                  null | string
	stampImprintConfiguration?: null | #ClickToSignStampImprintDto
})

#BackgroundImageDto: close({
	mimeType!:   string
	dataBase64!: string
})

#BankIdSettingsDto: close({
	authenticationCertificateThumbprint?: null | string
})

#BaseField: close({
	id!:        string
	page!:      int32 & int
	positionX!: number
	positionY!: number
	width!:     number
	height!:    number
	required!:  bool
	source!:    #FormFieldSource
})

#BaseFieldDto: close({
	id!: string
})

#BatchAssignUserGroupUserRoleDto: close({
	userIds!: [...string]
	businessRoleId!: string
})

#BatchDeleteUserGroupUserRoleDto: close({
	userIds!: [...string]
})

#BatchMode: "Basic" | "OptIn" | "OptOut" | "OptOutWithRequiredAlwaysSelected" | "OptInWithRequiredAlwaysSelected"

#BiometricSignature: close({
	signatureType!: "BiometricSignature"
})

#BiometricSignaturePositioning: "WithinField" | "OnPage" | "IntersectsWithField"

#BiometricSignatureTypeDto: close({
	biometricVerification?:             null | bool
	allowBiometricStoringOnly?:         null | bool
	storeSignedResponseWithoutBioData?: null | bool
	biometricServerUserId?:             null | string
	signaturePositioning?:              null | #BiometricSignaturePositioning
	preferred?:                         null | bool
	layoutId?:                          null | string
})

#BulkEnvelopeDetailDto: close({
	id!:   string
	name!: string
	stages!: [...#BulkStageDto]
	documents?: null | [...#Document]
})

#BulkEnvelopeFieldTaskItem: close({
	field!: matchN(1, [#SignatureFieldDto, #TextFieldDto, #CheckboxFieldDto, #DropDownFieldDto, #ListBoxFieldDto, #AttachmentFieldDto, #AnnotationFieldDto, #LinkFieldDto, #FileReadConfirmationFieldDto, #PageReadConfirmationFieldDto, #AreaReadConfirmationFieldDto, #RadioButtonFieldDto, #ApprovalFieldDto, #InvisibleSignatureFieldDto])
	sortOrder!:   int32 & int
	recipientId?: null | string
	source!:      #ElementSource
	stageId?:     null | string
})

#BulkEnvelopeFieldTaskItemRequest: close({
	field!: matchN(1, [#SignatureFieldDto, #TextFieldDto, #CheckboxFieldDto, #DropDownFieldDto, #ListBoxFieldDto, #AttachmentFieldDto, #AnnotationFieldDto, #LinkFieldDto, #FileReadConfirmationFieldDto, #PageReadConfirmationFieldDto, #AreaReadConfirmationFieldDto, #RadioButtonFieldDto, #ApprovalFieldDto, #InvisibleSignatureFieldDto])
	sortOrder!:   int32 & int
	recipientId?: null | string
	stageId?:     null | string
})

#BulkEnvelopeFileTasksResponse: close({
	tasks!: [...#BulkEnvelopeFieldTaskItem]
})

#BulkEnvelopeListDto: close({
	bulkEnvelopes!: [...#BulkEnvelopePartialDto]
	pagination!: #PaginationDto
})

#BulkEnvelopePartialDto: close({
	id!:        string
	name!:      string
	status!:    string
	createdAt!: time.Time
	updatedAt!: time.Time
})

#BulkRecipientDefinition: close({
	givenName!:   string
	surname!:     string
	email!:       string
	phoneNumber?: null | string
})

#BulkRecipientDto: close({
	id!:                  string
	givenName?:           null | string
	surname?:             null | string
	email?:               null | string
	phoneNumber?:         null | string
	recipientType!:       #RecipientType
	notificationChannel!: #NotificationChannel
	order!:               int32 & int
})

#BulkRecipientValidationErrorResponse: close({
	errors!: [...#RowError]
})

#BulkStageDto: close({
	id!:                        string
	mandatoryRecipientsNumber!: int32 & int
	name?:                      null | string
	stageMode!:                 #StageMode
	recipients?: null | [...#BulkRecipientDto]
})

#BusinessRoleCreateDto: close({
	name!:        string
	description?: null | string
})

#BusinessRoleDto: close({
	id!:              string
	organizationId!:  string
	name!:            string
	description?:     null | string
	assignmentCount!: int32 & int
	createdAt!:       time.Time
	updatedAt!:       time.Time
})

#BusinessRoleUpdateDto: close({
	name!:        string
	description?: null | string
})

#BusinessRolesListDto: close({
	items!: [...#BusinessRoleDto]
	pagination!: #PaginationDto
})

#BusinessRolesSortingKey: "Name"

#CallbackConfigurationDto: close({
	callbackUrl?:             null | string
	statusUpdateCallbackUrl?: null | string
	afterSendCallbackUrl?:    null | string
})

#CertificateDetailsResponse: close({
	subjectName!:    string
	thumbprint!:     string
	expirationDate!: time.Time
	issuer!:         string
})

#CheckBoxElementDefinition: close({
	position!:    #FileElementsPosition
	size!:        #FileElementsSize
	exportValue!: string
	readOnly!:    bool
})

#CheckBoxElementDto: close({
	elementId!:         string
	elementDefinition!: #CheckBoxElementDefinition
	source!:            #FormFieldSource
	required!:          bool
	isChecked!:         bool
	recipientId?:       null | string
	guidingOrder!:      int32 & int
})

#CheckboxField: #BaseField & {
	...
} & close({
	readOnly!:  bool
	checked!:   bool
	value!:     string
	fieldType!: "Checkbox"
})

#CheckboxFieldDto: #BaseFieldDto & {
	...
} & close({
	page!:      int32 & int
	positionX!: number
	positionY!: number
	width!:     number
	height!:    number
	checked!:   bool
	value!:     string
	required!:  bool
	readOnly!:  bool
	fieldType!: "Checkbox"
})

#CheckboxTaskUpdateRequest: close({
	isChecked!: bool
	fieldType!: "Checkbox"
})

#ClickToSignEnvelopeBulkSignDto: #EnvelopeBulkSignDto & {
	...
} & close({
	signatureType!: "ClickToSign"
})

#ClickToSignSignature: close({
	layoutId?:      null | string
	signatureType!: "ClickToSignSignature"
})

#ClickToSignSignatureTypeDto: close({
	useExternalSignatureImage?: null | #ExternalSignatureImageMode
	preferred?:                 null | bool
	layoutId?:                  null | string
	stampImprintConfiguration?: null | #ClickToSignStampImprintDto
})

#ClickToSignStampImprintDto: close({
	displayName!:             bool
	displaySignatureDate!:    bool
	displayExtraInformation!: bool
	fontName?:                null | string
	fontSizeInPt?:            null | int32 & int
	displayEmail!:            bool
	displayIp!:               bool
})

#ClonedEnvelopeDto: close({
	id!: string
})

#ContactDto: close({
	id!:             string
	givenName!:      string
	surname!:        string
	email!:          string
	cultureIsoCode!: string
	phoneNumber?:    null | string
})

#ContactImportResultDto: close({
	imported!: int32 & int
})

#ContactImportValidationErrorResponse: close({
	errors!: [...#RowError]
})

#ContactListDto: close({
	contacts!: [...#ContactDto]
	pagination!: #PaginationDto
})

#ContactRequest: close({
	givenName!:      string
	surname!:        string
	email!:          string
	cultureIsoCode!: string
	phoneNumber?:    null | string
})

#ContactsSortingKey: "GivenName" | "Surname" | "Email"

#CountriesDto: close({
	options!: [...#CountryListItemDto]
	selectedId?:      null | int32 & int @deprecated()
	selectedIsoCode?: null | string
})

#CountriesLookupResponse: close({
	countries!: [...#CountryDto]
})

#CountryDto: close({
	name!: string
	code!: string
})

#CountryListItemDto: close({
	id!:          int32 & int @deprecated()
	isoCode!:     string
	englishName!: string
})

#CreateATrustCertificateDto: close({
	phoneNumber?: null | string
})

#CreateAccessCodeDto: close({
	code?: null | string
})

#CreateAuthenticationConfigurationDto: close({
	accessCode?:         null | #CreateAccessCodeDto
	smsOneTimePassword?: null | #CreateSmsOneTimePasswordDto
	oAuthAuthentications?: null | [...#CreateOAuthAuthenticationDto]
})

#CreateAutomaticSignatureDataDto: close({
	profileId?: null | string
	pluginId?:  null | string
})

#CreateBulkEnvelopeStageRequest: close({
	type!:                         #EnvelopeStageType
	name?:                         null | string
	requiredRecipientCompletions?: null | int32 & int
	mode!:                         #StageMode
})

#CreateDisposableCertificateDto: close({
	documentIssuingCountry?:       null | string
	identificationIssuingCountry?: null | string
	identificationType?:           null | string
	phoneNumber?:                  null | string
	documentType?:                 null | string
	documentIssuedBy?:             null | string
	documentIssuedOn?:             null | time.Time
	documentExpiryDate?:           null | time.Time
	serialNumber?:                 null | string
	documentNumber?:               null | string
})

#CreateDocumentClassRequest: close({
	name!:        string
	description!: string
	metadata!: [...#DocumentClassMetadataFieldDto]
})

#CreateEnvelopeStageAutomaticRecipientRequest: #CreateEnvelopeStageRecipientRequest & {
	...
} & close({
	signatureProfile?:           null | string
	signatureReasonAllowChange?: null | bool
	metadata?: null | [...#RecipientMetadataEntry]
	type!: "Automatic"
})

#CreateEnvelopeStageRecipientRequest: close({
	languageCode?:    null | string
	signatureReason?: null | string
})

#CreateEnvelopeStageRequest: close({
	type!:                         #EnvelopeStageType
	name?:                         null | string
	requiredRecipientCompletions?: null | int32 & int
})

#CreateEnvelopeStageStandardRecipientRequest: #CreateEnvelopeStageRecipientRequest & {
	...
} & close({
	givenName?:                  null | string
	surname?:                    null | string
	email?:                      null | string
	phoneNumber?:                null | string
	notificationChannel?:        null | #NotificationChannel
	authentication?:             null | #CreateAuthenticationConfigurationDto
	signatureConfiguration?:     null | #CreateSignatureDataConfigurationDto
	personalMessage?:            null | string
	signatureReasonAllowChange?: null | bool
	metadata?: null | [...#RecipientMetadataEntry]
	type!: "Standard"
})

#CreateGenericSigningPluginsSenderDataDto: close({
	senderGenericSigningPlugins?: null | [...#CreateSenderGenericSigningPluginDto]
})

#CreateOAuthAuthenticationDto: close({
	providerName?: null | string
	externalId!:   string
})

#CreateOAuthFieldDefinitionRequest: close({
	path!:                         string
	mode!:                         #OAuthSignerProviderFieldMode
	target!:                       #OAuthSignerProviderFieldTarget
	customFieldName?:              null | string
	genericSigningPluginId?:       null | string
	genericSigningPluginFieldKey?: null | string
})

#CreateOAuthJwtConfigRequest: close({
	jwksUri!:          string
	issuer!:           string
	enforceNonce!:     bool
	validateAudience!: bool
	validateIssuer!:   bool
	validateLifetime!: bool
	oAuthFieldDefinitions?: null | [...#CreateOAuthFieldDefinitionRequest]
})

#CreateOAuthResourceUriRequest: close({
	uri!:                   string
	accessTokenParamName!:  string
	eIdServiceCombination?: null | string
	oAuthFieldDefinitions?: null | [...#CreateOAuthFieldDefinitionRequest]
})

#CreateOAuthSignerProviderDetailsRequest: close({
	oAuthSignerProvider!: #CreateOAuthSignerProviderRequest
	oAuthJwtConfig?:      null | #CreateOAuthJwtConfigRequest
	oAuthResourceUris?: null | [...#CreateOAuthResourceUriRequest]
})

#CreateOAuthSignerProviderRequest: close({
	name!:             string
	clientId!:         string
	clientSecret!:     string
	authorizationUri!: string
	tokenUri!:         string
	scope?:            null | string
	logoutUri?:        null | string
})

#CreateOrganizationDto: close({
	name!:                                  string
	isoCulture!:                            string
	license!:                               #LicenseDto
	onePlatformBusinessRelationIdentifier!: string
	featureFlagsNames!: [...string]
})

#CreateOrganizationUserRequestDto: close({
	givenName!:        string
	surname!:          string
	email!:            string
	regionalSettings!: #UserRegionalSettingsRequestDto
	phoneNumber?:      null | string
})

#CreateOrganizationUserResponse: close({
	id!: string
})

#CreateOtpSignatureDataDto: close({
	type?:        null | #OtpDeliveryChannel
	phoneNumber?: null | string
})

#CreatePersonalAccessTokenRequest: close({
	name!:      string
	expiresAt!: time.Time
})

#CreatePolicyRequest: close({
	name!:            string
	isActive!:        bool
	description?:     null | string
	documentClassId?: null | string
	conditions?: null | [...#PolicyConditionRequest]
})

#CreateRemoteCertificateDto: close({
	userId?:   null | string
	deviceId?: null | string
})

#CreateRoleRequest: close({
	name!: string
	permissions!: [...#PermissionDto]
	description?: null | string
})

#CreateSenderGenericSigningPluginDto: close({
	pluginId?: null | string
	settings?: null | [...#CreateSenderGenericSigningPluginSettingsDto]
})

#CreateSenderGenericSigningPluginSettingsDto: close({
	key?:   null | string
	value?: null | string
})

#CreateServiceAccountRequest: close({
	clientId!:         string
	email!:            string
	regionalSettings!: #UserRegionalSettingsDto
})

#CreateServiceAccountResponse: close({
	clientId!:     string
	clientSecret!: string
	userId!:       string
})

#CreateSignatureDataConfigurationDto: close({
	disposableCertificate?:           null | #CreateDisposableCertificateDto
	remoteCertificate?:               null | #CreateRemoteCertificateDto
	aTrustCertificate?:               null | #CreateATrustCertificateDto
	swissComOnDemand?:                null | #CreateSwissComOnDemandDto
	swedishBankId?:                   null | #CreateSwedishBankIdDto
	otpSignatureData?:                null | #CreateOtpSignatureDataDto
	genericSigningPluginsSenderData?: null | #CreateGenericSigningPluginsSenderDataDto
	automaticSignatureData?:          null | #CreateAutomaticSignatureDataDto
})

#CreateSmsOneTimePasswordDto: close({
	phoneNumber?: null | string
})

#CreateStageResponse: close({
	id!: string
})

#CreateSubstituteDelegationDto: close({
	delegateeUserEmail!:        string
	utilizeAlsoOnCCRecipients!: bool
	reason?:                    null | string
	startDate?:                 null | time.Time
	endDate?:                   null | time.Time
})

#CreateSwedishBankIdDto: close({
	personalNumber?:         null | string
	allowAnyPersonalNumber?: null | bool
})

#CreateSwissComOnDemandDto: close({
	commonName?:   null | string
	country?:      null | string
	phoneNumber?:  null | string
	organization?: null | string
	organizationUnits?: null | [...string]
	locality?:        null | string
	serialNumber?:    null | string
	stateOrProvince?: null | string
	pseudonym?:       null | string
})

#CreateTemplateStageAutomaticRecipientRequest: #CreateTemplateStageRecipientRequest & {
	...
} & close({
	signatureProfile?:           null | string
	signatureReasonAllowChange?: null | bool
	metadata?: null | [...#RecipientMetadataEntry]
	type!: "Automatic"
})

#CreateTemplateStageRecipientRequest: close({
	languageCode?:    null | string
	signatureReason?: null | string
})

#CreateTemplateStageRequest: close({
	type!:                         #EnvelopeStageType
	name?:                         null | string
	requiredRecipientCompletions?: null | int32 & int
})

#CreateTemplateStageStandardRecipientRequest: #CreateTemplateStageRecipientRequest & {
	...
} & close({
	givenName?:                  null | string
	surname?:                    null | string
	email?:                      null | string
	phoneNumber?:                null | string
	notificationChannel?:        null | #NotificationChannel
	authentication?:             null | #CreateAuthenticationConfigurationDto
	signatureConfiguration?:     null | #CreateSignatureDataConfigurationDto
	personalMessage?:            null | string
	signatureReasonAllowChange?: null | bool
	isDelegationEnabled!:        bool
	metadata?: null | [...#RecipientMetadataEntry]
	type!: "Standard"
})

#CreateUserDto: close({
	givenName!:   string
	surname!:     string
	email!:       string
	isoLanguage!: string
	enabled!:     bool
	roleNames!: [...#UserRoleRequest]
})

#CreatedDocumentClassDto: close({
	id!: string
})

#CreatedEnvelopeDto: close({
	id!: string
})

#CreatedEnvelopeFromTemplateDto: close({
	createdEnvelopeId!: string
})

#CreatedOrganizationDto: close({
	id!: string
})

#CreatedPersonalAccessTokenResponse: close({
	id!:        string
	name!:      string
	token!:     string
	createdAt!: time.Time
	expiresAt!: time.Time
})

#CreatedPolicyResponse: close({
	id!: string
})

#CreatedRecipientResponse: close({
	id!: string
})

#CreatedStageResponse: close({
	id!: string
})

#CreatedTemplateDto: close({
	id!: string
})

#CreatedTemplateStageRecipientDto: close({
	id!: string
})

#CreatedUserDto: close({
	id!: string
})

#DataFieldType: "Text" | "PhoneNumber" | "Number" | "List" | "Email" | "Password"

#DateAnnotationConfigDto: close({
	format!:         #DateFormatSwaggerEnumProvider
	annotationType!: "Date"
})

#DateFormatSwaggerEnumProvider: "dd.MM.yy" | "dd.MM.yyyy"

#DateInputConfig: #TextInputConfig & {
	...
} & close({
	value?:         null | time.Format("2006-01-02")
	format?:        null | #DateFormatSwaggerEnumProvider
	minValue?:      null | time.Format("2006-01-02")
	maxValue?:      null | time.Format("2006-01-02")
	textInputType!: "DateType"
})

#DateTimeDefinition: close({
	dateTimeFormat!: string
	valueFormat!:    "Date"
})

#DateTimeFormatDto: close({
	code!:    string
	example!: string
})

#DateTimeFormatSwaggerEnumProvider: "dd/MM/yyyy | HH:mm" | "dd/MM/yy | HH:mm" | "dd-MMM-yy | HH:mm" | "dd-MMM-yyyy | HH:mm" | "dd MMMM yyyy | HH:mm" | "yyyy-MM-dd | HH:mm" | "yyyy-MMM-dd | HH:mm" | "yyyy MMMM dd | HH:mm" | "MMM d, yyyy | HH:mm" | "MMM-dd-yyyy | HH:mm" | "MMMM d, yyyy | HH:mm" | "MM/d/yyyy | HH:mm" | "dd.MM.yyyy | HH:mm" | "dd. MMMM yyyy | HH:mm" | "dd.MM.yy | HH:mm"

#DateTimeFormatsDto: close({
	options!: [...#DateTimeOptionDto]
	selectedId?:   null | int32 & int @deprecated()
	selectedName?: null | string
})

#DateTimeFormatsLookupResponse: close({
	dateTimeFormats!: [...#DateTimeFormatDto]
})

#DateTimeOptionDto: close({
	id!:     int32 & int @deprecated()
	name!:   string
	sample!: string
})

#DbEnvelopeStatus: "Started" | "InProgress" | "Canceled" | "Completed" | "Expired" | "Rejected" | "Draft" | "Template"

#DbRecipientType: "Signer" | "CC" | "Acknowledge" | "Pkcs7Signer" | "Automatic" | "Approver"

#DbWorkstepResult: "NotSigned" | "Signed" | "Rejected" | "Delegated" | "DelegatedAutomated"

#DecimalSeparatorType: "Comma" | "Point" | "Apostrophe" | "None"

#DefaultUserGroupsDto: close({
	envelopesShare!: [...#UserDefaultUserGroup]
	templatesShare!: [...#UserDefaultUserGroup]
})

#DelegationInfo: close({
	enabled!:                 bool
	defaultDelegationPolicy?: null | #DelegationPolicy
})

#DelegationPolicy: "Deactivated" | "ActivatedWithDefaultOff" | "ActivatedWithDefaultOn"

#DisposableCertificateDto: close({
	documentIssuingCountry?:       null | string
	identificationIssuingCountry?: null | string
	identificationType?:           null | string
	phoneNumber?:                  null | string
	documentType?:                 null | string
	documentIssuedBy?:             null | string
	documentIssuedOn?:             null | time.Time
	documentExpiryDate?:           null | time.Time
	serialNumber?:                 null | string
	documentNumber?:               null | string
})

#DisposableCertificateSettingsDto: close({
	lraId?:                                         null | string
	user?:                                          null | string
	hasPassword!:                                   bool
	disposableType?:                                null | #DisposableType
	showDisclaimerBeforeCertificateRequest!:        bool
	sendDisposableDisclaimerDocumentNotifications!: bool
})

#DisposableCertificateSignature: close({
	layoutId?:      null | string
	signatureType!: "DisposableCertificateSignature"
})

#DisposableCertificateSignatureTypeDto: close({
	useExternalSignatureImage?: null | #ExternalSignatureImageMode
	preferred?:                 null | bool
	layoutId?:                  null | string
	stampImprintConfiguration?: null | #DisposableCertificateStampImprintDto
	isLongLived?:               null | bool
	validityInSeconds?:         null | int32 & int
})

#DisposableCertificateStampImprintDto: close({
	displayName!:             bool
	displaySignatureDate!:    bool
	displayExtraInformation!: bool
	fontName?:                null | string
	fontSizeInPt?:            null | int32 & int
	displayIp!:               bool
})

#DisposableType: "Disposable" | "LeanDisposable" | "LeanDisposableExtendedValidity"

#Document: close({
	id!:              string
	name!:            string
	sortOrder!:       int32 & int
	documentClassId?: null | string
})

#DocumentClassDto: close({
	id!:          string
	name!:        string
	description?: null | string
	metadata?: null | [...#DocumentClassMetadataDto]
})

#DocumentClassListItemDto: close({
	id!:                 string
	name!:               string
	description?:        null | string
	associatedPolicies?: null | string
})

#DocumentClassLookupResponse: close({
	id!:   string
	name!: string
})

#DocumentClassMetadataDto: close({
	id!:        string
	name!:      string
	dataType!:  #MetadataDataType
	required!:  bool
	sortOrder!: int32 & int
})

#DocumentClassMetadataFieldDto: close({
	name!:      string
	dataType!:  #MetadataDataType
	required!:  bool
	sortOrder!: int32 & int
})

#DocumentClassesResponse: close({
	documentClasses!: [...#DocumentClassListItemDto]
	pagination!: #PaginationResponse
})

#DocumentClassesSortingKey: "Name" | "AssociatedPolicies"

#DocumentReadConfirmationDto: close({
	elementId!:    string
	required!:     bool
	recipientId?:  null | string
	guidingOrder!: int32 & int
	displayName?:  null | string
})

#DocumentsUploadRequest: close({
	files!: [...string]
})

#DrawToSignSignature: close({
	layoutId?:      null | string
	signatureType!: "DrawToSignSignature"
})

#DrawToSignSignatureTypeDto: close({
	useExternalSignatureImage?: null | #ExternalSignatureImageMode
	preferred?:                 null | bool
	layoutId?:                  null | string
	stampImprintConfiguration?: null | #DrawToSignStampImprintDto
})

#DrawToSignStampImprintDto: close({
	displayName!:             bool
	displaySignatureDate!:    bool
	displayExtraInformation!: bool
	fontName?:                null | string
	fontSizeInPt?:            null | int32 & int
	displayEmail!:            bool
	displayIp!:               bool
})

#DropDownElementDefinition: close({
	position!:   #FileElementsPosition
	size!:       #FileElementsSize
	readOnly!:   bool
	textFormat!: #FileElementTextFormat
})

#DropDownElementDto: close({
	elementDefinition!: #DropDownElementDefinition
	source!:            #FormFieldSource
	elementId!:         string
	recipientId?:       null | string
	required!:          bool
	editable?:          null | bool
	value?:             null | string
	guidingOrder!:      int32 & int
	items?: null | [...#DropDownItemEntry]
})

#DropDownField: #BaseField & {
	...
} & close({
	readOnly!: bool
	font?:     null | #FontStyle
	options?: null | [...#Option]
	editable?:      null | bool
	selectedValue?: null | string
	fieldType!:     "DropDown"
})

#DropDownFieldDto: #BaseFieldDto & {
	...
} & close({
	page!:      int32 & int
	positionX!: number
	positionY!: number
	width!:     number
	height!:    number
	font!:      #FontStyle
	options?: null | [...#OptionDto]
	required!:  bool
	readOnly!:  bool
	fieldType!: "DropDown"
})

#DropDownItemEntry: close({
	value!:      string
	label!:      string
	isSelected?: null | bool
})

#DropDownTaskUpdateRequest: close({
	value!:     string
	fieldType!: "DropDown"
})

#ESealingRemoteSignatureProfileDto: close({
	id!:           string
	friendlyName?: null | string
})

#ElementSource: "File" | "UserDefined"

#EmailAnnotationConfigDto: close({
	annotationType!: "Email"
})

#EmailDefinition: close({
	valueFormat!: "Email"
})

#EmailSenderDisplayType: "SenderName" | "Organization" | "ProductName"

#EnableOrganizationDto: close({
	onePlatformBusinessRelationIdentifier!: string
})

#EnabledOrganizationDto: close({
	id!: string
})

#Entity: "Envelope" | "Template" | "Organization" | "User" | "UserGroup"

#EnvelopeAction: "Sign" | "View" | "Remind" | "Download" | "Delete" | "Restart" | "Cancel" | "Continue" | "Clone" | "Approve" | "Unlock" | "Share"

#EnvelopeActionResponse: close({
	envelopeId!: string
	statusCode!: int32 & int
	message?:    null | string
})

#EnvelopeActorDto: close({
	email!: string
})

#EnvelopeBacklogDto: close({
	id!:         string
	name!:       string
	senderName!: string
	sentDate!:   time.Time
})

#EnvelopeBulkSignDeviceDto: close({
	deviceId!:                  string
	otpDeviceType!:             string
	otpDeviceTypeId!:           string
	identificationInformation!: string
})

#EnvelopeBulkSignDevicesResponseDto: close({
	devices!: [...#EnvelopeBulkSignDeviceDto]
})

#EnvelopeBulkSignDto: close({
	envelopeIds!: [...string]
	ipAddress?: null | string
})

#EnvelopeBulkSignResultDto: close({
	signedEnvelopes!: [...string]
	failedEnvelopes!: [...#FailedEnvelope]
})

#EnvelopeBulkSignSignatureType: "ClickToSign" | "RemoteCertificate"

#EnvelopeBulkSignTransactionDto: close({
	transactionId!: string
	payloadFileId!: string
	expiresAt?:     null | time.Time
})

#EnvelopeCancelRequestDto: close({
	reason?: null | string
})

#EnvelopeDetailDto: close({
	id!:                      string
	name!:                    string
	status!:                  #EnvelopeDetailStatus
	expiringSoon!:            bool
	sendCopyToAllRecipients!: bool
	actions!: [...#EnvelopeAction]
	updatedAt!:      time.Time
	sentAt?:         null | time.Time
	expirationDate?: null | time.Time
	defaultAction?:  null | #EnvelopeAction
	documents?: null | [...#Document]
	stages?: null | [...#EnvelopeDetailStageDto]
	preventFieldsEditingWhenFinished!: bool
})

#EnvelopeDetailRecipientDto: close({
	id!:                           string
	givenName!:                    string
	surname!:                      string
	email!:                        string
	placeholder?:                  null | string
	order?:                        null | int32 & int
	type?:                         null | #RecipientType
	status?:                       null | #RecipientStatus
	statusReason?:                 null | string
	lastAction?:                   null | #LastRecipientAction
	lastActionDate?:               null | time.Time
	viewerLink?:                   null | string
	stageId?:                      null | string
	signatureProfile?:             null | string
	requiresDelegationCompletion!: bool
})

#EnvelopeDetailStageDto: close({
	id!:                           string
	sortOrder!:                    int32 & int
	requiredRecipientCompletions!: int32 & int
	recipients!: [...#EnvelopeDetailRecipientDto]
	name?: null | string
})

#EnvelopeDetailStatus: "WaitingForYou" | "WaitingForOthers" | "Completed" | "Rejected" | "Expired" | "Canceled" | "Draft"

#EnvelopeDownloadDto: close({
	id!:   string
	name!: string
	type!: string
})

#EnvelopeDownloadsResponse: close({
	downloads!: [...#EnvelopeDownloadDto]
})

#EnvelopeDto: close({
	id!:                                                             string
	name?:                                                           null | string
	defaultSubject?:                                                 null | string
	defaultBody?:                                                    null | string
	sendCopyToAllRecipients!:                                        bool
	lateIdent!:                                                      bool
	useInvisibleSignatureWithTimestampForAllDocumentsAndRecipients!: bool
	showOrganizationAgreements!:                                     bool
	reminderConfiguration!:                                          #ReminderConfigurationDto
	expirationConfiguration!:                                        #ExpirationConfigurationDto
	recipients?: null | [...#RecipientDto]
	stages?: null | [...#StageDto]
	documents?: null | [...#Document]
	agreements?: null | [...#Agreement]
	userGroupSharingIds!: [...string]
	callbackConfiguration?:            null | #CallbackConfigurationDto
	status!:                           #DbEnvelopeStatus
	createdAt!:                        time.Time
	updatedAt!:                        time.Time
	preventFieldsEditingWhenFinished!: bool
	afterSendRedirectUrl?:             null | string
	signatureReason?:                  null | string
	signatureReasonAllowChange!:       bool
	signatureFormat!:                  #SignatureFormat
	fileRestrictedVisibility!:         bool
})

#EnvelopeEventDto: close({
	id!:         string
	type!:       #EnvelopeEventType
	occurredAt!: time.Time
	actor!:      #EnvelopeActorDto
	data!: [string]: string
})

#EnvelopeEventType: "Created" | "Canceled" | "Completed" | "Deleted" | "NotificationSent" | "Rejected" | "Sent" | "WorkstepCompleted" | "StartSending" | "StartRestarting" | "Restarted"

#EnvelopeEventsDto: close({
	events!: [...#EnvelopeEventDto]
})

#EnvelopeFieldTaskItem: close({
	field!: matchN(1, [#SignatureFieldDto, #TextFieldDto, #CheckboxFieldDto, #DropDownFieldDto, #ListBoxFieldDto, #AttachmentFieldDto, #AnnotationFieldDto, #LinkFieldDto, #FileReadConfirmationFieldDto, #PageReadConfirmationFieldDto, #AreaReadConfirmationFieldDto, #RadioButtonFieldDto, #ApprovalFieldDto, #InvisibleSignatureFieldDto])
	sortOrder!:   int32 & int
	recipientId?: null | string
	source!:      #ElementSource
})

#EnvelopeFileDetailDocumentClassDto: close({
	documentClassId!: string
	name?:            null | string
	metadataValues?: null | [...#EnvelopeFileMetadataValueDto]
})

#EnvelopeFileDetailDocumentClassRequest: close({
	documentClassId!: string
	metadataValues!: [...#MetadataValueDto]
})

#EnvelopeFileMetadataValueDto: close({
	fieldDefinitionId!: string
	name?:              null | string
	value?:             null | string
	type!:              #MetadataDataType
})

#EnvelopeFileTasksResponse: close({
	tasks!: [...#EnvelopeFieldTaskItem]
})

#EnvelopeFilesResponse: close({
	files!: [...#Document]
})

#EnvelopeInsights: close({
	waitingForYou!:    #SingleInsight
	waitingForOthers!: #SingleInsight
	draft!:            #SingleInsight
	completed!:        #SingleInsight
	rejected!:         #SingleInsight
	expired!:          #SingleInsight
})

#EnvelopeListDto: close({
	envelopes!: [...#EnvelopePartialDto]
	pagination!: #PaginationDto
})

#EnvelopeLogGeneration: "Standard" | "PDFA2B" | "Disabled"

#EnvelopePartialDto: close({
	id!:           string
	name!:         string
	expiringSoon!: bool
	senderUser!:   #EnvelopeSenderDto
	updatedAt!:    time.Time
	status!:       #EnvelopeDetailStatus
	actions!: [...#EnvelopeAction]
	createdAt!: time.Time
	sentAt?:    null | time.Time
	recipients?: null | [...#EnvelopeDetailRecipientDto]
	defaultAction?: null | #EnvelopeAction
})

#EnvelopePermissions: close({
	read!:               bool
	createUpdateDelete!: bool
})

#EnvelopePoliciesVerifyDto: close({
	compliant!: bool
})

#EnvelopeRejectDto: close({
	message?: null | string
})

#EnvelopeResumeDto: close({
	newExpirationDate?:               null | time.Time
	expirationInSecondsAfterSending?: null | int64 & int
})

#EnvelopeSenderDto: close({
	givenName?: null | string
	surname?:   null | string
	email?:     null | string
})

#EnvelopeSignatureTypeDto: close({
	id!: string
	signatureTypes!: [...#SignatureType]
	canBeSignedInBulk!: bool
})

#EnvelopeSignatureTypesRequestDto: close({
	ids!: [...string]
})

#EnvelopeSortingKey: "LastUpdated" | "Name"

#EnvelopeStageAutomaticRecipientResponse: #EnvelopeStageRecipientResponse & {
	...
} & close({
	signatureProfile?:           null | string
	signatureReason?:            null | string
	signatureReasonAllowChange?: null | bool
	metadata?: null | [...#RecipientMetadataEntry]
	type!: "Automatic"
})

#EnvelopeStageAutomaticRecipientSummaryDto: #EnvelopeStageRecipientSummaryDto & {
	...
} & close({
	signatureProfile?: null | string
	type!:             "Automatic"
})

#EnvelopeStageItemDto: close({
	id!:                           string
	name?:                         null | string
	sortOrder!:                    int32 & int
	requiredRecipientCompletions!: int32 & int
	type!:                         #EnvelopeStageType
	recipients!: [...matchN(1, [#EnvelopeStageStandardRecipientSummaryDto, #EnvelopeStageAutomaticRecipientSummaryDto])]
})

#EnvelopeStageListDto: close({
	stages!: [...#EnvelopeStageItemDto]
})

#EnvelopeStageRecipientResponse: close({
	id!:           string
	languageCode?: null | string
})

#EnvelopeStageRecipientSummaryDto: close({
	id!: string
})

#EnvelopeStageStandardRecipientResponse: #EnvelopeStageRecipientResponse & {
	...
} & close({
	givenName?:                  null | string
	surname?:                    null | string
	email?:                      null | string
	phoneNumber?:                null | string
	notificationChannel?:        null | #NotificationChannel
	authentication?:             null | #RecipientAuthenticationDto
	signatureConfiguration?:     null | #RecipientSignatureDataDto
	personalMessage?:            null | string
	signatureReason?:            null | string
	signatureReasonAllowChange?: null | bool
	metadata?: null | [...#RecipientMetadataEntry]
	generalPoliciesOverrides?: null | #RecipientGeneralPoliciesOverridesDto
	type!:                     "Standard"
})

#EnvelopeStageStandardRecipientSummaryDto: #EnvelopeStageRecipientSummaryDto & {
	...
} & close({
	givenName!:           string
	surname!:             string
	email!:               string
	phoneNumber?:         null | string
	notificationChannel?: null | string
	type!:                "Standard"
})

#EnvelopeStageType: "Signer" | "CarbonCopy" | "Viewer" | "Automatic" | "Approver"

#EnvelopeType: "Envelope" | "Template"

#EnvelopeViewerLinkDto: close({
	viewerLink!: string
})

#ErrorCode: "G001" | "G002" | "G003" | "G004" | "G005" | "G006" | "G007" | "G008" | "A001" | "A002" | "A003" | "A004" | "A005" | "A006" | "A007" | "A008" | "A009" | "A010" | "A011" | "A012" | "A013" | "A014" | "A015" | "A016" | "A017" | "A018" | "A019" | "A020" | "C001" | "C002" | "E001" | "E002" | "E003" | "E004" | "E005" | "E006" | "E008" | "E009" | "E010" | "E011" | "E012" | "E013" | "E014" | "E015" | "E016" | "E017" | "E018" | "E019" | "E020" | "E021" | "E022" | "E023" | "E024" | "E025" | "E026" | "E027" | "E028" | "E029" | "E030" | "E031" | "E032" | "E033" | "E034" | "E035" | "E036" | "E037" | "E038" | "E039" | "E040" | "E041" | "E042" | "E043" | "E044" | "E045" | "E046" | "E047" | "E048" | "E049" | "E050" | "E051" | "E052" | "E053" | "E054" | "E055" | "E056" | "E057" | "E058" | "E059" | "E060" | "E061" | "E062" | "E063" | "E064" | "F001" | "F002" | "F003" | "F004" | "F005" | "F006" | "F007" | "O001" | "O002" | "O003" | "O004" | "O005" | "O007" | "O008" | "O009" | "O010" | "O011" | "O012" | "O013" | "O014" | "O015" | "O016" | "O017" | "O018" | "O019" | "O020" | "O021" | "O022" | "O023" | "O024" | "O025" | "O026" | "O027" | "O028" | "O029" | "O030" | "O031" | "O032" | "O033" | "O034" | "O035" | "O036" | "O037" | "O038" | "O039" | "O040" | "O041" | "O042" | "O043" | "O044" | "O045" | "O046" | "O047" | "O048" | "O049" | "O050" | "O051" | "O052" | "O053" | "O054" | "R001" | "R002" | "R003" | "R004" | "R005" | "R006" | "R007" | "R008" | "R009" | "R010" | "R011" | "R012" | "R013" | "R014" | "R015" | "R016" | "R017" | "R018" | "R019" | "R020" | "R021" | "R022" | "R023" | "R024" | "R025" | "R026" | "R027" | "R028" | "R029" | "R030" | "R031" | "R032" | "R033" | "R034" | "R035" | "ST001" | "ST002" | "ST003" | "ST004" | "ST005" | "ST006" | "ST007" | "ST008" | "S001" | "S002" | "S003" | "S004" | "S005" | "S007" | "S008" | "S009" | "S010" | "S011" | "S012" | "S013" | "S014" | "S015" | "S016" | "S017" | "S018" | "S019" | "S020" | "S021" | "S022" | "S023" | "S024" | "S025" | "S026" | "S027" | "S028" | "S029" | "S030" | "S031" | "U001" | "U002" | "U003" | "UG001" | "UG002" | "UG003" | "UG004" | "UG005" | "UG006" | "UG007" | "UG008" | "UG009" | "UG010" | "V001" | "V002" | "V003" | "V004" | "V005" | "V006" | "V007" | "V008" | "V009" | "V010" | "V011" | "V012" | "V013" | "V014" | "V015" | "V016" | "V017" | "V018" | "V019" | "V020" | "V021" | "V022" | "V023" | "V024" | "V025" | "V026" | "V027" | "V028" | "V029" | "V030" | "V031" | "V032" | "V033" | "V034" | "V035" | "V036" | "V037" | "V038" | "V039" | "V040" | "V041" | "V042" | "V043" | "V044" | "V045" | "V046" | "V047" | "V048" | "V049" | "V050" | "V051" | "V053" | "V054" | "V055" | "V056" | "V057" | "V058" | "V059" | "V060" | "V061" | "V062" | "V063" | "V064" | "V065" | "V066" | "V067" | "V068" | "V069" | "V070" | "V071" | "V072" | "V073" | "V074" | "V075" | "V078" | "V079" | "V080" | "V081" | "V082" | "V083" | "V084" | "V085" | "V086" | "V087" | "V088" | "V089" | "V090" | "V091" | "V092" | "V093" | "V094" | "V095" | "V096" | "V097" | "V098" | "V099" | "V100" | "V101" | "V102" | "V103" | "W001" | "W002" | "W003" | "W004" | "W005" | "W006" | "W007" | "W008" | "W009"

#ErrorResult: close({
	errorId!:     #ErrorCode
	description!: string
	errors?: null | {
		[string]: [...string]
	}
	field?: null | string
})

#ExpirationConfigurationDto: close({
	expirationDate?:                  null | time.Time
	expirationInSecondsAfterSending?: null | int64 & int
})

#ExpirationMode: "Relative" | "Absolute"

#ExternalSignatureImageMode: "Optional" | "Required" | "Disabled"

#FailedEnvelope: close({
	id!:      string
	errorId!: #ErrorCode
})

#FieldTask: close({
	field!: matchN(1, [#SignatureField, #TextInputField, #CheckboxField, #DropDownField, #ListBoxField, #AttachmentField, #AnnotationField, #LinkField, #FileReadConfirmationField, #PageReadConfirmationField, #AreaReadConfirmationField, #RadioButtonField, #ApprovalField, #InvisibleSignatureField])
	sortOrder!:   int32 & int
	recipientId?: null | string
	stageId?:     null | string
})

#FieldTaskItemRequest: close({
	field!: matchN(1, [#SignatureFieldDto, #TextFieldDto, #CheckboxFieldDto, #DropDownFieldDto, #ListBoxFieldDto, #AttachmentFieldDto, #AnnotationFieldDto, #LinkFieldDto, #FileReadConfirmationFieldDto, #PageReadConfirmationFieldDto, #AreaReadConfirmationFieldDto, #RadioButtonFieldDto, #ApprovalFieldDto, #InvisibleSignatureFieldDto])
	sortOrder!:   int32 & int
	recipientId?: null | string
})

#FieldTaskSignatureType: "ClickToSignSignature" | "DrawToSignSignature" | "TypeToSignSignature" | "LocalCertificateSignature" | "DisposableCertificateSignature" | "BiometricSignature" | "RemoteCertificateSignature" | "OneTimePasswordSignature" | "PluginSignature" | "AutomaticSignature"

#FieldType: "Signature" | "TextInput" | "Checkbox" | "DropDown" | "ListBox" | "Attachment" | "Annotation" | "Link" | "FileReadConfirmation" | "PageReadConfirmation" | "AreaReadConfirmation" | "RadioButton" | "Approval" | "InvisibleSignature"

#FieldValidationType: "None" | "Date" | "Email" | "Number" | "Phone" | "Time"

#FileDetailResponse: close({
	documentClass?: null | #EnvelopeFileDetailDocumentClassDto
	restrictedVisibilityRecipientIds?: null | [...string]
})

#FileElementDateValidationConfiguration: close({
	range!:      #FileElementFieldValidationRange
	dateFormat?: null | string
})

#FileElementFieldValidationRange: close({
	from?: null | string
	to?:   null | string
})

#FileElementNumberValidationConfiguration: close({
	symbolPosition!:     #SymbolLocationType
	range!:              #FileElementFieldValidationRange
	symbol?:             null | string
	thousandsSeparator!: #ThousandsSeparatorType
	decimalSeparator!:   #DecimalSeparatorType
	decimals?:           null | int32 & int
})

#FileElementPhoneValidationConfiguration: close({
	type!: #PhoneType
})

#FileElementTextFormat: close({
	textColor!:    string
	fontSizeInPt!: number
	fontName!:     string
	bold!:         bool
	italic!:       bool
	textAlign!:    #TextAlignment
})

#FileElementTimeValidationConfiguration: close({
	range!:      #FileElementFieldValidationRange
	timeFormat?: null | string
})

#FileElementsDto: close({
	textBoxElements!: [...#TextBoxElementDto]
	checkBoxElements!: [...#CheckBoxElementDto]
	signatureElements!: [...#SignatureElementDto]
	dropDownElements!: [...#DropDownElementDto]
	listElements!: [...#ListElementDto]
	documentReadConfirmations!: [...#DocumentReadConfirmationDto]
	pageReadConfirmations!: [...#PageReadConfirmationDto]
	areaReadConfirmations!: [...#AreaReadConfirmationDto]
	linkElements!: [...#LinkElementDto]
	attachmentElements!: [...#AttachmentElementDto]
	annotationElements!: [...#AnnotationElementDto]
	radioButtonElements!: [...#RadioButtonElementDto]
	approveElements!: [...#ApproveElementDto]
	invisibleSignatureElements!: [...#InvisibleSignatureElementDto]
})

#FileElementsFieldValidation: close({
	type!:                          #FieldValidationType
	dateValidationConfiguration?:   null | #FileElementDateValidationConfiguration
	numberValidationConfiguration?: null | #FileElementNumberValidationConfiguration
	phoneValidationConfiguration?:  null | #FileElementPhoneValidationConfiguration
	timeValidationConfiguration?:   null | #FileElementTimeValidationConfiguration
})

#FileElementsPosition: close({
	pageNumber!: int32 & int
	x!:          number
	y!:          number
})

#FileElementsSize: close({
	width!:  number
	height!: number
})

#FileOrderItem: close({
	id!:        string
	sortOrder!: int32 & int
})

#FileReadConfirmationField: #BaseField & {
	...
} & close({
	displayName?: null | string
	fieldType!:   "FileReadConfirmation"
})

#FileReadConfirmationFieldDto: #BaseFieldDto & {
	...
} & close({
	required!:    bool
	displayName?: null | string
	fieldType!:   "FileReadConfirmation"
})

#FileReadConfirmationTaskUpdateRequest: close({
	fieldType!: "FileReadConfirmation"
})

#FileTaskItem: close({
	field!: matchN(1, [#SignatureField, #TextInputField, #CheckboxField, #DropDownField, #ListBoxField, #AttachmentField, #AnnotationField, #LinkField, #FileReadConfirmationField, #PageReadConfirmationField, #AreaReadConfirmationField, #RadioButtonField, #ApprovalField, #InvisibleSignatureField])
	sortOrder!:   int32 & int
	recipientId?: null | string
})

#FirstNameAnnotationConfigDto: close({
	annotationType!: "FirstName"
})

#FirstNameDefinition: close({
	valueFormat!: "FirstName"
})

#FontStyle: close({
	textColor!:    string
	fontSizeInPt!: number
	fontName!:     string
	bold!:         bool
	italic!:       bool
	textAlign!:    int32 & int
})

#ForceAuthenticationModeApi: "None" | "Any" | "Pin" | "Sms" | "OAuth"

#ForcedAuthenticationRulesRequest: close({
	authenticationMode!:                          #ForceAuthenticationModeApi
	forceInputSmsAuthentication!:                 bool
	allowBiometricWithoutAuthentication!:         bool
	allowComplexSignaturesWithoutAuthentication!: bool
	authenticationProviderId?:                    null | string
})

#ForcedAuthenticationRulesResponse: close({
	authenticationMode!:                          #ForceAuthenticationModeApi
	authenticationProviderId?:                    null | string
	forceInputSmsAuthentication!:                 bool
	allowBiometricWithoutAuthentication!:         bool
	allowComplexSignaturesWithoutAuthentication!: bool
})

#FormFieldSource: "Document" | "AdvancedDocumentTag" | "UserDefined"

#FullNameAnnotationConfigDto: close({
	annotationType!: "FullName"
})

#FullNameDefinition: close({
	valueFormat!: "FullName"
})

#GeneralSettingsDto: close({
	name!:                      string
	contactUrl?:                null | string
	supportUrl?:                null | string
	allowSendCC!:               bool
	preventEmailFromBeingSent!: bool
	customStampImprintEnabled!: bool
})

#GenericSigningPluginDto: close({
	pluginId!:              string
	name!:                  string
	allowUserSigning!:      bool
	allowBatchUserSigning!: bool
	allowAutomaticSigning!: bool
	signatureFriendlyNames?: null | [...#GenericSigningPluginSettingLabelDto]
	category!: #SignatureCategory
})

#GenericSigningPluginSenderSettingsDto: close({
	pluginId!:              string
	name!:                  string
	allowUserSigning!:      bool
	allowBatchUserSigning!: bool
	allowAutomaticSigning!: bool
	signatureFriendlyNames?: null | [...#GenericSigningPluginSettingLabelDto]
	category!:              #SignatureCategory
	pluginFriendlyName!:    string
	signatureFriendlyName?: null | string
	senderDataFields?: null | [...#SenderDataFieldSettingDto]
	predefinedSenderDataFields?: null | [...#PredefinedSenderDataField]
	profiles?: null | [...#SenderAutomaticProfileDto]
})

#GenericSigningPluginSettingLabelDto: close({
	languageCode!: string
	text!:         string
})

#GenericSigningPluginsSenderDataDto: close({
	senderGenericSigningPlugins?: null | [...#SenderGenericSigningPluginDto]
})

#GetOrganizationsListResponse: close({
	organizations!: [...#OrganizationSummaryDto]
	pagination!: #PaginationDto
})

#GetUsersListResponse: close({
	users!: [...#OrganizationUserSummaryDto]
	pagination!: #PaginationDto
})

#GetUsersResponse: close({
	users!: [...#OrganizationUserDto]
	pagination!: #PaginationDto
})

#GuidingOrderMode: "AnyOrder" | "EnforceOrder"

#HttpValidationProblemDetails: {
	type?:     null | string
	title?:    null | string
	status?:   null | int32 & int
	detail?:   null | string
	instance?: null | string
	errors!: [string]: [...string]
	{[!~"^(type|title|status|detail|instance|errors)$"]: _}
}

#ImagePosition: "Background" | "Above" | "Below" | "Left" | "Right"

#InitialsAnnotationConfigDto: close({
	annotationType!: "Initials"
})

#InitialsDefinition: close({
	useMiddleNameInInitials!: bool
	valueFormat!:             "Initials"
})

#IntegrationBulkEnvelopeDto: close({
	id!:                      string
	name?:                    null | string
	createdAt!:               time.Time
	updatedAt!:               time.Time
	expirationConfiguration!: #ExpirationConfigurationDto
	expirationMode!:          #ExpirationMode
	reminderConfiguration!:   #ReminderConfigurationDto
	qualifiedTimeStamp!:      bool
	defaultSubject?:          null | string
	defaultBody?:             null | string
	signatureReason?:         null | string
	signatureFormat!:         #SignatureFormat
	stages!: [...#IntegrationStageDto]
	files!: [...#IntegrationFileDto]
	agreements?: null | [...#Agreement]
	status!:                   string
	statusChangeReason?:       null | string
	sentAt?:                   null | time.Time
	fileRestrictedVisibility!: bool
})

#IntegrationEnvelopeDto: close({
	id!:                      string
	name?:                    null | string
	createdAt!:               time.Time
	updatedAt!:               time.Time
	expirationConfiguration!: #ExpirationConfigurationDto
	expirationMode!:          #ExpirationMode
	reminderConfiguration!:   #ReminderConfigurationDto
	qualifiedTimeStamp!:      bool
	defaultSubject?:          null | string
	defaultBody?:             null | string
	signatureReason?:         null | string
	signatureFormat!:         #SignatureFormat
	stages!: [...#IntegrationStageDto]
	files!: [...#IntegrationFileDto]
	agreements?: null | [...#Agreement]
	status!:                   string
	statusChangeReason?:       null | string
	sentAt?:                   null | time.Time
	fileRestrictedVisibility!: bool
})

#IntegrationExpirationConfigurationDto: close({})

#IntegrationFileDto: close({
	id!:        string
	name!:      string
	sortOrder!: int32 & int
})

#IntegrationStageDto: close({
	id!:        string
	name?:      null | string
	sortOrder!: int32 & int
})

#IntegrationTemplateDto: close({
	id!:                      string
	name?:                    null | string
	createdAt!:               time.Time
	updatedAt!:               time.Time
	expirationConfiguration!: #ExpirationConfigurationDto
	expirationMode!:          #ExpirationMode
	reminderConfiguration!:   #ReminderConfigurationDto
	qualifiedTimeStamp!:      bool
	defaultSubject?:          null | string
	defaultBody?:             null | string
	signatureReason?:         null | string
	signatureFormat!:         #SignatureFormat
	stages!: [...#IntegrationStageDto]
	files!: [...#IntegrationFileDto]
	agreements?: null | [...#Agreement]
})

#InvisibleSignatureElementDto: close({
	elementId!:   string
	source!:      #FormFieldSource
	recipientId?: null | string
	required!:    bool
	allowedSignatureTypes?: null | [...matchN(1, [#LocalCertificateSignature, #RemoteCertificateSignature, #DisposableCertificateSignature, #PluginSignature])]
	qualifiedTimeStamp?: null | bool
	guidingOrder!:       int32 & int
})

#InvisibleSignatureField: #BaseField & {
	...
} & close({
	allowedSignatureTypes?: null | [...matchN(1, [#LocalCertificateSignature, #RemoteCertificateSignature, #DisposableCertificateSignature, #PluginSignature])]
	qualifiedTimeStamp?: null | bool
	fieldType!:          "InvisibleSignature"
})

#InvisibleSignatureFieldDto: #BaseFieldDto & {
	...
} & close({
	allowedSignatureTypes?: null | [...matchN(1, [#LocalCertificateSignature, #RemoteCertificateSignature, #DisposableCertificateSignature, #PluginSignature])]
	qualifiedTimeStamp?: null | bool
	fieldType!:          "InvisibleSignature"
})

#LanguageListItemDto: close({
	code!: string
	name!: string
})

#LanguageSettingDto: close({
	id!:       string
	code!:     string
	name!:     string
	isActive!: bool
})

#LanguageStateRequest: close({
	code!:     string
	isActive!: bool
})

#LanguagesDto: close({
	options!: [...#LanguageListItemDto]
})

#LanguagesLookupResponse: close({
	languages!: [...#UiLanguageDto]
})

#LanguagesSettingsResponse: close({
	languages!: [...#LanguageSettingDto]
})

#LanguagesSettingsUpdateRequest: close({
	languages!: [...#LanguageStateRequest]
})

#LastNameAnnotationConfigDto: close({
	annotationType!: "LastName"
})

#LastNameDefinition: close({
	valueFormat!: "LastName"
})

#LastRecipientAction: "SignNotificationSent" | "OpenedWorkstep" | "Signed" | "Rejected" | "Delegated" | "Viewed" | "InUse" | "ReceivedCopy" | "BouncedNotification" | "FailedNotificationDelivery"

#LicenseDto: close({
	type!:           #LicenseType
	expirationDate!: time.Time
	userLimit!:      int32 & int
	documentLimit!:  int32 & int
})

#LicenseType: "Trial" | "LicensedPerUser" | "LicensedPerDocumentsBasic" | "LicensedPerDocumentsProfessional" | "LicensedPerDocumentsBusiness" | "LicensedPerDocumentsEnterprise"

#LinkElementDefinition: close({
	position!: #FileElementsPosition
	size!:     #FileElementsSize
})

#LinkElementDto: close({
	elementDefinition!: #LinkElementDefinition
	source!:            #FormFieldSource
	elementId!:         string
	recipientId?:       null | string
	value!:             string
	guidingOrder!:      int32 & int
})

#LinkField: #BaseField & {
	...
} & close({
	url!:       string
	fieldType!: "Link"
})

#LinkFieldDto: #BaseFieldDto & {
	...
} & close({
	page!:      int32 & int
	positionX!: number
	positionY!: number
	width!:     number
	height!:    number
	reference!: string
	fieldType!: "Link"
})

#ListBoxField: #BaseField & {
	...
} & close({
	readOnly!: bool
	font?:     null | #FontStyle
	options?: null | [...#Option]
	multiselect!: bool
	fieldType!:   "ListBox"
})

#ListBoxFieldDto: #BaseFieldDto & {
	...
} & close({
	page!:      int32 & int
	positionX!: number
	positionY!: number
	width!:     number
	height!:    number
	font!:      #FontStyle
	options?: null | [...#OptionDto]
	multiselect!: bool
	required!:    bool
	readOnly!:    bool
	fieldType!:   "ListBox"
})

#ListBoxTaskUpdateRequest: close({
	selectedItemIds!: [...string]
	fieldType!: "ListBox"
})

#ListElementDefinition: close({
	position!:   #FileElementsPosition
	size!:       #FileElementsSize
	textFormat!: #FileElementTextFormat
	readOnly!:   bool
})

#ListElementDto: close({
	elementDefinition!: #ListElementDefinition
	elementId!:         string
	items!: [...#ListItemEntry]
	isRequired!:    bool
	isEditable!:    bool
	isMultiselect!: bool
	isChecked!:     bool
	source!:        #FormFieldSource
	recipientId?:   null | string
	guidingOrder!:  int32 & int
})

#ListItemEntry: close({
	key!:        string
	value!:      string
	isSelected!: bool
})

#LocalCertificateHashAlgorithm: "Sha256" | "Sha512"

#LocalCertificateSignature: close({
	layoutId?:      null | string
	signatureType!: "LocalCertificateSignature"
})

#LocalCertificateSignatureTypeDto: close({
	useExternalSignatureImage?:     null | #ExternalSignatureImageMode
	preferred?:                     null | bool
	layoutId?:                      null | string
	stampImprintConfiguration?:     null | #LocalCertificateStampImprintDto
	enforcePreferredHashAlgorithm?: null | bool
	preferredHashAlgorithm?:        null | #LocalCertificateHashAlgorithm
})

#LocalCertificateStampImprintDto: close({
	displayName!:             bool
	displaySignatureDate!:    bool
	displayExtraInformation!: bool
	fontName?:                null | string
	fontSizeInPt?:            null | int32 & int
	displayIp!:               bool
})

#MetadataDataType: "String" | "Number" | "Date"

#MetadataValueDto: close({
	fieldDefinitionId!: string
	value?:             null | string
})

#NamedSignatureAppearanceLayoutDto: close({
	id!:                string
	displayFirstname!:  bool
	displayLastname!:   bool
	displayCustomText!: bool
	displayDateTime!:   bool
	displayEmail!:      bool
	displayReason!:     bool
	backgroundImage?:   null | #BackgroundImageDto
	position!:          #ImagePosition
})

#NamedSignatureAppearanceLayoutRequest: close({
	id!:                string
	displayFirstname!:  bool
	displayLastname!:   bool
	displayCustomText!: bool
	displayDateTime!:   bool
	displayEmail!:      bool
	displayReason!:     bool
	backgroundImage?:   null | #BackgroundImageDto
	position!:          #ImagePosition
})

#NextRecipientDto: close({
	id!:        string
	givenName!: string
	surname!:   string
	type!:      #NextRecipientType
})

#NextRecipientLinkDto: close({
	recipient!: #NextRecipientDto
	link!:      string
})

#NextRecipientLinksResponse: close({
	nextRecipientLinks!: [...#NextRecipientLinkDto]
})

#NextRecipientType: "Signer" | "Automatic" | "Approver" | "Viewer"

#NotificationChannel: "Email" | "Sms" | "WhatsApp" | "DoNotSendNotification"

#NotificationChannelMessagesDto: close({
	messages!: [...#NotificationMessageDto]
})

#NotificationChannelsDto: close({
	email!:    bool
	sms!:      bool
	whatsApp!: bool
})

#NotificationMessageDto: close({
	subject?: null | string
	body?:    null | string
})

#NotificationPreferencesRequest: close({
	notifyRecipientOnActionNeeded!: bool
})

#NotificationPreferencesResponse: close({
	notifyRecipientOnActionNeeded!: bool
})

#NotificationSettingsDto: close({
	emailSenderDisplayType!:                  #EmailSenderDisplayType
	envelopeLimitReachedNotificationEnabled!: bool
	envelopesInPercentFromLimitNotification!: int32 & int
	envelopesLimitReachedPercentStep!:        int32 & int
	organizationCallbackEnabled!:             bool
	licenseExpireNotificationEnabled!:        bool
	licenseExpireNotificationBeforeDays!:     int32 & int
	licenseExpireNotificationRecurrentDays!:  int32 & int
	organizationCallbackUrl!:                 string
	reminderSendLimitInMinutes!:              int32 & int
})

#NumberInputConfig: #TextInputConfig & {
	...
} & close({
	value?:              null | number
	symbol?:             null | #NumberSymbol
	thousandsSeparator!: #ThousandsSeparatorType
	decimalSeparator!:   #DecimalSeparatorType
	decimalPlaces?:      null | int32 & int
	minValue?:           null | number
	maxValue?:           null | number
	textInputType!:      "NumberType"
})

#NumberSymbol: close({
	value?:    null | string
	position!: #SymbolLocationType
})

#OAuthAuthentication: close({
	providerName!: string
	externalId!:   string
})

#OAuthFieldDefinitionDto: close({
	id!:                           int64 & int
	path!:                         string
	mode!:                         #OAuthSignerProviderFieldMode
	target!:                       #OAuthSignerProviderFieldTarget
	oAuthResourceUriId?:           null | int64 & int
	oAuthJwtConfigId?:             null | int64 & int
	oAuthProviderId?:              null | int64 & int
	customFieldName?:              null | string
	genericSigningPluginId?:       null | string
	genericSigningPluginFieldKey?: null | string
})

#OAuthFieldReferenceDto: close({
	id?:                            null | string
	fieldTarget!:                   #OAuthFieldTarget
	customFieldName?:               null | string
	genericSigningPluginReference?: null | #OAuthGenericSigningPluginReferenceDto
})

#OAuthFieldTarget: "Custom" | "Recipient_GivenName" | "Recipient_Surname" | "Recipient_Email" | "Recipient_PhoneNumber" | "DisposableHolder_IdentificationType" | "DisposableHolder_IdentificationCountry" | "DisposableHolder_CountryResidence" | "DisposableHolder_PhoneMobile" | "DisposableHolder_RecognitionType" | "DisposableHolder_DocumentIssuedBy" | "DisposableHolder_DocumentIssuedOn" | "DisposableHolder_DocumentExpiryDate" | "DisposableHolder_TaxCode" | "DisposableHolder_DocumentNumber" | "GenericSigningPlugin_CustomSenderField"

#OAuthGenericSigningPluginReferenceDto: close({
	pluginId?: null | string
	key?:      null | string
})

#OAuthJwtConfigDto: close({
	oAuthProviderId!:  int64 & int
	jwksUri!:          string
	issuer!:           string
	enforceNonce!:     bool
	validateAudience!: bool
	validateIssuer!:   bool
	validateLifetime!: bool
	oAuthFieldDefinitions?: null | [...#OAuthFieldDefinitionDto]
})

#OAuthResourceUriDto: close({
	id!:                    int64 & int
	uri!:                   string
	accessTokenParamName!:  string
	eIdServiceCombination?: null | string
	oAuthFieldDefinitions?: null | [...#OAuthFieldDefinitionDto]
})

#OAuthSignerProvider: close({
	id!:                 int64 & int
	externalId!:         string
	name!:               string
	clientId!:           string
	clientSecret?:       null | string
	scope?:              null | string
	authorizationUri!:   string
	tokenUri!:           string
	logoutUri?:          null | string
	authenticationType!: int32 & int
	isActive?:           null | bool
	redirectUrl?:        null | string
})

#OAuthSignerProviderDetailsResponse: close({
	oAuthSignerProvider!: #OAuthSignerProviderDto
	oAuthJwtConfig?:      null | #OAuthJwtConfigDto
	oAuthResourceUris?: null | [...#OAuthResourceUriDto]
})

#OAuthSignerProviderDto: close({
	id!:                 int64 & int
	externalId!:         string
	name!:               string
	clientId!:           string
	clientSecret?:       null | string
	scope?:              null | string
	authorizationUri!:   string
	tokenUri!:           string
	logoutUri?:          null | string
	authenticationType!: int32 & int
	isActive?:           null | bool
	redirectUrl?:        null | string
})

#OAuthSignerProviderFieldMode: "ValidateEqualCaseSensitive" | "Update" | "ValidateEqualCaseInsensitive"

#OAuthSignerProviderFieldModeResponse: close({
	name!:  string
	value!: int32 & int
})

#OAuthSignerProviderFieldTarget: "Custom" | "Recipient_FirstName" | "Recipient_LastName" | "Recipient_Email" | "Recipient_PhoneNumber" | "DisposableHolder_IdentificationType" | "DisposableHolder_IdentificationCountry" | "DisposableHolder_CountryResidence" | "DisposableHolder_PhoneMobile" | "DisposableHolder_RecognitionType" | "DisposableHolder_DocumentIssuedBy" | "DisposableHolder_DocumentIssuedOn" | "DisposableHolder_DocumentExpiryDate" | "DisposableHolder_TaxCode" | "DisposableHolder_DocumentNumber" | "GenericSigningPlugin_CustomField"

#OAuthSignerProviderFieldTargetResponse: close({
	name!:  string
	value!: int32 & int
})

#OAuthSignerProvidersResponse: close({
	oAuthSignerProviders!: [...#OAuthSignerProvider]
	pagination!: #Pagination
})

#OAuthSignerProvidersSortingKey: "Name"

#OneTimePasswordSignature: close({
	layoutId?:      null | string
	signatureType!: "OneTimePasswordSignature"
})

#OneTimePasswordSignatureTypeDto: close({
	useExternalSignatureImage?: null | #ExternalSignatureImageMode
	preferred?:                 null | bool
	layoutId?:                  null | string
	stampImprintConfiguration?: null | #OneTimePasswordStampImprintDto
	validityInSeconds?:         null | int32 & int
})

#OneTimePasswordStampImprintDto: close({
	displayName!:             bool
	displaySignatureDate!:    bool
	displayExtraInformation!: bool
	fontName?:                null | string
	fontSizeInPt?:            null | int32 & int
	displayTransactionId!:    bool
	displayTransactionToken!: bool
	displayPhoneNumber!:      bool
	displayIp!:               bool
	displayEmail!:            bool
})

#Option: close({
	value!:      string
	label!:      string
	isSelected!: bool
})

#OptionDto: close({
	key!:        string
	value!:      string
	isSelected!: bool
})

#OrganizationCustomTimeStampServerSettings: close({
	url?:           null | string
	username?:      null | string
	password?:      null | string
	hashAlgorithm?: null | string
})

#OrganizationDefaultSignatureTypeDto: close({
	signatureType!: #SignatureType
})

#OrganizationDelegationSettingsDto: close({
	delegationPolicy!: #DelegationPolicy
})

#OrganizationDetailDto: close({
	id!:                    string
	name!:                  string
	creationDateUtc!:       time.Time
	canceled!:              bool
	licenseType!:           #LicenseType
	licenseExpirationDate!: time.Time
	userLimit!:             int32 & int
})

#OrganizationFeatureFlagResponse: close({
	id!:      int32 & int
	enabled!: bool
	name!:    string
})

#OrganizationFeatureFlagsResponse: close({
	featureFlags!: [...#OrganizationFeatureFlagResponse]
})

#OrganizationGeneralPoliciesDto: close({
	allowSaveDocument!:        bool
	allowSaveAuditTrail!:      bool
	allowPrintDocument!:       bool
	allowAdhocPdfAttachments!: bool
	allowRejectWorkstep!:      bool
	allowUndoLastAction!:      bool
})

#OrganizationItemDto: close({
	id!:     string
	name!:   string
	userId!: string
})

#OrganizationLanguageLookupDto: close({
	code!: string
	name!: string
})

#OrganizationPAdESConfiguration: close({
	simpleSignatures?:   null | #PAdESSignatureConfig
	enhancedSignatures?: null | #PAdESSignatureConfig
	complexSignatures?:  null | #PAdESSignatureConfig
	auditTrail?:         null | #PAdESSignatureConfig
})

#OrganizationRecipientAuthenticationTypesDto: close({
	allowedAuthenticationTypes?: null | [...#RecipientAuthenticationTypes]
	oAuthProviders?: null | [...#OrganizationRecipientOAuthProviderDto]
})

#OrganizationRecipientOAuthProviderDto: close({
	identifier!:                 string
	name!:                       string
	hasEIdAssertion!:            bool
	hasLateIdentSigTypes!:       bool
	providesIdentification!:     bool
	updateFieldComparisonValue!: int64 & int
	updateFields?: null | [...#OAuthFieldReferenceDto]
	validateFields?: null | [...#OAuthFieldReferenceDto]
})

#OrganizationRecipientSettingsDto: close({
	sendFinishedDocumentsToAllRecipients!: bool
	showNotEnoughSignaturesWarning!:       bool
	delegationAvailable!:                  bool
})

#OrganizationSettingsPermissions: close({
	read!:   bool
	update!: bool
})

#OrganizationSignatureTypesDto: close({
	allowedSignatureTypes!: [...string]
	allowedDefaultSignatureTypes!: [...string]
	allowedGenericSigningPlugins!: [...#GenericSigningPluginDto]
})

#OrganizationSummaryDto: close({
	id!:       string
	name!:     string
	canceled!: bool
})

#OrganizationUserDto: close({
	id!:               string
	givenName!:        string
	surname!:          string
	email!:            string
	regionalSettings!: #OrganizationUserRegionalSettingsDto
	phoneNumber?:      null | string
	enabled!:          bool
})

#OrganizationUserRegionalSettingsDto: close({
	timeZone!:       string
	language!:       string
	country!:        string
	dateTimeFormat!: #DateTimeFormatSwaggerEnumProvider
})

#OrganizationUserSummaryDto: close({
	id!:               string
	givenName!:        string
	surname!:          string
	email!:            string
	regionalSettings!: #OrganizationUserRegionalSettingsDto
	phoneNumber?:      null | string
	enabled!:          bool
})

#OtpDeliveryChannel: "Sms" | "Email"

#OtpSignatureDataDto: close({
	type?:        null | #OtpDeliveryChannel
	phoneNumber?: null | string
})

#PAdESLevel: "B" | "T" | "LT" | "LTA"

#PAdESSignatureConfig: close({
	enabled!: bool
	level!:   #PAdESLevel
})

#PageReadConfirmationDto: close({
	elementId!:    string
	pageNumber!:   int32 & int
	required!:     bool
	recipientId?:  null | string
	guidingOrder!: int32 & int
	displayName?:  null | string
})

#PageReadConfirmationField: #BaseField & {
	...
} & close({
	displayName?: null | string
	fieldType!:   "PageReadConfirmation"
})

#PageReadConfirmationFieldDto: #BaseFieldDto & {
	...
} & close({
	page!:        int32 & int
	required!:    bool
	displayName?: null | string
	fieldType!:   "PageReadConfirmation"
})

#PageReadConfirmationTaskUpdateRequest: close({
	fieldType!: "PageReadConfirmation"
})

#PaginatedRoles: close({
	roles!: [...#RoleDto]
	pagination!: #PaginationDto
})

#Pagination: close({
	page!:       int32 & int
	pageSize!:   int32 & int
	totalCount!: int32 & int
})

#PaginationDto: close({
	page!:       int32 & int
	pageSize!:   int32 & int
	totalCount!: int32 & int
})

#PaginationResponse: close({
	page!:       int32 & int
	pageSize!:   int32 & int
	totalCount!: int32 & int
})

#ParseBulkRecipientsResponse: close({
	bulkRecipients!: [...#BulkRecipientDefinition]
})

#PdfDocumentSettingsDto: close({
	pAdESConfiguration?:               null | #OrganizationPAdESConfiguration
	allowSigningOfLockedPdfDocuments!: bool
	customTimeStampSettings?:          null | #OrganizationCustomTimeStampServerSettings
})

#PermissionDto: close({
	entity!: #Entity
	action!: #Action
})

#PermissionsDto: close({
	envelopes!:            #EnvelopePermissions
	templates!:            #TemplatePermissions
	userGroups!:           #UserGroupsPermissions
	organizationSettings!: #OrganizationSettingsPermissions
	users!:                #UsersSettings
	roles!:                #RolesSettings
	automaticESealing!:    #AutomaticESealingPermissions
})

#PersonalAccessTokenListItemResponse: close({
	id!:        string
	name!:      string
	createdAt!: time.Time
	expiresAt!: time.Time
})

#PersonalAccessTokenListResponse: close({
	personalAccessTokens!: [...#PersonalAccessTokenListItemResponse]
})

#PhoneNumberInputConfig: #TextInputConfig & {
	...
} & close({
	value!:         string
	format!:        #PhoneType
	textInputType!: "PhoneNumberType"
})

#PhoneType: "International" | "InternationalLeadingZeros" | "InternationalLeadingPlus"

#PluginSignature: close({
	pluginId!:      string
	layoutId?:      null | string
	signatureType!: "PluginSignature"
})

#PluginStampImprintDto: close({
	displayName!:             bool
	displaySignatureDate!:    bool
	displayExtraInformation!: bool
	fontName?:                null | string
	fontSizeInPt?:            null | int32 & int
	displayEmail!:            bool
	displayIp!:               bool
})

#PoliciesResponse: close({
	policies!: [...#PolicyListItemResponse]
	pagination!: #PaginationResponse
})

#PoliciesSortingKey: "Name" | "Description" | "IsActive"

#PolicyActionDto: close({
	id?:              null | string
	sortOrder!:       int32 & int
	type!:            #PolicyActionType
	stage!:           #StageConfigurationDto
	recipientSource!: #PolicyRecipientSourceDto
})

#PolicyActionType: "AddEnvelopeStageConfiguration"

#PolicyConditionDto: close({
	id!:         string
	metadataId!: string
	operator!:   #PolicyConditionOperator
	value!:      string
	sortOrder!:  int32 & int
})

#PolicyConditionOperator: "GreaterThan" | "LessThan" | "Equals"

#PolicyConditionRequest: close({
	id!:         string
	metadataId!: string
	operator!:   #PolicyConditionOperator
	value!:      string
	sortOrder!:  int32 & int
})

#PolicyDto: close({
	id!:              string
	name!:            string
	isActive!:        bool
	sortOrder!:       int32 & int
	description?:     null | string
	documentClassId?: null | string
	conditions?: null | [...#PolicyConditionDto]
	actions?: null | [...#PolicyActionDto]
})

#PolicyListItemResponse: close({
	id!:          string
	name!:        string
	isActive!:    bool
	description?: null | string
})

#PolicyRecipientDto: close({
	givenName!:   string
	surname!:     string
	email!:       string
	phoneNumber?: null | string
})

#PolicyRecipientSourceDto: close({
	type!: #PolicyRecipientSourceType
	recipients?: null | [...#PolicyRecipientDto]
	userGroupId?:    null | string
	businessRoleId?: null | string
})

#PolicyRecipientSourceType: "Static" | "Dynamic"

#PredefinedSenderDataField: "RecipientEmail" | "RecipientGivenName" | "RecipientSurname"

#ProblemDetails: {
	type?:     null | string
	title?:    null | string
	status?:   null | int32 & int
	detail?:   null | string
	instance?: null | string
	{[!~"^(type|title|status|detail|instance)$"]: _}
}

#PutFileDetailRequest: close({
	documentClass?: null | #EnvelopeFileDetailDocumentClassRequest
	restrictedVisibilityRecipientIds?: null | [...string]
})

#RadioButtonElementDefinition: close({
	position!: #FileElementsPosition
	size!:     #FileElementsSize
	readOnly!: bool
})

#RadioButtonElementDto: close({
	elementId!:         string
	elementDefinition!: #RadioButtonElementDefinition
	source!:            #FormFieldSource
	groupName!:         string
	isChecked!:         bool
	isSelectInUnison!:  bool
	recipientId?:       null | string
	required!:          bool
	value!:             string
	guidingOrder!:      int32 & int
})

#RadioButtonField: #BaseField & {
	...
} & close({
	groupName!:        string
	isSelectInUnison!: bool
	readOnly!:         bool
	checked!:          bool
	value!:            string
	fieldType!:        "RadioButton"
})

#RadioButtonFieldDto: #BaseFieldDto & {
	...
} & close({
	page!:      int32 & int
	positionX!: number
	positionY!: number
	width!:     number
	height!:    number
	groupName!: string
	readOnly!:  bool
	checked!:   bool
	value!:     string
	required!:  bool
	fieldType!: "RadioButton"
})

#RadioButtonTaskUpdateRequest: close({
	selectedFieldId!: string
	fieldType!:       "RadioButton"
})

#RecipientAuthenticationDto: close({
	accessCode?:         null | #AccessCode
	smsOneTimePassword?: null | #SmsOneTimePassword
	oAuthAuthentications?: null | [...#OAuthAuthentication]
})

#RecipientAuthenticationSettingItemResponse: close({
	name!:      string
	isEnabled!: bool
})

#RecipientAuthenticationSettingsResponse: close({
	settings!: [...#RecipientAuthenticationSettingItemResponse]
})

#RecipientAuthenticationTypes: "Pin" | "SmsOtp" | "BankId" | "OAuth" | "Saml"

#RecipientDiscriminator: "Standard" | "Automatic"

#RecipientDto: close({
	id!:                          string
	givenName?:                   null | string
	surname?:                     null | string
	email?:                       null | string
	phoneNumber?:                 null | string
	placeholder?:                 null | string
	type?:                        null | #DbRecipientType
	isP7mSigner!:                 bool
	notificationChannel?:         null | #NotificationChannel
	order!:                       int32 & int
	languageCode?:                null | string
	authenticationConfiguration?: null | #RecipientAuthenticationDto
	signatureDataConfiguration?:  null | #RecipientSignatureDataDto
	stageId?:                     null | string
	personalMessage?:             null | string
	guidingOrderMode?:            null | #GuidingOrderMode
	isDelegationEnabled!:         bool
	generalPoliciesOverrides?:    null | #RecipientGeneralPoliciesOverridesDto
	signatureReason?:             null | string
	signatureReasonAllowChange?:  null | bool
	signatureProfile?:            null | string
	metadata?: null | [...#RecipientMetadataEntry]
	workstepResult?: null | #DbWorkstepResult
})

#RecipientGeneralPoliciesOverridesDto: close({
	allowSaveDocument!:        bool
	allowSaveAuditTrail!:      bool
	allowPrintDocument!:       bool
	allowAdhocPdfAttachments!: bool
	allowRejectWorkstep!:      bool
	allowUndoLastAction!:      bool
})

#RecipientMetadataEntry: close({
	name!:  string
	value!: string
})

#RecipientSignatureDataDto: close({
	disposableCertificate?:           null | #DisposableCertificateDto
	remoteCertificate?:               null | #RemoteCertificateDto
	aTrustCertificate?:               null | #ATrustCertificateDto
	swissComOnDemand?:                null | #SwissComOnDemandDto
	swedishBankId?:                   null | #SwedishBankIdDto
	otpSignatureData?:                null | #OtpSignatureDataDto
	genericSigningPluginsSenderData?: null | #GenericSigningPluginsSenderDataDto
	automaticSignatureData?:          null | #AutomaticSignatureDataDto
})

#RecipientStatus: "NotSigned" | "Signed" | "Rejected" | "Delegated"

#RecipientType: "Signer" | "CC" | "Acknowledge" | "Pkcs7Signer" | "Automatic" | "Approver"

#RegionalSettingsDto: close({
	id!:               string
	worldTimeZone!:    string
	dateTimeFormatId!: int32 & int
	uiLanguage!:       string
	countryId!:        int32 & int
})

#RelativeIntegrationExpirationDto: #IntegrationExpirationConfigurationDto & {
	...
} & close({
	afterSendInSeconds?: null | int64 & int
	mode!:               "Relative"
})

#ReminderConfigurationDto: close({
	enabled!:                      bool
	firstReminderInDays!:          int32 & int
	reminderResendIntervalInDays!: int32 & int
	beforeExpirationInDays!:       int32 & int
})

#RemoteCertificateDto: close({
	userId?:   null | string
	deviceId?: null | string
})

#RemoteCertificateEnvelopeBulkSignDto: #EnvelopeBulkSignDto & {
	...
} & close({
	signatureType!:     "RemoteCertificate"
	certificateUserId!: string
	devicePassword!:    string
	otp!:               string
	otpDeviceType?:     null | string
	otpDeviceTypeId?:   null | string
	transactionId?:     null | string
	payloadFileId?:     null | string
})

#RemoteCertificateSignature: close({
	layoutId?:      null | string
	signatureType!: "RemoteCertificateSignature"
})

#RemoteCertificateSignatureTypeDto: close({
	useExternalSignatureImage?: null | #ExternalSignatureImageMode
	preferred?:                 null | bool
	layoutId?:                  null | string
	stampImprintConfiguration?: null | #RemoteCertificateStampImprintDto
	validityInSeconds?:         null | int32 & int
})

#RemoteCertificateStampImprintDto: close({
	displayName!:             bool
	displaySignatureDate!:    bool
	displayExtraInformation!: bool
	fontName?:                null | string
	fontSizeInPt?:            null | int32 & int
	displayIp!:               bool
})

#ReplacedEnvelopeFileResponse: close({
	id!:         string
	orderIndex!: int32 & int
})

#ReplacedTemplateFileResponse: close({
	id!:         string
	orderIndex!: int32 & int
})

#RequestBulkSignDevicesDto: close({
	userId!: string
	envelopeIds!: [...string]
})

#ResumeBatchRequest: close({
	data!: #EnvelopeResumeDto
	envelopeIds!: [...string]
})

#RoleDetailsDto: close({
	id!:   string
	name!: string
	permissions!: [...#PermissionDto]
	createdAt!:    time.Time
	description?:  null | string
	isSystemRole!: bool
})

#RoleDto: close({
	id!:           string
	name!:         string
	isSystemRole!: bool
})

#RolesSettings: close({
	read!:               bool
	createUpdateDelete!: bool
	assign!:             bool
})

#RolesSortingKey: "Id" | "Name"

#RotateServiceAccountSecretResponse: close({
	clientSecret!: string
})

#RowError: close({
	row!:     int32 & int
	field!:   string
	message!: string
})

#SealingCertificateResponse: close({
	id!:                 int64 & int
	externalId!:         string
	isActive!:           bool
	sealingCertificate!: #CertificateDetailsResponse
	certificateChain!: [...#CertificateDetailsResponse]
})

#SenderAutomaticProfileDto: close({
	profileId!:           string
	profileFriendlyName?: null | string
})

#SenderDataFieldSettingDto: close({
	required!: bool
	translatedLabels!: [...#GenericSigningPluginSettingLabelDto]
	defaultValue?: null | string
	key?:          null | string
	type!:         #DataFieldType
	items?: null | {
		[string]: string
	}
})

#SenderGenericSigningPluginDto: close({
	pluginId?: null | string
	settings?: null | [...#SenderGenericSigningPluginSettingsDto]
})

#SenderGenericSigningPluginSettingsDto: close({
	key?:   null | string
	value?: null | string
})

#SentBulkEnvelopeResponse: close({
	id!: string
})

#SentEnvelopeDto: close({
	id!: string
})

#ServiceAccountListItemResponse: close({
	clientId!: string
	email!:    string
	userId!:   string
})

#ServiceAccountListResponse: close({
	items!: [...#ServiceAccountListItemResponse]
})

#SettingsDto: close({
	maxEnvelopeValidityInDays!:    int32 & int
	minEnvelopeValidityInSeconds!: int32 & int
	filterExpiringSoonDays!:       int32 & int
	notificationSettings!:         #NotificationSettingsDto
})

#SharingOptionsResponse: close({
	userGroupIds!: [...string]
})

#SignDeskOpenResultDto: close({
	workstepId?:  null | string
	culture?:     null | string
	redirectUrl?: null | string
})

#SignatureAppearanceLayoutDto: close({
	displayFirstname!:  bool
	displayLastname!:   bool
	displayCustomText!: bool
	displayDateTime!:   bool
	displayEmail!:      bool
	displayReason!:     bool
	backgroundImage?:   null | #BackgroundImageDto
	position!:          #ImagePosition
})

#SignatureAppearanceLayoutRequest: close({
	displayFirstname!:  bool
	displayLastname!:   bool
	displayDateTime!:   bool
	displayEmail!:      bool
	displayCustomText!: bool
	displayReason!:     bool
	backgroundImage?:   null | #BackgroundImageDto
	position!:          #ImagePosition
})

#SignatureCategory: "Advanced" | "MixedOrNotSpecified" | "Qualified" | "Simple"

#SignatureElementDefinition: close({
	position!: #FileElementsPosition
	size!:     #FileElementsSize
})

#SignatureElementDto: close({
	elementId!:                  string
	allowedSignatureTypes!:      #AllowedSignatureTypesDto
	elementDefinition!:          #SignatureElementDefinition
	source!:                     #FormFieldSource
	recipientId?:                null | string
	required!:                   bool
	displayName?:                null | string
	elementDescription?:         null | string
	useExternalTimestampServer?: null | bool
	guidingOrder!:               int32 & int
	taskConfiguration?:          null | #SignatureTaskConfiguration
	isApprove!:                  bool
})

#SignatureField: #BaseField & {
	...
} & close({
	allowedSignatureTypes?:      null | #AllowedSignatureTypesDto
	displayName?:                null | string
	elementDescription?:         null | string
	useExternalTimestampServer?: null | bool
	taskConfiguration?:          null | #SignatureTaskConfiguration
	fieldType!:                  "Signature"
})

#SignatureFieldDto: #BaseFieldDto & {
	...
} & close({
	page!:      int32 & int
	positionX!: number
	positionY!: number
	width!:     number
	height!:    number
	allowedSignatureTypes!: [...matchN(1, [#ClickToSignSignature, #DrawToSignSignature, #TypeToSignSignature, #LocalCertificateSignature, #DisposableCertificateSignature, #BiometricSignature, #RemoteCertificateSignature, #OneTimePasswordSignature, #PluginSignature, #AutomaticSignature])]
	qualifiedTimeStamp?: null | bool
	required!:           bool
	fieldType!:          "Signature"
})

#SignatureFormat: "Pades" | "Cades"

#SignatureImage: close({
	id!:            string
	name!:          string
	dataUrlPrefix!: string
	data!:          string
})

#SignatureOptions: "Timestamp" | "AllowUsingCustomTimestampService"

#SignaturePluginSignatureTypeDto: close({
	pluginId!:                  string
	preferred?:                 null | bool
	layoutId?:                  null | string
	stampImprintConfiguration?: null | #PluginStampImprintDto
})

#SignatureTaskConfiguration: close({
	batchGroup?:       null | string
	batchMode?:        null | #BatchMode
	useLocalTimezone?: null | bool
	dateTimeFormat?:   null | string
})

#SignatureTaskUpdateRequest: close({
	signature!: matchN(1, [#WorkUnitClickToSignSignature, #WorkUnitDrawToSignSignature, #WorkUnitTypeToSignSignature, #WorkUnitLocalCertificateSignature, #WorkUnitDisposableCertificateSignature, #WorkUnitBiometricSignature, #WorkUnitRemoteCertificateSignature, #WorkUnitOneTimePasswordSignature, #WorkUnitPluginSignature, #WorkUnitAutomaticSignature])
	fieldType!: "Signature"
})

#SignatureType: "None" | "ClickToSign" | "DrawToSign" | "TypeToSign" | "RemoteCertificate" | "Biometric" | "LocalCertificate" | "DisposableCertificate" | "OneTimePassword" | "SwissComOnDemand" | "PushTan" | "ATrustCertificate" | "SwedishBankId" | "SignaturePlugin" | "AutomaticSignature"

#SignerAgreements: close({
	isEnvelopeOverrideEnabled!: bool
})

#SingleInsight: close({
	envelopeCount!: int32 & int
})

#SmsOneTimePassword: close({
	phoneNumber?: null | string
})

#StageConfigurationDto: close({
	name!:                         string
	type!:                         #StageType
	requiredRecipientCompletions!: int32 & int
})

#StageDto: close({
	id!:                        string
	mandatoryRecipientsNumber!: int32 & int
	name?:                      null | string
})

#StageMode: "Standard" | "Bulk"

#StageSortOrderItem: close({
	id!:        string
	sortOrder!: int32 & int
})

#StageType: "Signer" | "Approver" | "Viewer" | "ReceivesCopy" | "SignAutomatically"

#StampImprintConfigurationDto: close({
	defaultLayout!: #SignatureAppearanceLayoutDto
	customSignatures!: [...#NamedSignatureAppearanceLayoutDto]
})

#StartBulkSignTransactionDto: close({
	userId!:          string
	deviceId!:        string
	otpDeviceType!:   string
	otpDeviceTypeId!: string
	envelopeIds!: [...string]
})

#StatusKey: "Canceled" | "Completed" | "Expired" | "Rejected" | "Active" | "Draft" | "WaitingForYou" | "WaitingForOthers" | "ExpiringSoon" | "InProgress"

#StringInputConfig: #TextInputConfig & {
	...
} & close({
	value!:         string
	password!:      bool
	multiline!:     bool
	maxLength!:     int32 & int
	textInputType!: "StringType"
})

#SubstituteDelegationDto: close({
	utilizeAlsoOnCCRecipients!: bool
	delegateeFirstName!:        string
	delegateeLastName!:         string
	delegateeEmail!:            string
	reason?:                    null | string
	startDate?:                 null | time.Time
	endDate?:                   null | time.Time
	delegateeUserId?:           null | string
})

#SupportedElectronicIdentitiesResponse: close({
	electronicIdentities!: [...#SupportedElectronicIdentityResponse]
})

#SupportedElectronicIdentityResponse: close({
	type!:    string
	country!: string
})

#SupportedFileFormatResponse: close({
	extension!: string
	mimeType!:  string
})

#SwedishBankIdDto: close({
	personalNumber?:         null | string
	allowAnyPersonalNumber?: null | bool
})

#SwedishBankIdSignatureTypeDto: close({
	preferred?:                 null | bool
	layoutId?:                  null | string
	stampImprintConfiguration?: null | #SwedishBankIdStampImprintDto
})

#SwedishBankIdStampImprintDto: close({
	displayName!:             bool
	displaySignatureDate!:    bool
	displayExtraInformation!: bool
	fontName?:                null | string
	fontSizeInPt?:            null | int32 & int
	displayTransactionId!:    bool
})

#SwissComOnDemandDto: close({
	commonName?:   null | string
	country?:      null | string
	phoneNumber?:  null | string
	organization?: null | string
	organizationUnits?: null | [...string]
	locality?:        null | string
	serialNumber?:    null | string
	stateOrProvince?: null | string
	pseudonym?:       null | string
})

#SwissComOnDemandSignatureTypeDto: close({
	validityInSeconds?:         null | int32 & int
	preferred?:                 null | bool
	layoutId?:                  null | string
	stampImprintConfiguration?: null | #SwissComOnDemandStampImprintDto
})

#SwissComOnDemandStampImprintDto: close({
	displayName!:             bool
	displaySignatureDate!:    bool
	displayExtraInformation!: bool
	fontName?:                null | string
	fontSizeInPt?:            null | int32 & int
	displayIp!:               bool
})

#SymbolLocationType: "Start" | "StartWithBlank" | "End" | "EndWithBlank"

#TemplateAction: "Use" | "Edit" | "Delete" | "Share"

#TemplateDto: close({
	id!:            string
	creatorUserId!: string
	name!:          string
	actions!: [...#TemplateAction]
	createdAt!:     time.Time
	updatedAt!:     time.Time
	defaultAction?: null | #TemplateAction
})

#TemplateFieldTask: close({
	field!: matchN(1, [#SignatureField, #TextInputField, #CheckboxField, #DropDownField, #ListBoxField, #AttachmentField, #AnnotationField, #LinkField, #FileReadConfirmationField, #PageReadConfirmationField, #AreaReadConfirmationField, #RadioButtonField, #ApprovalField, #InvisibleSignatureField])
	sortOrder!:   int32 & int
	recipientId?: null | string
})

#TemplateFieldTaskItem: close({
	field!: matchN(1, [#SignatureFieldDto, #TextFieldDto, #CheckboxFieldDto, #DropDownFieldDto, #ListBoxFieldDto, #AttachmentFieldDto, #AnnotationFieldDto, #LinkFieldDto, #FileReadConfirmationFieldDto, #PageReadConfirmationFieldDto, #AreaReadConfirmationFieldDto, #RadioButtonFieldDto, #ApprovalFieldDto, #InvisibleSignatureFieldDto])
	sortOrder!:   int32 & int
	recipientId?: null | string
	source!:      #ElementSource
})

#TemplateFileTasksResponse: close({
	tasks!: [...#TemplateFieldTaskItem]
})

#TemplateFilesResponse: close({
	files!: [...#Document]
})

#TemplateListDto: close({
	templates!: [...#TemplateDto]
	pagination!: #PaginationDto
})

#TemplatePermissions: close({
	read!:               bool
	createUpdateDelete!: bool
})

#TemplateStageAutomaticRecipientResponse: #TemplateStageRecipientResponse & {
	...
} & close({
	signatureProfile?:           null | string
	signatureReason?:            null | string
	signatureReasonAllowChange?: null | bool
	metadata?: null | [...#RecipientMetadataEntry]
	type!: "Automatic"
})

#TemplateStageAutomaticRecipientSummaryDto: #TemplateStageRecipientSummaryDto & {
	...
} & close({
	signatureProfile?: null | string
	type!:             "Automatic"
})

#TemplateStageItemDto: close({
	id!:                           string
	name?:                         null | string
	sortOrder!:                    int32 & int
	requiredRecipientCompletions!: int32 & int
	type!:                         #EnvelopeStageType
	recipients!: [...matchN(1, [#TemplateStageStandardRecipientSummaryDto, #TemplateStageAutomaticRecipientSummaryDto])]
})

#TemplateStageListDto: close({
	stages!: [...#TemplateStageItemDto]
})

#TemplateStageRecipientResponse: close({
	id!:           string
	languageCode?: null | string
})

#TemplateStageRecipientSummaryDto: close({
	id!: string
})

#TemplateStageStandardRecipientResponse: #TemplateStageRecipientResponse & {
	...
} & close({
	givenName?:                  null | string
	surname?:                    null | string
	email?:                      null | string
	phoneNumber?:                null | string
	notificationChannel?:        null | #NotificationChannel
	authentication?:             null | #RecipientAuthenticationDto
	signatureConfiguration?:     null | #RecipientSignatureDataDto
	personalMessage?:            null | string
	signatureReason?:            null | string
	signatureReasonAllowChange?: null | bool
	isDelegationEnabled!:        bool
	metadata?: null | [...#RecipientMetadataEntry]
	type!: "Standard"
})

#TemplateStageStandardRecipientSummaryDto: #TemplateStageRecipientSummaryDto & {
	...
} & close({
	givenName!:           string
	surname!:             string
	email!:               string
	phoneNumber?:         null | string
	notificationChannel?: null | string
	isDelegationEnabled!: bool
	type!:                "Standard"
})

#TemplateThumbnailDto: close({
	templateId!: string
	name!:       string
	fileData!:   string
})

#TextAlignment: "Left" | "Center" | "Right"

#TextAnnotationConfigDto: close({
	value?:          null | string
	annotationType!: "Text"
})

#TextBoxElementDefinition: close({
	position!:    #FileElementsPosition
	size!:        #FileElementsSize
	textFormat!:  #FileElementTextFormat
	readOnly!:    bool
	isMultiline!: bool
	isPassword!:  bool
	maxLength!:   int32 & int
})

#TextBoxElementDto: close({
	elementId!:         string
	elementDefinition!: #TextBoxElementDefinition
	source!:            #FormFieldSource
	recipientId?:       null | string
	required!:          bool
	value!:             string
	guidingOrder!:      int32 & int
	validation?:        null | #FileElementsFieldValidation
})

#TextDefinition: close({
	defaultValue!: string
	valueFormat!:  "Text"
})

#TextFieldDto: #BaseFieldDto & {
	...
} & close({
	page!:      int32 & int
	positionX!: number
	positionY!: number
	width!:     number
	height!:    number
	font!:      #FontStyle
	textInputConfig!: matchN(1, [#StringInputConfig, #DateInputConfig, #NumberInputConfig, #PhoneNumberInputConfig, #TimeInputConfig])
	required!:  bool
	readOnly!:  bool
	fieldType!: "TextInput"
})

#TextInputConfig: close({})

#TextInputField: #BaseField & {
	...
} & close({
	readOnly!:   bool
	font?:       null | #FontStyle
	text!:       string
	password!:   bool
	multiline!:  bool
	maxLength!:  int32 & int
	validation?: null | #FileElementsFieldValidation
	fieldType!:  "TextInput"
})

#TextInputType: "StringType" | "DateType" | "NumberType" | "PhoneNumberType" | "TimeType"

#TextTaskUpdateRequest: close({
	textInputValue!: matchN(1, [#WorkUnitStringInputValue, #WorkUnitNumberInputValue, #WorkUnitDateInputValue])
	fieldType!: "TextInput"
})

#ThousandsSeparatorType: "Comma" | "Point" | "Apostrophe" | "Blank" | "None"

#TimeFormatSwaggerEnumProvider: "HH:mm"

#TimeInputConfig: #TextInputConfig & {
	...
} & close({
	value?:         null | string
	format?:        null | #TimeFormatSwaggerEnumProvider
	minValue?:      null | string
	maxValue?:      null | string
	textInputType!: "TimeType"
})

#TimeZoneDto: close({
	code!: string
	name!: string
})

#TimeZoneListItemDto: close({
	timeZone!:  string
	code!:      string
	utcOffset!: string
})

#TimeZonesDto: close({
	options!: [...#TimeZoneListItemDto]
})

#TimeZonesLookupResponse: close({
	timeZones!: [...#TimeZoneDto]
})

#TimestampHashAlgorithm: "Sha1" | "Sha256" | "Sha512"

#TimestampSettingsDto: close({
	url!:           string
	username!:      string
	password!:      string
	hashAlgorithm!: #TimestampHashAlgorithm
})

#TypeToSignSignature: close({
	layoutId?:      null | string
	signatureType!: "TypeToSignSignature"
})

#TypeToSignSignatureTypeDto: close({
	useExternalSignatureImage?: null | #ExternalSignatureImageMode
	preferred?:                 null | bool
	layoutId?:                  null | string
	stampImprintConfiguration?: null | #TypeToSignStampImprintDto
})

#TypeToSignStampImprintDto: close({
	displayName!:             bool
	displaySignatureDate!:    bool
	displayExtraInformation!: bool
	fontName?:                null | string
	fontSizeInPt?:            null | int32 & int
	displayEmail!:            bool
	displayIp!:               bool
})

#UiLanguageDto: close({
	code!: string
	name!: string
})

#UpdateATrustCertificateDto: close({
	phoneNumber?: null | string
})

#UpdateAccessCodeDto: close({
	code!: string
})

#UpdateAuditTrailModeRequest: close({
	auditTrailMode!: #EnvelopeLogGeneration
})

#UpdateAuthenticationConfigurationDto: close({
	accessCode?:         null | #UpdateAccessCodeDto
	smsOneTimePassword?: null | #UpdateSmsOneTimePasswordDto
	oAuthAuthentications?: null | [...#UpdateOAuthAuthenticationDto]
})

#UpdateAutomaticSignatureDataDto: close({
	profileId?: null | string
	pluginId?:  null | string
})

#UpdateBankIdSettingsDto: close({
	authenticationCertificateThumbprint!: string
})

#UpdateBasicSettingsDto: close({
	givenName!:   string
	surname!:     string
	phoneNumber!: string
})

#UpdateBulkEnvelopeDto: close({
	expirationConfiguration!: #UpdateExpirationConfigurationDto
	reminderConfiguration!:   #UpdateReminderConfigurationDto
	name?:                    null | string
	recipients?: null | [...#UpdateEnvelopeRecipientDto]
	stages?: null | [...#BulkStageDto]
	sendCopyToAllRecipients?:                                        null | bool
	lateIdent?:                                                      null | bool
	useInvisibleSignatureWithTimestampForAllDocumentsAndRecipients?: null | bool
	defaultSubject?:                                                 null | string
	defaultBody?:                                                    null | string
	documentsIds?: null | [...string]
	agreements?: null | [...#Agreement]
	userGroupSharingIds?: null | [...string]
	envelopeType!:                     #EnvelopeType
	callbackConfiguration?:            null | #CallbackConfigurationDto
	preventFieldsEditingWhenFinished?: null | bool
	afterSendRedirectUrl?:             null | string
	signatureReason?:                  null | string
	allowChangeSignatureReason?:       null | bool
	signatureFormat?:                  null | #SignatureFormat
	fileRestrictedVisibility?:         null | bool
})

#UpdateBulkEnvelopeFileTasksRequest: close({
	tasks!: [...#BulkEnvelopeFieldTaskItemRequest]
})

#UpdateBulkEnvelopeForIntegrationDto: close({
	name!:     string
	reminder!: #UpdateForIntegrationReminderDto
	expiration!: matchN(1, [#AbsoluteIntegrationExpirationDto, #RelativeIntegrationExpirationDto])
	qualifiedTimeStamp?: null | bool
	signatureReason?:    null | string
	signatureFormat?:    null | #SignatureFormat
	notificationMessages?: null | [...#NotificationChannelMessagesDto]
	agreements?: null | [...#Agreement]
	fileRestrictedVisibility?: null | bool
})

#UpdateBulkFileTasksRequest: close({
	fieldTasks!: [...#FieldTask]
})

#UpdateDisposableCertificateDto: close({
	documentIssuingCountry?:       null | string
	identificationIssuingCountry?: null | string
	identificationType?:           null | string
	phoneNumber?:                  null | string
	documentType?:                 null | string
	documentIssuedBy?:             null | string
	documentIssuedOn?:             null | time.Time
	documentExpiryDate?:           null | time.Time
	serialNumber?:                 null | string
	documentNumber?:               null | string
})

#UpdateDisposableCertificateSettingsDto: close({
	lraId!:                                         string
	user!:                                          string
	password?:                                      null | string
	disposableType!:                                #DisposableType
	showDisclaimerBeforeCertificateRequest!:        bool
	sendDisposableDisclaimerDocumentNotifications!: bool
})

#UpdateDocumentClassRequest: close({
	name!:        string
	description?: null | string
	metadata?: null | [...#DocumentClassMetadataFieldDto]
})

#UpdateEnvelopeDto: close({
	expirationConfiguration!: #UpdateExpirationConfigurationDto
	reminderConfiguration!:   #UpdateReminderConfigurationDto
	name?:                    null | string
	recipients?: null | [...#UpdateEnvelopeRecipientDto]
	stages?: null | [...#UpdateStageDto]
	sendCopyToAllRecipients?:                                        null | bool
	lateIdent?:                                                      null | bool
	useInvisibleSignatureWithTimestampForAllDocumentsAndRecipients?: null | bool
	defaultSubject?:                                                 null | string
	defaultBody?:                                                    null | string
	documentsIds?: null | [...string]
	agreements?: null | [...#Agreement]
	userGroupSharingIds?: null | [...string]
	envelopeType!:                     #EnvelopeType
	callbackConfiguration?:            null | #CallbackConfigurationDto
	preventFieldsEditingWhenFinished?: null | bool
	afterSendRedirectUrl?:             null | string
	signatureReason?:                  null | string
	allowChangeSignatureReason?:       null | bool
	signatureFormat?:                  null | #SignatureFormat
	fileRestrictedVisibility?:         null | bool
})

#UpdateEnvelopeFileTasksRequest: close({
	tasks!: [...#FieldTaskItemRequest]
})

#UpdateEnvelopeForIntegrationDto: close({
	name!:     string
	reminder!: #UpdateForIntegrationReminderDto
	expiration!: matchN(1, [#AbsoluteIntegrationExpirationDto, #RelativeIntegrationExpirationDto])
	qualifiedTimeStamp?: null | bool
	signatureReason?:    null | string
	signatureFormat?:    null | #SignatureFormat
	notificationMessages?: null | [...#NotificationChannelMessagesDto]
	agreements?: null | [...#Agreement]
	fileRestrictedVisibility?: null | bool
})

#UpdateEnvelopeRecipientDto: close({
	id!:                          string
	givenName?:                   null | string
	surname?:                     null | string
	email?:                       null | string
	phoneNumber?:                 null | string
	placeholder?:                 null | string
	type?:                        null | #DbRecipientType
	notificationChannel?:         null | #NotificationChannel
	order?:                       null | int32 & int
	languageCode?:                null | string
	authenticationConfiguration?: null | #UpdateAuthenticationConfigurationDto
	signatureDataConfiguration?:  null | #UpdateSignatureDataConfigurationDto
	stageId?:                     null | string
	personalMessage?:             null | string
	guidingOrderMode?:            null | #GuidingOrderMode
	isDelegationEnabled!:         bool
	generalPoliciesOverrides?:    null | #UpdateGeneralPoliciesOverridesDto
	signatureReason?:             null | string
	signatureReasonAllowChange?:  null | bool
	metadata?: null | [...#RecipientMetadataEntry]
	syncId?: null | string
})

#UpdateEnvelopeStageAutomaticRecipientRequest: #UpdateEnvelopeStageRecipientRequest & {
	...
} & close({
	signatureProfile?:           null | string
	signatureReasonAllowChange?: null | bool
	metadata?: null | [...#RecipientMetadataEntry]
	type!: "Automatic"
})

#UpdateEnvelopeStageRecipientRequest: close({
	languageCode?:    null | string
	signatureReason?: null | string
})

#UpdateEnvelopeStageRequest: close({
	name?:                         null | string
	requiredRecipientCompletions?: null | int32 & int
	type?:                         null | #EnvelopeStageType
})

#UpdateEnvelopeStageStandardRecipientRequest: #UpdateEnvelopeStageRecipientRequest & {
	...
} & close({
	givenName?:                  null | string
	surname?:                    null | string
	email?:                      null | string
	phoneNumber?:                null | string
	notificationChannel?:        null | #NotificationChannel
	authentication?:             null | #UpdateAuthenticationConfigurationDto
	signatureConfiguration?:     null | #UpdateSignatureDataConfigurationDto
	personalMessage?:            null | string
	signatureReasonAllowChange?: null | bool
	metadata?: null | [...#RecipientMetadataEntry]
	generalPoliciesOverrides?: null | #UpdateGeneralPoliciesOverridesDto
	type!:                     "Standard"
})

#UpdateExpirationConfigurationDto: close({
	expirationDate?:                  null | time.Time
	expirationInSecondsAfterSending?: null | int64 & int
})

#UpdateFileOrderRequest: close({
	files!: [...#FileOrderItem]
})

#UpdateFileTasksRequest: close({
	fieldTasks!: [...#FileTaskItem]
})

#UpdateForIntegrationReminderDto: close({
	enabled!:                bool
	firstReminderInDays!:    int32 & int
	resendIntervalInDays!:   int32 & int
	beforeExpirationInDays!: int32 & int
})

#UpdateGeneralPoliciesOverridesDto: close({
	allowSaveDocument!:        bool
	allowSaveAuditTrail!:      bool
	allowPrintDocument!:       bool
	allowAdhocPdfAttachments!: bool
	allowRejectWorkstep!:      bool
	allowUndoLastAction!:      bool
})

#UpdateGenericSigningPluginsSenderDataDto: close({
	senderGenericSigningPlugins?: null | [...#UpdateSenderGenericSigningPluginDto]
})

#UpdateOAuthAuthenticationDto: close({
	externalId!: string
})

#UpdateOAuthFieldDefinitionRequest: close({
	path!:                         string
	mode!:                         #OAuthSignerProviderFieldMode
	target!:                       #OAuthSignerProviderFieldTarget
	id?:                           null | int64 & int
	customFieldName?:              null | string
	genericSigningPluginId?:       null | string
	genericSigningPluginFieldKey?: null | string
})

#UpdateOAuthJwtConfigRequest: close({
	oAuthProviderId!:  int64 & int
	jwksUri!:          string
	issuer!:           string
	enforceNonce!:     bool
	validateAudience!: bool
	validateIssuer!:   bool
	validateLifetime!: bool
	oAuthFieldDefinitions?: null | [...#UpdateOAuthFieldDefinitionRequest]
})

#UpdateOAuthResourceUriRequest: close({
	uri!:                   string
	accessTokenParamName!:  string
	id?:                    null | int64 & int
	eIdServiceCombination?: null | string
	oAuthFieldDefinitions?: null | [...#UpdateOAuthFieldDefinitionRequest]
})

#UpdateOAuthSignerProviderDetailsRequest: close({
	oAuthSignerProvider!: #UpdateOAuthSignerProviderRequest
	oAuthJwtConfig?:      null | #UpdateOAuthJwtConfigRequest
	oAuthResourceUris?: null | [...#UpdateOAuthResourceUriRequest]
})

#UpdateOAuthSignerProviderRequest: close({
	externalId!:         string
	name!:               string
	clientId!:           string
	authorizationUri!:   string
	tokenUri!:           string
	authenticationType!: int32 & int
	clientSecret?:       null | string
	scope?:              null | string
	logoutUri?:          null | string
})

#UpdateOrganizationDefaultSignatureTypeRequest: close({
	signatureType!: #SignatureType
})

#UpdateOrganizationDelegationSettingsRequest: close({
	delegationPolicy!: #DelegationPolicy
})

#UpdateOrganizationFeatureFlag: close({
	id!:      int32 & int
	enabled!: bool
})

#UpdateOrganizationFeatureFlagsRequest: close({
	featureFlags!: [...#UpdateOrganizationFeatureFlag]
})

#UpdateOrganizationRecipientSettingsRequest: close({
	sendFinishedDocumentsToAllRecipients!: bool
	showNotEnoughSignaturesWarning!:       bool
})

#UpdateOrganizationUserDto: close({
	givenName!:            string
	surname!:              string
	userRegionalSettings!: #UserRegionalSettingsRequestDto
	phoneNumber?:          null | string
})

#UpdateOrganizationUserRolesDto: close({
	roles!: [...string]
})

#UpdateOtpSignatureDataDto: close({
	type?:        null | #OtpDeliveryChannel
	phoneNumber?: null | string
})

#UpdatePdfDocumentSettingsDto: close({
	pAdESConfiguration?:               null | #OrganizationPAdESConfiguration
	allowSigningOfLockedPdfDocuments!: bool
	customTimeStampSettings?:          null | #OrganizationCustomTimeStampServerSettings
})

#UpdatePolicyRequest: close({
	name!:            string
	isActive!:        bool
	sortOrder!:       int32 & int
	description?:     null | string
	documentClassId?: null | string
	conditions?: null | [...#PolicyConditionDto]
	actions?: null | [...#PolicyActionDto]
})

#UpdateRecipientAuthenticationSettingItemRequest: close({
	name!:      string
	isEnabled!: bool
})

#UpdateRecipientAuthenticationSettingsRequest: close({
	settings?: null | [...#UpdateRecipientAuthenticationSettingItemRequest]
})

#UpdateRegionalSettingsDto: close({
	worldTimeZone!:    string
	dateTimeFormatId!: int32 & int
	uiLanguage!:       string
	countryId!:        int32 & int
})

#UpdateReminderConfigurationDto: close({
	enabled?:                      null | bool
	firstReminderInDays?:          null | int32 & int
	reminderResendIntervalInDays?: null | int32 & int
	beforeExpirationInDays?:       null | int32 & int
})

#UpdateRemoteCertificateDto: close({
	userId?:   null | string
	deviceId?: null | string
})

#UpdateRoleRequest: close({
	name!: string
	permissions!: [...#PermissionDto]
	description?: null | string
})

#UpdateSenderGenericSigningPluginDto: close({
	pluginId?: null | string
	settings?: null | [...#UpdateSenderGenericSigningPluginSettingsDto]
})

#UpdateSenderGenericSigningPluginSettingsDto: close({
	key?:   null | string
	value?: null | string
})

#UpdateSharingOptionsRequest: close({
	userGroupIds!: [...string]
})

#UpdateSignatureDataConfigurationDto: close({
	disposableCertificate?:           null | #UpdateDisposableCertificateDto
	remoteCertificate?:               null | #UpdateRemoteCertificateDto
	aTrustCertificate?:               null | #UpdateATrustCertificateDto
	swissComOnDemand?:                null | #UpdateSwissComOnDemandDto
	swedishBankId?:                   null | #UpdateSwedishBankIdDto
	otpSignatureData?:                null | #UpdateOtpSignatureDataDto
	genericSigningPluginsSenderData?: null | #UpdateGenericSigningPluginsSenderDataDto
	automaticSignatureData?:          null | #UpdateAutomaticSignatureDataDto
})

#UpdateSmsOneTimePasswordDto: close({
	phoneNumber?: null | string
})

#UpdateStageDto: close({
	id!:                        string
	mandatoryRecipientsNumber!: int32 & int
	name?:                      null | string
	type?:                      null | #EnvelopeStageType
	stageMode?:                 null | #StageMode
})

#UpdateStageSortOrderRequest: close({
	stages!: [...#StageSortOrderItem]
})

#UpdateStampImprintConfigurationRequest: close({
	defaultLayout!: #SignatureAppearanceLayoutRequest
	customSignatures!: [...#NamedSignatureAppearanceLayoutRequest]
})

#UpdateSubstituteDelegationDto: close({
	delegateeUserEmail!:        string
	utilizeAlsoOnCCRecipients!: bool
	reason?:                    null | string
	startDate?:                 null | time.Time
	endDate?:                   null | time.Time
})

#UpdateSwedishBankIdDto: close({
	personalNumber?:         null | string
	allowAnyPersonalNumber?: null | bool
})

#UpdateSwissComOnDemandDto: close({
	commonName?:   null | string
	country?:      null | string
	phoneNumber?:  null | string
	organization?: null | string
	organizationUnits?: null | [...string]
	locality?:        null | string
	serialNumber?:    null | string
	stateOrProvince?: null | string
	pseudonym?:       null | string
})

#UpdateTemplateDto: close({
	expirationConfiguration!: #UpdateExpirationConfigurationDto
	reminderConfiguration!:   #UpdateReminderConfigurationDto
	name?:                    null | string
	recipients?: null | [...#UpdateTemplateRecipientDto]
	sendCopyToAllRecipients?:                                        null | bool
	lateIdent?:                                                      null | bool
	useInvisibleSignatureWithTimestampForAllDocumentsAndRecipients?: null | bool
	documentsIds?: null | [...string]
	agreements?: null | [...#Agreement]
	stages?: null | [...#UpdateStageDto]
	defaultSubject?: null | string
	defaultBody?:    null | string
	userGroupSharingIds?: null | [...string]
	envelopeType!:                     #EnvelopeType
	callbackConfiguration?:            null | #CallbackConfigurationDto
	preventFieldsEditingWhenFinished?: null | bool
	afterSendRedirectUrl?:             null | string
	signatureReason?:                  null | string
	allowChangeSignatureReason?:       null | bool
	signatureFormat?:                  null | #SignatureFormat
	fileRestrictedVisibility?:         null | bool
})

#UpdateTemplateFieldTasksRequest: close({
	fieldTasks!: [...#TemplateFieldTask]
})

#UpdateTemplateFileTasksRequest: close({
	tasks!: [...#FieldTaskItemRequest]
})

#UpdateTemplateForIntegrationDto: close({
	name!:     string
	reminder!: #UpdateForIntegrationReminderDto
	expiration!: matchN(1, [#AbsoluteIntegrationExpirationDto, #RelativeIntegrationExpirationDto])
	qualifiedTimeStamp?: null | bool
	signatureReason?:    null | string
	signatureFormat?:    null | #SignatureFormat
	notificationMessages?: null | [...#NotificationChannelMessagesDto]
	agreements?: null | [...#Agreement]
	fileRestrictedVisibility?: null | bool
})

#UpdateTemplateRecipientDto: close({
	id!:                          string
	givenName?:                   null | string
	surname?:                     null | string
	email?:                       null | string
	phoneNumber?:                 null | string
	placeholder?:                 null | string
	type?:                        null | #DbRecipientType
	notificationChannel?:         null | #NotificationChannel
	order?:                       null | int32 & int
	languageCode?:                null | string
	authenticationConfiguration?: null | #UpdateAuthenticationConfigurationDto
	signatureDataConfiguration?:  null | #UpdateSignatureDataConfigurationDto
	stageId?:                     null | string
	personalMessage?:             null | string
	guidingOrderMode?:            null | #GuidingOrderMode
	isDelegationEnabled!:         bool
	generalPoliciesOverrides?:    null | #UpdateGeneralPoliciesOverridesDto
	signatureReason?:             null | string
	signatureReasonAllowChange?:  null | bool
	metadata?: null | [...#RecipientMetadataEntry]
	syncId?: null | string
})

#UpdateTemplateStageAutomaticRecipientRequest: #UpdateTemplateStageRecipientRequest & {
	...
} & close({
	signatureProfile?:           null | string
	signatureReasonAllowChange?: null | bool
	metadata?: null | [...#RecipientMetadataEntry]
	type!: "Automatic"
})

#UpdateTemplateStageRecipientRequest: close({
	languageCode?:    null | string
	signatureReason?: null | string
})

#UpdateTemplateStageRequest: close({
	name?:                         null | string
	requiredRecipientCompletions?: null | int32 & int
	type?:                         null | #EnvelopeStageType
})

#UpdateTemplateStageStandardRecipientRequest: #UpdateTemplateStageRecipientRequest & {
	...
} & close({
	givenName?:                  null | string
	surname?:                    null | string
	email?:                      null | string
	phoneNumber?:                null | string
	notificationChannel?:        null | #NotificationChannel
	authentication?:             null | #UpdateAuthenticationConfigurationDto
	signatureConfiguration?:     null | #UpdateSignatureDataConfigurationDto
	personalMessage?:            null | string
	signatureReasonAllowChange?: null | bool
	isDelegationEnabled?:        null | bool
	metadata?: null | [...#RecipientMetadataEntry]
	type!: "Standard"
})

#UpdatedBasicSettingsDto: close({
	id!:          string
	givenName!:   string
	surname!:     string
	phoneNumber?: null | string
})

#UserAndOrganizationDto: close({
	id!:               string
	givenName!:        string
	surname!:          string
	email!:            string
	organizationId!:   string
	organizationName!: string
	phoneNumber?:      null | string
})

#UserApplicationContextDto: close({
	signatureTypes!:       #OrganizationSignatureTypesDto
	defaultSignatureType!: #OrganizationDefaultSignatureTypeDto
	signatureOptions!: [...#SignatureOptions]
	recipientTypes!: [...#RecipientType]
	recipientAuthenticationTypes!:      #OrganizationRecipientAuthenticationTypesDto
	signerAgreements!:                  #SignerAgreements
	generalPolicies!:                   #OrganizationGeneralPoliciesDto
	notificationChannels!:              #NotificationChannelsDto
	userPermissions!:                   #PermissionsDto
	userGroupPermissions!:              #UserGroupPermissionsSetDto
	delegationInfo!:                    #DelegationInfo
	oAuthAvailable!:                    bool
	automaticRemoteSignatureAvailable!: bool
	documentClassesEnabled!:            bool
	envelopeEventServiceEnabled!:       bool
	fontFamilies!: [...string]
	bulkEnvelopeEnabled!: bool
})

#UserDefaultUserGroup: close({
	id!:   string
	name!: string
})

#UserDefaultUserGroupDefaultType: "Envelope" | "Template"

#UserGroupContactCreateDto: close({
	details?:        null | string
	givenName?:      null | string
	surname?:        null | string
	email?:          null | string
	phoneNumber?:    null | string
	cultureIsoCode?: null | string
})

#UserGroupContactDto: close({
	id!:             string
	userGroupId!:    string
	details?:        null | string
	givenName?:      null | string
	surname?:        null | string
	email?:          null | string
	phoneNumber?:    null | string
	cultureIsoCode?: null | string
})

#UserGroupContactFieldDto: close({
	id!:          string
	userGroupId!: string
	name!:        string
})

#UserGroupContactFieldListDto: close({
	userGroupContactFields!: [...#UserGroupContactFieldDto]
})

#UserGroupContactImportResultDto: close({
	imported!: int32 & int
})

#UserGroupContactImportValidationErrorResponse: close({
	errors!: [...#RowError]
})

#UserGroupContactUpdateDto: close({
	details?:        null | string
	givenName?:      null | string
	surname?:        null | string
	email?:          null | string
	phoneNumber?:    null | string
	cultureIsoCode?: null | string
})

#UserGroupContactsListDto: close({
	userGroupContacts!: [...#UserGroupContactDto]
	pagination!: #PaginationDto
})

#UserGroupContactsPermissionDto: close({
	read!:               bool
	createUpdateDelete!: bool
	customize!:          bool
})

#UserGroupContactsSortingKey: "GivenName" | "Surname" | "Email"

#UserGroupCreateDto: close({
	name!: string
})

#UserGroupCustomFieldUpdateData: close({
	userGroupId!: string
	name!:        string
	id?:          null | string
})

#UserGroupCustomFieldUpdateRequest: close({
	updatedCustomFields!: [...#UserGroupCustomFieldUpdateData]
})

#UserGroupDto: close({
	id!:             string
	organizationId!: string
	name!:           string
})

#UserGroupEnvelopesPermissionDto: close({
	share!:  bool
	manage!: bool
})

#UserGroupPermissionDataDto: close({
	name!:        string
	permissions!: #UserGroupPermissionDto
})

#UserGroupPermissionDto: close({
	users!:     #UserGroupUsersPermissionDto
	envelopes!: #UserGroupEnvelopesPermissionDto
	templates!: #UserGroupTemplatesPermissionDto
	contacts!:  #UserGroupContactsPermissionDto
})

#UserGroupPermissionsSetDto: close({
	userGroups!: [string]: #UserGroupPermissionDataDto
})

#UserGroupTemplatesPermissionDto: close({
	share!:  bool
	manage!: bool
})

#UserGroupUpdateDto: close({
	name!: string
})

#UserGroupUserBusinessRoleRequest: close({
	businessRoleId!: string
})

#UserGroupUserDto: close({
	id!:             string
	email!:          string
	givenName!:      string
	surname!:        string
	permissions!:    #UserGroupPermissionDto
	businessRole?:   null | string
	businessRoleId?: null | string
})

#UserGroupUserListDto: close({
	userGroupId!: string
	userGroupUsers!: [...#UserGroupUserDto]
	pagination!: #PaginationDto
})

#UserGroupUsersPermissionDto: close({
	read!:               bool
	createUpdateDelete!: bool
})

#UserGroupUsersSortingKey: "GivenName" | "Surname" | "Email" | "BusinessRole"

#UserGroupsListDto: close({
	userGroups!: [...#UserGroupDto]
	pagination!: #PaginationDto
})

#UserGroupsPermissions: close({
	read!:               bool
	createUpdateDelete!: bool
})

#UserGroupsSortingKey: "Name"

#UserImportResultDto: close({
	imported!: int32 & int
	failure?:  null | #RowError
})

#UserImportValidationErrorResponse: close({
	errors!: [...#RowError]
})

#UserOrganizationsDto: close({
	organizations!: [...#OrganizationItemDto]
	defaultOrganizationId!: string
})

#UserRegionalSettingsDto: close({
	worldTimeZone!:    string
	dateTimeFormatId!: int32 & int
	uiLanguage!:       string
	countryId!:        int32 & int
})

#UserRegionalSettingsRequestDto: close({
	timeZone!:       string
	dateTimeFormat!: #DateTimeFormatSwaggerEnumProvider
	language!:       string
	country!:        string
})

#UserRoleRequest: close({
	name!: string
})

#UserRolesDto: close({
	roles!: [...string]
})

#UsersSettings: close({
	read!:               bool
	createUpdateDelete!: bool
})

#UsersSortingKey: "GivenName" | "Surname" | "Email" | "Enabled"

#ValidateOrganizationDto: close({
	name!:                                  string
	isoCulture!:                            string
	onePlatformBusinessRelationIdentifier?: null | string
	features!: [...string]
})

#VersionInfo: close({
	imageTag!: string
	version!:  string
})

#WebhookAuthenticationRequest: close({
	headers?: null | {
		[string]: string
	}
	clientCert?: null | string
	clientKey?:  null | string
})

#WebhookSubscriptionDto: close({
	id!:                   string
	url!:                  string
	hasHeaders!:           bool
	hasClientCertificate!: bool
	createdAt!:            time.Time
})

#WebhookSubscriptionRequest: close({
	url!:            string
	authentication?: null | #WebhookAuthenticationRequest
})

#WorkUnitApprovalFieldResponse: #WorkUnitFieldResponse & {
	...
} & close({
	page!:      int32 & int
	positionX!: number
	positionY!: number
	width!:     number
	height!:    number
	required!:  bool
	fieldType!: "Approval"
})

#WorkUnitAreaReadConfirmationFieldResponse: #WorkUnitFieldResponse & {
	...
} & close({
	page!:      int32 & int
	positionX!: number
	positionY!: number
	width!:     number
	height!:    number
	required!:  bool
	readOnly!:  bool
	fieldType!: "AreaReadConfirmation"
})

#WorkUnitAttachmentFieldResponse: #WorkUnitFieldResponse & {
	...
} & close({
	page!:      int32 & int
	positionX!: number
	positionY!: number
	width!:     number
	height!:    number
	required!:  bool
	label?:     null | string
	fieldType!: "Attachment"
})

#WorkUnitAuthenticateRequest: close({
	code!: string
})

#WorkUnitAuthenticationProviderType: "AccessCode"

#WorkUnitAuthenticationRequiredResponse: close({
	provider?: null | #WorkUnitAuthenticationProviderType
})

#WorkUnitAutomaticSignature: close({
	layoutId?:      null | string
	signatureType!: "AutomaticSignature"
})

#WorkUnitAutomaticSignatureResponse: close({
	layoutId?:      null | string
	signatureType!: "AutomaticSignature"
})

#WorkUnitBiometricSignature: close({
	signatureType!: "BiometricSignature"
})

#WorkUnitBiometricSignatureResponse: close({
	signatureType!: "BiometricSignature"
})

#WorkUnitCheckboxFieldResponse: #WorkUnitFieldResponse & {
	...
} & close({
	page!:      int32 & int
	positionX!: number
	positionY!: number
	width!:     number
	height!:    number
	checked!:   bool
	value!:     string
	required!:  bool
	readOnly!:  bool
	fieldType!: "Checkbox"
})

#WorkUnitClickToSignSignature: close({
	layoutId?:      null | string
	signatureType!: "ClickToSignSignature"
})

#WorkUnitClickToSignSignatureRequest: close({
	signatureType!: "ClickToSignSignature"
})

#WorkUnitClickToSignSignatureResponse: close({
	layoutId?:      null | string
	signatureType!: "ClickToSignSignature"
})

#WorkUnitDateInputConfigResponseResponse: #WorkUnitTextInputConfigResponse & {
	...
} & close({
	value?:         null | time.Format("2006-01-02")
	format?:        null | #DateFormatSwaggerEnumProvider
	minValue?:      null | time.Format("2006-01-02")
	maxValue?:      null | time.Format("2006-01-02")
	textInputType!: "DateType"
})

#WorkUnitDateInputValue: close({
	value!:         time.Format("2006-01-02")
	textInputType!: "DateType"
})

#WorkUnitDecimalSeparatorTypeResponse: "None" | "Comma" | "Dot" | "Apostrophe"

#WorkUnitDisposableCertificateSignature: close({
	layoutId?:      null | string
	signatureType!: "DisposableCertificateSignature"
})

#WorkUnitDisposableCertificateSignatureResponseResponse: close({
	layoutId?:      null | string
	signatureType!: "DisposableCertificateSignature"
})

#WorkUnitDrawToSignSignature: close({
	signatureImage?: null | string
	layoutId?:       null | string
	signatureType!:  "DrawToSignSignature"
})

#WorkUnitDrawToSignSignatureRequest: close({
	signatureType!:  "DrawToSignSignature"
	signatureImage?: null | string
})

#WorkUnitDrawToSignSignatureResponse: close({
	layoutId?:      null | string
	signatureType!: "DrawToSignSignature"
})

#WorkUnitDropDownFieldResponse: #WorkUnitFieldResponse & {
	...
} & close({
	page!:      int32 & int
	positionX!: number
	positionY!: number
	width!:     number
	height!:    number
	font!:      #WorkUnitFontStyleResponse
	options?: null | [...#WorkUnitOptionResponse]
	required!:   bool
	readOnly!:   bool
	isEditable!: bool
	fieldType!:  "DropDown"
})

#WorkUnitElementSourceResponse: "File" | "UserDefined"

#WorkUnitFieldResponse: close({
	id!: string
})

#WorkUnitFieldTaskResponse: close({
	field!: matchN(1, [#WorkUnitSignatureFieldResponse, #WorkUnitTextFieldResponse, #WorkUnitCheckboxFieldResponse, #WorkUnitDropDownFieldResponse, #WorkUnitListBoxFieldResponse, #WorkUnitAttachmentFieldResponse, #WorkUnitLinkFieldResponse, #WorkUnitFileReadConfirmationFieldResponse, #WorkUnitPageReadConfirmationFieldResponse, #WorkUnitAreaReadConfirmationFieldResponse, #WorkUnitRadioButtonFieldResponse, #WorkUnitApprovalFieldResponse, #WorkUnitInvisibleSignatureFieldResponse])
	sortOrder!:   int32 & int
	recipientId?: null | string
	source!:      #WorkUnitElementSourceResponse
	displayName?: null | string
	completed!:   bool
})

#WorkUnitFieldTaskSignatureType: "ClickToSignSignature" | "DrawToSignSignature" | "TypeToSignSignature" | "LocalCertificateSignature" | "DisposableCertificateSignature" | "BiometricSignature" | "RemoteCertificateSignature" | "OneTimePasswordSignature" | "PluginSignature" | "AutomaticSignature"

#WorkUnitFieldTaskSignatureTypeRequest: "ClickToSignSignature" | "DrawToSignSignature" | "TypeToSignSignature"

#WorkUnitFieldTaskSignatureTypeResponse: "ClickToSignSignature" | "DrawToSignSignature" | "TypeToSignSignature" | "LocalCertificateSignature" | "DisposableCertificateSignature" | "BiometricSignature" | "RemoteCertificateSignature" | "OneTimePasswordSignature" | "PluginSignature" | "AutomaticSignature"

#WorkUnitFieldType: "Signature" | "TextInput" | "Checkbox" | "DropDown" | "ListBox" | "Attachment" | "Link" | "FileReadConfirmation" | "PageReadConfirmation" | "AreaReadConfirmation" | "RadioButton" | "Approval" | "InvisibleSignature"

#WorkUnitFieldTypeResponse: "Signature" | "TextInput" | "Checkbox" | "DropDown" | "ListBox" | "Attachment" | "Link" | "FileReadConfirmation" | "PageReadConfirmation" | "AreaReadConfirmation" | "RadioButton" | "Approval" | "InvisibleSignature"

#WorkUnitFileReadConfirmationFieldResponse: #WorkUnitFieldResponse & {
	...
} & close({
	required!:  bool
	confirmed!: bool
	readOnly!:  bool
	fieldType!: "FileReadConfirmation"
})

#WorkUnitFileResponse: close({
	documentNumber!: int32 & int
	name!:           string
	tasks!: [...#WorkUnitFieldTaskResponse]
})

#WorkUnitFontStyleResponse: close({
	color!:  string
	size!:   number
	name!:   string
	bold!:   bool
	italic!: bool
	align!:  #WorkUnitTextAlignResponse
})

#WorkUnitInvisibleSignatureFieldResponse: #WorkUnitFieldResponse & {
	...
} & close({
	allowedSignatureTypes?: null | [...matchN(1, [#WorkUnitLocalCertificateSignatureResponseResponse, #WorkUnitRemoteCertificateSignatureResponseResponse, #WorkUnitDisposableCertificateSignatureResponseResponse, #WorkUnitPluginSignatureResponseResponse])]
	qualifiedTimeStamp?: null | bool
	fieldType!:          "InvisibleSignature"
})

#WorkUnitLinkFieldResponse: #WorkUnitFieldResponse & {
	...
} & close({
	page!:      int32 & int
	positionX!: number
	positionY!: number
	width!:     number
	height!:    number
	reference?: null | string
	fieldType!: "Link"
})

#WorkUnitListBoxFieldResponse: #WorkUnitFieldResponse & {
	...
} & close({
	page!:      int32 & int
	positionX!: number
	positionY!: number
	width!:     number
	height!:    number
	font!:      #WorkUnitFontStyleResponse
	options?: null | [...#WorkUnitOptionResponse]
	multiSelect!: bool
	required!:    bool
	readOnly!:    bool
	fieldType!:   "ListBox"
})

#WorkUnitLocalCertificateSignature: close({
	layoutId?:      null | string
	signatureType!: "LocalCertificateSignature"
})

#WorkUnitLocalCertificateSignatureResponseResponse: close({
	layoutId?:      null | string
	signatureType!: "LocalCertificateSignature"
})

#WorkUnitNumberInputConfigResponseResponse: #WorkUnitTextInputConfigResponse & {
	...
} & close({
	value?:              null | number
	symbol?:             null | #WorkUnitNumberSymbol
	thousandsSeparator?: null | #WorkUnitThousandsSeparatorTypeResponse
	decimalSeparator?:   null | #WorkUnitDecimalSeparatorTypeResponse
	decimalPlaces?:      null | int32 & int
	minValue?:           null | number
	maxValue?:           null | number
	textInputType!:      "NumberType"
})

#WorkUnitNumberInputValue: close({
	value!:         number
	textInputType!: "NumberType"
})

#WorkUnitNumberSymbol: close({
	value?:    null | string
	position!: #WorkUnitSymbolLocationTypeResponse
})

#WorkUnitOneTimePasswordSignature: close({
	layoutId?:      null | string
	signatureType!: "OneTimePasswordSignature"
})

#WorkUnitOneTimePasswordSignatureResponse: close({
	layoutId?:      null | string
	signatureType!: "OneTimePasswordSignature"
})

#WorkUnitOptionResponse: close({
	key!:      string
	value!:    string
	selected!: bool
})

#WorkUnitPageReadConfirmationFieldResponse: #WorkUnitFieldResponse & {
	...
} & close({
	page!:      int32 & int
	positionX!: number
	positionY!: number
	width!:     number
	height!:    number
	required!:  bool
	readOnly!:  bool
	fieldType!: "PageReadConfirmation"
})

#WorkUnitPhoneNumberInputConfigResponseResponse: #WorkUnitTextInputConfigResponse & {
	...
} & close({
	value!:         string
	format?:        null | string
	textInputType!: "PhoneNumberType"
})

#WorkUnitPluginSignature: close({
	pluginId!:      string
	signatureType!: "PluginSignature"
})

#WorkUnitPluginSignatureResponseResponse: close({
	pluginId!:      string
	signatureType!: "PluginSignature"
})

#WorkUnitRadioButtonFieldResponse: #WorkUnitFieldResponse & {
	...
} & close({
	page!:      int32 & int
	positionX!: number
	positionY!: number
	width!:     number
	height!:    number
	groupName!: string
	readOnly!:  bool
	checked!:   bool
	value!:     string
	required!:  bool
	fieldType!: "RadioButton"
})

#WorkUnitRemoteCertificateSignature: close({
	layoutId?:      null | string
	signatureType!: "RemoteCertificateSignature"
})

#WorkUnitRemoteCertificateSignatureResponseResponse: close({
	layoutId?:      null | string
	signatureType!: "RemoteCertificateSignature"
})

#WorkUnitResponse: close({
	id!: string
	files!: [...#WorkUnitFileResponse]
	isSequenceEnforced!: bool
	isFinished!:         bool
})

#WorkUnitSignatureFieldResponse: #WorkUnitFieldResponse & {
	...
} & close({
	page!:      int32 & int
	positionX!: number
	positionY!: number
	width!:     number
	height!:    number
	allowedSignatureTypes!: [...matchN(1, [#WorkUnitClickToSignSignatureResponse, #WorkUnitDrawToSignSignatureResponse, #WorkUnitTypeToSignSignatureResponse, #WorkUnitLocalCertificateSignatureResponseResponse, #WorkUnitDisposableCertificateSignatureResponseResponse, #WorkUnitBiometricSignatureResponse, #WorkUnitRemoteCertificateSignatureResponseResponse, #WorkUnitOneTimePasswordSignatureResponse, #WorkUnitPluginSignatureResponseResponse, #WorkUnitAutomaticSignatureResponse])]
	qualifiedTimeStamp?: null | bool
	required!:           bool
	readOnly!:           bool
	fieldType!:          "Signature"
})

#WorkUnitSignaturePosition: close({
	x!:      number
	y!:      number
	width!:  number
	height!: number
})

#WorkUnitSignaturePositionRequest: close({
	x!:      number
	y!:      number
	width!:  number
	height!: number
})

#WorkUnitStringInputConfigResponseResponse: #WorkUnitTextInputConfigResponse & {
	...
} & close({
	value!:         string
	password!:      bool
	multiline!:     bool
	maxLength!:     int32 & int
	textInputType!: "StringType"
})

#WorkUnitStringInputValue: close({
	value!:         string
	textInputType!: "StringType"
})

#WorkUnitSymbolLocationTypeResponse: "Start" | "StartWithBlank" | "End" | "EndWithBlank"

#WorkUnitTextAlignResponse: "Left" | "Center" | "Right"

#WorkUnitTextFieldResponse: #WorkUnitFieldResponse & {
	...
} & close({
	page!:      int32 & int
	positionX!: number
	positionY!: number
	width!:     number
	height!:    number
	font!:      #WorkUnitFontStyleResponse
	textInputConfig!: matchN(1, [#WorkUnitStringInputConfigResponseResponse, #WorkUnitDateInputConfigResponseResponse, #WorkUnitNumberInputConfigResponseResponse, #WorkUnitPhoneNumberInputConfigResponseResponse, #WorkUnitTimeInputConfigResponse])
	required!:  bool
	readOnly!:  bool
	fieldType!: "TextInput"
})

#WorkUnitTextInputConfigResponse: close({})

#WorkUnitTextInputType: "StringType" | "DateType" | "NumberType" | "PhoneNumberType" | "TimeType"

#WorkUnitTextInputTypeResponse: "StringType" | "DateType" | "NumberType" | "PhoneNumberType" | "TimeType"

#WorkUnitThousandsSeparatorTypeResponse: "None" | "Comma" | "Dot" | "Apostrophe" | "Space"

#WorkUnitTimeInputConfigResponse: #WorkUnitTextInputConfigResponse & {
	...
} & close({
	value?:         null | string
	format?:        null | #TimeFormatSwaggerEnumProvider
	minValue?:      null | string
	maxValue?:      null | string
	textInputType!: "TimeType"
})

#WorkUnitTypeToSignSignature: close({
	text?:                 null | string
	textFontFamily?:       null | string
	textFontColor?:        null | string
	textFontSizeFraction?: null | number
	position?:             null | #WorkUnitSignaturePosition
	layoutId?:             null | string
	signatureType!:        "TypeToSignSignature"
})

#WorkUnitTypeToSignSignatureRequest: close({
	signatureType!:        "TypeToSignSignature"
	text?:                 null | string
	textFontFamily?:       null | string
	textFontColor?:        null | string
	textFontSizeFraction?: null | number
	position?:             null | #WorkUnitSignaturePositionRequest
})

#WorkUnitTypeToSignSignatureResponse: close({
	layoutId?:      null | string
	signatureType!: "TypeToSignSignature"
})
