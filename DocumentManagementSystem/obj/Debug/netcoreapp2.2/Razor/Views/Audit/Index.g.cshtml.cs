#pragma checksum "C:\Users\Tossen Macs\Desktop\New folder (2)\Telnet\DocumentManagementSystem\DocumentManagementSystem\Views\Audit\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "eea4ba70f774985f36a7168fde704dfb69dd3de2"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Audit_Index), @"mvc.1.0.view", @"/Views/Audit/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Audit/Index.cshtml", typeof(AspNetCore.Views_Audit_Index))]
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
#line 1 "C:\Users\Tossen Macs\Desktop\New folder (2)\Telnet\DocumentManagementSystem\DocumentManagementSystem\Views\_ViewImports.cshtml"
using DocumentManagementSystem;

#line default
#line hidden
#line 2 "C:\Users\Tossen Macs\Desktop\New folder (2)\Telnet\DocumentManagementSystem\DocumentManagementSystem\Views\_ViewImports.cshtml"
using DocumentManagementSystem.Models;

#line default
#line hidden
#line 3 "C:\Users\Tossen Macs\Desktop\New folder (2)\Telnet\DocumentManagementSystem\DocumentManagementSystem\Views\_ViewImports.cshtml"
using DocumentManagementSystem.ViewModels;

#line default
#line hidden
#line 4 "C:\Users\Tossen Macs\Desktop\New folder (2)\Telnet\DocumentManagementSystem\DocumentManagementSystem\Views\_ViewImports.cshtml"
using DocumentManagementSystem.Data;

#line default
#line hidden
#line 5 "C:\Users\Tossen Macs\Desktop\New folder (2)\Telnet\DocumentManagementSystem\DocumentManagementSystem\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"eea4ba70f774985f36a7168fde704dfb69dd3de2", @"/Views/Audit/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b396e8c565a6a1b84f1b1c7395ad595ad34f7c3e", @"/Views/_ViewImports.cshtml")]
    public class Views_Audit_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<AuditRailViewModel>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(40, 209, true);
            WriteLiteral("<section class=\"login-content\" style=\"margin-top:-150px\">\r\n    <div>\r\n        <table class=\"table bg-light\">\r\n            <thead>\r\n                <tr>\r\n                    <th>\r\n                        Name\r\n");
            EndContext();
            BeginContext(415, 170, true);
            WriteLiteral("                    </th>\r\n                    <th>\r\n                        Action\r\n                    </th>\r\n                    <th>\r\n                        Remark\r\n");
            EndContext();
            BeginContext(757, 352, true);
            WriteLiteral(@"                    </th>
                    <th>
                        Web Page
                    </th>
                    <th>
                        Ip Address
                    </th>
                    <th>
                        Date
                    </th>
                </tr>
            </thead>
            <tbody>
");
            EndContext();
#line 30 "C:\Users\Tossen Macs\Desktop\New folder (2)\Telnet\DocumentManagementSystem\DocumentManagementSystem\Views\Audit\Index.cshtml"
                 foreach (var item in Model)
                {

#line default
#line hidden
            BeginContext(1174, 84, true);
            WriteLiteral("                    <tr>\r\n                        <td>\r\n                            ");
            EndContext();
            BeginContext(1259, 43, false);
#line 34 "C:\Users\Tossen Macs\Desktop\New folder (2)\Telnet\DocumentManagementSystem\DocumentManagementSystem\Views\Audit\Index.cshtml"
                       Write(Html.DisplayFor(modelItem => item.UserName));

#line default
#line hidden
            EndContext();
            BeginContext(1302, 91, true);
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
            EndContext();
            BeginContext(1394, 45, false);
#line 37 "C:\Users\Tossen Macs\Desktop\New folder (2)\Telnet\DocumentManagementSystem\DocumentManagementSystem\Views\Audit\Index.cshtml"
                       Write(Html.DisplayFor(modelItem => item.ActionName));

#line default
#line hidden
            EndContext();
            BeginContext(1439, 91, true);
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
            EndContext();
            BeginContext(1531, 41, false);
#line 40 "C:\Users\Tossen Macs\Desktop\New folder (2)\Telnet\DocumentManagementSystem\DocumentManagementSystem\Views\Audit\Index.cshtml"
                       Write(Html.DisplayFor(modelItem => item.Remark));

#line default
#line hidden
            EndContext();
            BeginContext(1572, 91, true);
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
            EndContext();
            BeginContext(1664, 42, false);
#line 43 "C:\Users\Tossen Macs\Desktop\New folder (2)\Telnet\DocumentManagementSystem\DocumentManagementSystem\Views\Audit\Index.cshtml"
                       Write(Html.DisplayFor(modelItem => item.WebPage));

#line default
#line hidden
            EndContext();
            BeginContext(1706, 91, true);
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
            EndContext();
            BeginContext(1798, 44, false);
#line 46 "C:\Users\Tossen Macs\Desktop\New folder (2)\Telnet\DocumentManagementSystem\DocumentManagementSystem\Views\Audit\Index.cshtml"
                       Write(Html.DisplayFor(modelItem => item.IpAddress));

#line default
#line hidden
            EndContext();
            BeginContext(1842, 91, true);
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            ");
            EndContext();
            BeginContext(1934, 46, false);
#line 49 "C:\Users\Tossen Macs\Desktop\New folder (2)\Telnet\DocumentManagementSystem\DocumentManagementSystem\Views\Audit\Index.cshtml"
                       Write(Html.DisplayFor(modelItem => item.DateCreated));

#line default
#line hidden
            EndContext();
            BeginContext(1980, 33, true);
            WriteLiteral("\r\n                        </td>\r\n");
            EndContext();
            BeginContext(2357, 27, true);
            WriteLiteral("                    </tr>\r\n");
            EndContext();
#line 57 "C:\Users\Tossen Macs\Desktop\New folder (2)\Telnet\DocumentManagementSystem\DocumentManagementSystem\Views\Audit\Index.cshtml"
                }

#line default
#line hidden
            BeginContext(2403, 62, true);
            WriteLiteral("            </tbody>\r\n        </table>\r\n    </div>\r\n</section>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<AuditRailViewModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591
