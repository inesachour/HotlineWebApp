#pragma checksum "C:\Users\Ines\Desktop\HotlineWebApp\Tryingtofix\Hotline\Views\Authentication\Denied.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "bad7f18c25d6bbe47bb63f78b5b70d767be1a7e9"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Authentication_Denied), @"mvc.1.0.view", @"/Views/Authentication/Denied.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"bad7f18c25d6bbe47bb63f78b5b70d767be1a7e9", @"/Views/Authentication/Denied.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ec4a2a600302c4d57296472fe2ab22f4b39af785", @"/Views/_ViewImports.cshtml")]
    public class Views_Authentication_Denied : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 7 "C:\Users\Ines\Desktop\HotlineWebApp\Tryingtofix\Hotline\Views\Authentication\Denied.cshtml"
 if (ViewData["accessError"] != null)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <p class=\"alert\">");
#nullable restore
#line 9 "C:\Users\Ines\Desktop\HotlineWebApp\Tryingtofix\Hotline\Views\Authentication\Denied.cshtml"
                Write(ViewData["error"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n");
#nullable restore
#line 10 "C:\Users\Ines\Desktop\HotlineWebApp\Tryingtofix\Hotline\Views\Authentication\Denied.cshtml"
}
else
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <p class=\"alert\"> Acces Interdit </p>\r\n");
#nullable restore
#line 14 "C:\Users\Ines\Desktop\HotlineWebApp\Tryingtofix\Hotline\Views\Authentication\Denied.cshtml"
}

#line default
#line hidden
#nullable disable
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
