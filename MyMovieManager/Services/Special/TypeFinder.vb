Imports System.Reflection
Imports System.Text.RegularExpressions

Public Class TypeFinder

    Private ReadOnly loadAppDomainAssemblies As Boolean = True
    Private assemblySkipLoadingPattern As String = "^System|^mscorlib|^Microsoft|^CppCodeProvider|^VJSharpCodeProvider|^VBCodeProvider|^WebDev|^EntityFramework|^MvcContrib|^AjaxControlToolkit|^SMDiagnostics"
    Private assemblyRestrictToLoadingPattern As String = ".*"
    Private assemblyNames As New List(Of String)

    Private ReadOnly Property App As AppDomain
        Get
            Return AppDomain.CurrentDomain
        End Get
    End Property

    Public Function FindClassesOfType(Of T)() As List(Of Type)
        Return FindClassesOfType(GetType(T), getAssemblies())
    End Function

    Public Function FindClassesOfType(Of T)(ByRef Assembly As Assembly) As Type
        Dim alst As New List(Of Assembly)
        alst.Add(Assembly)
        Dim lst As List(Of Type) = FindClassesOfType(GetType(T), alst)
        If lst.Count > 0 Then
            Return lst(0)
        Else
            Return Nothing
        End If
    End Function

    Public Function FindClassesOfType(ByRef Type As Type, ByRef assembiles As List(Of Assembly)) As List(Of Type)
        Dim result As New List(Of Type)

        Try
            'loop through each assembly
            For Each a In assembiles
                'go through each type in assembly
                For Each t In a.GetTypes
                    'check if it is the type we are looking for
                    If Type.IsAssignableFrom(t) Then
                        'check if the type is not an interface class
                        If Not t.IsInterface Then
                            'check if it is a solid class
                            If t.IsClass And Not t.IsAbstract Then
                                result.Add(t)
                            End If
                        End If
                    End If
                Next
            Next
        Catch ex As ReflectionTypeLoadException
            Dim msg As New Text.StringBuilder()
            For Each e In ex.LoaderExceptions
                msg.AppendLine(e.Message)
            Next

            Dim fail As New Exception(msg.ToString, ex)
            Debug.WriteLine(fail.Message, fail)

            Throw fail

        End Try

        Return result
    End Function

    Public Function getAssemblies() As List(Of Assembly)
        Dim addedAssemblyNames As New List(Of String)
        Dim assemblies As New List(Of Assembly)

        If loadAppDomainAssemblies Then
            'add assemblies
            AddAssembliesInAppDomain(addedAssemblyNames, assemblies)
        End If

        Return assemblies
    End Function

    Private Sub AddAssembliesInAppDomain(ByRef addedAssembyNames As List(Of String), ByRef assemblies As List(Of Assembly))
        'loop through all assemblies in Current AppDomain
        For Each Assembly As Assembly In App.GetAssemblies
            'check if it matches our criteria
            If Matches(Assembly.FullName) Then
                'check if it does not already exist in our list
                If Not addedAssembyNames.Contains(Assembly.FullName) Then
                    assemblies.Add(Assembly)
                    addedAssembyNames.Add(Assembly.FullName)
                End If
            End If
        Next
    End Sub

    Private Function Matches(ByRef assemblyFullName As String) As Boolean
        Return Not Matches(assemblyFullName, assemblySkipLoadingPattern) And Matches(assemblyFullName, assemblyRestrictToLoadingPattern)
    End Function

    Private Function Matches(ByRef assemblyFullName As String, ByRef pattern As String) As Boolean
        Return Regex.IsMatch(assemblyFullName, pattern, RegexOptions.IgnoreCase Or RegexOptions.Compiled)
    End Function

    Private Sub LoadMatchingAssemblies(ByRef directoryPath As String)
        Dim loadedAssemblyNames As New List(Of String)

        For Each a As Assembly In getAssemblies()
            loadedAssemblyNames.Add(a.FullName)
        Next

        If Not IO.Directory.Exists(directoryPath) Then
            Exit Sub
        End If

        For Each dllPath As String In IO.Directory.GetFiles(directoryPath, "*.dll")
            Try
                Dim an = AssemblyName.GetAssemblyName(dllPath)
                If Matches(an.FullName) And Not loadedAssemblyNames.Contains(an.FullName) Then
                    App.Load(an)
                End If
            Catch ex As Exception
                Trace.TraceError(ex.ToString)
            End Try
        Next
    End Sub

End Class
