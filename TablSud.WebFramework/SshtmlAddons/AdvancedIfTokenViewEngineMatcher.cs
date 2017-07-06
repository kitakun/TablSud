using System;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using Nancy;
using Nancy.Owin;
using Nancy.ViewEngines.SuperSimpleViewEngine;
using TablSud.Services.Extensions;

namespace TablSud.WebFramework.SshtmlAddons
{
    public class AdvancedIfTokenViewEngineMatcher : ISuperSimpleViewEngineMatcher
    {
        /// <summary>
        /// Compiled Regex for accessing user object
        /// </summary>
        private static readonly Regex IfRegEx;

        static AdvancedIfTokenViewEngineMatcher()
        {
            // This regex will match strings like:
            // @User.Name
            IfRegEx = new Regex("(If(\")(?:(?<ParameterName>[a-zA-Z0-9-_.]+))(\"))", RegexOptions.Compiled);
        }

        public string Invoke(string content, dynamic model, IViewEngineHost host)
        {
            MatchCollection reslt = IfRegEx.Matches(content);
            if (reslt.Count > 0)
            {
                StringBuilder rdyContent = new StringBuilder(content);
                foreach (Match match in reslt)
                {

                    string rawRequest = match.Value;
                    string[] reqs = rawRequest.Split('.');
                    NancyContext context = host.Context as NancyContext;

                    
                    ClaimsPrincipal principial = context.CurrentUser;
                    object currentObject = principial.GetUser();
                    if (currentObject != null)
                    {
                        bool success = false;
                        if (reqs.Length > 0 && reqs[0] == "@User" && reqs.Length > 1)
                        {
                            for (int i = 1; i < reqs.Length; i++)
                            {
                                string curTarget = reqs[i];
                                if (curTarget.Contains("()"))
                                {
                                    MethodInfo targetMethod =
                                        currentObject.GetType().GetMethod(curTarget.Replace("()", ""));
                                    if (targetMethod != null)
                                    {
                                        success = i == reqs.Length - 1;
                                        currentObject = targetMethod.Invoke(currentObject, null);
                                    }
                                    else break;
                                }
                                else
                                {
                                    PropertyInfo targetProperty = currentObject.GetType().GetProperty(curTarget);
                                    if (targetProperty != null)
                                    {
                                        success = i == reqs.Length - 1;
                                        currentObject = targetProperty.GetValue(currentObject);
                                    }
                                    else break;
                                }
                            }
                        }
                        if (success)
                        {
                            rdyContent.Replace(rawRequest, currentObject.ToString());
                        }
                        else
                        {
                            rdyContent.Replace(rawRequest, "[ERR]");
                        }
                    }
                    else
                    {
                        rdyContent.Replace(rawRequest, "[null user]");
                    }
                }
                return rdyContent.ToString();
            }
            return content;
        }
    }
}
