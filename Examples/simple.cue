#ATrustCertificateDto: {
	phoneNumber?: null | string
}

#ATrustCertificateSignatureTypeDto: {
	templateId?: null | string
	preferred?:  null | bool
	layoutId?:   null | string
}

#AbsoluteIntegrationExpirationDto: #IntegrationExpirationConfigurationDto & {
	...
} & {
	expiresAt?: null | string
	mode:      "Absolute"
}

#AccessCode: {
	code?: null | string
}

#Action: "Read" | "Write"

#AddDefaultUserGroupDto: {
	userGroupId: string
	defaultType: #UserDefaultUserGroupDefaultType
}

#AddUserGroupUserDto: {
	addedUsers: [...string]
	skippedUsers: [...string]
}

#AddUsersToUserGroupDto: {
	userIds?: null | [...string]
}

#AddedEnvelopeFileResponse: {
	id: string
}

#AddedTemplateFileResponse: {
	id: string
}

#AdminMeDto: {
	email:           string
	givenName:       string
	surname:         string
	isInstanceAdmin: bool
	isAdminUser:     bool
	users: [...#AdminMeUserDto]
}

#AdminMeUserDto: {
	userId:           string
	organizationId:   string
	organizationName: string
	isEnabled:        bool
}

#Agreement: {
	language: string
	body:     string
	title?:    null | string
}

#AgreementRequest: {
	language: string
	body:     string
	title?:    null | string
}

#AgreementResponse: {
	language: string
	body:     string
	title?:    null | string
}

#AgreementSettingsRequest: {
	enabled:     bool
	overridable: bool
	agreements: [...#AgreementRequest]
}

#AgreementSettingsResponse: {
	enabled:     bool
	overridable: bool
	agreements: [...#AgreementResponse]
}

#AllowedSignatureTypesDto: {
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
}

#AnnotationElementDefinition: {
	position:   #FileElementsPosition
	size:       #FileElementsSize
	textFormat: #FileElementTextFormat
	valueFormat: matchN(1, [#DateTimeDefinition, #InitialsDefinition, #TextDefinition, #FullNameDefinition, #FirstNameDefinition, #LastNameDefinition, #EmailDefinition])
}

#AnnotationElementDto: {
	elementId:         string
	elementDefinition: #AnnotationElementDefinition
	source:            #FormFieldSource
	recipientId?:       null | string
	elementName?:       null | string
}

#AnnotationField: #BaseField & {
	...
} & {
	valueFormat: matchN(1, [#DateTimeDefinition, #InitialsDefinition, #TextDefinition, #FullNameDefinition, #FirstNameDefinition, #LastNameDefinition, #EmailDefinition])
	font?:        null | #FontStyle
	elementName?: null | string
	fieldType:   "Annotation"
}

#AnnotationFieldDto: #BaseFieldDto & {
	...
} & {
	page:      int32 & int
	positionX: number
	positionY: number
	width:     number
	height:    number
	annotationConfig: matchN(1, [#FullNameAnnotationConfigDto, #FirstNameAnnotationConfigDto, #LastNameAnnotationConfigDto, #InitialsAnnotationConfigDto, #EmailAnnotationConfigDto, #DateAnnotationConfigDto, #TextAnnotationConfigDto])
	font?:        null | #FontStyle
	elementName?: null | string
	fieldType:   "Annotation"
}

#AnnotationType: "FullName" | "FirstName" | "LastName" | "Initials" | "Email" | "Date" | "Text"

#AnnotationValueFormat: "FullName" | "FirstName" | "LastName" | "Initials" | "Email" | "Date" | "Text"

#ApprovalField: #BaseField & {
	...
} & {
	fieldType: "Approval"
}

#ApprovalFieldDto: #BaseFieldDto & {
	...
} & {
	page:      int32 & int
	positionX: number
	positionY: number
	width:     number
	height:    number
	required:  bool
	fieldType: "Approval"
}

#ApproveElementDto: {
	elementId:         string
	elementDefinition: #SignatureElementDefinition
	source:            #FormFieldSource
	recipientId?:       null | string
	required:          bool
	displayName?:       null | string
	guidingOrder:      int32 & int
}

#AreaReadConfirmationDto: {
	elementId:         string
	required:          bool
	elementDefinition: #AreaReadElementDefinition
	source:            #FormFieldSource
	recipientId?:       null | string
	displayName?:       null | string
	guidingOrder:      int32 & int
}

#AreaReadConfirmationField: #BaseField & {
	...
} & {
	displayName?: null | string
	fieldType:   "AreaReadConfirmation"
}

#AreaReadConfirmationFieldDto: #BaseFieldDto & {
	...
} & {
	page:        int32 & int
	positionX:   number
	positionY:   number
	width:       number
	height:      number
	required:    bool
	displayName?: null | string
	fieldType:   "AreaReadConfirmation"
}

#AreaReadConfirmationTaskUpdateRequest: {
	fieldType: "AreaReadConfirmation"
}

#AreaReadElementDefinition: {
	position: #FileElementsPosition
	size:     #FileElementsSize
}

#AssociateMyNamirialIdDto: {
	myNamirialId: string
}

#AttachmentElementDefinition: {
	position: #FileElementsPosition
	size:     #FileElementsSize
}

#AttachmentElementDto: {
	elementId:         string
	required:          bool
	elementDefinition: #AttachmentElementDefinition
	source:            #FormFieldSource
	recipientId?:       null | string
	label:             string
	guidingOrder:      int32 & int
}

#AttachmentField: #BaseField & {
	...
} & {
	label:     string
	fieldType: "Attachment"
}

#AttachmentFieldDto: #BaseFieldDto & {
	...
} & {
	page:      int32 & int
	positionX: number
	positionY: number
	width:     number
	height:    number
	required:  bool
	fieldType: "Attachment"
}

#AttachmentTaskUpdateRequest: {
	fileName:  string
	content:   string
	fieldType: "Attachment"
}

#AuditTrailModeResponse: {
	auditTrailMode: #EnvelopeLogGeneration
}

#AutomaticESealingPermissions: {
	createUpdateDelete: bool
}

#AutomaticSealingProfileDetailResponse: {
	id:       string
	name:     string
	username: string
	password: string
}

#AutomaticSealingProfileRequest: {
	name:     string
	username: string
	password: string
}

#AutomaticSealingProfileResponse: {
	id:   string
	name: string
}

#AutomaticSignature: {
	layoutId?:      null | string
	signatureType: "AutomaticSignature"
}

#AutomaticSignatureDataDto: {
	profileId?: null | string
	pluginId?:  null | string
}

#AutomaticSignatureTypeDto: {
	useExternalSignatureImage?: null | #ExternalSignatureImageMode
	preferred?:                 null | bool
	layoutId?:                  null | string
	stampImprintConfiguration?: null | #ClickToSignStampImprintDto
}

#BackgroundImageDto: {
	mimeType:   string
	dataBase64: string
}

#BankIdSettingsDto: {
	authenticationCertificateThumbprint?: null | string
}

#BaseField: {
	id:        string
	page:      int32 & int
	positionX: number
	positionY: number
	width:     number
	height:    number
	required:  bool
	source:    #FormFieldSource
	...
}

#BaseFieldDto: {
	id: string
	...
}

#BatchAssignUserGroupUserRoleDto: {
	userIds: [...string]
	businessRoleId: string
}

#BatchDeleteUserGroupUserRoleDto: {
	userIds: [...string]
}

#BatchMode: "Basic" | "OptIn" | "OptOut" | "OptOutWithRequiredAlwaysSelected" | "OptInWithRequiredAlwaysSelected"

#BiometricSignature: {
	signatureType: "BiometricSignature"
}

#BiometricSignaturePositioning: "WithinField" | "OnPage" | "IntersectsWithField"

#BiometricSignatureTypeDto: {
	biometricVerification?:             null | bool
	allowBiometricStoringOnly?:         null | bool
	storeSignedResponseWithoutBioData?: null | bool
	biometricServerUserId?:             null | string
	signaturePositioning?:              null | #BiometricSignaturePositioning
	preferred?:                         null | bool
	layoutId?:                          null | string
}

#BulkEnvelopeDetailDto: {
	id:   string
	name: string
	stages: [...#BulkStageDto]
	documents?: null | [...#Document]
}

#BulkEnvelopeFieldTaskItem: {
	field: matchN(1, [#SignatureFieldDto, #TextFieldDto, #CheckboxFieldDto, #DropDownFieldDto, #ListBoxFieldDto, #AttachmentFieldDto, #AnnotationFieldDto, #LinkFieldDto, #FileReadConfirmationFieldDto, #PageReadConfirmationFieldDto, #AreaReadConfirmationFieldDto, #RadioButtonFieldDto, #ApprovalFieldDto, #InvisibleSignatureFieldDto])
	sortOrder:   int32 & int
	recipientId?: null | string
	source:      #ElementSource
	stageId?:     null | string
}

#BulkEnvelopeFieldTaskItemRequest: {
	field: matchN(1, [#SignatureFieldDto, #TextFieldDto, #CheckboxFieldDto, #DropDownFieldDto, #ListBoxFieldDto, #AttachmentFieldDto, #AnnotationFieldDto, #LinkFieldDto, #FileReadConfirmationFieldDto, #PageReadConfirmationFieldDto, #AreaReadConfirmationFieldDto, #RadioButtonFieldDto, #ApprovalFieldDto, #InvisibleSignatureFieldDto])
	sortOrder:   int32 & int
	recipientId?: null | string
	stageId?:     null | string
}

#BulkEnvelopeFileTasksResponse: {
	tasks: [...#BulkEnvelopeFieldTaskItem]
}

#BulkEnvelopeListDto: {
	bulkEnvelopes: [...#BulkEnvelopePartialDto]
	pagination: #PaginationDto
}

#BulkEnvelopePartialDto: {
	id:        string
	name:      string
	status:    string
	createdAt: string
	updatedAt: string
}

#BulkRecipientDefinition: {
	givenName:   string
	surname:     string
	email:       string
	phoneNumber?: null | string
}

#BulkRecipientDto: {
	id:                  string
	givenName?:           null | string
	surname?:             null | string
	email?:               null | string
	phoneNumber?:         null | string
	recipientType:       #RecipientType
	notificationChannel: #NotificationChannel
	order:               int32 & int
}

#BulkRecipientValidationErrorResponse: {
	errors: [...#RowError]
}

#BulkStageDto: {
	id:                        string
	mandatoryRecipientsNumber: int32 & int
	name?:                      null | string
	stageMode:                 #StageMode
	recipients?: null | [...#BulkRecipientDto]
}

#BusinessRoleCreateDto: {
	name:        string
	description?: null | string
}

#BusinessRoleDto: {
	id:              string
	organizationId:  string
	name:            string
	description?:     null | string
	assignmentCount: int32 & int
	createdAt:       string
	updatedAt:       string
}

#BusinessRoleUpdateDto: {
	name:        string
	description?: null | string
}

#BusinessRolesListDto: {
	items: [...#BusinessRoleDto]
	pagination: #PaginationDto
}

#BusinessRolesSortingKey: "Name"

#CallbackConfigurationDto: {
	callbackUrl?:             null | string
	statusUpdateCallbackUrl?: null | string
	afterSendCallbackUrl?:    null | string
}

#CertificateDetailsResponse: {
	subjectName:    string
	thumbprint:     string
	expirationDate: string
	issuer:         string
}

#CheckBoxElementDefinition: {
	position:    #FileElementsPosition
	size:        #FileElementsSize
	exportValue: string
	readOnly:    bool
}

#CheckBoxElementDto: {
	elementId:         string
	elementDefinition: #CheckBoxElementDefinition
	source:            #FormFieldSource
	required:          bool
	isChecked:         bool
	recipientId?:       null | string
	guidingOrder:      int32 & int
}

#CheckboxField: #BaseField & {
	...
} & {
	readOnly:  bool
	checked:   bool
	value:     string
	fieldType: "Checkbox"
}

#CheckboxFieldDto: #BaseFieldDto & {
	...
} & {
	page:      int32 & int
	positionX: number
	positionY: number
	width:     number
	height:    number
	checked:   bool
	value:     string
	required:  bool
	readOnly:  bool
	fieldType: "Checkbox"
}

#CheckboxTaskUpdateRequest: {
	isChecked: bool
	fieldType: "Checkbox"
}

#ClickToSignEnvelopeBulkSignDto: #EnvelopeBulkSignDto & {
	...
} & {
	signatureType: "ClickToSign"
}

#ClickToSignSignature: {
	layoutId?:      null | string
	signatureType: "ClickToSignSignature"
}

#ClickToSignSignatureTypeDto: {
	useExternalSignatureImage?: null | #ExternalSignatureImageMode
	preferred?:                 null | bool
	layoutId?:                  null | string
	stampImprintConfiguration?: null | #ClickToSignStampImprintDto
}

#ClickToSignStampImprintDto: {
	displayName:             bool
	displaySignatureDate:    bool
	displayExtraInformation: bool
	fontName?:                null | string
	fontSizeInPt?:            null | int32 & int
	displayEmail:            bool
	displayIp:               bool
}

#ClonedEnvelopeDto: {
	id: string
}

#ContactDto: {
	id:             string
	givenName:      string
	surname:        string
	email:          string
	cultureIsoCode: string
	phoneNumber?:    null | string
}

#ContactImportResultDto: {
	imported: int32 & int
}

#ContactImportValidationErrorResponse: {
	errors: [...#RowError]
}

#ContactListDto: {
	contacts: [...#ContactDto]
	pagination: #PaginationDto
}

#ContactRequest: {
	givenName:      string
	surname:        string
	email:          string
	cultureIsoCode: string
	phoneNumber?:    null | string
}

#ContactsSortingKey: "GivenName" | "Surname" | "Email"

#CountriesDto: {
	options: [...#CountryListItemDto]
	selectedId?:      null | int32 & int @deprecated()
	selectedIsoCode?: null | string
}

#CountriesLookupResponse: {
	countries: [...#CountryDto]
}

#CountryDto: {
	name: string
	code: string
}

#CountryListItemDto: {
	id:          int32 & int @deprecated()
	isoCode:     string
	englishName: string
}

#CreateATrustCertificateDto: {
	phoneNumber?: null | string
}

#CreateAccessCodeDto: {
	code?: null | string
}

#CreateAuthenticationConfigurationDto: {
	accessCode?:         null | #CreateAccessCodeDto
	smsOneTimePassword?: null | #CreateSmsOneTimePasswordDto
	oAuthAuthentications?: null | [...#CreateOAuthAuthenticationDto]
}

#CreateAutomaticSignatureDataDto: {
	profileId?: null | string
	pluginId?:  null | string
}

#CreateBulkEnvelopeStageRequest: {
	type:                         #EnvelopeStageType
	name?:                         null | string
	requiredRecipientCompletions?: null | int32 & int
	mode:                         #StageMode
}

#CreateDisposableCertificateDto: {
	documentIssuingCountry?:       null | string
	identificationIssuingCountry?: null | string
	identificationType?:           null | string
	phoneNumber?:                  null | string
	documentType?:                 null | string
	documentIssuedBy?:             null | string
	documentIssuedOn?:             null | string
	documentExpiryDate?:           null | string
	serialNumber?:                 null | string
	documentNumber?:               null | string
}

#CreateDocumentClassRequest: {
	name:        string
	description: string
	metadata: [...#DocumentClassMetadataFieldDto]
}

#CreateEnvelopeStageAutomaticRecipientRequest: #CreateEnvelopeStageRecipientRequest & {
	...
} & {
	signatureProfile?:           null | string
	signatureReasonAllowChange?: null | bool
	metadata?: null | [...#RecipientMetadataEntry]
	type: "Automatic"
}

#CreateEnvelopeStageRecipientRequest: {
	languageCode?:    null | string
	signatureReason?: null | string
	...
}

#CreateEnvelopeStageRequest: {
	type:                         #EnvelopeStageType
	name?:                         null | string
	requiredRecipientCompletions?: null | int32 & int
}

#CreateEnvelopeStageStandardRecipientRequest: #CreateEnvelopeStageRecipientRequest & {
	...
} & {
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
	type: "Standard"
}

#CreateGenericSigningPluginsSenderDataDto: {
	senderGenericSigningPlugins?: null | [...#CreateSenderGenericSigningPluginDto]
}

#CreateOAuthAuthenticationDto: {
	providerName?: null | string
	externalId:   string
}

#CreateOAuthFieldDefinitionRequest: {
	path:                         string
	mode:                         #OAuthSignerProviderFieldMode
	target:                       #OAuthSignerProviderFieldTarget
	customFieldName?:              null | string
	genericSigningPluginId?:       null | string
	genericSigningPluginFieldKey?: null | string
}

#CreateOAuthJwtConfigRequest: {
	jwksUri:          string
	issuer:           string
	enforceNonce:     bool
	validateAudience: bool
	validateIssuer:   bool
	validateLifetime: bool
	oAuthFieldDefinitions?: null | [...#CreateOAuthFieldDefinitionRequest]
}

