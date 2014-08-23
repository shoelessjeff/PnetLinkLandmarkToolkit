Imports System.Data.SqlClient
Imports System.Data.Odbc
Imports System.Data


Public Class UpdateLandmark
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Request.QueryString("LandmarkID") = "" Then
            EditorOutput.Text = "<H3><center>No Matching Records found.</center></H3> <p>If you feel you have reached this message in error please open a helpdesk ticket at helpdeskcontact@domain.com</p>"
            Exit Sub
        End If

        Dim lmid As Integer = CInt(Request.QueryString("LandmarkID"))
        Dim lat As Double = CDbl(Request.QueryString("Lat"))
        Dim lng As Double = CDbl(Request.QueryString("Long"))
        Dim StpPrf As Integer = CInt(Request.QueryString("StpPrf"))

        'Single Landmark Validation
        Dim sqlconn As New SqlConnection
        sqlconn.ConnectionString = "Data Source=<SQLSERVERNAME>;Initial Catalog=PnetLink;Persist Security Info=True;User ID=<USERNAME>;Password=<PASSWORD>"
        Dim sqldacmd As New SqlCommand
        sqldacmd.Connection = sqlconn
        sqldacmd.CommandText = "SELECT TOP 1 [landmark_id]       ,[name]      ,[lmtype]      ,[description]      ,[addr1]      ,[addr2]      ,[city]      ,[state]      ,[zip]      ,[custom1] " & _
            ",[latitude]      ,[longitude]      ,[stopprofile_id]   ,[autocorrect] ,customer_id  FROM [PnetLink].[pnet].[landmarks] where [landmark_id] = " & lmid
        Dim sqlda As New SqlDataAdapter
        sqlda.SelectCommand = sqldacmd
        Dim pfmds As New DataSet
        sqlda.Fill(pfmds)
        If pfmds.Tables(0).Rows.Count = 0 Then
            EditorOutput.Text = "<H3><center>No Matching Records found.</center></H3> <center>If you feel you have reached this message in error please <br> open a helpdesk ticket at helpdeskcontact@domain.com<br>  If you recently updated this landmark note that <br> the process assigns a new LandmarkID. <br> Please reload the screen and try again. </center>"
            Exit Sub
        End If

        Dim sqlcmd As New SqlCommand
        sqlcmd.CommandText = "INSERT INTO <SQLSERVERNAME>.PnetLink.Pnet.Commands(Command, Id, Name, Vehicle_Number, Command_Fields) VALUES ('landmark_edit', " & Trim(pfmds.Tables(0).Rows(0).Item("landmark_id")) & _
            ", NULL, NULL,'" & Trim(pfmds.Tables(0).Rows(0).Item("name")) & "^" & Trim(pfmds.Tables(0).Rows(0).Item("lmtype")) & "^" & Trim(pfmds.Tables(0).Rows(0).Item("description")) & _
            "^" & Trim(pfmds.Tables(0).Rows(0).Item("addr1")) & "^" & Trim(pfmds.Tables(0).Rows(0).Item("addr2")) & "^" & Trim(pfmds.Tables(0).Rows(0).Item("city")) & _
            "^" & Trim(pfmds.Tables(0).Rows(0).Item("state")) & "^" & Trim(pfmds.Tables(0).Rows(0).Item("zip")) & "^1" & _
            "^^" & Trim(StpPrf) & "^" & Trim(pfmds.Tables(0).Rows(0).Item("customer_id")) & "^" & Trim(lat) & "^" & Trim(lng) & "^no^0^')"
        sqlcmd.Connection = sqlconn
        sqlconn.Open()
        Dim reccnt As Integer = sqlcmd.ExecuteNonQuery()
        sqlconn.Close()

        Dim sb As New StringBuilder
        sb.Append("<center><H3>Results for the Update of Landmark: " & lmid & "</H3><br><br><p>")
        sb.Append(reccnt & " Records Updated in PeopleNet Landmarks.</p><p>Update finished</p></center>")

        EditorOutput.Text = sb.ToString

    End Sub

End Class