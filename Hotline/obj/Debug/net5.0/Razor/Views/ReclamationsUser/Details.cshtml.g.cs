#pragma checksum "C:\Users\Ines\Desktop\HotlineWebApp\Tryingtofix\Hotline\Views\ReclamationsUser\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ad7d0cda7ff89a5255a05d7f9ad08c3c76e7dbec"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_ReclamationsUser_Details), @"mvc.1.0.view", @"/Views/ReclamationsUser/Details.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\Ines\Desktop\HotlineWebApp\Tryingtofix\Hotline\Views\_ViewImports.cshtml"
using Hotline;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Ines\Desktop\HotlineWebApp\Tryingtofix\Hotline\Views\_ViewImports.cshtml"
using Hotline.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ad7d0cda7ff89a5255a05d7f9ad08c3c76e7dbec", @"/Views/ReclamationsUser/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ec4a2a600302c4d57296472fe2ab22f4b39af785", @"/Views/_ViewImports.cshtml")]
    public class Views_ReclamationsUser_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Hotline.Models.Reclamation>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "ReclamationsUser", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\Ines\Desktop\HotlineWebApp\Tryingtofix\Hotline\Views\ReclamationsUser\Details.cshtml"
  
    ViewData["Title"] = "Details";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n<div id=\"center\" class=\"mt-5\">\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ad7d0cda7ff89a5255a05d7f9ad08c3c76e7dbec4060", async() => {
                WriteLiteral("<i class=\"fas fa-arrow-left fa-2x\"></i>");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n    <h3 class=\"mt-2 text-center\">Details de la réclamation N° ");
#nullable restore
#line 11 "C:\Users\Ines\Desktop\HotlineWebApp\Tryingtofix\Hotline\Views\ReclamationsUser\Details.cshtml"
                                                         Write(Model.Numero);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h3>\r\n    <hr />\r\n    <dl class=\"row\">\r\n        <dt class=\"col-sm-4\">\r\n            ");
#nullable restore
#line 15 "C:\Users\Ines\Desktop\HotlineWebApp\Tryingtofix\Hotline\Views\ReclamationsUser\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Numero));

#line default
#line hidden
#nullable disable
            WriteLiteral(" :\r\n        </dt>\r\n        <dd class=\"col-sm-8\">\r\n            ");
#nullable restore
#line 18 "C:\Users\Ines\Desktop\HotlineWebApp\Tryingtofix\Hotline\Views\ReclamationsUser\Details.cshtml"
       Write(Html.DisplayFor(model => model.Numero));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-4\">\r\n            ");
#nullable restore
#line 21 "C:\Users\Ines\Desktop\HotlineWebApp\Tryingtofix\Hotline\Views\ReclamationsUser\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Client));

#line default
#line hidden
#nullable disable
            WriteLiteral(" :\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 24 "C:\Users\Ines\Desktop\HotlineWebApp\Tryingtofix\Hotline\Views\ReclamationsUser\Details.cshtml"
       Write(Html.DisplayFor(model => model.Client.Login));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-4\">\r\n            ");
#nullable restore
#line 27 "C:\Users\Ines\Desktop\HotlineWebApp\Tryingtofix\Hotline\Views\ReclamationsUser\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Projet));

#line default
#line hidden
#nullable disable
            WriteLiteral(" :\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 30 "C:\Users\Ines\Desktop\HotlineWebApp\Tryingtofix\Hotline\Views\ReclamationsUser\Details.cshtml"
       Write(Html.DisplayFor(model => model.Projet.Nom));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-4\">\r\n            ");
#nullable restore
#line 33 "C:\Users\Ines\Desktop\HotlineWebApp\Tryingtofix\Hotline\Views\ReclamationsUser\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Domaine));

#line default
#line hidden
#nullable disable
            WriteLiteral(" :\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 36 "C:\Users\Ines\Desktop\HotlineWebApp\Tryingtofix\Hotline\Views\ReclamationsUser\Details.cshtml"
       Write(Html.DisplayFor(model => model.Domaine.Nom));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-4\">\r\n            ");
#nullable restore
#line 39 "C:\Users\Ines\Desktop\HotlineWebApp\Tryingtofix\Hotline\Views\ReclamationsUser\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Description));

#line default
#line hidden
#nullable disable
            WriteLiteral(" :\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 42 "C:\Users\Ines\Desktop\HotlineWebApp\Tryingtofix\Hotline\Views\ReclamationsUser\Details.cshtml"
       Write(Html.DisplayFor(model => model.Description));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-4\">\r\n            ");
#nullable restore
#line 45 "C:\Users\Ines\Desktop\HotlineWebApp\Tryingtofix\Hotline\Views\ReclamationsUser\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.DateSoumission));

#line default
#line hidden
#nullable disable
            WriteLiteral(" :\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 48 "C:\Users\Ines\Desktop\HotlineWebApp\Tryingtofix\Hotline\Views\ReclamationsUser\Details.cshtml"
       Write(Html.DisplayFor(model => model.DateSoumission));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-4\">\r\n            ");
#nullable restore
#line 51 "C:\Users\Ines\Desktop\HotlineWebApp\Tryingtofix\Hotline\Views\ReclamationsUser\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Statut));

#line default
#line hidden
#nullable disable
            WriteLiteral(" :\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 54 "C:\Users\Ines\Desktop\HotlineWebApp\Tryingtofix\Hotline\Views\ReclamationsUser\Details.cshtml"
       Write(Html.DisplayFor(model => model.Statut));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-4\">\r\n            ");
#nullable restore
#line 57 "C:\Users\Ines\Desktop\HotlineWebApp\Tryingtofix\Hotline\Views\ReclamationsUser\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.DateAffectation));

#line default
#line hidden
#nullable disable
            WriteLiteral(" :\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 60 "C:\Users\Ines\Desktop\HotlineWebApp\Tryingtofix\Hotline\Views\ReclamationsUser\Details.cshtml"
       Write(Html.DisplayFor(model => model.DateAffectation));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-4\">\r\n            ");
#nullable restore
#line 63 "C:\Users\Ines\Desktop\HotlineWebApp\Tryingtofix\Hotline\Views\ReclamationsUser\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.DateResolution));

#line default
#line hidden
#nullable disable
            WriteLiteral(" :\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 66 "C:\Users\Ines\Desktop\HotlineWebApp\Tryingtofix\Hotline\Views\ReclamationsUser\Details.cshtml"
       Write(Html.DisplayFor(model => model.DateResolution));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n        <dt class=\"col-sm-4\">\r\n            ");
#nullable restore
#line 69 "C:\Users\Ines\Desktop\HotlineWebApp\Tryingtofix\Hotline\Views\ReclamationsUser\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Solution));

#line default
#line hidden
#nullable disable
            WriteLiteral(" :\r\n        </dt>\r\n        <dd class=\"col-sm-10\">\r\n            ");
#nullable restore
#line 72 "C:\Users\Ines\Desktop\HotlineWebApp\Tryingtofix\Hotline\Views\ReclamationsUser\Details.cshtml"
       Write(Html.DisplayFor(model => model.Solution));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </dd>\r\n    </dl>\r\n</div>\r\n\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Hotline.Models.Reclamation> Html { get; private set; }
    }
}
#pragma warning restore 1591
