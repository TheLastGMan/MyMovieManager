Imports System.Web.Routing

Public Interface IRouteProvider

    Sub RegisterRoutes(ByRef routes As RouteCollection)
    ReadOnly Property Priority As Integer

End Interface