#CreateOAuthResourceUriRequest: {
	uri:                   string
	accessTokenParamName:  string
	eIdServiceCombination?: null | string
	oAuthFieldDefinitions?: null | [...#CreateOAuthFieldDefinitionRequest]
}

#CreateOAuthSignerProviderDetailsRequest: {
	oAuthSignerProvider: #CreateOAuthSignerProviderRequest
	oAuthJwtConfig?:      null | #CreateOAuthJwtConfigRequest
	oAuthResourceUris?: null | [...#CreateOAuthResourceUriRequest]
}

#CreateOAuthSignerProviderRequest: {
	name:             string
	clientId:         string
	clientSecret:     string
	authorizationUri: string
	tokenUri:         string
	scope?:            null | string
	logoutUri?:        null | string
}

#CreateOrganizationDto: {
	name:                                  string
	isoCulture:                            string
	license:                               #LicenseDto
	onePlatformBusinessRelationIdentifier: string
	featureFlagsNames: [...string]
}

#CreateOrganizationUserRequestDto: {
	givenName:        string
	surname:          string
	email:            string
	regionalSettings: #UserRegionalSettingsRequestDto
	phoneNumber?:      null | string
}

#CreateOrganizationUserResponse: {
	id: string
}

#CreateOtpSignatureDataDto: {
	type?:        null | #OtpDeliveryChannel
	phoneNumber?: null | string
}

#CreatePersonalAccessTokenRequest: {
	name:      string
	expiresAt: string
}

#CreatePolicyRequest: {
	name:            string
	isActive:        bool
	description?:     null | string
	documentClassId?: null | string
	conditions?: null | [...#PolicyConditionRequest]
}

#CreateRemoteCertificateDto: {
	userId?:   null | string
	deviceId?: null | string
}

#CreateRoleRequest: {
	name: string
	permissions: [...#PermissionDto]
	description?: null | string
}

#CreateSenderGenericSigningPluginDto: {
	pluginId?: null | string
	settings?: null | [...#CreateSenderGenericSigningPluginSettingsDto]
}

#CreateSenderGenericSigningPluginSettingsDto: {
	key?:   null | string
	value?: null | string
}

#CreateServiceAccountRequest: {
	clientId:         string
	email:            string
	regionalSettings: #UserRegionalSettingsDto
}

#CreateServiceAccountResponse: {
	clientId:     string
	clientSecret: string
	userId:       string
}

#CreateSignatureDataConfigurationDto: {
	disposableCertificate?:           null | #CreateDisposableCertificateDto
	remoteCertificate?:               null | #CreateRemoteCertificateDto
	aTrustCertificate?:               null | #CreateATrustCertificateDto
	swissComOnDemand?:                null | #CreateSwissComOnDemandDto
	swedishBankId?:                   null | #CreateSwedishBankIdDto
	otpSignatureData?:                null | #CreateOtpSignatureDataDto
	genericSigningPluginsSenderData?: null | #CreateGenericSigningPluginsSenderDataDto
	automaticSignatureData?:          null | #CreateAutomaticSignatureDataDto
}

#CreateSmsOneTimePasswordDto: {
	phoneNumber?: null | string
}

#CreateStageResponse: {
	id: string
}

#CreateSubstituteDelegationDto: {
	delegateeUserEmail:        string
	utilizeAlsoOnCCRecipients: bool
	reason?:                    null | string
	startDate?:                 null | string
	endDate?:                   null | string
}

#CreateSwedishBankIdDto: {
	personalNumber?:         null | string
	allowAnyPersonalNumber?: null | bool
}

#CreateSwissComOnDemandDto: {
	commonName?:   null | string
	country?:      null | string
	phoneNumber?:  null | string
	organization?: null | string
	organizationUnits?: null | [...string]
	locality?:        null | string
	serialNumber?:    null | string
	stateOrProvince?: null | string
	pseudonym?:       null | string
}

#CreateTemplateStageAutomaticRecipientRequest: #CreateTemplateStageRecipientRequest & {
	...
} & {
	signatureProfile?:           null | string
	signatureReasonAllowChange?: null | bool
	metadata?: null | [...#RecipientMetadataEntry]
	type: "Automatic"
}

#CreateTemplateStageRecipientRequest: {
	languageCode?:    null | string
	signatureReason?: null | string
	...
}

#CreateTemplateStageRequest: {
	type:                         #EnvelopeStageType
	name?:                         null | string
	requiredRecipientCompletions?: null | int32 & int
}

#CreateTemplateStageStandardRecipientRequest: #CreateTemplateStageRecipientRequest & {
	...
} & {
	givenName?:                  null | string
	surname?:                    null | string
	email?:                      null | string
	phoneNumber?:                null | string
	notificationChannel?:        null | #NotificationChannel
	authentication?:             null | #CreateAuthenticationConfigurationDto
	signatureConfiguration?:     null | #CreateSignatureDataConfigurationDto
	personalMessage?:            null | string
	signatureReasonAllowChange?: null | bool
	isDelegationEnabled:        bool
	metadata?: null | [...#RecipientMetadataEntry]
	type: "Standard"
}

#CreateUserDto: {
	givenName:   string
	surname:     string
	email:       string
	isoLanguage: string
	enabled:     bool
	roleNames: [...#UserRoleRequest]
}

#CreatedDocumentClassDto: {
	id: string
}

#CreatedEnvelopeDto: {
	id: string
}

#CreatedEnvelopeFromTemplateDto: {
	createdEnvelopeId: string
}

#CreatedOrganizationDto: {
	id: string
}

#CreatedPersonalAccessTokenResponse: {
	id:        string
	name:      string
	token:     string
	createdAt: string
	expiresAt: string
}

#CreatedPolicyResponse: {
	id: string
}

#CreatedRecipientResponse: {
	id: string
}

#CreatedStageResponse: {
	id: string
}

#CreatedTemplateDto: {
	id: string
}

#CreatedTemplateStageRecipientDto: {
	id: string
}

#CreatedUserDto: {
	id: string
}

#DataFieldType: "Text" | "PhoneNumber" | "Number" | "List" | "Email" | "Password"

#DateAnnotationConfigDto: {
	format:         #DateFormatSwaggerEnumProvider
	annotationType: "Date"
}

#DateFormatSwaggerEnumProvider: "dd.MM.yy" | "dd.MM.yyyy"

#DateInputConfig: #TextInputConfig & {
	...
} & {
	value?:         null | "2006-01-02"
	format?:        null | #DateFormatSwaggerEnumProvider
	minValue?:      null | "2006-01-02"
	maxValue?:      null | "2006-01-02"
	textInputType: "DateType"
}

#DateTimeDefinition: {
	dateTimeFormat: string
	valueFormat:    "Date"
}

#DateTimeFormatDto: {
	code:    string
	example: string
}

#DateTimeFormatSwaggerEnumProvider: "dd/MM/yyyy | HH:mm" | "dd/MM/yy | HH:mm" | "dd-MMM-yy | HH:mm" | "dd-MMM-yyyy | HH:mm" | "dd MMMM yyyy | HH:mm" | "yyyy-MM-dd | HH:mm" | "yyyy-MMM-dd | HH:mm" | "yyyy MMMM dd | HH:mm" | "MMM d, yyyy | HH:mm" | "MMM-dd-yyyy | HH:mm" | "MMMM d, yyyy | HH:mm" | "MM/d/yyyy | HH:mm" | "dd.MM.yyyy | HH:mm" | "dd. MMMM yyyy | HH:mm" | "dd.MM.yy | HH:mm"

#DateTimeFormatsDto: {
	options: [...#DateTimeOptionDto]
	selectedId?:   null | int32 & int @deprecated()
	selectedName?: null | string
}

#DateTimeFormatsLookupResponse: {
	dateTimeFormats: [...#DateTimeFormatDto]
}

#DateTimeOptionDto: {
	id:     int32 & int @deprecated()
	name:   string
	sample: string
}

#DbEnvelopeStatus: "Started" | "InProgress" | "Canceled" | "Completed" | "Expired" | "Rejected" | "Draft" | "Template"

#DbRecipientType: "Signer" | "CC" | "Acknowledge" | "Pkcs7Signer" | "Automatic" | "Approver"

#DbWorkstepResult: "NotSigned" | "Signed" | "Rejected" | "Delegated" | "DelegatedAutomated"

#DecimalSeparatorType: "Comma" | "Point" | "Apostrophe" | "None"

#DefaultUserGroupsDto: {
	envelopesShare: [...#UserDefaultUserGroup]
	templatesShare: [...#UserDefaultUserGroup]
}

#DelegationInfo: {
	enabled:                 bool
	defaultDelegationPolicy?: null | #DelegationPolicy
}

#DelegationPolicy: "Deactivated" | "ActivatedWithDefaultOff" | "ActivatedWithDefaultOn"

#DisposableCertificateDto: {
	documentIssuingCountry?:       null | string
	identificationIssuingCountry?: null | string
	identificationType?:           null | string
	phoneNumber?:                  null | string
	documentType?:                 null | string
	documentIssuedBy?:             null | string
	documentIssuedOn?:             null | string
	documentExpiryDate?:           null | string
	serialNumber?:                 null | string
	documentNumber?:               null | string
}

#DisposableCertificateSettingsDto: {
	lraId?:                                         null | string
	user?:                                          null | string
	hasPassword:                                   bool
	disposableType?:                                null | #DisposableType
	showDisclaimerBeforeCertificateRequest:        bool
	sendDisposableDisclaimerDocumentNotifications: bool
}

#DisposableCertificateSignature: {
	layoutId?:      null | string
	signatureType: "DisposableCertificateSignature"
}

#DisposableCertificateSignatureTypeDto: {
	useExternalSignatureImage?: null | #ExternalSignatureImageMode
	preferred?:                 null | bool
	layoutId?:                  null | string
	stampImprintConfiguration?: null | #DisposableCertificateStampImprintDto
	isLongLived?:               null | bool
	validityInSeconds?:         null | int32 & int
}

#DisposableCertificateStampImprintDto: {
	displayName:             bool
	displaySignatureDate:    bool
	displayExtraInformation: bool
	fontName?:                null | string
	fontSizeInPt?:            null | int32 & int
	displayIp:               bool
}

#DisposableType: "Disposable" | "LeanDisposable" | "LeanDisposableExtendedValidity"

#Document: {
	id:              string
	name:            string
	sortOrder:       int32 & int
	documentClassId?: null | string
}

#DocumentClassDto: {
	id:          string
	name:        string
	description?: null | string
	metadata?: null | [...#DocumentClassMetadataDto]
}

#DocumentClassListItemDto: {
	id:                 string
	name:               string
	description?:        null | string
	associatedPolicies?: null | string
}

#DocumentClassLookupResponse: {
	id:   string
	name: string
}

#DocumentClassMetadataDto: {
	id:        string
	name:      string
	dataType:  #MetadataDataType
	required:  bool
	sortOrder: int32 & int
}

#DocumentClassMetadataFieldDto: {
	name:      string
	dataType:  #MetadataDataType
	required:  bool
	sortOrder: int32 & int
}

#DocumentClassesResponse: {
	documentClasses: [...#DocumentClassListItemDto]
	pagination: #PaginationResponse
}

#DocumentClassesSortingKey: "Name" | "AssociatedPolicies"

#DocumentReadConfirmationDto: {
	elementId:    string
	required:     bool
	recipientId?:  null | string
	guidingOrder: int32 & int
	displayName?:  null | string
}

#DocumentsUploadRequest: {
	files: [...string]
}

#DrawToSignSignature: {
	layoutId?:      null | string
	signatureType: "DrawToSignSignature"
}

#DrawToSignSignatureTypeDto: {
	useExternalSignatureImage?: null | #ExternalSignatureImageMode
	preferred?:                 null | bool
	layoutId?:                  null | string
	stampImprintConfiguration?: null | #DrawToSignStampImprintDto
}

#DrawToSignStampImprintDto: {
	displayName:             bool
	displaySignatureDate:    bool
	displayExtraInformation: bool
	fontName?:                null | string
	fontSizeInPt?:            null | int32 & int
	displayEmail:            bool
	displayIp:               bool
}

#DropDownElementDefinition: {
	position:   #FileElementsPosition
	size:       #FileElementsSize
	readOnly:   bool
	textFormat: #FileElementTextFormat
}

#DropDownElementDto: {
	elementDefinition: #DropDownElementDefinition
	source:            #FormFieldSource
	elementId:         string
	recipientId?:       null | string
	required:          bool
	editable?:          null | bool
	value?:             null | string
	guidingOrder:      int32 & int
	items?: null | [...#DropDownItemEntry]
}

#DropDownField: #BaseField & {
	...
} & {
	readOnly: bool
	font?:     null | #FontStyle
	options?: null | [...#Option]
	editable?:      null | bool
	selectedValue?: null | string
	fieldType:     "DropDown"
}

#DropDownFieldDto: #BaseFieldDto & {
	...
} & {
	page:      int32 & int
	positionX: number
	positionY: number
	width:     number
	height:    number
	font:      #FontStyle
	options?: null | [...#OptionDto]
	required:  bool
	readOnly:  bool
	fieldType: "DropDown"
}

#DropDownItemEntry: {
	value:      string
	label:      string
	isSelected?: null | bool
}

#DropDownTaskUpdateRequest: {
	value:     string
	fieldType: "DropDown"
}

#ESealingRemoteSignatureProfileDto: {
	id:           string
	friendlyName?: null | string
}

#ElementSource: "File" | "UserDefined"

#EmailAnnotationConfigDto: {
	annotationType: "Email"
}

#EmailDefinition: {
	valueFormat: "Email"
}

#EmailSenderDisplayType: "SenderName" | "Organization" | "ProductName"

#EnableOrganizationDto: {
	onePlatformBusinessRelationIdentifier: string
}

#EnabledOrganizationDto: {
	id: string
}

#Entity: "Envelope" | "Template" | "Organization" | "User" | "UserGroup"

#EnvelopeAction: "Sign" | "View" | "Remind" | "Download" | "Delete" | "Restart" | "Cancel" | "Continue" | "Clone" | "Approve" | "Unlock" | "Share"

#EnvelopeActionResponse: {
	envelopeId: string
	statusCode: int32 & int
	message?:    null | string
}

#EnvelopeActorDto: {
	email: string
}

#EnvelopeBacklogDto: {
	id:         string
	name:       string
	senderName: string
	sentDate:   string
}

#EnvelopeBulkSignDeviceDto: {
	deviceId:                  string
	otpDeviceType:             string
	otpDeviceTypeId:           string
	identificationInformation: string
}

#EnvelopeBulkSignDevicesResponseDto: {
	devices: [...#EnvelopeBulkSignDeviceDto]
}

#EnvelopeBulkSignDto: {
	envelopeIds: [...string]
	ipAddress?: null | string
	...
}

#EnvelopeBulkSignResultDto: {
	signedEnvelopes: [...string]
	failedEnvelopes: [...#FailedEnvelope]
}

#EnvelopeBulkSignSignatureType: "ClickToSign" | "RemoteCertificate"

#EnvelopeBulkSignTransactionDto: {
	transactionId: string
	payloadFileId: string
	expiresAt?:     null | string
}

#EnvelopeCancelRequestDto: {
	reason?: null | string
}

#EnvelopeDetailDto: {
	id:                      string
	name:                    string
	status:                  #EnvelopeDetailStatus
	expiringSoon:            bool
	sendCopyToAllRecipients: bool
	actions: [...#EnvelopeAction]
	updatedAt:      string
	sentAt?:         null | string
	expirationDate?: null | string
	defaultAction?:  null | #EnvelopeAction
	documents?: null | [...#Document]
	stages?: null | [...#EnvelopeDetailStageDto]
	preventFieldsEditingWhenFinished: bool
}

#EnvelopeDetailRecipientDto: {
	id:                           string
	givenName:                    string
	surname:                      string
	email:                        string
	placeholder?:                  null | string
	order?:                        null | int32 & int
	type?:                         null | #RecipientType
	status?:                       null | #RecipientStatus
	statusReason?:                 null | string
	lastAction?:                   null | #LastRecipientAction
	lastActionDate?:               null | string
	viewerLink?:                   null | string
	stageId?:                      null | string
	signatureProfile?:             null | string
	requiresDelegationCompletion: bool
}

#EnvelopeDetailStageDto: {
	id:                           string
	sortOrder:                    int32 & int
	requiredRecipientCompletions: int32 & int
	recipients: [...#EnvelopeDetailRecipientDto]
	name?: null | string
}

#EnvelopeDetailStatus: "WaitingForYou" | "WaitingForOthers" | "Completed" | "Rejected" | "Expired" | "Canceled" | "Draft"

#EnvelopeDownloadDto: {
	id:   string
	name: string
	type: string
}

#EnvelopeDownloadsResponse: {
	downloads: [...#EnvelopeDownloadDto]
}

