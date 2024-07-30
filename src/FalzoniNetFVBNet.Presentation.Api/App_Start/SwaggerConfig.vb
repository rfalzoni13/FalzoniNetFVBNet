Imports System.Web.Http
Imports System.Web.Http.Description
Imports System.Web.Http.Filters
Imports Swashbuckle.Application
Imports Swashbuckle.Swagger

<Assembly: PreApplicationStartMethod(GetType(SwaggerConfig), "Register")>

Public Class SwaggerConfig
    Public Shared Sub Register()
        Dim thisAssembly = GetType(SwaggerConfig).Assembly

        GlobalConfiguration.Configuration.EnableSwagger(
            Sub(c)
                ' By default, the service root url Is inferred from the request used to access the docs.
                ' However, there may be situations (e.g. proxy And load-balanced environments) where this does Not
                ' resolve correctly. You can workaround this by providing your own code to determine the root URL.
                '
                'c.RootUrl(Function(req) GetRootUrlFromAppConfig())

                ' If schemes are Not explicitly provided in a Swagger 2.0 document, then the scheme used to access
                ' the docs Is taken as the default. If your API supports multiple schemes And you want to be explicit
                ' about them, you can use the "Schemes" option as shown below.
                '
                'c.Schemes({"http", "https"})

                ' Use "SingleApiVersion" to describe a single version API. Swagger 2.0 includes an "Info" object to
                ' hold additional metadata for an API. Version And title are required but you can also provide
                ' additional fields by chaining methods off SingleApiVersion.
                '
                c.SingleApiVersion("v1", "Falzoni Api").Description("Documentação Swagger da Api Falzoni")

                ' If you want the output Swagger docs to be indented properly, enable the "PrettyPrint" option.
                '
                'c.PrettyPrint()
                '
                ' If your API has multiple versions, use "MultipleApiVersions" instead of "SingleApiVersion".
                ' In this case, you must provide a lambda that tells Swashbuckle which actions should be
                ' included in the docs for a given API version. Like "SingleApiVersion", each call to "Version"
                ' returns an "Info" builder so you can provide additional metadata per API version.
                '
                'c.MultipleApiVersions(
                '    Function(apiDesc, targetApiVersion) ResolveVersionSupportByRouteConstraint(apiDesc, targetApiVersion),
                '    Sub(vc)
                '        vc.Version("v2", "Swashbuckle Dummy API V2")
                '        vc.Version("v1", "Swashbuckle Dummy API V1")
                '    End Sub)

                ' You can use "BasicAuth", "ApiKey" Or "OAuth2" options to describe security schemes for the API.
                ' See https:'github.com/swagger-api/swagger-spec/blob/master/versions/2.0.md for more details.
                ' NOTE: These only define the schemes And need to be coupled with a corresponding "security" property
                ' at the document Or operation level to indicate which schemes are required for an operation. To do this,
                ' you'll need to implement a custom IDocumentFilter and/or IOperationFilter to set these properties
                ' according to your specific authorization implementation
                '
                'c.BasicAuth("basic").Description("Basic HTTP Authentication")
                '
                ' NOTE: You must also configure 'EnableApiKeySupport' below in the SwaggerUI section
                'c.ApiKey("apiKey").
                'Description("API Key Authentication").
                'Name("apiKey").
                'In("header")
                '
                'c.OAuth2("oauth2").
                'Description("OAuth2 Implicit Grant").
                'Flow("implicit").
                'AuthorizationUrl("http:'petstore.swagger.wordnik.com/api/oauth/dialog").
                'TokenUrl("https:'tempuri.org/token").
                'Scopes(Sub(scopes)
                '           scopes.Add("read", "Read access to protected resources")
                '           scopes.Add("write", "Write access to protected resources")
                '       End Sub)

                ' Set this flag to omit descriptions for any actions decorated with the Obsolete attribute
                'c.IgnoreObsoleteActions()

                ' Each operation be assigned one Or more tags which are then used by consumers for various reasons.
                ' For example, the swagger-ui groups operations according to the first tag of each operation.
                ' By default, this will be controller name but you can use the "GroupActionsBy" option to
                ' override with any value.
                '
                'c.GroupActionsBy(Function(apiDesc) apiDesc.HttpMethod.ToString())

                ' You can also specify a custom sort order for groups (as defined by "GroupActionsBy") to dictate
                ' the order in which operations are listed. For example, if the default grouping Is in place
                ' (controller name) And you specify a descending alphabetic sort order, then actions from a
                ' ProductsController will be listed before those from a CustomersController. This Is typically
                ' used to customize the order of groupings in the swagger-ui.
                '
                'c.OrderActionGroupsBy(New DescendingAlphabeticComparer())

                ' If you annotate Controllers And API Types with
                ' Xml comments (http:'msdn.microsoft.com/en-us/library/b2s063f7(v=vs.110).aspx), you can incorporate
                ' those comments into the generated docs And UI. You can enable this by providing the path to one Or
                ' more Xml comment files.
                '
                c.IncludeXmlComments(String.Format("{0}\bin\FalzoniNetFVBNet.Presentation.Api.xml", System.AppDomain.CurrentDomain.BaseDirectory))

                ' Swashbuckle makes a best attempt at generating Swagger compliant JSON schemas for the various types
                ' exposed in your API. However, there may be occasions when more control of the output Is needed.
                ' This Is supported through the "MapType" And "SchemaFilter" options:
                '
                ' Use the "MapType" option to override the Schema generation for a specific type.
                ' It should be noted that the resulting Schema will be placed "inline" for any applicable Operations.
                ' While Swagger 2.0 supports inline definitions for "all" Schema types, the swagger-ui tool does Not.
                ' It expects "complex" Schemas to be defined separately And referenced. For this reason, you should only
                ' use the "MapType" option when the resulting Schema Is a primitive Or array type. If you need to alter a
                ' complex Schema, use a Schema filter.
                '
                'c.MapType(Of ProductType)(Function() New Schema With {.Type = "integer", .Format = "int32"})

                ' If you want to post-modify "complex" Schemas once they've been generated, across the board or for a
                ' specific type, you can wire up one Or more Schema filters.
                '
                'c.SchemaFilter(Of ApplySchemaVendorExtensions)()

                ' In a Swagger 2.0 document, complex types are typically declared globally And referenced by unique
                ' Schema Id. By default, Swashbuckle does Not use the full type name in Schema Ids. In most cases, this
                ' works well because it prevents the "implementation detail" of type namespaces from leaking into your
                ' Swagger docs And UI. However, if you have multiple types in your API with the same class name, you'll
                ' need to opt out of this behavior to avoid Schema Id conflicts.
                '
                'c.UseFullTypeNameInSchemaIds()

                ' Alternatively, you can provide your own custom strategy for inferring SchemaId's for
                ' describing "complex" types in your API.
                '
                'c.SchemaId(Function(t) If(t.FullName.Contains("`"), t.FullName.Substring(0, t.FullName.IndexOf("`")), t.FullName))

                ' Set this flag to omit schema property descriptions for any type properties decorated with the
                ' Obsolete attribute
                'c.IgnoreObsoleteProperties()

                ' In accordance with the built in JsonSerializer, Swashbuckle will, by default, describe enums as integers.
                ' You can change the serializer behavior by configuring the StringToEnumConverter globally Or for a given
                ' enum type. Swashbuckle will honor this change out-of-the-box. However, if you use a different
                ' approach to serialize enums as strings, you can also force Swashbuckle to describe them as strings.
                '
                'c.DescribeAllEnumsAsStrings()

                ' Similar to Schema filters, Swashbuckle also supports Operation And Document filters
                '
                ' Post-modify Operation descriptions once they've been generated by wiring up one or more
                ' Operation filters.
                '
                'c.OperationFilter(Of AddDefaultResponse)()
                '
                ' If you've defined an OAuth2 flow as described above, you could use a custom filter
                ' to inspect some attribute on each action And infer which (if any) OAuth2 scopes are required
                ' to execute the operation
                '
                c.OperationFilter(Of AddAuthorizationHeaderParameterOperationFilter)()

                ' Post-modify the entire Swagger document by wiring up one Or more Document filters.
                ' This gives full control to modify the final SwaggerDocument. You should have a good understanding of
                ' the Swagger 2.0 spec. - https:'github.com/swagger-api/swagger-spec/blob/master/versions/2.0.md
                ' before using this option.
                '
                c.DocumentFilter(Of AuthTokenOperation)()

                ' In contrast to WebApi, Swagger 2.0 does Not include the query string component when mapping a URL
                ' to an action. As a result, Swashbuckle will raise an exception if it encounters multiple actions
                ' with the same path (sans query string) And HTTP method. You can workaround this by providing a
                ' custom strategy to pick a winner Or merge the descriptions for the purposes of the Swagger docs
                '
                'c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First())

                ' Wrap the default SwaggerGenerator with additional behavior (e.g. caching) Or provide an
                ' alternative implementation for ISwaggerProvider with the CustomProvider option.
                '
                'c.CustomProvider((defaultProvider) => New CachingSwaggerProvider(defaultProvider))
            End Sub).
            EnableSwaggerUi(
            Sub(c)
                ' Use the "DocumentTitle" option to change the Document title.
                ' Very helpful when you have multiple Swagger pages open, to tell them apart.
                '
                'c.DocumentTitle("My Swagger UI")

                ' Use the "InjectStylesheet" option to enrich the UI with one Or more additional CSS stylesheets.
                ' The file must be included in your project as an "Embedded Resource", And then the resource's
                ' "Logical Name" Is passed to the method as shown below.
                '
                'c.InjectStylesheet(containingAssembly, "Swashbuckle.Dummy.SwaggerExtensions.testStyles1.css")

                ' Use the "InjectJavaScript" option to invoke one Or more custom JavaScripts after the swagger-ui
                ' has loaded. The file must be included in your project as an "Embedded Resource", And then the resource's
                ' "Logical Name" Is passed to the method as shown above.
                '
                'c.InjectJavaScript(thisAssembly, "Swashbuckle.Dummy.SwaggerExtensions.testScript1.js")

                ' The swagger-ui renders boolean data types as a dropdown. By default, it provides "true" And "false"
                ' strings as the possible choices. You can use this option to change these to something else,
                ' for example 0 And 1.
                '
                'c.BooleanValues({"0", "1"})

                ' By default, swagger-ui will validate specs against swagger.io's online validator and display the result
                ' in a badge at the bottom of the page. Use these options to set a different validator URL Or to disable the
                ' feature entirely.
                'c.SetValidatorUrl("http:'localhost/validator")
                'c.DisableValidator()

                ' Use this option to control how the Operation listing Is displayed.
                ' It can be set to "None" (default), "List" (shows operations for each resource),
                ' Or "Full" (fully expanded: shows operations And their details).
                '
                'c.DocExpansion(DocExpansion.List)

                ' Specify which HTTP operations will have the 'Try it out!' option. An empty paramter list disables
                ' it for all operations.
                '
                'c.SupportedSubmitMethods("GET", "HEAD")

                ' Use the CustomAsset option to provide your own version of assets used in the swagger-ui.
                ' It's typically used to instruct Swashbuckle to return your version instead of the default
                ' when a request Is made for "index.html". As with all custom content, the file must be included
                ' in your project as an "Embedded Resource", And then the resource's "Logical Name" is passed to
                ' the method as shown below.
                '
                'c.CustomAsset("index", containingAssembly, "YourWebApiProject.SwaggerExtensions.index.html")

                ' If your API has multiple versions And you've applied the MultipleApiVersions setting
                ' as described above, you can also enable a select box in the swagger-ui, that displays
                ' a discovery URL for each version. This provides a convenient way for users to browse documentation
                ' for different API versions.
                '
                'c.EnableDiscoveryUrlSelector()

                ' If your API supports the OAuth2 Implicit flow, And you've described it correctly, according to
                ' the Swagger 2.0 specification, you can enable UI support as shown below.
                '
                'c.EnableOAuth2Support(
                '    clientId:="test-client-id",
                '    clientSecret:=Nothing,
                ' realm:="test-realm",
                '    appName:="Swagger UI",
                '    additionalQueryStringParams:=New Dictionary(Of String, String)() {{"foo", "bar"}}
                ')

                ' If your API supports ApiKey, you can override the default values.
                ' "apiKeyIn" can either be "query" Or "header"
                '
                'c.EnableApiKeySupport("apiKey", "header")
            End Sub)
    End Sub
