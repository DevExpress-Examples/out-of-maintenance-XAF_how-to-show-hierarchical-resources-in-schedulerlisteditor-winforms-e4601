Imports Microsoft.VisualBasic
Imports System

Imports DevExpress.ExpressApp
Imports DevExpress.ExpressApp.Updating
Imports DevExpress.Xpo
Imports DevExpress.Data.Filtering
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.ExpressApp.Security
Imports System.Collections.Generic

Namespace WinSolution.Module.DatabaseUpdate
	Public Class Updater
		Inherits ModuleUpdater
		Public Sub New(ByVal objectSpace As IObjectSpace, ByVal currentDBVersion As Version)
			MyBase.New(objectSpace, currentDBVersion)
		End Sub
		Public Overrides Sub UpdateDatabaseAfterUpdateSchema()
			MyBase.UpdateDatabaseAfterUpdateSchema()

			If ObjectSpace.FindObject(Of HierarchicalResource)(Nothing) Is Nothing Then
				Dim list As New List(Of HierarchicalResource)()
				Dim num As Integer = 12
				Dim rnd As New Random()
				For i As Integer = 0 To num - 1
					Dim r As HierarchicalResource = ObjectSpace.CreateObject(Of HierarchicalResource)()
					r.Name = String.Format("Resource{0}", i)
					r.Color = GetColor(rnd.NextDouble(), rnd.NextDouble(), rnd.NextDouble())
					If list.Count > 0 Then
						r.Parent = list(rnd.Next(list.Count))
					End If
					list.Add(r)
				Next i
			End If
		End Sub
		Private Function GetColor(ByVal r As Double, ByVal g As Double, ByVal b As Double) As Integer
			Dim l As Double = (r + g + b) / 3
			Dim mag As Integer = 127, [off] As Integer = 128
			Return System.Drawing.Color.FromArgb(NormColor(r * l, mag, [off]), NormColor(g * l, mag, [off]), NormColor(b * l, mag, [off])).ToArgb()
		End Function
		Private Function NormColor(ByVal i As Double, ByVal mag As Integer, ByVal [off] As Integer) As Integer
			Return CInt(Fix(i * mag + [off]))
		End Function
	End Class
End Namespace