#EnvelopeDto: {
	id:                                                             string
	name?:                                                           null | string
	defaultSubject?:                                                 null | string
	defaultBody?:                                                    null | string
	sendCopyToAllRecipients:                                        bool
	lateIdent:                                                      bool
	useInvisibleSignatureWithTimestampForAllDocumentsAndRecipients: bool
	showOrganizationAgreements:                                     bool
	reminderConfiguration:                                          #ReminderConfigurationDto
	expirationConfiguration:                                        #ExpirationConfigurationDto
	recipients?: null | [...#RecipientDto]
	stages?: null | [...#StageDto]
	documents?: null | [...#Document]
	agreements?: null | [...#Agreement]
	userGroupSharingIds: [...string]
	callbackConfiguration?:            null | #CallbackConfigurationDto
	status:                           #DbEnvelopeStatus
	createdAt:                        string
	updatedAt:                        string
	preventFieldsEditingWhenFinished: bool
	afterSendRedirectUrl?:             null | string
	signatureReason?:                  null | string
	signatureReasonAllowChange:       bool
	signatureFormat:                  #SignatureFormat
	fileRestrictedVisibility:         bool
}

#EnvelopeEventDto: {
	id:         string
	type:       #EnvelopeEventType
	occurredAt: string
	actor:      #EnvelopeActorDto
	data: [string]: string
}

#EnvelopeEventType: "Created" | "Canceled" | "Completed" | "Deleted" | "NotificationSent" | "Rejected" | "Sent" | "WorkstepCompleted" | "StartSending" | "StartRestarting" | "Restarted"

#EnvelopeEventsDto: {
	events: [...#EnvelopeEventDto]
}

#EnvelopeFieldTaskItem: {
	field: matchN(1, [#SignatureFieldDto, #TextFieldDto, #CheckboxFieldDto, #DropDownFieldDto, #ListBoxFieldDto, #AttachmentFieldDto, #AnnotationFieldDto, #LinkFieldDto, #FileReadConfirmationFieldDto, #PageReadConfirmationFieldDto, #AreaReadConfirmationFieldDto, #RadioButtonFieldDto, #ApprovalFieldDto, #InvisibleSignatureFieldDto])
	sortOrder:   int32 & int
	recipientId?: null | string
	source:      #ElementSource
}

#EnvelopeFileDetailDocumentClassDto: {
	documentClassId: string
	name?:            null | string
	metadataValues?: null | [...#EnvelopeFileMetadataValueDto]
}

#EnvelopeFileDetailDocumentClassRequest: {
	documentClassId: string
	metadataValues: [...#MetadataValueDto]
}

#EnvelopeFileMetadataValueDto: {
	fieldDefinitionId: string
	name?:              null | string
	value?:             null | string
	type:              #MetadataDataType
}

#EnvelopeFileTasksResponse: {
	tasks: [...#EnvelopeFieldTaskItem]
}

#EnvelopeFilesResponse: {
	files: [...#Document]
}

#EnvelopeInsights: {
	waitingForYou:    #SingleInsight
	waitingForOthers: #SingleInsight
	draft:            #SingleInsight
	completed:        #SingleInsight
	rejected:         #SingleInsight
	expired:          #SingleInsight
}

#EnvelopeListDto: {
	envelopes: [...#EnvelopePartialDto]
	pagination: #PaginationDto
}

#EnvelopeLogGeneration: "Standard" | "PDFA2B" | "Disabled"

#EnvelopePartialDto: {
	id:           string
	name:         string
	expiringSoon: bool
	senderUser:   #EnvelopeSenderDto
	updatedAt:    string
	status:       #EnvelopeDetailStatus
	actions: [...#EnvelopeAction]
	createdAt: string
	sentAt?:    null | string
	recipients?: null | [...#EnvelopeDetailRecipientDto]
	defaultAction?: null | #EnvelopeAction
}

#EnvelopePermissions: {
	read:               bool
	createUpdateDelete: bool
}

#EnvelopePoliciesVerifyDto: {
	compliant: bool
}

#EnvelopeRejectDto: {
	message?: null | string
}

#EnvelopeResumeDto: {
	newExpirationDate?:               null | string
	expirationInSecondsAfterSending?: null | int64 & int
}

#EnvelopeSenderDto: {
	givenName?: null | string
	surname?:   null | string
	email?:     null | string
}

#EnvelopeSignatureTypeDto: {
	id: string
	signatureTypes: [...#SignatureType]
	canBeSignedInBulk: bool
}

#EnvelopeSignatureTypesRequestDto: {
	ids: [...string]
}

#EnvelopeSortingKey: "LastUpdated" | "Name"

#EnvelopeStageAutomaticRecipientResponse: #EnvelopeStageRecipientResponse & {
	...
} & {
	signatureProfile?:           null | string
	signatureReason?:            null | string
	signatureReasonAllowChange?: null | bool
	metadata?: null | [...#RecipientMetadataEntry]
	type: "Automatic"
}

#EnvelopeStageAutomaticRecipientSummaryDto: #EnvelopeStageRecipientSummaryDto & {
	...
} & {
	signatureProfile?: null | string
	type:             "Automatic"
}

#EnvelopeStageItemDto: {
	id:                           string
	name?:                         null | string
	sortOrder:                    int32 & int
	requiredRecipientCompletions: int32 & int
	type:                         #EnvelopeStageType
	recipients: [...matchN(1, [#EnvelopeStageStandardRecipientSummaryDto, #EnvelopeStageAutomaticRecipientSummaryDto])]
}

#EnvelopeStageListDto: {
	stages: [...#EnvelopeStageItemDto]
}

#EnvelopeStageRecipientResponse: {
	id:           string
	languageCode?: null | string
	...
}

#EnvelopeStageRecipientSummaryDto: {
	id: string
	...
}

#EnvelopeStageStandardRecipientResponse: #EnvelopeStageRecipientResponse & {
	...
} & {
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
	type:                     "Standard"
}

#EnvelopeStageStandardRecipientSummaryDto: #EnvelopeStageRecipientSummaryDto & {
	...
} & {
	givenName:           string
	surname:             string
	email:               string
	phoneNumber?:         null | string
	notificationChannel?: null | string
	type:                "Standard"
}

#EnvelopeStageType: "Signer" | "CarbonCopy" | "Viewer" | "Automatic" | "Approver"

#EnvelopeType: "Envelope" | "Template"

#EnvelopeViewerLinkDto: {
	viewerLink: string
}

#ErrorCode: "G001" | "G002" | "G003" | "G004" | "G005" | "G006" | "G007" | "G008" | "A001" | "A002" | "A003" | "A004" | "A005" | "A006" | "A007" | "A008" | "A009" | "A010" | "A011" | "A012" | "A013" | "A014" | "A015" | "A016" | "A017" | "A018" | "A019" | "A020" | "C001" | "C002" | "E001" | "E002" | "E003" | "E004" | "E005" | "E006" | "E008" | "E009" | "E010" | "E011" | "E012" | "E013" | "E014" | "E015" | "E016" | "E017" | "E018" | "E019" | "E020" | "E021" | "E022" | "E023" | "E024" | "E025" | "E026" | "E027" | "E028" | "E029" | "E030" | "E031" | "E032" | "E033" | "E034" | "E035" | "E036" | "E037" | "E038" | "E039" | "E040" | "E041" | "E042" | "E043" | "E044" | "E045" | "E046" | "E047" | "E048" | "E049" | "E050" | "E051" | "E052" | "E053" | "E054" | "E055" | "E056" | "E057" | "E058" | "E059" | "E060" | "E061" | "E062" | "E063" | "E064" | "F001" | "F002" | "F003" | "F004" | "F005" | "F006" | "F007" | "O001" | "O002" | "O003" | "O004" | "O005" | "O007" | "O008" | "O009" | "O010" | "O011" | "O012" | "O013" | "O014" | "O015" | "O016" | "O017" | "O018" | "O019" | "O020" | "O021" | "O022" | "O023" | "O024" | "O025" | "O026" | "O027" | "O028" | "O029" | "O030" | "O031" | "O032" | "O033" | "O034" | "O035" | "O036" | "O037" | "O038" | "O039" | "O040" | "O041" | "O042" | "O043" | "O044" | "O045" | "O046" | "O047" | "O048" | "O049" | "O050" | "O051" | "O052" | "O053" | "O054" | "R001" | "R002" | "R003" | "R004" | "R005" | "R006" | "R007" | "R008" | "R009" | "R010" | "R011" | "R012" | "R013" | "R014" | "R015" | "R016" | "R017" | "R018" | "R019" | "R020" | "R021" | "R022" | "R023" | "R024" | "R025" | "R026" | "R027" | "R028" | "R029" | "R030" | "R031" | "R032" | "R033" | "R034" | "R035" | "ST001" | "ST002" | "ST003" | "ST004" | "ST005" | "ST006" | "ST007" | "ST008" | "S001" | "S002" | "S003" | "S004" | "S005" | "S007" | "S008" | "S009" | "S010" | "S011" | "S012" | "S013" | "S014" | "S015" | "S016" | "S017" | "S018" | "S019" | "S020" | "S021" | "S022" | "S023" | "S024" | "S025" | "S026" | "S027" | "S028" | "S029" | "S030" | "S031" | "U001" | "U002" | "U003" | "UG001" | "UG002" | "UG003" | "UG004" | "UG005" | "UG006" | "UG007" | "UG008" | "UG009" | "UG010" | "V001" | "V002" | "V003" | "V004" | "V005" | "V006" | "V007" | "V008" | "V009" | "V010" | "V011" | "V012" | "V013" | "V014" | "V015" | "V016" | "V017" | "V018" | "V019" | "V020" | "V021" | "V022" | "V023" | "V024" | "V025" | "V026" | "V027" | "V028" | "V029" | "V030" | "V031" | "V032" | "V033" | "V034" | "V035" | "V036" | "V037" | "V038" | "V039" | "V040" | "V041" | "V042" | "V043" | "V044" | "V045" | "V046" | "V047" | "V048" | "V049" | "V050" | "V051" | "V053" | "V054" | "V055" | "V056" | "V057" | "V058" | "V059" | "V060" | "V061" | "V062" | "V063" | "V064" | "V065" | "V066" | "V067" | "V068" | "V069" | "V070" | "V071" | "V072" | "V073" | "V074" | "V075" | "V078" | "V079" | "V080" | "V081" | "V082" | "V083" | "V084" | "V085" | "V086" | "V087" | "V088" | "V089" | "V090" | "V091" | "V092" | "V093" | "V094" | "V095" | "V096" | "V097" | "V098" | "V099" | "V100" | "V101" | "V102" | "V103" | "W001" | "W002" | "W003" | "W004" | "W005" | "W006" | "W007" | "W008" | "W009"

#ErrorResult: {
	errorId:     #ErrorCode
	description: string
	errors?: null | {
		[string]: [...string]
	}
	field?: null | string
}

#ExpirationConfigurationDto: {
	expirationDate?:                  null | string
	expirationInSecondsAfterSending?: null | int64 & int
}

#ExpirationMode: "Relative" | "Absolute"

#ExternalSignatureImageMode: "Optional" | "Required" | "Disabled"

#FailedEnvelope: {
	id:      string
	errorId: #ErrorCode
}

#FieldTask: {
	field: matchN(1, [#SignatureField, #TextInputField, #CheckboxField, #DropDownField, #ListBoxField, #AttachmentField, #AnnotationField, #LinkField, #FileReadConfirmationField, #PageReadConfirmationField, #AreaReadConfirmationField, #RadioButtonField, #ApprovalField, #InvisibleSignatureField])
	sortOrder:   int32 & int
	recipientId?: null | string
	stageId?:     null | string
}

#FieldTaskItemRequest: {
	field: matchN(1, [#SignatureFieldDto, #TextFieldDto, #CheckboxFieldDto, #DropDownFieldDto, #ListBoxFieldDto, #AttachmentFieldDto, #AnnotationFieldDto, #LinkFieldDto, #FileReadConfirmationFieldDto, #PageReadConfirmationFieldDto, #AreaReadConfirmationFieldDto, #RadioButtonFieldDto, #ApprovalFieldDto, #InvisibleSignatureFieldDto])
	sortOrder:   int32 & int
	recipientId?: null | string
}

#FieldTaskSignatureType: "ClickToSignSignature" | "DrawToSignSignature" | "TypeToSignSignature" | "LocalCertificateSignature" | "DisposableCertificateSignature" | "BiometricSignature" | "RemoteCertificateSignature" | "OneTimePasswordSignature" | "PluginSignature" | "AutomaticSignature"

#FieldType: "Signature" | "TextInput" | "Checkbox" | "DropDown" | "ListBox" | "Attachment" | "Annotation" | "Link" | "FileReadConfirmation" | "PageReadConfirmation" | "AreaReadConfirmation" | "RadioButton" | "Approval" | "InvisibleSignature"

#FieldValidationType: "None" | "Date" | "Email" | "Number" | "Phone" | "Time"

#FileDetailResponse: {
	documentClass?: null | #EnvelopeFileDetailDocumentClassDto
	restrictedVisibilityRecipientIds?: null | [...string]
}

#FileElementDateValidationConfiguration: {
	range:      #FileElementFieldValidationRange
	dateFormat?: null | string
}

#FileElementFieldValidationRange: {
	from?: null | string
	to?:   null | string
}

#FileElementNumberValidationConfiguration: {
	symbolPosition:     #SymbolLocationType
	range:              #FileElementFieldValidationRange
	symbol?:             null | string
	thousandsSeparator: #ThousandsSeparatorType
	decimalSeparator:   #DecimalSeparatorType
	decimals?:           null | int32 & int
}

#FileElementPhoneValidationConfiguration: {
	type: #PhoneType
}

#FileElementTextFormat: {
	textColor:    string
	fontSizeInPt: number
	fontName:     string
	bold:         bool
	italic:       bool
	textAlign:    #TextAlignment
}

#FileElementTimeValidationConfiguration: {
	range:      #FileElementFieldValidationRange
	timeFormat?: null | string
}

#FileElementsDto: {
	textBoxElements: [...#TextBoxElementDto]
	checkBoxElements: [...#CheckBoxElementDto]
	signatureElements: [...#SignatureElementDto]
	dropDownElements: [...#DropDownElementDto]
	listElements: [...#ListElementDto]
	documentReadConfirmations: [...#DocumentReadConfirmationDto]
	pageReadConfirmations: [...#PageReadConfirmationDto]
	areaReadConfirmations: [...#AreaReadConfirmationDto]
	linkElements: [...#LinkElementDto]
	attachmentElements: [...#AttachmentElementDto]
	annotationElements: [...#AnnotationElementDto]
	radioButtonElements: [...#RadioButtonElementDto]
	approveElements: [...#ApproveElementDto]
	invisibleSignatureElements: [...#InvisibleSignatureElementDto]
}

#FileElementsFieldValidation: {
	type:                          #FieldValidationType
	dateValidationConfiguration?:   null | #FileElementDateValidationConfiguration
	numberValidationConfiguration?: null | #FileElementNumberValidationConfiguration
	phoneValidationConfiguration?:  null | #FileElementPhoneValidationConfiguration
	timeValidationConfiguration?:   null | #FileElementTimeValidationConfiguration
}

#FileElementsPosition: {
	pageNumber: int32 & int
	x:          number
	y:          number
}

#FileElementsSize: {
	width:  number
	height: number
}

#FileOrderItem: {
	id:        string
	sortOrder: int32 & int
}

#FileReadConfirmationField: #BaseField & {
	...
} & {
	displayName?: null | string
	fieldType:   "FileReadConfirmation"
}

#FileReadConfirmationFieldDto: #BaseFieldDto & {
	...
} & {
	required:    bool
	displayName?: null | string
	fieldType:   "FileReadConfirmation"
}

#FileReadConfirmationTaskUpdateRequest: {
	fieldType: "FileReadConfirmation"
}

#FileTaskItem: {
	field: matchN(1, [#SignatureField, #TextInputField, #CheckboxField, #DropDownField, #ListBoxField, #AttachmentField, #AnnotationField, #LinkField, #FileReadConfirmationField, #PageReadConfirmationField, #AreaReadConfirmationField, #RadioButtonField, #ApprovalField, #InvisibleSignatureField])
	sortOrder:   int32 & int
	recipientId?: null | string
}

#FirstNameAnnotationConfigDto: {
	annotationType: "FirstName"
}

#FirstNameDefinition: {
	valueFormat: "FirstName"
}

#FontStyle: {
	textColor:    string
	fontSizeInPt: number
	fontName:     string
	bold:         bool
	italic:       bool
	textAlign:    int32 & int
}

#ForceAuthenticationModeApi: "None" | "Any" | "Pin" | "Sms" | "OAuth"

#ForcedAuthenticationRulesRequest: {
	authenticationMode:                          #ForceAuthenticationModeApi
	forceInputSmsAuthentication:                 bool
	allowBiometricWithoutAuthentication:         bool
	allowComplexSignaturesWithoutAuthentication: bool
	authenticationProviderId?:                    null | string
}

#ForcedAuthenticationRulesResponse: {
	authenticationMode:                          #ForceAuthenticationModeApi
	authenticationProviderId?:                    null | string
	forceInputSmsAuthentication:                 bool
	allowBiometricWithoutAuthentication:         bool
	allowComplexSignaturesWithoutAuthentication: bool
}

#FormFieldSource: "Document" | "AdvancedDocumentTag" | "UserDefined"

#FullNameAnnotationConfigDto: {
	annotationType: "FullName"
}

#FullNameDefinition: {
	valueFormat: "FullName"
}