End Class

Public Class AuthTokenOperation
    Implements IDocumentFilter

    ''' <summary>
    ''' Token de acesso
    ''' </summary>
    ''' <param name="swaggerDoc"></param>
    ''' <param name="schemaRegistry"></param>
    ''' <param name="apiExplorer"></param>
    Public Sub Apply(swaggerDoc As SwaggerDocument, schemaRegistry As SchemaRegistry, apiExplorer As IApiExplorer) Implements IDocumentFilter.Apply
        swaggerDoc.paths.Add("/Api/Account/Login", New PathItem With
            {
                .post = New Operation With
                {
                    .summary = "Bearer Token de acesso",
                    .description = "Bearer Token de autoriza��o para endpoints",
                    .tags = New List(Of String) From {"Account"},
                    .consumes = New List(Of String) From
                    {
                        "application/x-www-form-urlencoded"
                    },
                    .parameters = New List(Of Parameter) From
                    {
                        New Parameter With
                        {
                            .type = "string",
                            .name = "grant_type",
                            .required = True,
                            .[in] = "formData",
                            .[default] = "password"
                        },
                        New Parameter With
                        {
                            .type = "string",
                            .name = "username",
                            .required = False,
                            .[in] = "formData"
                        },
                        New Parameter With
                        {
                            .type = "string",
                            .name = "password",
                            .required = False,
                            .[in] = "formData"
                        },
                        New Parameter With
                        {
                            .type = "string",
                            .name = "refresh_token",
                            .required = False,
                            .[in] = "formData"
                        }
                    },
                    .responses = New Dictionary(Of String, Response) From
                    {
                        {"500", New Response With
                            {
                                .description = "Internal Server Error"
                            }
                        },
                        {"400", New Response With
                            {
                                .description = "Bad Request"
                            }
                        }
                    }
                }
            })
    End Sub
End Class

Public Class AddAuthorizationHeaderParameterOperationFilter
    Implements IOperationFilter

    ''' <summary>
    ''' Applies the operation filter.
    ''' </summary>
    ''' <param name="operation"></param>
    ''' <param name="schemaRegistry"></param>
    ''' <param name="apiDescription"></param>
    Public Sub Apply(operation As Operation, schemaRegistry As SchemaRegistry, apiDescription As ApiDescription) Implements IOperationFilter.Apply
        If operation.parameters Is Nothing Then
            operation.parameters = New List(Of Parameter)()
        End If

        Dim filterPipeLine = apiDescription.ActionDescriptor.GetFilterPipeline()

        Dim isAuthorized = filterPipeLine.Select(Function(f) f.Instance).Any(Function(f) f Is GetType(IAuthorizationFilter))

        Dim allowAnonymousAttributes = apiDescription.ActionDescriptor.GetCustomAttributes(Of AllowAnonymousAttribute)().Any()

        If Not allowAnonymousAttributes And isAuthorized Then
            operation.parameters.Add(New Parameter With
            {
                .name = "Authorization",
                .[in] = "header",
                .description = "Token de acesso",
                .required = False,
                .type = "string"
            })
        End If
    End Sub
End Class

