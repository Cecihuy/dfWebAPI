using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;

namespace EmployeeService2.Controllers.Custom
{
  public class CustomControllerSelector : DefaultHttpControllerSelector {
    private HttpConfiguration _configuration;
    public CustomControllerSelector(HttpConfiguration configuration) : base(configuration){
      this._configuration = configuration;
    }
    public override HttpControllerDescriptor SelectController(HttpRequestMessage request) {
      var controllers = GetControllerMapping();
      var routeData = request.GetRouteData();
      var controllerName = routeData.Values["controller"].ToString();
      string versionNumber = "1";
      //var versionQueryString = HttpUtility.ParseQueryString(request.RequestUri.Query);
      //if(versionQueryString["v"] != null) {
      //  versionNumber = versionQueryString["v"];
      //}
      //string customHeader = "give-it-name-you-want";
      //if(request.Headers.Contains(customHeader)) {
      //  versionNumber = request.Headers.GetValues(customHeader).FirstOrDefault();
      //  if(versionNumber.Contains(",")) {
      //    versionNumber = versionNumber.Substring(0, versionNumber.IndexOf(","));
      //  }
      //}
      var acceptHeader = request.Headers.Accept.Where(
        a => a.Parameters.Count(p => p.Name == "version") > 0
      );
      if(acceptHeader.Any()) {
        versionNumber = acceptHeader.First().Parameters.First(p => p.Name.ToLower() == "version").Value;
      }
      if(versionNumber == "1") {
        controllerName = controllerName + "v1";
      } else {
        controllerName = controllerName + "v2";
      }
      HttpControllerDescriptor controllerDescriptor;
      if(controllers.TryGetValue(controllerName, out controllerDescriptor)) {
        return controllerDescriptor;
      }
      return null;
    }
  }
}