#GeneralSettingsDto: {
	name:                      string
	contactUrl?:                null | string
	supportUrl?:                null | string
	allowSendCC:               bool
	preventEmailFromBeingSent: bool
	customStampImprintEnabled: bool
}

#GenericSigningPluginDto: {
	pluginId:              string
	name:                  string
	allowUserSigning:      bool
	allowBatchUserSigning: bool
	allowAutomaticSigning: bool
	signatureFriendlyNames?: null | [...#GenericSigningPluginSettingLabelDto]
	category: #SignatureCategory
}

#GenericSigningPluginSenderSettingsDto: {
	pluginId:              string
	name:                  string
	allowUserSigning:      bool
	allowBatchUserSigning: bool
	allowAutomaticSigning: bool
	signatureFriendlyNames?: null | [...#GenericSigningPluginSettingLabelDto]
	category:              #SignatureCategory
	pluginFriendlyName:    string
	signatureFriendlyName?: null | string
	senderDataFields?: null | [...#SenderDataFieldSettingDto]
	predefinedSenderDataFields?: null | [...#PredefinedSenderDataField]
	profiles?: null | [...#SenderAutomaticProfileDto]
}

#GenericSigningPluginSettingLabelDto: {
	languageCode: string
	text:         string
}

#GenericSigningPluginsSenderDataDto: {
	senderGenericSigningPlugins?: null | [...#SenderGenericSigningPluginDto]
}

#GetOrganizationsListResponse: {
	organizations: [...#OrganizationSummaryDto]
	pagination: #PaginationDto
}

#GetUsersListResponse: {
	users: [...#OrganizationUserSummaryDto]
	pagination: #PaginationDto
}

#GetUsersResponse: {
	users: [...#OrganizationUserDto]
	pagination: #PaginationDto
}

#GuidingOrderMode: "AnyOrder" | "EnforceOrder"

#HttpValidationProblemDetails: {
	type?:     null | string
	title?:    null | string
	status?:   null | int32 & int
	detail?:   null | string
	instance?: null | string
	errors: [string]: [...string]
	{[!~"^(type|title|status|detail|instance|errors)$"]: _}
}

#ImagePosition: "Background" | "Above" | "Below" | "Left" | "Right"

#InitialsAnnotationConfigDto: {
	annotationType: "Initials"
}

#InitialsDefinition: {
	useMiddleNameInInitials: bool
	valueFormat:             "Initials"
}

#IntegrationBulkEnvelopeDto: {
	id:                      string
	name?:                    null | string
	createdAt:               string
	updatedAt:               string
	expirationConfiguration: #ExpirationConfigurationDto
	expirationMode:          #ExpirationMode
	reminderConfiguration:   #ReminderConfigurationDto
	qualifiedTimeStamp:      bool
	defaultSubject?:          null | string
	defaultBody?:             null | string
	signatureReason?:         null | string
	signatureFormat:         #SignatureFormat
	stages: [...#IntegrationStageDto]
	files: [...#IntegrationFileDto]
	agreements?: null | [...#Agreement]
	status:                   string
	statusChangeReason?:       null | string
	sentAt?:                   null | string
	fileRestrictedVisibility: bool
}

#IntegrationEnvelopeDto: {
	id:                      string
	name?:                    null | string
	createdAt:               string
	updatedAt:               string
	expirationConfiguration: #ExpirationConfigurationDto
	expirationMode:          #ExpirationMode
	reminderConfiguration:   #ReminderConfigurationDto
	qualifiedTimeStamp:      bool
	defaultSubject?:          null | string
	defaultBody?:             null | string
	signatureReason?:         null | string
	signatureFormat:         #SignatureFormat
	stages: [...#IntegrationStageDto]
	files: [...#IntegrationFileDto]
	agreements?: null | [...#Agreement]
	status:                   string
	statusChangeReason?:       null | string
	sentAt?:                   null | string
	fileRestrictedVisibility: bool
}

#IntegrationExpirationConfigurationDto: _

#IntegrationFileDto: {
	id:        string
	name:      string
	sortOrder: int32 & int
}

#IntegrationStageDto: {
	id:        string
	name?:      null | string
	sortOrder: int32 & int
}

#IntegrationTemplateDto: {
	id:                      string
	name?:                    null | string
	createdAt:               string
	updatedAt:               string
	expirationConfiguration: #ExpirationConfigurationDto
	expirationMode:          #ExpirationMode
	reminderConfiguration:   #ReminderConfigurationDto
	qualifiedTimeStamp:      bool
	defaultSubject?:          null | string
	defaultBody?:             null | string
	signatureReason?:         null | string
	signatureFormat:         #SignatureFormat
	stages: [...#IntegrationStageDto]
	files: [...#IntegrationFileDto]
	agreements?: null | [...#Agreement]
}

#InvisibleSignatureElementDto: {
	elementId:   string
	source:      #FormFieldSource
	recipientId?: null | string
	required:    bool
	allowedSignatureTypes?: null | [...matchN(1, [#LocalCertificateSignature, #RemoteCertificateSignature, #DisposableCertificateSignature, #PluginSignature])]
	qualifiedTimeStamp?: null | bool
	guidingOrder:       int32 & int
}

#InvisibleSignatureField: #BaseField & {
	...
} & {
	allowedSignatureTypes?: null | [...matchN(1, [#LocalCertificateSignature, #RemoteCertificateSignature, #DisposableCertificateSignature, #PluginSignature])]
	qualifiedTimeStamp?: null | bool
	fieldType:          "InvisibleSignature"
}

#InvisibleSignatureFieldDto: #BaseFieldDto & {
	...
} & {
	allowedSignatureTypes?: null | [...matchN(1, [#LocalCertificateSignature, #RemoteCertificateSignature, #DisposableCertificateSignature, #PluginSignature])]
	qualifiedTimeStamp?: null | bool
	fieldType:          "InvisibleSignature"
}

#LanguageListItemDto: {
	code: string
	name: string
}

#LanguageSettingDto: {
	id:       string
	code:     string
	name:     string
	isActive: bool
}

#LanguageStateRequest: {
	code:     string
	isActive: bool
}

#LanguagesDto: {
	options: [...#LanguageListItemDto]
}

#LanguagesLookupResponse: {
	languages: [...#UiLanguageDto]
}

#LanguagesSettingsResponse: {
	languages: [...#LanguageSettingDto]
}

#LanguagesSettingsUpdateRequest: {
	languages: [...#LanguageStateRequest]
}

#LastNameAnnotationConfigDto: {
	annotationType: "LastName"
}

#LastNameDefinition: {
	valueFormat: "LastName"
}

#LastRecipientAction: "SignNotificationSent" | "OpenedWorkstep" | "Signed" | "Rejected" | "Delegated" | "Viewed" | "InUse" | "ReceivedCopy" | "BouncedNotification" | "FailedNotificationDelivery"

#LicenseDto: {
	type:           #LicenseType
	expirationDate: string
	userLimit:      int32 & int
	documentLimit:  int32 & int
}

#LicenseType: "Trial" | "LicensedPerUser" | "LicensedPerDocumentsBasic" | "LicensedPerDocumentsProfessional" | "LicensedPerDocumentsBusiness" | "LicensedPerDocumentsEnterprise"

#LinkElementDefinition: {
	position: #FileElementsPosition
	size:     #FileElementsSize
}

#LinkElementDto: {
	elementDefinition: #LinkElementDefinition
	source:            #FormFieldSource
	elementId:         string
	recipientId?:       null | string
	value:             string
	guidingOrder:      int32 & int
}

#LinkField: #BaseField & {
	...
} & {
	url:       string
	fieldType: "Link"
}

#LinkFieldDto: #BaseFieldDto & {
	...
} & {
	page:      int32 & int
	positionX: number
	positionY: number
	width:     number
	height:    number
	reference: string
	fieldType: "Link"
}

#ListBoxField: #BaseField & {
	...
} & {
	readOnly: bool
	font?:     null | #FontStyle
	options?: null | [...#Option]
	multiselect: bool
	fieldType:   "ListBox"
}

#ListBoxFieldDto: #BaseFieldDto & {
	...
} & {
	page:      int32 & int
	positionX: number
	positionY: number
	width:     number
	height:    number
	font:      #FontStyle
	options?: null | [...#OptionDto]
	multiselect: bool
	required:    bool
	readOnly:    bool
	fieldType:   "ListBox"
}

#ListBoxTaskUpdateRequest: {
	selectedItemIds: [...string]
	fieldType: "ListBox"
}

#ListElementDefinition: {
	position:   #FileElementsPosition
	size:       #FileElementsSize
	textFormat: #FileElementTextFormat
	readOnly:   bool
}

#ListElementDto: {
	elementDefinition: #ListElementDefinition
	elementId:         string
	items: [...#ListItemEntry]
	isRequired:    bool
	isEditable:    bool
	isMultiselect: bool
	isChecked:     bool
	source:        #FormFieldSource
	recipientId?:   null | string
	guidingOrder:  int32 & int
}

#ListItemEntry: {
	key:        string
	value:      string
	isSelected: bool
}

#LocalCertificateHashAlgorithm: "Sha256" | "Sha512"

#LocalCertificateSignature: {
	layoutId?:      null | string
	signatureType: "LocalCertificateSignature"
}

#LocalCertificateSignatureTypeDto: {
	useExternalSignatureImage?:     null | #ExternalSignatureImageMode
	preferred?:                     null | bool
	layoutId?:                      null | string
	stampImprintConfiguration?:     null | #LocalCertificateStampImprintDto
	enforcePreferredHashAlgorithm?: null | bool
	preferredHashAlgorithm?:        null | #LocalCertificateHashAlgorithm
}

#LocalCertificateStampImprintDto: {
	displayName:             bool
	displaySignatureDate:    bool
	displayExtraInformation: bool
	fontName?:                null | string
	fontSizeInPt?:            null | int32 & int
	displayIp:               bool
}

#MetadataDataType: "String" | "Number" | "Date"

#MetadataValueDto: {
	fieldDefinitionId: string
	value?:             null | string
}

#NamedSignatureAppearanceLayoutDto: {
	id:                string
	displayFirstname:  bool
	displayLastname:   bool
	displayCustomText: bool
	displayDateTime:   bool
	displayEmail:      bool
	displayReason:     bool
	backgroundImage?:   null | #BackgroundImageDto
	position:          #ImagePosition
}

#NamedSignatureAppearanceLayoutRequest: {
	id:                string
	displayFirstname:  bool
	displayLastname:   bool
	displayCustomText: bool
	displayDateTime:   bool
	displayEmail:      bool
	displayReason:     bool
	backgroundImage?:   null | #BackgroundImageDto
	position:          #ImagePosition
}

#NextRecipientDto: {
	id:        string
	givenName: string
	surname:   string
	type:      #NextRecipientType
}

#NextRecipientLinkDto: {
	recipient: #NextRecipientDto
	link:      string
}

#NextRecipientLinksResponse: {
	nextRecipientLinks: [...#NextRecipientLinkDto]
}

#NextRecipientType: "Signer" | "Automatic" | "Approver" | "Viewer"

#NotificationChannel: "Email" | "Sms" | "WhatsApp" | "DoNotSendNotification"

#NotificationChannelMessagesDto: {
	messages: [...#NotificationMessageDto]
}

#NotificationChannelsDto: {
	email:    bool
	sms:      bool
	whatsApp: bool
}

#NotificationMessageDto: {
	subject?: null | string
	body?:    null | string
}

#NotificationPreferencesRequest: {
	notifyRecipientOnActionNeeded: bool
}

#NotificationPreferencesResponse: {
	notifyRecipientOnActionNeeded: bool
}

#NotificationSettingsDto: {
	emailSenderDisplayType:                  #EmailSenderDisplayType
	envelopeLimitReachedNotificationEnabled: bool
	envelopesInPercentFromLimitNotification: int32 & int
	envelopesLimitReachedPercentStep:        int32 & int
	organizationCallbackEnabled:             bool
	licenseExpireNotificationEnabled:        bool
	licenseExpireNotificationBeforeDays:     int32 & int
	licenseExpireNotificationRecurrentDays:  int32 & int
	organizationCallbackUrl:                 string
	reminderSendLimitInMinutes:              int32 & int
}

#NumberInputConfig: #TextInputConfig & {
	...
} & {
	value?:              null | number
	symbol?:             null | #NumberSymbol
	thousandsSeparator: #ThousandsSeparatorType
	decimalSeparator:   #DecimalSeparatorType
	decimalPlaces?:      null | int32 & int
	minValue?:           null | number
	maxValue?:           null | number
	textInputType:      "NumberType"
}

#NumberSymbol: {
	value?:    null | string
	position: #SymbolLocationType
}

#OAuthAuthentication: {
	providerName: string
	externalId:   string
}

#OAuthFieldDefinitionDto: {
	id:                           int64 & int
	path:                         string
	mode:                         #OAuthSignerProviderFieldMode
	target:                       #OAuthSignerProviderFieldTarget
	oAuthResourceUriId?:           null | int64 & int
	oAuthJwtConfigId?:             null | int64 & int
	oAuthProviderId?:              null | int64 & int
	customFieldName?:              null | string
	genericSigningPluginId?:       null | string
	genericSigningPluginFieldKey?: null | string
}

#OAuthFieldReferenceDto: {
	id?:                            null | string
	fieldTarget:                   #OAuthFieldTarget
	customFieldName?:               null | string
	genericSigningPluginReference?: null | #OAuthGenericSigningPluginReferenceDto
}

#OAuthFieldTarget: "Custom" | "Recipient_GivenName" | "Recipient_Surname" | "Recipient_Email" | "Recipient_PhoneNumber" | "DisposableHolder_IdentificationType" | "DisposableHolder_IdentificationCountry" | "DisposableHolder_CountryResidence" | "DisposableHolder_PhoneMobile" | "DisposableHolder_RecognitionType" | "DisposableHolder_DocumentIssuedBy" | "DisposableHolder_DocumentIssuedOn" | "DisposableHolder_DocumentExpiryDate" | "DisposableHolder_TaxCode" | "DisposableHolder_DocumentNumber" | "GenericSigningPlugin_CustomSenderField"

#OAuthGenericSigningPluginReferenceDto: {
	pluginId?: null | string
	key?:      null | string
}

#OAuthJwtConfigDto: {
	oAuthProviderId:  int64 & int
	jwksUri:          string
	issuer:           string
	enforceNonce:     bool
	validateAudience: bool
	validateIssuer:   bool
	validateLifetime: bool
	oAuthFieldDefinitions?: null | [...#OAuthFieldDefinitionDto]
}

#OAuthResourceUriDto: {
	id:                    int64 & int
	uri:                   string
	accessTokenParamName:  string
	eIdServiceCombination?: null | string
	oAuthFieldDefinitions?: null | [...#OAuthFieldDefinitionDto]
}

#OAuthSignerProvider: {
	id:                 int64 & int
	externalId:         string
	name:               string
	clientId:           string
	clientSecret?:       null | string
	scope?:              null | string
	authorizationUri:   string
	tokenUri:           string
	logoutUri?:          null | string
	authenticationType: int32 & int
	isActive?:           null | bool
	redirectUrl?:        null | string
}

#OAuthSignerProviderDetailsResponse: {
	oAuthSignerProvider: #OAuthSignerProviderDto
	oAuthJwtConfig?:      null | #OAuthJwtConfigDto
	oAuthResourceUris?: null | [...#OAuthResourceUriDto]
}

#OAuthSignerProviderDto: {
	id:                 int64 & int
	externalId:         string
	name:               string
	clientId:           string
	clientSecret?:       null | string
	scope?:              null | string
	authorizationUri:   string
	tokenUri:           string
	logoutUri?:          null | string
	authenticationType: int32 & int
	isActive?:           null | bool
	redirectUrl?:        null | string
}

#OAuthSignerProviderFieldMode: "ValidateEqualCaseSensitive" | "Update" | "ValidateEqualCaseInsensitive"

#OAuthSignerProviderFieldModeResponse: {
	name:  string
	value: int32 & int
}

#OAuthSignerProviderFieldTarget: "Custom" | "Recipient_FirstName" | "Recipient_LastName" | "Recipient_Email" | "Recipient_PhoneNumber" | "DisposableHolder_IdentificationType" | "DisposableHolder_IdentificationCountry" | "DisposableHolder_CountryResidence" | "DisposableHolder_PhoneMobile" | "DisposableHolder_RecognitionType" | "DisposableHolder_DocumentIssuedBy" | "DisposableHolder_DocumentIssuedOn" | "DisposableHolder_DocumentExpiryDate" | "DisposableHolder_TaxCode" | "DisposableHolder_DocumentNumber" | "GenericSigningPlugin_CustomField"

