Imports System.Web.Hosting

' Note: For instructions on enabling IIS6 or IIS7 classic mode, 
' visit http://go.microsoft.com/?LinkId=9394802

Public Class MvcApplication
    Inherits System.Web.HttpApplication

    Shared Sub RegisterGlobalFilters(ByVal filters As GlobalFilterCollection)
        filters.Add(New HandleErrorAttribute())
    End Sub

    Shared Sub RegisterRoutes(ByVal routes As RouteCollection)
        routes.IgnoreRoute("{resource}.axd/{*pathInfo}")
        routes.IgnoreRoute("favicon.ico")

        Dim rp As New Services.RoutePublisher()
        rp.RegisterRoutes(routes)

        ' MapRoute takes the following parameters, in order:
        ' (1) Route name
        ' (2) URL with parameters
        ' (3) Parameter defaults
        routes.MapRoute( _
            "Default", _
            "{controller}/{action}/{id}", _
            New With {.controller = "Home", .action = "Index", .id = UrlParameter.Optional} _
        )

    End Sub

    Sub Application_Start()
        Dim Logs As New Core.Repository.Logging
        Logs.AddMessage("MMM", "Global: Application_Start")

        AreaRegistration.RegisterAllAreas()

        RegisterGlobalFilters(GlobalFilters.Filters)
        RegisterRoutes(RouteTable.Routes)

        Dim embeddedViewResolver = New Services.EmbeddedViewResolver
        Dim embeddedProvider = New Services.EmbeddedViewVirtualPathProvider(embeddedViewResolver.GetEmbeddedViews)
        HostingEnvironment.RegisterVirtualPathProvider(embeddedProvider)

        Dim TM As New Services.TaskManager()
        TM.Init()

        Logs.AddMessage("MMM", "Global: Loaded Tasks: " & Services.TaskManager.Tasks.Count() & " | Clocks: " & Services.TaskManager.Clocks.Count())
    End Sub

    Sub Application_Error()
        Dim Logs As New Core.Repository.Logging
        Dim lm = HttpContext.Current.Server.GetLastError()
        Logs.AddMessage("MMM", "Global Error: " & lm.Message & vbCrLf & "Stack: " & lm.StackTrace)
    End Sub

    Sub Application_End()
        Dim Logs As New Core.Repository.Logging
        Dim TM As New Services.TaskManager()
        TM.Stop()
        Logs.AddMessage("MMM", "Application_End")
    End Sub

End Class