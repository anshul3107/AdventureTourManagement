#pragma checksum "\\Mac\Home\Documents\Project_final\AdventureTourManagement\AdventureTourManagement\AdventureTourManagement\Views\User\LoginUser.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "b266d673e63533748c312e58cd6c1e69c837cf76"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_User_LoginUser), @"mvc.1.0.view", @"/Views/User/LoginUser.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/User/LoginUser.cshtml", typeof(AspNetCore.Views_User_LoginUser))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b266d673e63533748c312e58cd6c1e69c837cf76", @"/Views/User/LoginUser.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e29496f4b743106e96e23c39fb9570612c73476d", @"/Views/_ViewImports.cshtml")]
    public class Views_User_LoginUser : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<AdventureTourManagement.ViewModels.VmUser>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "\\Mac\Home\Documents\Project_final\AdventureTourManagement\AdventureTourManagement\AdventureTourManagement\Views\User\LoginUser.cshtml"
  
    ViewData["Title"] = "LoginUser";
    Layout = "~/Views/Shared/Master.cshtml";

#line default
#line hidden
            BeginContext(141, 96, true);
            WriteLiteral("\r\n<table>\r\n    <tr>\r\n        <td>\r\n            Name :\r\n        </td>\r\n        <td>\r\n            ");
            EndContext();
            BeginContext(238, 26, false);
#line 13 "\\Mac\Home\Documents\Project_final\AdventureTourManagement\AdventureTourManagement\AdventureTourManagement\Views\User\LoginUser.cshtml"
       Write(Html.Label(Model.UserName));

#line default
#line hidden
            EndContext();
            BeginContext(264, 121, true);
            WriteLiteral("\r\n        </td>\r\n    </tr>\r\n    <tr>\r\n        <td>\r\n            Email :\r\n        </td>\r\n        <td>\r\n            <label>");
            EndContext();
            BeginContext(386, 15, false);
#line 21 "\\Mac\Home\Documents\Project_final\AdventureTourManagement\AdventureTourManagement\AdventureTourManagement\Views\User\LoginUser.cshtml"
              Write(Model.UserEmail);

#line default
#line hidden
            EndContext();
            BeginContext(401, 124, true);
            WriteLiteral("</label>\r\n        </td>\r\n    </tr>\r\n    <tr>\r\n        <td>\r\n            Contact :\r\n        </td>\r\n        <td>\r\n            ");
            EndContext();
            BeginContext(526, 29, false);
#line 29 "\\Mac\Home\Documents\Project_final\AdventureTourManagement\AdventureTourManagement\AdventureTourManagement\Views\User\LoginUser.cshtml"
       Write(Html.Label(Model.UserContact));

#line default
#line hidden
            EndContext();
            BeginContext(555, 91, true);
            WriteLiteral("\r\n        </td>\r\n    </tr>\r\n</table>\r\n<br />\r\n<table>\r\n    <tr>\r\n        <td>\r\n            ");
            EndContext();
            BeginContext(647, 102, false);
#line 37 "\\Mac\Home\Documents\Project_final\AdventureTourManagement\AdventureTourManagement\AdventureTourManagement\Views\User\LoginUser.cshtml"
       Write(Html.ActionLink("Update Details", "UpdateUserDetailView", "User", new { userEmail = Model.UserEmail }));

#line default
#line hidden
            EndContext();
            BeginContext(749, 35, true);
            WriteLiteral("\r\n        </td>\r\n        <td>\r\n    ");
            EndContext();
            BeginContext(785, 105, false);
#line 40 "\\Mac\Home\Documents\Project_final\AdventureTourManagement\AdventureTourManagement\AdventureTourManagement\Views\User\LoginUser.cshtml"
Write(Html.ActionLink("Change Password", "UpdateUserPasswordView", "User", new { userEmail = Model.UserEmail }));

#line default
#line hidden
            EndContext();
            BeginContext(890, 43, true);
            WriteLiteral("\r\n        </td>\r\n        <td>\r\n            ");
            EndContext();
            BeginContext(934, 102, false);
#line 43 "\\Mac\Home\Documents\Project_final\AdventureTourManagement\AdventureTourManagement\AdventureTourManagement\Views\User\LoginUser.cshtml"
       Write(Html.ActionLink("View Booking History", "GetBookingHistory","User",new { userEmail = Model.UserEmail}));

#line default
#line hidden
            EndContext();
            BeginContext(1036, 40, true);
            WriteLiteral("\r\n        </td>\r\n    </tr>\r\n</table>\r\n\r\n");
            EndContext();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<AdventureTourManagement.ViewModels.VmUser> Html { get; private set; }
    }
}
#pragma warning restore 1591