#OAuthSignerProviderFieldTargetResponse: {
	name:  string
	value: int32 & int
}

#OAuthSignerProvidersResponse: {
	oAuthSignerProviders: [...#OAuthSignerProvider]
	pagination: #Pagination
}

#OAuthSignerProvidersSortingKey: "Name"

#OneTimePasswordSignature: {
	layoutId?:      null | string
	signatureType: "OneTimePasswordSignature"
}

#OneTimePasswordSignatureTypeDto: {
	useExternalSignatureImage?: null | #ExternalSignatureImageMode
	preferred?:                 null | bool
	layoutId?:                  null | string
	stampImprintConfiguration?: null | #OneTimePasswordStampImprintDto
	validityInSeconds?:         null | int32 & int
}

#OneTimePasswordStampImprintDto: {
	displayName:             bool
	displaySignatureDate:    bool
	displayExtraInformation: bool
	fontName?:                null | string
	fontSizeInPt?:            null | int32 & int
	displayTransactionId:    bool
	displayTransactionToken: bool
	displayPhoneNumber:      bool
	displayIp:               bool
	displayEmail:            bool
}

#Option: {
	value:      string
	label:      string
	isSelected: bool
}

#OptionDto: {
	key:        string
	value:      string
	isSelected: bool
}

#OrganizationCustomTimeStampServerSettings: {
	url?:           null | string
	username?:      null | string
	password?:      null | string
	hashAlgorithm?: null | string
}

#OrganizationDefaultSignatureTypeDto: {
	signatureType: #SignatureType
}

#OrganizationDelegationSettingsDto: {
	delegationPolicy: #DelegationPolicy
}

#OrganizationDetailDto: {
	id:                    string
	name:                  string
	creationDateUtc:       string
	canceled:              bool
	licenseType:           #LicenseType
	licenseExpirationDate: string
	userLimit:             int32 & int
}

#OrganizationFeatureFlagResponse: {
	id:      int32 & int
	enabled: bool
	name:    string
}

#OrganizationFeatureFlagsResponse: {
	featureFlags: [...#OrganizationFeatureFlagResponse]
}

#OrganizationGeneralPoliciesDto: {
	allowSaveDocument:        bool
	allowSaveAuditTrail:      bool
	allowPrintDocument:       bool
	allowAdhocPdfAttachments: bool
	allowRejectWorkstep:      bool
	allowUndoLastAction:      bool
}

#OrganizationItemDto: {
	id:     string
	name:   string
	userId: string
}

#OrganizationLanguageLookupDto: {
	code: string
	name: string
}

#OrganizationPAdESConfiguration: {
	simpleSignatures?:   null | #PAdESSignatureConfig
	enhancedSignatures?: null | #PAdESSignatureConfig
	complexSignatures?:  null | #PAdESSignatureConfig
	auditTrail?:         null | #PAdESSignatureConfig
}

#OrganizationRecipientAuthenticationTypesDto: {
	allowedAuthenticationTypes?: null | [...#RecipientAuthenticationTypes]
	oAuthProviders?: null | [...#OrganizationRecipientOAuthProviderDto]
}

#OrganizationRecipientOAuthProviderDto: {
	identifier:                 string
	name:                       string
	hasEIdAssertion:            bool
	hasLateIdentSigTypes:       bool
	providesIdentification:     bool
	updateFieldComparisonValue: int64 & int
	updateFields?: null | [...#OAuthFieldReferenceDto]
	validateFields?: null | [...#OAuthFieldReferenceDto]
}

#OrganizationRecipientSettingsDto: {
	sendFinishedDocumentsToAllRecipients: bool
	showNotEnoughSignaturesWarning:       bool
	delegationAvailable:                  bool
}

#OrganizationSettingsPermissions: {
	read:   bool
	update: bool
}

#OrganizationSignatureTypesDto: {
	allowedSignatureTypes: [...string]
	allowedDefaultSignatureTypes: [...string]
	allowedGenericSigningPlugins: [...#GenericSigningPluginDto]
}

#OrganizationSummaryDto: {
	id:       string
	name:     string
	canceled: bool
}

#OrganizationUserDto: {
	id:               string
	givenName:        string
	surname:          string
	email:            string
	regionalSettings: #OrganizationUserRegionalSettingsDto
	phoneNumber?:      null | string
	enabled:          bool
}

#OrganizationUserRegionalSettingsDto: {
	timeZone:       string
	language:       string
	country:        string
	dateTimeFormat: #DateTimeFormatSwaggerEnumProvider
}

#OrganizationUserSummaryDto: {
	id:               string
	givenName:        string
	surname:          string
	email:            string
	regionalSettings: #OrganizationUserRegionalSettingsDto
	phoneNumber?:      null | string
	enabled:          bool
}

#OtpDeliveryChannel: "Sms" | "Email"

#OtpSignatureDataDto: {
	type?:        null | #OtpDeliveryChannel
	phoneNumber?: null | string
}

#PAdESLevel: "B" | "T" | "LT" | "LTA"

#PAdESSignatureConfig: {
	enabled: bool
	level:   #PAdESLevel
}

#PageReadConfirmationDto: {
	elementId:    string
	pageNumber:   int32 & int
	required:     bool
	recipientId?:  null | string
	guidingOrder: int32 & int
	displayName?:  null | string
}

#PageReadConfirmationField: #BaseField & {
	...
} & {
	displayName?: null | string
	fieldType:   "PageReadConfirmation"
}

#PageReadConfirmationFieldDto: #BaseFieldDto & {
	...
} & {
	page:        int32 & int
	required:    bool
	displayName?: null | string
	fieldType:   "PageReadConfirmation"
}

#PageReadConfirmationTaskUpdateRequest: {
	fieldType: "PageReadConfirmation"
}

#PaginatedRoles: {
	roles: [...#RoleDto]
	pagination: #PaginationDto
}

#Pagination: {
	page:       int32 & int
	pageSize:   int32 & int
	totalCount: int32 & int
}

#PaginationDto: {
	page:       int32 & int
	pageSize:   int32 & int
	totalCount: int32 & int
}

#PaginationResponse: {
	page:       int32 & int
	pageSize:   int32 & int
	totalCount: int32 & int
}

#ParseBulkRecipientsResponse: {
	bulkRecipients: [...#BulkRecipientDefinition]
}

#PdfDocumentSettingsDto: {
	pAdESConfiguration?:               null | #OrganizationPAdESConfiguration
	allowSigningOfLockedPdfDocuments: bool
	customTimeStampSettings?:          null | #OrganizationCustomTimeStampServerSettings
}

#PermissionDto: {
	entity: #Entity
	action: #Action
}

#PermissionsDto: {
	envelopes:            #EnvelopePermissions
	templates:            #TemplatePermissions
	userGroups:           #UserGroupsPermissions
	organizationSettings: #OrganizationSettingsPermissions
	users:                #UsersSettings
	roles:                #RolesSettings
	automaticESealing:    #AutomaticESealingPermissions
}

#PersonalAccessTokenListItemResponse: {
	id:        string
	name:      string
	createdAt: string
	expiresAt: string
}

#PersonalAccessTokenListResponse: {
	personalAccessTokens: [...#PersonalAccessTokenListItemResponse]
}

#PhoneNumberInputConfig: #TextInputConfig & {
	...
} & {
	value:         string
	format:        #PhoneType
	textInputType: "PhoneNumberType"
}

#PhoneType: "International" | "InternationalLeadingZeros" | "InternationalLeadingPlus"

#PluginSignature: {
	pluginId:      string
	layoutId?:      null | string
	signatureType: "PluginSignature"
}

#PluginStampImprintDto: {
	displayName:             bool
	displaySignatureDate:    bool
	displayExtraInformation: bool
	fontName?:                null | string
	fontSizeInPt?:            null | int32 & int
	displayEmail:            bool
	displayIp:               bool
}

#PoliciesResponse: {
	policies: [...#PolicyListItemResponse]
	pagination: #PaginationResponse
}

#PoliciesSortingKey: "Name" | "Description" | "IsActive"

#PolicyActionDto: {
	id?:              null | string
	sortOrder:       int32 & int
	type:            #PolicyActionType
	stage:           #StageConfigurationDto
	recipientSource: #PolicyRecipientSourceDto
}

#PolicyActionType: "AddEnvelopeStageConfiguration"

#PolicyConditionDto: {
	id:         string
	metadataId: string
	operator:   #PolicyConditionOperator
	value:      string
	sortOrder:  int32 & int
}

#PolicyConditionOperator: "GreaterThan" | "LessThan" | "Equals"

#PolicyConditionRequest: {
	id:         string
	metadataId: string
	operator:   #PolicyConditionOperator
	value:      string
	sortOrder:  int32 & int
}

#PolicyDto: {
	id:              string
	name:            string
	isActive:        bool
	sortOrder:       int32 & int
	description?:     null | string
	documentClassId?: null | string
	conditions?: null | [...#PolicyConditionDto]
	actions?: null | [...#PolicyActionDto]
}

#PolicyListItemResponse: {
	id:          string
	name:        string
	isActive:    bool
	description?: null | string
}

#PolicyRecipientDto: {
	givenName:   string
	surname:     string
	email:       string
	phoneNumber?: null | string
}

#PolicyRecipientSourceDto: {
	type: #PolicyRecipientSourceType
	recipients?: null | [...#PolicyRecipientDto]
	userGroupId?:    null | string
	businessRoleId?: null | string
}

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

#PutFileDetailRequest: {
	documentClass?: null | #EnvelopeFileDetailDocumentClassRequest
	restrictedVisibilityRecipientIds?: null | [...string]
}

#RadioButtonElementDefinition: {
	position: #FileElementsPosition
	size:     #FileElementsSize
	readOnly: bool
}

#RadioButtonElementDto: {
	elementId:         string
	elementDefinition: #RadioButtonElementDefinition
	source:            #FormFieldSource
	groupName:         string
	isChecked:         bool
	isSelectInUnison:  bool
	recipientId?:       null | string
	required:          bool
	value:             string
	guidingOrder:      int32 & int
}

#RadioButtonField: #BaseField & {
	...
} & {
	groupName:        string
	isSelectInUnison: bool
	readOnly:         bool
	checked:          bool
	value:            string
	fieldType:        "RadioButton"
}

#RadioButtonFieldDto: #BaseFieldDto & {
	...
} & {
	page:      int32 & int
	positionX: number
	positionY: number
	width:     number
	height:    number
	groupName: string
	readOnly:  bool
	checked:   bool
	value:     string
	required:  bool
	fieldType: "RadioButton"
}

#RadioButtonTaskUpdateRequest: {
	selectedFieldId: string
	fieldType:       "RadioButton"
}

#RecipientAuthenticationDto: {
	accessCode?:         null | #AccessCode
	smsOneTimePassword?: null | #SmsOneTimePassword
	oAuthAuthentications?: null | [...#OAuthAuthentication]
}

#RecipientAuthenticationSettingItemResponse: {
	name:      string
	isEnabled: bool
}

#RecipientAuthenticationSettingsResponse: {
	settings: [...#RecipientAuthenticationSettingItemResponse]
}

#RecipientAuthenticationTypes: "Pin" | "SmsOtp" | "BankId" | "OAuth" | "Saml"

#RecipientDiscriminator: "Standard" | "Automatic"

#RecipientDto: {
	id:                          string
	givenName?:                   null | string
	surname?:                     null | string
	email?:                       null | string
	phoneNumber?:                 null | string
	placeholder?:                 null | string
	type?:                        null | #DbRecipientType
	isP7mSigner:                 bool
	notificationChannel?:         null | #NotificationChannel
	order:                       int32 & int
	languageCode?:                null | string
	authenticationConfiguration?: null | #RecipientAuthenticationDto
	signatureDataConfiguration?:  null | #RecipientSignatureDataDto
	stageId?:                     null | string
	personalMessage?:             null | string
	guidingOrderMode?:            null | #GuidingOrderMode
	isDelegationEnabled:         bool
	generalPoliciesOverrides?:    null | #RecipientGeneralPoliciesOverridesDto
	signatureReason?:             null | string
	signatureReasonAllowChange?:  null | bool
	signatureProfile?:            null | string
	metadata?: null | [...#RecipientMetadataEntry]
	workstepResult?: null | #DbWorkstepResult
}

#RecipientGeneralPoliciesOverridesDto: {
	allowSaveDocument:        bool
	allowSaveAuditTrail:      bool
	allowPrintDocument:       bool
	allowAdhocPdfAttachments: bool
	allowRejectWorkstep:      bool
	allowUndoLastAction:      bool
}

#RecipientMetadataEntry: {
	name:  string
	value: string
}

#RecipientSignatureDataDto: {
	disposableCertificate?:           null | #DisposableCertificateDto
	remoteCertificate?:               null | #RemoteCertificateDto
	aTrustCertificate?:               null | #ATrustCertificateDto
	swissComOnDemand?:                null | #SwissComOnDemandDto
	swedishBankId?:                   null | #SwedishBankIdDto
	otpSignatureData?:                null | #OtpSignatureDataDto
	genericSigningPluginsSenderData?: null | #GenericSigningPluginsSenderDataDto
	automaticSignatureData?:          null | #AutomaticSignatureDataDto
}

#RecipientStatus: "NotSigned" | "Signed" | "Rejected" | "Delegated"

#RecipientType: "Signer" | "CC" | "Acknowledge" | "Pkcs7Signer" | "Automatic" | "Approver"

#RegionalSettingsDto: {
	id:               string
	worldTimeZone:    string
	dateTimeFormatId: int32 & int
	uiLanguage:       string
	countryId:        int32 & int
}

#RelativeIntegrationExpirationDto: #IntegrationExpirationConfigurationDto & {
	...
} & {
	afterSendInSeconds?: null | int64 & int
	mode:               "Relative"
}

#ReminderConfigurationDto: {
	enabled:                      bool
	firstReminderInDays:          int32 & int
	reminderResendIntervalInDays: int32 & int
	beforeExpirationInDays:       int32 & int
}

#RemoteCertificateDto: {
	userId?:   null | string
	deviceId?: null | string
}

#RemoteCertificateEnvelopeBulkSignDto: #EnvelopeBulkSignDto & {
	...
} & {
	signatureType:     "RemoteCertificate"
	certificateUserId: string
	devicePassword:    string
	otp:               string
	otpDeviceType?:     null | string
	otpDeviceTypeId?:   null | string
	transactionId?:     null | string
	payloadFileId?:     null | string
}

#RemoteCertificateSignature: {
	layoutId?:      null | string
	signatureType: "RemoteCertificateSignature"
}

#RemoteCertificateSignatureTypeDto: {
	useExternalSignatureImage?: null | #ExternalSignatureImageMode
	preferred?:                 null | bool
	layoutId?:                  null | string
	stampImprintConfiguration?: null | #RemoteCertificateStampImprintDto
	validityInSeconds?:         null | int32 & int
}

#RemoteCertificateStampImprintDto: {
	displayName:             bool
	displaySignatureDate:    bool
	displayExtraInformation: bool
	fontName?:                null | string
	fontSizeInPt?:            null | int32 & int
	displayIp:               bool
}

#ReplacedEnvelopeFileResponse: {
	id:         string
	orderIndex: int32 & int
}

#ReplacedTemplateFileResponse: {
	id:         string
	orderIndex: int32 & int
}

#RequestBulkSignDevicesDto: {
	userId: string
	envelopeIds: [...string]
}

#ResumeBatchRequest: {
	data: #EnvelopeResumeDto
	envelopeIds: [...string]
}

#RoleDetailsDto: {
	id:   string
	name: string
	permissions: [...#PermissionDto]
	createdAt:    string
	description?:  null | string
	isSystemRole: bool
}

#RoleDto: {
	id:           string
	name:         string
	isSystemRole: bool
}

#RolesSettings: {
	read:               bool
	createUpdateDelete: bool
	assign:             bool
}

#RolesSortingKey: "Id" | "Name"

#RotateServiceAccountSecretResponse: {
	clientSecret: string
}

#RowError: {
	row:     int32 & int
	field:   string
	message: string
}

#SealingCertificateResponse: {
	id:                 int64 & int
	externalId:         string
	isActive:           bool
	sealingCertificate: #CertificateDetailsResponse
	certificateChain: [...#CertificateDetailsResponse]
}

#SenderAutomaticProfileDto: {
	profileId:           string
	profileFriendlyName?: null | string
}

#SenderDataFieldSettingDto: {
	required: bool
	translatedLabels: [...#GenericSigningPluginSettingLabelDto]
	defaultValue?: null | string
	key?:          null | string
	type:         #DataFieldType
	items?: null | {
		[string]: string
	}
}

#SenderGenericSigningPluginDto: {
	pluginId?: null | string
	settings?: null | [...#SenderGenericSigningPluginSettingsDto]
}

#SenderGenericSigningPluginSettingsDto: {
	key?:   null | string
	value?: null | string
}

