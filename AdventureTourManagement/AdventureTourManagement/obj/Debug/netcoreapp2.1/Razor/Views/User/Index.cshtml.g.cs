#pragma checksum "\\Mac\Home\Documents\Project_final\AdventureTourManagement\AdventureTourManagement\AdventureTourManagement\Views\User\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d8e452d50eaae088807387461967199555563034"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_User_Index), @"mvc.1.0.view", @"/Views/User/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/User/Index.cshtml", typeof(AspNetCore.Views_User_Index))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d8e452d50eaae088807387461967199555563034", @"/Views/User/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e29496f4b743106e96e23c39fb9570612c73476d", @"/Views/_ViewImports.cshtml")]
    public class Views_User_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<AdventureTourManagement.ViewModels.VmUser>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "\\Mac\Home\Documents\Project_final\AdventureTourManagement\AdventureTourManagement\AdventureTourManagement\Views\User\Index.cshtml"
  
    ViewData["Title"] = "Index";
    Layout = "~/Views/Shared/Master.cshtml";

#line default
#line hidden
            BeginContext(137, 20, true);
            WriteLiteral("\r\n<h2>Index</h2>\r\n\r\n");
            EndContext();
#line 9 "\\Mac\Home\Documents\Project_final\AdventureTourManagement\AdventureTourManagement\AdventureTourManagement\Views\User\Index.cshtml"
 using (Html.BeginForm("LoginUser", "User", FormMethod.Post))
{

#line default
#line hidden
            BeginContext(223, 203, true);
            WriteLiteral("    <div class=\"row\">\r\n        <div class=\"col-md-12\">\r\n            Sign In\r\n        </div>\r\n    </div>\r\n    <div class=\"row\">\r\n        <div class=\"col-md-6\">\r\n            <label>Email ID</label> &nbsp; ");
            EndContext();
            BeginContext(427, 31, false);
#line 18 "\\Mac\Home\Documents\Project_final\AdventureTourManagement\AdventureTourManagement\AdventureTourManagement\Views\User\Index.cshtml"
                                      Write(Html.TextBoxFor(x=>x.UserEmail));

#line default
#line hidden
            EndContext();
            BeginContext(458, 95, true);
            WriteLiteral("\r\n        </div>\r\n\r\n        <div class=\"col-md-6\">\r\n            <label>Password</label> &nbsp; ");
            EndContext();
            BeginContext(554, 42, false);
#line 22 "\\Mac\Home\Documents\Project_final\AdventureTourManagement\AdventureTourManagement\AdventureTourManagement\Views\User\Index.cshtml"
                                      Write(Html.PasswordFor(x => x.UserEncyryptedKey));

#line default
#line hidden
            EndContext();
            BeginContext(596, 167, true);
            WriteLiteral("\r\n        </div>\r\n    </div>\r\n    <div class=\"row\">\r\n        <div class=\"col-md-6\">\r\n            <button type=\"submit\">Login</button>\r\n            &nbsp;\r\n            ");
            EndContext();
            BeginContext(764, 106, false);
#line 29 "\\Mac\Home\Documents\Project_final\AdventureTourManagement\AdventureTourManagement\AdventureTourManagement\Views\User\Index.cshtml"
       Write(Html.ActionLink("Forgot Password","ForgotPassword","User",null, new { @class = "btn btn-primary btn-xs" }));

#line default
#line hidden
            EndContext();
            BeginContext(870, 61, true);
            WriteLiteral("\r\n        </div>\r\n        <div class=\"col-md-6\">\r\n           ");
            EndContext();
            BeginContext(932, 101, false);
#line 32 "\\Mac\Home\Documents\Project_final\AdventureTourManagement\AdventureTourManagement\AdventureTourManagement\Views\User\Index.cshtml"
      Write(Html.ActionLink("New User", "RegisterNewUserView","User",null,new { @class="btn btn-primary btn-xs"}));

#line default
#line hidden
            EndContext();
            BeginContext(1033, 98, true);
            WriteLiteral("\r\n        </div>\r\n    </div>\r\n    <div class=\"row\">\r\n        <div class=\"col-md-12\">\r\n            ");
            EndContext();
            BeginContext(1132, 112, false);
#line 37 "\\Mac\Home\Documents\Project_final\AdventureTourManagement\AdventureTourManagement\AdventureTourManagement\Views\User\Index.cshtml"
       Write(Html.ActionLink("Continue as guest", "Index", "GuestDashboard", null, new { @class = "btn btn-primary btn-xs" }));

#line default
#line hidden
            EndContext();
            BeginContext(1244, 30, true);
            WriteLiteral("\r\n        </div>\r\n    </div>\r\n");
            EndContext();
#line 40 "\\Mac\Home\Documents\Project_final\AdventureTourManagement\AdventureTourManagement\AdventureTourManagement\Views\User\Index.cshtml"
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<AdventureTourManagement.ViewModels.VmUser> Html { get; private set; }
    }
}
#pragma warning restore 1591