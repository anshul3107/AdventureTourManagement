#pragma checksum "\\Mac\Home\Documents\Project_final\AdventureTourManagement\AdventureTourManagement\AdventureTourManagement\Views\Shop\GetUserDetails.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8b58d0adb67bbd5df1dc3594313636828575aa56"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shop_GetUserDetails), @"mvc.1.0.view", @"/Views/Shop/GetUserDetails.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Shop/GetUserDetails.cshtml", typeof(AspNetCore.Views_Shop_GetUserDetails))]
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
#line 1 "\\Mac\Home\Documents\Project_final\AdventureTourManagement\AdventureTourManagement\AdventureTourManagement\Views\_ViewImports.cshtml"
using AdventureTourManagement;

#line default
#line hidden
#line 2 "\\Mac\Home\Documents\Project_final\AdventureTourManagement\AdventureTourManagement\AdventureTourManagement\Views\_ViewImports.cshtml"
using AdventureTourManagement.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"8b58d0adb67bbd5df1dc3594313636828575aa56", @"/Views/Shop/GetUserDetails.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e29496f4b743106e96e23c39fb9570612c73476d", @"/Views/_ViewImports.cshtml")]
    public class Views_Shop_GetUserDetails : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<AdventureTourManagement.ViewModels.VMUserDetail>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "\\Mac\Home\Documents\Project_final\AdventureTourManagement\AdventureTourManagement\AdventureTourManagement\Views\Shop\GetUserDetails.cshtml"
  
    ViewBag.Title = "GetUserDetails";
    Layout = "~/Views/Shared/Master.cshtml";

#line default
#line hidden
            BeginContext(148, 27, true);
            WriteLiteral("\r\n<h2>User Details</h2>\r\n\r\n");
            EndContext();
#line 9 "\\Mac\Home\Documents\Project_final\AdventureTourManagement\AdventureTourManagement\AdventureTourManagement\Views\Shop\GetUserDetails.cshtml"
  
    if (!Model.IsToken)
    {
        

#line default
#line hidden
#line 12 "\\Mac\Home\Documents\Project_final\AdventureTourManagement\AdventureTourManagement\AdventureTourManagement\Views\Shop\GetUserDetails.cshtml"
         using (Html.BeginForm("AuthenticateUserEmail", "Shop", FormMethod.Post))
        {


#line default
#line hidden
            BeginContext(307, 35, true);
            WriteLiteral("            <div>\r\n                ");
            EndContext();
            BeginContext(343, 19, false);
#line 16 "\\Mac\Home\Documents\Project_final\AdventureTourManagement\AdventureTourManagement\AdventureTourManagement\Views\Shop\GetUserDetails.cshtml"
           Write(Html.Label("Email"));

#line default
#line hidden
            EndContext();
            BeginContext(362, 18, true);
            WriteLiteral("\r\n                ");
            EndContext();
            BeginContext(381, 34, false);
#line 17 "\\Mac\Home\Documents\Project_final\AdventureTourManagement\AdventureTourManagement\AdventureTourManagement\Views\Shop\GetUserDetails.cshtml"
           Write(Html.TextBoxFor(x => x.user_email));

#line default
#line hidden
            EndContext();
            BeginContext(415, 132, true);
            WriteLiteral("\r\n\r\n                <button class=\"btn btn-info btn-activity\" type=\"submit\">Send Verification Email</button>\r\n\r\n            </div>\r\n");
            EndContext();
#line 22 "\\Mac\Home\Documents\Project_final\AdventureTourManagement\AdventureTourManagement\AdventureTourManagement\Views\Shop\GetUserDetails.cshtml"
        }

#line default
#line hidden
#line 22 "\\Mac\Home\Documents\Project_final\AdventureTourManagement\AdventureTourManagement\AdventureTourManagement\Views\Shop\GetUserDetails.cshtml"
         
    }
    else
    {
        

#line default
#line hidden
#line 26 "\\Mac\Home\Documents\Project_final\AdventureTourManagement\AdventureTourManagement\AdventureTourManagement\Views\Shop\GetUserDetails.cshtml"
         using (Html.BeginForm("VerifyTokenAsync", "Shop", FormMethod.Post))
        {

#line default
#line hidden
            BeginContext(671, 13, true);
            WriteLiteral("<div>\r\n\r\n    ");
            EndContext();
            BeginContext(685, 33, false);
#line 30 "\\Mac\Home\Documents\Project_final\AdventureTourManagement\AdventureTourManagement\AdventureTourManagement\Views\Shop\GetUserDetails.cshtml"
Write(Html.HiddenFor(x => x.userAuthID));

#line default
#line hidden
            EndContext();
            BeginContext(718, 6, true);
            WriteLiteral("\r\n    ");
            EndContext();
            BeginContext(725, 33, false);
#line 31 "\\Mac\Home\Documents\Project_final\AdventureTourManagement\AdventureTourManagement\AdventureTourManagement\Views\Shop\GetUserDetails.cshtml"
Write(Html.HiddenFor(x => x.user_email));

#line default
#line hidden
            EndContext();
            BeginContext(758, 6, true);
            WriteLiteral("\r\n    ");
            EndContext();
            BeginContext(765, 17, false);
#line 32 "\\Mac\Home\Documents\Project_final\AdventureTourManagement\AdventureTourManagement\AdventureTourManagement\Views\Shop\GetUserDetails.cshtml"
Write(Html.Label("OTP"));

#line default
#line hidden
            EndContext();
            BeginContext(782, 6, true);
            WriteLiteral("\r\n    ");
            EndContext();
            BeginContext(789, 29, false);
#line 33 "\\Mac\Home\Documents\Project_final\AdventureTourManagement\AdventureTourManagement\AdventureTourManagement\Views\Shop\GetUserDetails.cshtml"
Write(Html.TextBoxFor(x => x.Token));

#line default
#line hidden
            EndContext();
            BeginContext(818, 91, true);
            WriteLiteral("\r\n\r\n    <button class=\"btn btn-info btn-activity\" type=\"submit\">Submit</button>\r\n\r\n</div>\r\n");
            EndContext();
#line 38 "\\Mac\Home\Documents\Project_final\AdventureTourManagement\AdventureTourManagement\AdventureTourManagement\Views\Shop\GetUserDetails.cshtml"
        }

#line default
#line hidden
#line 38 "\\Mac\Home\Documents\Project_final\AdventureTourManagement\AdventureTourManagement\AdventureTourManagement\Views\Shop\GetUserDetails.cshtml"
         
        }

#line default
#line hidden
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<AdventureTourManagement.ViewModels.VMUserDetail> Html { get; private set; }
    }
}
#pragma warning restore 1591