#SentBulkEnvelopeResponse: {
	id: string
}

#SentEnvelopeDto: {
	id: string
}

#ServiceAccountListItemResponse: {
	clientId: string
	email:    string
	userId:   string
}

#ServiceAccountListResponse: {
	items: [...#ServiceAccountListItemResponse]
}

#SettingsDto: {
	maxEnvelopeValidityInDays:    int32 & int
	minEnvelopeValidityInSeconds: int32 & int
	filterExpiringSoonDays:       int32 & int
	notificationSettings:         #NotificationSettingsDto
}

#SharingOptionsResponse: {
	userGroupIds: [...string]
}

#SignDeskOpenResultDto: {
	workstepId?:  null | string
	culture?:     null | string
	redirectUrl?: null | string
}

#SignatureAppearanceLayoutDto: {
	displayFirstname:  bool
	displayLastname:   bool
	displayCustomText: bool
	displayDateTime:   bool
	displayEmail:      bool
	displayReason:     bool
	backgroundImage?:   null | #BackgroundImageDto
	position:          #ImagePosition
}

#SignatureAppearanceLayoutRequest: {
	displayFirstname:  bool
	displayLastname:   bool
	displayDateTime:   bool
	displayEmail:      bool
	displayCustomText: bool
	displayReason:     bool
	backgroundImage?:   null | #BackgroundImageDto
	position:          #ImagePosition
}

#SignatureCategory: "Advanced" | "MixedOrNotSpecified" | "Qualified" | "Simple"

#SignatureElementDefinition: {
	position: #FileElementsPosition
	size:     #FileElementsSize
}

#SignatureElementDto: {
	elementId:                  string
	allowedSignatureTypes:      #AllowedSignatureTypesDto
	elementDefinition:          #SignatureElementDefinition
	source:                     #FormFieldSource
	recipientId?:                null | string
	required:                   bool
	displayName?:                null | string
	elementDescription?:         null | string
	useExternalTimestampServer?: null | bool
	guidingOrder:               int32 & int
	taskConfiguration?:          null | #SignatureTaskConfiguration
	isApprove:                  bool
}

#SignatureField: #BaseField & {
	...
} & {
	allowedSignatureTypes?:      null | #AllowedSignatureTypesDto
	displayName?:                null | string
	elementDescription?:         null | string
	useExternalTimestampServer?: null | bool
	taskConfiguration?:          null | #SignatureTaskConfiguration
	fieldType:                  "Signature"
}

#SignatureFieldDto: #BaseFieldDto & {
	...
} & {
	page:      int32 & int
	positionX: number
	positionY: number
	width:     number
	height:    number
	allowedSignatureTypes: [...matchN(1, [#ClickToSignSignature, #DrawToSignSignature, #TypeToSignSignature, #LocalCertificateSignature, #DisposableCertificateSignature, #BiometricSignature, #RemoteCertificateSignature, #OneTimePasswordSignature, #PluginSignature, #AutomaticSignature])]
	qualifiedTimeStamp?: null | bool
	required:           bool
	fieldType:          "Signature"
}

#SignatureFormat: "Pades" | "Cades"

#SignatureImage: {
	id:            string
	name:          string
	dataUrlPrefix: string
	data:          string
}

#SignatureOptions: "Timestamp" | "AllowUsingCustomTimestampService"

#SignaturePluginSignatureTypeDto: {
	pluginId:                  string
	preferred?:                 null | bool
	layoutId?:                  null | string
	stampImprintConfiguration?: null | #PluginStampImprintDto
}

#SignatureTaskConfiguration: {
	batchGroup?:       null | string
	batchMode?:        null | #BatchMode
	useLocalTimezone?: null | bool
	dateTimeFormat?:   null | string
}

#SignatureTaskUpdateRequest: {
	signature: matchN(1, [#WorkUnitClickToSignSignature, #WorkUnitDrawToSignSignature, #WorkUnitTypeToSignSignature, #WorkUnitLocalCertificateSignature, #WorkUnitDisposableCertificateSignature, #WorkUnitBiometricSignature, #WorkUnitRemoteCertificateSignature, #WorkUnitOneTimePasswordSignature, #WorkUnitPluginSignature, #WorkUnitAutomaticSignature])
	fieldType: "Signature"
}

#SignatureType: "None" | "ClickToSign" | "DrawToSign" | "TypeToSign" | "RemoteCertificate" | "Biometric" | "LocalCertificate" | "DisposableCertificate" | "OneTimePassword" | "SwissComOnDemand" | "PushTan" | "ATrustCertificate" | "SwedishBankId" | "SignaturePlugin" | "AutomaticSignature"

#SignerAgreements: {
	isEnvelopeOverrideEnabled: bool
}

#SingleInsight: {
	envelopeCount: int32 & int
}

#SmsOneTimePassword: {
	phoneNumber?: null | string
}

#StageConfigurationDto: {
	name:                         string
	type:                         #StageType
	requiredRecipientCompletions: int32 & int
}

#StageDto: {
	id:                        string
	mandatoryRecipientsNumber: int32 & int
	name?:                      null | string
}

#StageMode: "Standard" | "Bulk"

#StageSortOrderItem: {
	id:        string
	sortOrder: int32 & int
}

#StageType: "Signer" | "Approver" | "Viewer" | "ReceivesCopy" | "SignAutomatically"

#StampImprintConfigurationDto: {
	defaultLayout: #SignatureAppearanceLayoutDto
	customSignatures: [...#NamedSignatureAppearanceLayoutDto]
}

#StartBulkSignTransactionDto: {
	userId:          string
	deviceId:        string
	otpDeviceType:   string
	otpDeviceTypeId: string
	envelopeIds: [...string]
}

#StatusKey: "Canceled" | "Completed" | "Expired" | "Rejected" | "Active" | "Draft" | "WaitingForYou" | "WaitingForOthers" | "ExpiringSoon" | "InProgress"

#StringInputConfig: #TextInputConfig & {
	...
} & {
	value:         string
	password:      bool
	multiline:     bool
	maxLength:     int32 & int
	textInputType: "StringType"
}

#SubstituteDelegationDto: {
	utilizeAlsoOnCCRecipients: bool
	delegateeFirstName:        string
	delegateeLastName:         string
	delegateeEmail:            string
	reason?:                    null | string
	startDate?:                 null | string
	endDate?:                   null | string
	delegateeUserId?:           null | string
}

#SupportedElectronicIdentitiesResponse: {
	electronicIdentities: [...#SupportedElectronicIdentityResponse]
}

#SupportedElectronicIdentityResponse: {
	type:    string
	country: string
}

#SupportedFileFormatResponse: {
	extension: string
	mimeType:  string
}

#SwedishBankIdDto: {
	personalNumber?:         null | string
	allowAnyPersonalNumber?: null | bool
}

#SwedishBankIdSignatureTypeDto: {
	preferred?:                 null | bool
	layoutId?:                  null | string
	stampImprintConfiguration?: null | #SwedishBankIdStampImprintDto
}

#SwedishBankIdStampImprintDto: {
	displayName:             bool
	displaySignatureDate:    bool
	displayExtraInformation: bool
	fontName?:                null | string
	fontSizeInPt?:            null | int32 & int
	displayTransactionId:    bool
}

#SwissComOnDemandDto: {
	commonName?:   null | string
	country?:      null | string
	phoneNumber?:  null | string
	organization?: null | string
	organizationUnits?: null | [...string]
	locality?:        null | string
	serialNumber?:    null | string
	stateOrProvince?: null | string
	pseudonym?:       null | string
}

#SwissComOnDemandSignatureTypeDto: {
	validityInSeconds?:         null | int32 & int
	preferred?:                 null | bool
	layoutId?:                  null | string
	stampImprintConfiguration?: null | #SwissComOnDemandStampImprintDto
}

#SwissComOnDemandStampImprintDto: {
	displayName:             bool
	displaySignatureDate:    bool
	displayExtraInformation: bool
	fontName?:                null | string
	fontSizeInPt?:            null | int32 & int
	displayIp:               bool
}

#SymbolLocationType: "Start" | "StartWithBlank" | "End" | "EndWithBlank"

#TemplateAction: "Use" | "Edit" | "Delete" | "Share"

#TemplateDto: {
	id:            string
	creatorUserId: string
	name:          string
	actions: [...#TemplateAction]
	createdAt:     string
	updatedAt:     string
	defaultAction?: null | #TemplateAction
}

#TemplateFieldTask: {
	field: matchN(1, [#SignatureField, #TextInputField, #CheckboxField, #DropDownField, #ListBoxField, #AttachmentField, #AnnotationField, #LinkField, #FileReadConfirmationField, #PageReadConfirmationField, #AreaReadConfirmationField, #RadioButtonField, #ApprovalField, #InvisibleSignatureField])
	sortOrder:   int32 & int
	recipientId?: null | string
}

#TemplateFieldTaskItem: {
	field: matchN(1, [#SignatureFieldDto, #TextFieldDto, #CheckboxFieldDto, #DropDownFieldDto, #ListBoxFieldDto, #AttachmentFieldDto, #AnnotationFieldDto, #LinkFieldDto, #FileReadConfirmationFieldDto, #PageReadConfirmationFieldDto, #AreaReadConfirmationFieldDto, #RadioButtonFieldDto, #ApprovalFieldDto, #InvisibleSignatureFieldDto])
	sortOrder:   int32 & int
	recipientId?: null | string
	source:      #ElementSource
}

#TemplateFileTasksResponse: {
	tasks: [...#TemplateFieldTaskItem]
}

#TemplateFilesResponse: {
	files: [...#Document]
}

#TemplateListDto: {
	templates: [...#TemplateDto]
	pagination: #PaginationDto
}

#TemplatePermissions: {
	read:               bool
	createUpdateDelete: bool
}

#TemplateStageAutomaticRecipientResponse: #TemplateStageRecipientResponse & {
	...
} & {
	signatureProfile?:           null | string
	signatureReason?:            null | string
	signatureReasonAllowChange?: null | bool
	metadata?: null | [...#RecipientMetadataEntry]
	type: "Automatic"
}

#TemplateStageAutomaticRecipientSummaryDto: #TemplateStageRecipientSummaryDto & {
	...
} & {
	signatureProfile?: null | string
	type:             "Automatic"
}

#TemplateStageItemDto: {
	id:                           string
	name?:                         null | string
	sortOrder:                    int32 & int
	requiredRecipientCompletions: int32 & int
	type:                         #EnvelopeStageType
	recipients: [...matchN(1, [#TemplateStageStandardRecipientSummaryDto, #TemplateStageAutomaticRecipientSummaryDto])]
}

#TemplateStageListDto: {
	stages: [...#TemplateStageItemDto]
}

#TemplateStageRecipientResponse: {
	id:           string
	languageCode?: null | string
	...
}

#TemplateStageRecipientSummaryDto: {
	id: string
	...
}

#TemplateStageStandardRecipientResponse: #TemplateStageRecipientResponse & {
	...
} & {
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
	isDelegationEnabled:        bool
	metadata?: null | [...#RecipientMetadataEntry]
	type: "Standard"
}

#TemplateStageStandardRecipientSummaryDto: #TemplateStageRecipientSummaryDto & {
	...
} & {
	givenName:           string
	surname:             string
	email:               string
	phoneNumber?:         null | string
	notificationChannel?: null | string
	isDelegationEnabled: bool
	type:                "Standard"
}

#TemplateThumbnailDto: {
	templateId: string
	name:       string
	fileData:   string
}

#TextAlignment: "Left" | "Center" | "Right"

#TextAnnotationConfigDto: {
	value?:          null | string
	annotationType: "Text"
}

#TextBoxElementDefinition: {
	position:    #FileElementsPosition
	size:        #FileElementsSize
	textFormat:  #FileElementTextFormat
	readOnly:    bool
	isMultiline: bool
	isPassword:  bool
	maxLength:   int32 & int
}

#TextBoxElementDto: {
	elementId:         string
	elementDefinition: #TextBoxElementDefinition
	source:            #FormFieldSource
	recipientId?:       null | string
	required:          bool
	value:             string
	guidingOrder:      int32 & int
	validation?:        null | #FileElementsFieldValidation
}

#TextDefinition: {
	defaultValue: string
	valueFormat:  "Text"
}

#TextFieldDto: #BaseFieldDto & {
	...
} & {
	page:      int32 & int
	positionX: number
	positionY: number
	width:     number
	height:    number
	font:      #FontStyle
	textInputConfig: matchN(1, [#StringInputConfig, #DateInputConfig, #NumberInputConfig, #PhoneNumberInputConfig, #TimeInputConfig])
	required:  bool
	readOnly:  bool
	fieldType: "TextInput"
}

#TextInputConfig: _

#TextInputField: #BaseField & {
	...
} & {
	readOnly:   bool
	font?:       null | #FontStyle
	text:       string
	password:   bool
	multiline:  bool
	maxLength:  int32 & int
	validation?: null | #FileElementsFieldValidation
	fieldType:  "TextInput"
}

#TextInputType: "StringType" | "DateType" | "NumberType" | "PhoneNumberType" | "TimeType"

#TextTaskUpdateRequest: {
	textInputValue: matchN(1, [#WorkUnitStringInputValue, #WorkUnitNumberInputValue, #WorkUnitDateInputValue])
	fieldType: "TextInput"
}

#ThousandsSeparatorType: "Comma" | "Point" | "Apostrophe" | "Blank" | "None"

#TimeFormatSwaggerEnumProvider: "HH:mm"

#TimeInputConfig: #TextInputConfig & {
	...
} & {
	value?:         null | string
	format?:        null | #TimeFormatSwaggerEnumProvider
	minValue?:      null | string
	maxValue?:      null | string
	textInputType: "TimeType"
}

#TimeZoneDto: {
	code: string
	name: string
}

#TimeZoneListItemDto: {
	timeZone:  string
	code:      string
	utcOffset: string
}

#TimeZonesDto: {
	options: [...#TimeZoneListItemDto]
}

#TimeZonesLookupResponse: {
	timeZones: [...#TimeZoneDto]
}

#TimestampHashAlgorithm: "Sha1" | "Sha256" | "Sha512"

#TimestampSettingsDto: {
	url:           string
	username:      string
	password:      string
	hashAlgorithm: #TimestampHashAlgorithm
}

#TypeToSignSignature: {
	layoutId?:      null | string
	signatureType: "TypeToSignSignature"
}

#TypeToSignSignatureTypeDto: {
	useExternalSignatureImage?: null | #ExternalSignatureImageMode
	preferred?:                 null | bool
	layoutId?:                  null | string
	stampImprintConfiguration?: null | #TypeToSignStampImprintDto
}

#TypeToSignStampImprintDto: {
	displayName:             bool
	displaySignatureDate:    bool
	displayExtraInformation: bool
	fontName?:                null | string
	fontSizeInPt?:            null | int32 & int
	displayEmail:            bool
	displayIp:               bool
}

#UiLanguageDto: {
	code: string
	name: string
}

#UpdateATrustCertificateDto: {
	phoneNumber?: null | string
}

#UpdateAccessCodeDto: {
	code: string
}

#UpdateAuditTrailModeRequest: {
	auditTrailMode: #EnvelopeLogGeneration
}

#UpdateAuthenticationConfigurationDto: {
	accessCode?:         null | #UpdateAccessCodeDto
	smsOneTimePassword?: null | #UpdateSmsOneTimePasswordDto
	oAuthAuthentications?: null | [...#UpdateOAuthAuthenticationDto]
}

#UpdateAutomaticSignatureDataDto: {
	profileId?: null | string
	pluginId?:  null | string
}

#UpdateBankIdSettingsDto: {
	authenticationCertificateThumbprint: string
}

#UpdateBasicSettingsDto: {
	givenName:   string
	surname:     string
	phoneNumber: string
}

#UpdateBulkEnvelopeDto: {
	expirationConfiguration: #UpdateExpirationConfigurationDto
	reminderConfiguration:   #UpdateReminderConfigurationDto
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
	envelopeType:                     #EnvelopeType
	callbackConfiguration?:            null | #CallbackConfigurationDto
	preventFieldsEditingWhenFinished?: null | bool
	afterSendRedirectUrl?:             null | string
	signatureReason?:                  null | string
	allowChangeSignatureReason?:       null | bool
	signatureFormat?:                  null | #SignatureFormat
	fileRestrictedVisibility?:         null | bool
}

#UpdateBulkEnvelopeFileTasksRequest: {
	tasks: [...#BulkEnvelopeFieldTaskItemRequest]
}

