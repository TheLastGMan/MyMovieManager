Public Class LogTask
    Inherits Repository.Logging : Implements Services.ITask

    Public ReadOnly Property ExecMins As Short Implements Services.ITask.ExecMins
        Get
            Return 360
        End Get
    End Property

    Public Sub Execute() Implements Services.ITask.Execute
        DeleteOldLogs(Now.AddDays(-3))
        AddMessage("MMM", "LogTask Ran")
    End Sub

End Class
