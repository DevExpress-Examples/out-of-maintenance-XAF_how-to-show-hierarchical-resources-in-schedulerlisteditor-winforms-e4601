Imports Microsoft.VisualBasic
Imports System.ComponentModel
Imports DevExpress.Persistent.Base
Imports DevExpress.Persistent.Base.General
Imports DevExpress.Persistent.BaseImpl
Imports DevExpress.Xpo

Namespace WinSolution.Module

	<NavigationItem> _
	Public Class HierarchicalResource
		Inherits BaseObject
		Implements ITreeNode, IResource, ITreeResource

		Private _Name As String
		Private _Parent As HierarchicalResource
		Private _Color As Integer

		Public Sub New(ByVal s As Session)
			MyBase.New(s)
		End Sub

		Public Property Name() As String
			Get
				Return _Name
			End Get
			Set(ByVal value As String)
				SetPropertyValue("Name", _Name, value)
			End Set
		End Property
		<Association> _
		Public Property Parent() As HierarchicalResource
			Get
				Return _Parent
			End Get
			Set(ByVal value As HierarchicalResource)
				SetPropertyValue("Parent", _Parent, value)
			End Set
		End Property
		<Association> _
		Public ReadOnly Property Children() As XPCollection(Of HierarchicalResource)
			Get
				Return GetCollection(Of HierarchicalResource)("Children")
			End Get
		End Property
		Public Property Color() As Integer
			Get
				Return _Color
			End Get
			Set(ByVal value As Integer)
				SetPropertyValue("Color", _Color, value)
			End Set
		End Property


		Private ReadOnly Property ITreeNode_Children() As System.ComponentModel.IBindingList Implements ITreeNode.Children
			Get
				Return Children
			End Get
		End Property
		Private ReadOnly Property ITreeNode_Name() As String Implements ITreeNode.Name
			Get
				Return Name
			End Get
		End Property
		Private ReadOnly Property ITreeNode_Parent() As ITreeNode Implements ITreeNode.Parent
			Get
				Return Parent
			End Get
		End Property

		<Browsable(False), NonPersistent> _
		Public Property Caption() As String Implements IResource.Caption
			Get
				Return Name
			End Get
			Set(ByVal value As String)
				Name = value
			End Set
		End Property
		<Browsable(False)> _
		Public ReadOnly Property Id() As Object Implements IResource.Id
			Get
				Return Oid
			End Get
		End Property
		<Browsable(False)> _
		Public ReadOnly Property OleColor() As Integer Implements IResource.OleColor
			Get
				Return System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.FromArgb(Color))
			End Get
		End Property
	End Class
End Namespace