#UpdateBulkEnvelopeForIntegrationDto: {
	name:     string
	reminder: #UpdateForIntegrationReminderDto
	expiration: matchN(1, [#AbsoluteIntegrationExpirationDto, #RelativeIntegrationExpirationDto])
	qualifiedTimeStamp?: null | bool
	signatureReason?:    null | string
	signatureFormat?:    null | #SignatureFormat
	notificationMessages?: null | [...#NotificationChannelMessagesDto]
	agreements?: null | [...#Agreement]
	fileRestrictedVisibility?: null | bool
}

#UpdateBulkFileTasksRequest: {
	fieldTasks: [...#FieldTask]
}

#UpdateDisposableCertificateDto: {
	documentIssuingCountry?:       null | string
	identificationIssuingCountry?: null | string
	identificationType?:           null | string
	phoneNumber?:                  null | string
	documentType?:                 null | string
	documentIssuedBy?:             null | string
	documentIssuedOn?:             null | string
	documentExpiryDate?:           null | string
	serialNumber?:                 null | string
	documentNumber?:               null | string
}

#UpdateDisposableCertificateSettingsDto: {
	lraId:                                         string
	user:                                          string
	password?:                                      null | string
	disposableType:                                #DisposableType
	showDisclaimerBeforeCertificateRequest:        bool
	sendDisposableDisclaimerDocumentNotifications: bool
}

#UpdateDocumentClassRequest: {
	name:        string
	description?: null | string
	metadata?: null | [...#DocumentClassMetadataFieldDto]
}

#UpdateEnvelopeDto: {
	expirationConfiguration: #UpdateExpirationConfigurationDto
	reminderConfiguration:   #UpdateReminderConfigurationDto
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
	envelopeType:                     #EnvelopeType
	callbackConfiguration?:            null | #CallbackConfigurationDto
	preventFieldsEditingWhenFinished?: null | bool
	afterSendRedirectUrl?:             null | string
	signatureReason?:                  null | string
	allowChangeSignatureReason?:       null | bool
	signatureFormat?:                  null | #SignatureFormat
	fileRestrictedVisibility?:         null | bool
}

#UpdateEnvelopeFileTasksRequest: {
	tasks: [...#FieldTaskItemRequest]
}

#UpdateEnvelopeForIntegrationDto: {
	name:     string
	reminder: #UpdateForIntegrationReminderDto
	expiration: matchN(1, [#AbsoluteIntegrationExpirationDto, #RelativeIntegrationExpirationDto])
	qualifiedTimeStamp?: null | bool
	signatureReason?:    null | string
	signatureFormat?:    null | #SignatureFormat
	notificationMessages?: null | [...#NotificationChannelMessagesDto]
	agreements?: null | [...#Agreement]
	fileRestrictedVisibility?: null | bool
}

#UpdateEnvelopeRecipientDto: {
	id:                          string
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
	isDelegationEnabled:         bool
	generalPoliciesOverrides?:    null | #UpdateGeneralPoliciesOverridesDto
	signatureReason?:             null | string
	signatureReasonAllowChange?:  null | bool
	metadata?: null | [...#RecipientMetadataEntry]
	syncId?: null | string
}

#UpdateEnvelopeStageAutomaticRecipientRequest: #UpdateEnvelopeStageRecipientRequest & {
	...
} & {
	signatureProfile?:           null | string
	signatureReasonAllowChange?: null | bool
	metadata?: null | [...#RecipientMetadataEntry]
	type: "Automatic"
}

#UpdateEnvelopeStageRecipientRequest: {
	languageCode?:    null | string
	signatureReason?: null | string
	...
}

#UpdateEnvelopeStageRequest: {
	name?:                         null | string
	requiredRecipientCompletions?: null | int32 & int
	type?:                         null | #EnvelopeStageType
}

#UpdateEnvelopeStageStandardRecipientRequest: #UpdateEnvelopeStageRecipientRequest & {
	...
} & {
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
	type:                     "Standard"
}

#UpdateExpirationConfigurationDto: {
	expirationDate?:                  null | string
	expirationInSecondsAfterSending?: null | int64 & int
}

#UpdateFileOrderRequest: {
	files: [...#FileOrderItem]
}

#UpdateFileTasksRequest: {
	fieldTasks: [...#FileTaskItem]
}

#UpdateForIntegrationReminderDto: {
	enabled:                bool
	firstReminderInDays:    int32 & int
	resendIntervalInDays:   int32 & int
	beforeExpirationInDays: int32 & int
}

#UpdateGeneralPoliciesOverridesDto: {
	allowSaveDocument:        bool
	allowSaveAuditTrail:      bool
	allowPrintDocument:       bool
	allowAdhocPdfAttachments: bool
	allowRejectWorkstep:      bool
	allowUndoLastAction:      bool
}

#UpdateGenericSigningPluginsSenderDataDto: {
	senderGenericSigningPlugins?: null | [...#UpdateSenderGenericSigningPluginDto]
}

#UpdateOAuthAuthenticationDto: {
	externalId: string
}

#UpdateOAuthFieldDefinitionRequest: {
	path:                         string
	mode:                         #OAuthSignerProviderFieldMode
	target:                       #OAuthSignerProviderFieldTarget
	id?:                           null | int64 & int
	customFieldName?:              null | string
	genericSigningPluginId?:       null | string
	genericSigningPluginFieldKey?: null | string
}

#UpdateOAuthJwtConfigRequest: {
	oAuthProviderId:  int64 & int
	jwksUri:          string
	issuer:           string
	enforceNonce:     bool
	validateAudience: bool
	validateIssuer:   bool
	validateLifetime: bool
	oAuthFieldDefinitions?: null | [...#UpdateOAuthFieldDefinitionRequest]
}

#UpdateOAuthResourceUriRequest: {
	uri:                   string
	accessTokenParamName:  string
	id?:                    null | int64 & int
	eIdServiceCombination?: null | string
	oAuthFieldDefinitions?: null | [...#UpdateOAuthFieldDefinitionRequest]
}

#UpdateOAuthSignerProviderDetailsRequest: {
	oAuthSignerProvider: #UpdateOAuthSignerProviderRequest
	oAuthJwtConfig?:      null | #UpdateOAuthJwtConfigRequest
	oAuthResourceUris?: null | [...#UpdateOAuthResourceUriRequest]
}

#UpdateOAuthSignerProviderRequest: {
	externalId:         string
	name:               string
	clientId:           string
	authorizationUri:   string
	tokenUri:           string
	authenticationType: int32 & int
	clientSecret?:       null | string
	scope?:              null | string
	logoutUri?:          null | string
}

#UpdateOrganizationDefaultSignatureTypeRequest: {
	signatureType: #SignatureType
}

#UpdateOrganizationDelegationSettingsRequest: {
	delegationPolicy: #DelegationPolicy
}

#UpdateOrganizationFeatureFlag: {
	id:      int32 & int
	enabled: bool
}

#UpdateOrganizationFeatureFlagsRequest: {
	featureFlags: [...#UpdateOrganizationFeatureFlag]
}

#UpdateOrganizationRecipientSettingsRequest: {
	sendFinishedDocumentsToAllRecipients: bool
	showNotEnoughSignaturesWarning:       bool
}

#UpdateOrganizationUserDto: {
	givenName:            string
	surname:              string
	userRegionalSettings: #UserRegionalSettingsRequestDto
	phoneNumber?:          null | string
}

#UpdateOrganizationUserRolesDto: {
	roles: [...string]
}

#UpdateOtpSignatureDataDto: {
	type?:        null | #OtpDeliveryChannel
	phoneNumber?: null | string
}

#UpdatePdfDocumentSettingsDto: {
	pAdESConfiguration?:               null | #OrganizationPAdESConfiguration
	allowSigningOfLockedPdfDocuments: bool
	customTimeStampSettings?:          null | #OrganizationCustomTimeStampServerSettings
}

#UpdatePolicyRequest: {
	name:            string
	isActive:        bool
	sortOrder:       int32 & int
	description?:     null | string
	documentClassId?: null | string
	conditions?: null | [...#PolicyConditionDto]
	actions?: null | [...#PolicyActionDto]
}

#UpdateRecipientAuthenticationSettingItemRequest: {
	name:      string
	isEnabled: bool
}

#UpdateRecipientAuthenticationSettingsRequest: {
	settings?: null | [...#UpdateRecipientAuthenticationSettingItemRequest]
}

#UpdateRegionalSettingsDto: {
	worldTimeZone:    string
	dateTimeFormatId: int32 & int
	uiLanguage:       string
	countryId:        int32 & int
}

#UpdateReminderConfigurationDto: {
	enabled?:                      null | bool
	firstReminderInDays?:          null | int32 & int
	reminderResendIntervalInDays?: null | int32 & int
	beforeExpirationInDays?:       null | int32 & int
}

#UpdateRemoteCertificateDto: {
	userId?:   null | string
	deviceId?: null | string
}

#UpdateRoleRequest: {
	name: string
	permissions: [...#PermissionDto]
	description?: null | string
}

#UpdateSenderGenericSigningPluginDto: {
	pluginId?: null | string
	settings?: null | [...#UpdateSenderGenericSigningPluginSettingsDto]
}

#UpdateSenderGenericSigningPluginSettingsDto: {
	key?:   null | string
	value?: null | string
}

#UpdateSharingOptionsRequest: {
	userGroupIds: [...string]
}

#UpdateSignatureDataConfigurationDto: {
	disposableCertificate?:           null | #UpdateDisposableCertificateDto
	remoteCertificate?:               null | #UpdateRemoteCertificateDto
	aTrustCertificate?:               null | #UpdateATrustCertificateDto
	swissComOnDemand?:                null | #UpdateSwissComOnDemandDto
	swedishBankId?:                   null | #UpdateSwedishBankIdDto
	otpSignatureData?:                null | #UpdateOtpSignatureDataDto
	genericSigningPluginsSenderData?: null | #UpdateGenericSigningPluginsSenderDataDto
	automaticSignatureData?:          null | #UpdateAutomaticSignatureDataDto
}

#UpdateSmsOneTimePasswordDto: {
	phoneNumber?: null | string
}

#UpdateStageDto: {
	id:                        string
	mandatoryRecipientsNumber: int32 & int
	name?:                      null | string
	type?:                      null | #EnvelopeStageType
	stageMode?:                 null | #StageMode
}

#UpdateStageSortOrderRequest: {
	stages: [...#StageSortOrderItem]
}

#UpdateStampImprintConfigurationRequest: {
	defaultLayout: #SignatureAppearanceLayoutRequest
	customSignatures: [...#NamedSignatureAppearanceLayoutRequest]
}

#UpdateSubstituteDelegationDto: {
	delegateeUserEmail:        string
	utilizeAlsoOnCCRecipients: bool
	reason?:                    null | string
	startDate?:                 null | string
	endDate?:                   null | string
}

#UpdateSwedishBankIdDto: {
	personalNumber?:         null | string
	allowAnyPersonalNumber?: null | bool
}

#UpdateSwissComOnDemandDto: {
	commonName?:   null | string
	country?:      null | string
	phoneNumber?:  null | string
	organization?: null | string
	organizationUnits?: null | [...string]
	locality?:        null | string
	serialNumber?:    null | string
	stateOrProvince?: null | string
	pseudonym?:       null | string
}

#UpdateTemplateDto: {
	expirationConfiguration: #UpdateExpirationConfigurationDto
	reminderConfiguration:   #UpdateReminderConfigurationDto
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
	envelopeType:                     #EnvelopeType
	callbackConfiguration?:            null | #CallbackConfigurationDto
	preventFieldsEditingWhenFinished?: null | bool
	afterSendRedirectUrl?:             null | string
	signatureReason?:                  null | string
	allowChangeSignatureReason?:       null | bool
	signatureFormat?:                  null | #SignatureFormat
	fileRestrictedVisibility?:         null | bool
}

#UpdateTemplateFieldTasksRequest: {
	fieldTasks: [...#TemplateFieldTask]
}

#UpdateTemplateFileTasksRequest: {
	tasks: [...#FieldTaskItemRequest]
}

#UpdateTemplateForIntegrationDto: {
	name:     string
	reminder: #UpdateForIntegrationReminderDto
	expiration: matchN(1, [#AbsoluteIntegrationExpirationDto, #RelativeIntegrationExpirationDto])
	qualifiedTimeStamp?: null | bool
	signatureReason?:    null | string
	signatureFormat?:    null | #SignatureFormat
	notificationMessages?: null | [...#NotificationChannelMessagesDto]
	agreements?: null | [...#Agreement]
	fileRestrictedVisibility?: null | bool
}

#UpdateTemplateRecipientDto: {
	id:                          string
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
	isDelegationEnabled:         bool
	generalPoliciesOverrides?:    null | #UpdateGeneralPoliciesOverridesDto
	signatureReason?:             null | string
	signatureReasonAllowChange?:  null | bool
	metadata?: null | [...#RecipientMetadataEntry]
	syncId?: null | string
}

#UpdateTemplateStageAutomaticRecipientRequest: #UpdateTemplateStageRecipientRequest & {
	...
} & {
	signatureProfile?:           null | string
	signatureReasonAllowChange?: null | bool
	metadata?: null | [...#RecipientMetadataEntry]
	type: "Automatic"
}

#UpdateTemplateStageRecipientRequest: {
	languageCode?:    null | string
	signatureReason?: null | string
	...
}

#UpdateTemplateStageRequest: {
	name?:                         null | string
	requiredRecipientCompletions?: null | int32 & int
	type?:                         null | #EnvelopeStageType
}

#UpdateTemplateStageStandardRecipientRequest: #UpdateTemplateStageRecipientRequest & {
	...
} & {
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
	type: "Standard"
}

#UpdatedBasicSettingsDto: {
	id:          string
	givenName:   string
	surname:     string
	phoneNumber?: null | string
}

#UserAndOrganizationDto: {
	id:               string
	givenName:        string
	surname:          string
	email:            string
	organizationId:   string
	organizationName: string
	phoneNumber?:      null | string
}

#UserApplicationContextDto: {
	signatureTypes:       #OrganizationSignatureTypesDto
	defaultSignatureType: #OrganizationDefaultSignatureTypeDto
	signatureOptions: [...#SignatureOptions]
	recipientTypes: [...#RecipientType]
	recipientAuthenticationTypes:      #OrganizationRecipientAuthenticationTypesDto
	signerAgreements:                  #SignerAgreements
	generalPolicies:                   #OrganizationGeneralPoliciesDto
	notificationChannels:              #NotificationChannelsDto
	userPermissions:                   #PermissionsDto
	userGroupPermissions:              #UserGroupPermissionsSetDto
	delegationInfo:                    #DelegationInfo
	oAuthAvailable:                    bool
	automaticRemoteSignatureAvailable: bool
	documentClassesEnabled:            bool
	envelopeEventServiceEnabled:       bool
	fontFamilies: [...string]
	bulkEnvelopeEnabled: bool
}

#UserDefaultUserGroup: {
	id:   string
	name: string
}

#UserDefaultUserGroupDefaultType: "Envelope" | "Template"

#UserGroupContactCreateDto: {
	details?:        null | string
	givenName?:      null | string
	surname?:        null | string
	email?:          null | string
	phoneNumber?:    null | string
	cultureIsoCode?: null | string
}

#UserGroupContactDto: {
	id:             string
	userGroupId:    string
	details?:        null | string
	givenName?:      null | string
	surname?:        null | string
	email?:          null | string
	phoneNumber?:    null | string
	cultureIsoCode?: null | string
}

#UserGroupContactFieldDto: {
	id:          string
	userGroupId: string
	name:        string
}

#UserGroupContactFieldListDto: {
	userGroupContactFields: [...#UserGroupContactFieldDto]
}

#UserGroupContactImportResultDto: {
	imported: int32 & int
}

#UserGroupContactImportValidationErrorResponse: {
	errors: [...#RowError]
}

#UserGroupContactUpdateDto: {
	details?:        null | string
	givenName?:      null | string
	surname?:        null | string
	email?:          null | string
	phoneNumber?:    null | string
	cultureIsoCode?: null | string
}

#UserGroupContactsListDto: {
	userGroupContacts: [...#UserGroupContactDto]
	pagination: #PaginationDto
}

#UserGroupContactsPermissionDto: {
	read:               bool
	createUpdateDelete: bool
	customize:          bool
}

#UserGroupContactsSortingKey: "GivenName" | "Surname" | "Email"

#UserGroupCreateDto: {
	name: string
}

#UserGroupCustomFieldUpdateData: {
	userGroupId: string
	name:        string
	id?:          null | string
}

#UserGroupCustomFieldUpdateRequest: {
	updatedCustomFields: [...#UserGroupCustomFieldUpdateData]
}

#UserGroupDto: {
	id:             string
	organizationId: string
	name:           string
}

#UserGroupEnvelopesPermissionDto: {
	share:  bool
	manage: bool
}

#UserGroupPermissionDataDto: {
	name:        string
	permissions: #UserGroupPermissionDto
}

#UserGroupPermissionDto: {
	users:     #UserGroupUsersPermissionDto
	envelopes: #UserGroupEnvelopesPermissionDto
	templates: #UserGroupTemplatesPermissionDto
	contacts:  #UserGroupContactsPermissionDto
}

#UserGroupPermissionsSetDto: {
	userGroups: [string]: #UserGroupPermissionDataDto
}

#UserGroupTemplatesPermissionDto: {
	share:  bool
	manage: bool
}

#UserGroupUpdateDto: {
	name: string
}

#UserGroupUserBusinessRoleRequest: {
	businessRoleId: string
}

#UserGroupUserDto: {
	id:             string
	email:          string
	givenName:      string
	surname:        string
	permissions:    #UserGroupPermissionDto
	businessRole?:   null | string
	businessRoleId?: null | string
}

#UserGroupUserListDto: {
	userGroupId: string
	userGroupUsers: [...#UserGroupUserDto]
	pagination: #PaginationDto
}

#UserGroupUsersPermissionDto: {
	read:               bool
	createUpdateDelete: bool
}

#UserGroupUsersSortingKey: "GivenName" | "Surname" | "Email" | "BusinessRole"

#UserGroupsListDto: {
	userGroups: [...#UserGroupDto]
	pagination: #PaginationDto
}

#UserGroupsPermissions: {
	read:               bool
	createUpdateDelete: bool
}

#UserGroupsSortingKey: "Name"

#UserImportResultDto: {
	imported: int32 & int
	failure?:  null | #RowError
}

#UserImportValidationErrorResponse: {
	errors: [...#RowError]
}

#UserOrganizationsDto: {
	organizations: [...#OrganizationItemDto]
	defaultOrganizationId: string
}

#UserRegionalSettingsDto: {
	worldTimeZone:    string
	dateTimeFormatId: int32 & int
	uiLanguage:       string
	countryId:        int32 & int
}

#UserRegionalSettingsRequestDto: {
	timeZone:       string
	dateTimeFormat: #DateTimeFormatSwaggerEnumProvider
	language:       string
	country:        string
}

#UserRoleRequest: {
	name: string
}

#UserRolesDto: {
	roles: [...string]
}

#UsersSettings: {
	read:               bool
	createUpdateDelete: bool
}

#UsersSortingKey: "GivenName" | "Surname" | "Email" | "Enabled"

#ValidateOrganizationDto: {
	name:                                  string
	isoCulture:                            string
	onePlatformBusinessRelationIdentifier?: null | string
	features: [...string]
}

#VersionInfo: {
	imageTag: string
	version:  string
}

#WebhookAuthenticationRequest: {
	headers?: null | {
		[string]: string
	}
	clientCert?: null | string
	clientKey?:  null | string
}

#WebhookSubscriptionDto: {
	id:                   string
	url:                  string
	hasHeaders:           bool
	hasClientCertificate: bool
	createdAt:            string
}

#WebhookSubscriptionRequest: {
	url:            string
	authentication?: null | #WebhookAuthenticationRequest
}

#WorkUnitApprovalFieldResponse: #WorkUnitFieldResponse & {
	...
} & {
	page:      int32 & int
	positionX: number
	positionY: number
	width:     number
	height:    number
	required:  bool
	fieldType: "Approval"
}

#WorkUnitAreaReadConfirmationFieldResponse: #WorkUnitFieldResponse & {
	...
} & {
	page:      int32 & int
	positionX: number
	positionY: number
	width:     number
	height:    number
	required:  bool
	readOnly:  bool
	fieldType: "AreaReadConfirmation"
}

#WorkUnitAttachmentFieldResponse: #WorkUnitFieldResponse & {
	...
} & {
	page:      int32 & int
	positionX: number
	positionY: number
	width:     number
	height:    number
	required:  bool
	label?:     null | string
	fieldType: "Attachment"
}

#WorkUnitAuthenticateRequest: {
	code: string
}

#WorkUnitAuthenticationProviderType: "AccessCode"

#WorkUnitAuthenticationRequiredResponse: {
	provider?: null | #WorkUnitAuthenticationProviderType
}

#WorkUnitAutomaticSignature: {
	layoutId?:      null | string
	signatureType: "AutomaticSignature"
}

#WorkUnitAutomaticSignatureResponse: {
	layoutId?:      null | string
	signatureType: "AutomaticSignature"
}

#WorkUnitBiometricSignature: {
	signatureType: "BiometricSignature"
}

#WorkUnitBiometricSignatureResponse: {
	signatureType: "BiometricSignature"
}

#WorkUnitCheckboxFieldResponse: #WorkUnitFieldResponse & {
	...
} & {
	page:      int32 & int
	positionX: number
	positionY: number
	width:     number
	height:    number
	checked:   bool
	value:     string
	required:  bool
	readOnly:  bool
	fieldType: "Checkbox"
}

#WorkUnitClickToSignSignature: {
	layoutId?:      null | string
	signatureType: "ClickToSignSignature"
}

#WorkUnitClickToSignSignatureRequest: {
	signatureType: "ClickToSignSignature"
}

#WorkUnitClickToSignSignatureResponse: {
	layoutId?:      null | string
	signatureType: "ClickToSignSignature"
}

#WorkUnitDateInputConfigResponseResponse: #WorkUnitTextInputConfigResponse & {
	...
} & {
	value?:         null | "2006-01-02"
	format?:        null | #DateFormatSwaggerEnumProvider
	minValue?:      null | "2006-01-02"
	maxValue?:      null | "2006-01-02"
	textInputType: "DateType"
}

#WorkUnitDateInputValue: {
	value:         "2006-01-02"
	textInputType: "DateType"
}

#WorkUnitDecimalSeparatorTypeResponse: "None" | "Comma" | "Dot" | "Apostrophe"

#WorkUnitDisposableCertificateSignature: {
	layoutId?:      null | string
	signatureType: "DisposableCertificateSignature"
}

#WorkUnitDisposableCertificateSignatureResponseResponse: {
	layoutId?:      null | string
	signatureType: "DisposableCertificateSignature"
}

#WorkUnitDrawToSignSignature: {
	signatureImage?: null | string
	layoutId?:       null | string
	signatureType:  "DrawToSignSignature"
}

#WorkUnitDrawToSignSignatureRequest: {
	signatureType:  "DrawToSignSignature"
	signatureImage?: null | string
}

#WorkUnitDrawToSignSignatureResponse: {
	layoutId?:      null | string
	signatureType: "DrawToSignSignature"
}

#WorkUnitDropDownFieldResponse: #WorkUnitFieldResponse & {
	...
} & {
	page:      int32 & int
	positionX: number
	positionY: number
	width:     number
	height:    number
	font:      #WorkUnitFontStyleResponse
	options?: null | [...#WorkUnitOptionResponse]
	required:   bool
	readOnly:   bool
	isEditable: bool
	fieldType:  "DropDown"
}

#WorkUnitElementSourceResponse: "File" | "UserDefined"

#WorkUnitFieldResponse: {
	id: string,
	...
}

#WorkUnitFieldTaskResponse: {
	field: matchN(1, [#WorkUnitSignatureFieldResponse, #WorkUnitTextFieldResponse, #WorkUnitCheckboxFieldResponse, #WorkUnitDropDownFieldResponse, #WorkUnitListBoxFieldResponse, #WorkUnitAttachmentFieldResponse, #WorkUnitLinkFieldResponse, #WorkUnitFileReadConfirmationFieldResponse, #WorkUnitPageReadConfirmationFieldResponse, #WorkUnitAreaReadConfirmationFieldResponse, #WorkUnitRadioButtonFieldResponse, #WorkUnitApprovalFieldResponse, #WorkUnitInvisibleSignatureFieldResponse])
	sortOrder:   int32 & int
	recipientId?: null | string
	source:      #WorkUnitElementSourceResponse
	displayName?: null | string
	completed:   bool
}

#WorkUnitFieldTaskSignatureType: "ClickToSignSignature" | "DrawToSignSignature" | "TypeToSignSignature" | "LocalCertificateSignature" | "DisposableCertificateSignature" | "BiometricSignature" | "RemoteCertificateSignature" | "OneTimePasswordSignature" | "PluginSignature" | "AutomaticSignature"

#WorkUnitFieldTaskSignatureTypeRequest: "ClickToSignSignature" | "DrawToSignSignature" | "TypeToSignSignature"

#WorkUnitFieldTaskSignatureTypeResponse: "ClickToSignSignature" | "DrawToSignSignature" | "TypeToSignSignature" | "LocalCertificateSignature" | "DisposableCertificateSignature" | "BiometricSignature" | "RemoteCertificateSignature" | "OneTimePasswordSignature" | "PluginSignature" | "AutomaticSignature"

#WorkUnitFieldType: "Signature" | "TextInput" | "Checkbox" | "DropDown" | "ListBox" | "Attachment" | "Link" | "FileReadConfirmation" | "PageReadConfirmation" | "AreaReadConfirmation" | "RadioButton" | "Approval" | "InvisibleSignature"

#WorkUnitFieldTypeResponse: "Signature" | "TextInput" | "Checkbox" | "DropDown" | "ListBox" | "Attachment" | "Link" | "FileReadConfirmation" | "PageReadConfirmation" | "AreaReadConfirmation" | "RadioButton" | "Approval" | "InvisibleSignature"

#WorkUnitFileReadConfirmationFieldResponse: #WorkUnitFieldResponse & {
	...
} & {
	required:  bool
	confirmed: bool
	readOnly:  bool
	fieldType: "FileReadConfirmation"
}

#WorkUnitFileResponse: {
	documentNumber: int32 & int
	name:           string
	tasks: [...#WorkUnitFieldTaskResponse]
}

#WorkUnitFontStyleResponse: {
	color:  string
	size:   number
	name:   string
	bold:   bool
	italic: bool
	align:  #WorkUnitTextAlignResponse
}

#WorkUnitInvisibleSignatureFieldResponse: #WorkUnitFieldResponse & {
	...
} & {
	allowedSignatureTypes?: null | [...matchN(1, [#WorkUnitLocalCertificateSignatureResponseResponse, #WorkUnitRemoteCertificateSignatureResponseResponse, #WorkUnitDisposableCertificateSignatureResponseResponse, #WorkUnitPluginSignatureResponseResponse])]
	qualifiedTimeStamp?: null | bool
	fieldType:          "InvisibleSignature"
}

#WorkUnitLinkFieldResponse: #WorkUnitFieldResponse & {
	...
} & {
	page:      int32 & int
	positionX: number
	positionY: number
	width:     number
	height:    number
	reference?: null | string
	fieldType: "Link"
}

#WorkUnitListBoxFieldResponse: #WorkUnitFieldResponse & {
	...
} & {
	page:      int32 & int
	positionX: number
	positionY: number
	width:     number
	height:    number
	font:      #WorkUnitFontStyleResponse
	options?: null | [...#WorkUnitOptionResponse]
	multiSelect: bool
	required:    bool
	readOnly:    bool
	fieldType:   "ListBox"
}

#WorkUnitLocalCertificateSignature: {
	layoutId?:      null | string
	signatureType: "LocalCertificateSignature"
}

#WorkUnitLocalCertificateSignatureResponseResponse: {
	layoutId?:      null | string
	signatureType: "LocalCertificateSignature"
}

#WorkUnitNumberInputConfigResponseResponse: #WorkUnitTextInputConfigResponse & {
	...
} & {
	value?:              null | number
	symbol?:             null | #WorkUnitNumberSymbol
	thousandsSeparator?: null | #WorkUnitThousandsSeparatorTypeResponse
	decimalSeparator?:   null | #WorkUnitDecimalSeparatorTypeResponse
	decimalPlaces?:      null | int32 & int
	minValue?:           null | number
	maxValue?:           null | number
	textInputType:      "NumberType"
}

#WorkUnitNumberInputValue: {
	value:         number
	textInputType: "NumberType"
}

#WorkUnitNumberSymbol: {
	value?:    null | string
	position: #WorkUnitSymbolLocationTypeResponse
}

#WorkUnitOneTimePasswordSignature: {
	layoutId?:      null | string
	signatureType: "OneTimePasswordSignature"
}

#WorkUnitOneTimePasswordSignatureResponse: {
	layoutId?:      null | string
	signatureType: "OneTimePasswordSignature"
}

#WorkUnitOptionResponse: {
	key:      string
	value:    string
	selected: bool
}

#WorkUnitPageReadConfirmationFieldResponse: #WorkUnitFieldResponse & {
	...
} & {
	page:      int32 & int
	positionX: number
	positionY: number
	width:     number
	height:    number
	required:  bool
	readOnly:  bool
	fieldType: "PageReadConfirmation"
}

#WorkUnitPhoneNumberInputConfigResponseResponse: #WorkUnitTextInputConfigResponse & {
	...
} & {
	value:         string
	format?:        null | string
	textInputType: "PhoneNumberType"
}

#WorkUnitPluginSignature: {
	pluginId:      string
	signatureType: "PluginSignature"
}

#WorkUnitPluginSignatureResponseResponse: {
	pluginId:      string
	signatureType: "PluginSignature"
}

#WorkUnitRadioButtonFieldResponse: #WorkUnitFieldResponse & {
	...
} & {
	page:      int32 & int
	positionX: number
	positionY: number
	width:     number
	height:    number
	groupName: string
	readOnly:  bool
	checked:   bool
	value:     string
	required:  bool
	fieldType: "RadioButton"
}

#WorkUnitRemoteCertificateSignature: {
	layoutId?:      null | string
	signatureType: "RemoteCertificateSignature"
}

#WorkUnitRemoteCertificateSignatureResponseResponse: {
	layoutId?:      null | string
	signatureType: "RemoteCertificateSignature"
}

#WorkUnitResponse: {
	id: string
	files: [...#WorkUnitFileResponse]
	isSequenceEnforced: bool
	isFinished:         bool
}

#WorkUnitSignatureFieldResponse: #WorkUnitFieldResponse & {
	...
} & {
	page:      int32 & int
	positionX: number
	positionY: number
	width:     number
	height:    number
	allowedSignatureTypes: [...matchN(1, [#WorkUnitClickToSignSignatureResponse, #WorkUnitDrawToSignSignatureResponse, #WorkUnitTypeToSignSignatureResponse, #WorkUnitLocalCertificateSignatureResponseResponse, #WorkUnitDisposableCertificateSignatureResponseResponse, #WorkUnitBiometricSignatureResponse, #WorkUnitRemoteCertificateSignatureResponseResponse, #WorkUnitOneTimePasswordSignatureResponse, #WorkUnitPluginSignatureResponseResponse, #WorkUnitAutomaticSignatureResponse])]
	qualifiedTimeStamp?: null | bool
	required:           bool
	readOnly:           bool
	fieldType:          "Signature"
}

#WorkUnitSignaturePosition: {
	x:      number
	y:      number
	width:  number
	height: number
}

#WorkUnitSignaturePositionRequest: {
	x:      number
	y:      number
	width:  number
	height: number
}

#WorkUnitStringInputConfigResponseResponse: #WorkUnitTextInputConfigResponse & {
	...
} & {
	value:         string
	password:      bool
	multiline:     bool
	maxLength:     int32 & int
	textInputType: "StringType"
}

#WorkUnitStringInputValue: {
	value:         string
	textInputType: "StringType"
}

#WorkUnitSymbolLocationTypeResponse: "Start" | "StartWithBlank" | "End" | "EndWithBlank"

#WorkUnitTextAlignResponse: "Left" | "Center" | "Right"

#WorkUnitTextFieldResponse: #WorkUnitFieldResponse & {
	...
} & {
	page:      int32 & int
	positionX: number
	positionY: number
	width:     number
	height:    number
	font:      #WorkUnitFontStyleResponse
	textInputConfig: matchN(1, [#WorkUnitStringInputConfigResponseResponse, #WorkUnitDateInputConfigResponseResponse, #WorkUnitNumberInputConfigResponseResponse, #WorkUnitPhoneNumberInputConfigResponseResponse, #WorkUnitTimeInputConfigResponse])
	required:  bool
	readOnly:  bool
	fieldType: "TextInput"
}

#WorkUnitTextInputConfigResponse: _

#WorkUnitTextInputType: "StringType" | "DateType" | "NumberType" | "PhoneNumberType" | "TimeType"

#WorkUnitTextInputTypeResponse: "StringType" | "DateType" | "NumberType" | "PhoneNumberType" | "TimeType"

#WorkUnitThousandsSeparatorTypeResponse: "None" | "Comma" | "Dot" | "Apostrophe" | "Space"

#WorkUnitTimeInputConfigResponse: #WorkUnitTextInputConfigResponse & {
	...
} & {
	value?:         null | string
	format?:        null | #TimeFormatSwaggerEnumProvider
	minValue?:      null | string
	maxValue?:      null | string
	textInputType: "TimeType"
}

#WorkUnitTypeToSignSignature: {
	text?:                 null | string
	textFontFamily?:       null | string
	textFontColor?:        null | string
	textFontSizeFraction?: null | number
	position?:             null | #WorkUnitSignaturePosition
	layoutId?:             null | string
	signatureType:        "TypeToSignSignature"
}

#WorkUnitTypeToSignSignatureRequest: {
	signatureType:        "TypeToSignSignature"
	text?:                 null | string
	textFontFamily?:       null | string
	textFontColor?:        null | string
	textFontSizeFraction?: null | number
	position?:             null | #WorkUnitSignaturePositionRequest
}

#WorkUnitTypeToSignSignatureResponse: {
	layoutId?:      null | string
	signatureType: "TypeToSignSignature"
}
